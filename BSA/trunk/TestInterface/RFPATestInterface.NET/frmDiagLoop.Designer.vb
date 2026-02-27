<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiagLoop
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LstTestStep = New System.Windows.Forms.ListBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Text1 = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Text2 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnAbort = New System.Windows.Forms.Button
        Me.runStopButton = New System.Windows.Forms.Button
        Me.cmdCancelButton = New System.Windows.Forms.Button
        Me.btnSaveResults = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.lvResults = New System.Windows.Forms.ListView
        Me.Test = New System.Windows.Forms.ColumnHeader
        Me.Min = New System.Windows.Forms.ColumnHeader
        Me.Measured = New System.Windows.Forms.ColumnHeader
        Me.Max = New System.Windows.Forms.ColumnHeader
        Me.Status = New System.Windows.Forms.ColumnHeader
        Me.ElapsedTime = New System.Windows.Forms.ColumnHeader
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(109, -1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(297, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Diagnostic Loop Test"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(40, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "1. Select Test"
        '
        'LstTestStep
        '
        Me.LstTestStep.BackColor = System.Drawing.SystemColors.Window
        Me.LstTestStep.Cursor = System.Windows.Forms.Cursors.Default
        Me.LstTestStep.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstTestStep.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LstTestStep.ItemHeight = 14
        Me.LstTestStep.Location = New System.Drawing.Point(43, 67)
        Me.LstTestStep.Name = "LstTestStep"
        Me.LstTestStep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LstTestStep.Size = New System.Drawing.Size(153, 60)
        Me.LstTestStep.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(248, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(173, 32)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "2. Enter the number of times" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    to run the test, 250 Max"
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(264, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(139, 16)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Run                   times"
        '
        'Text1
        '
        Me.Text1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Text1.Location = New System.Drawing.Point(296, 93)
        Me.Text1.Name = "Text1"
        Me.Text1.Size = New System.Drawing.Size(65, 22)
        Me.Text1.TabIndex = 7
        Me.Text1.Text = "10"
        Me.Text1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(40, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(144, 32)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "3. Enter a delay interval" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    between loops."
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.Location = New System.Drawing.Point(36, 189)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(170, 16)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Delay                    seconds"
        '
        'Text2
        '
        Me.Text2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Text2.Location = New System.Drawing.Point(79, 188)
        Me.Text2.Name = "Text2"
        Me.Text2.Size = New System.Drawing.Size(65, 22)
        Me.Text2.TabIndex = 10
        Me.Text2.Text = "10"
        Me.Text2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(5, 219)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 16)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Status"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Location = New System.Drawing.Point(8, 235)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(250, 37)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Please make selections." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAbort
        '
        Me.btnAbort.Enabled = False
        Me.btnAbort.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbort.Location = New System.Drawing.Point(216, 197)
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Size = New System.Drawing.Size(89, 33)
        Me.btnAbort.TabIndex = 13
        Me.btnAbort.Text = "Abort"
        Me.btnAbort.UseVisualStyleBackColor = True
        '
        'runStopButton
        '
        Me.runStopButton.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.runStopButton.Location = New System.Drawing.Point(311, 197)
        Me.runStopButton.Name = "runStopButton"
        Me.runStopButton.Size = New System.Drawing.Size(89, 33)
        Me.runStopButton.TabIndex = 14
        Me.runStopButton.Text = "Run Test"
        Me.runStopButton.UseVisualStyleBackColor = True
        '
        'cmdCancelButton
        '
        Me.cmdCancelButton.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelButton.Location = New System.Drawing.Point(406, 197)
        Me.cmdCancelButton.Name = "cmdCancelButton"
        Me.cmdCancelButton.Size = New System.Drawing.Size(89, 33)
        Me.cmdCancelButton.TabIndex = 15
        Me.cmdCancelButton.Text = "Cancel"
        Me.cmdCancelButton.UseVisualStyleBackColor = True
        '
        'btnSaveResults
        '
        Me.btnSaveResults.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveResults.Location = New System.Drawing.Point(326, 239)
        Me.btnSaveResults.Name = "btnSaveResults"
        Me.btnSaveResults.Size = New System.Drawing.Size(169, 33)
        Me.btnSaveResults.TabIndex = 16
        Me.btnSaveResults.Text = "Save Results"
        Me.btnSaveResults.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 272)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 16)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Test Results"
        '
        'lvResults
        '
        Me.lvResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Test, Me.Min, Me.Measured, Me.Max, Me.Status, Me.ElapsedTime})
        Me.lvResults.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvResults.GridLines = True
        Me.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvResults.Location = New System.Drawing.Point(8, 288)
        Me.lvResults.Name = "lvResults"
        Me.lvResults.Size = New System.Drawing.Size(497, 225)
        Me.lvResults.TabIndex = 18
        Me.lvResults.UseCompatibleStateImageBehavior = False
        Me.lvResults.View = System.Windows.Forms.View.Details
        '
        'Test
        '
        Me.Test.Text = "Test"
        Me.Test.Width = 200
        '
        'Min
        '
        Me.Min.Text = "Min"
        Me.Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Measured
        '
        Me.Measured.Text = "Measured"
        Me.Measured.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Max
        '
        Me.Max.Text = "Max"
        Me.Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Status
        '
        Me.Status.Text = "Status"
        Me.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Status.Width = 40
        '
        'ElapsedTime
        '
        Me.ElapsedTime.Text = "Elapsed Time"
        Me.ElapsedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'frmDiagLoop
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(514, 519)
        Me.Controls.Add(Me.lvResults)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnSaveResults)
        Me.Controls.Add(Me.cmdCancelButton)
        Me.Controls.Add(Me.runStopButton)
        Me.Controls.Add(Me.btnAbort)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Text2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Text1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LstTestStep)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDiagLoop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Diagnostic Loop Test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents LstTestStep As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Text1 As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Text2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnAbort As System.Windows.Forms.Button
    Friend WithEvents runStopButton As System.Windows.Forms.Button
    Friend WithEvents cmdCancelButton As System.Windows.Forms.Button
    Friend WithEvents btnSaveResults As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lvResults As System.Windows.Forms.ListView
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Test As System.Windows.Forms.ColumnHeader
    Friend WithEvents Min As System.Windows.Forms.ColumnHeader
    Friend WithEvents Measured As System.Windows.Forms.ColumnHeader
    Friend WithEvents Max As System.Windows.Forms.ColumnHeader
    Friend WithEvents Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents ElapsedTime As System.Windows.Forms.ColumnHeader
End Class
