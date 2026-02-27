Option Strict Off
Imports AndrewIntegratedProducts.InstrumentsFramework
Imports AxSummitekPIM
Public Class SummitekDevice
	Inherits Instrument
	Implements IIMDDevice

	Private _RC As AxRC
	Private _frm As New FormComm
	Public Sub New()
		Try

			_frm = New FormComm
			_RC = _frm.AxRC1
			'_SerialNumber = _RC.CalibrationSN
			'_ModelNumber = _RC.TestSetID
			' frm.Dispose()

		Catch ex As Exception
			Throw New Exception(ex.Message)
		End Try
	End Sub
	Private Sub CheckErrorState()
		Try

			If _RC.ErrorState.code <> 0 Then Throw New Exception(_RC.ErrorState.desc)
			'Threading.Thread.Sleep(10)
		Catch ex As Exception
			Throw New Exception(ex.Message)
		End Try
	End Sub
	Public Property ALC() As Boolean
		Get
			Try
				Dim rnt As Boolean

				rnt = _RC.get_ALC()
				CheckErrorState()

				Return rnt
			Catch ex As Exception
				Throw New Exception("get ALC() - " & ex.Message)
			End Try
		End Get
		Set(value As Boolean)
			Try
				_RC.set_ALC(value)
				CheckErrorState()
			Catch ex As Exception
				Throw New Exception("set ALC() - " & ex.Message)
			End Try

		End Set
	End Property
	Public Property Hold() As Boolean
		Get
			Try
				Dim rnt As Boolean

				rnt = _RC.get_Hold
				CheckErrorState()

				Return rnt
			Catch ex As Exception
				Throw New Exception("get HOLD() - " & ex.Message)
			End Try
		End Get
		Set(value As Boolean)
			Try
				_RC.set_Hold(value)
				CheckErrorState()
			Catch ex As Exception
				Throw New Exception("set HOLD() - " & ex.Message)
			End Try

		End Set
	End Property
	Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
		Try

			If _RC.Connected = False Then _RC.Connect(MyBase.Address)
			CheckErrorState()
			_SerialNumber = _RC.CalibrationSN
			_ModelNumber = _RC.TestSetID
			ALC = True
			Hold = False

			Return True

		Catch ex As Exception
			Throw New Exception("open device " & MyBase.Address & " - " & ex.Message)
		End Try
	End Function
	Public Overrides Sub Close() Implements IIMDDevice.Close
		Try

			_RC.Disconnect()
			CheckErrorState()
			'_RC.Dispose()
			'_frm.Dispose()

		Catch ex As Exception
			Throw New Exception("close device " & MyBase.Address & " - " & ex.Message)
		End Try
	End Sub
	Public Property FreqBand As Integer Implements IIMDDevice.FreqBand
		Get
			Return 0
			'CheckErrorState()
			'Throw New NotImplementedException()
		End Get
		Set(value As Integer)
			'Throw New NotImplementedException()
		End Set
	End Property
	Public Property ImdOrder As Integer Implements IIMDDevice.ImdOrder
		Get
			Try
				Dim rnt As Byte
				rnt = _RC.get_IMOrder()
				CheckErrorState()
				Return (rnt * 2 + 3)
			Catch ex As Exception
				Throw New Exception("ImdOrder() - " & ex.Message)
			End Try

		End Get
		Set(value As Integer)
			Try
				_RC.set_IMOrder(CByte((value - 3) / 2))
				CheckErrorState()
			Catch ex As Exception
				Throw New Exception("ImdOrder() - " & ex.Message)
			End Try
		End Set
	End Property
	Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
		Get
			Try
				Dim rnt() As Double

				_RC.Trigger()
				rnt = _RC.IMPeakPower
				CheckErrorState()
				If (rnt Is Nothing) Then Throw New Exception("return nothing")
				If (rnt.GetLength(0) < 1) Then Throw New Exception("return error value")
                Return rnt(0)

            Catch ex As Exception
				Throw New Exception("ReadImd_dBm() - " & ex.Message)
			End Try
		End Get
	End Property
	Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
		Get
			Try
				Dim rnt() As Double

				_RC.Trigger()
				Threading.Thread.Sleep(20) 'change 50 to 20
				rnt = _RC.IMPowerdBc
				'  _RC.ReadIMPowerArraydBc(rnt)
				CheckErrorState()
				If (rnt Is Nothing) Then Throw New Exception("return nothing")
				If (rnt.GetLength(0) < 1) Then Throw New Exception("return error value")
				Return rnt(0)

			Catch ex As Exception
				Throw New Exception("ReadImd_dBc() - " & ex.Message)
			End Try
		End Get
	End Property
	Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
		Get
			Try
				Dim r As Array = Nothing
				Dim rnt As New IIMDDevice.stTxFreq
				r = _RC.TxRange()
				CheckErrorState()
				If r Is Nothing Then Throw New Exception("return nothing")
				rnt.Tx1.StartFreq = r(0)
				rnt.Tx1.StopFreq = r(1)
				rnt.Tx2.StartFreq = r(3)
				rnt.Tx2.StopFreq = r(4)

				Return rnt
			Catch ex As Exception
				Throw New Exception("ReadTxRange() - " & ex.Message)
			End Try
		End Get
	End Property
	Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
		Get
			Try
				Dim r As Array = Nothing
				Dim rnt As New IIMDDevice.stFreq
				r = _RC.RxRange
				CheckErrorState()
				If r Is Nothing Then Throw New Exception("return nothing")
				rnt.StartFreq = r(0)
				rnt.StopFreq = r(1)

				Return rnt
			Catch ex As Exception
				Throw New Exception("ReadRxRange() - " & ex.Message)
			End Try
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

    Public Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        Throw New NotImplementedException()
    End Sub
    Public Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
		Try
			Dim r As Array = {OnOff1, OnOff2}
			_RC.set_TxOn(r)
			CheckErrorState()
		Catch ex As Exception
			Throw New Exception("RFPowerOnOff_OnePort() - " & ex.Message)
		End Try
	End Sub
	Public Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
		Try
			Dim r As Array = {OnOff, OnOff}
			_RC.set_TxOn(r)
			CheckErrorState()
		Catch ex As Exception
			Throw New Exception("RFPowerOnOff_TwoPorts() - " & ex.Message)
		End Try
	End Sub
	Public Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
		Throw New NotImplementedException()
	End Function
	Public Sub SetFrequency(Port As IIMDDevice.enumRFPORTS, freqMHz As Double) Implements IIMDDevice.SetFrequency
		Try
			Dim r As Array = Nothing
			Dim s As Array = Nothing

			r = _RC.get_TxFrequency()
			If Port = IIMDDevice.enumRFPORTS.PORTA Then
				s = {CDbl(freqMHz), CDbl(r(1))}
			Else
				s = {CDbl(r(0)), CDbl(freqMHz)}
			End If
			_RC.set_TxFrequency(s)
			CheckErrorState()

		Catch ex As Exception
			Throw New Exception("SetFrequency() - " & ex.Message)
		End Try
	End Sub
	Public Sub SetFrequency(freqMHz1 As Double, freqMHz2 As Double) Implements IIMDDevice.SetFrequency
		Try
			Dim r As Array = {freqMHz1, freqMHz2}
			_RC.set_TxFrequency(r)

			CheckErrorState()
		Catch ex As Exception
			Throw New Exception("SetFrequency() - " & ex.Message)
		End Try
	End Sub
	Public Sub SetRFPower(Port As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower

	End Sub
	Public Sub SetRFPower(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
		Try
			Dim r As Array = {PowerDBM1, PowerDBM2}
			_RC.set_RequestedTxPower(r)

			CheckErrorState()
		Catch ex As Exception
			Throw New Exception("SetRFPower() - " & ex.Message)
		End Try
	End Sub
	Public Sub SetTestMode(Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode
		Try
			If Mode = IIMDDevice.enumTESTMODE.REFMODE Then
				_RC.set_IMMeasMode(SummitekPIM.MeasModes.pimMMReverse)
			ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
				_RC.set_IMMeasMode(SummitekPIM.MeasModes.pimMMForward)
			End If

			CheckErrorState()

		Catch ex As Exception
			Throw New Exception(ex.Message)
		End Try
	End Sub
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

    End Sub

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd

    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function
End Class
