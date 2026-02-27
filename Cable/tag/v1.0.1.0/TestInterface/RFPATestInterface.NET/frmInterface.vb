Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmMain
	Inherits System.Windows.Forms.Form

	Dim DiagLoop As frmDiagLoop
	Friend Sub CmdAbort_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdAbort.Click
		If TestObj Is Nothing Then
			Common.clsOperator.REvntAbortTest()
		Else
			Call TestObj.AbortTest()
		End If
	End Sub

	Private Sub CmdClearResults_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClearResults.Click
		Call ClearResults()
	End Sub
	Private Sub CmdRunAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdRunAll.Click
		Dim TestStep, TestStep2 As String
		Dim tmpCelestica As String = ""
		Dim tmpBarcode As String
		Dim TestStat As String
		Dim Cancel As Boolean
		Dim frmTestComplete As New frmTestComplete
		'Dim repeatTest As Boolean

		'repeatTest = True

		'On Error GoTo ErrorHandler
		CmdRunAll.Enabled = False
		Call EnableForm(False)
		'While repeatTest

		'Me.SSTab1.Tab = 1
		With Me.LstTestStep
			TestStep = LstTestStep.CurrentRow.Cells(0).Value.ToString
			TestStep2 = Replace(TestStep, " ", "_")
			'TestStep = Left(TestStep, 2)
		End With

		Call Globals_Renamed.Get_Testset_INI_Info()

		Common.UUT_Type = TxtPN.Text
		pResults.Test_System_ID = Common.Testset_ID
		pResults.Production_Site_ID = Common.Location
		pResults.Assembly_Type = Common.UUT_Type
		pResults.Test_SW_Rev = Common.SW_Version
		If Common.LimitsFile <> "" Then
			Call pResults.SetLimitsFile(Common.LimitsFile)
		End If
		'pResults.DCF_File = DataFile

		'Dim tmp As String
		'RaiseEvent QueryBarcode(TestStep, tmp)
		'If tmp = "" Then
		'Call pResults.ClearAllData
		tmpBarcode = TxtBarcode.Text
		Call ClearResults()
		SetStepStatus(TestStep, 1)

		If TestObj Is Nothing Then
			Common.clsOperator.REvntPreTest(TestStep, tmpBarcode, Cancel)
		Else
			Call TestObj.PreTest(Replace(TestStep, "Invalid_", ""), tmpBarcode, Cancel)
		End If

		If Cancel Then
			Call MsgBox("Test will abort", MsgBoxStyle.Critical, "Error")
			Call EnableForm(True)
			Exit Sub
		End If

		If tmpBarcode = "" Then
			If Not GetUUTInfo2() Then
				Call EnableForm(True)
				Exit Sub
			End If
			tmpBarcode = Me.TxtBarcode.Text
		Else
			Me.TxtBarcode.Text = tmpBarcode
		End If

		PC_Status = ""
		'ProcessCheck.TestStep = TestStep2
		pCheck.TestStep = TestStep2
		'If Not ProcessCheck.CheckOne(tmpBarcode, tmpCelestica, PC_String) Then
		If Not pCheck.CheckOne(tmpBarcode, tmpCelestica, PC_String) Then
			Call MsgBox("Process Check Failed", MsgBoxStyle.Critical, "Error")
			Call EnableForm(True)
			Exit Sub
		End If

		'clsOperator.OperatorID = ProcessCheck.OperatorID
		clsOperator.OperatorID = pCheck.OperatorID
		Common.CelesticaBarcode = tmpCelestica

		'Me.SSTab1.TabCaption(1) = "Test Results - " & tmpBarcode

		'pResults.DCF_File = ProcessCheck.DataFile
		pResults.DCF_File = pCheck.DataFile
		Call pResults.StartTest(tmpBarcode, TestStep2, (Common.CelesticaBarcode))

		If TestObj Is Nothing Then
			Common.clsOperator.REvntRunTest(Me.TxtBarcode.Text, Replace(TestStep, "Invalid_", ""), Cancel)
		Else
			Call TestObj.RunTest(Me.TxtBarcode.Text, Replace(TestStep, "Invalid_", ""), Cancel)
		End If

		Call clsOperator.StopTestGroup()
		Call pResults.StopTest()

		If TestObj Is Nothing Then
			Common.clsOperator.REvntPostTest()
		Else
			Call TestObj.PostTest()
		End If


		SetStepStatus(TestStep, 4)
		If (pResults.GetFailures() + pResults.GetPasses) > 0 Then
			Call pResults.Save_DCF()

			TestStat = pResults.GetTestStatus
			If Common.PassingUnits And TestStat = "PASS" Then Call pResults.PrintData_2(, False)
			If Common.FailingUnits And TestStat = "FAIL" Then Call pResults.PrintData_2(, False)
			If Common.FailingGroups And TestStat = "FAIL" Then Call pResults.PrintData_2(, True)

			If TestStat = "PASS" Then SetStepStatus(TestStep, 2)
			If TestStat = "FAIL" Then SetStepStatus(TestStep, 3)

			'Call ProcessCheck.TransferData
			Call pCheck.TransferData()
			'repeatTest = frmTestComplete.TestComplete(pResults.GetFailures() = 0, Common.enableRepeat)
			frmTestComplete.TestComplete(pResults.GetFailures() = 0, Common.enableRepeat)
		End If
		'End While
		Call EnableForm(True)
		Exit Sub

ErrorHandler:
		Call MsgBox(Err.Description, MsgBoxStyle.Critical, "Unhandled exception")
		Resume Next
	End Sub

	Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		AddHandler Me._mnuCalibrate_1.Click, AddressOf Me.mnuCalibrate_Click
		AddHandler Me._mnuTroubleshoot_0.Click, AddressOf Me.mnuTroubleshootCustom_Click
		'  Me.mnuTroubleshootCustom(0).Enabled = False
		'  mnuHelpCustom(0).Enabled = False
		'  mnuCalibrateCustom(0).Enabled = False
		tsslPCName.Text = "PC: " & My.Computer.Name
		tsslUserName.Text = "User: " & Environment.UserName.ToUpper
	End Sub

	Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		If TestObj Is Nothing Then
			Common.clsOperator.REvntExitInterface(0)
		Else
			Call TestObj.ExitInterface(0)
		End If
	End Sub

	Private Sub LstTestStep_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles LstTestStep.CellClick
		ShowSelectedStep()
	End Sub
	Private Sub ShowSelectedStep()
		With LstTestStep
			If LstTestStep.CurrentRow Is Nothing Then
				Exit Sub
			End If
			LstTestStep.CurrentRow.Selected = True
			Call UpdateGroupsGrid(LstTestStep.CurrentRow.Cells(0).Value.ToString)
		End With
	End Sub

	Public Sub mnuPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		Call pResults.PrintData_2(, False)
	End Sub

	Public Sub mnuPrintFail_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		Call pResults.PrintData_2(, True)
	End Sub

	Public Sub mnuToolsCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			' MsgBox(itemObject.Text, , "mnuToolsCustom_Click")

			Try
				Call EnableForm(False)
				If TestObj Is Nothing Then
					Common.clsOperator.REvntMenuClick("Tools", itemObject.Text)
				Else
					Call TestObj.MenuClick("Tools", itemObject.Text)
				End If
				Call EnableForm(True)
			Catch ex As Exception
				MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
			End Try
		End If
	End Sub

	Public Sub mnuSaveCSV_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		Dim fname As String
		Dim MyResult As System.Windows.Forms.DialogResult

		Try
			With Me.CommonDialog1Save
				.OverwritePrompt = True
				.FileName = "UNIT_" & VB.Right(pResults.Serial_Number, 4) & ".csv"
				.InitialDirectory = "c:\"
				.Filter = "CSV (*.csv)|*.csv"
				.ShowDialog()
				fname = .FileName
				If Not MyResult = System.Windows.Forms.DialogResult.Cancel Then
					Call pResults.Save_CSV(fname)
				End If
			End With
		Catch ex As Exception
		End Try
	End Sub

	Public Sub mnuCalibrate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			'MsgBox(itemObject.Text, , "mnuCalibrate_Click")

			Try
				Select Case itemObject.Text
					Case "Power Meter Sensors Cal"
						'Call MsgBox("Insert code to cal sensors")
				End Select
			Catch ex As Exception
				MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")

			Finally
				Call EnableForm(True)

			End Try
		End If
	End Sub

	Public Sub mnuCalibrateCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			'MsgBox(itemObject.Text, , "mnuCalibrateCustom_Click")

			Try
				Dim CalName As String = itemObject.Text
				Dim tmpCMbarcode As String = ""
				Dim tmpBarcode As String
				Dim PC_String As String = ""

				Call EnableForm(False)

				tmpBarcode = "CALIBRATION"

				pResults.Test_System_ID = Common.Testset_ID
				pResults.Production_Site_ID = Common.Location
				pResults.Assembly_Type = Common.UUT_Type
				pResults.Test_SW_Rev = Common.SW_Version

				PC_Status = ""
				'ProcessCheck.TestStep = CalName
				pCheck.TestStep = CalName
				'Call ProcessCheck.CheckOne(tmpBarcode, tmpCMbarcode, PC_String)
				Call pCheck.CheckOne(tmpBarcode, tmpCMbarcode, PC_String)
				'  If Not ProcessCheck.CheckOne(tmpBarcode, tmpCMbarcode, PC_String) Then
				'    Call MsgBox("Process Check Failed", vbCritical, "Error")
				'    Call EnableForm(True)
				'    Exit Sub
				'  End If

				'pResults.DCF_File = ProcessCheck.DataFile
				pResults.DCF_File = pCheck.DataFile
				Call pResults.StartTest(tmpBarcode, CalName)

				If TestObj Is Nothing Then
					Common.clsOperator.REvntMenuClick("Calibrate", itemObject.Text)
				Else
					Call TestObj.MenuClick("Calibrate", itemObject.Text)
				End If

				Call pResults.StopTest()
				Call pResults.Save_DCF()
				'Call ProcessCheck.TransferData
				Call pCheck.TransferData()

			Catch ex As Exception
				' MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")

			Finally
				Call EnableForm(True)

			End Try
		End If
	End Sub

	Public Sub mnuHelpCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			'MsgBox(itemObject.Text, , "mnuHelpCustom_Click")

			Try
				If TestObj Is Nothing Then
					Common.clsOperator.REvntMenuClick("Help", itemObject.Text)
				Else
					Call TestObj.MenuClick("Help", itemObject.Text)
				End If
			Catch ex As Exception
				'MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
			End Try
		End If
	End Sub

	Public Sub MnuQuit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MnuQuit.Click
		Me.Close()
	End Sub

	Public Sub mnuProductsCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			Dim itemParent As ToolStripMenuItem
			itemParent = itemObject.OwnerItem
			Dim item As Object

			'MsgBox(itemObject.Text, , "mnuTroubleshootCustom_Click")

			Try

				For Each item In itemParent.DropDownItems
					If TypeOf item Is ToolStripMenuItem Then
						item.Checked = False
					End If
				Next
				Call EnableForm(False)
				If TestObj Is Nothing Then
					Common.clsOperator.REvntMenuClick("Products", itemObject.Text)
				Else
					Call TestObj.MenuClick("Products", itemObject.Text)
				End If
				Call EnableForm(True)
				Me.Text = itemObject.Text & " Test Software (version " & Common.SW_Version & ")"
				itemObject.Checked = True
			Catch ex As Exception
				'MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
			End Try
		End If
	End Sub

	Public Sub mnuTroubleshootCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
		If (TypeOf eventSender Is ToolStripMenuItem) Then
			Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
			'MsgBox(itemObject.Text, , "mnuTroubleshootCustom_Click")

			Try
				Call EnableForm(False)
				If TestObj Is Nothing Then
					Common.clsOperator.REvntMenuClick("Troubleshoot", itemObject.Text)
				Else
					Call TestObj.MenuClick("Troubleshoot", itemObject.Text)
				End If
				Call EnableForm(True)
			Catch ex As Exception
				'MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
			End Try
		End If
	End Sub

	Private Sub DataGridView2_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentDoubleClick
		Dim tmpProcess As String
		Dim TGName As String

		With Me.LstTestStep
			tmpProcess = Me.LstTestStep.CurrentRow.Cells(0).Value.ToString
		End With
		TGName = Me.DataGridView2.Rows(e.RowIndex).Cells(0).Value

		If TGName <> "" Then
			On Error Resume Next
			Call EnableForm(False)
			If TestObj Is Nothing Then
				Common.clsOperator.REvntRunTestGroup(TGName, pResults.Serial_Number, tmpProcess)
			Else
				Call TestObj.RunTestGroup(TGName, pResults.Serial_Number, tmpProcess)
			End If
			Call EnableForm(True)
		End If
	End Sub

	Private Sub TxtTesterID_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles TxtTesterID.DoubleClick
		Dim tmp As String
		tmp = InputBox("Enter Operator ID", "Operator ID", TxtTesterID.Text)
		pResults.Operator_ID = tmp
		TxtTesterID.Text = tmp
	End Sub

	Private Sub EnableForm(ByVal Enable As Boolean)
		CmdAbort.Enabled = Not Enable
		CmdClearResults.Enabled = Enable
		'CmdRunAll.Enabled = Enable
		Me.DataGridView2.Enabled = Enable
		LstTestStep.Enabled = Enable
		'UPGRADE_ISSUE: Form property frmMain.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
		Cursor = IIf(Enable, System.Windows.Forms.Cursors.Default, System.Windows.Forms.Cursors.WaitCursor)
		mnuTroubleshootMain.Enabled = Enable
		mnuCalibrateMain.Enabled = Enable
		mnuToolsMain.Enabled = Enable
	End Sub

	Public Sub ClearResults()
		Dim i As Integer

		For i = 0 To Me.LstTestStep.Rows.Count - 1
			Call SetStepStatus(Me.LstTestStep.Rows(i).Cells(0).Value, 0)
		Next

		For i = 0 To Me.DataGridView2.Rows.Count - 1
			Call SetGroupStatus(Me.DataGridView2.Rows(i).Cells(0).Value, 0)
		Next

		Call clsOperator.ClearStatusMsg()
		Call pResults.ClearAllData()
		Me.TxtBarcode.Text = ""
		'Call clsOperator.SetBarcode("")
		'Me.SSTab1.TabCaption(1) = "Test Results"

		Me.DataGridView1.Rows.Clear()
	End Sub

	Friend Function GetUUTInfo2(Optional ByRef Default_Renamed As String = "") As Boolean
		Dim tmp1 As String

		tmp1 = UCase(Globals_Renamed.InputBoxMatch("Enter barcode for " & Common.UUT_Type, "Barcode", Default_Renamed, (Common.BarcodeMatch)))
		If tmp1 <> "" Then
			Me.TxtBarcode.Text = tmp1
			GetUUTInfo2 = True
		End If
	End Function

	Public Sub SetGroupStatus(ByVal TestGroup As String, ByVal Status As Short)
		Dim blackCell As New DataGridViewCellStyle
		blackCell.ForeColor = Color.Black
		Dim blueCell As New DataGridViewCellStyle
		blueCell.ForeColor = Color.Blue
		Dim greenCell As New DataGridViewCellStyle
		greenCell.ForeColor = Color.Green
		Dim redCell As New DataGridViewCellStyle
		redCell.ForeColor = Color.Red

		Dim i As Integer

		If Status < 0 Or Status > 3 Then Status = 0
		For i = 0 To Me.DataGridView2.Rows.Count - 1
			If Me.DataGridView2.Rows(i).Cells(0).Value = TestGroup Then Exit For
		Next
		If i > Me.DataGridView2.Rows.Count - 1 Then Exit Sub

		'DataGridView2.Rows(i).Selected = True
		DataGridView2.FirstDisplayedScrollingRowIndex = i
		Select Case Status
			Case 0
				Me.DataGridView2.Rows(i).Cells(1).Value = ""
				Me.DataGridView2.Rows(i).Cells(1).Style = blackCell
			Case 1
				Me.DataGridView2.Rows(i).Cells(1).Value = "Running"
				Me.DataGridView2.Rows(i).Cells(1).Style = blueCell
			Case 2
				Me.DataGridView2.Rows(i).Cells(1).Value = "Pass"
				Me.DataGridView2.Rows(i).Cells(1).Style = greenCell
			Case 3
				Me.DataGridView2.Rows(i).Cells(1).Value = "Fail"
				Me.DataGridView2.Rows(i).Cells(1).Style = redCell
		End Select

	End Sub

	Public Sub SetStepStatus(ByVal TestStep As String, ByVal Status As Short)
		Dim blackCell As New DataGridViewCellStyle
		blackCell.ForeColor = Color.Black
		Dim blueCell As New DataGridViewCellStyle
		blueCell.ForeColor = Color.Blue
		Dim greenCell As New DataGridViewCellStyle
		greenCell.ForeColor = Color.Green
		Dim redCell As New DataGridViewCellStyle
		redCell.ForeColor = Color.Red

		Dim i As Integer

		If Status < 0 Or Status > 4 Then Status = 0
		For i = 0 To Me.LstTestStep.Rows.Count - 1
			If Me.LstTestStep.Rows(i).Cells(0).Value = TestStep Then Exit For
		Next
		If i > Me.LstTestStep.Rows.Count - 1 Then Exit Sub

		Select Case Status
			Case 0
				Me.LstTestStep.Rows(i).Cells(1).Value = ""
				Me.LstTestStep.Rows(i).Cells(1).Style = blackCell
			Case 1
				Me.LstTestStep.Rows(i).Cells(1).Value = "Running"
				Me.LstTestStep.Rows(i).Cells(1).Style = blueCell
			Case 2
				Me.LstTestStep.Rows(i).Cells(1).Value = "Pass"
				Me.LstTestStep.Rows(i).Cells(1).Style = greenCell
			Case 3
				Me.LstTestStep.Rows(i).Cells(1).Value = "Fail"
				Me.LstTestStep.Rows(i).Cells(1).Style = redCell
			Case 4
				Me.LstTestStep.Rows(i).Cells(1).Value = "Abort"
				Me.LstTestStep.Rows(i).Cells(1).Style = redCell
		End Select

	End Sub

	Public Sub DisplayNumericResult(ByVal Test As String, ByVal Low As Double, ByVal measured As Double, ByVal High As Double, ByVal Passed As Boolean)
		Me.DataGridView1.Rows.Add()
		Dim redCell As New DataGridViewCellStyle
		redCell.ForeColor = Color.Red
		Dim greenCell As New DataGridViewCellStyle
		greenCell.ForeColor = Color.Green

		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(0).Value = Test
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(1).Value = Low.ToString("0.000")
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(2).Value = measured.ToString("0.000")
		If (Passed) Then
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Style = greenCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Value = "Pass"
		Else
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(2).Style = redCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Style = redCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Value = "Fail"
		End If
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(3).Value = High.ToString("0.000")
		Me.DataGridView1.CurrentCell = Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(0)
		If inDiagnosticMode Then
			DiagLoop.addResult(Test, Low.ToString("0.000"), measured.ToString("0.000"), High.ToString("0.000"), Passed)
		End If

	End Sub

	Public Sub DisplayStringResult(ByVal Test As String, ByVal Low As String, ByVal measured As String, ByVal High As String, ByVal Passed As Boolean)
		Me.DataGridView1.Rows.Add()
		Dim redCell As New DataGridViewCellStyle
		redCell.ForeColor = Color.Red
		Dim greenCell As New DataGridViewCellStyle
		greenCell.ForeColor = Color.Green

		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(0).Value = Test
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(1).Value = Low
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(2).Value = measured
		If (Passed) Then
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Style = greenCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Value = "Pass"
		Else
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(2).Style = redCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Style = redCell
			Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(4).Value = "Fail"
		End If
		Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(3).Value = High
		Me.DataGridView1.CurrentCell = Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells(0)
		If inDiagnosticMode Then
			DiagLoop.addResult(Test, Low, measured, High, Passed)
		End If

	End Sub

	Public Sub DisplayBlankRow()
		Me.DataGridView1.Rows.Add()
		If inDiagnosticMode Then
			DiagLoop.addResult("", "", "", "", True)
		End If
	End Sub

	Public Sub UpdateGroupsGrid(ByVal TestStep As String)
		Dim name_Renamed As String
		Dim pTestGroups As Collection

		Me.DataGridView2.Rows.Clear()
		If Common.TestGroups.Contains(TestStep) = False Then Exit Sub
		pTestGroups = Common.TestGroups.Item(TestStep)

		For Each name_Renamed In pTestGroups
			Me.DataGridView2.Rows.Add()
			Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells(0).Value = name_Renamed
			Me.DataGridView2.CurrentCell = Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells(0)
		Next
	End Sub

	Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
		Dim frmAbout As New About

		frmAbout.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		frmAbout.ShowDialog()
	End Sub

	'Private Sub mnuDiagLoop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDiagLoop.Click
	'  Dim i As Integer

	'  DiagLoop = New frmDiagLoop
	'  DiagLoop.SetMain(Me)

	'  Try
	'    Call EnableForm(False)

	'    DiagLoop.LstTestStep.Items.Clear()
	'    For i = 0 To LstTestStep.rows.Count - 1
	'      DiagLoop.LstTestStep.Items.Add(LstTestStep.Rows(i).Cells(0).Value)
	'    Next

	'    inDiagnosticMode = True
	'    DiagLoop.Left = Me.Left + 100
	'    DiagLoop.Top = Me.Top + 500
	'    DiagLoop.ShowDialog()
	'    inDiagnosticMode = False

	'    Call EnableForm(True)
	'  Catch
	'    Call MsgBox(Err.Description, vbCritical, "Unhandled exception")
	'  End Try
	'End Sub

	Private Sub TxtBarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtBarcode.KeyPress
		If e.KeyChar = vbCr Then
			TxtPN.Text = TxtPN.Text.ToUpper()
			TxtBarcode.Text = TxtBarcode.Text.ToUpper
			If TestObj IsNot Nothing Then

				CmdRunAll.Enabled = TestObj.CheckSN(TxtBarcode.Text, TxtPN.Text)
				Me.DataGridView1.Rows.Clear()
				ShowSelectedStep()
			End If
		End If
	End Sub
	Private Sub TxtPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPN.KeyPress
		If e.KeyChar = vbCr Then
			TxtBarcode.Focus()
		End If
	End Sub
	'Public Sub mnuPhaseStationToolStripButton_Click(sender As Object, e As System.EventArgs)
	'	Try
	'		If TypeOf (sender) Is ToolStripButton Then
	'			Dim itemObject As ToolStripButton = CType(sender, ToolStripButton)

	'			tsPhaseStation.Items(0).Text = itemObject.Text

	'			TestObj.MenuPhaseStation(itemObject.Tag)

	'		End If
	'	Catch ex As Exception
	'		MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
	'	End Try
	'End Sub
	Public Sub mnuPhaseStationToolStripItem_Click(sender As Object, e As System.EventArgs)
		Try
			If TypeOf (sender) Is ToolStripMenuItem Then
				Dim itemObject As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

				TSDP_PhaseStation.Text = itemObject.Text
				TestObj.MenuPhaseStationClick(itemObject.Tag)

			End If
		Catch ex As Exception
			MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
		End Try

	End Sub
	Public Sub mnuModeToolStripItem_Click(sender As Object, e As System.EventArgs)
		Try
			If TypeOf (sender) Is ToolStripMenuItem Then
				Dim itemObject As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

				If TestObj.MenuModeClick(itemObject.Tag) = True Then
					TSDP_Mode.Text = itemObject.Text
				End If

			End If
		Catch ex As Exception
			MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
		End Try

	End Sub
End Class