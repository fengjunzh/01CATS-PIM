Imports System.Text.RegularExpressions
Imports AndrewIntegratedProducts.InstrumentsFramework
Public Class RflightDevice

  Inherits Instrument
  Implements IIMDDevice

  Private TX_LENGTH As Integer = 8192
  Private POWER_DIFF As Double = 0.3

	'Private Shadows _ModelNumber As String
	'Private Shadows _SerialNumber As String
	'Private Shadows _FWRevision As String

	Private m_Port As New IO.Ports.SerialPort
  Private m_MyStream As System.IO.Stream
  Private m_TimeOutSec As Integer = 5
  Private m_Power1 As Double = 43
  Private m_Power2 As Double = 43
  Public Property TimeOutSec() As Integer
    Get
      Return m_TimeOutSec
    End Get
    Set(ByVal value As Integer)
      m_TimeOutSec = value
    End Set
  End Property
  Public Property FreqBand As Integer Implements IIMDDevice.FreqBand
    Get
      Try
        Dim currentBand As String = Send_CMD_Check("FREQ:BAND?")
        Return CInt(GetRegexField(currentBand, "(\d+)\s+"))
      Catch ex As Exception
        Throw New Exception("GET.FreqBand()::" & ex.Message)
      End Try

    End Get
    Set(value As Integer)
      Try
        Send_CMD_Check(String.Format("FREQ:BAND {0}", value))
      Catch ex As Exception
        Throw New Exception("SET.FreqBand()::" & ex.Message)
      End Try
    End Set
  End Property
  Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
    SyncLock m_Port
      Try
        With m_Port
          If Not .IsOpen Then
            .Parity = IO.Ports.Parity.None
            .DataBits = 8
            .StopBits = IO.Ports.StopBits.One
            .PortName = MyBase.Address
            .BaudRate = 115200
            .WriteBufferSize = TX_LENGTH
            .ReadBufferSize = 8192 * 10 + 100
            .ReadTimeout = TimeOutSec * 1000
            .WriteTimeout = TimeOutSec * 1000
            .Open()
            m_MyStream = .BaseStream
          End If
        End With
        If Initialization() Then
          Return True
        Else
          Return False
        End If

      Catch ex As Exception
        Throw New Exception(ex.Message)
        Return False
      End Try
    End SyncLock
  End Function
  Public Overrides Sub Close() Implements IIMDDevice.Close
    SyncLock m_Port
      Try
        If m_Port.IsOpen Then
          m_Port.Close()
        End If
      Catch ex As Exception
        Throw New Exception("ERROR DURING CLOSING SERIAL PORT: " & ex.Message)
      End Try
    End SyncLock
  End Sub
  Private Shared Function GetRegexField(ByVal input As String, ByVal pattern As String) As String
    Dim m As Match = Regex.Match(input, pattern)
    If m.Success Then
      Return m.Groups(1).Value
    Else
      Throw New Exception("Regular expression '" & pattern & "'not found in string: " & input)
    End If
  End Function
  Private Function Initialization() As Boolean
    Try

      Dim tmpIdn As String = ReadIDN()

      ' SetTestMode(IIMDDevice.enumTESTMODE.REFMODE)

      'Dim currentFreqBand As Integer = FreqBand
      Send_CMD_Check("*RST")

      ImdOrder = 3
      'Send_And_Read("#SSTd")
      'SetImdUnit_dBc()

      'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTA, 43)
      'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTB, 43)
    Catch ex As Exception
      Throw New Exception("Initialization()::" & ex.Message)
      Return False
    End Try
    Return True
  End Function
  Public Function ReadIDN() As String
    Try
      Dim idn As String = Send_CMD_Check("*IDN?")
      Dim x() As String

      x = idn.Split(",")

			_ModelNumber = x(1)
			_SerialNumber = x(2)
			_FWRevision = x(3)

			Return idn

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
	'Public Shadows Property Serial_Number() As String
	'  Get
	'    Return _SerialNumber
	'  End Get
	'  Set(value As String)
	'    _SerialNumber = value
	'  End Set
	'End Property
	'Public Shadows Property Model() As String
	'  Get
	'    Return _ModelNumber
	'  End Get
	'  Set(value As String)
	'    _ModelNumber = value
	'  End Set
	'End Property
	'Public Shadows Property FW_Revision() As String
	'  Get
	'    Return _FWRevision
	'  End Get
	'  Set(value As String)
	'    _FWRevision = value
	'  End Set
	'End Property
	Public Sub SetTestMode(Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode
    Try
      If Mode = IIMDDevice.enumTESTMODE.REFMODE Then
        Send_CMD_Check("SYST:MODE REFL")
      ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
        Send_CMD_Check("SYST:MODE TRAN")
      Else
        Throw New Exception("Unknow test mode!")
      End If
    Catch ex As Exception
      Throw New Exception("SetTestMode()::" & ex.Message)
    End Try

  End Sub
  Public Sub SetRFPortFreqMHz(PORT As IIMDDevice.enumRFPORTS, FreqMHz As Double) Implements IIMDDevice.SetFrequency
    Try
      Select Case PORT
        Case IIMDDevice.enumRFPORTS.PORTA
          Send_CMD_Check("CARR1:FREQ " & String.Format("{0:0.000}", FreqMHz) & " MHz")
        Case IIMDDevice.enumRFPORTS.PORTB
          Send_CMD_Check("CARR2:FREQ " & String.Format("{0:0.000}", FreqMHz) & " MHz")
      End Select
    Catch ex As Exception
      Throw New Exception("SetRFPortFreqMHz()::" & ex.Message)
    End Try
  End Sub
  Public Sub SetRFPortFreqMHz(FreqMHz1 As Double, FreqMHz2 As Double) Implements IIMDDevice.SetFrequency
    Try
      Send_CMD_Check("CARR1:FREQ " & String.Format("{0:0.000}", FreqMHz1) & " MHz")
      Send_CMD_Check("CARR2:FREQ " & String.Format("{0:0.000}", FreqMHz2) & " MHz")
    Catch ex As Exception
      Throw New Exception("SetRFPortFreqMHz()::" & ex.Message)
    End Try
  End Sub
  Public Sub SetRFPortPowerDBM(PORT As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
    '  If PowerDBM > 43 Then Throw New Exception("the power is over limit!")
    Select Case PORT
      Case IIMDDevice.enumRFPORTS.PORTA
        m_Power1 = PowerDBM
      Case IIMDDevice.enumRFPORTS.PORTB
        m_Power2 = PowerDBM
    End Select
  End Sub
  Public Sub SetRFPortPowerDBM(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
    m_Power1 = PowerDBM1
    m_Power2 = PowerDBM2
  End Sub
  Public Overloads Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
    Try
      Dim status As String
      If OnOff Then
        status = "ON"
      Else
        status = "OFF"
      End If

      Select Case PORT
        Case IIMDDevice.enumRFPORTS.PORTA
          Send_CMD_Check("CARR1:OUTP:STAT " & status)
        Case IIMDDevice.enumRFPORTS.PORTB
          Send_CMD_Check("CARR2:OUTP:STAT " & status)
      End Select
    Catch ex As Exception
      Throw New Exception("RFPowerOnOff_OnePort(PORT,OnOff)::" & ex.Message)
    End Try

  End Sub
  Public Overloads Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
    Try
      RFPowerOnOff_OnePort(IIMDDevice.enumRFPORTS.PORTA, OnOff1)
      RFPowerOnOff_OnePort(IIMDDevice.enumRFPORTS.PORTB, OnOff2)
    Catch ex As Exception
      Throw New Exception("RFPowerOnOff_OnePort(OnOff,OnOff)::" & ex.Message)
    End Try
  End Sub
  Public Overloads Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
    Try
      RFPowerOnOff_OnePort(IIMDDevice.enumRFPORTS.PORTA, OnOff)
      RFPowerOnOff_OnePort(IIMDDevice.enumRFPORTS.PORTB, OnOff)
    Catch ex As Exception
      Throw New Exception("RFPowerOnOff_TwoPorts(OnOff)::" & ex.Message)
    End Try
  End Sub
  Public Property ImdOrder() As Integer Implements IIMDDevice.ImdOrder
    Get
      Try
        Dim resp As String = Send_CMD_Check("IM:ORD?")
        Return CInt(resp.Replace(vbCrLf, ""))
      Catch ex As Exception
        Throw New Exception("GET.ImdOrder()::" & ex.Message)
      End Try

    End Get
    Set(value As Integer)
      Try
        Select Case value
          Case 3, 5, 7
            Send_CMD_Check("IM:ORD " & value)
          Case Else
            Send_CMD_Check("IM:ORD 3")
        End Select
      Catch ex As Exception
        Throw New Exception("SET.ImdOrder()::" & ex.Message)
      End Try

    End Set
  End Property
  Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
    Get
      Try
        Dim resp As String = Send_CMD_Check("IM:POW?")
        Return CDbl(GetRegexField(resp, "([+-]\d+\.\d+)\sdBm"))
      Catch ex As Exception
        Throw New Exception("GET.ReadImd_dBm()::" & ex.Message)
      End Try

    End Get
  End Property
  Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
    Get
      Dim resp As Double = ReadImd_dBm
      Return resp - m_Power1
    End Get
  End Property
  Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
    Get

    End Get
  End Property

  Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
    Get

    End Get
  End Property

    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        If m_MyStream IsNot Nothing Then
            SyncLock m_MyStream
                Try
                    Dim RespByte(0) As Byte
                    Dim ResponseData As String = ""
                    Dim txdata As Byte()
                    txdata = System.Text.Encoding.ASCII.GetBytes(cmd & vbLf)
                    m_MyStream.Write(txdata, 0, txdata.Length)
                    GenerateEventSentMessage(String.Format("Send: {0}", cmd))
                    Threading.Thread.Sleep(50)
                    ''''tic = Environment.TickCount
                    'ResponseData = m_Port.ReadLine
                    ResponseData = m_Port.ReadExisting() '(RespByte, 0, m_Port.ReadBufferSize)
                    GenerateEventSentMessage(String.Format("Read: {0}", ResponseData))
                    ''''Threading.Thread.Sleep(100)
                    'ResponseData = System.Text.Encoding.ASCII.GetString(RespByte)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception("_ERR_COM_CLOSED")
        End If
    End Function
    Private Function Send_CMD_Check(cmd As String) As String
    Dim f As New IO.StreamWriter("c:\cats\io.txt", True)
    Try
      Dim resp As String


      f.WriteLine(Now & " -1--> " & cmd)
      resp = Send_And_Read(cmd)
      f.WriteLine(Now & " <--1- " & resp)

      If resp.Contains("NAK") = True Then
        Threading.Thread.Sleep(100)

        f.WriteLine(Now & " -2--> " & cmd)
        resp = Send_And_Read(cmd)
        f.WriteLine(Now & " <--2- " & resp)

        If resp.Contains("NAK") = True Then Throw New Exception("CMD:" & cmd & ",ERROR RESPONSE:" & resp)
        Return resp
      Else
        Return resp
      End If

    Catch ex As Exception
      Throw New Exception(ex.Message)
    Finally
      f.Close()
    End Try
  End Function
  'Private Function Send_CMD_GetCheck(cmd As String) As String
  '  Try
  '    Dim resp As String

  '    resp = Send_And_Read(cmd)
  '    If resp.Contains("NAK") = False Then Return resp

  '    resp = Send_And_Read(cmd)
  '    If resp.Contains("NAK") = True Then Throw New Exception("ERROR RESPONSE:" & resp)

  '    Return resp

  '  Catch ex As Exception
  '    Throw New Exception(ex.Message)
  '  End Try
  'End Function
  Public Sub New()

  End Sub
  Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm
    Try

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc
    Try

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Private Function GetPortDBM(Port As IIMDDevice.enumRFPORTS) As Double
    Try
      Dim resp As String

      Select Case Port

        Case IIMDDevice.enumRFPORTS.PORTA
          resp = Send_CMD_Check("CARR1:POW?")
          Return CDbl(GetRegexField(resp, "(\+?\-?\d+\.\d+)\sdBm"))

        Case IIMDDevice.enumRFPORTS.PORTB
          resp = Send_CMD_Check("CARR2:POW?")
          Return CDbl(GetRegexField(resp, "(\+?\-?\d+\.\d+)\sdBm"))
        Case Else
          Return 0
      End Select
    Catch ex As Exception
      Throw New Exception("GetPortDBM()::" & ex.Message)
    End Try
  End Function

  Private Sub SetPortATT(Port As IIMDDevice.enumRFPORTS, ATT As Double)
    Try

      Select Case Port

        Case IIMDDevice.enumRFPORTS.PORTA

          Send_CMD_Check("CARR1:ATT " & String.Format("{0:0.00}", ATT) & " dB")

        Case IIMDDevice.enumRFPORTS.PORTB

          Send_CMD_Check("CARR2:ATT " & String.Format("{0:0.00}", ATT) & " dB")

      End Select

    Catch ex As Exception
      Throw New Exception("SetPortATT()::" & ex.Message)
    End Try
  End Sub
  Private Function GetPortATT(Port As IIMDDevice.enumRFPORTS) As Double
    Try
      Dim resp As String

      Select Case Port

        Case IIMDDevice.enumRFPORTS.PORTA
          resp = Send_CMD_Check("CARR1:ATT?")
          Return CDbl(GetRegexField(resp, "(\+?\-?\d+\.\d+)\sdB"))

        Case IIMDDevice.enumRFPORTS.PORTB
          resp = Send_CMD_Check("CARR2:ATT?")
          Return CDbl(GetRegexField(resp, "(\+?\-?\d+\.\d+)\sdB"))
        Case Else
          Return 0
      End Select
    Catch ex As Exception
      Throw New Exception("GetPortATT()::" & ex.Message)
    End Try
  End Function
  Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower
    Try
      Dim fpower As Double
      Dim power As Double
      Dim att As Double
      Dim diff As Double
      Dim ladj As Integer = 0

      Select Case Port
        Case IIMDDevice.enumRFPORTS.PORTA
          fpower = m_Power1
        Case IIMDDevice.enumRFPORTS.PORTB
          fpower = m_Power2
      End Select

      power = GetPortDBM(Port)
      diff = fpower - power

      While (Math.Abs(diff) > POWER_DIFF)
        ladj += 1
        If ladj > 20 Then Throw New Exception(Port.ToString & " not adjust power to " & fpower)
        att = GetPortATT(Port)
        SetPortATT(Port, att - diff)
        power = GetPortDBM(Port)
        diff = fpower - power
      End While

    Catch ex As Exception
      Throw New Exception("CorrectRFPower()::" & ex.Message)
    End Try
  End Sub
  'Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower
  '  Try
  '    Dim fpower As Single
  '    Dim power As Single
  '    Dim att As Single
  '    Dim diff As Single
  '    Dim ladj As Short = 0

  '    Select Case Port
  '      Case IIMDDevice.enumRFPORTS.PORTA
  '        fpower = m_Power1
  '      Case IIMDDevice.enumRFPORTS.PORTB
  '        fpower = m_Power2
  '    End Select

  '    power = GetPortDBM(Port)
  '    diff = Math.Abs(fpower - power)

  '    While (diff > POWER_DIFF)

  '      att = GetPortATT(Port)

  '      If diff >= 10 Then
  '        SetPortATT(Port, att - 3)
  '      ElseIf diff >= 5 Then
  '        SetPortATT(Port, att - 1.5)
  '      ElseIf diff >= 1 Then
  '        SetPortATT(Port, att - 0.5)
  '      Else
  '        ladj += 1
  '        SetPortATT(Port, att - 0.25)
  '      End If

  '      If ladj > 20 Then Throw New Exception(Port.ToString & " not adjust power to " & fpower)

  '      power = GetPortDBM(Port)
  '      diff = Math.Abs(fpower - power)

  '    End While

  '  Catch ex As Exception
  '    Throw New Exception("CorrectRFPower()::" & ex.Message)
  '  End Try
  'End Sub
  Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort
    Try

      CorrectRFPower(IIMDDevice.enumRFPORTS.PORTA)
      CorrectRFPower(IIMDDevice.enumRFPORTS.PORTB)

    Catch ex As Exception
      Throw New Exception("CorrectRFPower_TwoPort()::" & ex.Message)
    End Try
  End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String, Optional fixF As Single = 0, Optional stepF As Single = 0, Optional duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd

    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function
End Class
