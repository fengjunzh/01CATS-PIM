Imports System.Text.RegularExpressions
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class DummyDevice
    Inherits Instrument
    Implements IIMDDevice

    Private _TimeOutSec As Integer = 5
    Private rd As New Random
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
            Return 0
        End Get
        Set(value As Integer)
        End Set
    End Property

    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
        Try
            rd = New Random()
            If Initializtion() Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Sub Close() Implements IIMDDevice.Close
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
            'For i As Integer = 0 To 2
            '    Dim respstr As String = Send_And_Read("#SR")
            '    If Not Send_And_Read("#SR").Contains("SR") Then Throw New Exception("No device found!")
            'Next
            'Dim resp As String = String.Empty
            'Dim instrType As IIMD_INSTRUMENT.enumInstrType = Me.InstrumentType
            'Dim FirmwareVersion As Double = Me.Firmware
            SetTestMode(IIMDDevice.enumTESTMODE.REFMODE)
            Dim currentFreqBand As Integer = FreqBand
            ImdOrder = 3

            'Select Case instrType
            '    Case IIMD_INSTRUMENT.enumInstrType.IM_0710
            '        If currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.LTE700 And currentFreqBand <> IIMD_INSTRUMENT.enumFreqBand.EGSM900 Then
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
            '                'Else
            '                '    Throw New Exception("Current Frequency band is not supported in current instrument!")
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
            Send_And_Read("#RECUT1")
            'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTA, 43)
            'SetRFPortPowerDBM(IIMD_INSTRUMENT.enumRFPORTS.PORTB, 43)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Function ReadIDN() As String
        Return Send_And_Read("#IDN?")
    End Function

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
        'If PowerDBM > 43 Then Throw New Exception("the power is over limit!")
        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                Send_And_Read("#SPWA" & String.Format("{0:D5}", CInt(PowerDBM * 10)))
            Case IIMDDevice.enumRFPORTS.PORTB
                Send_And_Read("#SPWB" & String.Format("{0:D5}", CInt(PowerDBM * 10)))
        End Select
    End Sub
    Public Sub SetRFPortPowerDBM(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
        'If PowerDBM1 > 43 Or PowerDBM1 > 43 Then Throw New Exception("the power is over limit!")
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
        If OnOff2 Then
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
    Public Property ImdOrder As Integer Implements IIMDDevice.ImdOrder
        Get
            Dim resp As String = Send_And_Read("#USEID?")
            Return 3
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
            Threading.Thread.Sleep(1000)
            Return -150 + 10 * rd.NextDouble()
        End Get
    End Property

    Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
        Get
            Throw New NotImplementedException()
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

    Public Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        GenerateEventSentMessage(cmd)
        Return ""
    End Function

    Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm
        Throw New NotImplementedException()
    End Sub

    Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc
        Throw New NotImplementedException()
    End Sub

    Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

    End Sub

    Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String, Optional fixF As Single = 0, Optional stepF As Single = 0, Optional duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp
        Throw New NotImplementedException()
    End Sub

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd
        Throw New NotImplementedException()
    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp
        Throw New NotImplementedException()
    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function

    Public Sub SetPIMFreqMHz(freqMHzIM As Double) Implements IIMDDevice.SetPIMFreqMHz

    End Sub

    Public Function GetRFPower() As Double() Implements IIMDDevice.GetRFPower
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
