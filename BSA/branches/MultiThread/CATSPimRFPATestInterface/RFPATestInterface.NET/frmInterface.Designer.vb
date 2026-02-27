<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMain
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
    Public WithEvents TxtFixtureID As System.Windows.Forms.TextBox
	Public WithEvents TxtTesterID As System.Windows.Forms.TextBox
	Public WithEvents TxtBarcode As System.Windows.Forms.TextBox
	Public WithEvents CmdRunAll As System.Windows.Forms.Button
	Public WithEvents CmdAbort As System.Windows.Forms.Button
	Public WithEvents CmdClearResults As System.Windows.Forms.Button
    Public WithEvents LstTestStep As System.Windows.Forms.DataGridView
	Public WithEvents TxtStatus As System.Windows.Forms.TextBox
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Me.TxtFixtureID = New System.Windows.Forms.TextBox()
		Me.TxtTesterID = New System.Windows.Forms.TextBox()
		Me.TxtBarcode = New System.Windows.Forms.TextBox()
		Me.CmdRunAll = New System.Windows.Forms.Button()
		Me.CmdAbort = New System.Windows.Forms.Button()
		Me.CmdClearResults = New System.Windows.Forms.Button()
		Me.LstTestStep = New System.Windows.Forms.DataGridView()
		Me.Column1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
		Me.TestStep = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TestStepStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TxtStatus = New System.Windows.Forms.TextBox()
		Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.DataGridView1 = New System.Windows.Forms.DataGridView()
		Me.Test = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Min = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Measured = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Max = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridView2 = New System.Windows.Forms.DataGridView()
		Me.TestGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.shpLabMode = New System.Windows.Forms.Label()
		Me.labLabMode = New System.Windows.Forms.Label()
		Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuNothing = New System.Windows.Forms.ToolStripSeparator()
		Me.MnuQuit = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuProducts = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuToolsMain = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.mnuFreqBandMain = New System.Windows.Forms.ToolStripMenuItem()
		Me._mnuFreqBandCustom_0 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MnuTestProceduresMain = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuTroubleshootMain = New System.Windows.Forms.ToolStripMenuItem()
		Me._mnuTroubleshoot_0 = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuCalibrateMain = New System.Windows.Forms.ToolStripMenuItem()
		Me._mnuCalibrate_1 = New System.Windows.Forms.ToolStripMenuItem()
		Me.MnuHelpMain = New System.Windows.Forms.ToolStripMenuItem()
		Me.mnuHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.TxtPN = New System.Windows.Forms.TextBox()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.CmdCAL = New System.Windows.Forms.Button()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.tsslPCName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslUserName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslServerName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslModeName = New System.Windows.Forms.ToolStripStatusLabel()
		CType(Me.LstTestStep, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.MainMenu1.SuspendLayout()
		Me.GroupBox1.SuspendLayout()
		Me.StatusStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'TxtFixtureID
		'
		Me.TxtFixtureID.AcceptsReturn = True
		Me.TxtFixtureID.BackColor = System.Drawing.SystemColors.Window
		Me.TxtFixtureID.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtFixtureID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtFixtureID.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtFixtureID.Location = New System.Drawing.Point(265, 495)
		Me.TxtFixtureID.MaxLength = 0
		Me.TxtFixtureID.Name = "TxtFixtureID"
		Me.TxtFixtureID.ReadOnly = True
		Me.TxtFixtureID.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtFixtureID.Size = New System.Drawing.Size(89, 20)
		Me.TxtFixtureID.TabIndex = 10
		Me.TxtFixtureID.Visible = False
		'
		'TxtTesterID
		'
		Me.TxtTesterID.AcceptsReturn = True
		Me.TxtTesterID.BackColor = System.Drawing.SystemColors.Window
		Me.TxtTesterID.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtTesterID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtTesterID.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtTesterID.Location = New System.Drawing.Point(388, 495)
		Me.TxtTesterID.MaxLength = 0
		Me.TxtTesterID.Name = "TxtTesterID"
		Me.TxtTesterID.ReadOnly = True
		Me.TxtTesterID.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtTesterID.Size = New System.Drawing.Size(89, 20)
		Me.TxtTesterID.TabIndex = 9
		Me.TxtTesterID.Visible = False
		'
		'TxtBarcode
		'
		Me.TxtBarcode.AcceptsReturn = True
		Me.TxtBarcode.BackColor = System.Drawing.SystemColors.Window
		Me.TxtBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtBarcode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtBarcode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtBarcode.Location = New System.Drawing.Point(226, 26)
		Me.TxtBarcode.MaxLength = 0
		Me.TxtBarcode.Name = "TxtBarcode"
		Me.TxtBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtBarcode.Size = New System.Drawing.Size(118, 22)
		Me.TxtBarcode.TabIndex = 7
		Me.TxtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'CmdRunAll
		'
		Me.CmdRunAll.BackColor = System.Drawing.SystemColors.Control
		Me.CmdRunAll.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdRunAll.Enabled = False
		Me.CmdRunAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdRunAll.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdRunAll.Location = New System.Drawing.Point(535, 52)
		Me.CmdRunAll.Name = "CmdRunAll"
		Me.CmdRunAll.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdRunAll.Size = New System.Drawing.Size(97, 25)
		Me.CmdRunAll.TabIndex = 6
		Me.CmdRunAll.Text = "Run Test"
		Me.CmdRunAll.UseVisualStyleBackColor = False
		'
		'CmdAbort
		'
		Me.CmdAbort.BackColor = System.Drawing.SystemColors.Control
		Me.CmdAbort.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.CmdAbort.Enabled = False
		Me.CmdAbort.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdAbort.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdAbort.Location = New System.Drawing.Point(682, 52)
		Me.CmdAbort.Name = "CmdAbort"
		Me.CmdAbort.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdAbort.Size = New System.Drawing.Size(97, 25)
		Me.CmdAbort.TabIndex = 5
		Me.CmdAbort.Text = "Abort Test"
		Me.CmdAbort.UseVisualStyleBackColor = False
		'
		'CmdClearResults
		'
		Me.CmdClearResults.BackColor = System.Drawing.SystemColors.Control
		Me.CmdClearResults.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdClearResults.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdClearResults.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdClearResults.Location = New System.Drawing.Point(497, 493)
		Me.CmdClearResults.Name = "CmdClearResults"
		Me.CmdClearResults.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdClearResults.Size = New System.Drawing.Size(97, 25)
		Me.CmdClearResults.TabIndex = 4
		Me.CmdClearResults.Text = "Clear Results"
		Me.CmdClearResults.UseVisualStyleBackColor = False
		'
		'LstTestStep
		'
		Me.LstTestStep.AllowUserToAddRows = False
		Me.LstTestStep.AllowUserToDeleteRows = False
		Me.LstTestStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.LstTestStep.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.TestStep, Me.TestStepStatus})
		Me.LstTestStep.Location = New System.Drawing.Point(12, 97)
		Me.LstTestStep.MultiSelect = False
		Me.LstTestStep.Name = "LstTestStep"
		Me.LstTestStep.RowHeadersVisible = False
		Me.LstTestStep.RowTemplate.Height = 18
		Me.LstTestStep.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
		Me.LstTestStep.Size = New System.Drawing.Size(142, 362)
		Me.LstTestStep.TabIndex = 18
		'
		'Column1
		'
		Me.Column1.HeaderText = ""
		Me.Column1.Name = "Column1"
		Me.Column1.Width = 20
		'
		'TestStep
		'
		Me.TestStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		Me.TestStep.DefaultCellStyle = DataGridViewCellStyle1
		Me.TestStep.HeaderText = "Test Step"
		Me.TestStep.Name = "TestStep"
		Me.TestStep.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
		Me.TestStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'TestStepStatus
		'
		Me.TestStepStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.TestStepStatus.DefaultCellStyle = DataGridViewCellStyle2
		Me.TestStepStatus.HeaderText = "Status"
		Me.TestStepStatus.Name = "TestStepStatus"
		Me.TestStepStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.TestStepStatus.Width = 48
		'
		'TxtStatus
		'
		Me.TxtStatus.AcceptsReturn = True
		Me.TxtStatus.BackColor = System.Drawing.SystemColors.Window
		Me.TxtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtStatus.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtStatus.Location = New System.Drawing.Point(12, 465)
		Me.TxtStatus.MaxLength = 0
		Me.TxtStatus.Multiline = True
		Me.TxtStatus.Name = "TxtStatus"
		Me.TxtStatus.ReadOnly = True
		Me.TxtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.TxtStatus.Size = New System.Drawing.Size(773, 82)
		Me.TxtStatus.TabIndex = 0
		'
		'Label2
		'
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Location = New System.Drawing.Point(191, 498)
		Me.Label2.Name = "Label2"
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.Size = New System.Drawing.Size(57, 17)
		Me.Label2.TabIndex = 12
		Me.Label2.Text = "Fixture ID"
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label2.Visible = False
		'
		'Label3
		'
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Location = New System.Drawing.Point(289, 495)
		Me.Label3.Name = "Label3"
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.Size = New System.Drawing.Size(65, 17)
		Me.Label3.TabIndex = 11
		Me.Label3.Text = "Operator"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Visible = False
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Location = New System.Drawing.Point(202, 30)
		Me.Label1.Name = "Label1"
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.Size = New System.Drawing.Size(21, 14)
		Me.Label1.TabIndex = 8
		Me.Label1.Text = "SN"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.AllowUserToDeleteRows = False
		Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Test, Me.Min, Me.Measured, Me.Max, Me.Status})
		Me.DataGridView1.Location = New System.Drawing.Point(375, 97)
		Me.DataGridView1.MultiSelect = False
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.ReadOnly = True
		Me.DataGridView1.RowHeadersVisible = False
		Me.DataGridView1.RowTemplate.Height = 18
		Me.DataGridView1.RowTemplate.ReadOnly = True
		Me.DataGridView1.Size = New System.Drawing.Size(410, 362)
		Me.DataGridView1.TabIndex = 14
		'
		'Test
		'
		Me.Test.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
		DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
		DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White
		DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
		Me.Test.DefaultCellStyle = DataGridViewCellStyle3
		Me.Test.HeaderText = "Test Item"
		Me.Test.Name = "Test"
		Me.Test.ReadOnly = True
		Me.Test.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'Min
		'
		Me.Min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Min.DefaultCellStyle = DataGridViewCellStyle4
		Me.Min.HeaderText = "Min"
		Me.Min.Name = "Min"
		Me.Min.ReadOnly = True
		Me.Min.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Min.Width = 33
		'
		'Measured
		'
		Me.Measured.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Measured.DefaultCellStyle = DataGridViewCellStyle5
		Me.Measured.HeaderText = "Meas"
		Me.Measured.Name = "Measured"
		Me.Measured.ReadOnly = True
		Me.Measured.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Measured.Width = 43
		'
		'Max
		'
		Me.Max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Max.DefaultCellStyle = DataGridViewCellStyle6
		Me.Max.HeaderText = "Max"
		Me.Max.Name = "Max"
		Me.Max.ReadOnly = True
		Me.Max.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Max.Width = 35
		'
		'Status
		'
		Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.Status.DefaultCellStyle = DataGridViewCellStyle7
		Me.Status.HeaderText = "Status"
		Me.Status.Name = "Status"
		Me.Status.ReadOnly = True
		Me.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Status.Width = 48
		'
		'DataGridView2
		'
		Me.DataGridView2.AllowUserToAddRows = False
		Me.DataGridView2.AllowUserToDeleteRows = False
		Me.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TestGroup, Me.DataGridViewTextBoxColumn1})
		Me.DataGridView2.Location = New System.Drawing.Point(160, 97)
		Me.DataGridView2.MultiSelect = False
		Me.DataGridView2.Name = "DataGridView2"
		Me.DataGridView2.ReadOnly = True
		Me.DataGridView2.RowHeadersVisible = False
		Me.DataGridView2.RowTemplate.Height = 18
		Me.DataGridView2.RowTemplate.ReadOnly = True
		Me.DataGridView2.Size = New System.Drawing.Size(209, 362)
		Me.DataGridView2.TabIndex = 15
		'
		'TestGroup
		'
		Me.TestGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
		DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black
		DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White
		DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
		Me.TestGroup.DefaultCellStyle = DataGridViewCellStyle8
		Me.TestGroup.HeaderText = "Test Group"
		Me.TestGroup.Name = "TestGroup"
		Me.TestGroup.ReadOnly = True
		Me.TestGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle9
		Me.DataGridViewTextBoxColumn1.HeaderText = "Status"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		Me.DataGridViewTextBoxColumn1.ReadOnly = True
		Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.DataGridViewTextBoxColumn1.Width = 48
		'
		'shpLabMode
		'
		Me.shpLabMode.BackColor = System.Drawing.Color.Red
		Me.shpLabMode.Location = New System.Drawing.Point(254, 484)
		Me.shpLabMode.Name = "shpLabMode"
		Me.shpLabMode.Size = New System.Drawing.Size(115, 39)
		Me.shpLabMode.TabIndex = 16
		Me.shpLabMode.Visible = False
		'
		'labLabMode
		'
		Me.labLabMode.BackColor = System.Drawing.Color.White
		Me.labLabMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.labLabMode.ForeColor = System.Drawing.Color.Black
		Me.labLabMode.Location = New System.Drawing.Point(497, 484)
		Me.labLabMode.Name = "labLabMode"
		Me.labLabMode.Size = New System.Drawing.Size(226, 42)
		Me.labLabMode.TabIndex = 17
		Me.labLabMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.labLabMode.Visible = False
		'
		'mnuFile
		'
		Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNothing, Me.MnuQuit})
		Me.mnuFile.Name = "mnuFile"
		Me.mnuFile.Size = New System.Drawing.Size(37, 20)
		Me.mnuFile.Text = "&File"
		'
		'mnuNothing
		'
		Me.mnuNothing.Name = "mnuNothing"
		Me.mnuNothing.Size = New System.Drawing.Size(94, 6)
		'
		'MnuQuit
		'
		Me.MnuQuit.Name = "MnuQuit"
		Me.MnuQuit.Size = New System.Drawing.Size(97, 22)
		Me.MnuQuit.Text = "&Quit"
		'
		'mnuProducts
		'
		Me.mnuProducts.Name = "mnuProducts"
		Me.mnuProducts.Size = New System.Drawing.Size(66, 20)
		Me.mnuProducts.Text = "&Products"
		Me.mnuProducts.Visible = False
		'
		'mnuToolsMain
		'
		Me.mnuToolsMain.Name = "mnuToolsMain"
		Me.mnuToolsMain.Size = New System.Drawing.Size(48, 20)
		Me.mnuToolsMain.Text = "T&ools"
		'
		'ToolStripSeparator1
		'
		Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
		Me.ToolStripSeparator1.Size = New System.Drawing.Size(157, 6)
		'
		'mnuFreqBandMain
		'
		Me.mnuFreqBandMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._mnuFreqBandCustom_0})
		Me.mnuFreqBandMain.Name = "mnuFreqBandMain"
		Me.mnuFreqBandMain.Size = New System.Drawing.Size(46, 20)
		Me.mnuFreqBandMain.Text = "&Band"
		Me.mnuFreqBandMain.Visible = False
		'
		'_mnuFreqBandCustom_0
		'
		Me._mnuFreqBandCustom_0.Name = "_mnuFreqBandCustom_0"
		Me._mnuFreqBandCustom_0.Size = New System.Drawing.Size(116, 22)
		Me._mnuFreqBandCustom_0.Text = "Custom"
		'
		'MnuTestProceduresMain
		'
		Me.MnuTestProceduresMain.Name = "MnuTestProceduresMain"
		Me.MnuTestProceduresMain.Size = New System.Drawing.Size(103, 20)
		Me.MnuTestProceduresMain.Text = "&Test Procedures"
		Me.MnuTestProceduresMain.Visible = False
		'
		'mnuTroubleshootMain
		'
		Me.mnuTroubleshootMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._mnuTroubleshoot_0})
		Me.mnuTroubleshootMain.Name = "mnuTroubleshootMain"
		Me.mnuTroubleshootMain.Size = New System.Drawing.Size(90, 20)
		Me.mnuTroubleshootMain.Text = "Troubleshoot"
		Me.mnuTroubleshootMain.Visible = False
		'
		'_mnuTroubleshoot_0
		'
		Me._mnuTroubleshoot_0.Name = "_mnuTroubleshoot_0"
		Me._mnuTroubleshoot_0.Size = New System.Drawing.Size(143, 22)
		Me._mnuTroubleshoot_0.Text = "ATP Self Test"
		Me._mnuTroubleshoot_0.Visible = False
		'
		'mnuCalibrateMain
		'
		Me.mnuCalibrateMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._mnuCalibrate_1})
		Me.mnuCalibrateMain.Name = "mnuCalibrateMain"
		Me.mnuCalibrateMain.Size = New System.Drawing.Size(66, 20)
		Me.mnuCalibrateMain.Text = "&Calibrate"
		Me.mnuCalibrateMain.Visible = False
		'
		'_mnuCalibrate_1
		'
		Me._mnuCalibrate_1.Name = "_mnuCalibrate_1"
		Me._mnuCalibrate_1.Size = New System.Drawing.Size(204, 22)
		Me._mnuCalibrate_1.Text = "Power Meter Sensors Cal"
		Me._mnuCalibrate_1.Visible = False
		'
		'MnuHelpMain
		'
		Me.MnuHelpMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpAbout})
		Me.MnuHelpMain.Name = "MnuHelpMain"
		Me.MnuHelpMain.Size = New System.Drawing.Size(44, 20)
		Me.MnuHelpMain.Text = "&Help"
		'
		'mnuHelpAbout
		'
		Me.mnuHelpAbout.Name = "mnuHelpAbout"
		Me.mnuHelpAbout.Size = New System.Drawing.Size(107, 22)
		Me.mnuHelpAbout.Text = "About"
		'
		'MainMenu1
		'
		Me.MainMenu1.BackColor = System.Drawing.SystemColors.Control
		Me.MainMenu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuProducts, Me.mnuToolsMain, Me.mnuFreqBandMain, Me.MnuTestProceduresMain, Me.mnuTroubleshootMain, Me.mnuCalibrateMain, Me.MnuHelpMain})
		Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
		Me.MainMenu1.Name = "MainMenu1"
		Me.MainMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
		Me.MainMenu1.Size = New System.Drawing.Size(798, 24)
		Me.MainMenu1.TabIndex = 13
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Location = New System.Drawing.Point(9, 30)
		Me.Label4.Name = "Label4"
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.Size = New System.Drawing.Size(20, 14)
		Me.Label4.TabIndex = 19
		Me.Label4.Text = "PN"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'TxtPN
		'
		Me.TxtPN.AcceptsReturn = True
		Me.TxtPN.BackColor = System.Drawing.SystemColors.Window
		Me.TxtPN.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtPN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtPN.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtPN.Location = New System.Drawing.Point(32, 26)
		Me.TxtPN.MaxLength = 0
		Me.TxtPN.Name = "TxtPN"
		Me.TxtPN.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtPN.Size = New System.Drawing.Size(156, 22)
		Me.TxtPN.TabIndex = 20
		Me.TxtPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.TxtPN)
		Me.GroupBox1.Controls.Add(Me.Label4)
		Me.GroupBox1.Controls.Add(Me.TxtBarcode)
		Me.GroupBox1.Controls.Add(Me.Label1)
		Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(357, 64)
		Me.GroupBox1.TabIndex = 21
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Unit Info"
		'
		'CmdCAL
		'
		Me.CmdCAL.BackColor = System.Drawing.SystemColors.Control
		Me.CmdCAL.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdCAL.Enabled = False
		Me.CmdCAL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdCAL.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdCAL.Location = New System.Drawing.Point(388, 52)
		Me.CmdCAL.Name = "CmdCAL"
		Me.CmdCAL.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdCAL.Size = New System.Drawing.Size(97, 25)
		Me.CmdCAL.TabIndex = 22
		Me.CmdCAL.Text = "Calibrate"
		Me.CmdCAL.UseVisualStyleBackColor = False
		Me.CmdCAL.Visible = False
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPCName, Me.tsslUserName, Me.tsslServerName, Me.tsslModeName})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 550)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(798, 22)
		Me.StatusStrip1.TabIndex = 23
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'tsslPCName
		'
		Me.tsslPCName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslPCName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslPCName.Name = "tsslPCName"
		Me.tsslPCName.Size = New System.Drawing.Size(195, 17)
		Me.tsslPCName.Spring = True
		'
		'tsslUserName
		'
		Me.tsslUserName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslUserName.Name = "tsslUserName"
		Me.tsslUserName.Size = New System.Drawing.Size(195, 17)
		Me.tsslUserName.Spring = True
		'
		'tsslServerName
		'
		Me.tsslServerName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslServerName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslServerName.Name = "tsslServerName"
		Me.tsslServerName.Size = New System.Drawing.Size(195, 17)
		Me.tsslServerName.Spring = True
		'
		'tsslModeName
		'
		Me.tsslModeName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslModeName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslModeName.Name = "tsslModeName"
		Me.tsslModeName.Size = New System.Drawing.Size(195, 17)
		Me.tsslModeName.Spring = True
		'
		'frmMain
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.ClientSize = New System.Drawing.Size(798, 572)
		Me.Controls.Add(Me.StatusStrip1)
		Me.Controls.Add(Me.CmdCAL)
		Me.Controls.Add(Me.GroupBox1)
		Me.Controls.Add(Me.TxtStatus)
		Me.Controls.Add(Me.labLabMode)
		Me.Controls.Add(Me.DataGridView2)
		Me.Controls.Add(Me.DataGridView1)
		Me.Controls.Add(Me.TxtFixtureID)
		Me.Controls.Add(Me.TxtTesterID)
		Me.Controls.Add(Me.CmdRunAll)
		Me.Controls.Add(Me.CmdAbort)
		Me.Controls.Add(Me.CmdClearResults)
		Me.Controls.Add(Me.LstTestStep)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.MainMenu1)
		Me.Controls.Add(Me.shpLabMode)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ForeColor = System.Drawing.SystemColors.WindowText
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Location = New System.Drawing.Point(60, 65)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "frmMain"
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Product Test Name"
		CType(Me.LstTestStep, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.MainMenu1.ResumeLayout(False)
		Me.MainMenu1.PerformLayout()
		Me.GroupBox1.ResumeLayout(False)
		Me.GroupBox1.PerformLayout()
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents shpLabMode As System.Windows.Forms.Label
    Friend WithEvents labLabMode As System.Windows.Forms.Label
    Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
  Public WithEvents mnuNothing As System.Windows.Forms.ToolStripSeparator
    Public WithEvents MnuQuit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProducts As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuToolsMain As System.Windows.Forms.ToolStripMenuItem
  'Friend WithEvents mnuDiagLoop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuFreqBandMain As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _mnuFreqBandCustom_0 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuTestProceduresMain As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuTroubleshootMain As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _mnuTroubleshoot_0 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuCalibrateMain As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents _mnuCalibrate_1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuHelpMain As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents TxtPN As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents Test As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Min As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Measured As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Max As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents TestGroup As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Public WithEvents CmdCAL As System.Windows.Forms.Button
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents tsslPCName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslUserName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslServerName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslModeName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents Column1 As DataGridViewCheckBoxColumn
	Friend WithEvents TestStep As DataGridViewTextBoxColumn
	Friend WithEvents TestStepStatus As DataGridViewTextBoxColumn
#End Region
End Class