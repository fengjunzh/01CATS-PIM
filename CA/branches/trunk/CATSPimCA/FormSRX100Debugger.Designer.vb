<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormSRX100Debugger
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
        Me.NICcomboBox = New System.Windows.Forms.ComboBox()
        Me.SctBtn = New System.Windows.Forms.CheckBox()
        Me.SchBtn = New System.Windows.Forms.Button()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.DataText = New System.Windows.Forms.TextBox()
        Me.TgrBtn = New System.Windows.Forms.Button()
        Me.LiveviewForm1 = New Keyence.AutoID.SDK.LiveviewForm()
        Me.SuspendLayout()
        '
        'NICcomboBox
        '
        Me.NICcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NICcomboBox.FormattingEnabled = True
        Me.NICcomboBox.Location = New System.Drawing.Point(18, 13)
        Me.NICcomboBox.Name = "NICcomboBox"
        Me.NICcomboBox.Size = New System.Drawing.Size(150, 21)
        Me.NICcomboBox.TabIndex = 21
        '
        'SctBtn
        '
        Me.SctBtn.Appearance = System.Windows.Forms.Appearance.Button
        Me.SctBtn.AutoSize = True
        Me.SctBtn.Enabled = False
        Me.SctBtn.Location = New System.Drawing.Point(175, 45)
        Me.SctBtn.Name = "SctBtn"
        Me.SctBtn.Size = New System.Drawing.Size(82, 23)
        Me.SctBtn.TabIndex = 20
        Me.SctBtn.Text = "ReaderSelect"
        Me.SctBtn.UseVisualStyleBackColor = True
        '
        'SchBtn
        '
        Me.SchBtn.Location = New System.Drawing.Point(174, 10)
        Me.SchBtn.Name = "SchBtn"
        Me.SchBtn.Size = New System.Drawing.Size(83, 25)
        Me.SchBtn.TabIndex = 19
        Me.SchBtn.Text = "Search"
        Me.SchBtn.UseVisualStyleBackColor = True
        '
        'comboBox1
        '
        Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Location = New System.Drawing.Point(18, 47)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(150, 21)
        Me.comboBox1.TabIndex = 18
        '
        'DataText
        '
        Me.DataText.BackColor = System.Drawing.SystemColors.Control
        Me.DataText.Location = New System.Drawing.Point(12, 418)
        Me.DataText.MaxLength = 10
        Me.DataText.Name = "DataText"
        Me.DataText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.DataText.Size = New System.Drawing.Size(388, 20)
        Me.DataText.TabIndex = 17
        '
        'TgrBtn
        '
        Me.TgrBtn.Enabled = False
        Me.TgrBtn.Location = New System.Drawing.Point(18, 81)
        Me.TgrBtn.Name = "TgrBtn"
        Me.TgrBtn.Size = New System.Drawing.Size(103, 28)
        Me.TgrBtn.TabIndex = 16
        Me.TgrBtn.Text = "Trigger On"
        Me.TgrBtn.UseVisualStyleBackColor = True
        '
        'liveviewForm1
        '
        Me.LiveviewForm1.BackColor = System.Drawing.Color.Black
        Me.LiveviewForm1.BinningType = Keyence.AutoID.SDK.LiveviewForm.ImageBinningType.OneQuarter
        Me.LiveviewForm1.ImageFormat = Keyence.AutoID.SDK.LiveviewForm.ImageFormatType.Jpeg
        Me.LiveviewForm1.ImageQuality = 5
        Me.LiveviewForm1.IpAddress = "192.168.100.100"
        Me.LiveviewForm1.Location = New System.Drawing.Point(18, 120)
        Me.LiveviewForm1.Name = "liveviewForm1"
        Me.LiveviewForm1.PullTimeSpan = 100
        Me.LiveviewForm1.Size = New System.Drawing.Size(334, 219)
        Me.LiveviewForm1.TabIndex = 8
        Me.LiveviewForm1.TimeoutMs = 2000
        '
        'FormSRX100Debugger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(412, 450)
        Me.Controls.Add(Me.NICcomboBox)
        Me.Controls.Add(Me.SctBtn)
        Me.Controls.Add(Me.SchBtn)
        Me.Controls.Add(Me.comboBox1)
        Me.Controls.Add(Me.DataText)
        Me.Controls.Add(Me.TgrBtn)
        Me.Controls.Add(Me.LiveviewForm1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormSRX100Debugger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "KEYENCE SRX-100 Debugger"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents NICcomboBox As ComboBox
    Private WithEvents SctBtn As CheckBox
    Private WithEvents SchBtn As Button
    Private WithEvents comboBox1 As ComboBox
    Private WithEvents DataText As TextBox
    Private WithEvents TgrBtn As Button
    Friend WithEvents LiveviewForm1 As Keyence.AutoID.SDK.LiveviewForm
End Class
