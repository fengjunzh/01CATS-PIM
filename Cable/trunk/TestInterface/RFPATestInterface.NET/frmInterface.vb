Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports System.Drawing
Friend Class frmMain
    Inherits System.Windows.Forms.Form

    Dim DiagLoop As frmDiagLoop

    Private Sub CmdClearResults_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call ClearResults()
    End Sub

    Private Sub RunAllTest()
        Dim TestStep, TestStep2 As String
        Dim tmpCelestica As String = ""
        Dim tmpBarcode As String
        Dim TestStat As String
        Dim Cancel As Boolean
        'Dim frmTestComplete As New frmTestComplete
        'Dim repeatTest As Boolean

        'repeatTest = True

        'On Error GoTo ErrorHandler
        Call EnableForm(False)
        'While repeatTest

        'Me.SSTab1.Tab = 1
        'Get TestStep
        With Me.dgvTestPhase
            TestStep = dgvTestPhase.CurrentRow.Cells(0).Value.ToString
            TestStep2 = Replace(TestStep, " ", "_")
            'TestStep = Left(TestStep, 2)
        End With

        Call Globals_Renamed.Get_Testset_INI_Info()

        Common.UUT_Type = lblPartNumber.Text
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
        tmpBarcode = Me.lblSerialNumber.Text

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

        'If tmpBarcode = "" Then
        '    If Not GetUUTInfo2() Then
        '        Call EnableForm(True)
        '        Exit Sub
        '    End If
        '    tmpBarcode = Me.txtSN1.Text
        'Else
        '    Me.txtSN1.Text = tmpBarcode
        '    Me.txtSN2.Text = tmpBarcode2
        'End If

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
        Common.CelesticaBarcode = tmpCelestica

        'Me.SSTab1.TabCaption(1) = "Test Results - " & tmpBarcode

        'pResults.DCF_File = ProcessCheck.DataFile
        pResults.DCF_File = pCheck.DataFile
        ' Set pStartTestTime = Now
        Call pResults.StartTest(tmpBarcode, TestStep2, (Common.CelesticaBarcode))
        If TestObj Is Nothing Then
            Common.clsOperator.REvntRunTest(Me.lblSerialNumber.Text, Replace(TestStep, "Invalid_", ""), Cancel)
        Else
            Call TestObj.RunTest(NewCable.Serial_Number, Replace(TestStep, "Invalid_", ""), Cancel)
        End If

        'Call clsOperator.StopTestGroup()
        'Set pStopTestTime = Now
        Call pResults.StopTest()

        'Nothing happened
        If TestObj Is Nothing Then
            Common.clsOperator.REvntPostTest()
        Else
            Call TestObj.PostTest()
        End If

        'Set step status to "Abort"
        SetStepStatus(TestStep, 4)
        If (pResults.GetFailures() + pResults.GetPasses) > 0 Then
            'Call pResults.Save_DCF()

            TestStat = pResults.GetTestStatus
            If Common.PassingUnits And TestStat = "PASS" Then Call pResults.PrintData_2(, False)
            If Common.FailingUnits And TestStat = "FAIL" Then Call pResults.PrintData_2(, False)
            If Common.FailingGroups And TestStat = "FAIL" Then Call pResults.PrintData_2(, True)

            If TestStat = "PASS" Then SetStepStatus(TestStep, 2)
            If TestStat = "FAIL" Then SetStepStatus(TestStep, 3)

            Call pCheck.TransferData()

            'frmTestComplete.TestComplete(pResults.GetFailures() = 0, Common.enableRepeat)

        End If
        Call EnableForm(True)
        Exit Sub
ErrorHandler:
        Call MsgBox(Err.Description, MsgBoxStyle.Critical, "Unhandled exception")
        Resume Next
    End Sub

    Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        AddHandler Me._mnuCalibrate_1.Click, AddressOf Me.mnuCalibrate_Click
        AddHandler Me._mnuTroubleshoot_0.Click, AddressOf Me.mnuTroubleshootCustom_Click
        lblSerialNumber.Text = ""
        lblPartNumber.Text = ""
        lblCoreNumber.Text = ""
        lblOrderNumber.Text = ""
        lblTestConnector.Text = ""
        lblLength.Text = ""
        lblTestLength.Text = ""
        tsslPCName.Text = "PC: " & My.Computer.Name
        tsslUserName.Text = "User: " & Environment.UserName.ToUpper
    End Sub

    Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If TestObj Is Nothing Then
            Common.clsOperator.REvntExitInterface(0)
        Else
            Call TestObj.ExitInterface(0)
        End If
    End Sub

    Private Sub LstTestStep_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dgvTestPhase.CellClick
        ShowSelectedStep()
    End Sub
    Private Sub ShowSelectedStep()
        With dgvTestPhase
            If dgvTestPhase.CurrentRow Is Nothing Then
                Exit Sub
            End If
            dgvTestPhase.CurrentRow.Selected = True
            Call UpdateGroupsGrid(dgvTestPhase.CurrentRow.Cells(0).Value.ToString)
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

    Public Sub mnuSAPCustom_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        If (TypeOf eventSender Is ToolStripMenuItem) Then
            Dim itemObject As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)
            ' MsgBox(itemObject.Text, , "mnuToolsCustom_Click")

            Try
                Call EnableForm(False)
                If TestObj Is Nothing Then
                    Common.clsOperator.REvntMenuClick("SAP", itemObject.Text)
                Else
                    Call TestObj.MenuClick("SAP", itemObject.Text)
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

    Private Sub DataGridView2_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvTestGroup.CellContentDoubleClick
        Dim tmpProcess As String
        Dim TGName As String

        With Me.dgvTestPhase
            tmpProcess = Me.dgvTestPhase.CurrentRow.Cells(0).Value.ToString
        End With
        TGName = Me.dgvTestGroup.Rows(e.RowIndex).Cells(0).Value

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

    Private Sub EnableForm(ByVal Enable As Boolean)
        btnNewCable.Enabled = Enable
        btnLoadTest.Enabled = Enable
        Me.dgvTestGroup.Enabled = Enable
        dgvTestPhase.Enabled = Enable
        'UPGRADE_ISSUE: Form property frmMain.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
        Cursor = IIf(Enable, System.Windows.Forms.Cursors.Default, System.Windows.Forms.Cursors.WaitCursor)
        mnuTroubleshootMain.Enabled = Enable
        mnuCalibrateMain.Enabled = Enable
        mnuToolsMain.Enabled = Enable
    End Sub

    Public Sub ClearResults()
        Dim i As Integer

        For i = 0 To Me.dgvTestPhase.Rows.Count - 1
            Call SetStepStatus(Me.dgvTestPhase.Rows(i).Cells(0).Value, 0)
        Next

        For i = 0 To Me.dgvTestGroup.Rows.Count - 1
            Call SetGroupStatus(Me.dgvTestGroup.Rows(i).Cells(0).Value, 0)
        Next

        Call clsOperator.ClearStatusMsg()
        Call pResults.ClearAllData()
        'Me.txtSN1.Text = ""
        'Call clsOperator.SetBarcode("")
        'Me.SSTab1.TabCaption(1) = "Test Results"

        Me.dgvTestResult.Rows.Clear()
    End Sub
    Public Sub ClearDataGridView()
        Me.dgvTestPhase.Rows.Clear()
        Me.dgvTestResult.Rows.Clear()
        Me.dgvTestGroup.Rows.Clear()
    End Sub

    Friend Function GetUUTInfo2(Optional ByRef Default_Renamed As String = "") As Boolean
        Dim tmp1 As String

        tmp1 = UCase(Globals_Renamed.InputBoxMatch("Enter barcode for " & Common.UUT_Type, "Barcode", Default_Renamed, (Common.BarcodeMatch)))
        If tmp1 <> "" Then
            Me.lblSerialNumber.Text = tmp1
            GetUUTInfo2 = True
        End If
    End Function

    Public Sub SetGroupStatus(ByVal TestGroup As String, ByVal Status As Short)
        If TestGroup.Contains("PORT1") Then TestGroup = TestGroup.Replace("PORT1", "OH")
        If TestGroup.Contains("PORT2") Then TestGroup = TestGroup.Replace("PORT2", "HO")
        Dim blackCell As New DataGridViewCellStyle
        blackCell.ForeColor = Color.Black
        Dim blueCell As New DataGridViewCellStyle
        blueCell.ForeColor = Color.Blue
        Dim greenCell As New DataGridViewCellStyle
        greenCell.ForeColor = Color.Green
        Dim redCell As New DataGridViewCellStyle
        redCell.ForeColor = Color.Red

        Dim i As Integer

        If Status < 0 Or Status > 5 Then Status = 0
        For i = 0 To Me.dgvTestGroup.Rows.Count - 1
            If Me.dgvTestGroup.Rows(i).Cells(0).Value = TestGroup Then Exit For
        Next
        If i > Me.dgvTestGroup.Rows.Count - 1 Then Exit Sub

        'DataGridView2.Rows(i).Selected = True
        dgvTestGroup.FirstDisplayedScrollingRowIndex = i
        Select Case Status
            Case 0
                Me.dgvTestGroup.Rows(i).Cells(1).Value = ""
                Me.dgvTestGroup.Rows(i).Cells(1).Style = blackCell
            Case 1
                Me.dgvTestGroup.Rows(i).Cells(1).Value = "Running"
                Me.dgvTestGroup.Rows(i).Cells(1).Style = blueCell
            Case 2
                Me.dgvTestGroup.Rows(i).Cells(1).Value = "Pass"
                Me.dgvTestGroup.Rows(i).Cells(1).Style = greenCell
            Case 3
                Me.dgvTestGroup.Rows(i).Cells(1).Value = "Fail"
                Me.dgvTestGroup.Rows(i).Cells(1).Style = redCell
            Case 4
                Me.dgvTestGroup.Rows(i).Cells(1).Value = "Abort"
                Me.dgvTestGroup.Rows(i).Cells(1).Style = redCell
            Case 5
                Me.dgvTestGroup.Rows(i).Cells(1).Value = "Error"
                Me.dgvTestGroup.Rows(i).Cells(1).Style = redCell
        End Select
        Application.DoEvents()
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

        If Status < 0 Or Status > 5 Then Status = 0
        For i = 0 To Me.dgvTestPhase.Rows.Count - 1
            If Me.dgvTestPhase.Rows(i).Cells(0).Value = TestStep Then Exit For
        Next
        If i > Me.dgvTestPhase.Rows.Count - 1 Then Exit Sub

        Select Case Status
            Case 0
                Me.dgvTestPhase.Rows(i).Cells(1).Value = ""
                Me.dgvTestPhase.Rows(i).Cells(1).Style = blackCell
            Case 1
                Me.dgvTestPhase.Rows(i).Cells(1).Value = "Running"
                Me.dgvTestPhase.Rows(i).Cells(1).Style = blueCell
            Case 2
                Me.dgvTestPhase.Rows(i).Cells(1).Value = "Pass"
                Me.dgvTestPhase.Rows(i).Cells(1).Style = greenCell
            Case 3
                Me.dgvTestPhase.Rows(i).Cells(1).Value = "Fail"
                Me.dgvTestPhase.Rows(i).Cells(1).Style = redCell
            Case 4
                Me.dgvTestPhase.Rows(i).Cells(1).Value = "Abort"
                Me.dgvTestPhase.Rows(i).Cells(1).Style = redCell
            Case 5
                Me.dgvTestPhase.Rows(i).Cells(1).Value = "Error"
                Me.dgvTestPhase.Rows(i).Cells(1).Style = redCell
        End Select

    End Sub

    Public Sub DisplayNumericResult(ByVal Test As String, ByVal Low As Double, ByVal measured As Double, ByVal High As Double, ByVal Passed As Boolean)
        If Test.Contains("PORT1") Then Test = Test.Replace("PORT1", "OH")
        If Test.Contains("PORT2") Then Test = Test.Replace("PORT2", "HO")
        If Test.Contains("_T0") Then Test = Test.Replace("_T0", "")
        Me.dgvTestResult.Rows.Add()
        Dim redCell As New DataGridViewCellStyle
        redCell.ForeColor = Color.Red
        Dim greenCell As New DataGridViewCellStyle
        greenCell.ForeColor = Color.Green

        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(0).Value = Test
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(1).Value = Low.ToString("0.000")
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(2).Value = measured.ToString("0.000")
        If (Passed) Then
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Style = greenCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Value = "Pass"
        Else
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(2).Style = redCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Style = redCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Value = "Fail"
        End If
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(3).Value = High.ToString("0.000")
        Me.dgvTestResult.CurrentCell = Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(0)
        If inDiagnosticMode Then
            DiagLoop.addResult(Test, Low.ToString("0.000"), measured.ToString("0.000"), High.ToString("0.000"), Passed)
        End If

    End Sub

    Public Sub DisplayStringResult(ByVal Test As String, ByVal Low As String, ByVal measured As String, ByVal High As String, ByVal Passed As Boolean)
        Me.dgvTestResult.Rows.Add()
        Dim redCell As New DataGridViewCellStyle
        redCell.ForeColor = Color.Red
        Dim greenCell As New DataGridViewCellStyle
        greenCell.ForeColor = Color.Green

        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(0).Value = Test
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(1).Value = Low
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(2).Value = measured
        If (Passed) Then
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Style = greenCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Value = "Pass"
        Else
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(2).Style = redCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Style = redCell
            Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(4).Value = "Fail"
        End If
        Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(3).Value = High
        Me.dgvTestResult.CurrentCell = Me.dgvTestResult.Rows(Me.dgvTestResult.Rows.Count - 1).Cells(0)
        If inDiagnosticMode Then
            DiagLoop.addResult(Test, Low, measured, High, Passed)
        End If

    End Sub

    Public Sub DisplayBlankRow()
        Me.dgvTestResult.Rows.Add()
        If inDiagnosticMode Then
            DiagLoop.addResult("", "", "", "", True)
        End If
    End Sub

    Public Sub UpdateGroupsGrid(ByVal TestStep As String)
        Dim name_Renamed As String
        Dim pTestGroups As Collection

        Me.dgvTestGroup.Rows.Clear()
        If Common.TestGroups.Contains(TestStep) = False Then Exit Sub
        pTestGroups = Common.TestGroups.Item(TestStep)


        For Each name_Renamed In pTestGroups
            Me.dgvTestGroup.Rows.Add()
            Me.dgvTestGroup.Rows(Me.dgvTestGroup.Rows.Count - 1).Cells(0).Value = name_Renamed
            Me.dgvTestGroup.CurrentCell = Me.dgvTestGroup.Rows(Me.dgvTestGroup.Rows.Count - 1).Cells(0)
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


    Private Sub DisplayCable(bulkCable As Cable)
        Try
            If bulkCable Is Nothing Then Return
            Me.lblSerialNumber.Text = NewCable.Serial_Number
            Me.lblPartNumber.Text = NewCable.Part_Number
            Me.lblCoreNumber.Text = NewCable.Core_Number
            Me.lblOrderNumber.Text = NewCable.Work_Order
            Me.lblLength.Text = NewCable.Original_Length_M & "/" & String.Format("{0: 0.0}", NewCable.Original_Length_M * 3.281)
            Me.lblTestConnector.Text = NewCable.Test_Connector
            Me.lblTestLength.Text = NewCable.Test_Length_M & "/" & String.Format("{0: 0.0}", NewCable.Test_Length_M * 3.281)
        Catch ex As Exception
            Throw New Exception("frmInterface.DisplayCable()::" & ex.Message)
        End Try
    End Sub
    Private Function LoadTest(bulkCable As Cable) As Boolean
        Try
            If bulkCable Is Nothing Then Return False
            Dim lengthM As Decimal
            If phaseExtendCable Is Nothing Then
                lengthM = bulkCable.Original_Length_M
            Else
                lengthM = phaseExtendCable.test_length_m
            End If
            If TestObj.CheckSN(bulkCable, lengthM) = False Then Return False
            If TestObj.LoadSpec = False Then Return False
            If TestObj.LoadPhaseStatus(bulkCable.Serial_Number, bulkCable.Part_Number) = False Then Return False
            Return True
        Catch ex As Exception
            Throw New Exception("frmInterface.DisplayCable()::" & ex.Message)
        End Try
    End Function
    Private Sub btnNewCable_Click(sender As Object, e As EventArgs) Handles btnNewCable.Click
        Try
            Dim formNewCable As New frmNewCable
            formNewCable.ShowDialog()
            If formNewCable.DialogResult = DialogResult.OK Then
                ClearDataGridView()
                clsOperator.ClearStatusMsg()
                DisplayCable(NewCable)
                If LoadTest(NewCable) = True Then
                    btnRunTest.Enabled = True
                    btnRunTest.Focus()
                Else
                    btnNewCable.Focus()
                    btnRunTest.Enabled = False
                End If
                btnRunTest.Enabled = NewCable IsNot Nothing
                btnLoadTest.Enabled = TestObj.LoadGroupStatus()
            End If
        Catch ex As Exception
            MsgBox("Enter Cable Information()::" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnRunTest_Click(sender As Object, e As EventArgs) Handles btnRunTest.Click
        Try
            RunAllTest()
        Catch ex As Exception
            MsgBox("Run Test()::" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnLoadTest_Click(sender As Object, e As EventArgs) Handles btnLoadTest.Click
        Try
            ClearResults()
            DbCable = TestObj.CheckSN(NewCable.Serial_Number)
            If DbCable Is Nothing Then MsgBox("Cable number: " & NewCable.Serial_Number & " not exist in DB!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information) : Return
            phaseExtendCable = TestObj.LoadPhaseExtendCable(DbCable.Serial_Number, DbCable.Part_Number)
            If phaseExtendCable Is Nothing Then MsgBox("Cable number: " & NewCable.Serial_Number & vbCrLf & "Mode: " & Me.TSDP_Mode.Text & vbCrLf & " not exist in DB!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information) : Return
            If TestObj.LoadPhaseStatus(DbCable.Serial_Number, DbCable.Part_Number) = False Then Return
            If TestObj.LoadGroupStatus() = False Then Return
            If TestObj.LoadMeasDetail() = False Then Return
            TestObj.LoadPimPlot()
        Catch ex As Exception
            MsgBox("btnLoadPlot_Click()::" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub
End Class