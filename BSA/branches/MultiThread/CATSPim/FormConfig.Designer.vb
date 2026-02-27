<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConfig
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
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.cbRetAddress = New System.Windows.Forms.ComboBox()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.dgvInstrs = New System.Windows.Forms.DataGridView()
    Me.btnSave = New System.Windows.Forms.Button()
    Me.btnExit = New System.Windows.Forms.Button()
    Me.GroupBox3 = New System.Windows.Forms.GroupBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.cbVibAddress = New System.Windows.Forms.ComboBox()
    Me.ckVibEnable = New System.Windows.Forms.CheckBox()
    Me.GroupBox4 = New System.Windows.Forms.GroupBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.cbMode = New System.Windows.Forms.ComboBox()
    Me.Enable = New System.Windows.Forms.DataGridViewCheckBoxColumn()
    Me.FreqBand = New System.Windows.Forms.DataGridViewComboBoxColumn()
    Me.Model = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.Vendor = New System.Windows.Forms.DataGridViewComboBoxColumn()
    Me.Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.BandIdx = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TXLLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.TxRLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.RxLoss = New System.Windows.Forms.DataGridViewTextBoxColumn()
    Me.GroupBox1.SuspendLayout()
    Me.GroupBox2.SuspendLayout()
    CType(Me.dgvInstrs, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.GroupBox3.SuspendLayout()
    Me.GroupBox4.SuspendLayout()
    Me.SuspendLayout()
    '
    'GroupBox1
    '
    Me.GroupBox1.Controls.Add(Me.Label1)
    Me.GroupBox1.Controls.Add(Me.cbRetAddress)
    Me.GroupBox1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox1.Location = New System.Drawing.Point(173, 12)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(227, 71)
    Me.GroupBox1.TabIndex = 1
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Ret"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 21)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(63, 14)
    Me.Label1.TabIndex = 3
    Me.Label1.Text = "Address:"
    '
    'cbRetAddress
    '
    Me.cbRetAddress.FormattingEnabled = True
    Me.cbRetAddress.Location = New System.Drawing.Point(9, 38)
    Me.cbRetAddress.Name = "cbRetAddress"
    Me.cbRetAddress.Size = New System.Drawing.Size(205, 22)
    Me.cbRetAddress.TabIndex = 2
    '
    'GroupBox2
    '
    Me.GroupBox2.Controls.Add(Me.dgvInstrs)
    Me.GroupBox2.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox2.Location = New System.Drawing.Point(8, 89)
    Me.GroupBox2.Name = "GroupBox2"
    Me.GroupBox2.Size = New System.Drawing.Size(755, 239)
    Me.GroupBox2.TabIndex = 2
    Me.GroupBox2.TabStop = False
    Me.GroupBox2.Text = "Pim Equipments"
    '
    'dgvInstrs
    '
    Me.dgvInstrs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvInstrs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Enable, Me.FreqBand, Me.Model, Me.Vendor, Me.Address, Me.BandIdx, Me.TXLLoss, Me.TxRLoss, Me.RxLoss})
    Me.dgvInstrs.Location = New System.Drawing.Point(7, 20)
    Me.dgvInstrs.Name = "dgvInstrs"
    Me.dgvInstrs.Size = New System.Drawing.Size(738, 213)
    Me.dgvInstrs.TabIndex = 0
    '
    'btnSave
    '
    Me.btnSave.Font = New System.Drawing.Font("Microsoft YaHei", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnSave.Location = New System.Drawing.Point(575, 58)
    Me.btnSave.Name = "btnSave"
    Me.btnSave.Size = New System.Drawing.Size(78, 25)
    Me.btnSave.TabIndex = 3
    Me.btnSave.Text = "Save"
    Me.btnSave.UseVisualStyleBackColor = True
    '
    'btnExit
    '
    Me.btnExit.Font = New System.Drawing.Font("Microsoft YaHei", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.btnExit.Location = New System.Drawing.Point(672, 58)
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
    Me.GroupBox3.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox3.Location = New System.Drawing.Point(406, 12)
    Me.GroupBox3.Name = "GroupBox3"
    Me.GroupBox3.Size = New System.Drawing.Size(157, 71)
    Me.GroupBox3.TabIndex = 5
    Me.GroupBox3.TabStop = False
    Me.GroupBox3.Text = "Vibration"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(4, 21)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(63, 14)
    Me.Label2.TabIndex = 3
    Me.Label2.Text = "Address:"
    '
    'cbVibAddress
    '
    Me.cbVibAddress.FormattingEnabled = True
    Me.cbVibAddress.Location = New System.Drawing.Point(7, 38)
    Me.cbVibAddress.Name = "cbVibAddress"
    Me.cbVibAddress.Size = New System.Drawing.Size(139, 22)
    Me.cbVibAddress.TabIndex = 2
    '
    'ckVibEnable
    '
    Me.ckVibEnable.AutoSize = True
    Me.ckVibEnable.Location = New System.Drawing.Point(78, 21)
    Me.ckVibEnable.Name = "ckVibEnable"
    Me.ckVibEnable.Size = New System.Drawing.Size(68, 18)
    Me.ckVibEnable.TabIndex = 1
    Me.ckVibEnable.Text = "Enable"
    Me.ckVibEnable.UseVisualStyleBackColor = True
    '
    'GroupBox4
    '
    Me.GroupBox4.Controls.Add(Me.Label3)
    Me.GroupBox4.Controls.Add(Me.cbMode)
    Me.GroupBox4.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GroupBox4.Location = New System.Drawing.Point(8, 12)
    Me.GroupBox4.Name = "GroupBox4"
    Me.GroupBox4.Size = New System.Drawing.Size(159, 71)
    Me.GroupBox4.TabIndex = 6
    Me.GroupBox4.TabStop = False
    Me.GroupBox4.Text = "TestMode"
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(6, 21)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(42, 14)
    Me.Label3.TabIndex = 3
    Me.Label3.Text = "Name:"
    '
    'cbMode
    '
    Me.cbMode.FormattingEnabled = True
    Me.cbMode.Location = New System.Drawing.Point(9, 38)
    Me.cbMode.Name = "cbMode"
    Me.cbMode.Size = New System.Drawing.Size(137, 22)
    Me.cbMode.TabIndex = 2
    '
    'Enable
    '
    Me.Enable.DataPropertyName = "Enable"
    Me.Enable.FalseValue = "False"
    Me.Enable.HeaderText = "Enable"
    Me.Enable.Name = "Enable"
    Me.Enable.TrueValue = "True"
    Me.Enable.Width = 50
    '
    'FreqBand
    '
    Me.FreqBand.DataPropertyName = "BandName"
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
    Me.Address.Width = 120
    '
    'BandIdx
    '
    Me.BandIdx.DataPropertyName = "BandIdx"
    Me.BandIdx.HeaderText = "Band Idx"
    Me.BandIdx.Name = "BandIdx"
    Me.BandIdx.Width = 60
    '
    'TXLLoss
    '
    Me.TXLLoss.DataPropertyName = "Tx1Loss"
    Me.TXLLoss.HeaderText = "TxL Loss"
    Me.TXLLoss.Name = "TXLLoss"
    Me.TXLLoss.Width = 60
    '
    'TxRLoss
    '
    Me.TxRLoss.DataPropertyName = "Tx2Loss"
    Me.TxRLoss.HeaderText = "TxR Loss"
    Me.TxRLoss.Name = "TxRLoss"
    Me.TxRLoss.Width = 60
    '
    'RxLoss
    '
    Me.RxLoss.DataPropertyName = "RxLoss"
    Me.RxLoss.HeaderText = "Rx Loss"
    Me.RxLoss.Name = "RxLoss"
    Me.RxLoss.Width = 60
    '
    'FormConfig
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(770, 334)
    Me.Controls.Add(Me.GroupBox4)
    Me.Controls.Add(Me.GroupBox3)
    Me.Controls.Add(Me.btnExit)
    Me.Controls.Add(Me.btnSave)
    Me.Controls.Add(Me.GroupBox2)
    Me.Controls.Add(Me.GroupBox1)
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
  Friend WithEvents GroupBox4 As GroupBox
  Friend WithEvents Label3 As Label
  Friend WithEvents cbMode As ComboBox
  Friend WithEvents Enable As System.Windows.Forms.DataGridViewCheckBoxColumn
  Friend WithEvents FreqBand As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents Model As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents Vendor As System.Windows.Forms.DataGridViewComboBoxColumn
  Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents BandIdx As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents TXLLoss As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents TxRLoss As System.Windows.Forms.DataGridViewTextBoxColumn
  Friend WithEvents RxLoss As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
