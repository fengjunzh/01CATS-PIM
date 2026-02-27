<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GetSN
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
        Me.Manual_sel = New System.Windows.Forms.RadioButton()
        Me.InputGetSn = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckCode_Disp = New System.Windows.Forms.Label()
        Me.CheckCode_Input = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OK_InputSn = New System.Windows.Forms.Button()
        Me.Scan_sel = New System.Windows.Forms.RadioButton()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Manual_sel
        '
        Me.Manual_sel.AutoSize = True
        Me.Manual_sel.Location = New System.Drawing.Point(232, 13)
        Me.Manual_sel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Manual_sel.Name = "Manual_sel"
        Me.Manual_sel.Size = New System.Drawing.Size(152, 21)
        Me.Manual_sel.TabIndex = 11
        Me.Manual_sel.Text = "Input SN by Manual"
        Me.Manual_sel.UseVisualStyleBackColor = True
        '
        'InputGetSn
        '
        Me.InputGetSn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InputGetSn.Location = New System.Drawing.Point(65, 33)
        Me.InputGetSn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.InputGetSn.Name = "InputGetSn"
        Me.InputGetSn.Size = New System.Drawing.Size(369, 30)
        Me.InputGetSn.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(205, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Please Input DevSerialNumber:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.CheckCode_Disp)
        Me.Panel2.Controls.Add(Me.CheckCode_Input)
        Me.Panel2.Location = New System.Drawing.Point(43, 64)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(455, 120)
        Me.Panel2.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 51)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(175, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Please input CheckCode:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(2, 13)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 18)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "CheckCode:"
        '
        'CheckCode_Disp
        '
        Me.CheckCode_Disp.AutoSize = True
        Me.CheckCode_Disp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckCode_Disp.Location = New System.Drawing.Point(103, 10)
        Me.CheckCode_Disp.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CheckCode_Disp.Name = "CheckCode_Disp"
        Me.CheckCode_Disp.Size = New System.Drawing.Size(71, 25)
        Me.CheckCode_Disp.TabIndex = 2
        Me.CheckCode_Disp.Text = "Label2"
        '
        'CheckCode_Input
        '
        Me.CheckCode_Input.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckCode_Input.Location = New System.Drawing.Point(65, 79)
        Me.CheckCode_Input.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CheckCode_Input.Name = "CheckCode_Input"
        Me.CheckCode_Input.Size = New System.Drawing.Size(369, 30)
        Me.CheckCode_Input.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.InputGetSn)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(43, 198)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(455, 77)
        Me.Panel1.TabIndex = 8
        '
        'OK_InputSn
        '
        Me.OK_InputSn.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OK_InputSn.Location = New System.Drawing.Point(241, 279)
        Me.OK_InputSn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.OK_InputSn.Name = "OK_InputSn"
        Me.OK_InputSn.Size = New System.Drawing.Size(119, 36)
        Me.OK_InputSn.TabIndex = 7
        Me.OK_InputSn.Text = "OK"
        Me.OK_InputSn.UseVisualStyleBackColor = True
        '
        'Scan_sel
        '
        Me.Scan_sel.AutoSize = True
        Me.Scan_sel.Checked = True
        Me.Scan_sel.Location = New System.Drawing.Point(43, 13)
        Me.Scan_sel.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Scan_sel.Name = "Scan_sel"
        Me.Scan_sel.Size = New System.Drawing.Size(138, 21)
        Me.Scan_sel.TabIndex = 10
        Me.Scan_sel.TabStop = True
        Me.Scan_sel.Text = "Input SN by Scan"
        Me.Scan_sel.UseVisualStyleBackColor = True
        '
        'GetSN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(537, 375)
        Me.ControlBox = False
        Me.Controls.Add(Me.Manual_sel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.OK_InputSn)
        Me.Controls.Add(Me.Scan_sel)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "GetSN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "GetSN"
        Me.TopMost = True
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Manual_sel As RadioButton
    Friend WithEvents InputGetSn As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckCode_Disp As Label
    Friend WithEvents CheckCode_Input As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents OK_InputSn As Button
    Friend WithEvents Scan_sel As RadioButton
End Class
