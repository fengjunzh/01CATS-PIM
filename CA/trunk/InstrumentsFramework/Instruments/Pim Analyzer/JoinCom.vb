
Imports System.Threading
Imports System.Net.Sockets
Imports System.Text
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class JoinCom

    Inherits Instrument
    Implements IIMDDevice

    Private sSocket As Socket
    Private _IMD_ID As Integer
    Private TX_LENGTH As Integer = 8192
    Private _TimeOutSec As Integer = 5

    Private PowerOutput As Double

    Public Property TimeOutSec() As Integer 'ok
        Get
            Return _TimeOutSec
        End Get
        Set(ByVal value As Integer)
            _TimeOutSec = value
        End Set
    End Property

    Public Property FreqBand As Integer Implements IIMDDevice.FreqBand  'ok
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open 'ok
        sSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

        SyncLock sSocket
            Try
                'Dim ip As String = "192.168.0.176"
                'If MyBase.Address.Contains("192.168") And MyBase.Address.Contains("4001") Then
                '    Dim indexStart As Integer = MyBase.Address.IndexOf("192")
                '    Dim indexStop As Integer = MyBase.Address.IndexOf("4001") - indexStart
                '    ip = Mid(MyBase.Address, indexStart + 1, indexStop - 2)
                'Else
                '    ip = MyBase.Address
                'End If

                Dim ip As String = MyBase.Address

                If My.Computer.Network.Ping(ip) Then
                    sSocket.Connect(ip, 4001)
                Else
                    MsgBox("Error: Failed to Ping " & MyBase.Address)
                    Return False
                End If
                Thread.Sleep(200)
                If Initializtion() Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception("Error: Error duing opening Lan connection." & ex.Message)
                Return False
            End Try
        End SyncLock
    End Function

    Public Overrides Sub Close() Implements IIMDDevice.Close 'ok
        SyncLock sSocket
            Try
                If sSocket.Connected Then
                    sSocket.Shutdown(Net.Sockets.SocketShutdown.Both)
                    sSocket.Close()
                End If
            Catch ex As Exception
                Throw New Exception("Error: Error duing closing Lan connection." & ex.Message)
            End Try
        End SyncLock
    End Sub

    Private Function Initializtion() As Boolean 'ok
        Try
            Dim tmpIdn As String = ReadIDN()
            _ModelNumber = tmpIdn.Split(",")(1)

            SetTestMode(IIMDDevice.enumTESTMODE.REFMODE)

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function ReadIDN() As String 'ok
        Try
            Dim idn As String = Send_And_Read("*IDN?") 'Jointcom,JCIMA-COMPATB-700C-IV,07432018001,V4.1.5.6100
            _Vendor = idn.Split(",")(0).ToString.Trim
            _ModelNumber = idn.Split(",")(1).ToString.Trim
            _SerialNumber = idn.Split(",")(2).ToString.Trim
            _FWRevision = idn.Split(",")(3).ToString.Trim

            Return idn

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Property Port_Select As Integer
    Public Sub SetTestMode(ByVal Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode
        Dim resp As String = ""

        If Mode = IIMDDevice.enumTESTMODE.REFMODE Then

            If Port_Select = 1 Then
                resp = Send_And_Read("JC:PIM:CHP REV1")
            ElseIf Port_Select = 2 Then
                resp = Send_And_Read("JC:PIM:CHP REV2")
            Else
                Throw New Exception("Unknow Port select for JoinCom!")
            End If

        ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
            resp = Send_And_Read("JC:PIM:CH FWD1")
        Else
            Throw New Exception("Unknow test mode!")
        End If

        If resp.Contains("SUCCESS") = False Then Throw New Exception("Test mode setting failed: " & resp)
    End Sub


    'Public Sub SetTestMode(ByVal Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode
    '    Dim resp As String = ""

    '    If Mode = IIMDDevice.enumTESTMODE.REFMODE Then
    '        resp = Send_And_Read("JC:PIM:CH REV1")
    '    ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
    '        resp = Send_And_Read("JC:PIM:CH FWD1")
    '    Else
    '        Throw New Exception("Unknow test mode!")
    '    End If

    '    If resp.Contains("SUCCESS") = False Then Throw New Exception("Test mode setting failed: " & resp)
    'End Sub

    'ok========================================================================================================================
    'ok
    Public Sub SetPIMFreqMHz(ByVal FreqMHzIM As Double) Implements IIMDDevice.SetPIMFreqMHz '设置PIM测试频点，2F1-F2就是三阶，  3F1-2F2就是五阶

        Dim resp As String = ""
        resp = Send_And_Read("JC:PIM:FREQ " & FreqMHzIM * 1000) '620000
        If resp.Contains("SUCCESS") = False Then Throw New Exception("PIM Frequency setting failed: " & resp)

        '=====================add 0606==============================在读数据之前，必须要执行下下面的指令，切记！！
        'Dim tarPwr1 As Double, tarPwr2 As Double
        'Dim getPwr1 As Double, getPwr2 As Double
        'tarPwr1 = CDbl(Send_And_Read("JC:SIG1:POW?"))
        'getPwr1 = CDbl(Send_And_Read("JC:SIG1:DET?"))
        'If getPwr1 < tarPwr1 - 0.5 Or getPwr1 > tarPwr1 + 0.5 Then
        '    Throw New Exception("F1 Power is out of target, Target Power: " & tarPwr1 & ", Read Power: " & getPwr1)
        'End If

        'tarPwr2 = CDbl(Send_And_Read("JC:SIG2:POW?"))
        'getPwr2 = CDbl(Send_And_Read("JC:SIG2:DET?"))
        'If getPwr2 < tarPwr2 - 0.5 Or getPwr2 > tarPwr2 + 0.5 Then
        '    Throw New Exception("F2 Power is out of target, Target Power: " & tarPwr2 & ", Read Power: " & getPwr2)
        'End If

        '====================================================

    End Sub

    Public Sub SetRFPortFreqMHz(ByVal PORT As IIMDDevice.enumRFPORTS, ByVal FreqMHz As Double) Implements IIMDDevice.SetFrequency
        Dim resp As String = ""

        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA '0
                resp = Send_And_Read("JC:SIG1:FREQ " & FreqMHz * 1000)
            Case IIMDDevice.enumRFPORTS.PORTB '1
                resp = Send_And_Read("JC:SIG2:FREQ " & FreqMHz * 1000)
        End Select
        If resp.Contains("SUCCESS") = False Then Throw New Exception("Frequency setting failed: " & resp)

    End Sub
    'ok
    Public Sub SetRFPortFreqMHz(ByVal FreqMHz1 As Double, ByVal FreqMHz2 As Double) Implements IIMDDevice.SetFrequency
        Dim resp As String
        resp = Send_And_Read("JC:SIG1:FREQ " & FreqMHz1 * 1000)
        If resp.Contains("SUCCESS") = False Then Throw New Exception("F1 frequency setting failed: " & resp)

        resp = Send_And_Read("JC:SIG2:FREQ " & FreqMHz2 * 1000)
        If resp.Contains("SUCCESS") = False Then Throw New Exception("F2 frequency setting failed: " & resp)

    End Sub
    'ok
    Public Sub SetRFPortPowerDBM(ByVal PORT As IIMDDevice.enumRFPORTS, ByVal PowerDBM As Double) Implements IIMDDevice.SetRFPower
        Dim resp As String = ""
        If PowerDBM > 45.1 Then Throw New Exception("the power is over limit! Max value is 45, now is" & PowerDBM)

        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                resp = Send_And_Read("JC:SIG1:POW " & PowerDBM)
            Case IIMDDevice.enumRFPORTS.PORTB
                resp = Send_And_Read("JC:SIG2:POW " & PowerDBM)
        End Select

        If resp.Contains("SUCCESS") = False Then Throw New Exception("Power setting failed: " & resp)
    End Sub
    'ok
    Public Sub SetRFPortPowerDBM(ByVal PowerDBM1 As Double, ByVal PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
        Dim resp As String
        If PowerDBM1 > 45.1 Or PowerDBM2 > 45.1 Then Throw New Exception("the power is over limit! Max value is 45, now are" & PowerDBM1 & "and" & PowerDBM2)

        resp = Send_And_Read("JC:SIG1:POW " & PowerDBM1)
        If resp.Contains("SUCCESS") = False Then Throw New Exception("F1 Power setting failed: " & resp)

        resp = Send_And_Read("JC:SIG2:POW " & PowerDBM2)
        If resp.Contains("SUCCESS") = False Then Throw New Exception("F2 Power setting failed: " & resp)

    End Sub
    'ok
    Public Overloads Sub RFPowerOnOff_OnePort(ByVal PORT As IIMDDevice.enumRFPORTS, ByVal OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        Dim resp As String = ""
        Dim status As String
        Dim tarPwr1 As Double, tarPwr2 As Double
        Dim getPwr1 As Double, getPwr2 As Double
        If OnOff Then
            status = "ON"
        Else
            status = "OFF"
        End If

        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                resp = Send_And_Read("JC:SIG1:OUTP " & status)

            Case IIMDDevice.enumRFPORTS.PORTB
                resp = Send_And_Read("JC:SIG2:OUTP " & status)

        End Select

        Thread.Sleep(100)

        If OnOff Then
            Select Case PORT
                Case IIMDDevice.enumRFPORTS.PORTA
                    tarPwr1 = CDbl(Send_And_Read("JC:SIG1:POW?")) '36
                    getPwr1 = CDbl(Send_And_Read("JC:SIG1:DET?")) '36.1
                    If getPwr1 < tarPwr1 - 0.5 Or getPwr1 > tarPwr1 + 0.5 Then
                        Throw New Exception("F1 Power is out of target, Target Power: " & tarPwr1 & ", Read Power: " & getPwr1)
                    End If
                Case IIMDDevice.enumRFPORTS.PORTB
                    tarPwr2 = CDbl(Send_And_Read("JC:SIG2:POW?")) '37
                    getPwr2 = CDbl(Send_And_Read("JC:SIG2:DET?")) '35.9
                    If getPwr2 < tarPwr2 - 0.5 Or getPwr2 > tarPwr2 + 0.5 Then
                        Throw New Exception("F2 Power is out of target, Target Power: " & tarPwr2 & ", Read Power: " & getPwr2)
                    End If
            End Select
        End If
    End Sub
    'ok
    Public Overloads Sub RFPowerOnOff_OnePort(ByVal OnOff1 As Boolean, ByVal OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        Dim resp As String = ""
        Dim status1 As String
        Dim status2 As String
        Dim tarPwr1 As Double, tarPwr2 As Double
        Dim getPwr1 As Double, getPwr2 As Double
        If OnOff1 Then
            status1 = "ON"
        Else
            status1 = "OFF"
        End If

        If OnOff2 Then
            status2 = "ON"
        Else
            status2 = "OFF"
        End If

        resp = Send_And_Read("JC:SIG1:OUTP " & status1)
        resp = Send_And_Read("JC:SIG2:OUTP " & status2)

        Thread.Sleep(100)

        If OnOff1 Then
            tarPwr1 = CDbl(Send_And_Read("JC:SIG1:POW?"))
            getPwr1 = CDbl(Send_And_Read("JC:SIG1:DET?"))
            If getPwr1 < tarPwr1 - 0.5 Or getPwr1 > tarPwr1 + 0.5 Then
                Throw New Exception("F1 Power is out of target, Target Power: " & tarPwr1 & ", Read Power: " & getPwr1)
            End If
        End If
        If OnOff2 Then
            tarPwr2 = CDbl(Send_And_Read("JC:SIG2:POW?"))
            getPwr2 = CDbl(Send_And_Read("JC:SIG2:DET?"))
            If getPwr2 < tarPwr2 - 0.5 Or getPwr2 > tarPwr2 + 0.5 Then
                Throw New Exception("F2 Power is out of target, Target Power: " & tarPwr2 & ", Read Power: " & getPwr2)
            End If
        End If

        If OnOff1 And OnOff2 Then
            PowerOutput = (getPwr1 + getPwr2) / 2
        End If


    End Sub
    'ok
    Public Overloads Sub RFPowerOnOff_TwoPorts(ByVal OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
        Dim resp As String = ""
        Dim status As String
        Dim tarPwr1 As Double, tarPwr2 As Double
        Dim getPwr1 As Double, getPwr2 As Double
        If OnOff Then
            status = "ON"
        Else
            status = "OFF"
        End If

        resp = Send_And_Read("JC:SIG1:OUTP " & status)
        resp = Send_And_Read("JC:SIG2:OUTP " & status)

        Thread.Sleep(100)

        If OnOff Then
            tarPwr1 = CDbl(Send_And_Read("JC:SIG1:POW?"))
            getPwr1 = CDbl(Send_And_Read("JC:SIG1:DET?"))
            If getPwr1 < tarPwr1 - 0.5 Or getPwr1 > tarPwr1 + 0.5 Then
                Throw New Exception("F1 Power is out of target, Target Power: " & tarPwr1 & ", Read Power: " & getPwr1)
            End If

            tarPwr2 = CDbl(Send_And_Read("JC:SIG2:POW?"))
            getPwr2 = CDbl(Send_And_Read("JC:SIG2:DET?"))
            If getPwr2 < tarPwr2 - 0.5 Or getPwr2 > tarPwr2 + 0.5 Then
                Throw New Exception("F2 Power is out of target, Target Power: " & tarPwr2 & ", Read Power: " & getPwr2)
            End If
        End If
    End Sub

    Public Function GetRFPower() As Double() Implements IIMDDevice.GetRFPower
        Try

            Dim resp(1) As Double

            resp(0) = CDbl(Send_And_Read("JC:SIG1:DET?"))

            resp(1) = CDbl(Send_And_Read("JC:SIG2:DET?"))

            Return resp

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    'okend========================================================================================================================
    Public Property ImdOrder() As Integer Implements IIMDDevice.ImdOrder
        Get
            Return _IMD_ID
        End Get
        Set(ByVal value As Integer)

            _IMD_ID = value
        End Set
    End Property

    ReadOnly Property ReadImd_dBm() As Double Implements IIMDDevice.ReadImd_dBm
        Get
            Dim resp As String = ""
            Dim tarPwr1 As Double, tarPwr2 As Double
            Dim getPwr1 As Double, getPwr2 As Double

            resp = Send_And_Read("JC:SIG1:POW?;JC:SIG2:POW?;JC:SIG1:DET?;JC:SIG2:DET?;JC:PIM:DATA?") ' updated by tony 20190104

            tarPwr1 = CDbl(resp.Split(";")(0))
            tarPwr2 = CDbl(resp.Split(";")(1))
            getPwr1 = CDbl(resp.Split(";")(2))
            getPwr2 = CDbl(resp.Split(";")(3))

            If getPwr1 < tarPwr1 - 0.5 Or getPwr1 > tarPwr1 + 0.5 Then
                Throw New Exception("F1 Power is out of target, Target Power: " & tarPwr1 & ", Read Power: " & getPwr1)
            End If
            If getPwr2 < tarPwr2 - 0.5 Or getPwr2 > tarPwr2 + 0.5 Then
                Throw New Exception("F2 Power is out of target, Target Power: " & tarPwr2 & ", Read Power: " & getPwr2)
            End If

            Return CDbl(resp.Split(";")(4))
        End Get


    End Property

    ReadOnly Property ReadImd_dBc() As Double Implements IIMDDevice.ReadImd_dBc
        Get
            Return ReadImd_dBm - PowerOutput
        End Get
    End Property

    Public Function Send_And_Read(ByVal cmd As String) As String Implements IIMDDevice.Send_And_Read
        SyncLock sSocket
            Try
                Dim respByte(1000) As Byte, respStr As String = ""

                If sSocket.Connected Then
                    sSocket.Send(Encoding.ASCII.GetBytes(cmd & vbCrLf))
                    Thread.Sleep(20)
                    sSocket.Receive(respByte)
                    respStr = System.Text.Encoding.ASCII.GetString(respByte).Trim.Replace(vbCrLf, "").Replace(vbNullChar, "")
                End If
                Thread.Sleep(20)
                Return UCase(respStr)
            Catch ex As Exception
                Throw New Exception("Error: Send_And_Read commmand " & cmd & " error. " & ex.Message)
            End Try
        End SyncLock
    End Function

    '通讯的另外一种方法，这里不用+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Dim m_tcp = New TcpClient()
    'Public Overrides Function Open() As Boolean Implements IIMDDevice.Open 'ok
    '    Try
    '        Dim ip As String = "192.168.0.176"

    '        If My.Computer.Network.Ping(ip) Then
    '            'm_tcp.Connect(ip, "139")
    '            m_tcp.Connect(ip, "4001")
    '        Else
    '            MsgBox("Error: Failed to Ping " & MyBase.Address)
    '        End If


    '    Catch ex As Exception
    '        Throw New Exception("Error: Error duing opening Lan connection." & ex.Message)
    '        Return False
    '    End Try
    'End Function
    'Public Function Send_And_Read(ByVal cmd As String) As String Implements IIMDDevice.Send_And_Read
    '    SyncLock m_tcp
    '        Try
    '            Dim W_buff() As Byte, R_buff(256) As Byte
    '            Dim respByte(256) As Byte, respStr As String = ""

    '            If m_tcp.Connected Then
    '                'Write
    '                W_buff = Encoding.ASCII.GetBytes(cmd & vbCrLf)
    '                m_tcp.GetStream().Write(W_buff, 0, W_buff.Length)

    '                'Read
    '                m_tcp.GetStream().Read(R_buff, 0, 256)
    '                respStr = Encoding.ASCII.GetString(R_buff)
    '                Return respStr '.Contains("error")

    '            End If
    '            Thread.Sleep(50)
    '            Return UCase(respStr)
    '        Catch ex As Exception
    '            Throw New Exception("Error: Send_And_Read commmand " & cmd & " error. " & ex.Message)
    '        End Try
    '    End SyncLock
    'End Function
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    '以下不用==============================================================================================================================================================================
    Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm

    End Sub

    Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc

    End Sub

    Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

    End Sub

    Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

    End Sub

    Private Sub IIMDDevice_RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Private Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String, Optional ByVal fixF As Single = 0, Optional ByVal stepF As Single = 0, Optional ByVal duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Return "0"
        End Get
        Set(value As String)
        End Set
    End Property

    Public Property PointInUpSweepTrace As Boolean Implements IIMDDevice.PointInUpSweepTrace
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property DeviceCheckOK As Boolean Implements IIMDDevice.DeviceCheckOK
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd

    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function

    Public Function GetDTP_Time(Accurate As String, speed As String) As String Implements IIMDDevice.GetDTP_Time
        Throw New NotImplementedException()
    End Function

    Public Sub DTF_Cal() Implements IIMDDevice.DTF_Cal
        Throw New NotImplementedException()
    End Sub

    Public Function CheckPowerVersion(PowerDBM1 As Double) As Boolean Implements IIMDDevice.CheckPowerVersion

    End Function

    Public Function SwitchUpAndDown(SweepMode As Boolean) As Boolean Implements IIMDDevice.SwitchUpAndDown
        Throw New NotImplementedException()
    End Function
End Class
