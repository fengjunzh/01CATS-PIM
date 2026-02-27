<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormComm
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormComm))
    Me.AxRC1 = New AxSummitekPIM.AxRC()
    CType(Me.AxRC1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'AxRC1
    '
    Me.AxRC1.Enabled = True
    Me.AxRC1.Location = New System.Drawing.Point(12, 12)
    Me.AxRC1.Name = "AxRC1"
    Me.AxRC1.OcxState = CType(resources.GetObject("AxRC1.OcxState"), System.Windows.Forms.AxHost.State)
    Me.AxRC1.Size = New System.Drawing.Size(384, 120)
    Me.AxRC1.TabIndex = 0
    '
    'FormComm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(408, 146)
    Me.Controls.Add(Me.AxRC1)
    Me.Name = "FormComm"
    Me.Text = "FormComm"
    CType(Me.AxRC1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Public WithEvents AxRC1 As AxSummitekPIM.AxRC
End Class
