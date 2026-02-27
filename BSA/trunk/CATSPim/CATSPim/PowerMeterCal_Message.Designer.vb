<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PowerMeterCal_Message
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
        Me.CalMessage = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CalMessage
        '
        Me.CalMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalMessage.FormattingEnabled = True
        Me.CalMessage.ItemHeight = 31
        Me.CalMessage.Location = New System.Drawing.Point(-3, -4)
        Me.CalMessage.Name = "CalMessage"
        Me.CalMessage.ScrollAlwaysVisible = True
        Me.CalMessage.Size = New System.Drawing.Size(1075, 717)
        Me.CalMessage.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(951, 759)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(254, 70)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Close"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PowerMeterCal_Message
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.Yellow
        Me.ClientSize = New System.Drawing.Size(1200, 828)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CalMessage)
        Me.Name = "PowerMeterCal_Message"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CalMessage As ListBox
    Friend WithEvents Button1 As Button
End Class
