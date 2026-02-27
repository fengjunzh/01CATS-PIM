Imports NationalInstruments.VisaNS
Public Class SCPIInstrument
    Inherits Instrument

    Enum CommunicationsBus
        VISA = 0
        SCPI_TELNET = 1
    End Enum


    Protected ReadOnly Property VisaSession() As MessageBasedSession
        Get
            If _VisaSession IsNot Nothing Then
                Return _VisaSession
            Else
                Throw New Exception("Access to Visa Session not possible because not intialized")
            End If
        End Get
    End Property

    Protected ReadOnly Property TelnetSession() As SCPITelnetDevice
        Get
            If _TelnetSession IsNot Nothing Then
                Return _TelnetSession
            Else
                Throw New Exception("Access to Telnet Session not possible because not intialized")
            End If
        End Get
    End Property
    '***********************************************
    '*  Private functions and properties 
    '***********************************************
    Private _SessionMode As CommunicationsBus = CommunicationsBus.VISA
    Private _VisaSession As MessageBasedSession = Nothing
    Private WithEvents _TelnetSession As SCPITelnetDevice = Nothing
    Private _TelnetPort As Integer

    '***********************************************
    '*  Public functions and properties 
    '***********************************************
    Public Property CommunicationsMode() As CommunicationsBus
        Get
            Return _SessionMode
        End Get
        Set(ByVal value As CommunicationsBus)
            _SessionMode = value
        End Set
    End Property
    Public Property SocketPort() As Integer
        Get
            Return _TelnetPort
        End Get
        Set(ByVal value As Integer)
            _TelnetPort = value
        End Set
    End Property
    ''' <summary>
    ''' Timeout in msec for Visa or Telnet Communication
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Timeout() As Integer
        Get
            Try
                If _SessionMode = CommunicationsBus.SCPI_TELNET Then
                    Return _TelnetSession.TimeOutSec * 1000
                Else
                    Return _VisaSession.Timeout
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
        Set(ByVal value As Integer)
            Try
                If _SessionMode = CommunicationsBus.SCPI_TELNET Then
                    _TelnetSession.TimeOutSec = CInt(value / 1000)
                Else
                    _VisaSession.Timeout = value
                End If
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
        Dim SCPIresp As String = ""
        Dim oldTimeout As Integer
        Try
            Select Case _SessionMode
                Case CommunicationsBus.VISA
                    _VisaSession = CType(ResourceManager.GetLocalManager().Open(MyBase.Address), MessageBasedSession)
                    MyBase.GenerateEventSentMessage(String.Format("Visa Manufacturer: {0}, Visa ID: {1}, HW Interface Name: {2}", _VisaSession.ResourceManufacturerName, _VisaSession.ResourceManufacturerID, _VisaSession.HardwareInterfaceName))
                    oldTimeout = _VisaSession.Timeout
                    _VisaSession.Timeout = 10000 '
                    If _VisaSession.HardwareInterfaceType = HardwareInterfaceType.Serial Then
                        Dim x As SerialSession = CType(_VisaSession, SerialSession)
                        x.ReadTermination = SerialTerminationMethod.TerminationCharacter
                        x.WriteTermination = SerialTerminationMethod.TerminationCharacter
                        'x.TerminationCharacter = vbLf
                    End If
                    ClearError()
                    'Threading.Thread.Sleep(2000)
                    SCPIresp = SendSCPIComand("*IDN?")
                    _VisaSession.Timeout = oldTimeout
                    tmpInfo = SCPIresp.Split(","c)
                    _Vendor = tmpInfo(0).Trim
                    _ModelNumber = tmpInfo(1).Trim
                    _FWRevision = tmpInfo(3).Trim
                    _SerialNumber = tmpInfo(2).Trim
                Case CommunicationsBus.SCPI_TELNET
                    If _TelnetSession Is Nothing Then
                        _TelnetSession = New SCPITelnetDevice
                        _TelnetSession.Port = _TelnetPort
                        _TelnetSession.Address = MyBase.Address
                        _TelnetSession.Open()
                        ClearError()
                        oldTimeout = _TelnetSession.TimeOutSec
                        _TelnetSession.TimeOutSec = 30
                        SCPIresp = SendSCPIComand("*IDN?")
                        tmpInfo = SCPIresp.Split(","c)
                        _Vendor = tmpInfo(0).Trim
                        _ModelNumber = tmpInfo(1).Trim
                        _FWRevision = tmpInfo(3).Trim
                        _SerialNumber = tmpInfo(2).Trim

                    End If
            End Select

            Return True
        Catch exp As InvalidCastException
            Throw New Exception("Selected resource must be a message-based session")
        Catch exp As Exception
            Throw New Exception(String.Format("Got error message during opening address {0}:" + _
            Environment.NewLine + "{1}" + _
            Environment.NewLine + "SCPI string response: {2}", Me.Address, exp.Message, SCPIresp))
        Finally
            If _SessionMode = CommunicationsBus.VISA Then
                _VisaSession.Timeout = oldTimeout
            ElseIf _SessionMode = CommunicationsBus.SCPI_TELNET Then
                _TelnetSession.TimeOutSec = oldTimeout
            End If
        End Try
    End Function

    Public Overrides Sub Close()
        Select Case _SessionMode
            Case CommunicationsBus.VISA
                If _VisaSession IsNot Nothing Then
                    _VisaSession.Dispose()
                End If
            Case CommunicationsBus.SCPI_TELNET
                If _TelnetSession IsNot Nothing Then
                    _TelnetSession.Close()
                End If
        End Select
    End Sub

    Public Overridable Function SendSCPIComand(ByVal ComandString As String) As String
        Dim resp As String = ""
        Dim IsWrite As Boolean = True

        Try
            'Check if query or write
            If ComandString.Contains("?") = True Then
                IsWrite = False
            End If
            Select Case _SessionMode
                Case CommunicationsBus.SCPI_TELNET
                    If IsWrite Then
                        resp = TelnetWrite(ComandString)
                    Else
                        resp = TelnetQuery(ComandString)
                    End If
                Case CommunicationsBus.VISA
                    If IsWrite Then
                        resp = VisaWrite(ComandString)
                    Else
                        resp = VisaQuery(ComandString)
                    End If
            End Select
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Overridable Function GetError() As String
        Dim resp As String = ""
        Try
            'Dim comand As String = ":SYST:ERR?"
            'MyBase.GenerateEventSentMessage(MyModel & ",Sent query comand:  """ & comand & """")
            'Select Case _SessionMode
            '    Case CommunicationsBus.SCPI_TELNET
            '        resp = _TelnetSession.SendComand(comand)
            '    Case CommunicationsBus.VISA
            '        resp = _VisaSession.Query(comand)
            'End Select
            'Return resp
            Return "+0,""No error"""
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

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
            SendSpecialSCPICommand(":SYST:PRES")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub WaitOPC(ByVal TimeOutSec As Double)
        Try
            Select Case _SessionMode
                Case CommunicationsBus.SCPI_TELNET
                    'TO IMPLEMENT
                Case CommunicationsBus.VISA
                    Dim tmpTimeout As Integer
                    tmpTimeout = _VisaSession.Timeout
                    _VisaSession.Timeout = CInt(TimeOutSec * 1000)
                    _VisaSession.Query("*OPC?")
                    _VisaSession.Timeout = tmpTimeout
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '*****************************************
    '*  Private Functions
    '*****************************************
    Private Sub _TelnetSessionMessageReceivedEventHandler(ByVal Message As String) Handles _TelnetSession.DataArrived
        MyBase.GenerateEventSentMessage(Message)
    End Sub
    Public Function ReadSCPI() As String
        Try
            Select Case _SessionMode
                Case CommunicationsBus.SCPI_TELNET
                    Return _TelnetSession.SendComand(" ")
                Case CommunicationsBus.VISA
                    Return _VisaSession.ReadString()
                Case Else
                    Throw New Exception(String.Format("Session Mode {0} not implemented", Convert.ToInt32(_SessionMode)))
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Sub SendSpecialSCPICommand(ByVal CommandString As String)
        Try
            Select Case _SessionMode
                Case CommunicationsBus.SCPI_TELNET
                    _TelnetSession.SendComand(CommandString)
                    Exit Select
                Case CommunicationsBus.VISA
                    _VisaSession.Write(CommandString)
                    Exit Select
            End Select
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
  End Sub

  Public Function ReadDefiniteLengthBlockHeader() As Integer
    Dim tmp As String, count As Integer
    _VisaSession.Write(Command)

    'first read the number of bytes in the header
    tmp = _VisaSession.ReadString(2)
    If Left(tmp, 1) = "#" And Right(tmp, 1) Like "[0-9]" Then
      count = CInt(Right(tmp, 1))
    Else
      Throw New Exception("Invalid definite lenth block header: " & tmp)
    End If

    'next, read the header which contains the number of bytes in the data section
    tmp = _VisaSession.ReadString(count)
    count = CInt(tmp)

    Return count
  End Function

  Public Function ReadDefiniteLengthBlock(ByVal cmd As String) As Byte()
    _VisaSession.Write(cmd)
    Dim count As Integer = ReadDefiniteLengthBlockHeader()
    Return _VisaSession.ReadByteArray(count)
  End Function

  Private Function VisaWrite(ByVal Comand As String) As String
    Dim ErrParameters() As String
    Dim err As String
    Try
      MyBase.GenerateEventSentMessage(MyModel & ",Sent write comand:  """ & Comand & """")
      _VisaSession.Write(Comand)
      err = Me.GetError.TrimEnd(vbLf.ToCharArray)
      ErrParameters = err.Split(","c)
      MyBase.GenerateEventSentMessage(MyModel & ",Received:  " & err)
      If ErrParameters(0) <> "+0" And ErrParameters(0) <> "0" Then
        Throw New Exception("Error in " & _ModelNumber & _
         vbCrLf & "Address: " & MyBase.Address & _
         vbCrLf & "Sent comand: " & Comand & _
         vbCrLf & "Received error number :" & ErrParameters(0) & _
         vbCrLf & "Received error string :" & ErrParameters(1))
      End If
      Return ErrParameters(0)
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
    Private Function VisaQuery(ByVal Comand As String) As String
        Dim resp As String
        Dim ErrParameters() As String
        Dim err As String
        Try
            MyBase.GenerateEventSentMessage(MyModel & ",Sent query comand:  """ & Comand & """")
            resp = _VisaSession.Query(Comand).TrimEnd(vbLf.ToCharArray )
            MyBase.GenerateEventSentMessage(MyModel & ",Received:  " & resp)
            'Check Error during query
            err = Me.GetError.TrimEnd(vbLf.ToCharArray)
            ErrParameters = err.Split(","c)
            MyBase.GenerateEventSentMessage(MyModel & ",Received:  " & err)
            If ErrParameters(0) <> "+0" And ErrParameters(0) <> "0" Then
                Throw New Exception("Error in " & _ModelNumber & _
                                 vbCrLf & "Address: " & MyBase.Address & _
                                 vbCrLf & "Sent comand: " & Comand & _
                                 vbCrLf & "Received error number :" & ErrParameters(0) & _
                                 vbCrLf & "Received error string :" & ErrParameters(1))
            End If
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Function TelnetWrite(ByVal Comand As String) As String
        Dim ErrParameters() As String
        Dim err As String

        Try
            MyBase.GenerateEventSentMessage("Sent write comand to " & Address & ":  """ & Comand & """")
            _TelnetSession.SendComand(Comand)
            err = Me.GetError.TrimEnd(vbLf.ToCharArray)
            ErrParameters = err.Split(","c)
            MyBase.GenerateEventSentMessage("Received:  " & err)
            If ErrParameters(0) <> "+0" Then
                Throw New Exception("Error in " & _ModelNumber & _
                 vbCrLf & "Address: " & MyBase.Address & _
                 vbCrLf & "Sent comand: " & Comand & _
                 vbCrLf & "Received error number :" & ErrParameters(0) & _
                 vbCrLf & "Received error string :" & ErrParameters(1))
            End If
            Return ErrParameters(0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Function TelnetQuery(ByVal Comand As String) As String
        Dim resp As String
        Dim ErrParameters() As String
        Dim err As String
        Try
            MyBase.GenerateEventSentMessage("Sent write comand to " & Address & ":  """ & Comand & """")
            resp = _TelnetSession.SendComand(Comand).TrimEnd(vbLf.ToCharArray)
            MyBase.GenerateEventSentMessage("Received:  " & resp)
            'Check Error during query
            err = Me.GetError.TrimEnd(vbLf.ToCharArray)
            ErrParameters = err.Split(","c)
            MyBase.GenerateEventSentMessage("Received:  " & err)
            If ErrParameters(0) <> "+0" Then
                Throw New Exception("Error in " & _ModelNumber & _
                 vbCrLf & "Address: " & MyBase.Address & _
                 vbCrLf & "Sent comand: " & Comand & _
                 vbCrLf & "Received error number :" & ErrParameters(0) & _
                 vbCrLf & "Received error string :" & ErrParameters(1))
            End If
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Sub WriteByteArrayFromFile(ByVal filename As String, ByVal Swap As Boolean)
        Dim streamfile As System.IO.Stream = Nothing
        Dim byteArray() As Byte = Nothing
        Dim strData As String = ""
        Dim tmpbuffersize As Integer
        Try
            streamfile = System.IO.File.Open(filename, IO.FileMode.Open)
            byteArray = New Byte(CInt(streamfile.Length) - 1) {}
            streamfile.Read(byteArray, 0, CInt(streamfile.Length))

            If Swap Then
                For i As Integer = 0 To byteArray.Length - 1 Step 2
                    tmpbuffersize = byteArray(i)
                    byteArray(i) = byteArray(i + 1)
                    byteArray(i + 1) = CByte(tmpbuffersize)
                Next
            End If
            '1252 is MAGIC
            strData = System.Text.Encoding.GetEncoding(1252).GetString(byteArray, 0, byteArray.Length)
            Select Case _SessionMode
                Case CommunicationsBus.SCPI_TELNET
                    Throw New Exception("WriteByteArrayFromFile not Implemented for Telnet Instrument Session!")
                    Exit Select
                Case CommunicationsBus.VISA
                    tmpbuffersize = Me.VisaSession.DefaultBufferSize
                    Me.VisaSession.DefaultBufferSize = strData.Length
                    Me.VisaSession.Write(strData)
                    Me.VisaSession.DefaultBufferSize = tmpbuffersize
                    Exit Select
            End Select

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If streamfile IsNot Nothing Then
                streamfile.Close()
            End If
        End Try

    End Sub

    Public Sub New()

    End Sub
End Class

Public Class ARPSInstrument
  Inherits Instrument

  Enum CommunicationsBus
    VISA = 0
    SCPI_TELNET = 1
  End Enum


  Protected ReadOnly Property VisaSession() As MessageBasedSession
    Get
      If _VisaSession IsNot Nothing Then
        Return _VisaSession
      Else
        Throw New Exception("Access to Visa Session not possible because not intialized")
      End If
    End Get
  End Property
  Protected ReadOnly Property TelnetSession() As SCPITelnetDevice
    Get
      If _TelnetSession IsNot Nothing Then
        Return _TelnetSession
      Else
        Throw New Exception("Access to Telnet Session not possible because not intialized")
      End If
    End Get
  End Property
  '***********************************************
  '*  Private functions and properties 
  '***********************************************
  Private _SessionMode As CommunicationsBus = CommunicationsBus.VISA
  Private _VisaSession As MessageBasedSession = Nothing
  Private WithEvents _TelnetSession As SCPITelnetDevice = Nothing
  Private _TelnetPort As Integer

  '***********************************************
  '*  Public functions and properties 
  '***********************************************
  Public Property CommunicationsMode() As CommunicationsBus
    Get
      Return _SessionMode
    End Get
    Set(ByVal value As CommunicationsBus)
      _SessionMode = value
    End Set
  End Property
  Public Property SocketPort() As Integer
    Get
      Return _TelnetPort
    End Get
    Set(ByVal value As Integer)
      _TelnetPort = value
    End Set
  End Property
  ''' <summary>
  ''' Timeout in msec for Visa or Telnet Communication
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property Timeout() As Integer
    Get
      Try
        If _SessionMode = CommunicationsBus.SCPI_TELNET Then
          Return _TelnetSession.TimeOutSec * 1000
        Else
          Return _VisaSession.Timeout
        End If
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Get
    Set(ByVal value As Integer)
      Try
        If _SessionMode = CommunicationsBus.SCPI_TELNET Then
          _TelnetSession.TimeOutSec = CInt(value / 1000)
        Else
          _VisaSession.Timeout = value
        End If
      Catch ex As Exception
        Throw New Exception(ex.Message)
      End Try
    End Set
  End Property
    Public Overrides Function Open() As Boolean
        'Get Info Instrument
        Dim tmpInfo() As String
        Dim SCPIresp As String = ""
        Dim oldTimeout As Integer
        Try
            Select Case _SessionMode
                Case CommunicationsBus.VISA
                    _VisaSession = CType(ResourceManager.GetLocalManager().Open(MyBase.Address), MessageBasedSession)
                    MyBase.GenerateEventSentMessage(String.Format("Visa Manufacturer: {0}, Visa ID: {1}, HW Interface Name: {2}", _VisaSession.ResourceManufacturerName, _VisaSession.ResourceManufacturerID, _VisaSession.HardwareInterfaceName))
                    oldTimeout = _VisaSession.Timeout
                    _VisaSession.Timeout = 30000
                    SCPIresp = SendSCPIComand("ID?")
                    _VisaSession.Timeout = oldTimeout
                    SCPIresp = SCPIresp.Replace("  ", " ")
                    tmpInfo = SCPIresp.Split(" "c)
                    '_ModelNumber = tmpInfo(1).Trim
                    '_FWRevision = tmpInfo(3).Trim
                Case CommunicationsBus.SCPI_TELNET
            End Select

            Return True
        Catch exp As InvalidCastException
            Throw New Exception("Selected resource must be a message-based session")
        Catch exp As Exception
            Throw New Exception(String.Format("Got error message during opening address {0}:" + _
            Environment.NewLine + "{1}" + _
            Environment.NewLine + "SCPI string response: {2}", Me.Address, exp.Message, SCPIresp))
        Finally
            If _SessionMode = CommunicationsBus.VISA Then
                _VisaSession.Timeout = oldTimeout
            ElseIf _SessionMode = CommunicationsBus.SCPI_TELNET Then
                _TelnetSession.TimeOutSec = oldTimeout
            End If
        End Try
    End Function

  Public Overrides Sub Close()
    Select Case _SessionMode
      Case CommunicationsBus.VISA
        If _VisaSession IsNot Nothing Then
          _VisaSession.Dispose()
        End If
      Case CommunicationsBus.SCPI_TELNET
        If _TelnetSession IsNot Nothing Then
          _TelnetSession.Close()
        End If
    End Select
  End Sub

  Public Overridable Function SendSCPIComand(ByVal ComandString As String) As String
    Dim resp As String = ""
    Dim IsWrite As Boolean = True

    Try
      'Check if query or write
      If ComandString.Contains("?") = True Then
        IsWrite = False
      End If
      Select Case _SessionMode
        Case CommunicationsBus.SCPI_TELNET
          Exit Select
        Case CommunicationsBus.VISA
          If IsWrite Then
            resp = VisaWrite(ComandString)
          Else
            resp = VisaQuery(ComandString)
          End If
      End Select
      Return resp
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function

  Public Overridable Function GetError() As String
    Dim resp As String = ""
    Try
      Dim comand As String = "ERR?"
      MyBase.GenerateEventSentMessage("Sent query comand to " & Address & ":  """ & comand & """")
      Select Case _SessionMode
        Case CommunicationsBus.SCPI_TELNET
          resp = _TelnetSession.SendComand(comand)
        Case CommunicationsBus.VISA
          resp = _VisaSession.Query(comand)
      End Select
      Return resp
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try

  End Function

  Public Overridable Sub Reset()
    Try
      SendSCPIComand("CLR")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Overridable Sub ClearError()
  End Sub

  Public Overridable Sub Preset()
    Try
      SendSpecialSCPICommand("CLR")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub WaitOPC(ByVal TimeOutSec As String)
    Try
      Select Case _SessionMode
        Case CommunicationsBus.SCPI_TELNET
          'TO IMPLEMENT
        Case CommunicationsBus.VISA
      End Select
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  '*****************************************
  '*  Private Functions
  '*****************************************
  Private Sub _TelnetSessionMessageReceivedEventHandler(ByVal Message As String) Handles _TelnetSession.DataArrived
    MyBase.GenerateEventSentMessage(Message)
  End Sub
  Public Function ReadSCPI() As String
    Try
      Select Case _SessionMode
        Case CommunicationsBus.SCPI_TELNET
          Return _TelnetSession.SendComand(" ")
        Case CommunicationsBus.VISA
          Return _VisaSession.ReadString()
        Case Else
          Throw New Exception(String.Format("Session Mode {0} not implemented", Convert.ToInt32(_SessionMode)))
      End Select
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
  Public Sub SendSpecialSCPICommand(ByVal CommandString As String)
    Try
      Select Case _SessionMode
        Case CommunicationsBus.SCPI_TELNET
          Exit Select
        Case CommunicationsBus.VISA
          _VisaSession.Write(CommandString)
          Exit Select
      End Select
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Private Function VisaWrite(ByVal Comand As String) As String
    Dim ErrParameter As String
    Dim err As String
    Try
      MyBase.GenerateEventSentMessage("Sent write comand to " & Address & ":  """ & Comand & """")
      _VisaSession.Write(Comand)
      err = Me.GetError.TrimEnd(vbLf.ToCharArray)
      ErrParameter = err.Substring(err.IndexOf(" ")).Trim()
      MyBase.GenerateEventSentMessage("Received:  " & err)
      If ErrParameter <> "0" Then
        Throw New Exception("Error in " & _ModelNumber & _
         vbCrLf & "Address: " & MyBase.Address & _
         vbCrLf & "Sent comand: " & Comand & _
         vbCrLf & "Received error number :" & ErrParameter)
      End If
      Return ErrParameter
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
  Private Function VisaQuery(ByVal Comand As String) As String
    Dim resp As String
    'Dim ErrParameter As String
    'Dim err As String
    Try
      MyBase.GenerateEventSentMessage("Sent query comand to " & Address & ":  """ & Comand & """")
      resp = _VisaSession.Query(Comand).TrimEnd(vbLf.ToCharArray)
      MyBase.GenerateEventSentMessage("Received:  " & resp)
      'Check Error during query
      'err = Me.GetError.TrimEnd(vbLf)
      'ErrParameter = err.Substring(err.IndexOf(" ")).Trim()
      'MyBase.GenerateEventSentMessage("Received:  " & err)
      'If ErrParameter <> "0" Then
      '    Throw New Exception("Error in " & _ModelNumber & _
      '     vbCrLf & "Address: " & MyBase.Address & _
      '     vbCrLf & "Sent comand: " & Comand & _
      '     vbCrLf & "Received error number :" & ErrParameter)
      'End If
      Return resp
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
End Class

