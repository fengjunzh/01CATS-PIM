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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbRetAddress = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.dgvInstrs = New System.Windows.Forms.DataGridView()
        Me.Enable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.FreqBand = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Model = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Vendor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BandIdx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TXLLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxRLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RxLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CableSerNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbVibAddress = New System.Windows.Forms.ComboBox()
        Me.ckVibEnable = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.dgvInstrs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cbRetAddress)
        Me.GroupBox1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(292, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(250, 71)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "RET Device"
        Me.GroupBox1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Address:"
        '
        'cbRetAddress
        '
        Me.cbRetAddress.FormattingEnabled = True
        Me.cbRetAddress.Location = New System.Drawing.Point(12, 38)
        Me.cbRetAddress.Name = "cbRetAddress"
        Me.cbRetAddress.Size = New System.Drawing.Size(225, 23)
        Me.cbRetAddress.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgvInstrs)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(761, 492)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pim Equipments"
        '
        'dgvInstrs
        '
        Me.dgvInstrs.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInstrs.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvInstrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInstrs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Enable, Me.FreqBand, Me.Model, Me.Vendor, Me.Address, Me.BandIdx, Me.TXLLoss, Me.TxRLoss, Me.RxLoss, Me.CableSerNum})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Blue
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvInstrs.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvInstrs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInstrs.Location = New System.Drawing.Point(3, 17)
        Me.dgvInstrs.Name = "dgvInstrs"
        Me.dgvInstrs.RowHeadersVisible = False
        Me.dgvInstrs.Size = New System.Drawing.Size(755, 472)
        Me.dgvInstrs.TabIndex = 0
        '
        'Enable
        '
        Me.Enable.DataPropertyName = "Enable"
        Me.Enable.FalseValue = "False"
        Me.Enable.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Enable.HeaderText = "Enable"
        Me.Enable.Name = "Enable"
        Me.Enable.TrueValue = "True"
        Me.Enable.Width = 45
        '
        'FreqBand
        '
        Me.FreqBand.DataPropertyName = "BandName"
        Me.FreqBand.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.FreqBand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.FreqBand.HeaderText = "Freq Band"
        Me.FreqBand.Name = "FreqBand"
        Me.FreqBand.Width = 90
        '
        'Model
        '
        Me.Model.DataPropertyName = "Model"
        Me.Model.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
        Me.Model.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Model.HeaderText = "Model"
        Me.Model.Name = "Model"
        Me.Model.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Model.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Model.Width = 80
        '
        'Vendor
        '
        Me.Vendor.DataPropertyName = "Vendor"
        Me.Vendor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox
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
        Me.TXLLoss.Width = 50
        '
        'TxRLoss
        '
        Me.TxRLoss.DataPropertyName = "Tx2Loss"
        Me.TxRLoss.HeaderText = "TxR Loss"
        Me.TxRLoss.Name = "TxRLoss"
        Me.TxRLoss.Width = 50
        '
        'RxLoss
        '
        Me.RxLoss.DataPropertyName = "RxLoss"
        Me.RxLoss.HeaderText = "Rx Loss"
        Me.RxLoss.Name = "RxLoss"
        Me.RxLoss.Width = 50
        '
        'CableSerNum
        '
        Me.CableSerNum.DataPropertyName = "CableSerNum"
        Me.CableSerNum.HeaderText = "Cable SerialNum"
        Me.CableSerNum.Name = "CableSerNum"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(584, 507)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 25)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(674, 507)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 25)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
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
        Me.GroupBox3.Visible = False
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
        Me.ckVibEnable.Location = New System.Drawing.Point(170, 21)
        Me.ckVibEnable.Name = "ckVibEnable"
        Me.ckVibEnable.Size = New System.Drawing.Size(63, 19)
        Me.ckVibEnable.TabIndex = 1
        Me.ckVibEnable.Text = "Enable"
        Me.ckVibEnable.UseVisualStyleBackColor = True
        '
        'FormConfig
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(761, 544)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConfig"
        Me.Text = "CATS Device Setup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.dgvInstrs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
    Friend WithEvents Label2 As Label
    Friend WithEvents cbVibAddress As ComboBox
    Friend WithEvents ckVibEnable As CheckBox
    Friend WithEvents Enable As DataGridViewCheckBoxColumn
    Friend WithEvents FreqBand As DataGridViewComboBoxColumn
    Friend WithEvents Model As DataGridViewComboBoxColumn
    Friend WithEvents Vendor As DataGridViewComboBoxColumn
    Friend WithEvents Address As DataGridViewTextBoxColumn
    Friend WithEvents BandIdx As DataGridViewTextBoxColumn
    Friend WithEvents TXLLoss As DataGridViewTextBoxColumn
    Friend WithEvents TxRLoss As DataGridViewTextBoxColumn
    Friend WithEvents RxLoss As DataGridViewTextBoxColumn
    Friend WithEvents CableSerNum As DataGridViewTextBoxColumn
End Class
