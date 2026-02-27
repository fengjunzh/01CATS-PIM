<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormGRR
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormGRR))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblResIMResult = New System.Windows.Forms.Label()
        Me.label32 = New System.Windows.Forms.Label()
        Me.lblResIMPeak = New System.Windows.Forms.Label()
        Me.groupResIM = New System.Windows.Forms.GroupBox()
        Me.chartResIM = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.btnResIM = New System.Windows.Forms.Button()
        Me.label28 = New System.Windows.Forms.Label()
        Me.lblCompFactor = New System.Windows.Forms.Label()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.dgvCompensation = New System.Windows.Forms.DataGridView()
        Me.RunNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Mean = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Variance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StdDev = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompFactor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupConfiguration = New System.Windows.Forms.GroupBox()
        Me.txtFW = New System.Windows.Forms.TextBox()
        Me.txtSN = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtComPort = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtAnalyzerModel = New System.Windows.Forms.TextBox()
        Me.txtFreqBand = New System.Windows.Forms.TextBox()
        Me.txtAnalyzerVendor = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.label42 = New System.Windows.Forms.Label()
        Me.label41 = New System.Windows.Forms.Label()
        Me.label40 = New System.Windows.Forms.Label()
        Me.lblCompStatus = New System.Windows.Forms.Label()
        Me.linkGCProcedure = New System.Windows.Forms.LinkLabel()
        Me.lblLastGCDate = New System.Windows.Forms.Label()
        Me.label33 = New System.Windows.Forms.Label()
        Me.label34 = New System.Windows.Forms.Label()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnLoadProcedure = New System.Windows.Forms.Button()
        Me.lblGageCapability = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.label31 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label30 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label29 = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label27 = New System.Windows.Forms.Label()
        Me.lblRBar = New System.Windows.Forms.Label()
        Me.lblTolerance2 = New System.Windows.Forms.Label()
        Me.lblMeasError1 = New System.Windows.Forms.Label()
        Me.label25 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label26 = New System.Windows.Forms.Label()
        Me.label13 = New System.Windows.Forms.Label()
        Me.lblMultiplier2 = New System.Windows.Forms.Label()
        Me.lblMeasError2 = New System.Windows.Forms.Label()
        Me.label17 = New System.Windows.Forms.Label()
        Me.lblTolerance1 = New System.Windows.Forms.Label()
        Me.dgvGRRSamples = New System.Windows.Forms.DataGridView()
        Me.label11 = New System.Windows.Forms.Label()
        Me.lblDsub2 = New System.Windows.Forms.Label()
        Me.lblRBarCalc = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.groupGC = New System.Windows.Forms.GroupBox()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.lblMultiplier1 = New System.Windows.Forms.Label()
        Me.label19 = New System.Windows.Forms.Label()
        Me.label18 = New System.Windows.Forms.Label()
        Me.btnRunTest = New System.Windows.Forms.Button()
        Me.label16 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.gbAncillaryEquipment = New System.Windows.Forms.GroupBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cboTorqueWrenchSN = New System.Windows.Forms.ComboBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.cboAdapterSN = New System.Windows.Forms.ComboBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.cboTestLeadSN = New System.Windows.Forms.ComboBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.cboStandardSN = New System.Windows.Forms.ComboBox()
        Me.cboTerminationSN = New System.Windows.Forms.ComboBox()
        Me.RunNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.N5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Range = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.groupResIM.SuspendLayout()
        CType(Me.chartResIM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCompensation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupConfiguration.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        CType(Me.dgvGRRSamples, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupGC.SuspendLayout()
        Me.gbAncillaryEquipment.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblResIMResult
        '
        Me.lblResIMResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResIMResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResIMResult.Location = New System.Drawing.Point(812, 130)
        Me.lblResIMResult.Name = "lblResIMResult"
        Me.lblResIMResult.Size = New System.Drawing.Size(153, 32)
        Me.lblResIMResult.TabIndex = 141
        Me.lblResIMResult.Text = "Result"
        Me.lblResIMResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label32.Location = New System.Drawing.Point(814, 71)
        Me.label32.Name = "label32"
        Me.label32.Size = New System.Drawing.Size(110, 13)
        Me.label32.TabIndex = 140
        Me.label32.Text = "Residual IM (dBm)"
        '
        'lblResIMPeak
        '
        Me.lblResIMPeak.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResIMPeak.Location = New System.Drawing.Point(819, 95)
        Me.lblResIMPeak.Name = "lblResIMPeak"
        Me.lblResIMPeak.Size = New System.Drawing.Size(153, 22)
        Me.lblResIMPeak.TabIndex = 139
        Me.lblResIMPeak.Text = "000.0"
        Me.lblResIMPeak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'groupResIM
        '
        Me.groupResIM.Controls.Add(Me.chartResIM)
        Me.groupResIM.Controls.Add(Me.btnResIM)
        Me.groupResIM.Controls.Add(Me.lblResIMResult)
        Me.groupResIM.Controls.Add(Me.label32)
        Me.groupResIM.Controls.Add(Me.lblResIMPeak)
        Me.groupResIM.Enabled = False
        Me.groupResIM.Location = New System.Drawing.Point(10, 301)
        Me.groupResIM.Name = "groupResIM"
        Me.groupResIM.Size = New System.Drawing.Size(1001, 195)
        Me.groupResIM.TabIndex = 103
        Me.groupResIM.TabStop = False
        Me.groupResIM.Text = "Residual IM Verification"
        '
        'chartResIM
        '
        Me.chartResIM.BackColor = System.Drawing.Color.Transparent
        Me.chartResIM.BorderSkin.BackColor = System.Drawing.Color.Transparent
        ChartArea1.AxisX.Crossing = -1.7976931348623157E+308R
        ChartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.[True]
        ChartArea1.AxisX.Interval = 1.0R
        ChartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
        ChartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.IsStartedFromZero = False
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = 8
        ChartArea1.AxisX.LabelStyle.Interval = 5.0R
        ChartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.MajorGrid.Interval = 2.0R
        ChartArea1.AxisX.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        ChartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
        ChartArea1.AxisX.Maximum = 1910.0R
        ChartArea1.AxisX.Minimum = 1870.0R
        ChartArea1.AxisY.Crossing = -1.7976931348623157E+308R
        ChartArea1.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.[True]
        ChartArea1.AxisY.Interval = 10.0R
        ChartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.IsStartedFromZero = False
        ChartArea1.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea1.AxisY.LabelStyle.Interval = 10.0R
        ChartArea1.AxisY.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.MajorGrid.Interval = 10.0R
        ChartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        ChartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
        ChartArea1.AxisY.Maximum = -150.0R
        ChartArea1.AxisY.Minimum = -190.0R
        ChartArea1.BackColor = System.Drawing.Color.White
        ChartArea1.CursorX.LineColor = System.Drawing.Color.Green
        ChartArea1.CursorY.AutoScroll = False
        ChartArea1.CursorY.Interval = 0.1R
        ChartArea1.CursorY.IntervalOffset = 0.1R
        ChartArea1.CursorY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.CursorY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
        ChartArea1.CursorY.IsUserEnabled = True
        ChartArea1.CursorY.LineColor = System.Drawing.Color.Green
        ChartArea1.Name = "ChartArea1"
        Me.chartResIM.ChartAreas.Add(ChartArea1)
        Me.chartResIM.Dock = System.Windows.Forms.DockStyle.Left
        Me.chartResIM.Location = New System.Drawing.Point(3, 16)
        Me.chartResIM.Margin = New System.Windows.Forms.Padding(1)
        Me.chartResIM.Name = "chartResIM"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Color = System.Drawing.Color.Red
        Series1.IsVisibleInLegend = False
        Series1.MarkerColor = System.Drawing.Color.Blue
        Series1.Name = "UpperLimit"
        Series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series2.Color = System.Drawing.Color.Blue
        Series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square
        Series2.Name = "SweepUp"
        Series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Color = System.Drawing.Color.Green
        Series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square
        Series3.Name = "SweepDown"
        Series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.[Double]
        Me.chartResIM.Series.Add(Series1)
        Me.chartResIM.Series.Add(Series2)
        Me.chartResIM.Series.Add(Series3)
        Me.chartResIM.Size = New System.Drawing.Size(787, 176)
        Me.chartResIM.TabIndex = 144
        Me.chartResIM.TabStop = False
        Me.chartResIM.Text = "chart1"
        '
        'btnResIM
        '
        Me.btnResIM.Image = CType(resources.GetObject("btnResIM.Image"), System.Drawing.Image)
        Me.btnResIM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnResIM.Location = New System.Drawing.Point(813, 29)
        Me.btnResIM.Name = "btnResIM"
        Me.btnResIM.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.btnResIM.Size = New System.Drawing.Size(155, 29)
        Me.btnResIM.TabIndex = 1
        Me.btnResIM.Text = "&Collect Sample"
        Me.btnResIM.UseVisualStyleBackColor = True
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.Location = New System.Drawing.Point(29, 341)
        Me.label28.Name = "label28"
        Me.label28.Size = New System.Drawing.Size(99, 13)
        Me.label28.TabIndex = 142
        Me.label28.Text = "Error Compensation"
        '
        'lblCompFactor
        '
        Me.lblCompFactor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompFactor.Location = New System.Drawing.Point(818, 387)
        Me.lblCompFactor.Name = "lblCompFactor"
        Me.lblCompFactor.Size = New System.Drawing.Size(155, 56)
        Me.lblCompFactor.TabIndex = 4
        Me.lblCompFactor.Text = "0.00 dBm"
        Me.lblCompFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.SystemColors.Control
        Me.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblResult.Location = New System.Drawing.Point(32, 268)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.Size = New System.Drawing.Size(941, 49)
        Me.lblResult.TabIndex = 0
        Me.lblResult.Text = "Result"
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(819, 357)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(109, 13)
        Me.label12.TabIndex = 3
        Me.label12.Text = "Comp. Factor (3σ)"
        '
        'dgvCompensation
        '
        Me.dgvCompensation.AllowUserToAddRows = False
        Me.dgvCompensation.AllowUserToDeleteRows = False
        Me.dgvCompensation.AllowUserToResizeRows = False
        Me.dgvCompensation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCompensation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RunNum, Me.Mean, Me.Variance, Me.StdDev, Me.CompFactor})
        Me.dgvCompensation.Location = New System.Drawing.Point(112, 846)
        Me.dgvCompensation.MultiSelect = False
        Me.dgvCompensation.Name = "dgvCompensation"
        Me.dgvCompensation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvCompensation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCompensation.ShowEditingIcon = False
        Me.dgvCompensation.Size = New System.Drawing.Size(759, 92)
        Me.dgvCompensation.TabIndex = 2
        Me.dgvCompensation.Visible = False
        '
        'RunNum
        '
        Me.RunNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.RunNum.DefaultCellStyle = DataGridViewCellStyle1
        Me.RunNum.HeaderText = "Run #"
        Me.RunNum.Name = "RunNum"
        Me.RunNum.ReadOnly = True
        Me.RunNum.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RunNum.Width = 75
        '
        'Mean
        '
        Me.Mean.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Mean.DefaultCellStyle = DataGridViewCellStyle2
        Me.Mean.HeaderText = "Mean"
        Me.Mean.Name = "Mean"
        Me.Mean.ReadOnly = True
        Me.Mean.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Mean.Width = 145
        '
        'Variance
        '
        Me.Variance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Variance.DefaultCellStyle = DataGridViewCellStyle3
        Me.Variance.HeaderText = "Variance"
        Me.Variance.Name = "Variance"
        Me.Variance.ReadOnly = True
        Me.Variance.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Variance.Width = 145
        '
        'StdDev
        '
        Me.StdDev.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.StdDev.DefaultCellStyle = DataGridViewCellStyle4
        Me.StdDev.HeaderText = "Std. Dev. (σ)"
        Me.StdDev.Name = "StdDev"
        Me.StdDev.ReadOnly = True
        Me.StdDev.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.StdDev.Width = 145
        '
        'CompFactor
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.CompFactor.DefaultCellStyle = DataGridViewCellStyle5
        Me.CompFactor.HeaderText = "Comp. Factor"
        Me.CompFactor.Name = "CompFactor"
        Me.CompFactor.ReadOnly = True
        Me.CompFactor.Width = 145
        '
        'groupConfiguration
        '
        Me.groupConfiguration.Controls.Add(Me.txtFW)
        Me.groupConfiguration.Controls.Add(Me.txtSN)
        Me.groupConfiguration.Controls.Add(Me.TextBox4)
        Me.groupConfiguration.Controls.Add(Me.Label45)
        Me.groupConfiguration.Controls.Add(Me.Label35)
        Me.groupConfiguration.Controls.Add(Me.txtComPort)
        Me.groupConfiguration.Controls.Add(Me.Label36)
        Me.groupConfiguration.Controls.Add(Me.txtAnalyzerModel)
        Me.groupConfiguration.Controls.Add(Me.txtFreqBand)
        Me.groupConfiguration.Controls.Add(Me.txtAnalyzerVendor)
        Me.groupConfiguration.Controls.Add(Me.Label38)
        Me.groupConfiguration.Controls.Add(Me.label42)
        Me.groupConfiguration.Controls.Add(Me.label41)
        Me.groupConfiguration.Controls.Add(Me.label40)
        Me.groupConfiguration.Location = New System.Drawing.Point(10, 12)
        Me.groupConfiguration.Name = "groupConfiguration"
        Me.groupConfiguration.Size = New System.Drawing.Size(1001, 111)
        Me.groupConfiguration.TabIndex = 100
        Me.groupConfiguration.TabStop = False
        Me.groupConfiguration.Text = "Instrument Information"
        '
        'txtFW
        '
        Me.txtFW.Location = New System.Drawing.Point(416, 77)
        Me.txtFW.Name = "txtFW"
        Me.txtFW.Size = New System.Drawing.Size(156, 20)
        Me.txtFW.TabIndex = 2
        '
        'txtSN
        '
        Me.txtSN.Location = New System.Drawing.Point(99, 77)
        Me.txtSN.Name = "txtSN"
        Me.txtSN.Size = New System.Drawing.Size(156, 20)
        Me.txtSN.TabIndex = 1
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(750, 24)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(156, 20)
        Me.TextBox4.TabIndex = 161
        Me.TextBox4.TabStop = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(697, 28)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(49, 13)
        Me.Label45.TabIndex = 158
        Me.Label45.Text = "Band Idx"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(23, 80)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(73, 13)
        Me.Label35.TabIndex = 9
        Me.Label35.Text = "Serial Number"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComPort
        '
        Me.txtComPort.Location = New System.Drawing.Point(416, 51)
        Me.txtComPort.Name = "txtComPort"
        Me.txtComPort.ReadOnly = True
        Me.txtComPort.Size = New System.Drawing.Size(156, 20)
        Me.txtComPort.TabIndex = 157
        Me.txtComPort.TabStop = False
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(363, 80)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(50, 13)
        Me.Label36.TabIndex = 10
        Me.Label36.Text = "FW Rev."
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAnalyzerModel
        '
        Me.txtAnalyzerModel.Location = New System.Drawing.Point(99, 51)
        Me.txtAnalyzerModel.Name = "txtAnalyzerModel"
        Me.txtAnalyzerModel.ReadOnly = True
        Me.txtAnalyzerModel.Size = New System.Drawing.Size(156, 20)
        Me.txtAnalyzerModel.TabIndex = 157
        Me.txtAnalyzerModel.TabStop = False
        '
        'txtFreqBand
        '
        Me.txtFreqBand.Location = New System.Drawing.Point(416, 23)
        Me.txtFreqBand.Name = "txtFreqBand"
        Me.txtFreqBand.ReadOnly = True
        Me.txtFreqBand.Size = New System.Drawing.Size(156, 20)
        Me.txtFreqBand.TabIndex = 157
        Me.txtFreqBand.TabStop = False
        '
        'txtAnalyzerVendor
        '
        Me.txtAnalyzerVendor.Location = New System.Drawing.Point(99, 23)
        Me.txtAnalyzerVendor.Name = "txtAnalyzerVendor"
        Me.txtAnalyzerVendor.ReadOnly = True
        Me.txtAnalyzerVendor.Size = New System.Drawing.Size(156, 20)
        Me.txtAnalyzerVendor.TabIndex = 157
        Me.txtAnalyzerVendor.TabStop = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(359, 55)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(53, 13)
        Me.Label38.TabIndex = 154
        Me.Label38.Text = "COM Port"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label42
        '
        Me.label42.AutoSize = True
        Me.label42.Location = New System.Drawing.Point(329, 27)
        Me.label42.Name = "label42"
        Me.label42.Size = New System.Drawing.Size(85, 13)
        Me.label42.TabIndex = 10
        Me.label42.Text = "Frequency Band"
        Me.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label41
        '
        Me.label41.AutoSize = True
        Me.label41.Location = New System.Drawing.Point(18, 54)
        Me.label41.Name = "label41"
        Me.label41.Size = New System.Drawing.Size(79, 13)
        Me.label41.TabIndex = 9
        Me.label41.Text = "Analyzer Model"
        Me.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label40
        '
        Me.label40.AutoSize = True
        Me.label40.Location = New System.Drawing.Point(13, 27)
        Me.label40.Name = "label40"
        Me.label40.Size = New System.Drawing.Size(84, 13)
        Me.label40.TabIndex = 8
        Me.label40.Text = "Analyzer Vendor"
        Me.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCompStatus
        '
        Me.lblCompStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCompStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompStatus.Location = New System.Drawing.Point(750, 24)
        Me.lblCompStatus.Name = "lblCompStatus"
        Me.lblCompStatus.Size = New System.Drawing.Size(156, 32)
        Me.lblCompStatus.TabIndex = 144
        Me.lblCompStatus.Text = "OFF"
        Me.lblCompStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblCompStatus.Visible = False
        '
        'linkGCProcedure
        '
        Me.linkGCProcedure.AutoSize = True
        Me.linkGCProcedure.Location = New System.Drawing.Point(29, 35)
        Me.linkGCProcedure.Name = "linkGCProcedure"
        Me.linkGCProcedure.Size = New System.Drawing.Size(99, 13)
        Me.linkGCProcedure.TabIndex = 1
        Me.linkGCProcedure.TabStop = True
        Me.linkGCProcedure.Text = "Proc. 82CAW04-06"
        '
        'lblLastGCDate
        '
        Me.lblLastGCDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastGCDate.Location = New System.Drawing.Point(413, 35)
        Me.lblLastGCDate.Name = "lblLastGCDate"
        Me.lblLastGCDate.Size = New System.Drawing.Size(198, 13)
        Me.lblLastGCDate.TabIndex = 148
        Me.lblLastGCDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label33
        '
        Me.label33.AutoSize = True
        Me.label33.Location = New System.Drawing.Point(198, 35)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(209, 13)
        Me.label33.TabIndex = 147
        Me.label33.Text = "Last Gage Capability Procedure Completed"
        Me.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label34
        '
        Me.label34.AutoSize = True
        Me.label34.Location = New System.Drawing.Point(637, 35)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(107, 13)
        Me.label34.TabIndex = 143
        Me.label34.Text = "Compensation Status"
        Me.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.label34.Visible = False
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.btnLoadProcedure)
        Me.groupBox4.Controls.Add(Me.linkGCProcedure)
        Me.groupBox4.Controls.Add(Me.lblLastGCDate)
        Me.groupBox4.Controls.Add(Me.label33)
        Me.groupBox4.Controls.Add(Me.lblCompStatus)
        Me.groupBox4.Controls.Add(Me.label34)
        Me.groupBox4.Location = New System.Drawing.Point(10, 129)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(1001, 78)
        Me.groupBox4.TabIndex = 101
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "Gage Procedure"
        '
        'btnLoadProcedure
        '
        Me.btnLoadProcedure.Enabled = False
        Me.btnLoadProcedure.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnLoadProcedure.Location = New System.Drawing.Point(750, 27)
        Me.btnLoadProcedure.Name = "btnLoadProcedure"
        Me.btnLoadProcedure.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.btnLoadProcedure.Size = New System.Drawing.Size(155, 29)
        Me.btnLoadProcedure.TabIndex = 2
        Me.btnLoadProcedure.Text = "&Load Procedure"
        Me.btnLoadProcedure.UseVisualStyleBackColor = True
        '
        'lblGageCapability
        '
        Me.lblGageCapability.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGageCapability.Location = New System.Drawing.Point(866, 196)
        Me.lblGageCapability.Name = "lblGageCapability"
        Me.lblGageCapability.Size = New System.Drawing.Size(107, 20)
        Me.lblGageCapability.TabIndex = 141
        '
        'label24
        '
        Me.label24.AutoSize = True
        Me.label24.Location = New System.Drawing.Point(384, 154)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(120, 13)
        Me.label24.TabIndex = 140
        Me.label24.Text = "Gage Capability (% P/T)"
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label31.Location = New System.Drawing.Point(834, 193)
        Me.label31.Name = "label31"
        Me.label31.Size = New System.Drawing.Size(21, 24)
        Me.label31.TabIndex = 140
        Me.label31.Text = "="
        '
        'label23
        '
        Me.label23.AutoSize = True
        Me.label23.Location = New System.Drawing.Point(28, 154)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(112, 13)
        Me.label23.TabIndex = 139
        Me.label23.Text = "Measurement Error (σ)"
        '
        'label30
        '
        Me.label30.AutoSize = True
        Me.label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label30.Location = New System.Drawing.Point(796, 197)
        Me.label30.Name = "label30"
        Me.label30.Size = New System.Drawing.Size(32, 18)
        Me.label30.TabIndex = 139
        Me.label30.Text = "100"
        '
        'label4
        '
        Me.label4.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(64, 207)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(39, 16)
        Me.label4.TabIndex = 103
        Me.label4.Text = "gage"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label29.Location = New System.Drawing.Point(772, 194)
        Me.label29.Name = "label29"
        Me.label29.Size = New System.Drawing.Size(18, 23)
        Me.label29.TabIndex = 138
        Me.label29.Text = "x"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.Location = New System.Drawing.Point(133, 211)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(26, 23)
        Me.label14.TabIndex = 112
        Me.label14.Text = "d₂"
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.Location = New System.Drawing.Point(601, 196)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(21, 24)
        Me.label27.TabIndex = 137
        Me.label27.Text = "="
        '
        'lblRBar
        '
        Me.lblRBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRBar.Location = New System.Drawing.Point(866, 100)
        Me.lblRBar.Name = "lblRBar"
        Me.lblRBar.Size = New System.Drawing.Size(102, 20)
        Me.lblRBar.TabIndex = 138
        '
        'lblTolerance2
        '
        Me.lblTolerance2.AutoSize = True
        Me.lblTolerance2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTolerance2.Location = New System.Drawing.Point(689, 214)
        Me.lblTolerance2.Name = "lblTolerance2"
        Me.lblTolerance2.Size = New System.Drawing.Size(0, 18)
        Me.lblTolerance2.TabIndex = 136
        '
        'lblMeasError1
        '
        Me.lblMeasError1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeasError1.Location = New System.Drawing.Point(300, 197)
        Me.lblMeasError1.Name = "lblMeasError1"
        Me.lblMeasError1.Size = New System.Drawing.Size(54, 23)
        Me.lblMeasError1.TabIndex = 111
        '
        'label25
        '
        Me.label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.Location = New System.Drawing.Point(629, 201)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(137, 13)
        Me.label25.TabIndex = 135
        Me.label25.Text = "------------------------------------"
        Me.label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.Location = New System.Drawing.Point(827, 98)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(33, 23)
        Me.label10.TabIndex = 137
        Me.label10.Text = "R̅ ="
        '
        'label26
        '
        Me.label26.AutoSize = True
        Me.label26.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.Location = New System.Drawing.Point(688, 182)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(18, 23)
        Me.label26.TabIndex = 134
        Me.label26.Text = "x"
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.Location = New System.Drawing.Point(273, 196)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(21, 24)
        Me.label13.TabIndex = 110
        Me.label13.Text = "="
        '
        'lblMultiplier2
        '
        Me.lblMultiplier2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiplier2.Location = New System.Drawing.Point(638, 185)
        Me.lblMultiplier2.Name = "lblMultiplier2"
        Me.lblMultiplier2.Size = New System.Drawing.Size(44, 23)
        Me.lblMultiplier2.TabIndex = 133
        Me.lblMultiplier2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMeasError2
        '
        Me.lblMeasError2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMeasError2.Location = New System.Drawing.Point(712, 185)
        Me.lblMeasError2.Name = "lblMeasError2"
        Me.lblMeasError2.Size = New System.Drawing.Size(54, 23)
        Me.lblMeasError2.TabIndex = 132
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.Location = New System.Drawing.Point(181, 196)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(21, 24)
        Me.label17.TabIndex = 109
        Me.label17.Text = "="
        '
        'lblTolerance1
        '
        Me.lblTolerance1.AutoSize = True
        Me.lblTolerance1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTolerance1.Location = New System.Drawing.Point(537, 214)
        Me.lblTolerance1.Name = "lblTolerance1"
        Me.lblTolerance1.Size = New System.Drawing.Size(0, 18)
        Me.lblTolerance1.TabIndex = 131
        '
        'dgvGRRSamples
        '
        Me.dgvGRRSamples.AllowUserToAddRows = False
        Me.dgvGRRSamples.AllowUserToDeleteRows = False
        Me.dgvGRRSamples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGRRSamples.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RunNumber, Me.N1, Me.N2, Me.N3, Me.N4, Me.N5, Me.Range})
        Me.dgvGRRSamples.Location = New System.Drawing.Point(31, 32)
        Me.dgvGRRSamples.MultiSelect = False
        Me.dgvGRRSamples.Name = "dgvGRRSamples"
        Me.dgvGRRSamples.ReadOnly = True
        Me.dgvGRRSamples.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvGRRSamples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGRRSamples.ShowEditingIcon = False
        Me.dgvGRRSamples.Size = New System.Drawing.Size(759, 92)
        Me.dgvGRRSamples.TabIndex = 134
        Me.dgvGRRSamples.TabStop = False
        '
        'label11
        '
        Me.label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label11.Location = New System.Drawing.Point(210, 201)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(57, 13)
        Me.label11.TabIndex = 108
        Me.label11.Text = "----------------"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblDsub2
        '
        Me.lblDsub2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDsub2.Location = New System.Drawing.Point(213, 212)
        Me.lblDsub2.Name = "lblDsub2"
        Me.lblDsub2.Size = New System.Drawing.Size(51, 18)
        Me.lblDsub2.TabIndex = 107
        Me.lblDsub2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRBarCalc
        '
        Me.lblRBarCalc.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRBarCalc.Location = New System.Drawing.Point(213, 182)
        Me.lblRBarCalc.Name = "lblRBarCalc"
        Me.lblRBarCalc.Size = New System.Drawing.Size(51, 23)
        Me.lblRBarCalc.TabIndex = 106
        Me.lblRBarCalc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label8
        '
        Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(118, 201)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(57, 13)
        Me.label8.TabIndex = 105
        Me.label8.Text = "----------------"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(99, 196)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(21, 24)
        Me.label6.TabIndex = 104
        Me.label6.Text = "="
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(49, 196)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(21, 24)
        Me.label3.TabIndex = 102
        Me.label3.Text = "σ"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(136, 182)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(20, 23)
        Me.label5.TabIndex = 101
        Me.label5.Text = "R̅"
        '
        'groupGC
        '
        Me.groupGC.Controls.Add(Me.label28)
        Me.groupGC.Controls.Add(Me.lblCompFactor)
        Me.groupGC.Controls.Add(Me.lblResult)
        Me.groupGC.Controls.Add(Me.label12)
        Me.groupGC.Controls.Add(Me.lblGageCapability)
        Me.groupGC.Controls.Add(Me.label24)
        Me.groupGC.Controls.Add(Me.label31)
        Me.groupGC.Controls.Add(Me.label23)
        Me.groupGC.Controls.Add(Me.label30)
        Me.groupGC.Controls.Add(Me.label4)
        Me.groupGC.Controls.Add(Me.label29)
        Me.groupGC.Controls.Add(Me.label14)
        Me.groupGC.Controls.Add(Me.label27)
        Me.groupGC.Controls.Add(Me.lblRBar)
        Me.groupGC.Controls.Add(Me.lblTolerance2)
        Me.groupGC.Controls.Add(Me.lblMeasError1)
        Me.groupGC.Controls.Add(Me.label25)
        Me.groupGC.Controls.Add(Me.label10)
        Me.groupGC.Controls.Add(Me.label26)
        Me.groupGC.Controls.Add(Me.label13)
        Me.groupGC.Controls.Add(Me.lblMultiplier2)
        Me.groupGC.Controls.Add(Me.lblMeasError2)
        Me.groupGC.Controls.Add(Me.label17)
        Me.groupGC.Controls.Add(Me.lblTolerance1)
        Me.groupGC.Controls.Add(Me.label22)
        Me.groupGC.Controls.Add(Me.label11)
        Me.groupGC.Controls.Add(Me.label21)
        Me.groupGC.Controls.Add(Me.dgvGRRSamples)
        Me.groupGC.Controls.Add(Me.label20)
        Me.groupGC.Controls.Add(Me.lblDsub2)
        Me.groupGC.Controls.Add(Me.lblMultiplier1)
        Me.groupGC.Controls.Add(Me.label19)
        Me.groupGC.Controls.Add(Me.lblRBarCalc)
        Me.groupGC.Controls.Add(Me.label18)
        Me.groupGC.Controls.Add(Me.btnRunTest)
        Me.groupGC.Controls.Add(Me.label16)
        Me.groupGC.Controls.Add(Me.label15)
        Me.groupGC.Controls.Add(Me.label8)
        Me.groupGC.Controls.Add(Me.label9)
        Me.groupGC.Controls.Add(Me.label3)
        Me.groupGC.Controls.Add(Me.label6)
        Me.groupGC.Controls.Add(Me.label5)
        Me.groupGC.Controls.Add(Me.label2)
        Me.groupGC.Controls.Add(Me.label1)
        Me.groupGC.Controls.Add(Me.label7)
        Me.groupGC.Enabled = False
        Me.groupGC.Location = New System.Drawing.Point(9, 502)
        Me.groupGC.Name = "groupGC"
        Me.groupGC.Size = New System.Drawing.Size(1002, 338)
        Me.groupGC.TabIndex = 104
        Me.groupGC.TabStop = False
        Me.groupGC.Text = "Gage Capability"
        '
        'label22
        '
        Me.label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.Location = New System.Drawing.Point(487, 201)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(111, 13)
        Me.label22.TabIndex = 130
        Me.label22.Text = "----------------------"
        Me.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.Location = New System.Drawing.Point(537, 182)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(18, 23)
        Me.label21.TabIndex = 129
        Me.label21.Text = "x"
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.Location = New System.Drawing.Point(559, 181)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(21, 24)
        Me.label20.TabIndex = 128
        Me.label20.Text = "σ"
        '
        'lblMultiplier1
        '
        Me.lblMultiplier1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultiplier1.Location = New System.Drawing.Point(494, 185)
        Me.lblMultiplier1.Name = "lblMultiplier1"
        Me.lblMultiplier1.Size = New System.Drawing.Size(41, 23)
        Me.lblMultiplier1.TabIndex = 127
        Me.lblMultiplier1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label19.Location = New System.Drawing.Point(467, 196)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(21, 24)
        Me.label19.TabIndex = 126
        Me.label19.Text = "="
        '
        'label18
        '
        Me.label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.Location = New System.Drawing.Point(430, 201)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(31, 13)
        Me.label18.TabIndex = 125
        Me.label18.Text = "------"
        Me.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnRunTest
        '
        Me.btnRunTest.Image = CType(resources.GetObject("btnRunTest.Image"), System.Drawing.Image)
        Me.btnRunTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRunTest.Location = New System.Drawing.Point(813, 32)
        Me.btnRunTest.Name = "btnRunTest"
        Me.btnRunTest.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        Me.btnRunTest.Size = New System.Drawing.Size(155, 29)
        Me.btnRunTest.TabIndex = 1
        Me.btnRunTest.Text = "&Collect Samples"
        Me.btnRunTest.UseVisualStyleBackColor = True
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.Location = New System.Drawing.Point(409, 194)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(24, 23)
        Me.label16.TabIndex = 124
        Me.label16.Text = "%"
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.Location = New System.Drawing.Point(434, 211)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(19, 23)
        Me.label15.TabIndex = 123
        Me.label15.Text = "T"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label9.Location = New System.Drawing.Point(436, 182)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(20, 23)
        Me.label9.TabIndex = 122
        Me.label9.Text = "P"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(747, 125)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(17, 16)
        Me.label2.TabIndex = 136
        Me.label2.Text = "R̅"
        Me.label2.Visible = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(703, 127)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(38, 13)
        Me.label1.TabIndex = 135
        Me.label1.Text = "R-bar"
        Me.label1.Visible = False
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(770, 125)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(17, 16)
        Me.label7.TabIndex = 133
        Me.label7.Text = "R̅"
        Me.label7.Visible = False
        '
        'gbAncillaryEquipment
        '
        Me.gbAncillaryEquipment.Controls.Add(Me.Label37)
        Me.gbAncillaryEquipment.Controls.Add(Me.cboTorqueWrenchSN)
        Me.gbAncillaryEquipment.Controls.Add(Me.Label43)
        Me.gbAncillaryEquipment.Controls.Add(Me.cboAdapterSN)
        Me.gbAncillaryEquipment.Controls.Add(Me.Label44)
        Me.gbAncillaryEquipment.Controls.Add(Me.cboTestLeadSN)
        Me.gbAncillaryEquipment.Controls.Add(Me.Label49)
        Me.gbAncillaryEquipment.Controls.Add(Me.Label50)
        Me.gbAncillaryEquipment.Controls.Add(Me.cboStandardSN)
        Me.gbAncillaryEquipment.Controls.Add(Me.cboTerminationSN)
        Me.gbAncillaryEquipment.Location = New System.Drawing.Point(10, 213)
        Me.gbAncillaryEquipment.Name = "gbAncillaryEquipment"
        Me.gbAncillaryEquipment.Size = New System.Drawing.Size(1001, 82)
        Me.gbAncillaryEquipment.TabIndex = 102
        Me.gbAncillaryEquipment.TabStop = False
        Me.gbAncillaryEquipment.Text = "Ancillary Equipment Information"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(305, 25)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(105, 13)
        Me.Label37.TabIndex = 148
        Me.Label37.Text = "Torque Wrench S/N"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTorqueWrenchSN
        '
        Me.cboTorqueWrenchSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTorqueWrenchSN.FormattingEnabled = True
        Me.cboTorqueWrenchSN.Items.AddRange(New Object() {"010IM-Y0001", "010IM-Y0002", "010IM-Y0003", "010IM-Y0004", "010IM-Y0005"})
        Me.cboTorqueWrenchSN.Location = New System.Drawing.Point(416, 22)
        Me.cboTorqueWrenchSN.Name = "cboTorqueWrenchSN"
        Me.cboTorqueWrenchSN.Size = New System.Drawing.Size(156, 21)
        Me.cboTorqueWrenchSN.TabIndex = 3
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(340, 55)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(67, 13)
        Me.Label43.TabIndex = 146
        Me.Label43.Text = "Adapter S/N"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboAdapterSN
        '
        Me.cboAdapterSN.FormattingEnabled = True
        Me.cboAdapterSN.Items.AddRange(New Object() {"010IM-Y0001", "010IM-Y0002", "010IM-Y0003", "010IM-Y0004", "010IM-Y0005"})
        Me.cboAdapterSN.Location = New System.Drawing.Point(416, 49)
        Me.cboAdapterSN.Name = "cboAdapterSN"
        Me.cboAdapterSN.Size = New System.Drawing.Size(156, 21)
        Me.cboAdapterSN.TabIndex = 4
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(666, 28)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(78, 13)
        Me.Label44.TabIndex = 144
        Me.Label44.Text = "Test Lead S/N"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTestLeadSN
        '
        Me.cboTestLeadSN.FormattingEnabled = True
        Me.cboTestLeadSN.Items.AddRange(New Object() {"14LEAD0000001", "14LEAD0000002", "14LEAD0000003", "14LEAD0000004", "14LEAD0000005"})
        Me.cboTestLeadSN.Location = New System.Drawing.Point(750, 25)
        Me.cboTestLeadSN.Name = "cboTestLeadSN"
        Me.cboTestLeadSN.Size = New System.Drawing.Size(156, 21)
        Me.cboTestLeadSN.TabIndex = 5
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(8, 52)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(85, 13)
        Me.Label49.TabIndex = 7
        Me.Label49.Text = "Termination S/N"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(20, 28)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(73, 13)
        Me.Label50.TabIndex = 6
        Me.Label50.Text = "Standard S/N"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboStandardSN
        '
        Me.cboStandardSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStandardSN.FormattingEnabled = True
        Me.cboStandardSN.Items.AddRange(New Object() {"010IM-Y0001", "010IM-Y0002", "010IM-Y0003", "010IM-Y0004", "010IM-Y0005"})
        Me.cboStandardSN.Location = New System.Drawing.Point(99, 22)
        Me.cboStandardSN.Name = "cboStandardSN"
        Me.cboStandardSN.Size = New System.Drawing.Size(156, 21)
        Me.cboStandardSN.TabIndex = 1
        '
        'cboTerminationSN
        '
        Me.cboTerminationSN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTerminationSN.FormattingEnabled = True
        Me.cboTerminationSN.Items.AddRange(New Object() {"010IM-T0001", "010IM-T0002", "010IM-T0003", "010IM-T0004", "010IM-T0005"})
        Me.cboTerminationSN.Location = New System.Drawing.Point(99, 49)
        Me.cboTerminationSN.Name = "cboTerminationSN"
        Me.cboTerminationSN.Size = New System.Drawing.Size(156, 21)
        Me.cboTerminationSN.TabIndex = 2
        '
        'RunNumber
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.RunNumber.DefaultCellStyle = DataGridViewCellStyle6
        Me.RunNumber.HeaderText = "Run #"
        Me.RunNumber.Name = "RunNumber"
        Me.RunNumber.ReadOnly = True
        Me.RunNumber.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.RunNumber.Width = 75
        '
        'N1
        '
        Me.N1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N1.DefaultCellStyle = DataGridViewCellStyle7
        Me.N1.HeaderText = "N1 Peak, dBc"
        Me.N1.Name = "N1"
        Me.N1.ReadOnly = True
        Me.N1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'N2
        '
        Me.N2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N2.DefaultCellStyle = DataGridViewCellStyle8
        Me.N2.HeaderText = "N2 Peak, dBc"
        Me.N2.Name = "N2"
        Me.N2.ReadOnly = True
        Me.N2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'N3
        '
        Me.N3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N3.DefaultCellStyle = DataGridViewCellStyle9
        Me.N3.HeaderText = "N3 Peak, dBc"
        Me.N3.Name = "N3"
        Me.N3.ReadOnly = True
        Me.N3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'N4
        '
        Me.N4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N4.DefaultCellStyle = DataGridViewCellStyle10
        Me.N4.HeaderText = "N4 Peak, dBc"
        Me.N4.Name = "N4"
        Me.N4.ReadOnly = True
        Me.N4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'N5
        '
        Me.N5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.N5.DefaultCellStyle = DataGridViewCellStyle11
        Me.N5.HeaderText = "N5 Peak, dBc"
        Me.N5.Name = "N5"
        Me.N5.ReadOnly = True
        Me.N5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'Range
        '
        Me.Range.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Range.DefaultCellStyle = DataGridViewCellStyle12
        Me.Range.HeaderText = "Range"
        Me.Range.Name = "Range"
        Me.Range.ReadOnly = True
        Me.Range.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'FormGRR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1027, 861)
        Me.Controls.Add(Me.gbAncillaryEquipment)
        Me.Controls.Add(Me.groupResIM)
        Me.Controls.Add(Me.groupConfiguration)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupGC)
        Me.Controls.Add(Me.dgvCompensation)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormGRR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PIM - Gage Capability & Uncertainty Compensation"
        Me.groupResIM.ResumeLayout(False)
        Me.groupResIM.PerformLayout()
        CType(Me.chartResIM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCompensation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupConfiguration.ResumeLayout(False)
        Me.groupConfiguration.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        CType(Me.dgvGRRSamples, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupGC.ResumeLayout(False)
        Me.groupGC.PerformLayout()
        Me.gbAncillaryEquipment.ResumeLayout(False)
        Me.gbAncillaryEquipment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblResIMResult As Label
    Private WithEvents label32 As Label
    Private WithEvents lblResIMPeak As Label
    Private WithEvents groupResIM As GroupBox
    Private WithEvents label28 As Label
    Private WithEvents lblCompFactor As Label
    Private WithEvents lblResult As Label
    Private WithEvents label12 As Label
    Private WithEvents dgvCompensation As DataGridView
    Private WithEvents RunNum As DataGridViewTextBoxColumn
    Private WithEvents Mean As DataGridViewTextBoxColumn
    Private WithEvents Variance As DataGridViewTextBoxColumn
    Private WithEvents StdDev As DataGridViewTextBoxColumn
    Private WithEvents CompFactor As DataGridViewTextBoxColumn
    Private WithEvents groupConfiguration As GroupBox
    Private WithEvents label41 As Label
    Private WithEvents label40 As Label
    Private WithEvents lblCompStatus As Label
    Private WithEvents linkGCProcedure As LinkLabel
    Private WithEvents lblLastGCDate As Label
    Private WithEvents label33 As Label
    Private WithEvents label34 As Label
    Private WithEvents groupBox4 As GroupBox
    Private WithEvents lblGageCapability As Label
    Private WithEvents label24 As Label
    Private WithEvents label31 As Label
    Private WithEvents label23 As Label
    Private WithEvents label30 As Label
    Private WithEvents label4 As Label
    Private WithEvents label29 As Label
    Private WithEvents label14 As Label
    Private WithEvents label27 As Label
    Private WithEvents lblRBar As Label
    Private WithEvents lblTolerance2 As Label
    Private WithEvents lblMeasError1 As Label
    Private WithEvents label25 As Label
    Private WithEvents label10 As Label
    Private WithEvents label26 As Label
    Private WithEvents label13 As Label
    Private WithEvents lblMultiplier2 As Label
    Private WithEvents lblMeasError2 As Label
    Private WithEvents label17 As Label
    Private WithEvents lblTolerance1 As Label
    Private WithEvents dgvGRRSamples As DataGridView
    Private WithEvents label11 As Label
    Private WithEvents lblDsub2 As Label
    Private WithEvents lblRBarCalc As Label
    Private WithEvents label8 As Label
    Private WithEvents label6 As Label
    Private WithEvents label3 As Label
    Private WithEvents label5 As Label
    Private WithEvents groupGC As GroupBox
    Private WithEvents label22 As Label
    Private WithEvents label21 As Label
    Private WithEvents label20 As Label
    Private WithEvents lblMultiplier1 As Label
    Private WithEvents label19 As Label
    Private WithEvents label18 As Label
    Public WithEvents btnRunTest As Button
    Private WithEvents label16 As Label
    Private WithEvents label15 As Label
    Private WithEvents label9 As Label
    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
    Private WithEvents label7 As Label
    Public WithEvents btnResIM As Button
    Private WithEvents chartResIM As DataVisualization.Charting.Chart
    Public WithEvents btnLoadProcedure As Button
    Private WithEvents gbAncillaryEquipment As GroupBox
    Private WithEvents Label37 As Label
    Private WithEvents cboTorqueWrenchSN As ComboBox
    Private WithEvents Label43 As Label
    Private WithEvents cboAdapterSN As ComboBox
    Private WithEvents Label44 As Label
    Private WithEvents cboTestLeadSN As ComboBox
    Private WithEvents Label49 As Label
    Private WithEvents Label50 As Label
    Private WithEvents cboStandardSN As ComboBox
    Private WithEvents cboTerminationSN As ComboBox
    Private WithEvents Label35 As Label
    Private WithEvents Label36 As Label
    Private WithEvents Label38 As Label
    Friend WithEvents txtAnalyzerModel As TextBox
    Friend WithEvents txtAnalyzerVendor As TextBox
    Friend WithEvents txtComPort As TextBox
    Friend WithEvents TextBox4 As TextBox
    Private WithEvents Label45 As Label
    Friend WithEvents txtFW As TextBox
    Friend WithEvents txtSN As TextBox
    Friend WithEvents txtFreqBand As TextBox
    Private WithEvents label42 As Label
    Friend WithEvents RunNumber As DataGridViewTextBoxColumn
    Friend WithEvents N1 As DataGridViewTextBoxColumn
    Friend WithEvents N2 As DataGridViewTextBoxColumn
    Friend WithEvents N3 As DataGridViewTextBoxColumn
    Friend WithEvents N4 As DataGridViewTextBoxColumn
    Friend WithEvents N5 As DataGridViewTextBoxColumn
    Friend WithEvents Range As DataGridViewTextBoxColumn
End Class
