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
	Public WithEvents CmdRunAll As System.Windows.Forms.Button
	Public WithEvents CmdAbort As System.Windows.Forms.Button
	Public WithEvents CmdClearResults As System.Windows.Forms.Button
	Public WithEvents LstTestStep As System.Windows.Forms.DataGridView
	Public WithEvents TxtStatus As System.Windows.Forms.TextBox
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
		Me.TxtFixtureID = New System.Windows.Forms.TextBox()
		Me.TxtTesterID = New System.Windows.Forms.TextBox()
		Me.CmdRunAll = New System.Windows.Forms.Button()
		Me.CmdAbort = New System.Windows.Forms.Button()
		Me.CmdClearResults = New System.Windows.Forms.Button()
		Me.LstTestStep = New System.Windows.Forms.DataGridView()
		Me.TestStep = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TestStepStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.TxtStatus = New System.Windows.Forms.TextBox()
		Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
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
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.tsslPCName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslUserName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslServerName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsslModeName = New System.Windows.Forms.ToolStripStatusLabel()
		Me.tsTestOptions = New System.Windows.Forms.ToolStrip()
		Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
		Me.TSDP_Mode = New System.Windows.Forms.ToolStripDropDownButton()
		Me.PPPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
		Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
		Me.TSDP_PhaseStation = New System.Windows.Forms.ToolStripDropDownButton()
		Me.TxtPN = New System.Windows.Forms.TextBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.TxtBarcode = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.imgPhaseStation = New System.Windows.Forms.ImageList(Me.components)
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.lblTestTime = New System.Windows.Forms.Label()
		CType(Me.LstTestStep, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.MainMenu1.SuspendLayout()
		Me.StatusStrip1.SuspendLayout()
		Me.tsTestOptions.SuspendLayout()
		Me.GroupBox2.SuspendLayout()
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
		'CmdRunAll
		'
		Me.CmdRunAll.BackColor = System.Drawing.SystemColors.Control
		Me.CmdRunAll.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdRunAll.Enabled = False
		Me.CmdRunAll.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdRunAll.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdRunAll.Location = New System.Drawing.Point(473, 63)
		Me.CmdRunAll.Name = "CmdRunAll"
		Me.CmdRunAll.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdRunAll.Size = New System.Drawing.Size(91, 25)
		Me.CmdRunAll.TabIndex = 6
		Me.CmdRunAll.Text = "Run Test"
		Me.CmdRunAll.UseVisualStyleBackColor = True
		'
		'CmdAbort
		'
		Me.CmdAbort.BackColor = System.Drawing.SystemColors.Control
		Me.CmdAbort.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.CmdAbort.Enabled = False
		Me.CmdAbort.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdAbort.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdAbort.Location = New System.Drawing.Point(582, 63)
		Me.CmdAbort.Name = "CmdAbort"
		Me.CmdAbort.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdAbort.Size = New System.Drawing.Size(91, 25)
		Me.CmdAbort.TabIndex = 5
		Me.CmdAbort.Text = "Abort Test"
		Me.CmdAbort.UseVisualStyleBackColor = True
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
		DataGridViewCellStyle16.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstTestStep.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle16
		Me.LstTestStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.LstTestStep.ColumnHeadersHeight = 22
		Me.LstTestStep.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TestStep, Me.TestStepStatus})
		Me.LstTestStep.Location = New System.Drawing.Point(12, 103)
		Me.LstTestStep.MultiSelect = False
		Me.LstTestStep.Name = "LstTestStep"
		Me.LstTestStep.ReadOnly = True
		Me.LstTestStep.RowHeadersVisible = False
		DataGridViewCellStyle19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstTestStep.RowsDefaultCellStyle = DataGridViewCellStyle19
		Me.LstTestStep.RowTemplate.Height = 18
		Me.LstTestStep.Size = New System.Drawing.Size(215, 216)
		Me.LstTestStep.TabIndex = 18
		'
		'TestStep
		'
		Me.TestStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		Me.TestStep.DefaultCellStyle = DataGridViewCellStyle17
		Me.TestStep.HeaderText = "Test Step"
		Me.TestStep.Name = "TestStep"
		Me.TestStep.ReadOnly = True
		Me.TestStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'TestStepStatus
		'
		Me.TestStepStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.TestStepStatus.DefaultCellStyle = DataGridViewCellStyle18
		Me.TestStepStatus.HeaderText = "Status"
		Me.TestStepStatus.Name = "TestStepStatus"
		Me.TestStepStatus.ReadOnly = True
		Me.TestStepStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.TestStepStatus.Width = 48
		'
		'TxtStatus
		'
		Me.TxtStatus.AcceptsReturn = True
		Me.TxtStatus.BackColor = System.Drawing.SystemColors.Window
		Me.TxtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtStatus.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtStatus.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtStatus.Location = New System.Drawing.Point(12, 575)
		Me.TxtStatus.MaxLength = 0
		Me.TxtStatus.Multiline = True
		Me.TxtStatus.Name = "TxtStatus"
		Me.TxtStatus.ReadOnly = True
		Me.TxtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.TxtStatus.Size = New System.Drawing.Size(812, 82)
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
		'DataGridView1
		'
		Me.DataGridView1.AllowUserToAddRows = False
		Me.DataGridView1.AllowUserToDeleteRows = False
		DataGridViewCellStyle20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle20
		Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Test, Me.Min, Me.Measured, Me.Max, Me.Status})
		Me.DataGridView1.Location = New System.Drawing.Point(240, 103)
		Me.DataGridView1.MultiSelect = False
		Me.DataGridView1.Name = "DataGridView1"
		Me.DataGridView1.ReadOnly = True
		Me.DataGridView1.RowHeadersVisible = False
		DataGridViewCellStyle26.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle26
		Me.DataGridView1.RowTemplate.Height = 18
		Me.DataGridView1.RowTemplate.ReadOnly = True
		Me.DataGridView1.Size = New System.Drawing.Size(584, 466)
		Me.DataGridView1.TabIndex = 14
		'
		'Test
		'
		Me.Test.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle21.BackColor = System.Drawing.Color.White
		DataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black
		DataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.White
		DataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black
		Me.Test.DefaultCellStyle = DataGridViewCellStyle21
		Me.Test.HeaderText = "Test Item"
		Me.Test.Name = "Test"
		Me.Test.ReadOnly = True
		Me.Test.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'Min
		'
		Me.Min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Min.DefaultCellStyle = DataGridViewCellStyle22
		Me.Min.HeaderText = "Min"
		Me.Min.Name = "Min"
		Me.Min.ReadOnly = True
		Me.Min.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Min.Width = 33
		'
		'Measured
		'
		Me.Measured.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Measured.DefaultCellStyle = DataGridViewCellStyle23
		Me.Measured.HeaderText = "Meas"
		Me.Measured.Name = "Measured"
		Me.Measured.ReadOnly = True
		Me.Measured.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Measured.Width = 43
		'
		'Max
		'
		Me.Max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
		Me.Max.DefaultCellStyle = DataGridViewCellStyle24
		Me.Max.HeaderText = "Max"
		Me.Max.Name = "Max"
		Me.Max.ReadOnly = True
		Me.Max.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		Me.Max.Width = 35
		'
		'Status
		'
		Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.Status.DefaultCellStyle = DataGridViewCellStyle25
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
		DataGridViewCellStyle27.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle27
		Me.DataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TestGroup, Me.DataGridViewTextBoxColumn1})
		Me.DataGridView2.Location = New System.Drawing.Point(12, 325)
		Me.DataGridView2.MultiSelect = False
		Me.DataGridView2.Name = "DataGridView2"
		Me.DataGridView2.ReadOnly = True
		Me.DataGridView2.RowHeadersVisible = False
		DataGridViewCellStyle30.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.DataGridView2.RowsDefaultCellStyle = DataGridViewCellStyle30
		Me.DataGridView2.RowTemplate.Height = 18
		Me.DataGridView2.RowTemplate.ReadOnly = True
		Me.DataGridView2.Size = New System.Drawing.Size(215, 244)
		Me.DataGridView2.TabIndex = 15
		'
		'TestGroup
		'
		Me.TestGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
		DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
		DataGridViewCellStyle28.BackColor = System.Drawing.Color.White
		DataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black
		DataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.White
		DataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black
		Me.TestGroup.DefaultCellStyle = DataGridViewCellStyle28
		Me.TestGroup.HeaderText = "Test Group"
		Me.TestGroup.Name = "TestGroup"
		Me.TestGroup.ReadOnly = True
		Me.TestGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
		DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
		Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle29
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
		Me.mnuFile.Size = New System.Drawing.Size(40, 21)
		Me.mnuFile.Text = "&File"
		'
		'mnuNothing
		'
		Me.mnuNothing.Name = "mnuNothing"
		Me.mnuNothing.Size = New System.Drawing.Size(98, 6)
		'
		'MnuQuit
		'
		Me.MnuQuit.Name = "MnuQuit"
		Me.MnuQuit.Size = New System.Drawing.Size(101, 22)
		Me.MnuQuit.Text = "&Quit"
		'
		'mnuProducts
		'
		Me.mnuProducts.Name = "mnuProducts"
		Me.mnuProducts.Size = New System.Drawing.Size(72, 21)
		Me.mnuProducts.Text = "&Products"
		Me.mnuProducts.Visible = False
		'
		'mnuToolsMain
		'
		Me.mnuToolsMain.Name = "mnuToolsMain"
		Me.mnuToolsMain.Size = New System.Drawing.Size(49, 21)
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
		Me.mnuFreqBandMain.Size = New System.Drawing.Size(50, 21)
		Me.mnuFreqBandMain.Text = "&Band"
		Me.mnuFreqBandMain.Visible = False
		'
		'_mnuFreqBandCustom_0
		'
		Me._mnuFreqBandCustom_0.Name = "_mnuFreqBandCustom_0"
		Me._mnuFreqBandCustom_0.Size = New System.Drawing.Size(121, 22)
		Me._mnuFreqBandCustom_0.Text = "Custom"
		'
		'MnuTestProceduresMain
		'
		Me.MnuTestProceduresMain.Name = "MnuTestProceduresMain"
		Me.MnuTestProceduresMain.Size = New System.Drawing.Size(113, 21)
		Me.MnuTestProceduresMain.Text = "&Test Procedures"
		Me.MnuTestProceduresMain.Visible = False
		'
		'mnuTroubleshootMain
		'
		Me.mnuTroubleshootMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._mnuTroubleshoot_0})
		Me.mnuTroubleshootMain.Name = "mnuTroubleshootMain"
		Me.mnuTroubleshootMain.Size = New System.Drawing.Size(96, 21)
		Me.mnuTroubleshootMain.Text = "Troubleshoot"
		Me.mnuTroubleshootMain.Visible = False
		'
		'_mnuTroubleshoot_0
		'
		Me._mnuTroubleshoot_0.Name = "_mnuTroubleshoot_0"
		Me._mnuTroubleshoot_0.Size = New System.Drawing.Size(148, 22)
		Me._mnuTroubleshoot_0.Text = "ATP Self Test"
		Me._mnuTroubleshoot_0.Visible = False
		'
		'mnuCalibrateMain
		'
		Me.mnuCalibrateMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me._mnuCalibrate_1})
		Me.mnuCalibrateMain.Name = "mnuCalibrateMain"
		Me.mnuCalibrateMain.Size = New System.Drawing.Size(76, 21)
		Me.mnuCalibrateMain.Text = "&Calibrate"
		Me.mnuCalibrateMain.Visible = False
		'
		'_mnuCalibrate_1
		'
		Me._mnuCalibrate_1.Name = "_mnuCalibrate_1"
		Me._mnuCalibrate_1.Size = New System.Drawing.Size(222, 22)
		Me._mnuCalibrate_1.Text = "Power Meter Sensors Cal"
		Me._mnuCalibrate_1.Visible = False
		'
		'MnuHelpMain
		'
		Me.MnuHelpMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelpAbout})
		Me.MnuHelpMain.Name = "MnuHelpMain"
		Me.MnuHelpMain.Size = New System.Drawing.Size(47, 21)
		Me.MnuHelpMain.Text = "&Help"
		'
		'mnuHelpAbout
		'
		Me.mnuHelpAbout.Name = "mnuHelpAbout"
		Me.mnuHelpAbout.Size = New System.Drawing.Size(113, 22)
		Me.mnuHelpAbout.Text = "About"
		'
		'MainMenu1
		'
		Me.MainMenu1.BackColor = System.Drawing.SystemColors.Control
		Me.MainMenu1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.MainMenu1.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuProducts, Me.mnuToolsMain, Me.mnuFreqBandMain, Me.MnuTestProceduresMain, Me.mnuTroubleshootMain, Me.mnuCalibrateMain, Me.MnuHelpMain})
		Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
		Me.MainMenu1.Name = "MainMenu1"
		Me.MainMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
		Me.MainMenu1.Size = New System.Drawing.Size(836, 25)
		Me.MainMenu1.TabIndex = 13
		'
		'GroupBox1
		'
		Me.GroupBox1.Location = New System.Drawing.Point(220, 337)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(416, 50)
		Me.GroupBox1.TabIndex = 21
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Unit Info"
		Me.GroupBox1.Visible = False
		'
		'StatusStrip1
		'
		Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPCName, Me.tsslUserName, Me.tsslServerName, Me.tsslModeName})
		Me.StatusStrip1.Location = New System.Drawing.Point(0, 660)
		Me.StatusStrip1.Name = "StatusStrip1"
		Me.StatusStrip1.Size = New System.Drawing.Size(836, 22)
		Me.StatusStrip1.TabIndex = 23
		Me.StatusStrip1.Text = "StatusStrip1"
		'
		'tsslPCName
		'
		Me.tsslPCName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslPCName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslPCName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tsslPCName.Name = "tsslPCName"
		Me.tsslPCName.Size = New System.Drawing.Size(205, 17)
		Me.tsslPCName.Spring = True
		'
		'tsslUserName
		'
		Me.tsslUserName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslUserName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tsslUserName.Name = "tsslUserName"
		Me.tsslUserName.Size = New System.Drawing.Size(205, 17)
		Me.tsslUserName.Spring = True
		'
		'tsslServerName
		'
		Me.tsslServerName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslServerName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslServerName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tsslServerName.Name = "tsslServerName"
		Me.tsslServerName.Size = New System.Drawing.Size(205, 17)
		Me.tsslServerName.Spring = True
		'
		'tsslModeName
		'
		Me.tsslModeName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
			Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
		Me.tsslModeName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
		Me.tsslModeName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tsslModeName.Name = "tsslModeName"
		Me.tsslModeName.Size = New System.Drawing.Size(205, 17)
		Me.tsslModeName.Spring = True
		'
		'tsTestOptions
		'
		Me.tsTestOptions.Dock = System.Windows.Forms.DockStyle.None
		Me.tsTestOptions.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.tsTestOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.TSDP_Mode, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.TSDP_PhaseStation})
		Me.tsTestOptions.Location = New System.Drawing.Point(12, 24)
		Me.tsTestOptions.Name = "tsTestOptions"
		Me.tsTestOptions.Size = New System.Drawing.Size(356, 32)
		Me.tsTestOptions.Stretch = True
		Me.tsTestOptions.TabIndex = 0
		Me.tsTestOptions.Text = "ToolStrip1"
		Me.tsTestOptions.Visible = False
		'
		'ToolStripLabel2
		'
		Me.ToolStripLabel2.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ToolStripLabel2.Name = "ToolStripLabel2"
		Me.ToolStripLabel2.Size = New System.Drawing.Size(56, 29)
		Me.ToolStripLabel2.Text = "Mode:"
		'
		'TSDP_Mode
		'
		Me.TSDP_Mode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.TSDP_Mode.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PPPToolStripMenuItem})
		Me.TSDP_Mode.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TSDP_Mode.ForeColor = System.Drawing.Color.Blue
		Me.TSDP_Mode.Image = CType(resources.GetObject("TSDP_Mode.Image"), System.Drawing.Image)
		Me.TSDP_Mode.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.TSDP_Mode.Name = "TSDP_Mode"
		Me.TSDP_Mode.Size = New System.Drawing.Size(82, 29)
		Me.TSDP_Mode.Text = "PROD"
		'
		'PPPToolStripMenuItem
		'
		Me.PPPToolStripMenuItem.Name = "PPPToolStripMenuItem"
		Me.PPPToolStripMenuItem.Size = New System.Drawing.Size(120, 30)
		Me.PPPToolStripMenuItem.Text = "PPP"
		'
		'ToolStripSeparator2
		'
		Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
		Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
		'
		'ToolStripLabel1
		'
		Me.ToolStripLabel1.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ToolStripLabel1.Name = "ToolStripLabel1"
		Me.ToolStripLabel1.Size = New System.Drawing.Size(91, 29)
		Me.ToolStripLabel1.Text = "Test Station:"
		'
		'TSDP_PhaseStation
		'
		Me.TSDP_PhaseStation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.TSDP_PhaseStation.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TSDP_PhaseStation.ForeColor = System.Drawing.Color.Blue
		Me.TSDP_PhaseStation.Image = CType(resources.GetObject("TSDP_PhaseStation.Image"), System.Drawing.Image)
		Me.TSDP_PhaseStation.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.TSDP_PhaseStation.Name = "TSDP_PhaseStation"
		Me.TSDP_PhaseStation.Size = New System.Drawing.Size(109, 29)
		Me.TSDP_PhaseStation.Text = "FinalTest"
		'
		'TxtPN
		'
		Me.TxtPN.AcceptsReturn = True
		Me.TxtPN.BackColor = System.Drawing.SystemColors.Window
		Me.TxtPN.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtPN.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtPN.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtPN.Location = New System.Drawing.Point(40, 63)
		Me.TxtPN.MaxLength = 0
		Me.TxtPN.Name = "TxtPN"
		Me.TxtPN.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtPN.Size = New System.Drawing.Size(190, 26)
		Me.TxtPN.TabIndex = 27
		Me.TxtPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Location = New System.Drawing.Point(12, 67)
		Me.Label4.Name = "Label4"
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.Size = New System.Drawing.Size(25, 16)
		Me.Label4.TabIndex = 26
		Me.Label4.Text = "PN"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'TxtBarcode
		'
		Me.TxtBarcode.AcceptsReturn = True
		Me.TxtBarcode.BackColor = System.Drawing.SystemColors.Window
		Me.TxtBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.TxtBarcode.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TxtBarcode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.TxtBarcode.Location = New System.Drawing.Point(261, 63)
		Me.TxtBarcode.MaxLength = 0
		Me.TxtBarcode.Name = "TxtBarcode"
		Me.TxtBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TxtBarcode.Size = New System.Drawing.Size(193, 26)
		Me.TxtBarcode.TabIndex = 24
		Me.TxtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.Font = New System.Drawing.Font("Century Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Location = New System.Drawing.Point(233, 67)
		Me.Label1.Name = "Label1"
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.Size = New System.Drawing.Size(25, 16)
		Me.Label1.TabIndex = 25
		Me.Label1.Text = "SN"
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'imgPhaseStation
		'
		Me.imgPhaseStation.ImageStream = CType(resources.GetObject("imgPhaseStation.ImageStream"), System.Windows.Forms.ImageListStreamer)
		Me.imgPhaseStation.TransparentColor = System.Drawing.Color.Transparent
		Me.imgPhaseStation.Images.SetKeyName(0, "database_16px_535651_easyicon.net.png")
		Me.imgPhaseStation.Images.SetKeyName(1, "database_accept_16px_535385_easyicon.net.png")
		'
		'GroupBox2
		'
		Me.GroupBox2.Controls.Add(Me.lblTestTime)
		Me.GroupBox2.Controls.Add(Me.Label5)
		Me.GroupBox2.Location = New System.Drawing.Point(691, 24)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(133, 65)
		Me.GroupBox2.TabIndex = 28
		Me.GroupBox2.TabStop = False
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Location = New System.Drawing.Point(6, 16)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(99, 16)
		Me.Label5.TabIndex = 0
		Me.Label5.Text = "Test Time (Sec):"
		'
		'lblTestTime
		'
		Me.lblTestTime.AutoSize = True
		Me.lblTestTime.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblTestTime.Location = New System.Drawing.Point(46, 39)
		Me.lblTestTime.Name = "lblTestTime"
		Me.lblTestTime.Size = New System.Drawing.Size(25, 16)
		Me.lblTestTime.TabIndex = 1
		Me.lblTestTime.Text = "0.0"
		'
		'frmMain
		'
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.ClientSize = New System.Drawing.Size(836, 682)
		Me.Controls.Add(Me.GroupBox2)
		Me.Controls.Add(Me.tsTestOptions)
		Me.Controls.Add(Me.TxtPN)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.TxtBarcode)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.StatusStrip1)
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
		Me.StatusStrip1.ResumeLayout(False)
		Me.StatusStrip1.PerformLayout()
		Me.tsTestOptions.ResumeLayout(False)
		Me.tsTestOptions.PerformLayout()
		Me.GroupBox2.ResumeLayout(False)
		Me.GroupBox2.PerformLayout()
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
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents TestStep As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents TestStepStatus As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Test As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Min As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Measured As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Max As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents TestGroup As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
	Friend WithEvents tsslPCName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslUserName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslServerName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsslModeName As System.Windows.Forms.ToolStripStatusLabel
	Friend WithEvents tsTestOptions As ToolStrip
	Public WithEvents TxtPN As TextBox
	Public WithEvents Label4 As Label
	Public WithEvents TxtBarcode As TextBox
	Public WithEvents Label1 As Label
	Friend WithEvents imgPhaseStation As ImageList
	Friend WithEvents ToolStripLabel1 As ToolStripLabel
	Friend WithEvents TSDP_PhaseStation As ToolStripDropDownButton
	Friend WithEvents ToolStripLabel2 As ToolStripLabel
	Friend WithEvents TSDP_Mode As ToolStripDropDownButton
	Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
	Friend WithEvents PPPToolStripMenuItem As ToolStripMenuItem
	Friend WithEvents GroupBox2 As GroupBox
	Friend WithEvents lblTestTime As Label
	Friend WithEvents Label5 As Label
#End Region
End Class