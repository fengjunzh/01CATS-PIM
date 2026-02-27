Imports CATSPIM

Public Interface IVibrationDevice
	'Property DevAddress As String
	Function OpenDev() As Boolean
	Function CloseDev() As Boolean
	Function StartVib() As Boolean
	Function StopVib() As Boolean

	Function OpenRelay(num As Short)

	Function CloseRelay(num As Short)
End Interface
Public Class VibCtrl
	Implements IVibrationDevice
	Private m_address As String
	'Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX86
	Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.IUSB_VIB_CONTROLLER
	Public Enum VibJsbBoard
		JSB31
		JSB336
	End Enum
	Public Sub New(JsbBoard As VibJsbBoard)
		Try

			If JsbBoard = VibJsbBoard.JSB31 Then
				Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_Jsb31x86
				m_dev = dev
			ElseIf JsbBoard = VibJsbBoard.JSB336 Then
				Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_Jsb336x86v1
				m_dev = dev
			End If

		Catch ex As Exception
			Throw New Exception("VibCtrlx86.New()::" & ex.Message)
		End Try
	End Sub
	'Public Property DevAddress As String Implements IVibrationDevice.DevAddress
	'  Get
	'    'Return m_address
	'  End Get
	'  Set(value As String)
	'    'm_dev.Address = value
	'    'm_address = value
	'  End Set
	'End Property
	Public Function StartVib() As Boolean Implements IVibrationDevice.StartVib
		Try
			Return m_dev.StartVIB

		Catch ex As Exception
			Throw New Exception("VibCtrlx86.StartVib()::" & ex.Message)
		End Try
	End Function

	Public Function StopVib() As Boolean Implements IVibrationDevice.StopVib
		Try
			m_dev.StopVIB()
			Return True
		Catch ex As Exception
			Throw New Exception("VibCtrlx86.StopVib()::" & ex.Message)
		End Try
	End Function

	Private Function CloseDev() As Boolean Implements IVibrationDevice.CloseDev
		Return True
	End Function

	Private Function OpenDev() As Boolean Implements IVibrationDevice.OpenDev
		Try

			'If Environment.Is64BitOperatingSystem Then
			'  Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX64Bit
			'  dev.Open()
			'  m_dev = dev
			'  Return True
			'Else

			m_dev.Open()
			'm_dev = dev


			Return True
			'End If

			Return False
			'Return m_dev.StartVIB
		Catch ex As Exception
			Throw New Exception("VibCtrl.OpenDev()::" & ex.Message)
		End Try
	End Function

	Public Function OpenRelay(num As Short) As Object Implements IVibrationDevice.OpenRelay

		Try
			m_dev.OpenRelay(num)
			Return True
		Catch ex As Exception
			Throw New Exception("VibCtrl.OpenRelay()::" & ex.Message)
		End Try
	End Function

	Public Function CloseRelay(num As Short) As Object Implements IVibrationDevice.CloseRelay

		Try
			m_dev.CloseRelay(num)
			Return True
		Catch ex As Exception
			Throw New Exception("VibCtrl.CloseRelay()::" & ex.Message)
		End Try
	End Function
End Class
