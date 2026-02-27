<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConnectDevice
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
        Me.btnInsert = New System.Windows.Forms.Button()
        Me.btnGoHome = New System.Windows.Forms.Button()
        Me.btnInit = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnInsert
        '
        Me.btnInsert.Location = New System.Drawing.Point(132, 25)
        Me.btnInsert.Name = "btnInsert"
        Me.btnInsert.Size = New System.Drawing.Size(102, 23)
        Me.btnInsert.TabIndex = 0
        Me.btnInsert.Text = "Insert"
        Me.btnInsert.UseVisualStyleBackColor = True
        '
        'btnGoHome
        '
        Me.btnGoHome.Location = New System.Drawing.Point(132, 69)
        Me.btnGoHome.Name = "btnGoHome"
        Me.btnGoHome.Size = New System.Drawing.Size(102, 23)
        Me.btnGoHome.TabIndex = 0
        Me.btnGoHome.Text = "Go Home"
        Me.btnGoHome.UseVisualStyleBackColor = True
        '
        'btnInit
        '
        Me.btnInit.Location = New System.Drawing.Point(12, 25)
        Me.btnInit.Name = "btnInit"
        Me.btnInit.Size = New System.Drawing.Size(102, 23)
        Me.btnInit.TabIndex = 0
        Me.btnInit.Text = "Initialize"
        Me.btnInit.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(12, 69)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(102, 23)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "Close Device"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FormConnectDevice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(246, 125)
        Me.Controls.Add(Me.btnGoHome)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnInit)
        Me.Controls.Add(Me.btnInsert)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormConnectDevice"
        Me.Text = "Auto Connect Device Controller"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnInsert As Button
    Friend WithEvents btnGoHome As Button
    Friend WithEvents btnInit As Button
    Friend WithEvents btnClose As Button
End Class
