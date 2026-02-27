<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVibController
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnExit = New System.Windows.Forms.Button()
        Me.buttonRefresh = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.listBoxSerialNumbers = New System.Windows.Forms.ListBox()
        Me.textBoxNumOfModules = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.groupBoxRelay = New System.Windows.Forms.GroupBox()
        Me.checkedListBoxRelays = New System.Windows.Forms.CheckedListBox()
        Me.buttonLed = New System.Windows.Forms.Button()
        Me.groupBoxModuleInfo = New System.Windows.Forms.GroupBox()
        Me.LabelId = New System.Windows.Forms.Label()
        Me.labelModuleName = New System.Windows.Forms.Label()
        Me.labelFirwareVersion = New System.Windows.Forms.Label()
        Me.labelDriverVersion = New System.Windows.Forms.Label()
        Me.labelClassLibVersion = New System.Windows.Forms.Label()
        Me.groupBoxRelay.SuspendLayout()
        Me.groupBoxModuleInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 200
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(534, 54)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'buttonRefresh
        '
        Me.buttonRefresh.Location = New System.Drawing.Point(22, 13)
        Me.buttonRefresh.Name = "buttonRefresh"
        Me.buttonRefresh.Size = New System.Drawing.Size(142, 23)
        Me.buttonRefresh.TabIndex = 35
        Me.buttonRefresh.Text = "Rescan For Modules"
        Me.buttonRefresh.UseVisualStyleBackColor = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(35, 110)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(116, 13)
        Me.label2.TabIndex = 34
        Me.label2.Text = "Module Serial Numbers"
        '
        'listBoxSerialNumbers
        '
        Me.listBoxSerialNumbers.FormattingEnabled = True
        Me.listBoxSerialNumbers.Location = New System.Drawing.Point(29, 126)
        Me.listBoxSerialNumbers.Name = "listBoxSerialNumbers"
        Me.listBoxSerialNumbers.Size = New System.Drawing.Size(128, 108)
        Me.listBoxSerialNumbers.TabIndex = 33
        '
        'textBoxNumOfModules
        '
        Me.textBoxNumOfModules.CausesValidation = False
        Me.textBoxNumOfModules.Location = New System.Drawing.Point(74, 81)
        Me.textBoxNumOfModules.Name = "textBoxNumOfModules"
        Me.textBoxNumOfModules.ReadOnly = True
        Me.textBoxNumOfModules.Size = New System.Drawing.Size(38, 20)
        Me.textBoxNumOfModules.TabIndex = 32
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(44, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Number of Modules"
        '
        'groupBoxRelay
        '
        Me.groupBoxRelay.Controls.Add(Me.checkedListBoxRelays)
        Me.groupBoxRelay.Location = New System.Drawing.Point(303, 19)
        Me.groupBoxRelay.Name = "groupBoxRelay"
        Me.groupBoxRelay.Size = New System.Drawing.Size(200, 362)
        Me.groupBoxRelay.TabIndex = 36
        Me.groupBoxRelay.TabStop = False
        Me.groupBoxRelay.Text = "groupBox1"
        '
        'checkedListBoxRelays
        '
        Me.checkedListBoxRelays.CheckOnClick = True
        Me.checkedListBoxRelays.ColumnWidth = 70
        Me.checkedListBoxRelays.FormattingEnabled = True
        Me.checkedListBoxRelays.Items.AddRange(New Object() {"Relay 1111   "})
        Me.checkedListBoxRelays.Location = New System.Drawing.Point(34, 35)
        Me.checkedListBoxRelays.MultiColumn = True
        Me.checkedListBoxRelays.Name = "checkedListBoxRelays"
        Me.checkedListBoxRelays.Size = New System.Drawing.Size(160, 304)
        Me.checkedListBoxRelays.TabIndex = 11
        '
        'buttonLed
        '
        Me.buttonLed.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buttonLed.Location = New System.Drawing.Point(534, 19)
        Me.buttonLed.Name = "buttonLed"
        Me.buttonLed.Size = New System.Drawing.Size(75, 23)
        Me.buttonLed.TabIndex = 37
        Me.buttonLed.Text = "Flash Led"
        Me.buttonLed.UseVisualStyleBackColor = True
        '
        'groupBoxModuleInfo
        '
        Me.groupBoxModuleInfo.Controls.Add(Me.LabelId)
        Me.groupBoxModuleInfo.Controls.Add(Me.labelModuleName)
        Me.groupBoxModuleInfo.Controls.Add(Me.labelFirwareVersion)
        Me.groupBoxModuleInfo.Controls.Add(Me.labelDriverVersion)
        Me.groupBoxModuleInfo.Controls.Add(Me.labelClassLibVersion)
        Me.groupBoxModuleInfo.Location = New System.Drawing.Point(19, 254)
        Me.groupBoxModuleInfo.Name = "groupBoxModuleInfo"
        Me.groupBoxModuleInfo.Size = New System.Drawing.Size(235, 127)
        Me.groupBoxModuleInfo.TabIndex = 38
        Me.groupBoxModuleInfo.TabStop = False
        Me.groupBoxModuleInfo.Text = "Information"
        '
        'LabelId
        '
        Me.LabelId.AutoSize = True
        Me.LabelId.Location = New System.Drawing.Point(6, 87)
        Me.LabelId.Name = "LabelId"
        Me.LabelId.Size = New System.Drawing.Size(16, 13)
        Me.LabelId.TabIndex = 7
        Me.LabelId.Text = "Id"
        '
        'labelModuleName
        '
        Me.labelModuleName.AutoSize = True
        Me.labelModuleName.Location = New System.Drawing.Point(6, 16)
        Me.labelModuleName.MinimumSize = New System.Drawing.Size(140, 0)
        Me.labelModuleName.Name = "labelModuleName"
        Me.labelModuleName.Size = New System.Drawing.Size(140, 13)
        Me.labelModuleName.TabIndex = 6
        Me.labelModuleName.Text = "Module Name"
        '
        'labelFirwareVersion
        '
        Me.labelFirwareVersion.AutoSize = True
        Me.labelFirwareVersion.Location = New System.Drawing.Point(6, 68)
        Me.labelFirwareVersion.Name = "labelFirwareVersion"
        Me.labelFirwareVersion.Size = New System.Drawing.Size(87, 13)
        Me.labelFirwareVersion.TabIndex = 5
        Me.labelFirwareVersion.Text = "Firmware Version"
        '
        'labelDriverVersion
        '
        Me.labelDriverVersion.AutoSize = True
        Me.labelDriverVersion.Location = New System.Drawing.Point(6, 55)
        Me.labelDriverVersion.Name = "labelDriverVersion"
        Me.labelDriverVersion.Size = New System.Drawing.Size(73, 13)
        Me.labelDriverVersion.TabIndex = 5
        Me.labelDriverVersion.Text = "Driver Version"
        '
        'labelClassLibVersion
        '
        Me.labelClassLibVersion.AutoSize = True
        Me.labelClassLibVersion.Location = New System.Drawing.Point(6, 42)
        Me.labelClassLibVersion.Name = "labelClassLibVersion"
        Me.labelClassLibVersion.Size = New System.Drawing.Size(58, 13)
        Me.labelClassLibVersion.TabIndex = 5
        Me.labelClassLibVersion.Text = "CL Version"
        '
        'FormVibController
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 398)
        Me.Controls.Add(Me.groupBoxModuleInfo)
        Me.Controls.Add(Me.buttonLed)
        Me.Controls.Add(Me.groupBoxRelay)
        Me.Controls.Add(Me.buttonRefresh)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.listBoxSerialNumbers)
        Me.Controls.Add(Me.textBoxNumOfModules)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormVibController"
        Me.Text = "Vibration Controller"
        Me.groupBoxRelay.ResumeLayout(False)
        Me.groupBoxModuleInfo.ResumeLayout(False)
        Me.groupBoxModuleInfo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents btnExit As Button
    Private WithEvents buttonRefresh As Button
    Private WithEvents label2 As Label
    Private WithEvents listBoxSerialNumbers As ListBox
    Private WithEvents textBoxNumOfModules As TextBox
    Private WithEvents Label3 As Label
    Private WithEvents groupBoxRelay As GroupBox
    Private WithEvents checkedListBoxRelays As CheckedListBox
    Private WithEvents buttonLed As Button
    Private WithEvents groupBoxModuleInfo As GroupBox
    Friend WithEvents LabelId As Label
    Private WithEvents labelModuleName As Label
    Private WithEvents labelFirwareVersion As Label
    Private WithEvents labelDriverVersion As Label
    Private WithEvents labelClassLibVersion As Label
End Class
