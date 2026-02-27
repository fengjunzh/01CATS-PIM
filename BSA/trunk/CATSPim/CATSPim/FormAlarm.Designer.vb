<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAlarm
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.btnYes = New System.Windows.Forms.Button()
		Me.btnNo = New System.Windows.Forms.Button()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Location = New System.Drawing.Point(44, 19)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(283, 56)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "The current test station  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "          is not <FinalTest>."
		'
		'btnYes
		'
		Me.btnYes.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnYes.Location = New System.Drawing.Point(67, 167)
		Me.btnYes.Name = "btnYes"
		Me.btnYes.Size = New System.Drawing.Size(75, 23)
		Me.btnYes.TabIndex = 1
		Me.btnYes.Text = "YES"
		Me.btnYes.UseVisualStyleBackColor = True
		'
		'btnNo
		'
		Me.btnNo.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnNo.Location = New System.Drawing.Point(275, 167)
		Me.btnNo.Name = "btnNo"
		Me.btnNo.Size = New System.Drawing.Size(75, 23)
		Me.btnNo.TabIndex = 2
		Me.btnNo.Text = "NO"
		Me.btnNo.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.Font = New System.Drawing.Font("Century Gothic", 18.0!, System.Drawing.FontStyle.Bold)
		Me.Label2.Location = New System.Drawing.Point(44, 101)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(313, 44)
		Me.Label2.TabIndex = 3
		Me.Label2.Text = "Do you want to continue?"
		'
		'FormAlarm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.Red
		Me.ClientSize = New System.Drawing.Size(410, 212)
		Me.ControlBox = False
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.btnNo)
		Me.Controls.Add(Me.btnYes)
		Me.Controls.Add(Me.Label1)
		Me.Name = "FormAlarm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.TopMost = True
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Label1 As Label
	Friend WithEvents btnYes As Button
	Friend WithEvents btnNo As Button
	Friend WithEvents Label2 As Label
End Class
