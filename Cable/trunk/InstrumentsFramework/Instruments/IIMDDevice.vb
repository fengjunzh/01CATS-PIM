Public Interface IIMDDevice
	Enum enumTESTMODE
		REFMODE = 0  'Reflection Mode
		TRSMODE = 1  'Transmission Mode
	End Enum

	Enum enumTestMethod
		TWO_TONE = 0
		SWEEP = 1
	End Enum

	Enum enumInstrType
		UnkownType = 0
		IM_0710 = 1
		IM_1822 = 2
		IM_2526 = 3
	End Enum

	Enum enumFreqBand
		NOMATCH = 0
		LTE700 = 1
		AMPS800 = 2
		EGSM900 = 3
		DCS1800 = 4
		UMTSII2600 = 5
	End Enum
	Enum enumRFPORTS
		PORTA = 0
		PORTB = 1
	End Enum
	Structure stFreq
		Dim StartFreq As Single
		Dim StopFreq As Single
	End Structure
	Structure stTxFreq
		Dim Tx1 As stFreq
		Dim Tx2 As stFreq
	End Structure
	Function Open() As Boolean
	Sub Close()

	'ReadOnly Property Firmware() As Double
	'ReadOnly Property InstrumentType() As enumInstrType
	Property FreqBand() As Integer
	ReadOnly Property ReadImd_dBm() As Double
	ReadOnly Property ReadImd_dBc() As Double
	ReadOnly Property ReadTxRange() As stTxFreq
	ReadOnly Property ReadRxRange As stFreq
	Property ImdOrder() As Integer

	Sub SetFrequency(ByVal Port As enumRFPORTS, ByVal freqMHz As Double)
	Sub SetFrequency(ByVal freqMHz1 As Double, ByVal freqMHz2 As Double)
	Sub SetRFPower(ByVal Port As enumRFPORTS, ByVal PowerDBM As Double)
	Sub SetRFPower(ByVal PowerDBM1 As Double, ByVal PowerDBM2 As Double)
	Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean)
	Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean)
	Sub RFPowerOnOff_TwoPorts(OnOff As Boolean)
	Sub SetTestMode(Mode As enumTESTMODE)
	Sub SetImdUnit_dBm()
	Sub SetImdUnit_dBc()
	Sub CorrectRFPower(Port As enumRFPORTS)
	Sub CorrectRFPower_TwoPort()
    Function Send_And_Read(cmd As String) As String
    Property FreqBandSet As String
    Sub ClearEnd()

    Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String)
    Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String, Optional ByVal fixF As Single = 0, Optional ByVal stepF As Single = 0, Optional ByVal duration_Sec As Single = 30)

    Function ReadDTP() As String


End Interface
