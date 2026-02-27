<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSAPFailSafeMode
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
        Me.chkSAPFailSafeModeOnOff = New System.Windows.Forms.CheckBox()
        Me.txtWorkOrder = New System.Windows.Forms.TextBox()
        Me.lblWorkOrder = New System.Windows.Forms.Label()
        Me.lblPartNumber = New System.Windows.Forms.Label()
        Me.cmbPartNumber = New System.Windows.Forms.ComboBox()
        Me.lblLength = New System.Windows.Forms.Label()
        Me.txtLength = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboUOM = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'chkSAPFailSafeModeOnOff
        '
        Me.chkSAPFailSafeModeOnOff.AutoSize = True
        Me.chkSAPFailSafeModeOnOff.Location = New System.Drawing.Point(16, 24)
        Me.chkSAPFailSafeModeOnOff.Name = "chkSAPFailSafeModeOnOff"
        Me.chkSAPFailSafeModeOnOff.Size = New System.Drawing.Size(121, 17)
        Me.chkSAPFailSafeModeOnOff.TabIndex = 0
        Me.chkSAPFailSafeModeOnOff.Text = "SAP Fail Safe Mode"
        Me.chkSAPFailSafeModeOnOff.UseVisualStyleBackColor = True
        '
        'txtWorkOrder
        '
        Me.txtWorkOrder.Enabled = False
        Me.txtWorkOrder.Location = New System.Drawing.Point(82, 60)
        Me.txtWorkOrder.Name = "txtWorkOrder"
        Me.txtWorkOrder.Size = New System.Drawing.Size(216, 20)
        Me.txtWorkOrder.TabIndex = 1
        '
        'lblWorkOrder
        '
        Me.lblWorkOrder.AutoSize = True
        Me.lblWorkOrder.Location = New System.Drawing.Point(13, 64)
        Me.lblWorkOrder.Name = "lblWorkOrder"
        Me.lblWorkOrder.Size = New System.Drawing.Size(65, 13)
        Me.lblWorkOrder.TabIndex = 2
        Me.lblWorkOrder.Text = "Work Order:"
        '
        'lblPartNumber
        '
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.Location = New System.Drawing.Point(9, 92)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(69, 13)
        Me.lblPartNumber.TabIndex = 2
        Me.lblPartNumber.Text = "Part Number:"
        '
        'cmbPartNumber
        '
        Me.cmbPartNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPartNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPartNumber.Enabled = False
        Me.cmbPartNumber.FormattingEnabled = True
        Me.cmbPartNumber.Location = New System.Drawing.Point(82, 88)
        Me.cmbPartNumber.Name = "cmbPartNumber"
        Me.cmbPartNumber.Size = New System.Drawing.Size(216, 21)
        Me.cmbPartNumber.TabIndex = 2
        '
        'lblLength
        '
        Me.lblLength.AutoSize = True
        Me.lblLength.Location = New System.Drawing.Point(35, 118)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(43, 13)
        Me.lblLength.TabIndex = 2
        Me.lblLength.Text = "Length:"
        '
        'txtLength
        '
        Me.txtLength.Enabled = False
        Me.txtLength.Location = New System.Drawing.Point(82, 116)
        Me.txtLength.Name = "txtLength"
        Me.txtLength.Size = New System.Drawing.Size(216, 20)
        Me.txtLength.TabIndex = 3
        '
        'btnExit
        '
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(220, 186)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(78, 25)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(130, 186)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 25)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 146)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "UOM:"
        '
        'cboUOM
        '
        Me.cboUOM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUOM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUOM.FormattingEnabled = True
        Me.cboUOM.Items.AddRange(New Object() {"Meter", "Feet"})
        Me.cboUOM.Location = New System.Drawing.Point(82, 144)
        Me.cboUOM.Name = "cboUOM"
        Me.cboUOM.Size = New System.Drawing.Size(216, 21)
        Me.cboUOM.TabIndex = 4
        '
        'FormSAPFailSafteMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(312, 225)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cboUOM)
        Me.Controls.Add(Me.cmbPartNumber)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblLength)
        Me.Controls.Add(Me.lblPartNumber)
        Me.Controls.Add(Me.lblWorkOrder)
        Me.Controls.Add(Me.txtLength)
        Me.Controls.Add(Me.txtWorkOrder)
        Me.Controls.Add(Me.chkSAPFailSafeModeOnOff)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSAPFailSafteMode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SAP Fail Safte Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkSAPFailSafeModeOnOff As CheckBox
    Friend WithEvents txtWorkOrder As TextBox
    Friend WithEvents lblWorkOrder As Label
    Friend WithEvents lblPartNumber As Label
    Friend WithEvents cmbPartNumber As ComboBox
    Friend WithEvents lblLength As Label
    Friend WithEvents txtLength As TextBox
    Friend WithEvents btnExit As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboUOM As ComboBox
End Class
