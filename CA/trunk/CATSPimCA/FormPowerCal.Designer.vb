<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPowerCal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPowerCal))
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.fgInstrument = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlInstr = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tcPowerCal = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chartPIM = New C1.Win.C1Chart.C1Chart()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.fgCalData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.fgHistory = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlPowerCal = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblAttValue = New System.Windows.Forms.Label()
        Me.txtPSSN = New System.Windows.Forms.TextBox()
        Me.lblPower = New System.Windows.Forms.Label()
        Me.btnStartCal = New System.Windows.Forms.Button()
        Me.btnZero = New System.Windows.Forms.Button()
        Me.cbPower = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPimBand = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.pbProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.tsslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslTimeLapsed = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnExit = New System.Windows.Forms.ToolStripButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        CType(Me.fgInstrument, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlInstr.SuspendLayout()
        Me.tcPowerCal.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.chartPIM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.fgCalData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.fgHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPowerCal.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.Location = New System.Drawing.Point(0, 27)
        Me.splitMain.Margin = New System.Windows.Forms.Padding(4)
        Me.splitMain.Name = "splitMain"
        Me.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.fgInstrument)
        Me.splitMain.Panel1.Controls.Add(Me.pnlInstr)
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.tcPowerCal)
        Me.splitMain.Panel2.Controls.Add(Me.pnlPowerCal)
        Me.splitMain.Size = New System.Drawing.Size(1184, 701)
        Me.splitMain.SplitterDistance = 158
        Me.splitMain.SplitterWidth = 5
        Me.splitMain.TabIndex = 1
        '
        'fgInstrument
        '
        Me.fgInstrument.AllowEditing = False
        Me.fgInstrument.ColumnInfo = "1,1,0,0,0,125,Columns:"
        Me.fgInstrument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fgInstrument.ExtendLastCol = True
        Me.fgInstrument.Location = New System.Drawing.Point(0, 49)
        Me.fgInstrument.Margin = New System.Windows.Forms.Padding(4)
        Me.fgInstrument.Name = "fgInstrument"
        Me.fgInstrument.Rows.Count = 1
        Me.fgInstrument.Rows.DefaultSize = 25
        Me.fgInstrument.Size = New System.Drawing.Size(1184, 109)
        Me.fgInstrument.StyleInfo = resources.GetString("fgInstrument.StyleInfo")
        Me.fgInstrument.TabIndex = 2
        '
        'pnlInstr
        '
        Me.pnlInstr.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.pnlInstr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlInstr.Controls.Add(Me.btnRefresh)
        Me.pnlInstr.Controls.Add(Me.Label1)
        Me.pnlInstr.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInstr.Location = New System.Drawing.Point(0, 0)
        Me.pnlInstr.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlInstr.Name = "pnlInstr"
        Me.pnlInstr.Size = New System.Drawing.Size(1184, 49)
        Me.pnlInstr.TabIndex = 3
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(1083, 5)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 38)
        Me.btnRefresh.TabIndex = 1
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(346, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "My Power Sensor Instrument"
        '
        'tcPowerCal
        '
        Me.tcPowerCal.Controls.Add(Me.TabPage1)
        Me.tcPowerCal.Controls.Add(Me.TabPage2)
        Me.tcPowerCal.Controls.Add(Me.TabPage3)
        Me.tcPowerCal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcPowerCal.Location = New System.Drawing.Point(0, 80)
        Me.tcPowerCal.Margin = New System.Windows.Forms.Padding(4)
        Me.tcPowerCal.Name = "tcPowerCal"
        Me.tcPowerCal.SelectedIndex = 0
        Me.tcPowerCal.Size = New System.Drawing.Size(1184, 458)
        Me.tcPowerCal.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chartPIM)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage1.Size = New System.Drawing.Size(1176, 425)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Chart"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chartPIM
        '
        Me.chartPIM.BackColor = System.Drawing.Color.Transparent
        Me.chartPIM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartPIM.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.chartPIM.Location = New System.Drawing.Point(4, 4)
        Me.chartPIM.Margin = New System.Windows.Forms.Padding(4)
        Me.chartPIM.Name = "chartPIM"
        Me.chartPIM.PropBag = resources.GetString("chartPIM.PropBag")
        Me.chartPIM.Size = New System.Drawing.Size(1168, 417)
        Me.chartPIM.TabIndex = 5
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.fgCalData)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4)
        Me.TabPage2.Size = New System.Drawing.Size(1176, 432)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Data"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'fgCalData
        '
        Me.fgCalData.AllowEditing = False
        Me.fgCalData.ColumnInfo = "1,1,0,0,0,125,Columns:"
        Me.fgCalData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fgCalData.ExtendLastCol = True
        Me.fgCalData.Location = New System.Drawing.Point(4, 4)
        Me.fgCalData.Margin = New System.Windows.Forms.Padding(4)
        Me.fgCalData.Name = "fgCalData"
        Me.fgCalData.Rows.Count = 1
        Me.fgCalData.Rows.DefaultSize = 25
        Me.fgCalData.Size = New System.Drawing.Size(1168, 424)
        Me.fgCalData.StyleInfo = resources.GetString("fgCalData.StyleInfo")
        Me.fgCalData.TabIndex = 3
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.fgHistory)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1176, 432)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "History"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'fgHistory
        '
        Me.fgHistory.ColumnInfo = "1,1,0,0,0,125,Columns:"
        Me.fgHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fgHistory.Location = New System.Drawing.Point(3, 3)
        Me.fgHistory.Name = "fgHistory"
        Me.fgHistory.Rows.Count = 1
        Me.fgHistory.Rows.DefaultSize = 25
        Me.fgHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.fgHistory.Size = New System.Drawing.Size(1170, 426)
        Me.fgHistory.StyleInfo = resources.GetString("fgHistory.StyleInfo")
        Me.fgHistory.TabIndex = 0
        '
        'pnlPowerCal
        '
        Me.pnlPowerCal.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.pnlPowerCal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPowerCal.Controls.Add(Me.TableLayoutPanel1)
        Me.pnlPowerCal.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPowerCal.Location = New System.Drawing.Point(0, 0)
        Me.pnlPowerCal.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlPowerCal.Name = "pnlPowerCal"
        Me.pnlPowerCal.Size = New System.Drawing.Size(1184, 80)
        Me.pnlPowerCal.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TableLayoutPanel1.ColumnCount = 7
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblAttValue, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtPSSN, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPower, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnStartCal, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btnZero, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.cbPower, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblPimBand, 2, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1182, 78)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 40)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(172, 36)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Power Calibrate"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 2)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(172, 36)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Cal Options"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAttValue
        '
        Me.lblAttValue.AutoSize = True
        Me.lblAttValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAttValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttValue.Location = New System.Drawing.Point(188, 2)
        Me.lblAttValue.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAttValue.Name = "lblAttValue"
        Me.lblAttValue.Size = New System.Drawing.Size(192, 36)
        Me.lblAttValue.TabIndex = 3
        Me.lblAttValue.Text = "Attenuator SN:"
        Me.lblAttValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPSSN
        '
        Me.txtPSSN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPSSN.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPSSN.Location = New System.Drawing.Point(390, 6)
        Me.txtPSSN.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPSSN.Name = "txtPSSN"
        Me.txtPSSN.Size = New System.Drawing.Size(192, 34)
        Me.txtPSSN.TabIndex = 0
        '
        'lblPower
        '
        Me.lblPower.AutoSize = True
        Me.lblPower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPower.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblPower.Location = New System.Drawing.Point(592, 2)
        Me.lblPower.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPower.Name = "lblPower"
        Me.lblPower.Size = New System.Drawing.Size(142, 36)
        Me.lblPower.TabIndex = 6
        Me.lblPower.Text = "Cal Power@"
        Me.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStartCal
        '
        Me.btnStartCal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStartCal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStartCal.Location = New System.Drawing.Point(1016, 44)
        Me.btnStartCal.Margin = New System.Windows.Forms.Padding(4)
        Me.btnStartCal.Name = "btnStartCal"
        Me.btnStartCal.Size = New System.Drawing.Size(160, 28)
        Me.btnStartCal.TabIndex = 3
        Me.btnStartCal.Text = "Start"
        Me.btnStartCal.UseVisualStyleBackColor = True
        '
        'btnZero
        '
        Me.btnZero.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnZero.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnZero.Location = New System.Drawing.Point(846, 44)
        Me.btnZero.Margin = New System.Windows.Forms.Padding(4)
        Me.btnZero.Name = "btnZero"
        Me.btnZero.Size = New System.Drawing.Size(160, 28)
        Me.btnZero.TabIndex = 2
        Me.btnZero.Text = "Zero"
        Me.btnZero.UseVisualStyleBackColor = True
        '
        'cbPower
        '
        Me.cbPower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cbPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPower.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPower.FormattingEnabled = True
        Me.cbPower.Items.AddRange(New Object() {"43"})
        Me.cbPower.Location = New System.Drawing.Point(744, 6)
        Me.cbPower.Margin = New System.Windows.Forms.Padding(4)
        Me.cbPower.Name = "cbPower"
        Me.cbPower.Size = New System.Drawing.Size(92, 33)
        Me.cbPower.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(187, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(194, 36)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "PIM Analyzer Model:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPimBand
        '
        Me.lblPimBand.AutoSize = True
        Me.lblPimBand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPimBand.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblPimBand.Location = New System.Drawing.Point(389, 40)
        Me.lblPimBand.Name = "lblPimBand"
        Me.lblPimBand.Size = New System.Drawing.Size(194, 36)
        Me.lblPimBand.TabIndex = 8
        Me.lblPimBand.Text = "PIM700"
        Me.lblPimBand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pbProgress, Me.tsslStatus, Me.tsslTimeLapsed})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 728)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(2, 0, 21, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1184, 33)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'pbProgress
        '
        Me.pbProgress.Name = "pbProgress"
        Me.pbProgress.Size = New System.Drawing.Size(600, 25)
        '
        'tsslStatus
        '
        Me.tsslStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsslStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.tsslStatus.Name = "tsslStatus"
        Me.tsslStatus.Size = New System.Drawing.Size(496, 27)
        Me.tsslStatus.Spring = True
        '
        'tsslTimeLapsed
        '
        Me.tsslTimeLapsed.Name = "tsslTimeLapsed"
        Me.tsslTimeLapsed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.tsslTimeLapsed.Size = New System.Drawing.Size(63, 27)
        Me.tsslTimeLapsed.Text = "00:00:00"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnExit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1184, 27)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 24)
        Me.btnSave.Text = "Save"
        '
        'btnExit
        '
        Me.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(69, 24)
        Me.btnExit.Text = "Close"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'FormPowerCal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 761)
        Me.ControlBox = False
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormPowerCal"
        Me.Text = "PIM Analyzer Power Calibrate"
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        CType(Me.fgInstrument, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlInstr.ResumeLayout(False)
        Me.pnlInstr.PerformLayout()
        Me.tcPowerCal.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.chartPIM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.fgCalData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.fgHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPowerCal.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents fgInstrument As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlInstr As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents pnlPowerCal As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents btnStartCal As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents pbProgress As ToolStripProgressBar
    Friend WithEvents tsslStatus As ToolStripStatusLabel
    Friend WithEvents tsslTimeLapsed As ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents btnExit As ToolStripButton
    Friend WithEvents btnZero As Button
    Friend WithEvents chartPIM As C1.Win.C1Chart.C1Chart
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label5 As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lblPower As Label
    Friend WithEvents cbPower As ComboBox
    Friend WithEvents tcPowerCal As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents lblAttValue As Label
    Friend WithEvents txtPSSN As TextBox
    Friend WithEvents fgCalData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label3 As Label
    Friend WithEvents lblPimBand As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents fgHistory As C1.Win.C1FlexGrid.C1FlexGrid
End Class
