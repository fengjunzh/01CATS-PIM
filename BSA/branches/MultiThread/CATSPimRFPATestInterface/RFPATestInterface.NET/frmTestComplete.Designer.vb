<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTestComplete
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents CmdAction As System.Windows.Forms.Button
    Public WithEvents cmdRepeat As System.Windows.Forms.Button
    Public WithEvents LabelPassFail As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container()
    Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
    Me.CmdAction = New System.Windows.Forms.Button()
    Me.cmdRepeat = New System.Windows.Forms.Button()
    Me.LabelPassFail = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'CmdAction
    '
    Me.CmdAction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.CmdAction.BackColor = System.Drawing.SystemColors.Control
    Me.CmdAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.CmdAction.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CmdAction.ForeColor = System.Drawing.SystemColors.ControlText
    Me.CmdAction.Location = New System.Drawing.Point(102, 115)
    Me.CmdAction.Name = "CmdAction"
    Me.CmdAction.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.CmdAction.Size = New System.Drawing.Size(81, 33)
    Me.CmdAction.TabIndex = 2
    Me.CmdAction.Text = "OK"
    Me.CmdAction.UseVisualStyleBackColor = False
    '
    'cmdRepeat
    '
    Me.cmdRepeat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRepeat.BackColor = System.Drawing.SystemColors.Control
    Me.cmdRepeat.Cursor = System.Windows.Forms.Cursors.Default
    Me.cmdRepeat.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.cmdRepeat.ForeColor = System.Drawing.SystemColors.ControlText
    Me.cmdRepeat.Location = New System.Drawing.Point(7, 115)
    Me.cmdRepeat.Name = "cmdRepeat"
    Me.cmdRepeat.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.cmdRepeat.Size = New System.Drawing.Size(88, 33)
    Me.cmdRepeat.TabIndex = 1
    Me.cmdRepeat.Text = "Next Board"
    Me.cmdRepeat.UseVisualStyleBackColor = False
    Me.cmdRepeat.Visible = False
    '
    'LabelPassFail
    '
    Me.LabelPassFail.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.LabelPassFail.BackColor = System.Drawing.SystemColors.Control
    Me.LabelPassFail.Cursor = System.Windows.Forms.Cursors.Default
    Me.LabelPassFail.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelPassFail.ForeColor = System.Drawing.SystemColors.ControlText
    Me.LabelPassFail.Location = New System.Drawing.Point(-2, 27)
    Me.LabelPassFail.Name = "LabelPassFail"
    Me.LabelPassFail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.LabelPassFail.Size = New System.Drawing.Size(295, 56)
    Me.LabelPassFail.TabIndex = 0
    Me.LabelPassFail.Text = "PASS"
    Me.LabelPassFail.TextAlign = System.Drawing.ContentAlignment.TopCenter
    '
    'frmTestComplete
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.ClientSize = New System.Drawing.Size(291, 157)
    Me.ControlBox = False
    Me.Controls.Add(Me.CmdAction)
    Me.Controls.Add(Me.cmdRepeat)
    Me.Controls.Add(Me.LabelPassFail)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Location = New System.Drawing.Point(3, 22)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmTestComplete"
    Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Test Complete"
    Me.TopMost = True
    Me.ResumeLayout(False)

  End Sub
#End Region 
End Class