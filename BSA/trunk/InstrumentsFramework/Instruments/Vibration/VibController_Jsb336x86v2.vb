Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class VibController_Jsb336x86v2
	Inherits Instrument
	Implements IUSB_VIB_CONTROLLER
	Declare Function jwControlNumberOfModules Lib "jwControlX32.dll" () As Byte
	Declare Function jwControlRelayBitPattern Lib "jwControlX32.dll" (ByVal SerialNumber As String) As UInt32
	Declare Function jwControlOptoInBitPattern Lib "jwControlX32.dll" (ByVal SerialNumber As String) As UInt32
	Declare Function jwControlIsRelayOn Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal RelayNumber As Byte) As Boolean
	Declare Function jwControlIsOptoInputOn Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal OptoNumber As Byte) As Boolean
	Declare Function jwControlAdcChannels Lib "jwControlX32.dll" (ByVal SerialNumber As String) As Byte
	Declare Function jwControlAdcVoltage Lib "jwControlX32.dll" (ByVal SerialNumber As String) As Double
	Declare Function jwControlMaxRelays Lib "jwControlX32.dll" (ByVal SerialNumber As String) As Byte
	Declare Function jwControlMaxOptoInputs Lib "jwControlX32.dll" (ByVal SerialNumber As String) As Byte
	Declare Function jwControlLastStatus Lib "jwControlX32.dll" () As Short
	Declare Function jwControlPrivateId Lib "jwControlX32.dll" (ByVal SerialNum As String) As UInt32
	Declare Sub jwControlModuleName Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal RtnValue As String, ByVal SizeOfRtnValue As Short)
	Declare Sub jwControlDllVersion Lib "jwControlX32.dll" (ByVal RtnValue As String, ByVal SizeOfRtnValue As Short)
	Declare Sub jwControlDriverVersion Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal RtnValue As String, ByVal SizeOfRtnValue As Short)
	Declare Sub jwControlFirmwareVersion Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal RtnValue As String, ByVal SizeOfRtnValue As Short)
	Declare Sub jwControlSerialNumber Lib "jwControlX32.dll" (ByVal ModuleNumber As Byte, ByVal RtnValue As String, ByVal SizeOfRtnValue As Short)
	Declare Sub jwControlFlashLed Lib "jwControlX32.dll" (ByVal SerialNumber As String)
	Declare Sub jwControlAdcSetChannel Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal Value As Byte)
	Declare Sub jwControlSetRelayBitPattern Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal Value As UInt32)
	Declare Sub jwControlSetRelayState Lib "jwControlX32.dll" (ByVal SerialNumber As String, ByVal RelayNumber As Byte, ByVal State As Boolean)

	Public g_strSerial As String = New String(vbNullChar, 32)
	Public Function StartVIB() As Boolean Implements IUSB_VIB_CONTROLLER.StartVIB
		Try
			jwControlSetRelayBitPattern(g_strSerial, &H80)
			Return True
		Catch ex As Exception
			Throw New Exception("Start Vibration USB controller error!" & vbNewLine & ex.Message)
		End Try
	End Function
	Public Sub StopVIB() Implements IUSB_VIB_CONTROLLER.StopVIB
		Try
			jwControlSetRelayBitPattern(g_strSerial, &H0)
		Catch ex As Exception
			Throw New Exception(ex.Message)
		End Try
	End Sub
	Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close

	End Sub


    Public Overloads Function Open(ByVal COMPort As String) As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Throw New NotImplementedException()
    End Function
    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Dim nNumberOfModules As Integer = 0
        Try
            nNumberOfModules = jwControlNumberOfModules
            If nNumberOfModules > 0 Then
                jwControlSerialNumber(1, g_strSerial, 32)

                ' State
                ' True  - not affect other bits 
                ' False - the other bits will set to 0,and the RelayNumber will set 1
                'jwControlSetRelayState(g_strSerial, 8, True)

                jwControlSetRelayBitPattern(g_strSerial, &H0)

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

    Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IUSB_VIB_CONTROLLER.ReadInput
        'Try
        '    Return Jsb336GetInput(g_strSerial)
        'Catch ex As Exception
        '    Throw New Exception("CloseRelay()::" & ex.Message)
        'End Try
        Throw New NotImplementedException()
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
