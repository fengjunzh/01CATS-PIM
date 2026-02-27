Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class VibController_Jsb31x64
	Inherits Instrument
	Implements IUSB_VIB_CONTROLLER
	' **************************************************************************
	' Sample code to demostrate the dll interface to the J-Works, Inc JSB-31x
	'       The purpose of this code is to show how to communicate with the
	'       interface DLL and visual basic. It was written in a simple and
	'       clear manner to show the interface without a lot of complicated
	'       code.(it may not always be the most efficent) The user is encouraged
	'       to use it as a starting point for their applications.

	' Revision History
	'    99Aug04bjn Initial Release
	'    07Jun30jcd Converted from JSB210 to support JSB31x
	'

	'***************************************************************************
	' DLL interface subs and function declarations
	'
	'          If only one module is attached, an empty string "" can be used in
	'          place of the actual full serial number string.

	Declare Function Jsb31xGetNumberOfModules Lib "Jsb31x64Bit.dll" () As Short
	Declare Function Jsb31xGetSerialNumber Lib "Jsb31x64Bit.dll" (ByVal nNum As Integer, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb31xGetFirmwareVersion Lib "Jsb31x64Bit.dll" (ByVal sSerial As String, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb31xGetDriverVersion Lib "Jsb31x64Bit.dll" (ByVal sSerial As String, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb31xGetDllVersion Lib "Jsb31x64Bit.dll" (ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb31xIsRelayClosed Lib "Jsb31x64Bit.dll" (ByVal sSerial As String) As Boolean
	Declare Function Jsb31xGetInput Lib "Jsb31x64Bit.dll" (ByVal sSerial As String) As Short
	Declare Function Jsb31xCloseRelay Lib "Jsb31x64Bit.dll" (ByVal sSerial As String) As Boolean

	Declare Function Jsb31xOpenRelay Lib "Jsb31x64Bit.dll" (ByVal sSerial As String) As Boolean

	Declare Function Jsb31xFlashLed Lib "Jsb31x64Bit.dll" (ByVal sSerial As String) As Boolean



	'**********************************************************
	'   global vars
	'
	Public g_strSerial As String = New String(vbNullChar, 32)
	Public Function StartVIB() As Boolean Implements IUSB_VIB_CONTROLLER.StartVIB
		Try
			Jsb31xCloseRelay(g_strSerial)
			Return True
		Catch ex As Exception
			Throw New Exception("Start Vibration USB controller error!" & vbNewLine & ex.Message)
		End Try
	End Function

    Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close

    End Sub

    Public Overloads Function Open(ByVal COMPort As String) As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Throw New NotImplementedException()
    End Function

    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
		Dim nNumberOfModules As Integer = 0
		Try
			nNumberOfModules = Jsb31xGetNumberOfModules
			If nNumberOfModules > 0 Then
				Call Jsb31xGetSerialNumber(1, g_strSerial, 32)
				Jsb31xOpenRelay(g_strSerial)
			Else
				Throw New Exception("Cannot find Vibration USB controller!")
			End If
			Return True
			'Catch exp As InvalidCastException
			'    Throw New Exception("Selected resource must be a message-based session")
		Catch exp As Exception
			Throw New Exception(String.Format("Got error message during opening address {0}:" +
			Environment.NewLine + "{1}" +
			Environment.NewLine + "SCPI string response: {2}_{3}", Me.Address, exp.Message, _ModelNumber, _FWRevision))
		End Try
	End Function

    Public Sub StopVIB() Implements IUSB_VIB_CONTROLLER.StopVIB
        Try
            Jsb31xOpenRelay(g_strSerial)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IUSB_VIB_CONTROLLER.ReadInput
        Try
            Return False
        Catch ex As Exception
            Throw New Exception("CloseRelay()::" & ex.Message)
        End Try
    End Function

    Public Function ReadInputForChamberID() As Integer Implements IUSB_VIB_CONTROLLER.ReadInputForChamberID

        Throw New NotImplementedException()
    End Function

    Public Function OpenRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.OpenRelay
		Throw New NotImplementedException()
	End Function

	Public Function CloseRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.CloseRelay
		Throw New NotImplementedException()
	End Function
End Class
