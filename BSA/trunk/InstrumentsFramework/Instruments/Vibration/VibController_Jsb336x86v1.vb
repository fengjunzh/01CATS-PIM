Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class VibController_Jsb336x86v1
	Inherits Instrument
	Implements IUSB_VIB_CONTROLLER
	Declare Function Jsb336GetNumberOfModules Lib "Jsb336.dll" () As Short
	Declare Function Jsb336GetSerialNumber Lib "Jsb336.dll" (ByVal nNum As Integer, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb336GetFirmwareVersion Lib "Jsb336.dll" (ByVal sSerial As String, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb336GetDriverVersion Lib "Jsb336.dll" (ByVal sSerial As String, ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb336GetDllVersion Lib "Jsb336.dll" (ByVal sNum As String, ByVal iSize As Short) As Boolean
	Declare Function Jsb336IsRelayClosed Lib "Jsb336.dll" (ByVal sSerial As String, ByVal sNum As Short) As Boolean
	Declare Function Jsb336GetInput Lib "Jsb336.dll" (ByVal sSerial As String) As Short
	Declare Function Jsb336CloseRelay Lib "Jsb336.dll" (ByVal sSerial As String, ByVal sNum As Short) As Boolean
	Declare Function Jsb336OpenRelay Lib "Jsb336.dll" (ByVal sSerial As String, ByVal sNum As Short) As Boolean
	Declare Function Jsb336FlashLed Lib "Jsb336.dll" (ByVal sSerial As String) As Boolean

	Public g_strSerial As String = New String(vbNullChar, 32)
	Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close

	End Sub

    Public Overloads Function Open(ByVal COMPort As String) As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Throw New NotImplementedException()
    End Function
    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
		Dim nNumberOfModules As Integer = 0
		Try
			nNumberOfModules = Jsb336GetNumberOfModules
			If nNumberOfModules > 0 Then
				Jsb336GetSerialNumber(1, g_strSerial, 32)

				' State
				' True  - not affect other bits 
				' False - the other bits will set to 0,and the RelayNumber will set 1
				'jwControlSetRelayState(g_strSerial, 8, True)

				Jsb336OpenRelay(g_strSerial, 8)

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

	Public Function StartVIB() As Boolean Implements IUSB_VIB_CONTROLLER.StartVIB
		Try
			Jsb336CloseRelay(g_strSerial, 8)
			Return True
		Catch ex As Exception
			Throw New Exception("StartVIB()::" & ex.Message)
		End Try
	End Function

	Public Sub StopVIB() Implements IUSB_VIB_CONTROLLER.StopVIB
		Try
			Jsb336OpenRelay(g_strSerial, 8)
		Catch ex As Exception
			Throw New Exception("StopVIB()::" & ex.Message)
		End Try

	End Sub

	Public Function OpenRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.OpenRelay
		Try
			Jsb336OpenRelay(g_strSerial, num)
			Return True
		Catch ex As Exception
			Throw New Exception("OpenRelay()::" & ex.Message)
		End Try
	End Function

    Public Function CloseRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.CloseRelay
        Try
            Jsb336CloseRelay(g_strSerial, num)
            Return True
        Catch ex As Exception
            Throw New Exception("CloseRelay()::" & ex.Message)
        End Try
    End Function

    ' add by tony start======================================================================
    Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IUSB_VIB_CONTROLLER.ReadInput
        Try
            Dim Inputdata As Boolean = False

            Dim m_abytSL As Short = 0
            m_abytSL = Jsb336GetInput(g_strSerial)

            Select Case InputNo
                Case 1
                    Inputdata = CBool(m_abytSL And &H1)'door monitor
                Case 2
                    Inputdata = CBool(m_abytSL And &H2)
                Case 3
                    Inputdata = CBool(m_abytSL And &H4)
                Case 4
                    Inputdata = CBool(m_abytSL And &H8)
                Case Else
                    MsgBox("error, please check InputNo set")
                    Inputdata = False
            End Select

            Return Inputdata

        Catch ex As Exception
            Throw New Exception("ReadInput()::" & ex.Message)
            Return False
        End Try
    End Function


    Public Function ReadInputForChamberID() As Integer Implements IUSB_VIB_CONTROLLER.ReadInputForChamberID
        Try
            Dim m_abytSL As Short = Jsb336GetInput(g_strSerial)
            m_abytSL = m_abytSL >> 1
            Return m_abytSL
        Catch ex As Exception
            Throw New Exception("ReadInputForChamberID()::" & ex.Message)
        End Try
    End Function
    'add by tony end=========================================================

End Class
