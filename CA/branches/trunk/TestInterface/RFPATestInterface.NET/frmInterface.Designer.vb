Imports System.Windows.Forms
Imports System

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
    Public WithEvents CmdClearResults As System.Windows.Forms.Button
    Public WithEvents dgvTestPhase As System.Windows.Forms.DataGridView
    Public WithEvents TxtStatus As System.Windows.Forms.TextBox
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TxtFixtureID = New System.Windows.Forms.TextBox()
        Me.TxtTesterID = New System.Windows.Forms.TextBox()
        Me.CmdClearResults = New System.Windows.Forms.Button()
        Me.dgvTestPhase = New System.Windows.Forms.DataGridView()
        Me.TestStep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TestStepStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxtStatus = New System.Windows.Forms.TextBox()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvTestResult = New System.Windows.Forms.DataGridView()
        Me.Test = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Measured = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Max = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTestGroup = New System.Windows.Forms.DataGridView()
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
        Me.mnuSAPMain = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslPlant = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslPCName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslUserName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslSAPFailSafeModeOn = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTestOptions = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.TSDP_Mode = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PPPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TSDP_PhaseStation = New System.Windows.Forms.ToolStripDropDownButton()
        Me.txtSN1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.imgPhaseStation = New System.Windows.Forms.ImageList(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblOrderNumber = New System.Windows.Forms.Label()
        Me.lblSerialNumber1 = New System.Windows.Forms.Label()
        Me.gbAssemblyInfo = New System.Windows.Forms.GroupBox()
        Me.lblSerialNumber2 = New System.Windows.Forms.Label()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblLength = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblPartNumber = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.gbSN = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSN2 = New System.Windows.Forms.TextBox()
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip()
        Me.tsslMiiStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslDatabase = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslInstrVirtualMode = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslAdapterCounter = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip3 = New System.Windows.Forms.StatusStrip()
        Me.tsslDynamicStatic = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslRetryCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslTestCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslAuto = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip4 = New System.Windows.Forms.StatusStrip()
        Me.tsslProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.dgvTestPhase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTestResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTestGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.tsTestOptions.SuspendLayout()
        Me.gbAssemblyInfo.SuspendLayout()
        Me.gbSN.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.StatusStrip3.SuspendLayout()
        Me.StatusStrip4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtFixtureID
        '
        Me.TxtFixtureID.AcceptsReturn = True
        Me.TxtFixtureID.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFixtureID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtFixtureID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFixtureID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtFixtureID.Location = New System.Drawing.Point(253, 307)
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
        Me.TxtTesterID.Location = New System.Drawing.Point(381, 307)
        Me.TxtTesterID.MaxLength = 0
        Me.TxtTesterID.Name = "TxtTesterID"
        Me.TxtTesterID.ReadOnly = True
        Me.TxtTesterID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTesterID.Size = New System.Drawing.Size(89, 20)
        Me.TxtTesterID.TabIndex = 9
        Me.TxtTesterID.Visible = False
        '
        'CmdClearResults
        '
        Me.CmdClearResults.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClearResults.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClearResults.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClearResults.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClearResults.Location = New System.Drawing.Point(445, 629)
        Me.CmdClearResults.Name = "CmdClearResults"
        Me.CmdClearResults.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClearResults.Size = New System.Drawing.Size(97, 25)
        Me.CmdClearResults.TabIndex = 4
        Me.CmdClearResults.Text = "Clear Results"
        Me.CmdClearResults.UseVisualStyleBackColor = False
        Me.CmdClearResults.Visible = False
        '
        'dgvTestPhase
        '
        Me.dgvTestPhase.AllowUserToAddRows = False
        Me.dgvTestPhase.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestPhase.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTestPhase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTestPhase.ColumnHeadersHeight = 26
        Me.dgvTestPhase.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TestStep, Me.TestStepStatus})
        Me.dgvTestPhase.Location = New System.Drawing.Point(6, 172)
        Me.dgvTestPhase.MultiSelect = False
        Me.dgvTestPhase.Name = "dgvTestPhase"
        Me.dgvTestPhase.ReadOnly = True
        Me.dgvTestPhase.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestPhase.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTestPhase.RowTemplate.Height = 18
        Me.dgvTestPhase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvTestPhase.Size = New System.Drawing.Size(206, 167)
        Me.dgvTestPhase.TabIndex = 18
        '
        'TestStep
        '
        Me.TestStep.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.TestStep.DefaultCellStyle = DataGridViewCellStyle2
        Me.TestStep.HeaderText = "Test Step"
        Me.TestStep.Name = "TestStep"
        Me.TestStep.ReadOnly = True
        Me.TestStep.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TestStepStatus
        '
        Me.TestStepStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.TestStepStatus.DefaultCellStyle = DataGridViewCellStyle3
        Me.TestStepStatus.HeaderText = "Status"
        Me.TestStepStatus.Name = "TestStepStatus"
        Me.TestStepStatus.ReadOnly = True
        Me.TestStepStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TestStepStatus.Width = 59
        '
        'TxtStatus
        '
        Me.TxtStatus.AcceptsReturn = True
        Me.TxtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.TxtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtStatus.Location = New System.Drawing.Point(6, 533)
        Me.TxtStatus.MaxLength = 0
        Me.TxtStatus.Multiline = True
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.ReadOnly = True
        Me.TxtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtStatus.Size = New System.Drawing.Size(849, 78)
        Me.TxtStatus.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(461, 658)
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
        Me.Label3.Location = New System.Drawing.Point(277, 307)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(65, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Operator"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Visible = False
        '
        'dgvTestResult
        '
        Me.dgvTestResult.AllowUserToAddRows = False
        Me.dgvTestResult.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestResult.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvTestResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTestResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTestResult.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Test, Me.Min, Me.Measured, Me.Max, Me.Status})
        Me.dgvTestResult.Location = New System.Drawing.Point(218, 172)
        Me.dgvTestResult.MultiSelect = False
        Me.dgvTestResult.Name = "dgvTestResult"
        Me.dgvTestResult.ReadOnly = True
        Me.dgvTestResult.RowHeadersVisible = False
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestResult.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvTestResult.RowTemplate.Height = 18
        Me.dgvTestResult.RowTemplate.ReadOnly = True
        Me.dgvTestResult.Size = New System.Drawing.Size(637, 355)
        Me.dgvTestResult.TabIndex = 14
        '
        'Test
        '
        Me.Test.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.Test.DefaultCellStyle = DataGridViewCellStyle6
        Me.Test.HeaderText = "Test Item"
        Me.Test.Name = "Test"
        Me.Test.ReadOnly = True
        Me.Test.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Min
        '
        Me.Min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Min.DefaultCellStyle = DataGridViewCellStyle7
        Me.Min.HeaderText = "Min"
        Me.Min.Name = "Min"
        Me.Min.ReadOnly = True
        Me.Min.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Min.Width = 40
        '
        'Measured
        '
        Me.Measured.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Measured.DefaultCellStyle = DataGridViewCellStyle8
        Me.Measured.HeaderText = "Meas"
        Me.Measured.Name = "Measured"
        Me.Measured.ReadOnly = True
        Me.Measured.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Measured.Width = 52
        '
        'Max
        '
        Me.Max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Max.DefaultCellStyle = DataGridViewCellStyle9
        Me.Max.HeaderText = "Max"
        Me.Max.Name = "Max"
        Me.Max.ReadOnly = True
        Me.Max.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Max.Width = 43
        '
        'Status
        '
        Me.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Status.DefaultCellStyle = DataGridViewCellStyle10
        Me.Status.HeaderText = "Status"
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Status.Width = 59
        '
        'dgvTestGroup
        '
        Me.dgvTestGroup.AllowUserToAddRows = False
        Me.dgvTestGroup.AllowUserToDeleteRows = False
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestGroup.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle12
        Me.dgvTestGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvTestGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTestGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TestGroup, Me.DataGridViewTextBoxColumn1})
        Me.dgvTestGroup.Location = New System.Drawing.Point(6, 345)
        Me.dgvTestGroup.MultiSelect = False
        Me.dgvTestGroup.Name = "dgvTestGroup"
        Me.dgvTestGroup.ReadOnly = True
        Me.dgvTestGroup.RowHeadersVisible = False
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestGroup.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvTestGroup.RowTemplate.Height = 18
        Me.dgvTestGroup.RowTemplate.ReadOnly = True
        Me.dgvTestGroup.Size = New System.Drawing.Size(206, 182)
        Me.dgvTestGroup.TabIndex = 15
        '
        'TestGroup
        '
        Me.TestGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black
        Me.TestGroup.DefaultCellStyle = DataGridViewCellStyle13
        Me.TestGroup.HeaderText = "Test Group"
        Me.TestGroup.Name = "TestGroup"
        Me.TestGroup.ReadOnly = True
        Me.TestGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle14
        Me.DataGridViewTextBoxColumn1.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 59
        '
        'shpLabMode
        '
        Me.shpLabMode.BackColor = System.Drawing.Color.Red
        Me.shpLabMode.Location = New System.Drawing.Point(232, 296)
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
        Me.labLabMode.Location = New System.Drawing.Point(548, 629)
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
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuProducts, Me.mnuToolsMain, Me.mnuFreqBandMain, Me.MnuTestProceduresMain, Me.mnuTroubleshootMain, Me.mnuCalibrateMain, Me.mnuSAPMain, Me.MnuHelpMain, Me.ToolStripMenuItem1})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MainMenu1.Size = New System.Drawing.Size(867, 25)
        Me.MainMenu1.TabIndex = 13
        '
        'mnuSAPMain
        '
        Me.mnuSAPMain.Name = "mnuSAPMain"
        Me.mnuSAPMain.Size = New System.Drawing.Size(43, 21)
        Me.mnuSAPMain.Text = "SAP"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 21)
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(155, 639)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(416, 50)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Unit Info"
        Me.GroupBox1.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPlant, Me.tsslPCName, Me.tsslUserName, Me.tsslSAPFailSafeModeOn})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 614)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(867, 25)
        Me.StatusStrip1.TabIndex = 23
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsslPlant
        '
        Me.tsslPlant.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslPlant.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslPlant.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslPlant.Name = "tsslPlant"
        Me.tsslPlant.Size = New System.Drawing.Size(213, 20)
        Me.tsslPlant.Spring = True
        Me.tsslPlant.Text = "ASZ"
        '
        'tsslPCName
        '
        Me.tsslPCName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslPCName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslPCName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslPCName.Name = "tsslPCName"
        Me.tsslPCName.Size = New System.Drawing.Size(213, 20)
        Me.tsslPCName.Spring = True
        Me.tsslPCName.Text = "PC"
        '
        'tsslUserName
        '
        Me.tsslUserName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslUserName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslUserName.Name = "tsslUserName"
        Me.tsslUserName.Size = New System.Drawing.Size(213, 20)
        Me.tsslUserName.Spring = True
        Me.tsslUserName.Text = "FSJ"
        '
        'tsslSAPFailSafeModeOn
        '
        Me.tsslSAPFailSafeModeOn.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslSAPFailSafeModeOn.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslSAPFailSafeModeOn.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslSAPFailSafeModeOn.Name = "tsslSAPFailSafeModeOn"
        Me.tsslSAPFailSafeModeOn.Size = New System.Drawing.Size(213, 20)
        Me.tsslSAPFailSafeModeOn.Spring = True
        Me.tsslSAPFailSafeModeOn.Text = "SAP"
        '
        'tsTestOptions
        '
        Me.tsTestOptions.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsTestOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.TSDP_Mode, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.TSDP_PhaseStation})
        Me.tsTestOptions.Location = New System.Drawing.Point(0, 25)
        Me.tsTestOptions.Name = "tsTestOptions"
        Me.tsTestOptions.Size = New System.Drawing.Size(867, 32)
        Me.tsTestOptions.Stretch = True
        Me.tsTestOptions.TabIndex = 0
        Me.tsTestOptions.Text = "ToolStrip1"
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
        'txtSN1
        '
        Me.txtSN1.AcceptsReturn = True
        Me.txtSN1.AcceptsTab = True
        Me.txtSN1.BackColor = System.Drawing.SystemColors.Window
        Me.txtSN1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSN1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSN1.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSN1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSN1.Location = New System.Drawing.Point(46, 31)
        Me.txtSN1.MaxLength = 0
        Me.txtSN1.Name = "txtSN1"
        Me.txtSN1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSN1.Size = New System.Drawing.Size(148, 26)
        Me.txtSN1.TabIndex = 0
        Me.txtSN1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(30, 15)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "SN1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'imgPhaseStation
        '
        Me.imgPhaseStation.ImageStream = CType(resources.GetObject("imgPhaseStation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgPhaseStation.TransparentColor = System.Drawing.Color.Transparent
        Me.imgPhaseStation.Images.SetKeyName(0, "database_16px_535651_easyicon.net.png")
        Me.imgPhaseStation.Images.SetKeyName(1, "database_accept_16px_535385_easyicon.net.png")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(8, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 15)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Serial Number1:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(310, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 15)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Order Number:"
        '
        'lblOrderNumber
        '
        Me.lblOrderNumber.AutoSize = True
        Me.lblOrderNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderNumber.Location = New System.Drawing.Point(404, 54)
        Me.lblOrderNumber.Name = "lblOrderNumber"
        Me.lblOrderNumber.Size = New System.Drawing.Size(88, 15)
        Me.lblOrderNumber.TabIndex = 0
        Me.lblOrderNumber.Text = "Order Number"
        '
        'lblSerialNumber1
        '
        Me.lblSerialNumber1.AutoSize = True
        Me.lblSerialNumber1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNumber1.Location = New System.Drawing.Point(110, 30)
        Me.lblSerialNumber1.Name = "lblSerialNumber1"
        Me.lblSerialNumber1.Size = New System.Drawing.Size(117, 18)
        Me.lblSerialNumber1.TabIndex = 0
        Me.lblSerialNumber1.Text = "Serial Number1"
        '
        'gbAssemblyInfo
        '
        Me.gbAssemblyInfo.Controls.Add(Me.lblSerialNumber2)
        Me.gbAssemblyInfo.Controls.Add(Me.lblSerialNumber1)
        Me.gbAssemblyInfo.Controls.Add(Me.lblUnit)
        Me.gbAssemblyInfo.Controls.Add(Me.Label11)
        Me.gbAssemblyInfo.Controls.Add(Me.lblLength)
        Me.gbAssemblyInfo.Controls.Add(Me.Label8)
        Me.gbAssemblyInfo.Controls.Add(Me.lblPartNumber)
        Me.gbAssemblyInfo.Controls.Add(Me.Label7)
        Me.gbAssemblyInfo.Controls.Add(Me.lblOrderNumber)
        Me.gbAssemblyInfo.Controls.Add(Me.Label10)
        Me.gbAssemblyInfo.Controls.Add(Me.Label6)
        Me.gbAssemblyInfo.Controls.Add(Me.Label4)
        Me.gbAssemblyInfo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAssemblyInfo.Location = New System.Drawing.Point(218, 60)
        Me.gbAssemblyInfo.Name = "gbAssemblyInfo"
        Me.gbAssemblyInfo.Size = New System.Drawing.Size(637, 106)
        Me.gbAssemblyInfo.TabIndex = 29
        Me.gbAssemblyInfo.TabStop = False
        Me.gbAssemblyInfo.Text = "Assembly Information:"
        '
        'lblSerialNumber2
        '
        Me.lblSerialNumber2.AutoSize = True
        Me.lblSerialNumber2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNumber2.Location = New System.Drawing.Point(404, 30)
        Me.lblSerialNumber2.Name = "lblSerialNumber2"
        Me.lblSerialNumber2.Size = New System.Drawing.Size(117, 18)
        Me.lblSerialNumber2.TabIndex = 0
        Me.lblSerialNumber2.Text = "Serial Number2"
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnit.Location = New System.Drawing.Point(404, 77)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(29, 15)
        Me.lblUnit.TabIndex = 0
        Me.lblUnit.Text = "Unit"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(367, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(32, 15)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Unit:"
        '
        'lblLength
        '
        Me.lblLength.AutoSize = True
        Me.lblLength.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLength.Location = New System.Drawing.Point(110, 78)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(46, 15)
        Me.lblLength.TabIndex = 0
        Me.lblLength.Text = "Length"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(57, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 15)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Length:"
        '
        'lblPartNumber
        '
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumber.Location = New System.Drawing.Point(110, 54)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(97, 18)
        Me.lblPartNumber.TabIndex = 0
        Me.lblPartNumber.Text = "Part Number"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 15)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Part Number:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(302, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 15)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Serial Number2:"
        '
        'gbSN
        '
        Me.gbSN.Controls.Add(Me.Label9)
        Me.gbSN.Controls.Add(Me.Label1)
        Me.gbSN.Controls.Add(Me.txtSN2)
        Me.gbSN.Controls.Add(Me.txtSN1)
        Me.gbSN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSN.Location = New System.Drawing.Point(7, 60)
        Me.gbSN.Name = "gbSN"
        Me.gbSN.Size = New System.Drawing.Size(205, 106)
        Me.gbSN.TabIndex = 31
        Me.gbSN.TabStop = False
        Me.gbSN.Text = "Scan Label:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(30, 15)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "SN2"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSN2
        '
        Me.txtSN2.AcceptsReturn = True
        Me.txtSN2.AcceptsTab = True
        Me.txtSN2.BackColor = System.Drawing.SystemColors.WindowText
        Me.txtSN2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSN2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSN2.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSN2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSN2.Location = New System.Drawing.Point(46, 63)
        Me.txtSN2.MaxLength = 0
        Me.txtSN2.Name = "txtSN2"
        Me.txtSN2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSN2.Size = New System.Drawing.Size(148, 26)
        Me.txtSN2.TabIndex = 0
        Me.txtSN2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StatusStrip2
        '
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslMiiStatus, Me.tsslDatabase, Me.tsslInstrVirtualMode, Me.tsslAdapterCounter})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 639)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(867, 25)
        Me.StatusStrip2.TabIndex = 32
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'tsslMiiStatus
        '
        Me.tsslMiiStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslMiiStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslMiiStatus.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslMiiStatus.Name = "tsslMiiStatus"
        Me.tsslMiiStatus.Size = New System.Drawing.Size(213, 20)
        Me.tsslMiiStatus.Spring = True
        Me.tsslMiiStatus.Text = "MII"
        '
        'tsslDatabase
        '
        Me.tsslDatabase.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslDatabase.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslDatabase.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslDatabase.Name = "tsslDatabase"
        Me.tsslDatabase.Size = New System.Drawing.Size(213, 20)
        Me.tsslDatabase.Spring = True
        Me.tsslDatabase.Text = "DB"
        '
        'tsslInstrVirtualMode
        '
        Me.tsslInstrVirtualMode.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslInstrVirtualMode.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslInstrVirtualMode.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslInstrVirtualMode.Name = "tsslInstrVirtualMode"
        Me.tsslInstrVirtualMode.Size = New System.Drawing.Size(213, 20)
        Me.tsslInstrVirtualMode.Spring = True
        Me.tsslInstrVirtualMode.Text = "InstrMode"
        '
        'tsslAdapterCounter
        '
        Me.tsslAdapterCounter.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslAdapterCounter.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslAdapterCounter.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslAdapterCounter.Name = "tsslAdapterCounter"
        Me.tsslAdapterCounter.Size = New System.Drawing.Size(213, 20)
        Me.tsslAdapterCounter.Spring = True
        Me.tsslAdapterCounter.Text = "Counter"
        '
        'StatusStrip3
        '
        Me.StatusStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslDynamicStatic, Me.tsslRetryCount, Me.tsslTestCount, Me.tsslAuto})
        Me.StatusStrip3.Location = New System.Drawing.Point(0, 664)
        Me.StatusStrip3.Name = "StatusStrip3"
        Me.StatusStrip3.Size = New System.Drawing.Size(867, 25)
        Me.StatusStrip3.TabIndex = 33
        Me.StatusStrip3.Text = "StatusStrip3"
        '
        'tsslDynamicStatic
        '
        Me.tsslDynamicStatic.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslDynamicStatic.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslDynamicStatic.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslDynamicStatic.Name = "tsslDynamicStatic"
        Me.tsslDynamicStatic.Size = New System.Drawing.Size(213, 20)
        Me.tsslDynamicStatic.Spring = True
        Me.tsslDynamicStatic.Text = "Dynamic"
        '
        'tsslRetryCount
        '
        Me.tsslRetryCount.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslRetryCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslRetryCount.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslRetryCount.Name = "tsslRetryCount"
        Me.tsslRetryCount.Size = New System.Drawing.Size(213, 20)
        Me.tsslRetryCount.Spring = True
        Me.tsslRetryCount.Text = "Retry Count"
        '
        'tsslTestCount
        '
        Me.tsslTestCount.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslTestCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslTestCount.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslTestCount.Name = "tsslTestCount"
        Me.tsslTestCount.Size = New System.Drawing.Size(213, 20)
        Me.tsslTestCount.Spring = True
        Me.tsslTestCount.Text = "Test Count"
        '
        'tsslAuto
        '
        Me.tsslAuto.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslAuto.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslAuto.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslAuto.Name = "tsslAuto"
        Me.tsslAuto.Size = New System.Drawing.Size(213, 20)
        Me.tsslAuto.Spring = True
        Me.tsslAuto.Text = "Automation"
        '
        'StatusStrip4
        '
        Me.StatusStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslProgressBar, Me.tsslStatus})
        Me.StatusStrip4.Location = New System.Drawing.Point(0, 689)
        Me.StatusStrip4.Name = "StatusStrip4"
        Me.StatusStrip4.Size = New System.Drawing.Size(867, 26)
        Me.StatusStrip4.TabIndex = 34
        Me.StatusStrip4.Text = "StatusStrip4"
        '
        'tsslProgressBar
        '
        Me.tsslProgressBar.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslProgressBar.Name = "tsslProgressBar"
        Me.tsslProgressBar.Size = New System.Drawing.Size(403, 20)
        '
        'tsslStatus
        '
        Me.tsslStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslStatus.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(447, 21)
        Me.tsslStatus.Spring = True
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(867, 715)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.TxtStatus)
        Me.Controls.Add(Me.gbSN)
        Me.Controls.Add(Me.tsTestOptions)
        Me.Controls.Add(Me.gbAssemblyInfo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.labLabMode)
        Me.Controls.Add(Me.CmdClearResults)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.dgvTestGroup)
        Me.Controls.Add(Me.dgvTestPhase)
        Me.Controls.Add(Me.dgvTestResult)
        Me.Controls.Add(Me.TxtTesterID)
        Me.Controls.Add(Me.shpLabMode)
        Me.Controls.Add(Me.TxtFixtureID)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.StatusStrip3)
        Me.Controls.Add(Me.StatusStrip4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(60, 65)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Product Test Name"
        CType(Me.dgvTestPhase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTestResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTestGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.tsTestOptions.ResumeLayout(False)
        Me.tsTestOptions.PerformLayout()
        Me.gbAssemblyInfo.ResumeLayout(False)
        Me.gbAssemblyInfo.PerformLayout()
        Me.gbSN.ResumeLayout(False)
        Me.gbSN.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.StatusStrip3.ResumeLayout(False)
        Me.StatusStrip3.PerformLayout()
        Me.StatusStrip4.ResumeLayout(False)
        Me.StatusStrip4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvTestResult As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTestGroup As System.Windows.Forms.DataGridView
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
    Friend WithEvents tsslSAPFailSafeModeOn As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsslPlant As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tsTestOptions As ToolStrip
    Public WithEvents txtSN1 As TextBox
    Public WithEvents Label1 As Label
    Friend WithEvents imgPhaseStation As ImageList
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents TSDP_PhaseStation As ToolStripDropDownButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents TSDP_Mode As ToolStripDropDownButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents PPPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblOrderNumber As Label
    Friend WithEvents lblSerialNumber1 As Label
    Friend WithEvents gbAssemblyInfo As GroupBox
    Friend WithEvents lblPartNumber As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblLength As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents mnuSAPMain As ToolStripMenuItem
    Friend WithEvents gbSN As GroupBox
    Public WithEvents Label9 As Label
    Public WithEvents txtSN2 As TextBox
    Friend WithEvents lblSerialNumber2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblUnit As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents TestStep As DataGridViewTextBoxColumn
    Friend WithEvents TestStepStatus As DataGridViewTextBoxColumn
    Friend WithEvents StatusStrip2 As StatusStrip
    Friend WithEvents tsslMiiStatus As ToolStripStatusLabel
    Friend WithEvents tsslDatabase As ToolStripStatusLabel
    Friend WithEvents tsslInstrVirtualMode As ToolStripStatusLabel
    Friend WithEvents tsslAdapterCounter As ToolStripStatusLabel
    Friend WithEvents StatusStrip3 As StatusStrip
    Friend WithEvents tsslDynamicStatic As ToolStripStatusLabel
    Friend WithEvents tsslRetryCount As ToolStripStatusLabel
    Friend WithEvents tsslTestCount As ToolStripStatusLabel
    Friend WithEvents tsslAuto As ToolStripStatusLabel
    Friend WithEvents StatusStrip4 As StatusStrip
    Friend WithEvents tsslProgressBar As ToolStripProgressBar
    Friend WithEvents tsslStatus As ToolStripStatusLabel
#End Region
End Class