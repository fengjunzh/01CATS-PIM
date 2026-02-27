Imports System.Text.RegularExpressions
Imports AndrewIntegratedProducts.InstrumentsFramework
Public Class RosenbergerDevice

  Inherits Instrument
  Implements IIMDDevice


    Private pPort As New IO.Ports.SerialPort
  Private _PortName As String
  Private TX_LENGTH As Integer = 8192
  Private MyStream As System.IO.Stream

  Private _TimeOutSec As Integer = 5
	'Private Shadows _ModelNumber As String
	'Private Shadows _SerialNumber As String
	'Private Shadows _FWRevision As String
	Public Property TimeOutSec() As Integer
    Get
      Return _TimeOutSec
    End Get
    Set(ByVal value As Integer)
      _TimeOutSec = value
    End Set
  End Property
  Public Property FreqBand As Integer Implements IIMDDevice.FreqBand
    Get
      Dim currentBand As String = Send_And_Read("#SAB?")
      Return CInt(currentBand.Substring(3))
    End Get
    Set(value As Integer)
      Send_And_Read(String.Format("#SAB{0}", value))
    End Set
  End Property

  Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
    SyncLock pPort
      Try
        With pPort
          If Not .IsOpen Then
            .Parity = IO.Ports.Parity.None
            .DataBits = 8
            .StopBits = IO.Ports.StopBits.One
            .PortName = MyBase.Address
            .BaudRate = 9600
            .WriteBufferSize = TX_LENGTH
            .ReadBufferSize = 8192 * 10 + 100
            .ReadTimeout = TimeOutSec * 1000
            .WriteTimeout = TimeOutSec * 1000
            .Open()
            MyStream = .BaseStream
          End If
        End With
        If Initializtion() Then
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
    SyncLock pPort
      Try
        If pPort.IsOpen Then
          pPort.Close()
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

  Private Function Initializtion() As Boolean
    Try
      For i As Integer = 0 To 2
        Dim respstr As String = Send_And_Read("#SR")
        If Not Send_And_Read("#SR").Contains("SR") Then Throw New Exception("No device found!")
      Next
      'Dim resp As String = String.Empty
      'Dim instrType As IIMD_INSTRUMENT.enumInstrType = Me.InstrumentType
      'Dim FirmwareVersion As Double = Me.Firmware
      Dim tmpIdn As String = ReadIDN()

      _ModelNumber = tmpIdn.Split(",")(0)

      SetTestMode(IIMDDevice.enumTESTMODE.REFMODE)
      Dim currentFreqBand As Integer = FreqBand
      ImdOrder = 3

      'Select Case instrType
      '    Case IIMD_INSTRUMENT.enumInstrType.IM_0710
      '        If currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.LTE700 Or currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.EGSM900 Then
      '            Throw New Exception("Current Frequency band is not supported in current instrument!")
      '        End If
      '        If Firmware > 6 Then  'firmware 6.15
      '            For i As Integer = 200 To 221
      '                Send_And_Read(String.Format("#RBSRE,125,0,{0}", i))
      '            Next

      '            If currentFreqBand = IIMD_INSTRUMENT.enumFreqBand.LTE700 Then
      '                'do nothing
      '            ElseIf currentFreqBand = IIMD_INSTRUMENT.enumFreqBand.EGSM900 Then
      '                Send_And_Read("#SFA09350")
      '                Send_And_Read("#SFB09600")
      '                Send_And_Read("#SSA09250")
      '                Send_And_Read("#SSB09600")
      '                Send_And_Read("#STA09370")
      '                Send_And_Read("#STB09460")
      '                Send_And_Read("#SWA00010")
      '            End If
      '        Else 'firmware 5.3 for both LTE700_Only and AMPS800
      '            If currentFreqBand = IIMD_INSTRUMENT.enumFreqBand.LTE700 Or currentFreqBand = IIMD_INSTRUMENT.enumFreqBand.AMPS800 Then
      '                Send_And_Read("$RBSREnx0")
      '                Send_And_Read("$RBSREny0")
      '                Send_And_Read("$RBSREnt0")
      '            Else
      '                Throw New Exception("Current Frequency band is not supported in current instrument!")
      '            End If
      '        End If
      '        Exit Select

      '    Case IIMD_INSTRUMENT.enumInstrType.IM_1822
      '        If currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.DCS1800 Then
      '            Throw New Exception("Current Frequency band is not supported in current instrument!")
      '        End If
      '        Send_And_Read("$RBSREnx0")
      '        Send_And_Read("$RBSREny0")
      '        Send_And_Read("$RBSREnt0")
      '        Exit Select
      '    Case IIMD_INSTRUMENT.enumInstrType.IM_2526
      '        If currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.UMTSII2600 Then
      '            Throw New Exception("Current Frequency band is not supported in current instrument!")
      '        End If
      '        For i As Integer = 200 To 221
      '            Send_And_Read(String.Format("#RBSRE,125,0,{0}", i))
      '        Next
      '        Exit Select
      '    Case Else
      '        Throw New Exception("Wrong  Frequency Band!")
      '        Exit Select
      'End Select

      Send_And_Read("#SSTd")
      SetImdUnit_dBc()
      'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTA, 43)
      'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTB, 43)
    Catch ex As Exception
      Throw New Exception(ex.Message)
      Return False
    End Try
    Return True
  End Function
  Public Function ReadIDN() As String
    Try
      Dim idn As String = Send_And_Read("#IDN?")
      Dim match As Match

      match = Regex.Match(idn, "(IM\S+)\s+Serial\ No\.\s+(\S+),Firmware\s+(\S+)", RegexOptions.Multiline)
			_ModelNumber = match.Groups(1).ToString.Trim
			_SerialNumber = match.Groups(2).ToString.Trim
			_FWRevision = match.Groups(3).ToString.Trim

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
    If Mode = IIMDDevice.enumTESTMODE.REFMODE Then
      Send_And_Read("#SMREF")
    ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
      Send_And_Read("#SMRTRA")
    Else
      Throw New Exception("Unknow test mode!")
    End If
  End Sub

  Public Sub SetRFPortFreqMHz(PORT As IIMDDevice.enumRFPORTS, FreqMHz As Double) Implements IIMDDevice.SetFrequency
    Select Case PORT
      Case IIMDDevice.enumRFPORTS.PORTA
        Send_And_Read("#SFA" & String.Format("{0:D5}", CInt(FreqMHz * 10)))
      Case IIMDDevice.enumRFPORTS.PORTB
        Send_And_Read("#SFB" & String.Format("{0:D5}", CInt(FreqMHz * 10)))
    End Select
  End Sub
  Public Sub SetRFPortFreqMHz(FreqMHz1 As Double, FreqMHz2 As Double) Implements IIMDDevice.SetFrequency
    Send_And_Read("#SFA" & String.Format("{0:D5}", CInt(FreqMHz1 * 10)))
    Send_And_Read("#SFB" & String.Format("{0:D5}", CInt(FreqMHz2 * 10)))
  End Sub

  Public Sub SetRFPortPowerDBM(PORT As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
    '  If PowerDBM > 43 Then Throw New Exception("the power is over limit!")
    Select Case PORT
      Case IIMDDevice.enumRFPORTS.PORTA
        Send_And_Read("#SPWA" & String.Format("{0:D5}", CInt(PowerDBM * 10)))
      Case IIMDDevice.enumRFPORTS.PORTB
        Send_And_Read("#SPWB" & String.Format("{0:D5}", CInt(PowerDBM * 10)))
    End Select
  End Sub
  Public Sub SetRFPortPowerDBM(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
    ' If PowerDBM1 > 43 Or PowerDBM1 > 43 Then Throw New Exception("the power is over limit!")
    Send_And_Read("#SPWA" & String.Format("{0:D5}", CInt(PowerDBM1 * 10)))
    Send_And_Read("#SPWB" & String.Format("{0:D5}", CInt(PowerDBM2 * 10)))
  End Sub
  Public Overloads Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
    Dim status As String
    If OnOff Then
      status = "1"
    Else
      status = "0"
    End If

    Select Case PORT
      Case IIMDDevice.enumRFPORTS.PORTA
        Send_And_Read("#SPA" & status)
      Case IIMDDevice.enumRFPORTS.PORTB
        Send_And_Read("#SPB" & status)
    End Select
  End Sub
  Public Overloads Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
    Dim status1 As String
    If OnOff1 Then
      status1 = "1"
    Else
      status1 = "0"
    End If
    Dim status2 As String
    If OnOff1 Then
      status2 = "1"
    Else
      status2 = "0"
    End If
    Send_And_Read("#SPA" & status1)
    Send_And_Read("#SPB" & status2)

  End Sub
  Public Overloads Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
    Dim status As String
    If OnOff Then
      status = "1"
    Else
      status = "0"
    End If
    Send_And_Read("#M2T" & status)
  End Sub
  Public Property ImdOrder() As Integer Implements IIMDDevice.ImdOrder
    Get
      Dim resp As String = Send_And_Read("#USEID?")
      Return CInt(GetRegexField(resp, "USEID(\d+)"))
    End Get
    Set(value As Integer)
      Select Case value
        Case 3, 5, 7
          Send_And_Read("#USEID" & value)
        Case Else
          Send_And_Read("#USEID3")
      End Select
    End Set
  End Property
  Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
    Get
      Dim resp As String = Send_And_Read("#IMP?")
      Return CDbl(GetRegexField(resp, "IMP\d(\S+)dBm"))
    End Get
  End Property
  Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
    Get
      Dim resp As String = Send_And_Read("#IMP?")
      Return CDbl(GetRegexField(resp, "IMP\d(\S+)dBc"))
    End Get
  End Property

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

    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        If MyStream IsNot Nothing Then
            SyncLock MyStream
                Try
                    Dim ResponseData As String = ""
                    Dim txdata As Byte()
                    txdata = System.Text.Encoding.ASCII.GetBytes(cmd & vbCr)
                    MyStream.Write(txdata, 0, txdata.Length)
                    GenerateEventSentMessage(String.Format("Send: {0}", cmd))
                    Threading.Thread.Sleep(50)
                    'tic = Environment.TickCount
                    ResponseData = pPort.ReadLine().Trim
                    GenerateEventSentMessage(String.Format("Read: {0}", ResponseData))
                    'Threading.Thread.Sleep(100)
                    Return ResponseData
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End SyncLock
        Else
            Throw New Exception("_ERR_COM_CLOSED")
        End If
    End Function
    Public Sub New()

  End Sub
  Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm
    Try
      Send_And_Read("#RECUT1")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc
    Try
      Send_And_Read("#RECUT0")
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

  End Sub

  Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

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
