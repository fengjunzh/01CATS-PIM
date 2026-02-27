<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRetryReason
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormRetryReason))
        Me.picReconnect = New System.Windows.Forms.PictureBox()
        Me.picRetest = New System.Windows.Forms.PictureBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.rbRetry = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.rbReconnect = New System.Windows.Forms.RadioButton()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.rbOther = New System.Windows.Forms.RadioButton()
        Me.txtOther = New System.Windows.Forms.TextBox()
        Me.btnRetry = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.picReconnect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRetest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picReconnect
        '
        Me.picReconnect.Image = CType(resources.GetObject("picReconnect.Image"), System.Drawing.Image)
        Me.picReconnect.Location = New System.Drawing.Point(50, 140)
        Me.picReconnect.Margin = New System.Windows.Forms.Padding(6)
        Me.picReconnect.Name = "picReconnect"
        Me.picReconnect.Size = New System.Drawing.Size(221, 19)
        Me.picReconnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picReconnect.TabIndex = 0
        Me.picReconnect.TabStop = False
        '
        'picRetest
        '
        Me.picRetest.Image = CType(resources.GetObject("picRetest.Image"), System.Drawing.Image)
        Me.picRetest.Location = New System.Drawing.Point(50, 90)
        Me.picRetest.Margin = New System.Windows.Forms.Padding(6)
        Me.picRetest.Name = "picRetest"
        Me.picRetest.Size = New System.Drawing.Size(155, 20)
        Me.picRetest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picRetest.TabIndex = 1
        Me.picRetest.TabStop = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "reconnect.png")
        Me.ImageList1.Images.SetKeyName(1, "retest.png")
        '
        'rbRetry
        '
        Me.rbRetry.AutoSize = True
        Me.rbRetry.Checked = True
        Me.rbRetry.Location = New System.Drawing.Point(10, 96)
        Me.rbRetry.Margin = New System.Windows.Forms.Padding(6)
        Me.rbRetry.Name = "rbRetry"
        Me.rbRetry.Size = New System.Drawing.Size(27, 26)
        Me.rbRetry.TabIndex = 2
        Me.rbRetry.TabStop = True
        Me.rbRetry.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(10, 23)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(918, 50)
        Me.PictureBox1.TabIndex = 3
        Me.PictureBox1.TabStop = False
        '
        'rbReconnect
        '
        Me.rbReconnect.AutoSize = True
        Me.rbReconnect.Location = New System.Drawing.Point(10, 146)
        Me.rbReconnect.Margin = New System.Windows.Forms.Padding(6)
        Me.rbReconnect.Name = "rbReconnect"
        Me.rbReconnect.Size = New System.Drawing.Size(27, 26)
        Me.rbReconnect.TabIndex = 4
        Me.rbReconnect.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(50, 190)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(6)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(84, 19)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 5
        Me.PictureBox2.TabStop = False
        '
        'rbOther
        '
        Me.rbOther.AutoSize = True
        Me.rbOther.Location = New System.Drawing.Point(10, 194)
        Me.rbOther.Margin = New System.Windows.Forms.Padding(6)
        Me.rbOther.Name = "rbOther"
        Me.rbOther.Size = New System.Drawing.Size(27, 26)
        Me.rbOther.TabIndex = 6
        Me.rbOther.UseVisualStyleBackColor = True
        '
        'txtOther
        '
        Me.txtOther.Location = New System.Drawing.Point(50, 229)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(6)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(878, 31)
        Me.txtOther.TabIndex = 7
        '
        'btnRetry
        '
        Me.btnRetry.Location = New System.Drawing.Point(254, 289)
        Me.btnRetry.Margin = New System.Windows.Forms.Padding(6)
        Me.btnRetry.Name = "btnRetry"
        Me.btnRetry.Size = New System.Drawing.Size(168, 48)
        Me.btnRetry.TabIndex = 8
        Me.btnRetry.Text = "Retry Test"
        Me.btnRetry.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(552, 289)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(168, 48)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit Test"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 91)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 25)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 25)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Label2"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(50, 466)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(878, 152)
        Me.Panel1.TabIndex = 12
        Me.Panel1.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(50, 409)
        Me.Button1.Margin = New System.Windows.Forms.Padding(6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(221, 48)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "DTF Test(Debug)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormRetryReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(938, 656)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnRetry)
        Me.Controls.Add(Me.txtOther)
        Me.Controls.Add(Me.rbOther)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.rbReconnect)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.rbRetry)
        Me.Controls.Add(Me.picRetest)
        Me.Controls.Add(Me.picReconnect)
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormRetryReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Retry Reason"
        CType(Me.picReconnect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRetest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picReconnect As PictureBox
  Friend WithEvents picRetest As PictureBox
  Friend WithEvents ImageList1 As ImageList
  Friend WithEvents rbRetry As RadioButton
  Friend WithEvents PictureBox1 As PictureBox
  Friend WithEvents rbReconnect As System.Windows.Forms.RadioButton
  Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
  Friend WithEvents rbOther As System.Windows.Forms.RadioButton
  Friend WithEvents txtOther As System.Windows.Forms.TextBox
  Friend WithEvents btnRetry As System.Windows.Forms.Button
  Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
End Class
