<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CalibrationRecord
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
        Me.Cal_Record = New System.Windows.Forms.DataGridView()
        Me.PIMBand = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PC_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowPim_Load = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LowPim_Load_Spec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Standard_110dBm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Power = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Op_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.Cal_Record, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Cal_Record
        '
        Me.Cal_Record.AllowUserToAddRows = False
        Me.Cal_Record.AllowUserToDeleteRows = False
        Me.Cal_Record.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.Cal_Record.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Cal_Record.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PIMBand, Me.PC_Name, Me.LowPim_Load, Me.LowPim_Load_Spec, Me.Standard_110dBm, Me.Power, Me.Op_ID})
        Me.Cal_Record.Location = New System.Drawing.Point(8, 8)
        Me.Cal_Record.Margin = New System.Windows.Forms.Padding(2)
        Me.Cal_Record.Name = "Cal_Record"
        Me.Cal_Record.RowTemplate.Height = 33
        Me.Cal_Record.Size = New System.Drawing.Size(1133, 589)
        Me.Cal_Record.TabIndex = 1
        '
        'PIMBand
        '
        Me.PIMBand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.PIMBand.HeaderText = "PIMBand"
        Me.PIMBand.Name = "PIMBand"
        Me.PIMBand.ReadOnly = True
        Me.PIMBand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PC_Name
        '
        Me.PC_Name.HeaderText = "PC_Name"
        Me.PC_Name.Name = "PC_Name"
        Me.PC_Name.ReadOnly = True
        '
        'LowPim_Load
        '
        Me.LowPim_Load.HeaderText = "LowPim_Load"
        Me.LowPim_Load.Name = "LowPim_Load"
        Me.LowPim_Load.ReadOnly = True
        '
        'LowPim_Load_Spec
        '
        Me.LowPim_Load_Spec.HeaderText = "LowPim_Load_Spec"
        Me.LowPim_Load_Spec.Name = "LowPim_Load_Spec"
        Me.LowPim_Load_Spec.ReadOnly = True
        '
        'Standard_110dBm
        '
        Me.Standard_110dBm.HeaderText = "Standard_110dBm"
        Me.Standard_110dBm.Name = "Standard_110dBm"
        Me.Standard_110dBm.ReadOnly = True
        '
        'Power
        '
        Me.Power.HeaderText = "Power"
        Me.Power.Name = "Power"
        Me.Power.ReadOnly = True
        '
        'Op_ID
        '
        Me.Op_ID.HeaderText = "Op_ID"
        Me.Op_ID.Name = "Op_ID"
        Me.Op_ID.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(466, 559)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 33)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'CalibrationRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1386, 600)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Cal_Record)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "CalibrationRecord"
        Me.Text = "CalibrationRecord"
        CType(Me.Cal_Record, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Cal_Record As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents PIMBand As DataGridViewTextBoxColumn
    Friend WithEvents PC_Name As DataGridViewTextBoxColumn
    Friend WithEvents LowPim_Load As DataGridViewTextBoxColumn
    Friend WithEvents LowPim_Load_Spec As DataGridViewTextBoxColumn
    Friend WithEvents Standard_110dBm As DataGridViewTextBoxColumn
    Friend WithEvents Power As DataGridViewTextBoxColumn
    Friend WithEvents Op_ID As DataGridViewTextBoxColumn
End Class
