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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GetDTP_Time = New System.Windows.Forms.GroupBox()
        Me.AccuracyValue = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.StartTest_DTF = New System.Windows.Forms.Button()
        Me.DTP_Time = New System.Windows.Forms.Label()
        Me.SpeedValue = New System.Windows.Forms.ComboBox()
        Me.Labeo0 = New System.Windows.Forms.Label()
        Me.Calibration_DTF = New System.Windows.Forms.GroupBox()
        Me.Start_Calibration = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GetDTP_Time.SuspendLayout()
        Me.Calibration_DTF.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.Location = New System.Drawing.Point(80, 610)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(144, 44)
        Me.btnStart.TabIndex = 1
        Me.btnStart.Text = "Start Test"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(266, 610)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(144, 44)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'lstFreqBands
        '
        Me.lstFreqBands.FormattingEnabled = True
        Me.lstFreqBands.ItemHeight = 28
        Me.lstFreqBands.Location = New System.Drawing.Point(18, 37)
        Me.lstFreqBands.Margin = New System.Windows.Forms.Padding(6)
        Me.lstFreqBands.Name = "lstFreqBands"
        Me.lstFreqBands.Size = New System.Drawing.Size(366, 340)
        Me.lstFreqBands.TabIndex = 7
        '
        'txtLimit
        '
        Me.txtLimit.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLimit.Location = New System.Drawing.Point(90, 37)
        Me.txtLimit.Margin = New System.Windows.Forms.Padding(6)
        Me.txtLimit.Name = "txtLimit"
        Me.txtLimit.Size = New System.Drawing.Size(218, 37)
        Me.txtLimit.TabIndex = 9
        Me.txtLimit.Text = "-153"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 42)
        Me.Label3.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 28)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Limit:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(324, 42)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 28)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "dBc"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLimit)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(50, 44)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox1.Size = New System.Drawing.Size(400, 96)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstFreqBands)
        Me.GroupBox2.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(50, 178)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(6)
        Me.GroupBox2.Size = New System.Drawing.Size(400, 393)
        Me.GroupBox2.TabIndex = 12
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Freq Bands"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.btnStart)
        Me.GroupBox3.Controls.Add(Me.btnExit)
        Me.GroupBox3.Location = New System.Drawing.Point(47, 42)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(496, 688)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "PIM test"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GetDTP_Time)
        Me.GroupBox4.Controls.Add(Me.Calibration_DTF)
        Me.GroupBox4.Location = New System.Drawing.Point(587, 42)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(598, 688)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "DTF Test ( Only ZULU )"
        '
        'GetDTP_Time
        '
        Me.GetDTP_Time.Controls.Add(Me.AccuracyValue)
        Me.GetDTP_Time.Controls.Add(Me.Label6)
        Me.GetDTP_Time.Controls.Add(Me.StartTest_DTF)
        Me.GetDTP_Time.Controls.Add(Me.DTP_Time)
        Me.GetDTP_Time.Controls.Add(Me.SpeedValue)
        Me.GetDTP_Time.Controls.Add(Me.Labeo0)
        Me.GetDTP_Time.Location = New System.Drawing.Point(20, 272)
        Me.GetDTP_Time.Name = "GetDTP_Time"
        Me.GetDTP_Time.Size = New System.Drawing.Size(556, 369)
        Me.GetDTP_Time.TabIndex = 1
        Me.GetDTP_Time.TabStop = False
        Me.GetDTP_Time.Text = "GetDTP_Time"
        '
        'AccuracyValue
        '
        Me.AccuracyValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AccuracyValue.FormattingEnabled = True
        Me.AccuracyValue.Items.AddRange(New Object() {"High", "Median"})
        Me.AccuracyValue.Location = New System.Drawing.Point(192, 62)
        Me.AccuracyValue.Name = "AccuracyValue"
        Me.AccuracyValue.Size = New System.Drawing.Size(204, 33)
        Me.AccuracyValue.TabIndex = 20
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(85, 136)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 25)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Speed:"
        '
        'StartTest_DTF
        '
        Me.StartTest_DTF.Location = New System.Drawing.Point(21, 209)
        Me.StartTest_DTF.Margin = New System.Windows.Forms.Padding(6)
        Me.StartTest_DTF.Name = "StartTest_DTF"
        Me.StartTest_DTF.Size = New System.Drawing.Size(163, 80)
        Me.StartTest_DTF.TabIndex = 18
        Me.StartTest_DTF.Text = "Start"
        Me.StartTest_DTF.UseVisualStyleBackColor = True
        '
        'DTP_Time
        '
        Me.DTP_Time.AutoSize = True
        Me.DTP_Time.Location = New System.Drawing.Point(249, 237)
        Me.DTP_Time.Name = "DTP_Time"
        Me.DTP_Time.Size = New System.Drawing.Size(77, 25)
        Me.DTP_Time.TabIndex = 17
        Me.DTP_Time.Text = "Label2"
        '
        'SpeedValue
        '
        Me.SpeedValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SpeedValue.FormattingEnabled = True
        Me.SpeedValue.Items.AddRange(New Object() {"Fast", "Middle Fast", "Median", "Slow"})
        Me.SpeedValue.Location = New System.Drawing.Point(192, 128)
        Me.SpeedValue.Name = "SpeedValue"
        Me.SpeedValue.Size = New System.Drawing.Size(204, 33)
        Me.SpeedValue.TabIndex = 1
        '
        'Labeo0
        '
        Me.Labeo0.AutoSize = True
        Me.Labeo0.Location = New System.Drawing.Point(56, 70)
        Me.Labeo0.Name = "Labeo0"
        Me.Labeo0.Size = New System.Drawing.Size(107, 25)
        Me.Labeo0.TabIndex = 0
        Me.Labeo0.Text = "Accuracy:"
        '
        'Calibration_DTF
        '
        Me.Calibration_DTF.Controls.Add(Me.Start_Calibration)
        Me.Calibration_DTF.Location = New System.Drawing.Point(20, 44)
        Me.Calibration_DTF.Name = "Calibration_DTF"
        Me.Calibration_DTF.Size = New System.Drawing.Size(556, 118)
        Me.Calibration_DTF.TabIndex = 0
        Me.Calibration_DTF.TabStop = False
        Me.Calibration_DTF.Text = "Calibration_DTF"
        '
        'Start_Calibration
        '
        Me.Start_Calibration.Location = New System.Drawing.Point(174, 37)
        Me.Start_Calibration.Name = "Start_Calibration"
        Me.Start_Calibration.Size = New System.Drawing.Size(202, 54)
        Me.Start_Calibration.TabIndex = 0
        Me.Start_Calibration.Text = "Start Calibration"
        Me.Start_Calibration.UseVisualStyleBackColor = True
        '
        'FormDebug
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1236, 754)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "FormDebug"
        Me.Opacity = 0.95R
        Me.Text = "Debug Test"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GetDTP_Time.ResumeLayout(False)
        Me.GetDTP_Time.PerformLayout()
        Me.Calibration_DTF.ResumeLayout(False)
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
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GetDTP_Time As GroupBox
    Friend WithEvents Calibration_DTF As GroupBox
    Friend WithEvents Start_Calibration As Button
    Friend WithEvents SpeedValue As ComboBox
    Friend WithEvents Labeo0 As Label
    Friend WithEvents StartTest_DTF As Button
    Friend WithEvents DTP_Time As Label
    Friend WithEvents AccuracyValue As ComboBox
    Friend WithEvents Label6 As Label
End Class
