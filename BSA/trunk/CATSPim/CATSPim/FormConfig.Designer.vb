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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbRetAddress = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvInstrs = New System.Windows.Forms.DataGridView()
        Me.Enable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FreqBand = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Model = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Vendor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BandIdx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXLLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxRLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RxLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CableSerNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DevSN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CheckChamberID = New System.Windows.Forms.CheckBox()
        Me.CheckDoor = New System.Windows.Forms.CheckBox()
        Me.vib8222COM = New System.Windows.Forms.ComboBox()
        Me.cbVibAddress = New System.Windows.Forms.ComboBox()
        Me.ckVibEnable = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ckbProcesscheck = New System.Windows.Forms.CheckBox()
        Me.CalibrateStart = New System.Windows.Forms.Button()
        Me.Panel_Cal = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PowerCal_SN = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Finally_F2 = New System.Windows.Forms.Label()
        Me.Finally_F1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Sel_Automatic = New System.Windows.Forms.RadioButton()
        Me.Sel_Manual = New System.Windows.Forms.RadioButton()
        Me.ReCal = New System.Windows.Forms.Button()
        Me.Offset_F2 = New System.Windows.Forms.Label()
        Me.Original_F2 = New System.Windows.Forms.Label()
        Me.Calibration_F2 = New System.Windows.Forms.Label()
        Me.Offset_F1 = New System.Windows.Forms.Label()
        Me.Original_F1 = New System.Windows.Forms.Label()
        Me.Calibration_F1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Start_Cal = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.F1_sel = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.F2_sel = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.AttenuationValue = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.USBDeviceList = New System.Windows.Forms.ListBox()
        Me.SettingDevice = New System.Windows.Forms.Button()
        Me.OpenUSBDev = New System.Windows.Forms.Button()
        Me.Device_Message = New System.Windows.Forms.Label()
        Me.ResetDec = New System.Windows.Forms.Button()
        Me.OutPowersettingvalue = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CloseDev = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PowCalFre = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.gg = New System.Windows.Forms.Label()
        Me.Cal_Done = New System.Windows.Forms.Button()
        Me.CheckVibration = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvInstrs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel_Cal.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbRetAddress)
        Me.GroupBox1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(333, 95)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RET Device"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Address:"
        '
        'cbRetAddress
        '
        Me.cbRetAddress.FormattingEnabled = True
        Me.cbRetAddress.Location = New System.Drawing.Point(16, 47)
        Me.cbRetAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.cbRetAddress.Name = "cbRetAddress"
        Me.cbRetAddress.Size = New System.Drawing.Size(299, 25)
        Me.cbRetAddress.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvInstrs)
        Me.GroupBox2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 117)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(1109, 636)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pim Equipments"
        '
        'dgvInstrs
        '
        Me.dgvInstrs.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInstrs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvInstrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInstrs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Enable, Me.FreqBand, Me.Model, Me.Vendor, Me.Address, Me.BandIdx, Me.TXLLoss, Me.TxRLoss, Me.RxLoss, Me.CableSerNum, Me.DevSN})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInstrs.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvInstrs.Location = New System.Drawing.Point(4, 21)
        Me.dgvInstrs.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvInstrs.Name = "dgvInstrs"
        Me.dgvInstrs.RowHeadersVisible = False
        Me.dgvInstrs.Size = New System.Drawing.Size(1103, 612)
        Me.dgvInstrs.TabIndex = 0
        '
        'Enable
        '
        Me.Enable.DataPropertyName = "Enable"
        Me.Enable.FalseValue = "False"
        Me.Enable.HeaderText = "Enable"
        Me.Enable.Name = "Enable"
        Me.Enable.TrueValue = "True"
        Me.Enable.Width = 45
        '
        'FreqBand
        '
        Me.FreqBand.DataPropertyName = "BandName"
        Me.FreqBand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FreqBand.HeaderText = "Freq Band"
        Me.FreqBand.Name = "FreqBand"
        Me.FreqBand.Width = 90
        '
        'Model
        '
        Me.Model.DataPropertyName = "Model"
        Me.Model.HeaderText = "Model"
        Me.Model.Name = "Model"
        Me.Model.Width = 80
        '
        'Vendor
        '
        Me.Vendor.DataPropertyName = "Vendor"
        Me.Vendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Vendor.HeaderText = "Vendor"
        Me.Vendor.Name = "Vendor"
        Me.Vendor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Vendor.Width = 110
        '
        'Address
        '
        Me.Address.DataPropertyName = "Address"
        Me.Address.HeaderText = "Address"
        Me.Address.Name = "Address"
        Me.Address.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Address.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'BandIdx
        '
        Me.BandIdx.DataPropertyName = "BandIdx"
        Me.BandIdx.HeaderText = "Band Idx"
        Me.BandIdx.Name = "BandIdx"
        Me.BandIdx.Width = 50
        '
        'TXLLoss
        '
        Me.TXLLoss.DataPropertyName = "Tx1Loss"
        Me.TXLLoss.HeaderText = "TxL Loss"
        Me.TXLLoss.Name = "TXLLoss"
        Me.TXLLoss.ReadOnly = True
        Me.TXLLoss.Width = 50
        '
        'TxRLoss
        '
        Me.TxRLoss.DataPropertyName = "Tx2Loss"
        Me.TxRLoss.HeaderText = "TxR Loss"
        Me.TxRLoss.Name = "TxRLoss"
        Me.TxRLoss.ReadOnly = True
        Me.TxRLoss.Width = 50
        '
        'RxLoss
        '
        Me.RxLoss.DataPropertyName = "RxLoss"
        Me.RxLoss.HeaderText = "Rx Loss"
        Me.RxLoss.Name = "RxLoss"
        Me.RxLoss.ReadOnly = True
        Me.RxLoss.Width = 50
        '
        'CableSerNum
        '
        Me.CableSerNum.DataPropertyName = "CableSerNum"
        Me.CableSerNum.HeaderText = "Cable SerialNum"
        Me.CableSerNum.Name = "CableSerNum"
        '
        'DevSN
        '
        Me.DevSN.DataPropertyName = "DevSN"
        Me.DevSN.HeaderText = "DevSerialNum"
        Me.DevSN.Name = "DevSN"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(975, 8)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(132, 31)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(975, 51)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(132, 31)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CheckVibration)
        Me.GroupBox3.Controls.Add(Me.CheckChamberID)
        Me.GroupBox3.Controls.Add(Me.cbVibAddress)
        Me.GroupBox3.Controls.Add(Me.vib8222COM)
        Me.GroupBox3.Controls.Add(Me.CheckDoor)
        Me.GroupBox3.Controls.Add(Me.ckVibEnable)
        Me.GroupBox3.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(349, 15)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(448, 95)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Vibration Device"
        '
        'CheckChamberID
        '
        Me.CheckChamberID.AutoSize = True
        Me.CheckChamberID.Location = New System.Drawing.Point(134, 65)
        Me.CheckChamberID.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckChamberID.Name = "CheckChamberID"
        Me.CheckChamberID.Size = New System.Drawing.Size(153, 21)
        Me.CheckChamberID.TabIndex = 9
        Me.CheckChamberID.Text = "CheckChamberID"
        Me.CheckChamberID.UseVisualStyleBackColor = True
        '
        'CheckDoor
        '
        Me.CheckDoor.AutoSize = True
        Me.CheckDoor.Location = New System.Drawing.Point(17, 65)
        Me.CheckDoor.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckDoor.Name = "CheckDoor"
        Me.CheckDoor.Size = New System.Drawing.Size(107, 21)
        Me.CheckDoor.TabIndex = 8
        Me.CheckDoor.Text = "CheckDoor"
        Me.CheckDoor.UseVisualStyleBackColor = True
        '
        'vib8222COM
        '
        Me.vib8222COM.FormattingEnabled = True
        Me.vib8222COM.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15"})
        Me.vib8222COM.Location = New System.Drawing.Point(192, 24)
        Me.vib8222COM.Margin = New System.Windows.Forms.Padding(4)
        Me.vib8222COM.Name = "vib8222COM"
        Me.vib8222COM.Size = New System.Drawing.Size(95, 25)
        Me.vib8222COM.TabIndex = 4
        '
        'cbVibAddress
        '
        Me.cbVibAddress.FormattingEnabled = True
        Me.cbVibAddress.Location = New System.Drawing.Point(17, 24)
        Me.cbVibAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.cbVibAddress.Name = "cbVibAddress"
        Me.cbVibAddress.Size = New System.Drawing.Size(148, 25)
        Me.cbVibAddress.TabIndex = 2
        '
        'ckVibEnable
        '
        Me.ckVibEnable.AutoSize = True
        Me.ckVibEnable.Location = New System.Drawing.Point(295, 27)
        Me.ckVibEnable.Margin = New System.Windows.Forms.Padding(4)
        Me.ckVibEnable.Name = "ckVibEnable"
        Me.ckVibEnable.Size = New System.Drawing.Size(137, 21)
        Me.ckVibEnable.TabIndex = 1
        Me.ckVibEnable.Text = "VibrationEnable"
        Me.ckVibEnable.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ckbProcesscheck)
        Me.GroupBox4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(808, 15)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(116, 94)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "MII "
        '
        'ckbProcesscheck
        '
        Me.ckbProcesscheck.AutoSize = True
        Me.ckbProcesscheck.Location = New System.Drawing.Point(19, 26)
        Me.ckbProcesscheck.Margin = New System.Windows.Forms.Padding(4)
        Me.ckbProcesscheck.Name = "ckbProcesscheck"
        Me.ckbProcesscheck.Size = New System.Drawing.Size(75, 21)
        Me.ckbProcesscheck.TabIndex = 7
        Me.ckbProcesscheck.Text = "Online"
        Me.ckbProcesscheck.UseVisualStyleBackColor = True
        '
        'CalibrateStart
        '
        Me.CalibrateStart.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalibrateStart.Location = New System.Drawing.Point(975, 92)
        Me.CalibrateStart.Margin = New System.Windows.Forms.Padding(4)
        Me.CalibrateStart.Name = "CalibrateStart"
        Me.CalibrateStart.Size = New System.Drawing.Size(132, 31)
        Me.CalibrateStart.TabIndex = 7
        Me.CalibrateStart.Text = "PowerCalibrate"
        Me.CalibrateStart.UseVisualStyleBackColor = True
        '
        'Panel_Cal
        '
        Me.Panel_Cal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Cal.Controls.Add(Me.Label12)
        Me.Panel_Cal.Controls.Add(Me.Label6)
        Me.Panel_Cal.Controls.Add(Me.PowerCal_SN)
        Me.Panel_Cal.Controls.Add(Me.Label18)
        Me.Panel_Cal.Controls.Add(Me.GroupBox6)
        Me.Panel_Cal.Controls.Add(Me.GroupBox5)
        Me.Panel_Cal.Controls.Add(Me.Cal_Done)
        Me.Panel_Cal.Location = New System.Drawing.Point(1123, 15)
        Me.Panel_Cal.Name = "Panel_Cal"
        Me.Panel_Cal.Size = New System.Drawing.Size(631, 735)
        Me.Panel_Cal.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(537, 84)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(82, 17)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "ex. Cal0300"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(384, 108)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 17)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "CalibrationSN:"
        '
        'PowerCal_SN
        '
        Me.PowerCal_SN.FormattingEnabled = True
        Me.PowerCal_SN.Items.AddRange(New Object() {"", "Cal0300"})
        Me.PowerCal_SN.Location = New System.Drawing.Point(491, 104)
        Me.PowerCal_SN.Margin = New System.Windows.Forms.Padding(4)
        Me.PowerCal_SN.Name = "PowerCal_SN"
        Me.PowerCal_SN.Size = New System.Drawing.Size(132, 24)
        Me.PowerCal_SN.TabIndex = 29
        Me.PowerCal_SN.Text = "Cal0300"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 22.125!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(141, 31)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(329, 42)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "Power Calibration"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Finally_F2)
        Me.GroupBox6.Controls.Add(Me.Finally_F1)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Controls.Add(Me.Panel1)
        Me.GroupBox6.Controls.Add(Me.ReCal)
        Me.GroupBox6.Controls.Add(Me.Offset_F2)
        Me.GroupBox6.Controls.Add(Me.Original_F2)
        Me.GroupBox6.Controls.Add(Me.Calibration_F2)
        Me.GroupBox6.Controls.Add(Me.Offset_F1)
        Me.GroupBox6.Controls.Add(Me.Original_F1)
        Me.GroupBox6.Controls.Add(Me.Calibration_F1)
        Me.GroupBox6.Controls.Add(Me.Label5)
        Me.GroupBox6.Controls.Add(Me.Start_Cal)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.F1_sel)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.F2_sel)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 368)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(613, 260)
        Me.GroupBox6.TabIndex = 16
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Calibration"
        '
        'Finally_F2
        '
        Me.Finally_F2.AutoSize = True
        Me.Finally_F2.Location = New System.Drawing.Point(271, 202)
        Me.Finally_F2.Name = "Finally_F2"
        Me.Finally_F2.Size = New System.Drawing.Size(16, 17)
        Me.Finally_F2.TabIndex = 39
        Me.Finally_F2.Text = "0"
        '
        'Finally_F1
        '
        Me.Finally_F1.AutoSize = True
        Me.Finally_F1.Location = New System.Drawing.Point(160, 203)
        Me.Finally_F1.Name = "Finally_F1"
        Me.Finally_F1.Size = New System.Drawing.Size(16, 17)
        Me.Finally_F1.TabIndex = 38
        Me.Finally_F1.Text = "0"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(13, 203)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(117, 17)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Finally test value:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Sel_Automatic)
        Me.Panel1.Controls.Add(Me.Sel_Manual)
        Me.Panel1.Location = New System.Drawing.Point(399, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(216, 31)
        Me.Panel1.TabIndex = 35
        '
        'Sel_Automatic
        '
        Me.Sel_Automatic.AutoSize = True
        Me.Sel_Automatic.Checked = True
        Me.Sel_Automatic.Location = New System.Drawing.Point(19, 3)
        Me.Sel_Automatic.Name = "Sel_Automatic"
        Me.Sel_Automatic.Size = New System.Drawing.Size(91, 21)
        Me.Sel_Automatic.TabIndex = 33
        Me.Sel_Automatic.TabStop = True
        Me.Sel_Automatic.Text = "Automatic"
        Me.Sel_Automatic.UseVisualStyleBackColor = True
        '
        'Sel_Manual
        '
        Me.Sel_Manual.AutoSize = True
        Me.Sel_Manual.Location = New System.Drawing.Point(129, 3)
        Me.Sel_Manual.Name = "Sel_Manual"
        Me.Sel_Manual.Size = New System.Drawing.Size(75, 21)
        Me.Sel_Manual.TabIndex = 34
        Me.Sel_Manual.Text = "Manual"
        Me.Sel_Manual.UseVisualStyleBackColor = True
        '
        'ReCal
        '
        Me.ReCal.Location = New System.Drawing.Point(536, 223)
        Me.ReCal.Name = "ReCal"
        Me.ReCal.Size = New System.Drawing.Size(73, 32)
        Me.ReCal.TabIndex = 32
        Me.ReCal.Text = "ReCal"
        Me.ReCal.UseVisualStyleBackColor = True
        '
        'Offset_F2
        '
        Me.Offset_F2.AutoSize = True
        Me.Offset_F2.Location = New System.Drawing.Point(269, 122)
        Me.Offset_F2.Name = "Offset_F2"
        Me.Offset_F2.Size = New System.Drawing.Size(16, 17)
        Me.Offset_F2.TabIndex = 31
        Me.Offset_F2.Text = "0"
        '
        'Original_F2
        '
        Me.Original_F2.AutoSize = True
        Me.Original_F2.Location = New System.Drawing.Point(269, 83)
        Me.Original_F2.Name = "Original_F2"
        Me.Original_F2.Size = New System.Drawing.Size(16, 17)
        Me.Original_F2.TabIndex = 29
        Me.Original_F2.Text = "0"
        '
        'Calibration_F2
        '
        Me.Calibration_F2.AutoSize = True
        Me.Calibration_F2.Location = New System.Drawing.Point(271, 164)
        Me.Calibration_F2.Name = "Calibration_F2"
        Me.Calibration_F2.Size = New System.Drawing.Size(16, 17)
        Me.Calibration_F2.TabIndex = 30
        Me.Calibration_F2.Text = "0"
        '
        'Offset_F1
        '
        Me.Offset_F1.AutoSize = True
        Me.Offset_F1.Location = New System.Drawing.Point(159, 124)
        Me.Offset_F1.Name = "Offset_F1"
        Me.Offset_F1.Size = New System.Drawing.Size(16, 17)
        Me.Offset_F1.TabIndex = 28
        Me.Offset_F1.Text = "0"
        '
        'Original_F1
        '
        Me.Original_F1.AutoSize = True
        Me.Original_F1.Location = New System.Drawing.Point(159, 84)
        Me.Original_F1.Name = "Original_F1"
        Me.Original_F1.Size = New System.Drawing.Size(16, 17)
        Me.Original_F1.TabIndex = 26
        Me.Original_F1.Text = "0"
        '
        'Calibration_F1
        '
        Me.Calibration_F1.AutoSize = True
        Me.Calibration_F1.Location = New System.Drawing.Point(160, 165)
        Me.Calibration_F1.Name = "Calibration_F1"
        Me.Calibration_F1.Size = New System.Drawing.Size(16, 17)
        Me.Calibration_F1.TabIndex = 27
        Me.Calibration_F1.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(81, 181)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 20)
        Me.Label5.TabIndex = 25
        '
        'Start_Cal
        '
        Me.Start_Cal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Start_Cal.Location = New System.Drawing.Point(379, 104)
        Me.Start_Cal.Name = "Start_Cal"
        Me.Start_Cal.Size = New System.Drawing.Size(195, 75)
        Me.Start_Cal.TabIndex = 6
        Me.Start_Cal.Text = "Start"
        Me.Start_Cal.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(44, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 17)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Offset value:"
        '
        'F1_sel
        '
        Me.F1_sel.AutoSize = True
        Me.F1_sel.Checked = True
        Me.F1_sel.Location = New System.Drawing.Point(152, 40)
        Me.F1_sel.Name = "F1_sel"
        Me.F1_sel.Size = New System.Drawing.Size(81, 21)
        Me.F1_sel.TabIndex = 20
        Me.F1_sel.TabStop = True
        Me.F1_sel.Text = "F1 /dBm"
        Me.F1_sel.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 84)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 17)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Original test value:"
        '
        'F2_sel
        '
        Me.F2_sel.AutoSize = True
        Me.F2_sel.Location = New System.Drawing.Point(264, 40)
        Me.F2_sel.Name = "F2_sel"
        Me.F2_sel.Size = New System.Drawing.Size(81, 21)
        Me.F2_sel.TabIndex = 21
        Me.F2_sel.Text = "F2 /dBm"
        Me.F2_sel.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 17)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Calibration value:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.AttenuationValue)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.USBDeviceList)
        Me.GroupBox5.Controls.Add(Me.SettingDevice)
        Me.GroupBox5.Controls.Add(Me.OpenUSBDev)
        Me.GroupBox5.Controls.Add(Me.Device_Message)
        Me.GroupBox5.Controls.Add(Me.ResetDec)
        Me.GroupBox5.Controls.Add(Me.OutPowersettingvalue)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.CloseDev)
        Me.GroupBox5.Controls.Add(Me.Label4)
        Me.GroupBox5.Controls.Add(Me.PowCalFre)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.gg)
        Me.GroupBox5.Location = New System.Drawing.Point(9, 120)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(613, 236)
        Me.GroupBox5.TabIndex = 15
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Device setting"
        '
        'AttenuationValue
        '
        Me.AttenuationValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AttenuationValue.FormattingEnabled = True
        Me.AttenuationValue.Items.AddRange(New Object() {"0", "20", "30", "40", "50"})
        Me.AttenuationValue.Location = New System.Drawing.Point(329, 42)
        Me.AttenuationValue.Margin = New System.Windows.Forms.Padding(4)
        Me.AttenuationValue.Name = "AttenuationValue"
        Me.AttenuationValue.Size = New System.Drawing.Size(68, 24)
        Me.AttenuationValue.TabIndex = 32
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(11, 49)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(141, 17)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "CalibrationFreqBand:"
        '
        'USBDeviceList
        '
        Me.USBDeviceList.FormattingEnabled = True
        Me.USBDeviceList.ItemHeight = 16
        Me.USBDeviceList.Location = New System.Drawing.Point(13, 97)
        Me.USBDeviceList.Name = "USBDeviceList"
        Me.USBDeviceList.Size = New System.Drawing.Size(347, 132)
        Me.USBDeviceList.TabIndex = 1
        '
        'SettingDevice
        '
        Me.SettingDevice.Location = New System.Drawing.Point(379, 171)
        Me.SettingDevice.Name = "SettingDevice"
        Me.SettingDevice.Size = New System.Drawing.Size(96, 40)
        Me.SettingDevice.TabIndex = 2
        Me.SettingDevice.Text = "Zero/Cal"
        Me.SettingDevice.UseVisualStyleBackColor = True
        '
        'OpenUSBDev
        '
        Me.OpenUSBDev.Location = New System.Drawing.Point(379, 106)
        Me.OpenUSBDev.Name = "OpenUSBDev"
        Me.OpenUSBDev.Size = New System.Drawing.Size(96, 40)
        Me.OpenUSBDev.TabIndex = 3
        Me.OpenUSBDev.Text = "Open"
        Me.OpenUSBDev.UseVisualStyleBackColor = True
        '
        'Device_Message
        '
        Me.Device_Message.AutoSize = True
        Me.Device_Message.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Device_Message.Location = New System.Drawing.Point(436, 51)
        Me.Device_Message.Name = "Device_Message"
        Me.Device_Message.Size = New System.Drawing.Size(0, 20)
        Me.Device_Message.TabIndex = 26
        '
        'ResetDec
        '
        Me.ResetDec.Location = New System.Drawing.Point(524, 171)
        Me.ResetDec.Name = "ResetDec"
        Me.ResetDec.Size = New System.Drawing.Size(73, 37)
        Me.ResetDec.TabIndex = 4
        Me.ResetDec.Text = "Reset"
        Me.ResetDec.UseVisualStyleBackColor = True
        '
        'OutPowersettingvalue
        '
        Me.OutPowersettingvalue.Location = New System.Drawing.Point(516, 42)
        Me.OutPowersettingvalue.Name = "OutPowersettingvalue"
        Me.OutPowersettingvalue.Size = New System.Drawing.Size(48, 22)
        Me.OutPowersettingvalue.TabIndex = 9
        Me.OutPowersettingvalue.Text = "43"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(248, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 17)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Attenuation:"
        '
        'CloseDev
        '
        Me.CloseDev.Location = New System.Drawing.Point(524, 106)
        Me.CloseDev.Name = "CloseDev"
        Me.CloseDev.Size = New System.Drawing.Size(73, 40)
        Me.CloseDev.TabIndex = 5
        Me.CloseDev.Text = "Close"
        Me.CloseDev.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(401, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "dB"
        '
        'PowCalFre
        '
        Me.PowCalFre.Location = New System.Drawing.Point(157, 44)
        Me.PowCalFre.Name = "PowCalFre"
        Me.PowCalFre.Size = New System.Drawing.Size(69, 22)
        Me.PowCalFre.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(561, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "dBm"
        '
        'gg
        '
        Me.gg.AutoSize = True
        Me.gg.Location = New System.Drawing.Point(444, 47)
        Me.gg.Name = "gg"
        Me.gg.Size = New System.Drawing.Size(74, 17)
        Me.gg.TabIndex = 10
        Me.gg.Text = "OutPower:"
        '
        'Cal_Done
        '
        Me.Cal_Done.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cal_Done.Location = New System.Drawing.Point(272, 664)
        Me.Cal_Done.Name = "Cal_Done"
        Me.Cal_Done.Size = New System.Drawing.Size(133, 43)
        Me.Cal_Done.TabIndex = 24
        Me.Cal_Done.Text = "Done"
        Me.Cal_Done.UseVisualStyleBackColor = True
        '
        'CheckVibration
        '
        Me.CheckVibration.AutoSize = True
        Me.CheckVibration.Location = New System.Drawing.Point(295, 65)
        Me.CheckVibration.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckVibration.Name = "CheckVibration"
        Me.CheckVibration.Size = New System.Drawing.Size(136, 21)
        Me.CheckVibration.TabIndex = 10
        Me.CheckVibration.Text = "CheckVibration"
        Me.CheckVibration.UseVisualStyleBackColor = True
        '
        'FormConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1789, 783)
        Me.Controls.Add(Me.Panel_Cal)
        Me.Controls.Add(Me.CalibrateStart)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FormConfig"
        Me.Text = "CATS Device Setup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvInstrs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel_Cal.ResumeLayout(False)
        Me.Panel_Cal.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbRetAddress As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents dgvInstrs As DataGridView
    Friend WithEvents btnSave As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents cbVibAddress As ComboBox
    Friend WithEvents ckVibEnable As CheckBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents ckbProcesscheck As CheckBox
    Friend WithEvents CalibrateStart As Button
    Friend WithEvents Panel_Cal As Panel
    Friend WithEvents USBDeviceList As ListBox
    Friend WithEvents Label9 As Label
    Friend WithEvents SettingDevice As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents OpenUSBDev As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents ResetDec As Button
    Friend WithEvents CloseDev As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents PowCalFre As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents OutPowersettingvalue As TextBox
    Friend WithEvents gg As Label
    Friend WithEvents F2_sel As RadioButton
    Friend WithEvents F1_sel As RadioButton
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Start_Cal As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Cal_Done As Button
    Friend WithEvents Device_Message As Label
    Friend WithEvents Offset_F2 As Label
    Friend WithEvents Original_F2 As Label
    Friend WithEvents Calibration_F2 As Label
    Friend WithEvents Offset_F1 As Label
    Friend WithEvents Original_F1 As Label
    Friend WithEvents Calibration_F1 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents CheckDoor As CheckBox
    Friend WithEvents ReCal As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Sel_Automatic As RadioButton
    Friend WithEvents Sel_Manual As RadioButton
    Friend WithEvents Finally_F2 As Label
    Friend WithEvents Finally_F1 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents vib8222COM As ComboBox
    Friend WithEvents PowerCal_SN As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Enable As DataGridViewCheckBoxColumn
    Friend WithEvents FreqBand As DataGridViewComboBoxColumn
    Friend WithEvents Model As DataGridViewTextBoxColumn
    Friend WithEvents Vendor As DataGridViewComboBoxColumn
    Friend WithEvents Address As DataGridViewTextBoxColumn
    Friend WithEvents BandIdx As DataGridViewTextBoxColumn
    Friend WithEvents TXLLoss As DataGridViewTextBoxColumn
    Friend WithEvents TxRLoss As DataGridViewTextBoxColumn
    Friend WithEvents RxLoss As DataGridViewTextBoxColumn
    Friend WithEvents CableSerNum As DataGridViewTextBoxColumn
    Friend WithEvents DevSN As DataGridViewTextBoxColumn
    Friend WithEvents CheckChamberID As CheckBox
    Friend WithEvents AttenuationValue As ComboBox
    Friend WithEvents CheckVibration As CheckBox
End Class
