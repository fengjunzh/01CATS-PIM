Option Strict Off
Option Explicit On
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Logging
Imports Opc.Ua
Imports Opc.Ua.Client
Imports VB = Microsoft.VisualBasic
Friend Class frmMain
    Inherits System.Windows.Forms.Form

    Dim DiagLoop As frmDiagLoop
    Private cableAssembly1 As CableAssembly
    Private cableAssembly2 As CableAssembly
    Private PIM_Values As Dictionary(Of String, Object)
    Private Sub CmdClearResults_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CmdClearResults.Click
        Call ClearResults()
    End Sub

    Private Sub RunAllTest()
        Try
            Dim TestStep, TestStep2 As String
            Dim tmpCelestica As String = ""
            Dim tmpBarcode As String
            Dim tmpBarcode2 As String
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
            tmpBarcode = Me.txtSN1.Text
            tmpBarcode2 = Me.txtSN2.Text
            'Call ClearResults()
            SetStepStatus(TestStep, 1)
            If TestObj Is Nothing Then
                Common.GUI.REvntPreTest(TestStep, tmpBarcode, Cancel)
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

            If Not pCheck.CheckOne(tmpBarcode, tmpCelestica, PC_String) Then
                Call MsgBox("Process Check Failed", MsgBoxStyle.Critical, "Error")
                Call EnableForm(True)
                Exit Sub
            End If

            'clsOperator.OperatorID = ProcessCheck.OperatorID
            GUI.OperatorID = pCheck.OperatorID
            Common.CelesticaBarcode = tmpCelestica

            'Me.SSTab1.TabCaption(1) = "Test Results - " & tmpBarcode

            'pResults.DCF_File = ProcessCheck.DataFile
            pResults.DCF_File = pCheck.DataFile
            ' Set pStartTestTime = Now
            Call pResults.StartTest(tmpBarcode, TestStep2, (Common.CelesticaBarcode))
            If TestObj Is Nothing Then
                Common.GUI.REvntRunTest(Me.txtSN1.Text, Replace(TestStep, "Invalid_", ""), Cancel)
            Else
                Call TestObj.RunTest(cableAssembly1.SerialNumber, Replace(TestStep, "Invalid_", ""), Cancel)
            End If

            'Call clsOperator.StopTestGroup()
            'Set pStopTestTime = Now
            Call pResults.StopTest()

            'Nothing happened
            If TestObj Is Nothing Then
                Common.GUI.REvntPostTest()
            Else
                Call TestObj.PostTest()
            End If

            'Set step status to "Abort"
            SetStepStatus(TestStep, 4)
            If (pResults.GetFailures() + pResults.GetPasses) > 0 Then
                Call pResults.Save_DCF()

                TestStat = pResults.GetTestStatus
                If Common.PassingUnits And TestStat = "PASS" Then Call pResults.PrintData_2(, False)
                If Common.FailingUnits And TestStat = "FAIL" Then Call pResults.PrintData_2(, False)
                If Common.FailingGroups And TestStat = "FAIL" Then Call pResults.PrintData_2(, True)

                If TestStat = "PASS" Then SetStepStatus(TestStep, 2) : GUI.WritePlcValues(numTestPhases.AfterTest, 1) 'After Test to tell PLC test done and passed
                If TestStat = "FAIL" Then SetStepStatus(TestStep, 3) : GUI.WritePlcValues(numTestPhases.AfterTest, 2) 'After Test to tell PLC test done and failed

                Call pCheck.TransferData()
                'repeatTest = frmTestComplete.TestComplete(pResults.GetFailures() = 0, Common.enableRepeat)
                'frmTestComplete.TestComplete(pResults.GetFailures() = 0, Common.enableRepeat)
            End If

        Catch ex As Exception
            ' After Test Error Handling, to tell PLC test done and failed
            GUI.WritePlcValues(numTestPhases.Err)
            Call MsgBox(Err.Description, MsgBoxStyle.Critical, "Unhandled exception")
        Finally
            Call EnableForm(True)
            txtSN1.Clear()
        End Try

    End Sub

    Private Sub frmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        AddHandler Me._mnuCalibrate_1.Click, AddressOf Me.mnuCalibrate_Click
        AddHandler Me._mnuTroubleshoot_0.Click, AddressOf Me.mnuTroubleshootCustom_Click
        '  Me.mnuTroubleshootCustom(0).Enabled = False
        '  mnuHelpCustom(0).Enabled = False
        '  mnuCalibrateCustom(0).Enabled = False
        txtSN2.Enabled = False
        lblSerialNumber1.Text = ""
        lblSerialNumber2.Text = ""
        lblOrderNumber.Text = ""
        lblPartNumber.Text = ""
        lblLength.Text = ""
        lblUnit.Text = ""
        tsslPCName.Text = "PC: " & My.Computer.Name
        tsslUserName.Text = "User: " & Environment.UserName.ToUpper
        tsslDynamicStatic.Text = ""
        txtSN1.Focus()
        txtSN1.Select()
    End Sub

    Private cts As CancellationTokenSource
    ' Start watching asynchronously
    Public Async Sub SendBeatHeart()
        cts = New CancellationTokenSource()
        Dim token = cts.Token
        Dim heartBeat As Boolean
        Try
            Await Task.Run(Async Function()
                               While Not token.IsCancellationRequested
                                   GUI.WritePlcValues(numTestPhases.HeartBeat,, IIf(heartBeat, 1, 0))
                                   heartBeat = Not heartBeat
                                   ' Wait 1 second asynchronously
                                   Await Task.Delay(1000, token)
                               End While
                           End Function, token)
        Catch ex As TaskCanceledException
            Console.WriteLine("Watching canceled.")
        End Try
    End Sub
    ' Stop watching
    Public Sub StopWatching()
        If cts IsNot Nothing Then
            cts.Cancel()
        End If
    End Sub
    Private Sub frmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        If TestObj Is Nothing Then
            Common.GUI.REvntExitInterface(0)
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
                    Common.GUI.REvntMenuClick("Tools", itemObject.Text)
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
                    Common.GUI.REvntMenuClick("SAP", itemObject.Text)
                Else
                    Call TestObj.MenuClick("SAP", itemObject.Text)
                End If
                Call EnableForm(True)
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
            End Try
        End If
    End Sub
    Public Sub AppendTextToLog(newText As String)
        Me.Invoke(New MethodInvoker(Sub()
                                        Me.TxtStatus.AppendText($"{DateTime.Now.ToString("HH:mm:ss:fff")} : {newText}" &
                                                     Environment.NewLine)
                                    End Sub))
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
                    Common.GUI.REvntMenuClick("Calibrate", itemObject.Text)
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
                    Common.GUI.REvntMenuClick("Help", itemObject.Text)
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
                    Common.GUI.REvntMenuClick("Products", itemObject.Text)
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
                    Common.GUI.REvntMenuClick("Troubleshoot", itemObject.Text)
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
                Common.GUI.REvntRunTestGroup(TGName, pResults.Serial_Number, tmpProcess)
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
        CmdClearResults.Enabled = Enable
        'CmdRunAll.Enabled = Enable
        Me.dgvTestGroup.Enabled = Enable
        dgvTestPhase.Enabled = Enable
        'UPGRADE_ISSUE: Form property frmMain.MousePointer does not support custom mousepointers. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="45116EAB-7060-405E-8ABE-9DBB40DC2E86"'
        Cursor = IIf(Enable, System.Windows.Forms.Cursors.Default, System.Windows.Forms.Cursors.WaitCursor)
        mnuTroubleshootMain.Enabled = Enable
        mnuCalibrateMain.Enabled = Enable
        mnuToolsMain.Enabled = Enable
        If Enable Then
            txtSN1.Focus()
            txtSN1.SelectAll()
        End If
    End Sub

    Public Sub ClearResults()
        Dim i As Integer

        For i = 0 To Me.dgvTestPhase.Rows.Count - 1
            Call SetStepStatus(Me.dgvTestPhase.Rows(i).Cells(0).Value, 0)
        Next

        For i = 0 To Me.dgvTestGroup.Rows.Count - 1
            Call SetGroupStatus(Me.dgvTestGroup.Rows(i).Cells(0).Value, 0)
        Next

        Call GUI.ClearStatusMsg()
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
            Me.txtSN1.Text = tmp1
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
    Private Sub txtSN2_TextChanged(sender As Object, e As EventArgs) Handles txtSN2.TextChanged
        If txtSN2.Text.Length = 7 OrElse txtSN2.Text.Length = 13 Then

            If Me.txtSN2.Text.Length = 7 Then
                If Not IsNumeric(Me.txtSN2.Text) Then
                    Return
                End If
            End If
            cableAssembly2 = New CableAssembly
            cableAssembly2.SerialNumber = txtSN2.Text
            If txtSN1.Text = txtSN2.Text Then
                MsgBox("Input SN2 can't be same as SN1", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                txtSN2.Focus()
                txtSN2.SelectAll()
                Exit Sub
            End If
            If TestObj.CheckSN(cableAssembly2, True) = True Then
                If cableAssembly1.CompareTo(cableAssembly2) Then
                    lblSerialNumber2.Text = cableAssembly2.SerialNumber
                    RunAllTest()
                Else
                    txtSN1.Focus()
                    txtSN1.SelectAll()
                    txtSN2.Clear()
                    txtSN2.BackColor = SystemColors.WindowText
                    txtSN2.Enabled = False
                End If
            Else
                txtSN1.Focus()
                txtSN1.SelectAll()
                txtSN2.Clear()
                txtSN2.BackColor = SystemColors.ControlDarkDark
                txtSN2.Enabled = False
            End If
        End If
    End Sub

    Private Sub DisplayCableAssembly(cableAssembly As CableAssembly)
        Me.lblSerialNumber1.Text = cableAssembly.SerialNumber
        Me.lblPartNumber.Text = cableAssembly.PartNumber
        Me.lblOrderNumber.Text = cableAssembly.OrderNumber
        Me.lblLength.Text = cableAssembly.Length
        Me.lblUnit.Text = cableAssembly.UOM
    End Sub

    Private Sub txtSN1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSN1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If TestObj Is Nothing Then Return
                Dim readSN As String = TestObj.ReadScan
                If readSN Is Nothing Then readSN = Me.txtSN1.Text
                If readSN.Length = 0 Then Return
                If Not (readSN = gLowPimLoadTestSn Or readSN = gInputLowPimLoadTestSn) Then
                    If Not (readSN.Length = 7 Or readSN.Length = 13) Then Return
                End If
                Me.txtSN1.Text = readSN
                If Me.txtSN1.Text.Length = 7 Then
                    If Not IsNumeric(Me.txtSN1.Text) Then
                        Return
                    End If
                End If
                cableAssembly1 = New CableAssembly
                cableAssembly1.SerialNumber = txtSN1.Text
                ClearDataGridView()
                If TestObj IsNot Nothing Then
                    If TestObj.CheckSN(cableAssembly1, False) = True Then
                        If cableAssembly1 IsNot Nothing Then
                            DisplayCableAssembly(cableAssembly1)
                            lblSerialNumber2.Text = "N/A"
                        End If
                        If TestObj.LoadSpec() = True Then
                            'ShowSelectedStep()
                            If TestObj.LoadPhaseStatus(cableAssembly1.SerialNumber, cableAssembly1.PartNumber) = True Then
                                TestObj.LoadPimTestData(cableAssembly1.SerialNumber, cableAssembly1.PartNumber, GUI.SelectedTestPhase)
                                If TestDoubleLength Then
                                    txtSN2.BackColor = SystemColors.Window
                                    txtSN2.Enabled = True
                                    txtSN2.Focus()
                                    txtSN2.SelectAll()
                                Else
                                    txtSN2.Clear()
                                    txtSN2.BackColor = SystemColors.WindowText
                                    txtSN2.Enabled = False
                                    RunAllTest()
                                End If
                            End If
                        Else
                            txtSN1.Focus()
                            txtSN1.SelectAll()
                            txtSN2.Clear()
                            txtSN2.BackColor = SystemColors.WindowText
                            txtSN2.Enabled = False
                        End If
                    Else
                        txtSN1.Focus()
                        txtSN1.SelectAll()
                        txtSN2.Clear()
                        txtSN2.BackColor = SystemColors.WindowText
                        txtSN2.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
        End Try
    End Sub

    Private Sub txtSN1_TextChanged(sender As Object, e As EventArgs) Handles txtSN1.TextChanged
        Try
            If TestObj Is Nothing Then Return
            Dim readSN As String = TestObj.ReadScan
            If readSN Is Nothing Then readSN = Me.txtSN1.Text
            If readSN.Length = 0 Then Return
            If Not (readSN = gLowPimLoadTestSn Or readSN = gInputLowPimLoadTestSn) Then
                If Not (readSN.Length = 7 Or readSN.Length = 13) Then Return
            End If
            Me.txtSN1.Text = readSN
            If Me.txtSN1.Text.Length = 7 Then
                If Not IsNumeric(Me.txtSN1.Text) Then
                    Return
                End If
            End If
            cableAssembly1 = New CableAssembly
            cableAssembly1.SerialNumber = txtSN1.Text
            ClearDataGridView()
            If TestObj IsNot Nothing And (gAutomation And gTrigger_PIM) Then
                If TestObj.CheckSN(cableAssembly1, False) = True Then
                    If cableAssembly1 IsNot Nothing Then
                        DisplayCableAssembly(cableAssembly1)
                        lblSerialNumber2.Text = "N/A"
                    End If
                    If TestObj.LoadSpec() = True Then
                        'ShowSelectedStep()
                        If TestObj.LoadPhaseStatus(cableAssembly1.SerialNumber, cableAssembly1.PartNumber) = True Then
                            TestObj.LoadPimTestData(cableAssembly1.SerialNumber, cableAssembly1.PartNumber, GUI.SelectedTestPhase)
                            If TestDoubleLength Then
                                txtSN2.BackColor = SystemColors.Window
                                txtSN2.Enabled = True
                                txtSN2.Focus()
                                txtSN2.SelectAll()
                            Else
                                txtSN2.Clear()
                                txtSN2.BackColor = SystemColors.WindowText
                                txtSN2.Enabled = False
                                RunAllTest()
                            End If
                        End If
                    Else
                        txtSN1.Focus()
                        txtSN1.SelectAll()
                        txtSN2.Clear()
                        txtSN2.BackColor = SystemColors.WindowText
                        txtSN2.Enabled = False
                    End If
                Else
                    txtSN1.Focus()
                    txtSN1.SelectAll()
                    txtSN2.Clear()
                    txtSN2.BackColor = SystemColors.WindowText
                    txtSN2.Enabled = False
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Critical, "Unhandled exception")
        End Try
    End Sub
End Class
Public Enum numTestPhases
    NULL = 0
    RequestSn = 1
    PreparedReady = 2 'Spec loaded and PIM instrument initialized
    RotateFixture = 3 'Request to rotate fixture
    AfterTest = 4 'After all test done to send PLC test result
    HeartBeat = 5 'Heartbeat signal to PLC
    Err = 6 'Error occurred during test
    FinallyResetTrigger = 7 'Reset trigger signal after test completed
End Enum