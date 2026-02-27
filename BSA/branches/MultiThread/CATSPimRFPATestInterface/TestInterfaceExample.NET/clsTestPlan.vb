Option Strict Off
Option Explicit On
Friend Class clsTestPlan
  Implements RFPATestInterface.ITestPlan

  Public Sub InitializeGui()
    Call GUI.AssignTestPlan(Me)

    GUI.SW_Version = "1.0"
    GUI.INI_File = String.Format("{0}\niagara.ini", Application.StartupPath)
    GUI.repeatEnabled = False

    'Call GUI.ShowSplash(2)

    'Here is where the developer customizes the menus in the GUI
    'Call GUI.DefineTroubleshootMenu("TS item 1", "TS item 2")
    Call GUI.DefineCalMenu("Calibrate Network Analyzer")

    'GUI.ServerName = "server"
    GUI.ModeName = "PROD"
    GUI.CalButtonVisable = True
    Call GUI.ShowInterface()
  End Sub

  Private Function CheckSN(ByVal sn As String, ByVal pn As String) As Boolean Implements RFPATestInterface.ITestPlan.CheckSN
    Select Case sn.ToUpper()
      Case "TEST1"
        GUI.DefineTestSteps("RL_ISO", "IMD700", "IMD800", "IMD1900")
        GUI.SetStepStatus("RL_ISO", 0)
        GUI.SetStepStatus("IMD700", 0)
        GUI.SetStepStatus("IMD800", 0)
        GUI.SetStepStatus("IMD1900", 0)

        GUI.DefineTestGroups("RL_ISO", "Band1", "Band2", "Band3", "Band4", "Band5", "Band6")
        GUI.SetGroupStatus("Band1", 0)
        GUI.SetGroupStatus("Band2", 0)
        GUI.SetGroupStatus("Band3", 0)
        GUI.SetGroupStatus("Band4", 0)
        GUI.SetGroupStatus("Band5", 0)
        GUI.SetGroupStatus("Band6", 0)

        GUI.DefineTestGroups("IMD700", "Band3", "Band4", "Band5", "Band6", "Band7", "Band8")
        GUI.SetGroupStatus("Band3", 0)
        GUI.SetGroupStatus("Band4", 0)
        GUI.SetGroupStatus("Band5", 0)
        GUI.SetGroupStatus("Band6", 0)
        GUI.SetGroupStatus("Band7", 0)
        GUI.SetGroupStatus("Band8", 0)

        'GUI.ServerName = "server name"
        GUI.ModeName = "NPI"

      Case "TEST2"
        GUI.DefineTestSteps("RL_ISO", "IMD700", "IMD800", "IMD850")
        GUI.SetStepStatus("RL_ISO", 3)
        GUI.DefineTestGroups("RL_ISO", "P3", "P4")
        GUI.SetGroupStatus("P1", 0)
      Case Else
        GUI.AddStatusMsg(String.Format("Serial number '{0} is not found!'", sn))
        Return False
    End Select
    GUI.UUT_Type = pn
    Return True
  End Function
  Private Sub LoadTestData(ByVal SN As String, ByVal PN As String, ByVal teststep As String) Implements RFPATestInterface.ITestPlan.LoadTestData
    Call GUI.StartTestGroup("Band1")
    Call GUI.RecordResult("RL_Port1", -18.0#, -50.0#, -14.5#)
    Call GUI.RecordResult("RL_Port2", -18.0#, -50.0#, -14.5#)
    Call GUI.RecordResult("ISO_Port1_2", -32.0#, -70.0#, -28.0#)
    Call GUI.StopTestGroup()
  End Sub

  Private Sub ITestPlan_AbortTest() Implements RFPATestInterface.ITestPlan.AbortTest
    'this procedure can be used to set a boolean global variable to false
    'Then, if this global variable is periodically checked during code execution, the
    'code can be aborted
    AbortFlag = True
  End Sub

  Private Sub ITestPlan_ExitInterface(ByRef Cancel As Short) Implements RFPATestInterface.ITestPlan.ExitInterface
    FrmMain.Close()
  End Sub

  Private Sub ITestPlan_MenuClick(ByVal MenuName As String, ByRef ItemName As String) Implements RFPATestInterface.ITestPlan.MenuClick
    Dim tmp As String
    tmp = "This message is located in the application software, not the User Interface" & vbNewLine & "This message box can be replaced by a 'select/case' statement in order to take the appropriate " & "action for each menu item"

    Call MsgBox(tmp & vbNewLine & "The following item was selected: " & MenuName & "->" & ItemName, MsgBoxStyle.Information, MenuName)
  End Sub

  Private Sub ITestPlan_PostTest() Implements RFPATestInterface.ITestPlan.PostTest
    'usually nothing needed here, but this procedure is called after the test sequence is finished
  End Sub

	Private Sub ITestPlan_PreTest(ByVal TestPhase As List(Of String), ByRef Barcode As String, ByRef Cancel As Boolean) Implements RFPATestInterface.ITestPlan.PreTest
		'This routine is called prior to executing the test sequence
		'The intended use is to verify calibations and to scan the barcode
		'If any of the calibrations fail or the barcode scan fails, set the argument 'Cancel' to
		'true and the test will not proceed.
		'Dim tmp As String = ""
		AbortFlag = False
		'tmp = InputBox("Enter Barcode", TestPhase)
		'If tmp = "" Then Cancel = True
		'Barcode = tmp
	End Sub

	Private Sub ITestPlan_RunTest(ByVal Barcode As String, ByVal TestPhase As List(Of String), ByRef Cancel As Boolean) Implements RFPATestInterface.ITestPlan.RunTest
		'Here is where the test sequence should be implemented
		Try

			Select Case TestPhase(0)
				Case "RL_ISO"
					Call GUI.StartTestGroup("Band1")
					Call GUI.RecordResult("RL_Port1", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port2", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port1_2", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Dim dgRun As New MethodInvoker(AddressOf RetTilts)
					Dim res As IAsyncResult = dgRun.BeginInvoke(Nothing, Nothing)
					dgRun.EndInvoke(res)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("Band2")
					Call GUI.RecordResult("RL_Port3", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port4", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port3_4", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("Band3")
					Call GUI.RecordResult("RL_Port5", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port6", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port5_6", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("Band4")
					Call GUI.RecordResult("RL_Port7", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port8", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port7_8", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("Band5")
					Call GUI.RecordResult("RL_Port9", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port10", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port9_10", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("Band6")
					Call GUI.RecordResult("RL_Port11", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("RL_Port12", -18.0#, -50.0#, -14.5#)
					Call MyDelay(1000)
					Call GUI.RecordResult("ISO_Port11_12", -32.0#, -70.0#, -28.0#)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

				Case "PostBurnin"
					Call GUI.StartTestGroup("Distortion Tests")
					Call GUI.RecordResult("Distortion_Freq1", -51, -80, -48)
					Call MyDelay(1000)
					Call GUI.RecordResult("Distortion_Freq2", -50, -80, -48)
					Call MyDelay(1000)
					Call GUI.RecordResult("Distortion_Freq3", -49, -80, -48)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()

					Call GUI.StartTestGroup("S-Parameter Tests")
					Call GUI.RecordResult("S11_LogMagnitude", -21, -80, -20)
					Call MyDelay(1000)
					Call GUI.RecordResult("S21_Phase", 130, -180, 180)
					Call MyDelay(1000)
					Call GUI.RecordResult("S22_LogMagnitude", -27.3, -80, -20)
					Call MyDelay(1000)
					Call GUI.StopTestGroup()
				Case Else
					Call MsgBox(TestPhase(0), MsgBoxStyle.Critical, "Error")
					Exit Sub
			End Select

		Catch
			'This error handler catches the 'abort' exception as well as any other unhandled exceptions
			Cancel = True
		End Try
	End Sub
	Private Sub RetTilts()
    GUI.AddStatusMsg("help", True)
  End Sub
  Private Sub ITestPlan_RunTestGroup(ByVal TestGroup As String, ByVal Barcode As String, ByVal TestPhase As String) Implements RFPATestInterface.ITestPlan.RunTestGroup
    'this routine is called when the user 'double-clicks' on a test group name in the flex grid
    'the intended use is for when the operator is troubleshooting a DUT and only wants to'
    'run part of the test sequence

    'this example only implements this 'troubleshooting' capability for one of the test groups
    If TestGroup = "Voltage Tests" Then
      Call GUI.StartTestGroup("Voltage Tests")
      Call GUI.RecordResult("Voltage_Node1", 0.5, 0.0#, 3.0#)
      Call MyDelay(1000)
      Call GUI.RecordResult("Voltage_Node2", 2.1, 0.0#, 3.0#)
      Call MyDelay(1000)
      Call GUI.RecordResult("Voltage_Node3", 2.9, 2.0#, 3.0#)
      Call MyDelay(1000)
      Call GUI.StopTestGroup()
    Else
      Call MsgBox("This 'troubleshooting' procedure is not implemented - only the 'Voltage Tests' test group has this capability", MsgBoxStyle.Critical, "Error")
    End If
  End Sub
  Private Sub Calibrate(ByVal teststep As String) Implements RFPATestInterface.ITestPlan.Calibrate
    MsgBox("Calibrate " & teststep)
  End Sub
End Class