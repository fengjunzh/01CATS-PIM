<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDebug
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.btnStart = New System.Windows.Forms.Button()
    Me.btnExit = New System.Windows.Forms.Button()
    Me.lstFreqBands = New System.Windows.Forms.ListBox()
    Me.txtLimit = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnStart
    '
    Me.btnStart.Location = New System.Drawing.Point(18, 197)
    Me.btnStart.Name = "btnStart"
    Me.btnStart.Size = New System.Drawing.Size(72, 23)
    Me.btnStart.TabIndex = 1
    Me.btnStart.Text = "Start Test"
    Me.btnStart.UseVisualStyleBackColor = True
    '
    'btnExit
    '
    Me.btnExit.Location = New System.Drawing.Point(111, 197)
    Me.btnExit.Name = "btnExit"
    Me.btnExit.Size = New System.Drawing.Size(72, 23)
    Me.btnExit.TabIndex = 2
    Me.btnExit.Text = "Exit"
    Me.btnExit.UseVisualStyleBackColor = True
    '
    'lstFreqBands
    '
    Me.lstFreqBands.FormattingEnabled = True
    Me.lstFreqBands.Location = New System.Drawing.Point(9, 19)
    Me.lstFreqBands.Name = "lstFreqBands"
    Me.lstFreqBands.Size = New System.Drawing.Size(185, 95)
    Me.lstFreqBands.TabIndex = 7
    '
    'txtLimit
    '
    Me.txtLimit.Location = New System.Drawing.Point(45, 19)
    Me.txtLimit.Name = "txtLimit"
    Me.txtLimit.Size = New System.Drawing.Size(111, 20)
    Me.txtLimit.TabIndex = 9
    Me.txtLimit.Text = "-153"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(8, 22)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(31, 13)
    Me.Label3.TabIndex = 8
    Me.Label3.Text = "Limit:"
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(162, 22)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(26, 13)
    Me.Label4.TabIndex = 10
    Me.Label4.Text = "dBc"
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.txtLimit)
    Me.GroupBox1.Controls.Add(Me.Label4)
    Me.GroupBox1.Controls.Add(Me.Label3)
    Me.GroupBox1.Location = New System.Drawing.Point(7, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(200, 50)
    Me.GroupBox1.TabIndex = 11
    Me.GroupBox1.TabStop = False
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.lstFreqBands)
    Me.GroupBox2.Location = New System.Drawing.Point(7, 69)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(200, 122)
    Me.GroupBox2.TabIndex = 12
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Freq Bands"
    '
    'FormDebug
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(216, 234)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
    Me.Controls.Add(Me.btnExit)
    Me.Controls.Add(Me.btnStart)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.Name = "FormDebug"
    Me.Opacity = 0.95R
    Me.Text = "Debug Test"
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.GroupBox2.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents btnStart As System.Windows.Forms.Button
  Friend WithEvents btnExit As System.Windows.Forms.Button
  Friend WithEvents lstFreqBands As System.Windows.Forms.ListBox
  Friend WithEvents txtLimit As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents Label4 As System.Windows.Forms.Label
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
