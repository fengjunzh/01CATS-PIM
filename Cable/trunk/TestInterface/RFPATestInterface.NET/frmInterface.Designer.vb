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
    Public WithEvents dgvTestPhase As System.Windows.Forms.DataGridView
    Public WithEvents TxtStatus As System.Windows.Forms.TextBox
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
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
        Me.dgvTestPhase = New System.Windows.Forms.DataGridView()
        Me.TestStep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TestStepStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxtStatus = New System.Windows.Forms.TextBox()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.dgvTestResult = New System.Windows.Forms.DataGridView()
        Me.Test = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Min = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Measured = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Max = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTestGroup = New System.Windows.Forms.DataGridView()
        Me.TestGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslPlant = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslPCName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslUserName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslDb = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsTestOptions = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.TSDP_Mode = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PPPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TSDP_PhaseStation = New System.Windows.Forms.ToolStripDropDownButton()
        Me.imgPhaseStation = New System.Windows.Forms.ImageList(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblOrderNumber = New System.Windows.Forms.Label()
        Me.lblSerialNumber = New System.Windows.Forms.Label()
        Me.gbAssemblyInfo = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblPartNumber = New System.Windows.Forms.Label()
        Me.lblLength = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCoreNumber = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblTestConnector = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTestLength = New System.Windows.Forms.Label()
        Me.btnNewCable = New System.Windows.Forms.Button()
        Me.btnRunTest = New System.Windows.Forms.Button()
        Me.btnLoadTest = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel()
        CType(Me.dgvTestPhase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTestResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTestGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.tsTestOptions.SuspendLayout()
        Me.gbAssemblyInfo.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.SuspendLayout()
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
        Me.dgvTestPhase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestPhase.Location = New System.Drawing.Point(3, 3)
        Me.dgvTestPhase.MultiSelect = False
        Me.dgvTestPhase.Name = "dgvTestPhase"
        Me.dgvTestPhase.ReadOnly = True
        Me.dgvTestPhase.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestPhase.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvTestPhase.RowTemplate.Height = 18
        Me.dgvTestPhase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvTestPhase.Size = New System.Drawing.Size(238, 188)
        Me.dgvTestPhase.TabIndex = 18
        Me.dgvTestPhase.TabStop = False
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
        Me.TxtStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtStatus.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtStatus.Location = New System.Drawing.Point(3, 535)
        Me.TxtStatus.MaxLength = 0
        Me.TxtStatus.Multiline = True
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.ReadOnly = True
        Me.TxtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtStatus.Size = New System.Drawing.Size(938, 172)
        Me.TxtStatus.TabIndex = 10
        Me.TxtStatus.TabStop = False
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
        Me.dgvTestResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestResult.Location = New System.Drawing.Point(253, 3)
        Me.dgvTestResult.MultiSelect = False
        Me.dgvTestResult.Name = "dgvTestResult"
        Me.dgvTestResult.ReadOnly = True
        Me.dgvTestResult.RowHeadersVisible = False
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestResult.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvTestResult.RowTemplate.Height = 18
        Me.dgvTestResult.RowTemplate.ReadOnly = True
        Me.dgvTestResult.Size = New System.Drawing.Size(682, 388)
        Me.dgvTestResult.TabIndex = 14
        Me.dgvTestResult.TabStop = False
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
        Me.dgvTestGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTestGroup.Location = New System.Drawing.Point(3, 197)
        Me.dgvTestGroup.MultiSelect = False
        Me.dgvTestGroup.Name = "dgvTestGroup"
        Me.dgvTestGroup.ReadOnly = True
        Me.dgvTestGroup.RowHeadersVisible = False
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvTestGroup.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvTestGroup.RowTemplate.Height = 18
        Me.dgvTestGroup.RowTemplate.ReadOnly = True
        Me.dgvTestGroup.Size = New System.Drawing.Size(238, 188)
        Me.dgvTestGroup.TabIndex = 15
        Me.dgvTestGroup.TabStop = False
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
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuProducts, Me.mnuToolsMain, Me.mnuFreqBandMain, Me.MnuTestProceduresMain, Me.mnuTroubleshootMain, Me.mnuCalibrateMain, Me.MnuHelpMain, Me.ToolStripMenuItem1})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MainMenu1.Size = New System.Drawing.Size(944, 25)
        Me.MainMenu1.TabIndex = 13
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(12, 21)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPlant, Me.tsslPCName, Me.tsslUserName, Me.tsslDb})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 767)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(944, 24)
        Me.StatusStrip1.TabIndex = 23
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsslPlant
        '
        Me.tsslPlant.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslPlant.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslPlant.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tsslPlant.Name = "tsslPlant"
        Me.tsslPlant.Size = New System.Drawing.Size(232, 19)
        Me.tsslPlant.Spring = True
        '
        'tsslPCName
        '
        Me.tsslPCName.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslPCName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslPCName.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslPCName.Name = "tsslPCName"
        Me.tsslPCName.Size = New System.Drawing.Size(232, 19)
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
        Me.tsslUserName.Size = New System.Drawing.Size(232, 19)
        Me.tsslUserName.Spring = True
        '
        'tsslDb
        '
        Me.tsslDb.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslDb.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslDb.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsslDb.Name = "tsslDb"
        Me.tsslDb.Size = New System.Drawing.Size(232, 19)
        Me.tsslDb.Spring = True
        Me.tsslDb.Text = "DB"
        '
        'tsTestOptions
        '
        Me.tsTestOptions.Font = New System.Drawing.Font("Century Gothic", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsTestOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.TSDP_Mode, Me.ToolStripSeparator2, Me.ToolStripLabel1, Me.TSDP_PhaseStation})
        Me.tsTestOptions.Location = New System.Drawing.Point(0, 25)
        Me.tsTestOptions.Name = "tsTestOptions"
        Me.tsTestOptions.Size = New System.Drawing.Size(944, 32)
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
        Me.TSDP_PhaseStation.Enabled = False
        Me.TSDP_PhaseStation.Font = New System.Drawing.Font("Century Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSDP_PhaseStation.ForeColor = System.Drawing.Color.Blue
        Me.TSDP_PhaseStation.Image = CType(resources.GetObject("TSDP_PhaseStation.Image"), System.Drawing.Image)
        Me.TSDP_PhaseStation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSDP_PhaseStation.Name = "TSDP_PhaseStation"
        Me.TSDP_PhaseStation.Size = New System.Drawing.Size(109, 29)
        Me.TSDP_PhaseStation.Text = "FinalTest"
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
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(5, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 30)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Cable Number:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(276, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 30)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Work Order:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblOrderNumber
        '
        Me.lblOrderNumber.AutoSize = True
        Me.lblOrderNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrderNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrderNumber.Location = New System.Drawing.Point(389, 34)
        Me.lblOrderNumber.Name = "lblOrderNumber"
        Me.lblOrderNumber.Size = New System.Drawing.Size(154, 30)
        Me.lblOrderNumber.TabIndex = 0
        Me.lblOrderNumber.Text = "Work Order"
        Me.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.AutoSize = True
        Me.lblSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSerialNumber.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSerialNumber.Location = New System.Drawing.Point(142, 2)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(126, 30)
        Me.lblSerialNumber.TabIndex = 0
        Me.lblSerialNumber.Text = "Serial Number"
        Me.lblSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbAssemblyInfo
        '
        Me.gbAssemblyInfo.Controls.Add(Me.TableLayoutPanel1)
        Me.gbAssemblyInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbAssemblyInfo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbAssemblyInfo.Location = New System.Drawing.Point(3, 3)
        Me.gbAssemblyInfo.Name = "gbAssemblyInfo"
        Me.gbAssemblyInfo.Size = New System.Drawing.Size(802, 120)
        Me.gbAssemblyInfo.TabIndex = 29
        Me.gbAssemblyInfo.TabStop = False
        Me.gbAssemblyInfo.Text = "Cable Information:"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 132.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblSerialNumber, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPartNumber, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblLength, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblOrderNumber, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblCoreNumber, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label9, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTestConnector, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTestLength, 5, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 18)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(796, 99)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(276, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(105, 30)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Cable Type:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPartNumber
        '
        Me.lblPartNumber.AutoSize = True
        Me.lblPartNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPartNumber.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNumber.Location = New System.Drawing.Point(389, 2)
        Me.lblPartNumber.Name = "lblPartNumber"
        Me.lblPartNumber.Size = New System.Drawing.Size(154, 30)
        Me.lblPartNumber.TabIndex = 0
        Me.lblPartNumber.Text = "Part Number"
        Me.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLength
        '
        Me.lblLength.AutoSize = True
        Me.lblLength.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLength.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLength.Location = New System.Drawing.Point(142, 66)
        Me.lblLength.Name = "lblLength"
        Me.lblLength.Size = New System.Drawing.Size(126, 31)
        Me.lblLength.TabIndex = 0
        Me.lblLength.Text = "Length"
        Me.lblLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(129, 31)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Original Length(M/FT):"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(5, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 30)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Core Number:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCoreNumber
        '
        Me.lblCoreNumber.AutoSize = True
        Me.lblCoreNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCoreNumber.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCoreNumber.Location = New System.Drawing.Point(142, 34)
        Me.lblCoreNumber.Name = "lblCoreNumber"
        Me.lblCoreNumber.Size = New System.Drawing.Size(126, 30)
        Me.lblCoreNumber.TabIndex = 0
        Me.lblCoreNumber.Text = "Core Number"
        Me.lblCoreNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(276, 66)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(105, 31)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Test Connector:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestConnector
        '
        Me.lblTestConnector.AutoSize = True
        Me.lblTestConnector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTestConnector.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTestConnector.Location = New System.Drawing.Point(389, 66)
        Me.lblTestConnector.Name = "lblTestConnector"
        Me.lblTestConnector.Size = New System.Drawing.Size(154, 31)
        Me.lblTestConnector.TabIndex = 0
        Me.lblTestConnector.Text = "Test Connector"
        Me.lblTestConnector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(551, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 30)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Test Length(M/FT):"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTestLength
        '
        Me.lblTestLength.AutoSize = True
        Me.lblTestLength.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTestLength.Location = New System.Drawing.Point(670, 2)
        Me.lblTestLength.Name = "lblTestLength"
        Me.lblTestLength.Size = New System.Drawing.Size(121, 30)
        Me.lblTestLength.TabIndex = 2
        Me.lblTestLength.Text = "Test Length"
        Me.lblTestLength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnNewCable
        '
        Me.btnNewCable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnNewCable.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewCable.Location = New System.Drawing.Point(3, 3)
        Me.btnNewCable.Name = "btnNewCable"
        Me.btnNewCable.Size = New System.Drawing.Size(118, 34)
        Me.btnNewCable.TabIndex = 0
        Me.btnNewCable.Text = "New Cable"
        Me.btnNewCable.UseVisualStyleBackColor = True
        '
        'btnRunTest
        '
        Me.btnRunTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRunTest.Enabled = False
        Me.btnRunTest.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRunTest.Location = New System.Drawing.Point(3, 43)
        Me.btnRunTest.Name = "btnRunTest"
        Me.btnRunTest.Size = New System.Drawing.Size(118, 34)
        Me.btnRunTest.TabIndex = 1
        Me.btnRunTest.Text = "Run Test"
        Me.btnRunTest.UseVisualStyleBackColor = True
        '
        'btnLoadTest
        '
        Me.btnLoadTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLoadTest.Enabled = False
        Me.btnLoadTest.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadTest.Location = New System.Drawing.Point(3, 83)
        Me.btnLoadTest.Name = "btnLoadTest"
        Me.btnLoadTest.Size = New System.Drawing.Size(118, 34)
        Me.btnLoadTest.TabIndex = 2
        Me.btnLoadTest.Text = "Load Test"
        Me.btnLoadTest.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TxtStatus, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 57)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 132.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(944, 710)
        Me.TableLayoutPanel2.TabIndex = 30
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 2
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.dgvTestResult, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 135)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(938, 394)
        Me.TableLayoutPanel3.TabIndex = 30
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.dgvTestPhase, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.dgvTestGroup, 0, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(244, 388)
        Me.TableLayoutPanel4.TabIndex = 15
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 2
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.gbAssemblyInfo, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TableLayoutPanel6, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(938, 126)
        Me.TableLayoutPanel5.TabIndex = 31
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 1
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.btnNewCable, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.btnLoadTest, 0, 2)
        Me.TableLayoutPanel6.Controls.Add(Me.btnRunTest, 0, 1)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(811, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 3
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(124, 120)
        Me.TableLayoutPanel6.TabIndex = 30
        '
        'frmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(944, 791)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.tsTestOptions)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MainMenu1)
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
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvTestResult As System.Windows.Forms.DataGridView
    Friend WithEvents dgvTestGroup As System.Windows.Forms.DataGridView
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
    Friend WithEvents tsTestOptions As ToolStrip
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
    Friend WithEvents lblSerialNumber As Label
    Friend WithEvents gbAssemblyInfo As GroupBox
    Friend WithEvents lblPartNumber As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblLength As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TestStep As DataGridViewTextBoxColumn
    Friend WithEvents TestStepStatus As DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Label5 As Label
    Friend WithEvents lblCoreNumber As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblTestConnector As Label
    Friend WithEvents btnNewCable As Button
    Friend WithEvents btnRunTest As Button
    Friend WithEvents btnLoadTest As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lblTestLength As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As TableLayoutPanel
    Friend WithEvents tsslPlant As ToolStripStatusLabel
    Friend WithEvents tsslDb As ToolStripStatusLabel
#End Region
End Class