<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormConfig
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormConfig))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.fgPimDevices = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.btnSaveExit = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbVibAddress = New System.Windows.Forms.ComboBox()
        Me.ckVibEnable = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ckMiiOnLine = New System.Windows.Forms.CheckBox()
        Me.gbSnReader = New System.Windows.Forms.GroupBox()
        Me.ckSnReader = New System.Windows.Forms.CheckBox()
        Me.btnPowerCal = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ckPretest = New System.Windows.Forms.CheckBox()
        Me.ckAuto = New System.Windows.Forms.CheckBox()
        Me.GroupBox2.SuspendLayout()
        CType(Me.fgPimDevices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gbSnReader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.fgPimDevices)
        Me.GroupBox2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 79)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1107, 415)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "PIM Equipments"
        '
        'fgPimDevices
        '
        Me.fgPimDevices.AllowAddNew = True
        Me.fgPimDevices.AllowDelete = True
        Me.fgPimDevices.ColumnInfo = "1,1,0,0,0,100,Columns:"
        Me.fgPimDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fgPimDevices.ExtendLastCol = True
        Me.fgPimDevices.Location = New System.Drawing.Point(3, 17)
        Me.fgPimDevices.Name = "fgPimDevices"
        Me.fgPimDevices.Rows.Count = 1
        Me.fgPimDevices.Rows.DefaultSize = 20
        Me.fgPimDevices.Size = New System.Drawing.Size(1101, 395)
        Me.fgPimDevices.StyleInfo = resources.GetString("fgPimDevices.StyleInfo")
        Me.fgPimDevices.TabIndex = 0
        '
        'btnSaveExit
        '
        Me.btnSaveExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveExit.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveExit.Location = New System.Drawing.Point(1008, 12)
        Me.btnSaveExit.Name = "btnSaveExit"
        Me.btnSaveExit.Size = New System.Drawing.Size(108, 30)
        Me.btnSaveExit.TabIndex = 3
        Me.btnSaveExit.Text = "Save and Exit"
        Me.btnSaveExit.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cbVibAddress)
        Me.GroupBox3.Controls.Add(Me.ckVibEnable)
        Me.GroupBox3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(245, 71)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Vibration Device"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Jsb Board:"
        '
        'cbVibAddress
        '
        Me.cbVibAddress.FormattingEnabled = True
        Me.cbVibAddress.Location = New System.Drawing.Point(10, 38)
        Me.cbVibAddress.Name = "cbVibAddress"
        Me.cbVibAddress.Size = New System.Drawing.Size(222, 23)
        Me.cbVibAddress.TabIndex = 2
        '
        'ckVibEnable
        '
        Me.ckVibEnable.AutoSize = True
        Me.ckVibEnable.Enabled = False
        Me.ckVibEnable.Location = New System.Drawing.Point(170, 21)
        Me.ckVibEnable.Name = "ckVibEnable"
        Me.ckVibEnable.Size = New System.Drawing.Size(63, 19)
        Me.ckVibEnable.TabIndex = 1
        Me.ckVibEnable.Text = "Enable"
        Me.ckVibEnable.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ckMiiOnLine)
        Me.GroupBox4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(424, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(81, 70)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "MII "
        '
        'ckMiiOnLine
        '
        Me.ckMiiOnLine.AutoSize = True
        Me.ckMiiOnLine.Location = New System.Drawing.Point(6, 21)
        Me.ckMiiOnLine.Name = "ckMiiOnLine"
        Me.ckMiiOnLine.Size = New System.Drawing.Size(62, 19)
        Me.ckMiiOnLine.TabIndex = 7
        Me.ckMiiOnLine.Text = "Online"
        Me.ckMiiOnLine.UseVisualStyleBackColor = True
        '
        'gbSnReader
        '
        Me.gbSnReader.Controls.Add(Me.ckSnReader)
        Me.gbSnReader.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSnReader.Location = New System.Drawing.Point(304, 7)
        Me.gbSnReader.Name = "gbSnReader"
        Me.gbSnReader.Size = New System.Drawing.Size(81, 70)
        Me.gbSnReader.TabIndex = 7
        Me.gbSnReader.TabStop = False
        Me.gbSnReader.Text = "SX-100W"
        '
        'ckSnReader
        '
        Me.ckSnReader.AutoSize = True
        Me.ckSnReader.Location = New System.Drawing.Point(6, 21)
        Me.ckSnReader.Name = "ckSnReader"
        Me.ckSnReader.Size = New System.Drawing.Size(63, 19)
        Me.ckSnReader.TabIndex = 7
        Me.ckSnReader.Text = "Enable"
        Me.ckSnReader.UseVisualStyleBackColor = True
        '
        'btnPowerCal
        '
        Me.btnPowerCal.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPowerCal.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnPowerCal.Location = New System.Drawing.Point(666, 8)
        Me.btnPowerCal.Name = "btnPowerCal"
        Me.btnPowerCal.Size = New System.Drawing.Size(81, 70)
        Me.btnPowerCal.TabIndex = 8
        Me.btnPowerCal.Text = "Power Cal"
        Me.btnPowerCal.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ckPretest)
        Me.GroupBox1.Controls.Add(Me.ckAuto)
        Me.GroupBox1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(539, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(93, 70)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Automation"
        '
        'ckPretest
        '
        Me.ckPretest.AutoSize = True
        Me.ckPretest.Location = New System.Drawing.Point(6, 42)
        Me.ckPretest.Name = "ckPretest"
        Me.ckPretest.Size = New System.Drawing.Size(62, 19)
        Me.ckPretest.TabIndex = 7
        Me.ckPretest.Text = "Pretest"
        Me.ckPretest.UseVisualStyleBackColor = True
        '
        'ckAuto
        '
        Me.ckAuto.AutoSize = True
        Me.ckAuto.Location = New System.Drawing.Point(6, 21)
        Me.ckAuto.Name = "ckAuto"
        Me.ckAuto.Size = New System.Drawing.Size(63, 19)
        Me.ckAuto.TabIndex = 7
        Me.ckAuto.Text = "Enable"
        Me.ckAuto.UseVisualStyleBackColor = True
        '
        'FormConfig
        '
        Me.AcceptButton = Me.btnSaveExit
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1128, 508)
        Me.Controls.Add(Me.btnPowerCal)
        Me.Controls.Add(Me.gbSnReader)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnSaveExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConfig"
        Me.Text = "CATS Device Setup"
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.fgPimDevices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gbSnReader.ResumeLayout(False)
        Me.gbSnReader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnSaveExit As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbVibAddress As ComboBox
    Friend WithEvents ckVibEnable As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ckMiiOnLine As CheckBox
    Friend WithEvents fgPimDevices As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents gbSnReader As GroupBox
    Friend WithEvents ckSnReader As CheckBox
    Friend WithEvents btnPowerCal As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ckAuto As CheckBox
    Friend WithEvents ckPretest As CheckBox
End Class
