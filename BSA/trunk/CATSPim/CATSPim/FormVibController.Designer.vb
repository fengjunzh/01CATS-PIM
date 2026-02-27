<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVibController
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
        Me.components = New System.ComponentModel.Container()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.gbCon = New System.Windows.Forms.GroupBox()
        Me.ckRelay8 = New System.Windows.Forms.CheckBox()
        Me.gbLeap = New System.Windows.Forms.GroupBox()
        Me.ckRelay7 = New System.Windows.Forms.CheckBox()
        Me.ckRelay6 = New System.Windows.Forms.CheckBox()
        Me.ckRelay5 = New System.Windows.Forms.CheckBox()
        Me.ckRelay4 = New System.Windows.Forms.CheckBox()
        Me.ckRelay3 = New System.Windows.Forms.CheckBox()
        Me.ckRelay2 = New System.Windows.Forms.CheckBox()
        Me.ckRelay1 = New System.Windows.Forms.CheckBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.cbVibDevice = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.vib8222COM = New System.Windows.Forms.ComboBox()
        Me.gbCon.SuspendLayout()
        Me.gbLeap.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(612, 550)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(150, 44)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        Me.btnStop.Enabled = False
        Me.btnStop.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.Location = New System.Drawing.Point(612, 473)
        Me.btnStop.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(150, 44)
        Me.btnStop.TabIndex = 8
        Me.btnStop.Text = "Vib Off"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'gbCon
        '
        Me.gbCon.Controls.Add(Me.ckRelay8)
        Me.gbCon.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCon.Location = New System.Drawing.Point(318, 121)
        Me.gbCon.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.gbCon.Name = "gbCon"
        Me.gbCon.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.gbCon.Size = New System.Drawing.Size(282, 475)
        Me.gbCon.TabIndex = 6
        Me.gbCon.TabStop = False
        Me.gbCon.Text = "Continuous"
        '
        'ckRelay8
        '
        Me.ckRelay8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay8.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay8.ImageIndex = 0
        Me.ckRelay8.Location = New System.Drawing.Point(36, 37)
        Me.ckRelay8.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay8.Name = "ckRelay8"
        Me.ckRelay8.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay8.TabIndex = 8
        Me.ckRelay8.Tag = "8"
        Me.ckRelay8.Text = "Relay 8"
        Me.ckRelay8.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay8.UseVisualStyleBackColor = True
        '
        'gbLeap
        '
        Me.gbLeap.Controls.Add(Me.ckRelay7)
        Me.gbLeap.Controls.Add(Me.ckRelay6)
        Me.gbLeap.Controls.Add(Me.ckRelay5)
        Me.gbLeap.Controls.Add(Me.ckRelay4)
        Me.gbLeap.Controls.Add(Me.ckRelay3)
        Me.gbLeap.Controls.Add(Me.ckRelay2)
        Me.gbLeap.Controls.Add(Me.ckRelay1)
        Me.gbLeap.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbLeap.Location = New System.Drawing.Point(24, 121)
        Me.gbLeap.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.gbLeap.Name = "gbLeap"
        Me.gbLeap.Padding = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.gbLeap.Size = New System.Drawing.Size(282, 475)
        Me.gbLeap.TabIndex = 5
        Me.gbLeap.TabStop = False
        Me.gbLeap.Text = "Leap"
        '
        'ckRelay7
        '
        Me.ckRelay7.Enabled = False
        Me.ckRelay7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay7.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay7.ImageIndex = 0
        Me.ckRelay7.Location = New System.Drawing.Point(38, 394)
        Me.ckRelay7.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay7.Name = "ckRelay7"
        Me.ckRelay7.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay7.TabIndex = 7
        Me.ckRelay7.Tag = "7"
        Me.ckRelay7.Text = "Relay 7"
        Me.ckRelay7.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay7.UseVisualStyleBackColor = True
        '
        'ckRelay6
        '
        Me.ckRelay6.Enabled = False
        Me.ckRelay6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay6.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay6.ImageIndex = 0
        Me.ckRelay6.Location = New System.Drawing.Point(38, 335)
        Me.ckRelay6.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay6.Name = "ckRelay6"
        Me.ckRelay6.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay6.TabIndex = 6
        Me.ckRelay6.Tag = "6"
        Me.ckRelay6.Text = "Relay 6"
        Me.ckRelay6.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay6.UseVisualStyleBackColor = True
        '
        'ckRelay5
        '
        Me.ckRelay5.Enabled = False
        Me.ckRelay5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay5.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay5.ImageIndex = 0
        Me.ckRelay5.Location = New System.Drawing.Point(38, 275)
        Me.ckRelay5.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay5.Name = "ckRelay5"
        Me.ckRelay5.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay5.TabIndex = 5
        Me.ckRelay5.Tag = "5"
        Me.ckRelay5.Text = "Relay 5"
        Me.ckRelay5.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay5.UseVisualStyleBackColor = True
        '
        'ckRelay4
        '
        Me.ckRelay4.Checked = True
        Me.ckRelay4.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckRelay4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay4.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay4.ImageIndex = 0
        Me.ckRelay4.Location = New System.Drawing.Point(38, 215)
        Me.ckRelay4.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay4.Name = "ckRelay4"
        Me.ckRelay4.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay4.TabIndex = 4
        Me.ckRelay4.Tag = "4"
        Me.ckRelay4.Text = "Relay 4"
        Me.ckRelay4.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay4.UseVisualStyleBackColor = True
        '
        'ckRelay3
        '
        Me.ckRelay3.Checked = True
        Me.ckRelay3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckRelay3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay3.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay3.ImageIndex = 0
        Me.ckRelay3.Location = New System.Drawing.Point(38, 156)
        Me.ckRelay3.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay3.Name = "ckRelay3"
        Me.ckRelay3.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay3.TabIndex = 3
        Me.ckRelay3.Tag = "3"
        Me.ckRelay3.Text = "Relay 3"
        Me.ckRelay3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay3.UseVisualStyleBackColor = True
        '
        'ckRelay2
        '
        Me.ckRelay2.Checked = True
        Me.ckRelay2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckRelay2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay2.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay2.ImageIndex = 0
        Me.ckRelay2.Location = New System.Drawing.Point(38, 96)
        Me.ckRelay2.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay2.Name = "ckRelay2"
        Me.ckRelay2.Size = New System.Drawing.Size(182, 48)
        Me.ckRelay2.TabIndex = 2
        Me.ckRelay2.Tag = "2"
        Me.ckRelay2.Text = "Relay 2"
        Me.ckRelay2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay2.UseVisualStyleBackColor = True
        '
        'ckRelay1
        '
        Me.ckRelay1.Checked = True
        Me.ckRelay1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ckRelay1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ckRelay1.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckRelay1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ckRelay1.ImageIndex = 0
        Me.ckRelay1.Location = New System.Drawing.Point(38, 37)
        Me.ckRelay1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ckRelay1.Name = "ckRelay1"
        Me.ckRelay1.Size = New System.Drawing.Size(180, 48)
        Me.ckRelay1.TabIndex = 1
        Me.ckRelay1.Tag = "1"
        Me.ckRelay1.Text = "Relay 1"
        Me.ckRelay1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.ckRelay1.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Enabled = False
        Me.btnStart.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStart.Location = New System.Drawing.Point(612, 396)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(150, 44)
        Me.btnStart.TabIndex = 7
        Me.btnStart.Text = "Vib On"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'cbVibDevice
        '
        Me.cbVibDevice.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVibDevice.FormattingEnabled = True
        Me.cbVibDevice.Location = New System.Drawing.Point(254, 31)
        Me.cbVibDevice.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.cbVibDevice.Name = "cbVibDevice"
        Me.cbVibDevice.Size = New System.Drawing.Size(342, 34)
        Me.cbVibDevice.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(200, 26)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Vibration Device :"
        '
        'btnOpen
        '
        Me.btnOpen.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpen.Location = New System.Drawing.Point(628, 27)
        Me.btnOpen.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(150, 44)
        Me.btnOpen.TabIndex = 12
        Me.btnOpen.Text = "Open"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(628, 83)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(150, 44)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'vib8222COM
        '
        Me.vib8222COM.FormattingEnabled = True
        Me.vib8222COM.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15"})
        Me.vib8222COM.Location = New System.Drawing.Point(433, 76)
        Me.vib8222COM.Margin = New System.Windows.Forms.Padding(6)
        Me.vib8222COM.Name = "vib8222COM"
        Me.vib8222COM.Size = New System.Drawing.Size(163, 33)
        Me.vib8222COM.TabIndex = 14
        Me.vib8222COM.Text = "COM1"
        '
        'FormVibController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 619)
        Me.Controls.Add(Me.vib8222COM)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbVibDevice)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.gbCon)
        Me.Controls.Add(Me.gbLeap)
        Me.Controls.Add(Me.btnStart)
        Me.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.Name = "FormVibController"
        Me.Text = "Vibration Controller"
        Me.gbCon.ResumeLayout(False)
        Me.gbLeap.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
	Friend WithEvents btnStop As Button
	Friend WithEvents gbCon As GroupBox
	Friend WithEvents ckRelay8 As CheckBox
	Friend WithEvents gbLeap As GroupBox
	Friend WithEvents ckRelay7 As CheckBox
	Friend WithEvents ckRelay6 As CheckBox
	Friend WithEvents ckRelay5 As CheckBox
	Friend WithEvents ckRelay4 As CheckBox
	Friend WithEvents ckRelay3 As CheckBox
	Friend WithEvents ckRelay2 As CheckBox
	Friend WithEvents ckRelay1 As CheckBox
	Friend WithEvents btnStart As Button
	Friend WithEvents cbVibDevice As ComboBox
	Friend WithEvents Label1 As Label
	Friend WithEvents btnOpen As Button
	Friend WithEvents btnClose As Button
	Friend WithEvents Timer1 As Timer
    Friend WithEvents vib8222COM As ComboBox
End Class
