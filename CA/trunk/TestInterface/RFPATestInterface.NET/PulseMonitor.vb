
Imports System.Timers
Imports System.Threading

''' <summary>
''' Monitors an integer "pulse" at a fixed interval and raises events:
''' PulseChanged, PulseUnchanged, Stale, RangeAlert, ErrorOccurred.
''' </summary>
Public Class PulseMonitor
    Implements IDisposable

    ' --- Configuration ---
    Public Property PollIntervalMs As Integer = 2000              ' default: 2 seconds
    Public Property StaleTimeoutMs As Integer = 5000              ' if no update for 5s => Stale
    Public Property UnchangedThreshold As Integer = 1             ' N consecutive same values before PulseUnchanged event
    Public Property ValidMin As Integer = Integer.MinValue        ' optional lower bound
    Public Property ValidMax As Integer = Integer.MaxValue        ' optional upper bound

    ' --- Provider of the pulse value ---
    Private ReadOnly _provider As Func(Of Integer)

    ' --- Timers ---
    Private _pollTimer As Timers.Timer
    Private _watchdogTimer As Timers.Timer

    ' --- State ---
    Private _lastObserved As Integer = Integer.MinValue
    Private _lastUpdateUtc As DateTime = DateTime.MinValue
    Private _unchangedCount As Integer = 0
    Private _isRunning As Boolean = False
    Private ReadOnly _sync As New Object()

    ' --- Events ---
    Public Event PulseChanged(oldValue As Integer, newValue As Integer, atUtc As DateTime)
    Public Event PulseUnchanged(value As Integer, repeats As Integer, atUtc As DateTime)
    Public Event Stale(lastUpdateUtc As DateTime, elapsedMs As Integer)
    Public Event RangeAlert(value As Integer, validMin As Integer, validMax As Integer, atUtc As DateTime)
    Public Event ErrorOccurred(ex As Exception)

    ''' <summary>
    ''' Create a PulseMonitor with a provider function that returns the current pulse integer.
    ''' </summary>
    ''' <param name="provider">A function to fetch the pulse value (e.g., read shared variable, serial, network).</param>
    Public Sub New(provider As Func(Of Integer))
        If provider Is Nothing Then Throw New ArgumentNullException(NameOf(provider))
        _provider = provider
    End Sub

    ''' <summary>
    ''' Start polling and watchdog timers.
    ''' </summary>
    Public Sub Start()
        SyncLock _sync
            If _isRunning Then Return
            _pollTimer = New Timers.Timer(PollIntervalMs)
            AddHandler _pollTimer.Elapsed, AddressOf OnPoll
            _pollTimer.AutoReset = True
            _pollTimer.Start()

            _watchdogTimer = New Timers.Timer(Math.Max(1000, Math.Min(2000, StaleTimeoutMs \ 2)))
            AddHandler _watchdogTimer.Elapsed, AddressOf OnWatchdog
            _watchdogTimer.AutoReset = True
            _watchdogTimer.Start()

            _isRunning = True
        End SyncLock
    End Sub

    ''' <summary>
    ''' Stop timers.
    ''' </summary>
    Public Sub StopPulse()
        SyncLock _sync
            If Not _isRunning Then Return
            Try
                _pollTimer?.Stop()
                RemoveHandler _pollTimer.Elapsed, AddressOf OnPoll
                _pollTimer?.Dispose()
            Catch
            End Try
            Try
                _watchdogTimer?.Stop()
                RemoveHandler _watchdogTimer.Elapsed, AddressOf OnWatchdog
                _watchdogTimer?.Dispose()
            Catch
            End Try
            _isRunning = False
        End SyncLock
    End Sub

    Private Sub OnPoll(sender As Object, e As ElapsedEventArgs)
        Try
            Dim nowUtc = DateTime.UtcNow
            Dim current As Integer = _provider.Invoke()

            ' Update last update time (we "received" a new sample)
            _lastUpdateUtc = nowUtc

            ' Range alert (optional)
            If current < ValidMin OrElse current > ValidMax Then
                RaiseEvent RangeAlert(current, ValidMin, ValidMax, nowUtc)
            End If

            ' First observation
            If _lastObserved = Integer.MinValue Then
                _lastObserved = current
                _unchangedCount = 0
                RaiseEvent PulseChanged(Integer.MinValue, current, nowUtc)
                Return
            End If

            ' Compare values
            If current = _lastObserved Then
                _unchangedCount += 1
                If _unchangedCount >= UnchangedThreshold Then
                    RaiseEvent PulseUnchanged(current, _unchangedCount, nowUtc)
                End If
            Else
                Dim old = _lastObserved
                _lastObserved = current
                _unchangedCount = 0
                RaiseEvent PulseChanged(old, current, nowUtc)
            End If
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex)
        End Try
    End Sub

    Private Sub OnWatchdog(sender As Object, e As ElapsedEventArgs)
        Try
            If _lastUpdateUtc = DateTime.MinValue Then
                ' No samples yet; consider stale
                RaiseEvent Stale(DateTime.MinValue, StaleTimeoutMs)
                Return
            End If

            Dim elapsedMs As Integer = CInt((DateTime.UtcNow - _lastUpdateUtc).TotalMilliseconds)
            If elapsedMs > StaleTimeoutMs Then
                RaiseEvent Stale(_lastUpdateUtc, elapsedMs)
            End If
        Catch ex As Exception
            RaiseEvent ErrorOccurred(ex)
        End Try
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Stop
    End Sub
End Class