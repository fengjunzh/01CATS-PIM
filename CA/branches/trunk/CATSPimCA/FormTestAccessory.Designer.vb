<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormTestAccessory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormTestAccessory))
        Me.fgAccessory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.gbAccessoryDetail = New System.Windows.Forms.GroupBox()
        Me.btnSave = New System.Windows.Forms.Button()
        CType(Me.fgAccessory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbAccessoryDetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'fgAccessory
        '
        Me.fgAccessory.AllowAddNew = True
        Me.fgAccessory.AllowDelete = True
        Me.fgAccessory.ColumnInfo = "1,1,0,0,0,105,Columns:"
        Me.fgAccessory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fgAccessory.ExtendLastCol = True
        Me.fgAccessory.Location = New System.Drawing.Point(3, 18)
        Me.fgAccessory.Name = "fgAccessory"
        Me.fgAccessory.Rows.Count = 1
        Me.fgAccessory.Rows.DefaultSize = 21
        Me.fgAccessory.Size = New System.Drawing.Size(758, 320)
        Me.fgAccessory.StyleInfo = resources.GetString("fgAccessory.StyleInfo")
        Me.fgAccessory.TabIndex = 1
        Me.fgAccessory.VisualStyle = C1.Win.C1FlexGrid.VisualStyle.Office2010Blue
        '
        'gbAccessoryDetail
        '
        Me.gbAccessoryDetail.Controls.Add(Me.fgAccessory)
        Me.gbAccessoryDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbAccessoryDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAccessoryDetail.Location = New System.Drawing.Point(0, 0)
        Me.gbAccessoryDetail.Name = "gbAccessoryDetail"
        Me.gbAccessoryDetail.Size = New System.Drawing.Size(764, 341)
        Me.gbAccessoryDetail.TabIndex = 3
        Me.gbAccessoryDetail.TabStop = False
        Me.gbAccessoryDetail.Text = "Accessory Detail:"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(636, 369)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(116, 25)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "Save and Exit"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'FormTestAccessory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 406)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.gbAccessoryDetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormTestAccessory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Test Accessory"
        CType(Me.fgAccessory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbAccessoryDetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents fgAccessory As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents gbAccessoryDetail As GroupBox
    Friend WithEvents btnSave As Button
End Class
