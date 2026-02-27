Option Strict Off
Imports System
Imports System.DateTime
Imports System.Drawing
Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Public Class frmDiagLoop
    Dim testStep As String
    Dim testStep2 As String
    Dim runForever As Boolean
    Dim runCount As Long
    Dim delayInterval As Long
    Dim stopTest As Boolean
    Dim abortNow As Boolean
    Dim unsavedResults As Boolean

    Dim loopCount As Long   'How many loops have been run
    Dim iterStartTime As Date
    Dim frmMain As frmMain

    Friend Sub SetMain(ByRef Main As frmMain)
        frmMain = Main
    End Sub

    Private Sub frmDiagLoop_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.LstTestStep.SelectedIndex = 0
        testStep = Me.LstTestStep.Items(0).ToString
        testStep2 = Replace(testStep, " ", "_")
    End Sub

    Private Sub frmDiagLoop_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CancelAction()
    End Sub
    Private Sub frmDiagLoop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        testStep = ""
        runForever = False
        delayInterval = 10
        runCount = 10
        loopCount = 0
        stopTest = False
        abortNow = False
        unsavedResults = False
        iterStartTime = Now()

    End Sub

    Private Sub LstTestStep_Click()
        testStep = Me.LstTestStep.Items(0).ToString
        testStep2 = Replace(testStep, " ", "_")
        Call frmMain.UpdateGroupsGrid(testStep)
    End Sub

    'Private Sub Option1_Click()
    '    Me.Text1.Enabled = False
    'End Sub

    'Private Sub Option2_Click()
    '    Me.Text1.Enabled = True
    'End Sub

    Friend Sub addResult(ByVal Test As String, ByVal Min As String, ByVal Meas As String, ByVal Max As String, ByVal Pass As Boolean)
        Dim status As String
        Dim elapsedTime As TimeSpan
        Dim duration As String

        If Pass Then
            status = "OK"
        Else
            status = "FAIL"
        End If

        elapsedTime = Now() - iterStartTime   'DateDiff("s", iterStartTime, Now())
        If Len(Meas) = 0 Then
            status = ""
            duration = ""
        Else
            duration = elapsedTime.Seconds.ToString
        End If

        lvResults.Items.Add(Test)   '(lvItemCount, , Test)
        If Meas.Length = 0 And Test.Length > 0 Then
            lvResults.Items(lvResults.Items.Count - 1).BackColor = Color.PowderBlue
        Else
            lvResults.Items(lvResults.Items.Count - 1).UseItemStyleForSubItems = False
        End If
        lvResults.Items(lvResults.Items.Count - 1).SubItems.Add(Min)
        lvResults.Items(lvResults.Items.Count - 1).SubItems.Add(Meas)
        If Not Pass Then lvResults.Items(lvResults.Items.Count - 1).SubItems(2).ForeColor = Color.Red
        lvResults.Items(lvResults.Items.Count - 1).SubItems.Add(Max)
        lvResults.Items(lvResults.Items.Count - 1).SubItems.Add(status)
        lvResults.Items(lvResults.Items.Count - 1).SubItems.Add(duration)
        lvResults.Items(lvResults.Items.Count - 1).EnsureVisible()

    End Sub

    Private Sub Text1_KeyUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Text1.KeyUp
        If e.KeyCode > 47 And e.KeyCode < 58 Then
            runCount = CLng(Me.Text1.Text)
            If runCount > 250 Then
                runCount = 250
                Me.Text1.Text = runCount.ToString
                Beep()
            End If
        Else
            Me.Text1.Text = runCount.ToString
            Beep()
        End If
    End Sub

    Private Sub Text2_KeyUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Text2.KeyUp
        If e.KeyCode > 47 And e.KeyCode < 58 Then
            delayInterval = CLng(Me.Text2.Text)
        Else
            Me.Text2.Text = CStr(delayInterval)
            Beep()
        End If
    End Sub

    Private Sub runStopButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles runStopButton.Click

        Dim tmpBarcode As String
        Dim tmpCelestica As String = String.Empty
        Dim count As Long
        Dim i As Integer
        Dim saveMsg As String
        Dim passCountDown As Long

        'Do a quick check to make sure everything makes sense

        'Start loop
        Try

            If unsavedResults Then
                saveMsg = "You have unsaved data." & vbCrLf &
                      "If you run another loop test, this data will be lost." & vbCrLf &
                      "Do you want to save this data before running another test?"

                If MsgBox(saveMsg, vbYesNo, "Unsaved Data") = vbYes Then
                    SaveResultsAction()
                End If
            End If

            Label9.Text = "Preparing to run pass 1" & vbCrLf
            Label9.Refresh()

            lvResults.Items.Clear()
            stopTest = False
            abortNow = False
            'Me.runStopButton.Caption = "Stop Test"
            Me.runStopButton.Enabled = False
            Me.LstTestStep.Enabled = False
            Me.btnAbort.Enabled = True
            Me.Text1.Enabled = False
            Me.Text2.Enabled = False
            '    Me.Option1.Enabled = False
            '    Me.Option2.Enabled = False
            Me.cmdCancelButton.Enabled = False
            Me.btnSaveResults.Enabled = False
            unsavedResults = False


            Call RFPATestInterface.Globals_Renamed.Get_Testset_INI_Info()

            pResults.Test_System_ID = Common.Testset_ID
            pResults.Production_Site_ID = Common.Location
            pResults.Assembly_Type = Common.UUT_Type
            pResults.Test_SW_Rev = Common.SW_Version
            If Common.LimitsFile <> "" Then
                Call pResults.SetLimitsFile(Common.LimitsFile)
            End If

            Dim Cancel As Boolean
            Call frmMain.ClearResults()
            tmpBarcode = ""

            If TestObj Is Nothing Then
                Common.clsOperator.REvntPreTest(Replace(testStep, "Invalid_", ""), tmpBarcode, Cancel)
            Else
                Call TestObj.PreTest(Replace(testStep, "Invalid_", ""), tmpBarcode, Cancel)
            End If
            If Cancel Then
                Call MsgBox("Test will abort", vbCritical, "Error")
                Me.cmdCancelButton.Text = "Done"
                stopTest = False
                abortNow = False
                Me.LstTestStep.Enabled = True
                Me.Text1.Enabled = True
                Me.Text2.Enabled = True
                Me.cmdCancelButton.Enabled = True
                Me.runStopButton.Enabled = True
                Me.btnAbort.Enabled = False
                btnSaveResults.Enabled = False
                unsavedResults = False
                Exit Sub
            End If

            If tmpBarcode = "" Then
                If Not frmMain.GetUUTInfo2() Then
                    '            Call EnableForm(True)
                    Exit Sub
                End If
                tmpBarcode = frmMain.lblSerialNumber.Text
            Else
                frmMain.lblSerialNumber.Text = tmpBarcode
            End If

            '    PC_Status = ""
            '    pCheck.testStep = testStep2
            '    If Not pCheck.CheckOne(tmpBarcode, tmpCelestica, PC_String) Then
            '        Call MsgBox("Process Check Failed", vbCritical, "Error")
            ''        Call EnableForm(True)
            '        Exit Sub
            '    End If
            '
            Common.CelesticaBarcode = tmpCelestica
            '
            '    'Me.SSTab1.TabCaption(1) = "Test Results - " & tmpBarcode
            '
            '    pResults.DCF_File = pCheck.DataFile
            Call pResults.StartTest(tmpBarcode, testStep2, Common.CelesticaBarcode)
            count = 0
            If runForever Then
                While Not stopTest And Not abortNow
                    If count > 0 Then
                        For i = 0 To delayInterval
                            System.Windows.Forms.Application.DoEvents()
                            If stopTest Or abortNow Then Exit For
                            System.Threading.Thread.Sleep(1000)
                        Next
                    End If
                    count = count + 1
                    If Not stopTest And Not abortNow Then
                        Me.Label9.Text = count.ToString
                        Call frmMain.ClearResults()
                        addResult("", "", "", "", True)
                        Call frmMain.DisplayStringResult("Test Iteration " & count, Format(Now(), "hh:mm:ss"), "", "", True)
                        '                addResult "Test Iteration " & count, "", "", "", True
                        iterStartTime = Now()
                        If TestObj Is Nothing Then
                            Common.clsOperator.REvntRunTest(tmpBarcode, Replace(testStep, "Invalid_", ""), Cancel)
                        Else
                            Call TestObj.RunTest(frmMain.lblSerialNumber.Text, Replace(testStep, "Invalid_", ""), Cancel)
                        End If
                    End If
                End While
            Else
                While count < runCount And Not stopTest And Not abortNow
                    If count > 0 Then
                        For i = 0 To delayInterval
                            System.Windows.Forms.Application.DoEvents()
                            If stopTest Or abortNow Then Exit For
                            System.Threading.Thread.Sleep(1000)
                            passCountDown = passCountDown - 1
                            Label9.Text = "Pass " & count.ToString & " complete." & vbCrLf & "Waiting " & passCountDown.ToString & " seconds for next pass."
                        Next
                    End If
                    count = count + 1
                    If Not stopTest And Not abortNow Then
                        Me.Label9.Text = "Running pass " & count.ToString & vbCrLf
                        Call frmMain.ClearResults()
                        addResult("", "", "", "", True)
                        Call frmMain.DisplayStringResult("Test Iteration " & count, Format(Now(), "hh:mm:ss"), "", "", True)
                        '                addResult "Test Iteration " & count, "", "", "", True
                        iterStartTime = Now()
                        If TestObj Is Nothing Then
                            Common.clsOperator.REvntRunTest(frmMain.lblSerialNumber.Text, Replace(testStep, "Invalid_", ""), Cancel)
                        Else
                            Call TestObj.RunTest(tmpBarcode, Replace(testStep, "Invalid_", ""), Cancel)
                        End If
                        If count < runCount Then
                            passCountDown = delayInterval
                            Label9.Text = "Pass " & count.ToString & " complete." & vbCrLf & "Waiting " & passCountDown.ToString & " seconds for next pass."
                        Else
                            Label9.Text = "All tests complete." & vbCrLf & "Please save your data."
                        End If
                    End If
                End While
                If abortNow Then
                    Label9.Text = "Testing aborted at pass " & count.ToString & vbCrLf & "Please save your data."
                End If
            End If

            Me.cmdCancelButton.Text = "Done"
            stopTest = False
            abortNow = False
            Me.LstTestStep.Enabled = True
            Me.Text1.Enabled = True
            Me.Text2.Enabled = True
            '    Me.Option1.Enabled = True
            '    Me.Option2.Enabled = True
            Me.cmdCancelButton.Enabled = True
            Me.runStopButton.Enabled = True
            Me.btnAbort.Enabled = False
            btnSaveResults.Enabled = True
            unsavedResults = True

        Catch

            Call MsgBox(Err.Description, vbCritical, "Unhandled exception")

        End Try

    End Sub

    Private Sub cmdCancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancelButton.Click
        CancelAction()
    End Sub

    Private Sub CancelAction()
        Dim saveMsg As String

        If unsavedResults Then
            saveMsg = "You have unsaved data." & vbCrLf &
                  "If you exit now, this data will be lost." & vbCrLf &
                  "Do you want to save this data before exiting?"

            If MsgBox(saveMsg, vbYesNo, "Unsaved Data") = vbYes Then
                SaveResultsAction()
            End If
        End If
        unsavedResults = False
        Me.Hide()
    End Sub
    Private Sub btnSaveResults_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveResults.Click
        SaveResultsAction()
    End Sub

    Private Sub SaveResultsAction()
        Dim fname As String
        Dim PrintFile As Integer
        Dim i As Integer
        Dim testNames As New Collection
        Dim outLines() As String
        Dim strIndx As Long

        Me.SaveFileDialog1.OverwritePrompt = True
        Me.SaveFileDialog1.FileName = "Diag_Loop_Data_For_" & pResults.Serial_Number & ".csv"
        Me.SaveFileDialog1.InitialDirectory = "c:\"
        Me.SaveFileDialog1.DefaultExt = "csv"
        Me.SaveFileDialog1.Filter = "CSV (*.csv)|*.csv"
        Dim result As DialogResult = Me.SaveFileDialog1.ShowDialog()
        fname = Me.SaveFileDialog1.FileName

        If (result = System.Windows.Forms.DialogResult.OK) Then

            'Create the csv file here
            Dim file As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(fname, False)

            file.WriteLine("""AssemblyType"",""" & pResults.Assembly_Type & """")
            file.WriteLine("""SoftwareRef"", """ & pResults.Test_SW_Rev & """")
            file.WriteLine("""Date"",""" & Now().ToLongDateString & """")
            file.WriteLine("""SerialNumber"",""" & pResults.Serial_Number & """")
            file.WriteLine("""ProcessStep"",""" & pResults.Process_Step_ID & """")
            file.WriteLine("""Testset ID"",""" & pResults.Test_System_ID & """")
            file.WriteLine()
            'Write #PrintFile, "Measurement Name", "LL", "Value", "UL", "Status", "Elapsed Time"

            ReDim Preserve outLines(2)
            outLines(0) = """Test Iteration"",,"
            outLines(1) = """Test Time"",,"
            outLines(2) = """TEST"",""LL"",""UL"""

            For i = 0 To lvResults.Items.Count - 1
                If lvResults.Items(i).Text.Length > 0 Then
                    If StrComp("Test Iteration", Mid(lvResults.Items(i).Text, 1, 14), vbTextCompare) = 0 Then
                        outLines(0) = outLines(0) & ",""" & Mid(lvResults.Items(i).Text, 15) & """"
                        outLines(1) = outLines(1) & ",""" & lvResults.Items(i).SubItems(0).Text & """"
                    Else
                        On Error Resume Next
                        strIndx = testNames(UCase(lvResults.Items(i).Text))
                        If Err.Number <> 0 Then
                            ReDim Preserve outLines(UBound(outLines) + 1)
                            strIndx = UBound(outLines)
                            testNames.Add(strIndx, UCase(lvResults.Items(i).Text))
                            outLines(strIndx) = outLines(strIndx) & lvResults.Items(i).Text & """,""" & lvResults.Items(i).SubItems(1).Text & """,""" & lvResults.Items(i).SubItems(3).Text & """"
                            Err.Clear()
                        End If
                        On Error GoTo 0
                        outLines(strIndx) = outLines(strIndx) & ",""" & lvResults.Items(i).SubItems(2).Text & """"
                    End If
                End If

                '        Write #PrintFile, lvResults.ListItems(i).Text, lvResults.ListItems(i).ListSubItems(1).Text, lvResults.ListItems(i).ListSubItems(2).Text, lvResults.ListItems(i).ListSubItems(3).Text, lvResults.ListItems(i).ListSubItems(4).Text, lvResults.ListItems(i).ListSubItems(5).Text
            Next

            For i = 0 To UBound(outLines)
                file.WriteLine(outLines(i))
            Next

            file.Close()

        End If
        'Finish up
        Me.btnSaveResults.Enabled = False
        Label9.Text = "Data saved." & vbCrLf & "Please make selections."
        unsavedResults = False

    End Sub
End Class