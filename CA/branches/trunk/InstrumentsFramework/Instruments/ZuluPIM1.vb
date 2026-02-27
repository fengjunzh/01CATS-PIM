Imports Renci.SshNet
Imports Renci.SshNet.Common
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Linq
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class ZuluPIM
    Inherits Instrument
    Implements IIMDDevice

    Private userName As String = "root"
    Private passWord As String = "ION4you1"
    Private expectEnd As String = "dSMR>"
    Private _SshClient As SshClient = Nothing
    Private _timeOutSec As Integer = 5
    Private _ShellStream As ShellStream = Nothing
    Private _P1, _P2, _F1, _F2 As Double
    Private _PowerPA As Single
    Private _ImdBoxMode As String = ""
    Private _bandName As String
    Private _currentBandName As String
    Private _filterBandName As String
    Private _bySinglePointsFlag As Boolean = False
    Public Overrides Sub Close() Implements IIMDDevice.Close
        If _ShellStream IsNot Nothing Then
            _ShellStream.Close()
            _ShellStream = Nothing

        End If
        If _SshClient IsNot Nothing Then
            _SshClient.Disconnect()
            _SshClient = Nothing
        End If
    End Sub
    ' sweep down is true ,up is set false 
    Private _ImdOrder As Integer = 3
    Public Property ImdOrder As Integer Implements IIMDDevice.ImdOrder
        Get
            Return _ImdOrder
        End Get
        Set(value As Integer)
            Send_And_Read(String.Format("pimtest rx0 filtmode {0} {1}", "0x103", 10))
            Send_And_Read(String.Format("pimsweep order {0}", value))
            Send_And_Read(String.Format("pim2tone order {0}", value))
            _ImdOrder = value
        End Set
    End Property
    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
        Try
            Dim connInfo As New PasswordConnectionInfo(Me.Address, Me.userName, Me.passWord)
            _SshClient = New SshClient(connInfo)
            If _SshClient.IsConnected = False Then
                _SshClient.Connect()
            End If
            _ShellStream = _SshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024)
            Send_And_Read("/etc/init.d/watchdog stop;/opt/andrew/bin/console")
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private ReadOnly Property Version() As String
        Get
            Try
                Dim verStr As String = Send_And_Read(String.Format("ver"))
                Return verStr
            Catch ex As Exception
                Throw New Exception("Version()::" & ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property FilterBandName()
        Get
            Dim temStr As String = Send_And_Read(String.Format("lna0mcu eedata v"))
            Dim ma As Match = Regex.Match(temStr, "[sS]erial_number: (\S+)")
            Dim maBand As Match = Regex.Match(temStr, "band_name(?:_\d+)?: (\S+)")
            Dim temfw As String = Send_And_Read(String.Format("ver"))
            Dim maFw As Match = Regex.Match(temfw, "version: (\S+)")
            If ma.Success = False Then
                _SerialNumber = "NULLB000"
            Else
                _SerialNumber = ma.Groups(1).Value
            End If

            If maFw.Success = False Then
                _FWRevision = ""
            Else
                _FWRevision = maFw.Groups(1).Value
            End If
            If Trim(maBand.Groups(1).Value.ToUpper).Contains("LTE700L") Or Trim(maBand.Groups(1).Value.ToUpper).Contains("LTE700U") Then
                _filterBandName = "LTE700LU"
            Else
                _filterBandName = Trim(maBand.Groups(1).Value.ToUpper)
            End If
            _ModelNumber = "ZuluPimTester " & _filterBandName
            Return _filterBandName
        End Get
    End Property
    Private ReadOnly Property GetCurrentBandName As String
        Get
            Dim temStr As String = Send_And_Read(String.Format("bandconfig v"))
            'temStr = Send_And_Read(String.Format("bandconfig v"))
            Dim ma As Match = Regex.Match(temStr, "Current band: (\S+)")
            If ma.Success = False Then Return Nothing
            Return Trim(ma.Groups(1).Value.ToUpper)
        End Get
    End Property
    Private Function ReadSinglePointsRssi() As Double
        Try
            Dim resp As String

            resp = Send_And_Read(String.Format("pimsweep {0} -10 {1} -10", _F1, _F2))
            Dim ms As MatchCollection = Regex.Matches(resp, "rssi=\S+dBm, rssi2=(\S+)dBm")
            Dim imdvalues(ms.Count \ 2 - 1) As Double
            For i As Integer = 0 To imdvalues.Length - 1
                imdvalues(i) = ms(i).Groups(1).Value
            Next

            Return imdvalues.Max()
        Catch ex As Exception
            Throw New Exception("ZuluPIM.ReadSinglePointsRssi()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Function
    Private Function ReadSweepFreqRssi() As Double

        Dim patten As String = ""
        Dim res(1) As Double
        Dim resstr As String
        patten += "RX0 ch A, freq=(\S+)" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm"
        resstr = _ShellStream.Expect(New Regex(patten), TimeSpan.FromSeconds(5))
        If resstr Is Nothing Then Return Nothing
        GenerateEventSentMessage(resstr)
        Dim ma As Match = Regex.Match(resstr, patten)
        If ma.Success = False Then Return Nothing
        res(0) = ma.Groups(1).Value
        Dim imdval(2) As Double
        For i As Integer = 0 To imdval.Length - 1
            If Double.TryParse(ma.Groups(2 + i).Value, imdval(i)) = False Then
                imdval(i) = Double.MinValue
            End If
        Next
        res(1) = imdval(0)
        For i As Integer = 1 To imdval.Length - 1
            res(1) = Math.Max(res(1), imdval(i))
        Next
        Return res(1)
    End Function
    Private Function ReadSweepTime() As Double

        Dim patten As String = ""
        patten += "RX0 ch A, freq=(\S+)" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm"
        Dim resstr As String = _ShellStream.Expect(New Regex(patten), TimeSpan.FromSeconds(1.1))
        If resstr Is Nothing Then Return Nothing
        GenerateEventSentMessage(resstr)
        Dim ma As Match = Regex.Match(resstr, patten)
        If ma.Success = False Then Return Nothing
        Dim res(1) As Double
        res(0) = ma.Groups(1).Value
        Dim imdval As Double
        If Double.TryParse(ma.Groups(2).Value, imdval) Then
            res(1) = imdval
        Else
            res(1) = Double.MinValue
        End If
        Return res(1)
    End Function
    Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
        Get
            Try
                Dim resi As Double
                If _bySinglePointsFlag = True Then
                    resi = ReadSinglePointsRssi() - _PowerPA
                Else
                    If _ImdBoxMode = "UPSWEEP" OrElse _ImdBoxMode = "DOWNSWEEP" Then
                        resi = ReadSweepFreqRssi() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweepfrequency : Can't read validate value .")
                    ElseIf _ImdBoxMode = "OVERTIME" Then
                        resi = ReadSweepTime() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweeptime : Can't read validate value .")
                    End If
                End If
                'antenna need set the limmter
                'If resi < -175 Then
                '    resi = -175
                'End If
                'Return Math.Round(resi, 3)
                Return resi
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
        Get
            Try
                Dim resi As Double
                If _bySinglePointsFlag = True Then
                    resi = ReadSinglePointsRssi() - _PowerPA
                Else
                    If _ImdBoxMode = "UPSWEEP" OrElse _ImdBoxMode = "UPSWEEP" Then
                        resi = ReadSweepFreqRssi() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweepfrequency : Can't read validate value .")
                    ElseIf _ImdBoxMode = "OVERTIME" Then
                        resi = ReadSweepTime() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweeptime : Can't read validate value .")
                    End If
                End If
                'antenna need set the limmter
                'If resi < -132 Then
                '    resi = -132
                'End If
                'Return Math.Round(resi, 3)
                Return resi
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property
    Public Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        If OnOff1 = True And OnOff2 = False Then
            If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
            Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, -10, _F2, -100))
            Threading.Thread.Sleep(500)
        End If
        If OnOff1 = False And OnOff2 = True Then
            If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
            Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, -100, _F2, -10))
            Threading.Thread.Sleep(500)
        End If
        If OnOff1 = False And OnOff2 = False Then
            Send_And_Read("pimtest req txoff")
        End If

        'PA_OnOff(0, OnOff1) = OnOff1
        'PA_OnOff(1, OnOff2) = OnOff2
    End Sub

    Public Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
        PA_OnOff(0) = OnOff
        PA_OnOff(1) = OnOff
    End Sub
    Private Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        Try
            _ShellStream.WriteLine(cmd)
            Dim res As String = _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(_timeOutSec))
            GenerateEventSentMessage(res)
            Return res
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SetFrequency(freqMHz1 As Double, freqMHz2 As Double) Implements IIMDDevice.SetFrequency
        _F1 = Math.Round(freqMHz1, 1)
        _F2 = Math.Round(freqMHz2, 1)

    End Sub

    Public Sub SetFrequency(Port As IIMDDevice.enumRFPORTS, freqMHz As Double) Implements IIMDDevice.SetFrequency
        Select Case Port
            Case IIMDDevice.enumRFPORTS.PORTA
                _F1 = Math.Round(freqMHz, 1)
            Case IIMDDevice.enumRFPORTS.PORTB
                _F2 = Math.Round(freqMHz, 1)
        End Select
    End Sub
    Public Sub SetRFPower(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
        _PowerPA = Math.Round(PowerDBM1, 2)
        SetRFPower(IIMDDevice.enumRFPORTS.PORTA, PowerDBM1)
        SetRFPower(IIMDDevice.enumRFPORTS.PORTB, PowerDBM2)
    End Sub

    Public Sub SetRFPower(Port As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
        PowerDBM -= 53
        Dim MaxPower As Double = -2
        If PowerDBM > MaxPower Then PowerDBM = MaxPower
        Select Case Port
            Case IIMDDevice.enumRFPORTS.PORTA
                _P1 = PowerDBM
            Case IIMDDevice.enumRFPORTS.PORTB
                _P2 = PowerDBM
        End Select
    End Sub
    WriteOnly Property PA_OnOff(ByVal i As Integer, Optional IsBiasChange As Boolean = True) As Boolean
        Set(value As Boolean)
            If value = False AndAlso _ImdBoxMode.Contains("SWEEP") = False Then
                Send_And_Read("pimtest req txoff")
            End If
        End Set
    End Property
    'Public Function GetRFPower() As Double() Implements IIMDDevice.GetRFPower
    '    Try
    '        Dim resp(1) As Double
    '        resp(0) = _PowerPA
    '        resp(1) = _PowerPA
    '        Return resp
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function

    Private Function OverTimeMode(ByVal band As String) As Boolean
        'overtime point set as like below
        'Sweep down is true,Sweep up is false

        If band = "LTE600" Then Return False
        If band = "LTE700L" Then Return True
        If band = "LTE700U" Then Return False
        Return True
    End Function

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp
        Try
            _bySinglePointsFlag = True
            _ImdBoxMode = ImdBoxMode.Trim.ToUpper
            SetSelectPA(_ImdBoxMode)
            Send_And_Read("pimtest sweep mode 3")
            Send_And_Read(String.Format("pimsweep {0} -10 {1} -10", startF, stopF)) 'R&D ask to send the first point which is in the band 

        Catch ex As Exception
            Throw New Exception("RFPowerRampUp() " & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Sub RFPowerRampUp(startF0 As Single, fixF1 As Single, ImdBoxMode As String, Optional stopF0 As Single = 0, Optional stepF0 As Single = 0, Optional duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp
        Try
            Dim cmd As String
            _bySinglePointsFlag = False
            _ImdBoxMode = ImdBoxMode.Trim.ToUpper
            SetSelectPA(_ImdBoxMode)
            If startF0 = 0 Or fixF1 = 0 Then Throw New Exception("The parameters : startF0 = " & startF0 & " Or fixF1 = " & fixF1 & " is not correct ")
            If _ImdBoxMode <> "OVERTIME" Then
                _timeOutSec = 30
                cmd = String.Format("pimsweep {0} {1} {2} {4} {3} {5}", startF0, stopF0, stepF0, fixF1, _P1, _P2)
            Else
                Send_And_Read(String.Format("pim2tone gldelay 3"))
                cmd = String.Format("pim2tone {0} {1} {2} {3} {4} {5}", startF0, _P1, fixF1, _P2, duration_Sec, 0.1)
            End If
            _ShellStream.WriteLine(cmd)
            GenerateEventSentMessage(cmd & vbCrLf)
        Catch ex As Exception
            Send_And_Read("pimtest req txoff")
            Throw New Exception("RFPowerRampUp() " & vbCrLf & ex.Message)
        End Try
    End Sub
    Private Sub SetSelectPA(ByVal temImdBoxMode As String)

        Select Case temImdBoxMode
            Case "OVERTIME"
                Send_And_Read(String.Format("pimsweep init sp ", IIf(OverTimeMode(_bandName), "down", "up")))
            Case "UPSWEEP"
                Send_And_Read("pimsweep init mp up")
            Case "DOWNSWEEP"
                Send_And_Read("pimsweep init mp down")
        End Select
    End Sub
    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Return _bandName
        End Get
        Set(value As String)
            If value.ToString.ToUpper = "DIG-DIV850" Then value = "DIG-DIV"
            If value.ToString.ToUpper = "1400" Then value = "1400M"
            If value.ToString.ToUpper = "600" Then value = "LTE600"
            _currentBandName = GetCurrentBandName
            If _currentBandName = value Then
                Exit Property
            End If
            Send_And_Read("Pimtest gainloop tx0 off")
            Send_And_Read("Pimtest gainloop tx1 off")
            Send_And_Read("pimtest req txoff")
            _timeOutSec = 30
            Send_And_Read(String.Format("bandconfig {0}", value))
            Threading.Thread.Sleep(30)
            _bandName = value
        End Set
    End Property
    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd
        If _bySinglePointsFlag = False Then
            Dim res As String = _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(5))
            GenerateEventSentMessage(res)
        End If

    End Sub

    Public Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort

    End Sub

    Public Sub SetTestMode(Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode

    End Sub
    Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc

    End Sub

    Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm

    End Sub
    Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
        Get

        End Get
    End Property

    Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
        Get

        End Get
    End Property

    Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

    End Sub

    Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Try
            Dim dtpStr As String = Send_And_Read(String.Format("dist2pim sweep"))
            Return dtpStr
        Catch ex As Exception
            Throw New Exception("ReadDTP()::" & ex.Message)
        End Try
    End Function

    Public Sub SetPIMFreqMHz(freqMHzIM As Double) Implements IIMDDevice.SetPIMFreqMHz

    End Sub

    Public Function GetRFPower() As Double() Implements IIMDDevice.GetRFPower
        Throw New NotImplementedException()
    End Function

    Public Property FreqBand As Integer Implements IIMDDevice.FreqBand
        Get
            'Return _BandID
        End Get
        Set(value As Integer)

        End Set
    End Property

End Class


