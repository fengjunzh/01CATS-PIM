<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CATSRunBox
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
		Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
		Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
		Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
		Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
		Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series8 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
		Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
		Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Dim Series11 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
		Me.gbTestresult = New System.Windows.Forms.GroupBox()
		Me.gvTestresult = New System.Windows.Forms.DataGridView()
		Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.btnRunTest = New System.Windows.Forms.Button()
		Me.btnAbort = New System.Windows.Forms.Button()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.GroupBox1 = New System.Windows.Forms.GroupBox()
		Me.gbTestitem = New System.Windows.Forms.DataGridView()
		Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
		Me.GroupBox2 = New System.Windows.Forms.GroupBox()
		Me.btnRetry = New System.Windows.Forms.Button()
		Me.lblStatus = New System.Windows.Forms.LinkLabel()
		Me.gbTimeSweepLamda = New System.Windows.Forms.GroupBox()
		Me.ChartLamda = New System.Windows.Forms.DataVisualization.Charting.Chart()
		Me.gbTimeSweep = New System.Windows.Forms.GroupBox()
		Me.ChartSweepTime = New System.Windows.Forms.DataVisualization.Charting.Chart()
		Me.gbFreqSweep = New System.Windows.Forms.GroupBox()
		Me.ChartSweepFreq = New System.Windows.Forms.DataVisualization.Charting.Chart()
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.gbTestresult.SuspendLayout()
		CType(Me.gvTestresult, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.GroupBox1.SuspendLayout()
		CType(Me.gbTestitem, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbTimeSweepLamda.SuspendLayout()
		CType(Me.ChartLamda, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbTimeSweep.SuspendLayout()
		CType(Me.ChartSweepTime, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbFreqSweep.SuspendLayout()
		CType(Me.ChartSweepFreq, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.TableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbTestresult
		'
		Me.gbTestresult.Controls.Add(Me.gvTestresult)
		Me.gbTestresult.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbTestresult.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.gbTestresult.Location = New System.Drawing.Point(693, 362)
		Me.gbTestresult.Name = "gbTestresult"
		Me.gbTestresult.Size = New System.Drawing.Size(224, 175)
		Me.gbTestresult.TabIndex = 68
		Me.gbTestresult.TabStop = False
		Me.gbTestresult.Text = "Test Result"
		'
		'gvTestresult
		'
		Me.gvTestresult.AllowUserToAddRows = False
		Me.gvTestresult.AllowUserToDeleteRows = False
		Me.gvTestresult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.gvTestresult.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
		Me.gvTestresult.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gvTestresult.Location = New System.Drawing.Point(3, 16)
		Me.gvTestresult.Name = "gvTestresult"
		Me.gvTestresult.Size = New System.Drawing.Size(218, 156)
		Me.gvTestresult.TabIndex = 69
		'
		'Column1
		'
		Me.Column1.HeaderText = "Item"
		Me.Column1.Name = "Column1"
		'
		'Column2
		'
		Me.Column2.HeaderText = "Result"
		Me.Column2.Name = "Column2"
		'
		'Column3
		'
		Me.Column3.HeaderText = "Unit"
		Me.Column3.Name = "Column3"
		'
		'btnRunTest
		'
		Me.btnRunTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnRunTest.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnRunTest.Location = New System.Drawing.Point(658, 23)
		Me.btnRunTest.Name = "btnRunTest"
		Me.btnRunTest.Size = New System.Drawing.Size(80, 25)
		Me.btnRunTest.TabIndex = 69
		Me.btnRunTest.Text = "Run Test"
		Me.btnRunTest.UseVisualStyleBackColor = True
		'
		'btnAbort
		'
		Me.btnAbort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnAbort.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnAbort.Location = New System.Drawing.Point(748, 23)
		Me.btnAbort.Name = "btnAbort"
		Me.btnAbort.Size = New System.Drawing.Size(80, 25)
		Me.btnAbort.TabIndex = 70
		Me.btnAbort.Text = "Abort Test"
		Me.btnAbort.UseVisualStyleBackColor = True
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.ForeColor = System.Drawing.Color.Black
		Me.Label1.Location = New System.Drawing.Point(8, 22)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(60, 16)
		Me.Label1.TabIndex = 72
		Me.Label1.Text = "Test Item:"
		'
		'LinkLabel1
		'
		Me.LinkLabel1.AutoSize = True
		Me.LinkLabel1.Location = New System.Drawing.Point(74, 24)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(59, 13)
		Me.LinkLabel1.TabIndex = 73
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "LinkLabel1"
		'
		'LinkLabel2
		'
		Me.LinkLabel2.AutoSize = True
		Me.LinkLabel2.Location = New System.Drawing.Point(293, 24)
		Me.LinkLabel2.Name = "LinkLabel2"
		Me.LinkLabel2.Size = New System.Drawing.Size(59, 13)
		Me.LinkLabel2.TabIndex = 75
		Me.LinkLabel2.TabStop = True
		Me.LinkLabel2.Text = "LinkLabel2"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.ForeColor = System.Drawing.Color.Black
		Me.Label2.Location = New System.Drawing.Point(238, 22)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(54, 16)
		Me.Label2.TabIndex = 74
		Me.Label2.Text = "RET Tilt:"
		'
		'GroupBox1
		'
		Me.GroupBox1.Controls.Add(Me.gbTestitem)
		Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GroupBox1.Font = New System.Drawing.Font("Arial Narrow", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox1.Location = New System.Drawing.Point(693, 183)
		Me.GroupBox1.Name = "GroupBox1"
		Me.GroupBox1.Size = New System.Drawing.Size(224, 173)
		Me.GroupBox1.TabIndex = 76
		Me.GroupBox1.TabStop = False
		Me.GroupBox1.Text = "Test Status"
		'
		'gbTestitem
		'
		Me.gbTestitem.AllowUserToAddRows = False
		Me.gbTestitem.AllowUserToDeleteRows = False
		Me.gbTestitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.gbTestitem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column4})
		Me.gbTestitem.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbTestitem.Location = New System.Drawing.Point(3, 17)
		Me.gbTestitem.Name = "gbTestitem"
		Me.gbTestitem.Size = New System.Drawing.Size(218, 153)
		Me.gbTestitem.TabIndex = 71
		'
		'DataGridViewTextBoxColumn1
		'
		Me.DataGridViewTextBoxColumn1.HeaderText = "Test Item"
		Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
		'
		'Column4
		'
		Me.Column4.HeaderText = "Status"
		Me.Column4.Name = "Column4"
		'
		'GroupBox2
		'
		Me.GroupBox2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.GroupBox2.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.GroupBox2.Location = New System.Drawing.Point(693, 3)
		Me.GroupBox2.Name = "GroupBox2"
		Me.GroupBox2.Size = New System.Drawing.Size(224, 174)
		Me.GroupBox2.TabIndex = 77
		Me.GroupBox2.TabStop = False
		Me.GroupBox2.Text = "Test Message"
		'
		'btnRetry
		'
		Me.btnRetry.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnRetry.Location = New System.Drawing.Point(838, 23)
		Me.btnRetry.Name = "btnRetry"
		Me.btnRetry.Size = New System.Drawing.Size(84, 25)
		Me.btnRetry.TabIndex = 15
		Me.btnRetry.Text = "Retry Test"
		Me.btnRetry.UseVisualStyleBackColor = True
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.ForeColor = System.Drawing.Color.Green
		Me.lblStatus.LinkColor = System.Drawing.Color.Green
		Me.lblStatus.Location = New System.Drawing.Point(494, 20)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(65, 24)
		Me.lblStatus.TabIndex = 78
		Me.lblStatus.TabStop = True
		Me.lblStatus.Text = "PASS"
		'
		'gbTimeSweepLamda
		'
		Me.gbTimeSweepLamda.Controls.Add(Me.ChartLamda)
		Me.gbTimeSweepLamda.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbTimeSweepLamda.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.gbTimeSweepLamda.ForeColor = System.Drawing.SystemColors.Highlight
		Me.gbTimeSweepLamda.Location = New System.Drawing.Point(3, 362)
		Me.gbTimeSweepLamda.Name = "gbTimeSweepLamda"
		Me.gbTimeSweepLamda.Size = New System.Drawing.Size(684, 175)
		Me.gbTimeSweepLamda.TabIndex = 65
		Me.gbTimeSweepLamda.TabStop = False
		Me.gbTimeSweepLamda.Text = "[ Stability Check ]"
		'
		'ChartLamda
		'
		Me.ChartLamda.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		ChartArea1.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.None
		ChartArea1.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.None
		ChartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea1.AxisX.IsLabelAutoFit = False
		ChartArea1.AxisX.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea1.AxisX.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea1.AxisX.MajorTickMark.Enabled = False
		ChartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX.ScaleBreakStyle.Enabled = True
		ChartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Red
		ChartArea1.AxisX.ScrollBar.BackColor = System.Drawing.Color.Silver
		ChartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX.Title = "Time (Second)"
		ChartArea1.AxisX.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea1.AxisX2.Interval = 2.0R
		ChartArea1.AxisX2.IsLabelAutoFit = False
		ChartArea1.AxisX2.LabelAutoFitMaxFontSize = 8
		ChartArea1.AxisX2.LabelStyle.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea1.AxisX2.LabelStyle.Interval = 0R
		ChartArea1.AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
		ChartArea1.AxisX2.LabelStyle.TruncatedLabels = True
		ChartArea1.AxisX2.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisX2.MajorGrid.Enabled = False
		ChartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX2.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None
		ChartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX2.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisX2.TitleForeColor = System.Drawing.Color.Silver
		ChartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea1.AxisY.IsLabelAutoFit = False
		ChartArea1.AxisY.LabelAutoFitMaxFontSize = 8
		ChartArea1.AxisY.LabelAutoFitMinFontSize = 5
		ChartArea1.AxisY.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea1.AxisY.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisY.MajorGrid.Interval = 0R
		ChartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.[Auto]
		ChartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea1.AxisY.MajorTickMark.Interval = 0R
		ChartArea1.AxisY.MajorTickMark.Size = 0.5!
		ChartArea1.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisY.Title = "ƛ"
		ChartArea1.AxisY.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea1.AxisY2.IsLabelAutoFit = False
		ChartArea1.AxisY2.LineColor = System.Drawing.Color.Gray
		ChartArea1.AxisY2.MajorGrid.Enabled = False
		ChartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisY2.MajorTickMark.Enabled = False
		ChartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea1.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.LightGray
		ChartArea1.BackColor = System.Drawing.Color.White
		ChartArea1.BackSecondaryColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		ChartArea1.BorderColor = System.Drawing.Color.Gray
		ChartArea1.Name = "ChartArea0"
		Me.ChartLamda.ChartAreas.Add(ChartArea1)
		Legend1.BackColor = System.Drawing.Color.Transparent
		Legend1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top
		Legend1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled
		Legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
		Legend1.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Legend1.IsTextAutoFit = False
		Legend1.Name = "Legend1"
		Me.ChartLamda.Legends.Add(Legend1)
		Me.ChartLamda.Location = New System.Drawing.Point(6, 16)
		Me.ChartLamda.Name = "ChartLamda"
		Series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
		Series1.ChartArea = "ChartArea0"
		Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series1.Color = System.Drawing.Color.Red
		Series1.Legend = "Legend1"
		Series1.Name = "Limit"
		Series2.BorderWidth = 2
		Series2.ChartArea = "ChartArea0"
		Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series2.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Series2.Legend = "Legend1"
		Series2.Name = "2-Tone(ƛ)"
		Series2.YValuesPerPoint = 2
		Series3.ChartArea = "ChartArea0"
		Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
		Series3.Color = System.Drawing.Color.Blue
		Series3.Legend = "Legend1"
		Series3.MarkerSize = 8
		Series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
		Series3.Name = "Selected Point"
		Me.ChartLamda.Series.Add(Series1)
		Me.ChartLamda.Series.Add(Series2)
		Me.ChartLamda.Series.Add(Series3)
		Me.ChartLamda.Size = New System.Drawing.Size(672, 153)
		Me.ChartLamda.TabIndex = 3
		Me.ChartLamda.Text = "chart1"
		'
		'gbTimeSweep
		'
		Me.gbTimeSweep.Controls.Add(Me.ChartSweepTime)
		Me.gbTimeSweep.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbTimeSweep.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.gbTimeSweep.ForeColor = System.Drawing.SystemColors.Highlight
		Me.gbTimeSweep.Location = New System.Drawing.Point(3, 183)
		Me.gbTimeSweep.Name = "gbTimeSweep"
		Me.gbTimeSweep.Size = New System.Drawing.Size(684, 173)
		Me.gbTimeSweep.TabIndex = 64
		Me.gbTimeSweep.TabStop = False
		Me.gbTimeSweep.Text = "[ 2-Tone Time Domain Test ]"
		'
		'ChartSweepTime
		'
		Me.ChartSweepTime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		ChartArea2.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.None
		ChartArea2.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.None
		ChartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea2.AxisX.IsLabelAutoFit = False
		ChartArea2.AxisX.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea2.AxisX.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea2.AxisX.MajorTickMark.Enabled = False
		ChartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX.ScaleBreakStyle.Enabled = True
		ChartArea2.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Red
		ChartArea2.AxisX.ScrollBar.BackColor = System.Drawing.Color.Silver
		ChartArea2.AxisX.ScrollBar.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX.Title = "Time (Second)"
		ChartArea2.AxisX.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea2.AxisX2.Interval = 2.0R
		ChartArea2.AxisX2.IsLabelAutoFit = False
		ChartArea2.AxisX2.LabelAutoFitMaxFontSize = 8
		ChartArea2.AxisX2.LabelStyle.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea2.AxisX2.LabelStyle.Interval = 0R
		ChartArea2.AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
		ChartArea2.AxisX2.LabelStyle.TruncatedLabels = True
		ChartArea2.AxisX2.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisX2.MajorGrid.Enabled = False
		ChartArea2.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX2.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None
		ChartArea2.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX2.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisX2.TitleForeColor = System.Drawing.Color.Silver
		ChartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea2.AxisY.IsLabelAutoFit = False
		ChartArea2.AxisY.LabelAutoFitMaxFontSize = 8
		ChartArea2.AxisY.LabelAutoFitMinFontSize = 5
		ChartArea2.AxisY.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea2.AxisY.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisY.MajorGrid.Interval = 0R
		ChartArea2.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.[Auto]
		ChartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea2.AxisY.MajorTickMark.Interval = 0R
		ChartArea2.AxisY.MajorTickMark.Size = 0.5!
		ChartArea2.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisY.Title = "Pim (dBc)"
		ChartArea2.AxisY.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea2.AxisY2.IsLabelAutoFit = False
		ChartArea2.AxisY2.LineColor = System.Drawing.Color.Gray
		ChartArea2.AxisY2.MajorGrid.Enabled = False
		ChartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisY2.MajorTickMark.Enabled = False
		ChartArea2.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea2.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.LightGray
		ChartArea2.BackColor = System.Drawing.Color.White
		ChartArea2.BackSecondaryColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		ChartArea2.BorderColor = System.Drawing.Color.Gray
		ChartArea2.Name = "ChartArea0"
		Me.ChartSweepTime.ChartAreas.Add(ChartArea2)
		Legend2.BackColor = System.Drawing.Color.Transparent
		Legend2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top
		Legend2.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled
		Legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
		Legend2.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Legend2.IsTextAutoFit = False
		Legend2.Name = "Legend1"
		Me.ChartSweepTime.Legends.Add(Legend2)
		Me.ChartSweepTime.Location = New System.Drawing.Point(6, 19)
		Me.ChartSweepTime.Name = "ChartSweepTime"
		Series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
		Series4.ChartArea = "ChartArea0"
		Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series4.Color = System.Drawing.Color.Red
		Series4.Legend = "Legend1"
		Series4.Name = "Limit"
		Series5.BorderWidth = 2
		Series5.ChartArea = "ChartArea0"
		Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series5.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
		Series5.Legend = "Legend1"
		Series5.Name = "2-Tone"
		Series5.YValuesPerPoint = 2
		Series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
		Series6.ChartArea = "ChartArea0"
		Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series6.Color = System.Drawing.Color.Fuchsia
		Series6.Legend = "Legend1"
		Series6.Name = "-6STD"
		Series7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
		Series7.ChartArea = "ChartArea0"
		Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series7.Color = System.Drawing.Color.Fuchsia
		Series7.Legend = "Legend1"
		Series7.Name = "+6STD"
		Series8.ChartArea = "ChartArea0"
		Series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
		Series8.Color = System.Drawing.Color.Blue
		Series8.Legend = "Legend1"
		Series8.MarkerSize = 8
		Series8.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
		Series8.Name = "Selected Point"
		Me.ChartSweepTime.Series.Add(Series4)
		Me.ChartSweepTime.Series.Add(Series5)
		Me.ChartSweepTime.Series.Add(Series6)
		Me.ChartSweepTime.Series.Add(Series7)
		Me.ChartSweepTime.Series.Add(Series8)
		Me.ChartSweepTime.Size = New System.Drawing.Size(672, 148)
		Me.ChartSweepTime.TabIndex = 2
		Me.ChartSweepTime.Text = "chart1"
		'
		'gbFreqSweep
		'
		Me.gbFreqSweep.Controls.Add(Me.ChartSweepFreq)
		Me.gbFreqSweep.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbFreqSweep.Font = New System.Drawing.Font("Arial Narrow", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.gbFreqSweep.ForeColor = System.Drawing.SystemColors.Highlight
		Me.gbFreqSweep.Location = New System.Drawing.Point(3, 3)
		Me.gbFreqSweep.Name = "gbFreqSweep"
		Me.gbFreqSweep.Size = New System.Drawing.Size(684, 174)
		Me.gbFreqSweep.TabIndex = 63
		Me.gbFreqSweep.TabStop = False
		Me.gbFreqSweep.Text = "[ Freq Sweep ]"
		'
		'ChartSweepFreq
		'
		Me.ChartSweepFreq.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		ChartArea3.AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.None
		ChartArea3.AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.None
		ChartArea3.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea3.AxisX.IsLabelAutoFit = False
		ChartArea3.AxisX.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea3.AxisX.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea3.AxisX.MajorTickMark.Enabled = False
		ChartArea3.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX.ScaleBreakStyle.Enabled = True
		ChartArea3.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Red
		ChartArea3.AxisX.ScrollBar.BackColor = System.Drawing.Color.Silver
		ChartArea3.AxisX.ScrollBar.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX.Title = "Frequency (MHz)"
		ChartArea3.AxisX.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea3.AxisX2.Interval = 2.0R
		ChartArea3.AxisX2.IsLabelAutoFit = False
		ChartArea3.AxisX2.LabelAutoFitMaxFontSize = 8
		ChartArea3.AxisX2.LabelStyle.Font = New System.Drawing.Font("Arial", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea3.AxisX2.LabelStyle.Interval = 0R
		ChartArea3.AxisX2.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number
		ChartArea3.AxisX2.LabelStyle.TruncatedLabels = True
		ChartArea3.AxisX2.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisX2.MajorGrid.Enabled = False
		ChartArea3.AxisX2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX2.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None
		ChartArea3.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX2.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisX2.TitleForeColor = System.Drawing.Color.Silver
		ChartArea3.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount
		ChartArea3.AxisY.IsLabelAutoFit = False
		ChartArea3.AxisY.LabelAutoFitMaxFontSize = 8
		ChartArea3.AxisY.LabelAutoFitMinFontSize = 5
		ChartArea3.AxisY.LabelStyle.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea3.AxisY.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisY.MajorGrid.Interval = 0R
		ChartArea3.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.[Auto]
		ChartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot
		ChartArea3.AxisY.MajorTickMark.Interval = 0R
		ChartArea3.AxisY.MajorTickMark.Size = 0.5!
		ChartArea3.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisY.Title = "Pim (dBc)"
		ChartArea3.AxisY.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		ChartArea3.AxisY2.IsLabelAutoFit = False
		ChartArea3.AxisY2.LineColor = System.Drawing.Color.Gray
		ChartArea3.AxisY2.MajorGrid.Enabled = False
		ChartArea3.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisY2.MajorTickMark.Enabled = False
		ChartArea3.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Silver
		ChartArea3.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.LightGray
		ChartArea3.BackColor = System.Drawing.Color.White
		ChartArea3.BackSecondaryColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
		ChartArea3.BorderColor = System.Drawing.Color.Gray
		ChartArea3.Name = "ChartArea0"
		Me.ChartSweepFreq.ChartAreas.Add(ChartArea3)
		Legend3.BackColor = System.Drawing.Color.Transparent
		Legend3.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top
		Legend3.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Scaled
		Legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
		Legend3.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Legend3.IsTextAutoFit = False
		Legend3.Name = "Legend1"
		Legend3.TitleFont = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ChartSweepFreq.Legends.Add(Legend3)
		Me.ChartSweepFreq.Location = New System.Drawing.Point(6, 16)
		Me.ChartSweepFreq.Name = "ChartSweepFreq"
		Series9.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
		Series9.ChartArea = "ChartArea0"
		Series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series9.Color = System.Drawing.Color.Red
		Series9.Legend = "Legend1"
		Series9.Name = "Limit"
		Series10.BorderWidth = 2
		Series10.ChartArea = "ChartArea0"
		Series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series10.Color = System.Drawing.Color.Blue
		Series10.Legend = "Legend1"
		Series10.Name = "Sweep Up"
		Series10.YValuesPerPoint = 2
		Series11.BorderWidth = 2
		Series11.ChartArea = "ChartArea0"
		Series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
		Series11.Color = System.Drawing.Color.Green
		Series11.Legend = "Legend1"
		Series11.Name = "Sweep Down"
		Me.ChartSweepFreq.Series.Add(Series9)
		Me.ChartSweepFreq.Series.Add(Series10)
		Me.ChartSweepFreq.Series.Add(Series11)
		Me.ChartSweepFreq.Size = New System.Drawing.Size(672, 149)
		Me.ChartSweepFreq.TabIndex = 1
		Me.ChartSweepFreq.Text = "chart1"
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TableLayoutPanel1.ColumnCount = 2
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.gbFreqSweep, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.gbTimeSweep, 0, 1)
		Me.TableLayoutPanel1.Controls.Add(Me.gbTimeSweepLamda, 0, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.gbTestresult, 1, 2)
		Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 1, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 1, 1)
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 52)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 3
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33555!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33223!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33223!))
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(920, 540)
		Me.TableLayoutPanel1.TabIndex = 0
		'
		'CATSRunBox
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.Controls.Add(Me.TableLayoutPanel1)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.btnRetry)
		Me.Controls.Add(Me.LinkLabel2)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.LinkLabel1)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.btnAbort)
		Me.Controls.Add(Me.btnRunTest)
		Me.Name = "CATSRunBox"
		Me.Size = New System.Drawing.Size(926, 595)
		Me.gbTestresult.ResumeLayout(False)
		CType(Me.gvTestresult, System.ComponentModel.ISupportInitialize).EndInit()
		Me.GroupBox1.ResumeLayout(False)
		CType(Me.gbTestitem, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbTimeSweepLamda.ResumeLayout(False)
		CType(Me.ChartLamda, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbTimeSweep.ResumeLayout(False)
		CType(Me.ChartSweepTime, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbFreqSweep.ResumeLayout(False)
		CType(Me.ChartSweepFreq, System.ComponentModel.ISupportInitialize).EndInit()
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents gbTestresult As System.Windows.Forms.GroupBox
	Friend WithEvents gvTestresult As System.Windows.Forms.DataGridView
	Friend WithEvents btnRunTest As System.Windows.Forms.Button
	Friend WithEvents btnAbort As System.Windows.Forms.Button
	Friend WithEvents Label1 As System.Windows.Forms.Label
	Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
	Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
	Friend WithEvents Label2 As System.Windows.Forms.Label
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
	Friend WithEvents gbTestitem As System.Windows.Forms.DataGridView
	Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
	Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
	Friend WithEvents btnRetry As System.Windows.Forms.Button
	Friend WithEvents lblStatus As LinkLabel
	Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
	Friend WithEvents gbFreqSweep As GroupBox
	Public WithEvents ChartSweepFreq As DataVisualization.Charting.Chart
	Friend WithEvents gbTimeSweep As GroupBox
	Public WithEvents ChartSweepTime As DataVisualization.Charting.Chart
	Friend WithEvents gbTimeSweepLamda As GroupBox
	Public WithEvents ChartLamda As DataVisualization.Charting.Chart
End Class
