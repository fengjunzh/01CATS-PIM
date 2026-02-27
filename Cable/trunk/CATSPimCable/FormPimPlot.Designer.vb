<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPimPlot
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPimPlot))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSN = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSweepUpMaxOH = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtSweepDownMaxOH = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.gbResult = New System.Windows.Forms.GroupBox()
        Me.gbTestTime = New System.Windows.Forms.GroupBox()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblStartTime = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.lblStopTime = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.gbSweepHO = New System.Windows.Forms.GroupBox()
        Me.gbCommentHO = New System.Windows.Forms.GroupBox()
        Me.lblCommentHO = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSweepDownMaxHO = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSweepUpMaxHO = New System.Windows.Forms.TextBox()
        Me.txtSweepGapHO = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.gb2ToneOH = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txt_2tone_avg = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_2tone_std = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_2tone_lambda_percent = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_2tone_max = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.gbStsAOH = New System.Windows.Forms.GroupBox()
        Me.txt_StsA_stdev = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_StsA_max = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.gbSweepOH = New System.Windows.Forms.GroupBox()
        Me.gbCommentOH = New System.Windows.Forms.GroupBox()
        Me.lblCommentOH = New System.Windows.Forms.Label()
        Me.txtSweepGapOH = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtPN = New System.Windows.Forms.TextBox()
        Me.layoutTop = New System.Windows.Forms.TableLayoutPanel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.gbChartOH = New System.Windows.Forms.GroupBox()
        Me.chartOH = New C1.Win.C1Chart.C1Chart()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gbChartHO = New System.Windows.Forms.GroupBox()
        Me.chartHO = New C1.Win.C1Chart.C1Chart()
        Me.gbResult.SuspendLayout()
        Me.gbTestTime.SuspendLayout()
        Me.gbSweepHO.SuspendLayout()
        Me.gbCommentHO.SuspendLayout()
        Me.gb2ToneOH.SuspendLayout()
        Me.gbStsAOH.SuspendLayout()
        Me.gbSweepOH.SuspendLayout()
        Me.gbCommentOH.SuspendLayout()
        Me.layoutTop.SuspendLayout()
        Me.gbChartOH.SuspendLayout()
        CType(Me.chartOH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gbChartHO.SuspendLayout()
        CType(Me.chartHO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(253, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 30)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "PN"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSN
        '
        Me.txtSN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSN.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSN.Location = New System.Drawing.Point(53, 3)
        Me.txtSN.Name = "txtSN"
        Me.txtSN.ReadOnly = True
        Me.txtSN.Size = New System.Drawing.Size(194, 22)
        Me.txtSN.TabIndex = 81
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 30)
        Me.Label5.TabIndex = 80
        Me.Label5.Text = "SN"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSweepUpMaxOH
        '
        Me.txtSweepUpMaxOH.BackColor = System.Drawing.Color.White
        Me.txtSweepUpMaxOH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepUpMaxOH.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepUpMaxOH.ForeColor = System.Drawing.Color.Black
        Me.txtSweepUpMaxOH.Location = New System.Drawing.Point(91, 18)
        Me.txtSweepUpMaxOH.Name = "txtSweepUpMaxOH"
        Me.txtSweepUpMaxOH.ReadOnly = True
        Me.txtSweepUpMaxOH.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepUpMaxOH.TabIndex = 2
        Me.txtSweepUpMaxOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(163, 44)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(28, 15)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "dBc"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(163, 20)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(28, 15)
        Me.Label24.TabIndex = 14
        Me.Label24.Text = "dBc"
        '
        'txtSweepDownMaxOH
        '
        Me.txtSweepDownMaxOH.BackColor = System.Drawing.Color.White
        Me.txtSweepDownMaxOH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepDownMaxOH.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepDownMaxOH.ForeColor = System.Drawing.Color.Black
        Me.txtSweepDownMaxOH.Location = New System.Drawing.Point(91, 42)
        Me.txtSweepDownMaxOH.Name = "txtSweepDownMaxOH"
        Me.txtSweepDownMaxOH.ReadOnly = True
        Me.txtSweepDownMaxOH.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepDownMaxOH.TabIndex = 2
        Me.txtSweepDownMaxOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(163, 68)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(21, 15)
        Me.Label33.TabIndex = 18
        Me.Label33.Text = "dB"
        '
        'gbResult
        '
        Me.gbResult.Controls.Add(Me.gbTestTime)
        Me.gbResult.Controls.Add(Me.gbSweepHO)
        Me.gbResult.Controls.Add(Me.gb2ToneOH)
        Me.gbResult.Controls.Add(Me.gbStsAOH)
        Me.gbResult.Controls.Add(Me.gbSweepOH)
        Me.gbResult.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbResult.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbResult.Location = New System.Drawing.Point(1170, 3)
        Me.gbResult.Name = "gbResult"
        Me.gbResult.Size = New System.Drawing.Size(212, 766)
        Me.gbResult.TabIndex = 79
        Me.gbResult.TabStop = False
        Me.gbResult.Text = "[ Test Result ]"
        '
        'gbTestTime
        '
        Me.gbTestTime.Controls.Add(Me.lblDuration)
        Me.gbTestTime.Controls.Add(Me.Label4)
        Me.gbTestTime.Controls.Add(Me.lblStartTime)
        Me.gbTestTime.Controls.Add(Me.Label36)
        Me.gbTestTime.Controls.Add(Me.lblStopTime)
        Me.gbTestTime.Controls.Add(Me.Label39)
        Me.gbTestTime.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbTestTime.Location = New System.Drawing.Point(8, 329)
        Me.gbTestTime.Name = "gbTestTime"
        Me.gbTestTime.Size = New System.Drawing.Size(193, 113)
        Me.gbTestTime.TabIndex = 78
        Me.gbTestTime.TabStop = False
        Me.gbTestTime.Text = "Test time"
        '
        'lblDuration
        '
        Me.lblDuration.AutoSize = True
        Me.lblDuration.BackColor = System.Drawing.SystemColors.Control
        Me.lblDuration.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDuration.ForeColor = System.Drawing.Color.Black
        Me.lblDuration.Location = New System.Drawing.Point(84, 84)
        Me.lblDuration.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(13, 15)
        Me.lblDuration.TabIndex = 72
        Me.lblDuration.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(6, 84)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 15)
        Me.Label4.TabIndex = 71
        Me.Label4.Text = "Duration(s):"
        '
        'lblStartTime
        '
        Me.lblStartTime.AutoSize = True
        Me.lblStartTime.BackColor = System.Drawing.SystemColors.Control
        Me.lblStartTime.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartTime.ForeColor = System.Drawing.Color.Black
        Me.lblStartTime.Location = New System.Drawing.Point(48, 29)
        Me.lblStartTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStartTime.Name = "lblStartTime"
        Me.lblStartTime.Size = New System.Drawing.Size(13, 15)
        Me.lblStartTime.TabIndex = 68
        Me.lblStartTime.Text = "0"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(6, 29)
        Me.Label36.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(33, 15)
        Me.Label36.TabIndex = 67
        Me.Label36.Text = "Start:"
        '
        'lblStopTime
        '
        Me.lblStopTime.AutoSize = True
        Me.lblStopTime.BackColor = System.Drawing.SystemColors.Control
        Me.lblStopTime.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStopTime.ForeColor = System.Drawing.Color.Black
        Me.lblStopTime.Location = New System.Drawing.Point(47, 55)
        Me.lblStopTime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStopTime.Name = "lblStopTime"
        Me.lblStopTime.Size = New System.Drawing.Size(13, 15)
        Me.lblStopTime.TabIndex = 70
        Me.lblStopTime.Text = "0"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(6, 55)
        Me.Label39.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(33, 15)
        Me.Label39.TabIndex = 69
        Me.Label39.Text = "Stop:"
        '
        'gbSweepHO
        '
        Me.gbSweepHO.Controls.Add(Me.gbCommentHO)
        Me.gbSweepHO.Controls.Add(Me.Label10)
        Me.gbSweepHO.Controls.Add(Me.Label14)
        Me.gbSweepHO.Controls.Add(Me.txtSweepDownMaxHO)
        Me.gbSweepHO.Controls.Add(Me.Label16)
        Me.gbSweepHO.Controls.Add(Me.txtSweepUpMaxHO)
        Me.gbSweepHO.Controls.Add(Me.txtSweepGapHO)
        Me.gbSweepHO.Controls.Add(Me.Label18)
        Me.gbSweepHO.Controls.Add(Me.Label19)
        Me.gbSweepHO.Controls.Add(Me.Label25)
        Me.gbSweepHO.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSweepHO.Location = New System.Drawing.Point(8, 173)
        Me.gbSweepHO.Name = "gbSweepHO"
        Me.gbSweepHO.Size = New System.Drawing.Size(193, 150)
        Me.gbSweepHO.TabIndex = 77
        Me.gbSweepHO.TabStop = False
        Me.gbSweepHO.Text = "HO Sweep"
        '
        'gbCommentHO
        '
        Me.gbCommentHO.Controls.Add(Me.lblCommentHO)
        Me.gbCommentHO.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCommentHO.Location = New System.Drawing.Point(11, 94)
        Me.gbCommentHO.Name = "gbCommentHO"
        Me.gbCommentHO.Size = New System.Drawing.Size(180, 43)
        Me.gbCommentHO.TabIndex = 77
        Me.gbCommentHO.TabStop = False
        Me.gbCommentHO.Text = "Test comment"
        '
        'lblCommentHO
        '
        Me.lblCommentHO.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCommentHO.AutoSize = True
        Me.lblCommentHO.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommentHO.Location = New System.Drawing.Point(12, 19)
        Me.lblCommentHO.Name = "lblCommentHO"
        Me.lblCommentHO.Size = New System.Drawing.Size(33, 15)
        Me.lblCommentHO.TabIndex = 74
        Me.lblCommentHO.Text = "NULL"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(163, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(28, 15)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "dBc"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(163, 20)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(28, 15)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "dBc"
        '
        'txtSweepDownMaxHO
        '
        Me.txtSweepDownMaxHO.BackColor = System.Drawing.Color.White
        Me.txtSweepDownMaxHO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepDownMaxHO.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepDownMaxHO.ForeColor = System.Drawing.Color.Black
        Me.txtSweepDownMaxHO.Location = New System.Drawing.Point(91, 42)
        Me.txtSweepDownMaxHO.Name = "txtSweepDownMaxHO"
        Me.txtSweepDownMaxHO.ReadOnly = True
        Me.txtSweepDownMaxHO.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepDownMaxHO.TabIndex = 2
        Me.txtSweepDownMaxHO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(163, 68)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(21, 15)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "dB"
        '
        'txtSweepUpMaxHO
        '
        Me.txtSweepUpMaxHO.BackColor = System.Drawing.Color.White
        Me.txtSweepUpMaxHO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepUpMaxHO.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepUpMaxHO.ForeColor = System.Drawing.Color.Black
        Me.txtSweepUpMaxHO.Location = New System.Drawing.Point(91, 18)
        Me.txtSweepUpMaxHO.Name = "txtSweepUpMaxHO"
        Me.txtSweepUpMaxHO.ReadOnly = True
        Me.txtSweepUpMaxHO.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepUpMaxHO.TabIndex = 2
        Me.txtSweepUpMaxHO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSweepGapHO
        '
        Me.txtSweepGapHO.BackColor = System.Drawing.Color.White
        Me.txtSweepGapHO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepGapHO.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepGapHO.ForeColor = System.Drawing.Color.Black
        Me.txtSweepGapHO.Location = New System.Drawing.Point(91, 66)
        Me.txtSweepGapHO.Name = "txtSweepGapHO"
        Me.txtSweepGapHO.ReadOnly = True
        Me.txtSweepGapHO.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepGapHO.TabIndex = 6
        Me.txtSweepGapHO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(8, 68)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 15)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Gap up-down:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Black
        Me.Label19.Location = New System.Drawing.Point(8, 44)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(66, 15)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "Down max:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(8, 20)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(49, 15)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "Up max:"
        '
        'gb2ToneOH
        '
        Me.gb2ToneOH.Controls.Add(Me.Label22)
        Me.gb2ToneOH.Controls.Add(Me.txt_2tone_avg)
        Me.gb2ToneOH.Controls.Add(Me.Label23)
        Me.gb2ToneOH.Controls.Add(Me.Label34)
        Me.gb2ToneOH.Controls.Add(Me.Label1)
        Me.gb2ToneOH.Controls.Add(Me.txt_2tone_std)
        Me.gb2ToneOH.Controls.Add(Me.Label7)
        Me.gb2ToneOH.Controls.Add(Me.txt_2tone_lambda_percent)
        Me.gb2ToneOH.Controls.Add(Me.Label8)
        Me.gb2ToneOH.Controls.Add(Me.txt_2tone_max)
        Me.gb2ToneOH.Controls.Add(Me.Label9)
        Me.gb2ToneOH.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb2ToneOH.ForeColor = System.Drawing.Color.Black
        Me.gb2ToneOH.Location = New System.Drawing.Point(8, 519)
        Me.gb2ToneOH.Name = "gb2ToneOH"
        Me.gb2ToneOH.Size = New System.Drawing.Size(193, 134)
        Me.gb2ToneOH.TabIndex = 39
        Me.gb2ToneOH.TabStop = False
        Me.gb2ToneOH.Text = "2-Tone Time Domain"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(156, 47)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(31, 16)
        Me.Label22.TabIndex = 76
        Me.Label22.Text = "dBc"
        '
        'txt_2tone_avg
        '
        Me.txt_2tone_avg.BackColor = System.Drawing.Color.White
        Me.txt_2tone_avg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_2tone_avg.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_2tone_avg.Location = New System.Drawing.Point(78, 45)
        Me.txt_2tone_avg.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_2tone_avg.Name = "txt_2tone_avg"
        Me.txt_2tone_avg.ReadOnly = True
        Me.txt_2tone_avg.Size = New System.Drawing.Size(77, 22)
        Me.txt_2tone_avg.TabIndex = 75
        Me.txt_2tone_avg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(9, 47)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(63, 16)
        Me.Label23.TabIndex = 74
        Me.Label23.Text = "Average:"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(156, 74)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(18, 16)
        Me.Label34.TabIndex = 18
        Me.Label34.Text = "%"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(156, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "dBc"
        '
        'txt_2tone_std
        '
        Me.txt_2tone_std.BackColor = System.Drawing.Color.White
        Me.txt_2tone_std.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_2tone_std.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_2tone_std.ForeColor = System.Drawing.Color.Black
        Me.txt_2tone_std.Location = New System.Drawing.Point(78, 99)
        Me.txt_2tone_std.Name = "txt_2tone_std"
        Me.txt_2tone_std.ReadOnly = True
        Me.txt_2tone_std.Size = New System.Drawing.Size(77, 22)
        Me.txt_2tone_std.TabIndex = 6
        Me.txt_2tone_std.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 101)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 16)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Ctrn_Std:"
        '
        'txt_2tone_lambda_percent
        '
        Me.txt_2tone_lambda_percent.BackColor = System.Drawing.Color.White
        Me.txt_2tone_lambda_percent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_2tone_lambda_percent.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_2tone_lambda_percent.ForeColor = System.Drawing.Color.Black
        Me.txt_2tone_lambda_percent.Location = New System.Drawing.Point(78, 72)
        Me.txt_2tone_lambda_percent.Name = "txt_2tone_lambda_percent"
        Me.txt_2tone_lambda_percent.ReadOnly = True
        Me.txt_2tone_lambda_percent.Size = New System.Drawing.Size(77, 22)
        Me.txt_2tone_lambda_percent.TabIndex = 4
        Me.txt_2tone_lambda_percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(9, 74)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(69, 16)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "ƛ_Percent:"
        '
        'txt_2tone_max
        '
        Me.txt_2tone_max.BackColor = System.Drawing.Color.White
        Me.txt_2tone_max.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_2tone_max.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_2tone_max.ForeColor = System.Drawing.Color.Black
        Me.txt_2tone_max.Location = New System.Drawing.Point(78, 18)
        Me.txt_2tone_max.Name = "txt_2tone_max"
        Me.txt_2tone_max.ReadOnly = True
        Me.txt_2tone_max.Size = New System.Drawing.Size(77, 22)
        Me.txt_2tone_max.TabIndex = 2
        Me.txt_2tone_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(9, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 16)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Max:"
        '
        'gbStsAOH
        '
        Me.gbStsAOH.Controls.Add(Me.txt_StsA_stdev)
        Me.gbStsAOH.Controls.Add(Me.Label11)
        Me.gbStsAOH.Controls.Add(Me.Label13)
        Me.gbStsAOH.Controls.Add(Me.txt_StsA_max)
        Me.gbStsAOH.Controls.Add(Me.Label17)
        Me.gbStsAOH.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbStsAOH.ForeColor = System.Drawing.Color.Black
        Me.gbStsAOH.Location = New System.Drawing.Point(8, 659)
        Me.gbStsAOH.Name = "gbStsAOH"
        Me.gbStsAOH.Size = New System.Drawing.Size(193, 81)
        Me.gbStsAOH.TabIndex = 76
        Me.gbStsAOH.TabStop = False
        Me.gbStsAOH.Text = "STS-A"
        '
        'txt_StsA_stdev
        '
        Me.txt_StsA_stdev.BackColor = System.Drawing.Color.White
        Me.txt_StsA_stdev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_StsA_stdev.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_StsA_stdev.Location = New System.Drawing.Point(72, 45)
        Me.txt_StsA_stdev.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_StsA_stdev.Name = "txt_StsA_stdev"
        Me.txt_StsA_stdev.ReadOnly = True
        Me.txt_StsA_stdev.Size = New System.Drawing.Size(77, 22)
        Me.txt_StsA_stdev.TabIndex = 75
        Me.txt_StsA_stdev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(9, 47)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 15)
        Me.Label11.TabIndex = 74
        Me.Label11.Text = "Stdev"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(150, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(28, 15)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "dBc"
        '
        'txt_StsA_max
        '
        Me.txt_StsA_max.BackColor = System.Drawing.Color.White
        Me.txt_StsA_max.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_StsA_max.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_StsA_max.ForeColor = System.Drawing.Color.Black
        Me.txt_StsA_max.Location = New System.Drawing.Point(72, 18)
        Me.txt_StsA_max.Name = "txt_StsA_max"
        Me.txt_StsA_max.ReadOnly = True
        Me.txt_StsA_max.Size = New System.Drawing.Size(77, 22)
        Me.txt_StsA_max.TabIndex = 2
        Me.txt_StsA_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(9, 20)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(33, 15)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "Max:"
        '
        'gbSweepOH
        '
        Me.gbSweepOH.Controls.Add(Me.gbCommentOH)
        Me.gbSweepOH.Controls.Add(Me.Label29)
        Me.gbSweepOH.Controls.Add(Me.Label24)
        Me.gbSweepOH.Controls.Add(Me.txtSweepDownMaxOH)
        Me.gbSweepOH.Controls.Add(Me.Label33)
        Me.gbSweepOH.Controls.Add(Me.txtSweepUpMaxOH)
        Me.gbSweepOH.Controls.Add(Me.txtSweepGapOH)
        Me.gbSweepOH.Controls.Add(Me.Label21)
        Me.gbSweepOH.Controls.Add(Me.Label20)
        Me.gbSweepOH.Controls.Add(Me.Label15)
        Me.gbSweepOH.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbSweepOH.Location = New System.Drawing.Point(8, 18)
        Me.gbSweepOH.Name = "gbSweepOH"
        Me.gbSweepOH.Size = New System.Drawing.Size(193, 149)
        Me.gbSweepOH.TabIndex = 39
        Me.gbSweepOH.TabStop = False
        Me.gbSweepOH.Text = "OH Sweep"
        '
        'gbCommentOH
        '
        Me.gbCommentOH.Controls.Add(Me.lblCommentOH)
        Me.gbCommentOH.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCommentOH.Location = New System.Drawing.Point(7, 94)
        Me.gbCommentOH.Name = "gbCommentOH"
        Me.gbCommentOH.Size = New System.Drawing.Size(180, 43)
        Me.gbCommentOH.TabIndex = 76
        Me.gbCommentOH.TabStop = False
        Me.gbCommentOH.Text = "Test comment"
        '
        'lblCommentOH
        '
        Me.lblCommentOH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCommentOH.AutoSize = True
        Me.lblCommentOH.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommentOH.Location = New System.Drawing.Point(12, 19)
        Me.lblCommentOH.Name = "lblCommentOH"
        Me.lblCommentOH.Size = New System.Drawing.Size(33, 15)
        Me.lblCommentOH.TabIndex = 74
        Me.lblCommentOH.Text = "NULL"
        '
        'txtSweepGapOH
        '
        Me.txtSweepGapOH.BackColor = System.Drawing.Color.White
        Me.txtSweepGapOH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSweepGapOH.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSweepGapOH.ForeColor = System.Drawing.Color.Black
        Me.txtSweepGapOH.Location = New System.Drawing.Point(91, 66)
        Me.txtSweepGapOH.Name = "txtSweepGapOH"
        Me.txtSweepGapOH.ReadOnly = True
        Me.txtSweepGapOH.Size = New System.Drawing.Size(70, 22)
        Me.txtSweepGapOH.TabIndex = 6
        Me.txtSweepGapOH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(8, 68)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(85, 15)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "Gap up-down:"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Black
        Me.Label20.Location = New System.Drawing.Point(8, 44)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 15)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Down max:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Century Gothic", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(8, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(49, 15)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Up max:"
        '
        'txtPN
        '
        Me.txtPN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPN.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtPN.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPN.Location = New System.Drawing.Point(303, 3)
        Me.txtPN.Name = "txtPN"
        Me.txtPN.ReadOnly = True
        Me.txtPN.Size = New System.Drawing.Size(194, 22)
        Me.txtPN.TabIndex = 83
        '
        'layoutTop
        '
        Me.layoutTop.ColumnCount = 6
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutTop.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.layoutTop.Controls.Add(Me.Label5, 0, 0)
        Me.layoutTop.Controls.Add(Me.txtSN, 1, 0)
        Me.layoutTop.Controls.Add(Me.txtPN, 3, 0)
        Me.layoutTop.Controls.Add(Me.btnExit, 4, 0)
        Me.layoutTop.Controls.Add(Me.Label3, 2, 0)
        Me.layoutTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.layoutTop.Location = New System.Drawing.Point(0, 0)
        Me.layoutTop.Margin = New System.Windows.Forms.Padding(2)
        Me.layoutTop.Name = "layoutTop"
        Me.layoutTop.RowCount = 1
        Me.layoutTop.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.layoutTop.Size = New System.Drawing.Size(1385, 30)
        Me.layoutTop.TabIndex = 84
        '
        'btnExit
        '
        Me.btnExit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnExit.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(503, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 24)
        Me.btnExit.TabIndex = 68
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'gbChartOH
        '
        Me.gbChartOH.Controls.Add(Me.chartOH)
        Me.gbChartOH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbChartOH.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbChartOH.Location = New System.Drawing.Point(0, 0)
        Me.gbChartOH.Name = "gbChartOH"
        Me.gbChartOH.Size = New System.Drawing.Size(1163, 350)
        Me.gbChartOH.TabIndex = 75
        Me.gbChartOH.TabStop = False
        Me.gbChartOH.Text = "[ Freq Sweep ]"
        '
        'chartOH
        '
        Me.chartOH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartOH.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chartOH.Location = New System.Drawing.Point(3, 18)
        Me.chartOH.Margin = New System.Windows.Forms.Padding(2)
        Me.chartOH.Name = "chartOH"
        Me.chartOH.PropBag = resources.GetString("chartOH.PropBag")
        Me.chartOH.Size = New System.Drawing.Size(1157, 329)
        Me.chartOH.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 218.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.gbResult, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.SplitContainer1, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 30)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1385, 772)
        Me.TableLayoutPanel2.TabIndex = 85
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gbChartOH)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.gbChartHO)
        Me.SplitContainer1.Size = New System.Drawing.Size(1163, 768)
        Me.SplitContainer1.SplitterDistance = 350
        Me.SplitContainer1.SplitterIncrement = 4
        Me.SplitContainer1.SplitterWidth = 2
        Me.SplitContainer1.TabIndex = 80
        '
        'gbChartHO
        '
        Me.gbChartHO.Controls.Add(Me.chartHO)
        Me.gbChartHO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbChartHO.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbChartHO.Location = New System.Drawing.Point(0, 0)
        Me.gbChartHO.Name = "gbChartHO"
        Me.gbChartHO.Size = New System.Drawing.Size(1163, 416)
        Me.gbChartHO.TabIndex = 76
        Me.gbChartHO.TabStop = False
        Me.gbChartHO.Text = "[ Freq Sweep ]"
        '
        'chartHO
        '
        Me.chartHO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartHO.Font = New System.Drawing.Font("Century Gothic", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chartHO.Location = New System.Drawing.Point(3, 18)
        Me.chartHO.Margin = New System.Windows.Forms.Padding(2)
        Me.chartHO.Name = "chartHO"
        Me.chartHO.PropBag = resources.GetString("chartHO.PropBag")
        Me.chartHO.Size = New System.Drawing.Size(1157, 395)
        Me.chartHO.TabIndex = 0
        '
        'FormPimPlot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1385, 802)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.layoutTop)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "FormPimPlot"
        Me.Text = "FormPimPlot"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.gbResult.ResumeLayout(False)
        Me.gbTestTime.ResumeLayout(False)
        Me.gbTestTime.PerformLayout()
        Me.gbSweepHO.ResumeLayout(False)
        Me.gbSweepHO.PerformLayout()
        Me.gbCommentHO.ResumeLayout(False)
        Me.gbCommentHO.PerformLayout()
        Me.gb2ToneOH.ResumeLayout(False)
        Me.gb2ToneOH.PerformLayout()
        Me.gbStsAOH.ResumeLayout(False)
        Me.gbStsAOH.PerformLayout()
        Me.gbSweepOH.ResumeLayout(False)
        Me.gbSweepOH.PerformLayout()
        Me.gbCommentOH.ResumeLayout(False)
        Me.gbCommentOH.PerformLayout()
        Me.layoutTop.ResumeLayout(False)
        Me.layoutTop.PerformLayout()
        Me.gbChartOH.ResumeLayout(False)
        CType(Me.chartOH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gbChartHO.ResumeLayout(False)
        CType(Me.chartHO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents txtSN As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtSweepUpMaxOH As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents txtSweepDownMaxOH As TextBox
    Friend WithEvents Label33 As Label
    Friend WithEvents gbResult As GroupBox
    Friend WithEvents gb2ToneOH As GroupBox
    Friend WithEvents Label22 As Label
    Friend WithEvents txt_2tone_avg As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents Label34 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_2tone_std As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txt_2tone_lambda_percent As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txt_2tone_max As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents gbStsAOH As GroupBox
    Friend WithEvents txt_StsA_stdev As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txt_StsA_max As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents gbSweepOH As GroupBox
    Friend WithEvents txtSweepGapOH As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtPN As TextBox
    Friend WithEvents layoutTop As TableLayoutPanel
    Friend WithEvents gbChartOH As GroupBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents gbChartHO As GroupBox
    Friend WithEvents chartOH As C1.Win.C1Chart.C1Chart
    Friend WithEvents chartHO As C1.Win.C1Chart.C1Chart
    Friend WithEvents gbSweepHO As GroupBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents txtSweepDownMaxHO As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtSweepUpMaxHO As TextBox
    Friend WithEvents txtSweepGapHO As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label25 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents gbCommentHO As GroupBox
    Friend WithEvents lblCommentHO As Label
    Friend WithEvents gbCommentOH As GroupBox
    Friend WithEvents lblCommentOH As Label
    Friend WithEvents gbTestTime As GroupBox
    Friend WithEvents lblDuration As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblStartTime As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents lblStopTime As Label
    Friend WithEvents Label39 As Label
End Class
