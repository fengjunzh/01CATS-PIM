Imports Ivi.Visa.Interop

Public Class SCPIInstrument
    Inherits Instrument

    Protected ReadOnly Property rMgr() As ResourceManager
        Get
            If _rMgr IsNot Nothing Then
                Return _rMgr
            Else
                Throw New Exception("Access to Visa Session not possible because not intialized")
            End If
        End Get
    End Property
    Protected ReadOnly Property VisaSession() As FormattedIO488
        Get
            If _visaSession IsNot Nothing Then
                Return _visaSession
            Else
                Throw New Exception("Access to Visa Session not possible because not intialized")
            End If
        End Get
    End Property

    '***********************************************
    '*  Private functions and properties 
    '***********************************************
    Private _rMgr As New ResourceManager
    Private _visaSession As FormattedIO488

    ''' <summary>
    ''' Timeout in msec for Visa or Telnet Communication
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Timeout() As Integer
        Get
            Try
                Return _visaSession.IO.Timeout
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                _visaSession.IO.Timeout = value
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property
    Private ReadOnly Property MyModel() As String
        Get
            If MyBase.Model = "" Then
                Return MyBase.Address
            Else
                Return MyBase.Model
            End If
        End Get
    End Property
    Public Overrides Function Open() As Boolean
        'Get Info Instrument
        Dim tmpInfo() As String
        Dim oldTimeout As Integer = 50000
        Try
            '_rMgr = New ResourceManager
            _visaSession = New FormattedIO488
            _visaSession.IO = rMgr.Open(MyBase.Address)
            _visaSession.IO.Timeout = 100000
            oldTimeout = _visaSession.IO.Timeout
            _visaSession.IO.Clear()
            _visaSession.WriteString("*RST", True)
            _visaSession.WriteString("*IDN?", True)
            _IDN = _visaSession.ReadString
            tmpInfo = _IDN.Split(",")
            _Vendor = tmpInfo(0).Trim
            _ModelNumber = tmpInfo(1).Trim
            _FWRevision = tmpInfo(3).Trim
            _SerialNumber = tmpInfo(2).Trim
            _visaSession.WriteString("*OPT?", True)
            _InstalledOptions = _visaSession.ReadString.TrimEnd(vbLf)
            Return True
        Catch exp As InvalidCastException
            Throw New Exception("Selected resource must be a message-based session")
        Catch exp As Exception
            Throw New Exception(String.Format("Got error message during opening address {0}:" +
            Environment.NewLine + "{1}" +
            Environment.NewLine + "SCPI string response: {2}", Me.Address, exp.Message, _IDN))
        Finally
            _visaSession.IO.Timeout = oldTimeout
        End Try
    End Function
    Public Overridable Function FindDevices() As List(Of String)
        'Get Connected Device List
        Dim resList As New List(Of String)
        Try
            Dim resStr As String() = _rMgr.FindRsrc("?*INSTR")
            For Each res As String In resStr
                If res.StartsWith("USB") And res.Contains("0x") Then resList.Add(res)
                'If res.StartsWith("GPIB") Then resList.Add(res)
            Next
            Return resList
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Overrides Sub Close()
        If _visaSession.IO IsNot Nothing Then
            _visaSession.IO.Close()
        End If
    End Sub

    Public Overridable Function SendSCPIComand(ByVal ComandString As String, Optional ByVal IsQueryCmd As Boolean = False) As String
        Dim resp As String = ""
        Dim IsWrite As Boolean = True

        Try
            'Check if query or write
            If ComandString.Contains("?") = True Or IsQueryCmd Then
                IsWrite = False
            End If

            _visaSession.WriteString(ComandString)

            If IsWrite = False Then
                resp = _visaSession.ReadString()
            End If

            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Overridable Function GetError() As String
        Dim resp As String = ""
        Try
            Dim comand As String = ":SYST:ERR?"
            MyBase.GenerateEventSentMessage(MyModel & ",Sent query comand:  """ & comand & """")
            resp = SendSCPIComand(comand)
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Public Overridable Sub Beeper(ByVal isTrue As Boolean)
        Try
            SendSCPIComand(":SYSTem:BEEPer:WARNing:STATe " & IIf(isTrue, 1, 0))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Overridable Sub Reset()
        Try
            SendSCPIComand("*RST")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Overridable Sub ClearError()
        Try
            SendSCPIComand("*CLS;*OPC?")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Overridable Sub Preset()
        Try
            _visaSession.WriteString(":SYST:PRES")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub WaitOPC(ByVal TimeOutSec As String)
        Try
            Dim tmpTimeout As Integer
            tmpTimeout = _visaSession.IO.Timeout
            _visaSession.IO.Timeout = TimeOutSec * 1000
            _visaSession.Query("*OPC?")
            _visaSession.IO.Timeout = tmpTimeout
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class


