Imports System.Net.Sockets
Public Class SCPITelnetDevice
    Event DataArrived(ByVal Message As String)
    Private Sub _GenerateDataArrivedEvent(ByVal Message As String)
        RaiseEvent DataArrived(Message)
    End Sub

    'Private variables
    Private _ClientTelnet As TcpClient = Nothing
    Private _NetworkStream As NetworkStream = Nothing
    Private _Prompt As String = ""
    Private _IPAddress As String = "10.10.10.1"
    Private _TimeOutSec As Integer = 30
    Private _Port As Integer = 8081

    '***********************************************
    '*  Base functions and properties to override
    '***********************************************
    Public Sub Close()
        If _NetworkStream IsNot Nothing Then
            _NetworkStream.Close()
            _NetworkStream = Nothing
        End If
        If _ClientTelnet IsNot Nothing Then
            _ClientTelnet.Close()
            _ClientTelnet = Nothing
        End If
    End Sub

    Public Function Open() As Boolean
        Try
            If _ClientTelnet Is Nothing Then
                _ClientTelnet = New TcpClient()
            End If
            'Open TCP Client Connection
            _ClientTelnet.Connect(_IPAddress, _Port)
            'Create a network stream
            If _NetworkStream Is Nothing Then
                _NetworkStream = _ClientTelnet.GetStream
            End If
            Threading.Thread.Sleep(50)
            'Get Prompt
            _GetPrompt()
            Return True
        Catch ex As SocketException
            If _ClientTelnet IsNot Nothing Then
                _ClientTelnet.Close()
            End If
            Throw New Exception("SocketException: " & ex.Message)
        End Try
    End Function

    Public Function SendComand(ByVal ComandString As String) As String
        Dim resp As String = ""
        Try
            resp = _SendCommandAndReceive(ComandString, True)
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Property Address() As String
        Get
            Return _IPAddress
        End Get
        Set(ByVal value As String)
            'TODO Check of IP address format
            _IPAddress = value
        End Set
    End Property

    '*****************************************
    '*  Properties
    '*****************************************
    Property TimeOutSec() As Integer
        Get
            Return _TimeOutSec
        End Get
        Set(ByVal value As Integer)
            Try
                If value >= 0 Then
                    _TimeOutSec = value
                Else
                    Throw New Exception("TimeOut in Sec cannot be negative!")
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Set
    End Property
    Property Port() As Integer
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            If value >= 0 Then
                _Port = value
            Else
                Throw New Exception("Port for Telnet cannot be negative!")
            End If
        End Set
    End Property

    '*****************************************
    '*  Private Functions
    '*****************************************
    Private Sub _GetPrompt()
        Dim Response As String
        Dim StartPoint As Integer
        Dim StopPoint As Integer
        Try
            _Prompt = ""
            Response = _SendCommandAndReceive(" ", False)
            StartPoint = Response.LastIndexOf(vbCrLf)
            Response = Response.Remove(0, Response.Length - StartPoint)
            Response = Response.Replace(vbCrLf, "")
            StartPoint = Response.IndexOf(">")
            StopPoint = Response.LastIndexOf(">")
            _Prompt = Response.Substring(StartPoint + 2, StopPoint - StartPoint - 1)
            _GenerateDataArrivedEvent("Obtained Prompt: " & _Prompt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function _SendCommandAndReceive(ByVal Message As String, ByVal WaitForPrompt As Boolean) As String
        If _NetworkStream IsNot Nothing Then
            Try
                Dim FormattedAnswer As String = ""
                Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(Message & vbCrLf)
                _NetworkStream.Write(data, 0, data.Length)
                'Reset data to be used as buffer to store the response bytes.
                data = New Byte(256) {}
                ' String to store the response ASCII representation.
                Dim ResponseData As String = String.Empty
                ' Read the first batch of the TcpServer response bytes.
                Dim bytes As Int32
                Dim CyrrentTime As DateTime
                Dim ElapsedTime As TimeSpan
                Dim StartTime As DateTime = DateTime.Now
                ' The following keeps on receiving data until the buffer is empty
                If WaitForPrompt = False Then
                    _TimeOutSec = 20
                    Do While True
                        If _ClientTelnet.Available > 0 Then
                            bytes = _NetworkStream.Read(data, 0, data.Length)
                        End If
                        If bytes > 0 Then
                            ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                            bytes = 0
                        End If
                        If ResponseData.Contains(">") Then
                            If ResponseData.LastIndexOf(">") > ResponseData.IndexOf(">") Then
                                Exit Do
                            End If
                        End If
                        Threading.Thread.Sleep(50)
                        CyrrentTime = DateTime.Now
                        ElapsedTime = CyrrentTime.Subtract(StartTime).Duration()
                        If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
                            Throw New Exception("Timeout expired waiting for receving TCP data")
                        End If
                    Loop
                Else
                    Do While True
                        If _ClientTelnet.Available > 0 Then
                            ReDim data(0 To _ClientTelnet.Available - 1)
                            bytes = _NetworkStream.Read(data, 0, data.Length)
                        End If
                        If bytes > 0 Then
                            ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(data, 0, bytes)
                            bytes = 0
                        End If

                        Threading.Thread.Sleep(20)

                        If ResponseData.Contains(_Prompt) Then
                            'Format the string
                            Dim RespStringArray() As String
                            ResponseData = ResponseData.Replace(System.Text.Encoding.ASCII.GetString(New Byte(0) {10}), "")
                            RespStringArray = ResponseData.Split(vbCrLf.ToCharArray)
                            If RespStringArray.Length = 3 Then
                                'SCPI Query
                                'FormattedAnswer = "Sent query comand to " & _IPAddress & ":  " & RespStringArray(0) & vbCrLf
                                'FormattedAnswer += "Received: " & RespStringArray(1)
                                ResponseData = RespStringArray(1)
                                Exit Do
                            ElseIf RespStringArray.Length = 2 Then
                                If Message.Contains("?") Then
                                    'SCPI Query no echo of the comand
                                    'FormattedAnswer = "Sent write comand to " & _IPAddress & ":  " & Message
                                    ResponseData = RespStringArray(0)
                                Else
                                    'SCPI Write
                                    'FormattedAnswer = "Sent write comand to " & _IPAddress & ":  " & RespStringArray(0)
                                    ResponseData = ""
                                End If
                                Exit Do
                            ElseIf RespStringArray.Length = 1 Then
                                ResponseData = ""
                                Exit Do
                            Else
                                Throw New Exception("SCPI Telnet received not handled message: " & ResponseData)
                            End If
                        End If
                        Threading.Thread.Sleep(50)
                        CyrrentTime = DateTime.Now
                        ElapsedTime = CyrrentTime.Subtract(StartTime).Duration()
                        If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
                            Throw New Exception("Timeout expired waiting for receving TCP data")
                        End If
                    Loop
                    '_GenerateDataArrivedEvent(FormattedAnswer)
                End If
                Return ResponseData

            Catch ex As Exception
                _TimeOutSec = 10
                Throw New Exception(ex.Message)
            End Try
        Else
            Throw New Exception("TCP Connection not open")
        End If
    End Function
    'Public Sub Write(ByVal Message As String)
    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            Dim data As Byte() = System.Text.Encoding.ASCII.GetBytes(Message & vbCrLf)
    '            _NetworkStream.Write(data, 0, data.Length)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If
    'End Sub

    'Public Property DefaultBufferSize() As Integer
    '    Get
    '        Return _ClientTelnet.ReceiveBufferSize
    '    End Get
    '    Set(ByVal value As Integer)
    '        _ClientTelnet.ReceiveBufferSize = value
    '    End Set
    'End Property
    'Public Function ReadString(ByVal length As Integer) As String
    '    Dim tmparray() As Byte = Nothing
    '    Dim bytes As Integer
    '    Dim tmpbuffersize As Integer
    '    Dim ResponseData As String = ""
    '    Dim CurrentTime As DateTime, StartTime As DateTime, ElapsedTime As TimeSpan
    '    StartTime = DateTime.Now

    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            tmpbuffersize = DefaultBufferSize
    '            DefaultBufferSize = length
    '            Do While (1)
    '                If _ClientTelnet.Available Then
    '                    ReDim tmparray(0 To _ClientTelnet.Available - 1)
    '                    bytes = _NetworkStream.Read(tmparray, 0, _ClientTelnet.Available - 1)
    '                End If
    '                If bytes > 0 Then
    '                    ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(tmparray, 0, bytes)
    '                    bytes = 0
    '                End If
    '                Threading.Thread.Sleep(20)
    '                If ResponseData.Length = Me.DefaultBufferSize Then
    '                    Exit Do
    '                End If
    '                CurrentTime = DateTime.Now
    '                ElapsedTime = CurrentTime.Subtract(StartTime).Duration()
    '                If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
    '                    Throw New Exception("Timeout expired waiting for receving TCP data")
    '                End If
    '            Loop
    '            Return ResponseData
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        Finally
    '            DefaultBufferSize = tmpbuffersize
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If
    'End Function
    'Public Function ReadByteArray(ByVal length As Integer) As Byte()
    '    Dim tmparray() As Byte = Nothing
    '    Dim bytes As Integer
    '    Dim tmpbuffersize As Integer
    '    Dim ResponseData As String = ""
    '    Dim CurrentTime As DateTime, StartTime As DateTime, ElapsedTime As TimeSpan
    '    StartTime = DateTime.Now
    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            tmpbuffersize = DefaultBufferSize
    '            DefaultBufferSize = length
    '            Do While (1)
    '                If _ClientTelnet.Available Then
    '                    ReDim tmparray(0 To _ClientTelnet.Available - 1)
    '                    bytes = _NetworkStream.Read(tmparray, 0, _ClientTelnet.Available - 1)
    '                End If
    '                If bytes > 0 Then
    '                    ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(tmparray, 0, bytes)
    '                    bytes = 0
    '                End If
    '                Threading.Thread.Sleep(20)
    '                If ResponseData.Length = Me.DefaultBufferSize Then
    '                    Exit Do
    '                End If
    '                CurrentTime = DateTime.Now
    '                ElapsedTime = CurrentTime.Subtract(StartTime).Duration()
    '                If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
    '                    Throw New Exception("Timeout expired waiting for receving TCP data")
    '                End If
    '            Loop
    '            Return System.Text.Encoding.ASCII.GetBytes(ResponseData)
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        Finally
    '            DefaultBufferSize = tmpbuffersize
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If

    'End Function
    'Public Function Read() As Byte()
    '    Dim data(0 To 1024 * 1024) As Byte
    '    Dim tmparray() As Byte = Nothing
    '    Dim bytes As Integer
    '    Dim counter As Integer = 0
    '    Dim ResponseData As String = ""
    '    Dim CurrentTime As DateTime, StartTime As DateTime, ElapsedTime As TimeSpan
    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            StartTime = DateTime.Now
    '            Do While (1)
    '                If _ClientTelnet.Available Then
    '                    ReDim tmparray(0 To _ClientTelnet.Available - 1)
    '                    bytes = _NetworkStream.Read(tmparray, 0, _ClientTelnet.Available - 1)
    '                    tmparray.CopyTo(data, counter)
    '                    counter = counter + bytes
    '                End If
    '                If bytes > 0 Then
    '                    ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(tmparray, 0, bytes)
    '                    bytes = 0
    '                End If
    '                Threading.Thread.Sleep(20)
    '                If ResponseData.Contains(_Prompt) Then
    '                    Exit Do
    '                End If
    '                Threading.Thread.Sleep(50)
    '                CurrentTime = DateTime.Now
    '                ElapsedTime = CurrentTime.Subtract(StartTime).Duration()
    '                If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
    '                    Throw New Exception("Timeout expired waiting for receving TCP data")
    '                End If
    '            Loop
    '            Dim buffer(0 To counter) As Byte
    '            data.CopyTo(buffer, counter)
    '            Return buffer
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If
    'End Function
    'Public Function Read() As String
    '    Dim tmparray() As Byte = Nothing
    '    Dim bytes As Integer
    '    Dim ResponseData As String = ""
    '    Dim CurrentTime As DateTime, StartTime As DateTime, ElapsedTime As TimeSpan
    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            StartTime = DateTime.Now
    '            Do While (1)
    '                If _ClientTelnet.Available Then
    '                    ReDim tmparray(0 To _ClientTelnet.Available - 1)
    '                    bytes = _NetworkStream.Read(tmparray, 0, _ClientTelnet.Available - 1)
    '                End If
    '                'If bytes > 0 Then
    '                '    ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(tmparray, 0, bytes)
    '                '    bytes = 0
    '                'End If
    '                Threading.Thread.Sleep(20)
    '                If ResponseData.Contains(_Prompt) Then
    '                    Exit Do
    '                End If
    '                Threading.Thread.Sleep(50)
    '                CurrentTime = DateTime.Now
    '                ElapsedTime = CurrentTime.Subtract(StartTime).Duration()
    '                If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
    '                    'Throw New Exception("Timeout expired waiting for receving TCP data")
    '                End If
    '            Loop
    '            Return ResponseData
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If
    'End Function
    'Public Function Read() As Byte()
    '    Dim tmparray() As Byte = Nothing
    '    Dim bytes As Integer
    '    Dim data(0 To 1024 * 1024) As Byte
    '    Dim ResponseData As String = ""
    '    Dim CurrentTime As DateTime, StartTime As DateTime, ElapsedTime As TimeSpan
    '    Dim counter As Integer = 0
    '    If _NetworkStream IsNot Nothing Then
    '        Try
    '            StartTime = DateTime.Now
    '            Do While (1)
    '                If _ClientTelnet.Available Then
    '                    ReDim tmparray(0 To _ClientTelnet.Available - 1)
    '                    bytes = _NetworkStream.Read(tmparray, 0, _ClientTelnet.Available - 1)
    '                    tmparray.CopyTo(data, counter)
    '                    counter = counter + bytes
    '                End If
    '                'If bytes > 0 Then
    '                '    ResponseData = ResponseData + System.Text.Encoding.ASCII.GetString(tmparray, 0, bytes)
    '                '    bytes = 0
    '                'End If
    '                Threading.Thread.Sleep(20)
    '                'If ResponseData.Contains(_Prompt) Then
    '                '    Exit Do
    '                'End If
    '                Threading.Thread.Sleep(50)
    '                CurrentTime = DateTime.Now
    '                ElapsedTime = CurrentTime.Subtract(StartTime).Duration()
    '                If ElapsedTime.TotalSeconds > Me.TimeOutSec Then
    '                    Exit Do
    '                    'Throw New Exception("Timeout expired waiting for receving TCP data")
    '                End If
    '            Loop
    '            Array.Resize(data, counter)
    '            Return data
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    Else
    '        Throw New Exception("TCP Connection not open")
    '    End If
    'End Function
End Class


