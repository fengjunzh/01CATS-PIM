<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormRetController
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
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.dgvRET = New System.Windows.Forms.DataGridView()
		Me.dgvcRetSN = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcAntennaSN = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcModel = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcType = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcMinTilt = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcMaxTilt = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.dgvcCurrTilt = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.btnOpen = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.cmbPortName = New System.Windows.Forms.ComboBox()
		Me.btnClose = New System.Windows.Forms.Button()
		Me.btnScan = New System.Windows.Forms.Button()
		Me.btnSetTilt = New System.Windows.Forms.Button()
		Me.btnExit = New System.Windows.Forms.Button()
		CType(Me.dgvRET, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'dgvRET
		'
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
		DataGridViewCellStyle1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
		DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
		Me.dgvRET.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
		Me.dgvRET.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dgvRET.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvcRetSN, Me.dgvcAntennaSN, Me.dgvcModel, Me.dgvcType, Me.dgvcMinTilt, Me.dgvcMaxTilt, Me.dgvcCurrTilt})
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
		DataGridViewCellStyle2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
		DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
		DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
		DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
		Me.dgvRET.DefaultCellStyle = DataGridViewCellStyle2
		Me.dgvRET.Location = New System.Drawing.Point(12, 49)
		Me.dgvRET.Name = "dgvRET"
		Me.dgvRET.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.dgvRET.Size = New System.Drawing.Size(752, 356)
		Me.dgvRET.TabIndex = 0
		'
		'dgvcRetSN
		'
		Me.dgvcRetSN.HeaderText = "RET SN"
		Me.dgvcRetSN.Name = "dgvcRetSN"
		Me.dgvcRetSN.Width = 150
		'
		'dgvcAntennaSN
		'
		Me.dgvcAntennaSN.HeaderText = "Antenna SN"
		Me.dgvcAntennaSN.Name = "dgvcAntennaSN"
		Me.dgvcAntennaSN.ReadOnly = True
		Me.dgvcAntennaSN.Width = 150
		'
		'dgvcModel
		'
		Me.dgvcModel.HeaderText = "Model"
		Me.dgvcModel.Name = "dgvcModel"
		Me.dgvcModel.ReadOnly = True
		Me.dgvcModel.Width = 90
		'
		'dgvcType
		'
		Me.dgvcType.HeaderText = "Type"
		Me.dgvcType.Name = "dgvcType"
		Me.dgvcType.ReadOnly = True
		Me.dgvcType.Width = 60
		'
		'dgvcMinTilt
		'
		Me.dgvcMinTilt.HeaderText = "Min Tilt"
		Me.dgvcMinTilt.Name = "dgvcMinTilt"
		Me.dgvcMinTilt.ReadOnly = True
		Me.dgvcMinTilt.Width = 70
		'
		'dgvcMaxTilt
		'
		Me.dgvcMaxTilt.HeaderText = "Max Tilt"
		Me.dgvcMaxTilt.Name = "dgvcMaxTilt"
		Me.dgvcMaxTilt.ReadOnly = True
		Me.dgvcMaxTilt.Width = 75
		'
		'dgvcCurrTilt
		'
		Me.dgvcCurrTilt.HeaderText = "Tilt"
		Me.dgvcCurrTilt.Name = "dgvcCurrTilt"
		Me.dgvcCurrTilt.ReadOnly = True
		Me.dgvcCurrTilt.Width = 70
		'
		'btnOpen
		'
		Me.btnOpen.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnOpen.Location = New System.Drawing.Point(275, 20)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(89, 23)
		Me.btnOpen.TabIndex = 2
		Me.btnOpen.Text = "Open Port"
		Me.btnOpen.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(9, 23)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(66, 15)
		Me.Label1.TabIndex = 3
		Me.Label1.Text = "Port Name:"
		'
		'cmbPortName
		'
		Me.cmbPortName.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmbPortName.FormattingEnabled = True
		Me.cmbPortName.Location = New System.Drawing.Point(83, 19)
		Me.cmbPortName.Name = "cmbPortName"
		Me.cmbPortName.Size = New System.Drawing.Size(177, 23)
		Me.cmbPortName.TabIndex = 4
		'
		'btnClose
		'
		Me.btnClose.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnClose.Location = New System.Drawing.Point(375, 20)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(89, 23)
		Me.btnClose.TabIndex = 5
		Me.btnClose.Text = "Close Port"
		Me.btnClose.UseVisualStyleBackColor = True
		'
		'btnScan
		'
		Me.btnScan.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnScan.Location = New System.Drawing.Point(475, 20)
		Me.btnScan.Name = "btnScan"
		Me.btnScan.Size = New System.Drawing.Size(89, 23)
		Me.btnScan.TabIndex = 6
		Me.btnScan.Text = "Scan RET"
		Me.btnScan.UseVisualStyleBackColor = True
		'
		'btnSetTilt
		'
		Me.btnSetTilt.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnSetTilt.Location = New System.Drawing.Point(575, 20)
		Me.btnSetTilt.Name = "btnSetTilt"
		Me.btnSetTilt.Size = New System.Drawing.Size(89, 23)
		Me.btnSetTilt.TabIndex = 7
		Me.btnSetTilt.Text = "Set Tilt"
		Me.btnSetTilt.UseVisualStyleBackColor = True
		'
		'btnExit
		'
		Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnExit.Location = New System.Drawing.Point(675, 20)
		Me.btnExit.Name = "btnExit"
		Me.btnExit.Size = New System.Drawing.Size(89, 23)
		Me.btnExit.TabIndex = 8
		Me.btnExit.Text = "Exit"
		Me.btnExit.UseVisualStyleBackColor = True
		'
		'FormRetController
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(776, 419)
		Me.Controls.Add(Me.btnExit)
		Me.Controls.Add(Me.btnSetTilt)
		Me.Controls.Add(Me.btnScan)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.cmbPortName)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.btnOpen)
		Me.Controls.Add(Me.dgvRET)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.Name = "FormRetController"
		Me.Text = "RET Controller"
		CType(Me.dgvRET, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents dgvRET As DataGridView
	Friend WithEvents btnOpen As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents cmbPortName As ComboBox
	Friend WithEvents btnClose As Button
	Friend WithEvents btnScan As Button
	Friend WithEvents btnSetTilt As Button
	Friend WithEvents btnExit As Button
	Friend WithEvents dgvcRetSN As DataGridViewTextBoxColumn
	Friend WithEvents dgvcAntennaSN As DataGridViewTextBoxColumn
	Friend WithEvents dgvcModel As DataGridViewTextBoxColumn
	Friend WithEvents dgvcType As DataGridViewTextBoxColumn
	Friend WithEvents dgvcMinTilt As DataGridViewTextBoxColumn
	Friend WithEvents dgvcMaxTilt As DataGridViewTextBoxColumn
	Friend WithEvents dgvcCurrTilt As DataGridViewTextBoxColumn
End Class
