Imports CATS.Model
Imports CATSPIM
Imports System.IO.Ports
Imports System.Collections.Generic
Imports Sunisoft.IrisSkin
Imports C1.Win.C1FlexGrid

Public Class FormGRR
    Private Const ResidualIMSpec As Decimal = -163.0
    Private Const Tolerance As Decimal = 6
    Private Const SigmaMultiplier As Decimal = 6
    Private Const d2 As Decimal = 2.326
    Private SampelRunCompletedTime As DateTime
    Private SampleRunId(2) As String
    Private SampleRange(2) As Decimal
    Private Means(2) As Decimal
    Private MeanDiffs(2, 4) As Decimal
    Private StdDevs(2) As Decimal
    Private Variances(2) As Decimal
    Private CorrFactors(2) As Decimal
    Private SamplePoints As List(Of GC.SamplePoint)
    Private GC_Procedure_Model As CATS.Model.GC_Procedure
    Private _AnalzyerSN As String
    Private _AnalyzerModel As String
    Private _AnalyzerVendor As String
    Private _AnalyzerFW As String
    Private _SWRevision As String
    Private _Instrument As DataModels.Instrument
    Private _SFBox As CATS.Model.cfg_imd_sfbox
    Private _GCProcedureDatePerformed As DateTime
    Private _ResIMTestTrace As Dictionary(Of Short, List(Of DataModels.FrequencyPoint))
    Private _ResIMCompleted As Boolean
    Private _SweepUpIM3Freqs As String
    Private _SweepUpIM3PowerdBm As String
    Private _SweepDownIM3Freqs As String
    Private _SweepDownIM3PowerdBm As String
    Private _SavedResIMSweepId As String
    Private _ResIMMeasureTime As DateTime
    Private _SweepUpPeakIM3Freq As Decimal
    Private _SweepDownPeakIM3Freq As Decimal
    Private _SweepUpPeakIM3PowerdBm As Decimal
    Private _SweepDownPeakIM3PowerdBm As Decimal
    Private _SweepPeakIM3PowerdBm As Decimal
    Private _GageCapability As Decimal
    Private _CompensationEnabled As Boolean
    Private _SampleRuns As Dictionary(Of Short, List(Of GC.SamplePoint))
    Private _ResIMQulificationResult As String
    Private _ApplyCorrection As Boolean
    Private _IsExpired As Boolean
    Private _MeetStdTolerance As Boolean
    Private _RBar As Decimal
    Private _MeasError As Decimal
    Private _CorrFactor As Decimal
    Private _InstrPowerLoss As New DataModels.PowerLoss
    Private _SamplePeakIMValue As Decimal
    Private _FreqBand As String
    Private _FreqBandModel As List(Of CATS.Model.cfg_imd_freq_band)
    Private _ComPort As String
    Private _BandIdx As String
    Private _VendorId As Short


#Region "FormGRR Property"
    Public Property AnalyzerSN As String
        Get
            Return _AnalzyerSN
        End Get
        Set(value As String)
            _AnalzyerSN = value
            txtSN.Text = value
        End Set
    End Property

    Public Property AnalyzerModel As String
        Get
            Return _AnalyzerModel
        End Get
        Set(value As String)
            _AnalyzerModel = value
            Me.txtAnalyzerModel.Text = value
        End Set
    End Property

    Public Property AnalyzerVendor As String
        Get
            Return _AnalyzerVendor
        End Get
        Set(value As String)
            _AnalyzerVendor = value
            Me.txtAnalyzerVendor.Text = value
        End Set
    End Property

    Public Property AnalyzerFW As String
        Get
            Return _AnalyzerFW
        End Get
        Set(value As String)
            _AnalyzerFW = value
            txtFW.Text = value
        End Set
    End Property

    Public Property SWRevision As String
        Get
            Return _SWRevision
        End Get
        Set(value As String)
            _SWRevision = value
        End Set
    End Property

    Public Property Instrument As DataModels.Instrument
        Get
            Return _Instrument
        End Get
        Set(value As DataModels.Instrument)
            _Instrument = value
        End Set
    End Property

    Public Property SFBox As cfg_imd_sfbox
        Get
            Return _SFBox
        End Get
        Set(value As cfg_imd_sfbox)
            _SFBox = value
        End Set
    End Property

    Public Property ResIMTestTrace As Dictionary(Of Short, List(Of DataModels.FrequencyPoint))
        Get
            Return _ResIMTestTrace
        End Get
        Set(value As Dictionary(Of Short, List(Of DataModels.FrequencyPoint)))
            _ResIMTestTrace = value
        End Set
    End Property

    Public Property GCProcedureDatePerformed As Date
        Get
            Return _GCProcedureDatePerformed
        End Get
        Set(value As Date)
            _GCProcedureDatePerformed = value
        End Set
    End Property

    Public Property ResIMCompleted As Boolean
        Get
            Return _ResIMCompleted
        End Get
        Set(value As Boolean)
            _ResIMCompleted = value
        End Set
    End Property

    Public Property SweepUpIM3Freqs As String
        Get
            Return _SweepUpIM3Freqs
        End Get
        Set(value As String)
            _SweepUpIM3Freqs = value
        End Set
    End Property

    Public Property SweepUpIM3PowerdBm As String
        Get
            Return _SweepUpIM3PowerdBm
        End Get
        Set(value As String)
            _SweepUpIM3PowerdBm = value
        End Set
    End Property

    Public Property SweepDownIM3Freqs As String
        Get
            Return _SweepDownIM3Freqs
        End Get
        Set(value As String)
            _SweepDownIM3Freqs = value
        End Set
    End Property

    Public Property SweepDownIM3PowerdBm As String
        Get
            Return _SweepDownIM3PowerdBm
        End Get
        Set(value As String)
            _SweepDownIM3PowerdBm = value
        End Set
    End Property

    Public Property ResIMMeasureTime As Date
        Get
            Return _ResIMMeasureTime
        End Get
        Set(value As Date)
            _ResIMMeasureTime = value
        End Set
    End Property

    Public Property SavedResIMSweepId As String
        Get
            Return _SavedResIMSweepId
        End Get
        Set(value As String)
            _SavedResIMSweepId = value
        End Set
    End Property

    Public Property SweepUpPeakIM3Freq As Decimal
        Get
            Return _SweepUpPeakIM3Freq
        End Get
        Set(value As Decimal)
            _SweepUpPeakIM3Freq = value
        End Set
    End Property

    Public Property SweepDownPeakIM3Freq As Decimal
        Get
            Return _SweepDownPeakIM3Freq
        End Get
        Set(value As Decimal)
            _SweepDownPeakIM3Freq = value
        End Set
    End Property

    Public Property SweepUpPeakIM3PowerdBm As Decimal
        Get
            Return _SweepUpPeakIM3PowerdBm
        End Get
        Set(value As Decimal)
            _SweepUpPeakIM3PowerdBm = value
        End Set
    End Property

    Public Property SweepDownPeakIM3PowerdBm As Decimal
        Get
            Return _SweepDownPeakIM3PowerdBm
        End Get
        Set(value As Decimal)
            _SweepDownPeakIM3PowerdBm = value
        End Set
    End Property

    Public Property SweepPeakIM3PowerdBm As Decimal
        Get
            Return _SweepPeakIM3PowerdBm
        End Get
        Set(value As Decimal)
            _SweepPeakIM3PowerdBm = value
        End Set
    End Property

    Public Property GageCapability As Decimal
        Get
            Return _GageCapability
        End Get
        Set(value As Decimal)
            _GageCapability = value
        End Set
    End Property

    Public Property CompensationEnabled As Boolean
        Get
            Return _CompensationEnabled
        End Get
        Set(value As Boolean)
            _CompensationEnabled = value
        End Set
    End Property

    Public Property SampleRuns As Dictionary(Of Short, List(Of GC.SamplePoint))
        Get
            Return _SampleRuns
        End Get
        Set(value As Dictionary(Of Short, List(Of GC.SamplePoint)))
            _SampleRuns = value
        End Set
    End Property

    Public Property ResIMQulificationResult As String
        Get
            Return _ResIMQulificationResult
        End Get
        Set(value As String)
            _ResIMQulificationResult = value
        End Set
    End Property

    Public Property ApplyCorrection As Boolean
        Get
            Return _ApplyCorrection
        End Get
        Set(value As Boolean)
            _ApplyCorrection = value
        End Set
    End Property

    Public Property IsExpired As Boolean
        Get
            Return _IsExpired
        End Get
        Set(value As Boolean)
            _IsExpired = value
        End Set
    End Property

    Public Property MeetStdTolerance As Boolean
        Get
            Return _MeetStdTolerance
        End Get
        Set(value As Boolean)
            _MeetStdTolerance = value
        End Set
    End Property

    Public Property RBar As Decimal
        Get
            Return _RBar
        End Get
        Set(value As Decimal)
            _RBar = value
        End Set
    End Property

    Public Property MeasError As Decimal
        Get
            Return _MeasError
        End Get
        Set(value As Decimal)
            _MeasError = value
        End Set
    End Property

    Public Property CorrFactor As Decimal
        Get
            Return _CorrFactor
        End Get
        Set(value As Decimal)
            _CorrFactor = value
        End Set
    End Property
    Public Property InstrPowerLoss() As DataModels.PowerLoss
        Get
            Return _InstrPowerLoss
        End Get
        Set(value As DataModels.PowerLoss)
            _InstrPowerLoss = value
        End Set
    End Property

    Public Property SamplePeakIMValue As Decimal
        Get
            Return _SamplePeakIMValue
        End Get
        Set(value As Decimal)
            _SamplePeakIMValue = value
        End Set
    End Property

    Public Property FreqBand As String
        Get
            Return _FreqBand
        End Get
        Set(value As String)
            _FreqBand = value
            Me.txtFreqBand.Text = value
        End Set
    End Property

    Public Property FreqBandModel As List(Of cfg_imd_freq_band)
        Get
            Return _FreqBandModel
        End Get
        Set(value As List(Of cfg_imd_freq_band))
            _FreqBandModel = value
        End Set
    End Property

    Public Property ComPort As String
        Get
            Return _ComPort
        End Get
        Set(value As String)
            _ComPort = value
            Me.txtComPort.Text = value
        End Set
    End Property
    Public Property BandIdx As String
        Get
            Return _BandIdx
        End Get
        Set(value As String)
            _BandIdx = value
            Me.TextBox4.Text = value
        End Set
    End Property

    Public Property VendorId As Short
        Get
            Return _VendorId
        End Get
        Set(value As Short)
            _VendorId = value
        End Set
    End Property
#End Region
    Private Sub linkGCProcedure_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkGCProcedure.LinkClicked
        ' Specify that the link was visited.
        Me.linkGCProcedure.LinkVisited = True

        ' Navigate to a URL.
        System.Diagnostics.Process.Start("https://commscope.sharepoint.com/:w:/r/sites/Quality-Andrew-Qsys/_layouts/15/Doc.aspx?sourcedoc=%7B02708071-1338-44A8-A9F3-C9E835C5A8F2%7D&file=82CAW04-06%20-%20PIM%20Gage%20Capability%20Procedure.doc&action=default&mobileredirect=true")
    End Sub

    Private Sub FormGRR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.UseWaitCursor = True
            Application.DoEvents()
            InitGridHistory()

            Dim cs As CellStyle
            ' Set test phases style
            cs = fgHistory.Styles.Add("Red")
            cs.BackColor = Color.Red
            cs = fgHistory.Styles.Add("Green")
            cs.BackColor = Color.Green

            ' Show version
            SWRevision = "1.0.0.1"
            Me.Text += "(Ver." & SWRevision & ")"

            If gPimTestConfig Is Nothing Then
                MsgBox("Don't find intrument configuration, please setup device", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Dim frmPimConfig = New FormConfig(gPimCfgFileName, gPimTestConfig)
                frmPimConfig.ShowDialog()
                Return
            End If

#If Not INSTR_NORMAL_TEST Then
            Dim instr As CInstrument = gPimTestConfig.InstrumentList.Find(Function(o) o.Enable = True)
            If instr IsNot Nothing Then
                TestModules.InitVirtualPimDevice(instr.FreqBand)
            Else
                Return
            End If
#End If
            AnalyzerVendor = gSelectedInstrument.Vendor
            AnalyzerModel = gSelectedInstrument.Model
            FreqBand = gSelectedInstrument.FreqBand
            ComPort = gSelectedInstrument.Address
            BandIdx = gSelectedInstrument.BandIdx

            Dim freqDesc As String = "STS-K_900M_43"
            If AnalyzerModel.StartsWith("PIM") Then
                Select Case AnalyzerModel
                    Case "PIM700L"
                        freqDesc = "STS-K_" & AnalyzerModel.Substring(3, 3).PadLeft(4, "0") & "ML_43"
                    Case "PIM700U"
                        freqDesc = "STS-K_" & AnalyzerModel.Substring(3, 3).PadLeft(4, "0") & "MU_43"
                    Case Else
                        freqDesc = "STS-K_" & AnalyzerModel.Substring(3).PadLeft(4, "0") & "M_43"
                End Select
            End If

            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager

            iv = ivBll.SelectByVendorName(AnalyzerVendor)
            If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & gSelectedInstrument.Vendor.Trim & ">")
            VendorId = iv.id
            ' Get swept frequency
            SFBox = GetSweepFreqs(freqDesc, VendorId)


#If INSTR_NORMAL_TEST Then
            Instrument = InitPimDevice()
#Else
            Instrument = InitVirtualPimDevice()
#End If

            If Instrument.SerialNumber.Length > 0 Then
                AnalyzerSN = Instrument.SerialNumber
            Else
                If gSelectedInstrument.SerialNumber.Length = 0 Then
                    MsgBox("The reading instrument serial number is empty, please input isntrument SN manually at device config", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Dim frmPimConfig = New FormConfig(gPimCfgFileName, gPimTestConfig)
                    frmPimConfig.ShowDialog()
                    Me.Close()
                    Return
                Else
                    AnalyzerSN = gSelectedInstrument.SerialNumber
                End If
            End If
            AnalyzerFW = Instrument.Firmware

            lblCompStatus.BackColor = Color.Gray
            lblCompStatus.Text = "OFF"
            lblResult.BackColor = Color.Gray
            lblResult.Text = "Gage Capability Measurement"
            lblResIMPeak.Text = String.Empty
            lblResIMResult.BackColor = Color.Gray
            lblResIMResult.Text = "NA"

            My.Application.DoEvents()

            ' Get accessory(starndard, low PIM load - termination, torque wrench, adapter, test lead)
            LoadAncillaryEquipment()

            If AnalyzerSN <> String.Empty Then
                LoadProcedure()
                'If GC_Procedure_Model IsNot Nothing Then
                '    If GC_Procedure_Model.AnalyzerModel <> AnalyzerModel Then
                '        MsgBox("Instrument setup up dosn't match the PIM analyzer connected, please setup device first", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
                '        Dim frmPimConfig = New FormConfig(gPimCfgFileName, gPimTestConfig)
                '        frmPimConfig.ShowDialog()
                '        Return
                '    End If
                'Else
                InitResIMChart(SFBox, -200, -163)
                'End If
                'Else
                '    ValidateForLoadProcedureButton()
                '    InitResIMChart(SFBox, -200, -163)
            End If

            Me.UseWaitCursor = False
            Application.DoEvents()

        Catch ex As Exception
            MsgBox("FormGRR_Load()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub InitResIMChart(sfReq As CATS.Model.cfg_imd_sfbox, ll As Single, ul As Single)
        Try
            If sfReq Is Nothing Then Return

            Dim tp_axis As DataModels.AxisMaxMin

            With Me.chartResIM

                .Series(0).Points.Clear() 'UpperLimit
                .Series(1).Points.Clear() 'SweepUp
                .Series(2).Points.Clear() 'SweepDown

                .ChartAreas(0).AxisY.MajorGrid.Interval = 10
                .ChartAreas(0).AxisY.Minimum = -190
                .ChartAreas(0).AxisY.Maximum = -150

                .ChartAreas(0).AxisX.MajorGrid.Interval = 2
                .ChartAreas(0).AxisX.LabelStyle.Interval = 4

                tp_axis = Calculate.GetImRange(sfReq)

                .ChartAreas(0).AxisX.Minimum = Math.Round(tp_axis.Min, 1)
                .ChartAreas(0).AxisX.Maximum = Math.Round(tp_axis.Max, 1)

                .Series(0).Points.AddXY(.ChartAreas(0).AxisX.Minimum, ul)
                .Series(0).Points.AddXY(.ChartAreas(0).AxisX.Maximum, ul)
                If ll > -180 Then
                    .Series(0).Points.AddXY(.ChartAreas(0).AxisX.Minimum, ll)
                    .Series(0).Points.AddXY(.ChartAreas(0).AxisX.Maximum, ll)
                End If
                My.Application.DoEvents()

            End With

        Catch ex As Exception
            Throw New Exception("FormGRR.InitResIMChart()::" & ex.Message)
        End Try
    End Sub
    Private Sub btnRunTest_Click(sender As Object, e As EventArgs) Handles btnRunTest.Click
        Try
            btnRunTest.Enabled = False
            btnResIM.Enabled = False

            lblLastGCDate.Text = "In progress"
            lblCompStatus.BackColor = Color.Gray
            lblCompStatus.Text = "Pending"

            lblResult.BackColor = Color.Gray
            lblResult.Text = "Collecting Samples..."

            dgvGRRSamples.Rows.Clear()
            ClearCalculationsGUI()

            Dim cancelProcedure As Boolean = False

            Dim intRowIdx As Integer = 0

            SampleRuns = New Dictionary(Of Short, List(Of GC.SamplePoint))

            For intRun As Integer = 1 To 3 Step 1
                lblResult.Text = "Run number " + intRun.ToString() + "..."
                My.Application.DoEvents()
                intRowIdx = dgvGRRSamples.Rows.Add()
                dgvGRRSamples.Rows(intRowIdx).Cells(0).Value = (intRowIdx + 1).ToString()
                Dim YesNo As DialogResult
                YesNo = MessageBox.Show("Connect the -110 dBm standard and the low PIM load, and click 'Yes' when finished.", "Gage Capability", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If YesNo = DialogResult.No Then
                    cancelProcedure = True
                    Exit For
                End If

                Dim GC_SampleRun_Model As New CATS.Model.GC_SampleRun
                Dim GC_SampleRub_Bll As New CATS.BLL.GC_SampleRunManager

                With GC_SampleRun_Model
                    Dim DateStamp As String = Now.Year.ToString() + Now.Month.ToString("00") + Now.Day.ToString("00")
                    DateStamp += Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")
                    .SampleRunID = pPlantCode & AnalyzerSN & DateStamp
                    SampleRunId(intRun - 1) = .SampleRunID
                    .ResidualIMSweepID = SavedResIMSweepId
                    .AnalyzerSN = AnalyzerSN
                    .StandardSN = cboStandardSN.Text
                    .TerminationSN = cboTerminationSN.Text
                    .TorqueWrenchSN = cboTorqueWrenchSN.Text
                    .AdapterSN = cboAdapterSN.Text
                    .TestLeadSN = cboTestLeadSN.Text
                    .MeasureDate = Today
                    .RunStartTime = Now
                    .TesterLogin = Environment.UserName
                    .WorkStation = Environment.MachineName
                    .TestInstrument = AnalyzerModel & Space(1) & AnalyzerSN
                    .AnalyzerVendor = AnalyzerVendor
                    .AnalyzerModel = AnalyzerModel
                End With

                SamplePoints = New List(Of GC.SamplePoint)

                For intSample As Integer = 1 To 5 Step 1
                    Dim samplePoint As New GC.SamplePoint
                    lblResult.Text = "Collecting sample " + intSample.ToString() + "..."
                    My.Application.DoEvents()
                    lblResult.Text = "Analyzing sample " + intSample.ToString() + " data..."
                    My.Application.DoEvents()
                    samplePoint.SampleNumber = intSample
#If INSTR_NORMAL_TEST Then
                    samplePoint.Peak = ReadSamplePeakValue(SFBox)
#Else

                    samplePoint.Peak = GetRandomNumber()
#End If
                    SamplePoints.Add(samplePoint)
                    dgvGRRSamples.Rows(intRowIdx).Cells(intSample).Value = samplePoint.Peak.ToString("##0.0")

                    VerifyMeetStdTolerance(samplePoint)

                    If Not MeetStdTolerance Then
                        ' Format cell if cell value is out of tolerance
                        Dim redCell As New DataGridViewCellStyle
                        redCell.ForeColor = Color.Red
                        Dim normalCell As New DataGridViewCellStyle
                        redCell.ForeColor = Color.Black
                        dgvGRRSamples.Rows(intRowIdx).Cells(intSample).Style = redCell
                        lblResult.BackColor = Color.Red
                        lblResult.Text = "Sample out of +/- 3 dBm tolerance at number of " & intRun & " run"
                        My.Application.DoEvents()
                        YesNo = MessageBox.Show("A sample was outside the +/- 3 dBm tolerance of the standard.  Continue collecting samples?", "ADCS Gage Capability", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If YesNo = DialogResult.No Then
                            cancelProcedure = True
                            Exit For
                        Else
                            YesNo = MessageBox.Show("Would you like to repeat Run #" & intRun & "?", "ADCS Gage Capability", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            If YesNo = DialogResult.Yes Then
                                intSample = 0
                                SamplePoints.Clear()
                                For i As Integer = 1 To 5
                                    Me.dgvGRRSamples.Rows(intRowIdx).Cells(i).Value = String.Empty
                                    dgvGRRSamples.Rows(intRowIdx).Cells(i).Style = normalCell
                                Next
                                Continue For
                            Else
                                cancelProcedure = True
                                Exit For
                            End If
                        End If
                    Else
                        lblResult.BackColor = Color.Gray
                        lblResult.Text = "Sample within +/- 3 dBm tolerance"
                        My.Application.DoEvents()
                    End If

                Next
                ' End samples

                If cancelProcedure Then
                    Exit For
                End If

                Dim range As Decimal = Calculate.GetGRRSamplesRange(SamplePoints)
                SampleRange(intRun - 1) = range
                dgvGRRSamples.Rows(intRowIdx).Cells(6).Value = range.ToString("##0.0")
                SampleRuns.Add(intRun, SamplePoints)
                With GC_SampleRun_Model
                    .N1PeakdBm = SamplePoints(0).Peak
                    .N2PeakdBm = SamplePoints(1).Peak
                    .N3PeakdBm = SamplePoints(2).Peak
                    .N4PeakdBm = SamplePoints(3).Peak
                    .N5PeakdBm = SamplePoints(4).Peak
                    .Range = range
                    .RunEndTime = Now
                    If intRun = 3 Then
                        SampelRunCompletedTime = .RunEndTime
                    End If
                End With
                GC_SampleRub_Bll.Add(GC_SampleRun_Model)
            Next
            ' End runs


            If cancelProcedure = True Then
                ClearCalculationsGUI()
                lblResult.BackColor = Color.Gray
                lblResult.Text = "Canceled"
                lblLastGCDate.Text = GCProcedureDatePerformed.ToString()
                If CompensationEnabled = True Then

                    lblCompStatus.Text = "ON"
                    lblCompStatus.BackColor = Color.Green
                    ApplyCorrection = True

                Else

                    lblCompStatus.Text = "OFF"
                    lblCompStatus.BackColor = Color.Red
                    ApplyCorrection = False

                End If
            Else
                VerifyMeetStdTolerance(SampleRuns)

                If MeetStdTolerance Then
                    lblResult.BackColor = Color.Gray
                    lblResult.Text = "All Samples within +/- 3 dBm tolerance"
                    My.Application.DoEvents()
                Else
                    lblResult.BackColor = Color.Red
                    lblResult.Text = "A sample was outside the +/- 3 dBm tolerance of the standard"
                    My.Application.DoEvents()
                End If

                lblDsub2.Text = d2.ToString("0.000")
                lblTolerance1.Text = Tolerance.ToString("0.##")
                lblTolerance2.Text = Tolerance.ToString("0.##")

                CalculateRBar(SampleRange)

                lblRBar.Text = RBar.ToString("#0.000")
                lblRBarCalc.Text = RBar.ToString("#0.000")
                Application.DoEvents()

                MeasError = RBar / d2

                lblMeasError1.Text = MeasError.ToString("#0.000")
                lblMeasError2.Text = MeasError.ToString("#0.000")
                Application.DoEvents()

                CalculateGageCapability()

                lblMultiplier1.Text = SigmaMultiplier.ToString("0.##")
                lblMultiplier2.Text = SigmaMultiplier.ToString("0.##")
                lblGageCapability.Text = GageCapability.ToString("#0.0") + "%"
                Application.DoEvents()

                DisplayGageResult()
                Application.DoEvents()

                CalculateMeans()

                CalculateVariances()

                CalculateStdDeviations()

                CalculateCorrFactors()

                DisplayCompResults(dgvCompensation)

                lblCompFactor.Text = CorrFactor.ToString("0.0") + " dBm"

                CompStatusFinalVerification()

                Dim GC_Procedure_Model As New CATS.Model.GC_Procedure
                Dim GC_Procedure_Bll As New CATS.BLL.GC_ProcedureManager
                With GC_Procedure_Model
                    Dim DateStamp As String = Now.Year.ToString() + Now.Month.ToString("00") + Now.Day.ToString("00")
                    DateStamp += Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")
                    .ProcedureID = pPlantCode & AnalyzerSN & DateStamp
                    .ResidualIMSweepID = SavedResIMSweepId
                    .SampleRunID1 = SampleRunId(0)
                    .SampleRunID2 = SampleRunId(1)
                    .SampleRunID3 = SampleRunId(2)
                    .AnalyzerSN = AnalyzerSN
                    .StandardSN = cboStandardSN.Text
                    .TerminationSN = cboTerminationSN.Text
                    .TorqueWrenchSN = cboTorqueWrenchSN.Text
                    .AdapterSN = cboAdapterSN.Text
                    .TestLeadSN = cboTestLeadSN.Text
                    .RBar = RBar
                    .DSub2 = d2
                    .MeasurementError = MeasError
                    .PTRatio = GageCapability
                    GCProcedureDatePerformed = Today
                    .DatePerformed = GCProcedureDatePerformed
                    .TimeCompleted = Now
                    .ResidualIMCompleted = ResIMMeasureTime
                    .SampleRunsCompleted = SampelRunCompletedTime
                    .CompFactordBm = CorrFactor
                    .CompStatus = IIf(CompensationEnabled, "ON", "OFF")
                    .TesterLogin = Environment.UserName
                    .Workstation = Environment.MachineName
                    .AnalyzerVendor = AnalyzerVendor
                    .AnalyzerModel = AnalyzerModel
                    .FWRevision = AnalyzerFW
                    .SWRevision = SWRevision
                End With

                GC_Procedure_Bll.Add(GC_Procedure_Model)

                If CompensationEnabled Then
                    lblCompStatus.BackColor = Color.Green
                    lblCompStatus.Text = "ON"
                    ApplyCorrection = True
                Else
                    lblCompStatus.BackColor = Color.Red
                    lblCompStatus.Text = "OFF"
                    ApplyCorrection = False
                End If

                lblLastGCDate.Text = GC_Procedure_Model.TimeCompleted.ToString("MM/dd/yyyy hh: mm:ss tt")

            End If

            btnResIM.Enabled = True
            btnRunTest.Enabled = True

        Catch ex As Exception
            MsgBox("btnRunTest_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub btnResIM_Click(sender As Object, e As EventArgs) Handles btnResIM.Click
        Try
            btnRunTest.Enabled = False
            btnResIM.Enabled = False

            dgvGRRSamples.Rows.Clear()
            ClearCalculationsGUI()

            AnalyzerSN = txtSN.Text

            ResIMCompleted = False
            Dim cancelResIMTest As Boolean = False
            ApplyCorrection = False

            Dim YesNo As DialogResult
            YesNo = MessageBox.Show("Connect the low PIM load, and click 'Yes' when finished.", "Measure Residual PIM", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If YesNo = DialogResult.No Then
                cancelResIMTest = True
            End If

            If Not cancelResIMTest Then
                chartResIM.Series("SweepUp").Points.Clear()
                chartResIM.Series("SweepDown").Points.Clear()
                lblResIMPeak.Text = String.Empty
                lblResIMResult.BackColor = Color.Gray
                lblResIMResult.Text = "Running..."
                My.Application.DoEvents()

                RunResIMTest(SFBox)

                Dim GC_ResIM_Model As New CATS.Model.GC_ResidualIM
                Dim GC_ResIM_Bll As New CATS.BLL.GC_ResidualIMManager

                With GC_ResIM_Model
                    Dim dateStamp As String = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00")
                    dateStamp += DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00")
                    SavedResIMSweepId = pPlantCode & AnalyzerSN & dateStamp
                    .ResidualIMSweepID = SavedResIMSweepId
                    .AnalyzerSN = AnalyzerSN
                    .MeasureDate = DateTime.Today
                    .MeasureTime = DateTime.Now
                    ResIMMeasureTime = .MeasureTime
                    .Plant = pPlantCode
                    .TerminationSN = cboTerminationSN.Text
                    .TestLeadSN = cboTestLeadSN.Text
                    .TorqueWrenchSN = cboTorqueWrenchSN.Text
                    .AdapterSN = cboAdapterSN.Text
                    .StartFrequency = chartResIM.ChartAreas("ChartArea1").AxisX.Minimum / 1000
                    .EndFrequency = chartResIM.ChartAreas("ChartArea1").AxisX.Maximum / 1000
                    .AnalyzerVendor = AnalyzerVendor
                    .AnalyzerModel = AnalyzerModel
                    .Spec = ResidualIMSpec '-163.0
                    .SweepUpNumberOfPoints = SweepUpIM3Freqs.Split(",").Length
                    .SweepUpIM3Freqs = SweepUpIM3Freqs
                    .SweepUpIM3PowerdBm = SweepUpIM3PowerdBm
                    .SweepUpPeakIM3Freq = SweepUpPeakIM3Freq / 1000
                    .SweepUpPeakIM3dBm = SweepUpPeakIM3PowerdBm
                    .SweepDownNumberOfPoints = SweepDownIM3Freqs.Split(",").Length
                    .SweepDownIM3Freqs = SweepDownIM3Freqs
                    .SweepDownIM3PowerdBm = SweepDownIM3PowerdBm
                    .SweepDownPeakIM3Freq = SweepDownPeakIM3Freq / 1000
                    .SweepDownPeakIM3dBm = SweepDownPeakIM3PowerdBm
                    .OverallPeakIM3dBm = Math.Max(SweepUpPeakIM3PowerdBm, SweepDownPeakIM3PowerdBm)
                    If .OverallPeakIM3dBm = SweepUpPeakIM3PowerdBm Then
                        .OverallPeakIM3Freq = SweepUpPeakIM3Freq / 1000
                    Else
                        .OverallPeakIM3Freq = SweepDownPeakIM3Freq / 1000
                    End If
                    .OverallPassFail = IIf(.OverallPeakIM3dBm > ResidualIMSpec, "F", "P")
                    .TesterLogin = Environment.UserName
                    .WorkStation = Environment.MachineName
                    .TestInstrument = AnalyzerModel & Space(1) & AnalyzerSN
                End With

                GC_ResIM_Bll.Add(GC_ResIM_Model)

                ResIMCompleted = True
            Else
                ResIMCompleted = False
            End If

            btnResIM.Enabled = True
            ValidateForGC()

        Catch ex As Exception
            MsgBox("btnResIM_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Sub ValidateResIMGUI()
        If Me.cboTerminationSN.SelectedIndex > -1 And Me.cboTorqueWrenchSN.SelectedIndex > -1 And Me.txtSN.Text.Length > 0 Then
            Me.groupResIM.Enabled = True
        Else
            Me.groupResIM.Enabled = False
        End If
    End Sub

    Private Sub cboTerminationSN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTerminationSN.SelectedIndexChanged
        ValidateResIMGUI()
    End Sub

    Private Sub cboTorqueWrenchSN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTorqueWrenchSN.SelectedIndexChanged
        ValidateResIMGUI()
    End Sub

    Private Sub RunResIMTest(sfReq As CATS.Model.cfg_imd_sfbox)
        Try
            ' Clear residual IM sweep up and down test data
            SweepUpIM3Freqs = String.Empty
            SweepUpIM3PowerdBm = String.Empty
            SweepDownIM3Freqs = String.Empty
            SweepDownIM3PowerdBm = String.Empty

            ' Init peak power of sweep up and down
            SweepUpPeakIM3PowerdBm = -200.0
            SweepDownPeakIM3PowerdBm = -200.0

            Dim txlFreq As Single
            Dim txrFreq As Single
            Dim rxFreq As Single
            Dim rxValue As Single

            Dim imdPoints As New List(Of DataModels.FrequencyPoint)
            Dim dspPoint As PointF
            Dim m_HighPowerTest As Boolean = False

#If INSTR_NORMAL_TEST Then
            'If HiPowerCheck() Then
            '    m_HighPowerTest = True
            'End If

            pPimDev.CheckPowerVersion(CSng(sfReq.power))

            pPimDev.ImdOrder = sfReq.imd_order
            pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
            pPimDev.SetRFPower(sfReq.power + InstrPowerLoss.TxL, sfReq.power + InstrPowerLoss.TxR)

            If m_HighPowerTest And sfReq.power > 43 Then
                pPimDev.RFPowerOnOff_OnePort(False, False)
                MyDelay(500)
                pPimDev.Send_And_Read("#MDNL0")
                MyDelay(500)
            End If

            pPimDev.RFPowerOnOff_OnePort(True, True)
            MyDelay(500)
#End If

            txrFreq = sfReq.ufreq_fixed

            If txrFreq <> 0 Then

#If INSTR_NORMAL_TEST Then
                pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
                pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
                MyDelay(100)
                pPimDev.RFPowerRampUp(sfReq.ufreq_start, sfReq.ufreq_fixed, "UPSWEEP", sfReq.ufreq_stop, sfReq.ufreq_step)
#End If

                For txlFreq = sfReq.ufreq_start To sfReq.ufreq_stop Step sfReq.ufreq_step

#If INSTR_NORMAL_TEST Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
                    MyDelay(10)
#End If

                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)

#If INSTR_NORMAL_TEST Then
                    pPimDev.SetPIMFreqMHz(rxFreq)
                    rxValue = ReadImdValue()

#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue

                    SweepUpIM3Freqs += rxFreq.ToString & ","
                    SweepUpIM3PowerdBm += rxValue.ToString & ","

                    Me.lblResIMResult.Text = rxValue

                    If rxValue > SweepUpPeakIM3PowerdBm Then
                        SweepUpPeakIM3PowerdBm = rxValue
                        SweepUpPeakIM3Freq = rxFreq
                    End If

                    Me.chartResIM.Series(1).Points.AddXY(rxFreq, rxValue)

                    My.Application.DoEvents()

                Next txlFreq

            End If

#If INSTR_NORMAL_TEST Then
            pPimDev.ClearEnd()
#End If

            ' end up sweep

            ' start down sweep
            txlFreq = sfReq.dfreq_fixed

            If txlFreq <> 0 Then

#If INSTR_NORMAL_TEST Then
                pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
                pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
                MyDelay(10)
                pPimDev.RFPowerRampUp(sfReq.dfreq_start, sfReq.dfreq_fixed, "DOWNSWEEP", sfReq.dfreq_stop, -sfReq.dfreq_step)

                If m_HighPowerTest And sfReq.power > 43 Then
                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    Threading.Thread.Sleep(500)
                    pPimDev.Send_And_Read("#MDNL1")
                    MyDelay(500)
                    pPimDev.RFPowerOnOff_OnePort(True, True)
                    MyDelay(500)
                End If
#End If

                For txrFreq = sfReq.dfreq_start To sfReq.dfreq_stop Step sfReq.dfreq_step * -1

#If INSTR_NORMAL_TEST Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
#End If
                    MyDelay(100)
                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)
#If INSTR_NORMAL_TEST Then
                    pPimDev.SetPIMFreqMHz(rxFreq)
                    rxValue = ReadImdValue()
#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue

                    SweepDownIM3Freqs += rxFreq.ToString & ","
                    SweepDownIM3PowerdBm += rxValue.ToString & ","

                    Me.lblResIMResult.Text = rxValue

                    If rxValue > SweepDownPeakIM3PowerdBm Then
                        SweepDownPeakIM3PowerdBm = rxValue
                        SweepDownPeakIM3Freq = rxFreq
                    End If

                    Me.chartResIM.Series(2).Points.AddXY(rxFreq, rxValue)

                    Application.DoEvents()

                Next txrFreq

            End If

#If INSTR_NORMAL_TEST Then
            pPimDev.ClearEnd()
#End If
            ' end down sweep
            SweepPeakIM3PowerdBm = Math.Max(SweepUpPeakIM3PowerdBm, SweepDownPeakIM3PowerdBm)
            Me.lblResIMPeak.Text = SweepPeakIM3PowerdBm
            If SweepPeakIM3PowerdBm <= ResidualIMSpec Then '-163.0
                ResIMQulificationResult = "P"
                lblResIMResult.BackColor = Color.Green
                lblResIMResult.Text = "PASS"
            Else
                ResIMQulificationResult = "F"
                lblResIMResult.BackColor = Color.Red
                lblResIMResult.Text = "FAIL"
            End If
            My.Application.DoEvents()

            SweepUpIM3Freqs = SweepUpIM3Freqs.TrimEnd(","c)
            SweepUpIM3PowerdBm = SweepUpIM3PowerdBm.TrimEnd(","c)
            SweepDownIM3Freqs = SweepDownIM3Freqs.TrimEnd(","c)
            SweepDownIM3PowerdBm = SweepDownIM3PowerdBm.TrimEnd(","c)

        Catch exa As AbortedException

            Throw New AbortedException("SweepFrequencyTest()::" & exa.Message)

        Catch ex As Exception

            Throw New Exception("SweepFrequencyTest()::" & ex.Message)

        Finally

            Try

#If INSTR_NORMAL_TEST Then
                pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
            Catch ex As Exception
                MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
            End Try
        End Try
    End Sub

    Private Function ReadSamplePeakValue(sfReq As CATS.Model.cfg_imd_sfbox) As Decimal
        Try

            SamplePeakIMValue = -200

            Dim txlFreq As Single
            Dim txrFreq As Single
            Dim rxFreq As Single
            Dim rxValue As Single

            Dim imdPoints As New List(Of DataModels.FrequencyPoint)
            Dim dspPoint As PointF
            Dim m_HighPowerTest As Boolean = False


#If INSTR_NORMAL_TEST Then
            'If HiPowerCheck() Then
            '    m_HighPowerTest = True
            'End If

            pPimDev.ImdOrder = sfReq.imd_order
            pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
            pPimDev.SetRFPower(sfReq.power + InstrPowerLoss.TxL, sfReq.power + InstrPowerLoss.TxR)

            If m_HighPowerTest And sfReq.power > 43 Then
                pPimDev.RFPowerOnOff_OnePort(False, False)
                MyDelay(500)
                pPimDev.Send_And_Read("#MDNL0")
                MyDelay(500)
            End If

            pPimDev.RFPowerOnOff_OnePort(True, True)
            MyDelay(500)
#End If


            txrFreq = sfReq.ufreq_fixed

            If txrFreq <> 0 Then

#If INSTR_NORMAL_TEST Then
                pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
                pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
                MyDelay(100)
                pPimDev.RFPowerRampUp(sfReq.ufreq_start, sfReq.ufreq_fixed, "UPSWEEP", sfReq.ufreq_stop, sfReq.ufreq_step)
#End If

                For txlFreq = sfReq.ufreq_start To sfReq.ufreq_stop Step sfReq.ufreq_step


#If INSTR_NORMAL_TEST Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
                    MyDelay(10)
#End If


#If INSTR_NORMAL_TEST Then
                    If m_HighPowerTest And sfReq.power > 43 Then
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        Threading.Thread.Sleep(500)
                        pPimDev.Send_And_Read("#MDNL1")
                        MyDelay(500)
                        pPimDev.RFPowerOnOff_OnePort(True, True)
                        MyDelay(500)
                    End If
#End If

                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)

#If INSTR_NORMAL_TEST Then
                    pPimDev.SetPIMFreqMHz(rxFreq)
                    rxValue = ReadImdValue()

#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue

                    If rxValue > SamplePeakIMValue Then
                        SamplePeakIMValue = rxValue
                    End If

                    My.Application.DoEvents()

                Next txlFreq

#If INSTR_NORMAL_TEST Then
                pPimDev.ClearEnd()
#End If

            End If

            ' end up sweep

            ' start down sweep
            txlFreq = sfReq.dfreq_fixed

            If txlFreq <> 0 Then

#If INSTR_NORMAL_TEST Then
                pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
                pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
                MyDelay(100)
                pPimDev.RFPowerRampUp(sfReq.dfreq_start, sfReq.dfreq_fixed, "DOWNSWEEP", sfReq.dfreq_stop, -sfReq.dfreq_step)
#End If

                For txrFreq = sfReq.dfreq_start To sfReq.dfreq_stop Step sfReq.dfreq_step * -1

#If INSTR_NORMAL_TEST Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
                    MyDelay(10)
#End If

#If INSTR_NORMAL_TEST Then
                    If m_HighPowerTest And sfReq.power > 43 Then
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        Threading.Thread.Sleep(500)
                        pPimDev.Send_And_Read("#MDNL1")
                        MyDelay(500)
                        pPimDev.RFPowerOnOff_OnePort(True, True)
                        MyDelay(500)
                    End If
#End If

                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)
#If INSTR_NORMAL_TEST Then
                    pPimDev.SetPIMFreqMHz(rxFreq)
                    rxValue = ReadImdValue()
#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue

                    If rxValue > SamplePeakIMValue Then
                        SamplePeakIMValue = rxValue
                    End If

                    Application.DoEvents()

                Next txrFreq

#If INSTR_NORMAL_TEST Then
                pPimDev.ClearEnd()
#End If

            End If
            ' end down sweep

            Return SamplePeakIMValue

        Catch exa As AbortedException

            Throw New AbortedException("SweepFrequencyTest()::" & exa.Message)

        Catch ex As Exception

            Throw New Exception("SweepFrequencyTest()::" & ex.Message)

        Finally

            Try

#If INSTR_NORMAL_TEST Then
                pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
            Catch ex As Exception
                MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
            End Try
        End Try
    End Function
    Private Sub ValidateForGC()
        If Me.cboStandardSN.SelectedIndex > -1 And ResIMCompleted = True And lblResIMResult.Text = "PASS" Then
            groupGC.Enabled = True
            btnRunTest.Enabled = True
        Else
            groupGC.Enabled = False
            btnRunTest.Enabled = False
        End If
    End Sub

    Private Function GetRandomNumber() As Single
        Try
            MyDelay(200)
            Randomize()
            Dim x As Single
            Dim rnd As New Random
            x = (rnd.NextDouble * 2 - 1) * 4 + -153.0
            Return x
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Sub VerifyMeetStdTolerance(ByVal sample As GC.SamplePoint)
        Try
            MeetStdTolerance = True
            If sample.Peak > -150 Or sample.Peak < -156 Then
                MeetStdTolerance = False
            End If
        Catch ex As Exception
            Throw New Exception("GC.VerifyMeetTolerance()::" & ex.Message)
        End Try
    End Sub

    Private Sub VerifyMeetStdTolerance(ByVal sampleRuns As Dictionary(Of Short, List(Of GC.SamplePoint)))
        Try
            MeetStdTolerance = True
            For Each run In sampleRuns
                For Each sample In run.Value
                    If sample.Peak < -156 Or sample.Peak > -150 Then
                        MeetStdTolerance = False
                        Return
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception("GC.VerifyMeetTolerance()::" & ex.Message)
        End Try
    End Sub

    Private Sub ClearCalculationsGUI()
        lblRBar.Text = String.Empty
        lblRBarCalc.Text = String.Empty
        lblMeasError1.Text = String.Empty
        lblMeasError2.Text = String.Empty
        lblTolerance1.Text = String.Empty
        lblTolerance2.Text = String.Empty
        lblGageCapability.Text = String.Empty
        lblResult.Text = String.Empty
    End Sub

    Private Sub LoadAncillaryEquipment()
        Try
            Dim GC_AncillaryEquipment_List As List(Of CATS.Model.GC_AncillaryEquipment)
            Dim GC_AncillaryEquipment_BLL As New CATS.BLL.GC_AncillaryEquipmentManager

            GC_AncillaryEquipment_List = GC_AncillaryEquipment_BLL.SelectRowsByPlant(pPlantCode)

            If GC_AncillaryEquipment_List IsNot Nothing Then
                Me.cboStandardSN.DataSource = GC_AncillaryEquipment_List.Where(Function(o) o.Type = "Standard").ToList
                Me.cboTerminationSN.DataSource = GC_AncillaryEquipment_List.Where(Function(o) o.Type = "Termination").ToList
                Me.cboTorqueWrenchSN.DataSource = GC_AncillaryEquipment_List.Where(Function(o) o.Type = "TorqueWrench").ToList
            End If

        Catch ex As Exception
            Throw New Exception("FormGRR.LoadAncillaryEquipment()::" & ex.Message)
        End Try
    End Sub

    Private Sub DisplayGageProcedure()
        MeetStdTolerance = True
        If CompensationEnabled = True Then
            lblCompStatus.Text = "ON"
            lblCompStatus.BackColor = Color.Green
        Else
            lblCompStatus.Text = "OFF"
            lblCompStatus.BackColor = Color.Red
        End If

        DisplayGageResult()
        lblResult.BackColor = Color.Gray

        cboStandardSN.Text = GC_Procedure_Model.StandardSN
        cboTerminationSN.Text = GC_Procedure_Model.TerminationSN
        cboTorqueWrenchSN.Text = GC_Procedure_Model.TorqueWrenchSN
        cboAdapterSN.Text = GC_Procedure_Model.AdapterSN
        cboTestLeadSN.Text = GC_Procedure_Model.TestLeadSN

        ' Get GC_ResidualIM data
        Dim GC_ResIM_Model As CATS.Model.GC_ResidualIM
        Dim GC_ResIM_Bll As New CATS.BLL.GC_ResidualIMManager
        GC_ResIM_Model = GC_ResIM_Bll.SelectById(GC_Procedure_Model.ResidualIMSweepID)

        ' Plot Residual IM
        chartResIM.Series("UpperLimit").Points.Clear()
        chartResIM.Series("SweepUp").Points.Clear()
        chartResIM.Series("SweepDown").Points.Clear()
        chartResIM.ChartAreas("ChartArea1").AxisX.Minimum = Convert.ToDouble(GC_ResIM_Model.StartFrequency * 1000)
        chartResIM.ChartAreas("ChartArea1").AxisX.Maximum = Convert.ToDouble(GC_ResIM_Model.EndFrequency * 1000)
        chartResIM.Series(0).Points.AddXY(chartResIM.ChartAreas(0).AxisX.Minimum, -163)
        chartResIM.Series(0).Points.AddXY(chartResIM.ChartAreas(0).AxisX.Maximum, -163)
        AddPointToResIMTesTrace(1, GC_ResIM_Model.SweepUpIM3Freqs, GC_ResIM_Model.SweepUpIM3PowerdBm)
        AddPointToResIMTesTrace(2, GC_ResIM_Model.SweepDownIM3Freqs, GC_ResIM_Model.SweepDownIM3PowerdBm)

        For Each sweep In ResIMTestTrace
            For Each point In sweep.Value
                chartResIM.Series(sweep.Key).Points.AddXY(Convert.ToSingle(point.XData), Convert.ToSingle(point.YData))
            Next
        Next

        lblResIMPeak.Text = GC_ResIM_Model.OverallPeakIM3dBm.ToString("#00.0")
        If (GC_ResIM_Model.OverallPassFail = "P") Then
            lblResIMResult.Text = "PASS"
            lblResIMResult.BackColor = Color.Green
        Else
            lblResIMResult.Text = "FAIL"
            lblResIMResult.BackColor = Color.Red
        End If

        ' Get GC_SampleRun data
        Dim GC_SampleRun_Model As CATS.Model.GC_SampleRun
        Dim GC_SampleRun_Bll As New CATS.BLL.GC_SampleRunManager
        ' Get SampleRunID array
        Dim sampleRunId(2) As String
        sampleRunId(0) = GC_Procedure_Model.SampleRunID1
        sampleRunId(1) = GC_Procedure_Model.SampleRunID2
        sampleRunId(2) = GC_Procedure_Model.SampleRunID3
        ' Samples
        Dim intRowIdx As Integer = 0
        For intRun As Integer = 0 To 2
            intRowIdx = Me.dgvGRRSamples.Rows.Add()
            Me.dgvGRRSamples.Rows(intRowIdx).Cells(0).Value = (intRowIdx + 1).ToString
            GC_SampleRun_Model = GC_SampleRun_Bll.SelectById(sampleRunId(intRun))
            Dim samplePeakdBm(4) As Decimal
            samplePeakdBm(0) = GC_SampleRun_Model.N1PeakdBm.ToString("##0.0")
            samplePeakdBm(1) = GC_SampleRun_Model.N2PeakdBm.ToString("##0.0")
            samplePeakdBm(2) = GC_SampleRun_Model.N3PeakdBm.ToString("##0.0")
            samplePeakdBm(3) = GC_SampleRun_Model.N4PeakdBm.ToString("##0.0")
            samplePeakdBm(4) = GC_SampleRun_Model.N5PeakdBm.ToString("##0.0")
            For intSample As Integer = 0 To 4
                Me.dgvGRRSamples.Rows(intRowIdx).Cells(intSample + 1).Value = samplePeakdBm(intSample)

            Next
            For intSample As Integer = 0 To 4
                If samplePeakdBm(intSample) > -150 Or samplePeakdBm(intSample) < -156 Then
                    MeetStdTolerance = False
                    Exit For
                End If
            Next
        Next

        ' Calculations
        lblRBar.Text = GC_Procedure_Model.RBar.ToString("#0.000")
        lblDsub2.Text = GC_Procedure_Model.DSub2.ToString("0.000")
        lblTolerance1.Text = Tolerance.ToString("0.##")
        lblTolerance2.Text = Tolerance.ToString("0.##")
        lblRBarCalc.Text = GC_Procedure_Model.RBar.ToString("#0.000")
        lblMeasError1.Text = GC_Procedure_Model.MeasurementError.ToString("#0.000")
        lblMeasError2.Text = GC_Procedure_Model.MeasurementError.ToString("#0.000")
        lblMultiplier1.Text = SigmaMultiplier.ToString("0.##")
        lblMultiplier2.Text = SigmaMultiplier.ToString("0.##")
        lblGageCapability.Text = GageCapability.ToString("#0.0") + "%"

        My.Application.DoEvents()

    End Sub

    Private Sub DisplayGageResult()
        If Not GageCapability = 0 Then
            If GageCapability < 10 Then
                lblResult.Text = "Gage Capability is Excellent ( < 10% )"
                lblResult.BackColor = System.Drawing.Color.Green
            ElseIf GageCapability < 20 Then
                lblResult.Text = "Gage Capability is Good ( < 20% )"
                lblResult.BackColor = System.Drawing.Color.Green
            ElseIf GageCapability < 30 Then
                lblResult.Text = "Gage Capability is Acceptable ( < 30% )"
                lblResult.BackColor = System.Drawing.Color.Yellow
            Else
                lblResult.Text = "Gage Capability is Unacceptable ( > 30% )"
                lblResult.BackColor = System.Drawing.Color.Red
            End If
        End If
        'If CompensationEnabled Then
        '    lblResult.Text += " - Uncertainty Compensation Enabled"
        'End If
        If Not MeetStdTolerance Then
            lblResult.Text += " - Sample(s) outside standard +/- 3 dBm tolerance"
        End If
    End Sub

    Private Sub AddPointToResIMTesTrace(ByVal seriesId As Short, ByVal strSweepIM3Freqs As String, ByVal strSweepPowerdBm As String)
        Try
            Dim intNumberOfPoints As Integer
            Dim SweepIM3Frequency() As String = strSweepIM3Freqs.Split(",")
            Dim SweepIM3PowerdBm() As String = strSweepPowerdBm.Split(",")
            Dim intNumberOfFrequencyPoints = SweepIM3Frequency.Length
            Dim intNumberOfPowerPoints = SweepIM3PowerdBm.Length
            If intNumberOfFrequencyPoints <> intNumberOfPowerPoints Then
                MsgBox("The number of points mismatch", MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
            Else
                intNumberOfPoints = intNumberOfFrequencyPoints
            End If
            Dim lstPoints As New List(Of DataModels.FrequencyPoint)
            For i As Integer = 0 To intNumberOfPoints - 1
                Dim point As New DataModels.FrequencyPoint
                point.XData = Convert.ToSingle(SweepIM3Frequency(i))
                point.YData = Convert.ToSingle(SweepIM3PowerdBm(i))
                lstPoints.Add(point)
            Next
            If ResIMTestTrace.ContainsKey(seriesId) = False Then
                ResIMTestTrace.Add(seriesId, lstPoints)
            End If
        Catch ex As Exception
            Throw New Exception("GRR.AddPointToResIMTestTrace()::" & ex.Message)
        End Try
    End Sub

    Private Sub LoadProcedure()
        Try
            ResIMTestTrace = New Dictionary(Of Short, List(Of DataModels.FrequencyPoint))

            Dim GC_Procedure_List As List(Of CATS.Model.GC_Procedure)

            Dim GC_Procedure_Bll As New CATS.BLL.GC_ProcedureManager

            AnalyzerSN = txtSN.Text

            GC_Procedure_List = GC_Procedure_Bll.SelectMostRecent(AnalyzerSN)

            If GC_Procedure_List IsNot Nothing Then
                Dim query = From gc In GC_Procedure_List
                            Where gc.AnalyzerModel = gSelectedInstrument.Model

                If query.Count > 0 Then
                    GC_Procedure_Model = query.First
                    GCProcedureDatePerformed = GC_Procedure_Model.TimeCompleted.ToString("MM/dd/yyyy hh:mm:ss tt")
                    lblLastGCDate.Text = GCProcedureDatePerformed
                    GageCapability = GC_Procedure_Model.PTRatio
                    CompensationEnabled = IIf(GC_Procedure_Model.CompStatus = "ON", True, False)
                    Dim temp As DateTime = Now
                    Dim expiry As DateTime = GC_Procedure_Model.TimeCompleted.AddDays(14)
                    If temp > expiry Then
                        IsExpired = True
                        CompensationEnabled = False
                        lblCompStatus.Text = "OFF"
                        lblCompStatus.BackColor = Color.Red
                    Else
                        IsExpired = False
                        DisplayGageProcedure()
                    End If
                Else
                    lblLastGCDate.Text = "N/A"
                    ValidateForGC()
                    dgvGRRSamples.Rows.Clear()
                    ClearCalculationsGUI()
                End If
            Else
                If MsgBox(String.Format("Not find any GRR result of the instrument SN <{0}>, do you still want to continue?", AnalyzerSN), MsgBoxStyle.YesNo + MsgBoxStyle.Information) = DialogResult.No Then
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("GC.LoadProcedure()::" & ex.Message)
        End Try
    End Sub

    Private Sub CalculateRBar(ByVal range() As Decimal)
        Try
            Dim total As Decimal = 0
            For Each value In range
                total += value
            Next
            RBar = total / (range.Length)
        Catch ex As Exception
            Throw New Exception("GC.CalculateRBar()::" & ex.Message)
        End Try
    End Sub
    Private Sub CalculateGageCapability()
        Try
            GageCapability = SigmaMultiplier / Tolerance * MeasError * 100
            If GageCapability < 30 And ResIMQulificationResult = "P" And MeetStdTolerance Then
                CompensationEnabled = True
            End If
        Catch ex As Exception
            Throw New Exception("GC.CalculateGageCapability()::" & ex.Message)
        End Try
    End Sub
    Private Sub CalculateMeans()
        Try
            Dim intRunCounter As Integer = 0
            For Each run In SampleRuns
                Dim total As Decimal = 0
                Dim intSampleCounter As Integer = 0
                For Each sample In run.Value
                    total += sample.Peak
                    intSampleCounter += 1
                Next
                Means(intRunCounter) = total / intSampleCounter
                intRunCounter += 1
            Next
        Catch ex As Exception
            Throw New Exception("GC.CalculateMeans()::" & ex.Message)
        End Try
    End Sub
    Private Sub CalculateVariances()
        Try
            Dim intRunCounter As Integer = 0
            For Each run In SampleRuns
                Dim total As Decimal = 0
                Dim intSampleCounter As Integer = 0
                For Each sample In run.Value
                    MeanDiffs(intRunCounter, intSampleCounter) = sample.Peak - Means(intRunCounter)
                    Variances(intRunCounter) += MeanDiffs(intRunCounter, intSampleCounter) ^ 2
                    intSampleCounter += 1
                Next
                Variances(intRunCounter) = Variances(intRunCounter) / intSampleCounter
                intRunCounter += 1
            Next
        Catch ex As Exception
            Throw New Exception("GC.CalculateVariances()::" & ex.Message)
        End Try
    End Sub
    Private Sub CalculateStdDeviations()
        Try
            Dim intRunCounter As Integer = 0
            For Each run In SampleRuns
                For Each sample In run.Value
                    StdDevs(intRunCounter) = Math.Sqrt(Variances(intRunCounter))
                Next
                intRunCounter += 1
            Next
        Catch ex As Exception
            Throw New Exception("GC.CalculateStdDeviations()::" & ex.Message)
        End Try
    End Sub
    Private Sub CalculateCorrFactors()
        Try
            Dim intRunCounter As Integer = 0
            Dim tmpSum As Decimal = 0
            For Each run In SampleRuns
                CorrFactors(intRunCounter) = 3 * StdDevs(intRunCounter)
                tmpSum += StdDevs(intRunCounter)
                intRunCounter += 1
            Next
            CorrFactor = tmpSum / intRunCounter * 3
        Catch ex As Exception
            Throw New Exception("GC.CalculateCorrFactors()::" & ex.Message)
        End Try
    End Sub
    Private Sub DisplayCompResults(ByVal rDGV As System.Windows.Forms.DataGridView)
        Dim intRowIdx As Integer = 0
        Dim intRunCounter As Integer = 0
        For Each run In SampleRuns
            intRowIdx = rDGV.Rows.Add()
            rDGV.Rows(intRowIdx).Cells(0).Value = (intRowIdx + 1).ToString()
            rDGV.Rows(intRowIdx).Cells(0).Value = Means(intRunCounter).ToString()
            rDGV.Rows(intRowIdx).Cells(0).Value = Variances(intRunCounter).ToString()
            rDGV.Rows(intRowIdx).Cells(0).Value = StdDevs(intRunCounter).ToString("0.000")
            rDGV.Rows(intRowIdx).Cells(0).Value = CorrFactors(intRunCounter).ToString("0.0")
            intRunCounter += 1
        Next
    End Sub
    Private Sub CompStatusFinalVerification()
        If GageCapability < 30 And ResIMQulificationResult = "P" And MeetStdTolerance And CorrFactor < 1 Then
            CompensationEnabled = True
        Else
            CompensationEnabled = False
        End If
    End Sub

    Private Function ReadImdValue() As Single
        Try

            Return pPimDev.ReadImd_dBc + InstrPowerLoss.Rx

        Catch ex As Exception
            Throw New Exception("FormTest::ReadImdValue()::" & vbCrLf & ex.Message)
        End Try

    End Function
    Private Function GetVirtualImValue() As Single
        Try
            Randomize()
            Dim x As Single = 0

            x = CInt(250 * Rnd() + 16800) / 100

            Threading.Thread.Sleep(222)

            'Return -153
            Return Math.Round((-1) * x, 2)

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function InitPimDevice() As DataModels.Instrument
        Try

            Dim dev As New DataModels.Instrument

            If AnalyzerVendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = ComPort
                devRos.Open()

                dev.Idn = devRos.ReadIDN
                dev.SerialNumber = devRos.Serial_Number
                dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
                dev.Hardware = ""
                dev.Firmware = devRos.FW_Revision

                devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRos.FreqBand = BandIdx
                devRos.RFPowerOnOff_TwoPorts(False)

                pPimDev = devRos

            ElseIf AnalyzerVendor.Trim.ToUpper = "Zulu".ToString.ToUpper Then
                Dim devZulu As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                devZulu.Address = gSelectedInstrument.Address
                devZulu.Open()
                devZulu.FreqBandSet = gSelectedInstrument.FreqBand

                dev.Idn = "Commscope Zulu " & devZulu.FilterBandName & "_" & devZulu.Serial_Number

                If dev.Idn.ToUpper.Contains("Mismatch for the current Master with Filter".ToUpper) Then Throw New Exception("InitPimDevice()::" & "Mismatch for the current Master with Filter")

                dev.Model = devZulu.Model
                dev.SerialNumber = devZulu.Serial_Number
                dev.Hardware = ""
                dev.Firmware = devZulu.FW_Revision

                devZulu.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)

                devZulu.RFPowerOnOff_TwoPorts(False)

                pPimDev = devZulu

            ElseIf AnalyzerVendor.Trim.ToUpper = "Summitek".ToString.ToUpper Then
                Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devSum.Address = ComPort
                devSum.Open()

                dev.Idn = " "
                dev.SerialNumber = devSum.Serial_Number
                dev.Model = devSum.Model
                dev.Hardware = " "
                dev.Firmware = " " 'devSum.FW_Revision

                devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devSum.FreqBand = BandIdx
                devSum.RFPowerOnOff_TwoPorts(False)

                pPimDev = devSum

            ElseIf AnalyzerVendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                devKae.Address = ComPort
                devKae.Open()

                dev.Idn = " "
                dev.Model = devKae.Model
                dev.SerialNumber = devKae.Serial_Number
                dev.Hardware = " "
                dev.Firmware = " "

                devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devKae.FreqBand = BandIdx
                devKae.RFPowerOnOff_TwoPorts(False)

                pPimDev = devKae

            ElseIf AnalyzerVendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                devRfl.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim  'add by tony 20190529
                devRfl.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim 'add by tony 20190529
                If devRfl.Port_Select < 1 Or devRfl.Port_Select > 2 Then Throw New Exception("InitPimDevice():: Rflight Port selection") 'add by tony 20190529
                devRfl.Open()

                dev.Idn = devRfl.ReadIDN
                dev.SerialNumber = devRfl.Serial_Number
                dev.Model = devRfl.Model
                dev.Hardware = ""
                dev.Firmware = devRfl.FW_Revision

                devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRfl.FreqBand = BandIdx
                devRfl.RFPowerOnOff_TwoPorts(False)

                pPimDev = devRfl
            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                devJoc.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim
                devJoc.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim
                If devJoc.Port_Select < 1 Or devJoc.Port_Select > 2 Then Throw New Exception("InitPimDevice():: JointCom Port selection wrong")
                devJoc.Open()

                dev.Idn = devJoc.ReadIDN
                dev.SerialNumber = devJoc.Serial_Number
                dev.Model = devJoc.Model
                dev.Hardware = ""
                dev.Firmware = devJoc.FW_Revision

                devJoc.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devJoc.FreqBand = gSelectedInstrument.BandIdx
                devJoc.RFPowerOnOff_TwoPorts(False)

                pPimDev = devJoc
            End If

            InstrPowerLoss.TxL = gSelectedInstrument.TxLLoss
            InstrPowerLoss.TxR = gSelectedInstrument.TxRLoss
            InstrPowerLoss.Rx = 0

            Return dev

        Catch ex As Exception
            Throw New Exception("FormGRR.InitPimDevice()::" & ex.Message)
        End Try
    End Function
    Private Function InitVirtualPimDevice() As DataModels.Instrument
        Try

            Dim dev As New DataModels.Instrument

            dev.Idn = "Rosenberger HF-Technik GmbH & Co. KG,IM-19AWS    Serial No. 010IM-A3815,Firmware v7.53 [2015-05-11]"
            dev.SerialNumber = "010IM-A3815"
            dev.Model = "IM-19AWS"
            dev.Hardware = ""
            dev.Firmware = "v7.53"

            InstrPowerLoss.TxL = 0
            InstrPowerLoss.TxR = 0
            InstrPowerLoss.Rx = 0

            Return dev

        Catch ex As Exception
            Throw New Exception("FormGRR.InitVirtualPimDevice()::" & ex.Message)
        End Try
    End Function
    Private Sub btnLoadProcedure_Click(sender As Object, e As EventArgs) Handles btnLoadProcedure.Click
        LoadProcedure()
    End Sub

    Private Function GetSweepFrequency(freqDesc As String, vendorId As Integer) As cfg_imd_sfbox
        Try
            Dim resp As cfg_imd_sfbox
            Dim cisBll As New CATS.BLL.cfg_imd_sfboxManager

            resp = cisBll.SelectByDescrVendorId(freqDesc, vendorId)

            Return resp
        Catch ex As Exception
            Throw New Exception("FormGRR.GetSweepFrequnecy()::" & ex.Message)
        End Try
    End Function

    Private Sub txtSN_TextChanged(sender As Object, e As EventArgs) Handles txtSN.TextChanged
        If txtSN.Text.Length > 0 Then
            ValidateForLoadProcedureButton()
            ValidateResIMGUI()
        Else
            ValidateForLoadProcedureButton()
            ValidateResIMGUI()
        End If
    End Sub

    Private Sub ValidateForLoadProcedureButton()
        If txtSN.Text.Length > 0 Then
            Me.btnLoadProcedure.Enabled = True
        Else
            Me.btnLoadProcedure.Enabled = False
        End If
    End Sub

    Private Sub FormGRR_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ClosePimDevice()
        Catch ex As Exception

        End Try
    End Sub
    Private Function ClosePimDevice() As Boolean
        Try
            If pPimDev IsNot Nothing Then
                pPimDev.Close()
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("FormGRR.ClosePimDevice()::" & ex.Message)
        End Try
    End Function

    Private Sub tcGRR_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles tcGRR.Selecting
        Try
            If e.TabPageIndex = 1 Then
                If txtSN.Text.Length = 0 Then
                    MsgBox("Please enter the Analyzer Serial Number first.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                    e.Cancel = True
                    Exit Sub
                End If
                Dim gcpBll As New CATS.BLL.GC_ProcedureManager
                Dim gcpML As List(Of CATS.Model.GC_Procedure) = gcpBll.SelectMostRecent(txtSN.Text)
                If gcpML Is Nothing Then
                    MsgBox("Not find any GRR result of the instrument SN <" & txtSN.Text & ">", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                    e.Cancel = True
                    Exit Sub
                End If
                Dim query = From gc In gcpML
                            Where gc.TimeCompleted > DateTime.Now.AddDays(-180)
                If query.Count = 0 Then
                    MsgBox("Not find any GRR result of the instrument SN <" & txtSN.Text & "> in a month", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                    e.Cancel = True
                    Exit Sub
                End If
                InitGridHistory()
                Dim r As Row
                For Each gcp As GC_Procedure In query.ToList
                    fgHistory.Rows.Add()
                    r = fgHistory.Rows(fgHistory.Rows.Count - 1)
                    r(1) = gcp.AnalyzerSN
                    r(2) = gcp.AnalyzerModel
                    r(3) = gcp.AnalyzerVendor
                    r(4) = gcp.TimeCompleted.ToString("MM/dd/yyyy hh:mm:ss tt")
                    r(5) = gcp.PTRatio.ToString("#0.0") & "%"
                    r(6) = 30.ToString("#0.0") & "%"
                    r(7) = IIf(gcp.PTRatio < 30, "PASS", "FAIL")
                Next
                FormatGrid(fgHistory, 9, 9)
            End If
        Catch ex As Exception
            MsgBox("tcGRR_Selecting" & ex.Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Sub InitGridHistory()
        Try
            'set up grid
            fgHistory.Clear()
            fgHistory.Cols.Fixed = 1
            fgHistory.Cols.Count = 8
            fgHistory.Rows.Fixed = 1
            fgHistory.Rows.Count = 1
            fgHistory.Cols(1).Caption = "Analyzer SN"
            fgHistory.Cols(2).Caption = "Analyzer Model"
            fgHistory.Cols(3).Caption = "Analyzer Vendor"
            fgHistory.Cols(4).Caption = "Complete Time"
            fgHistory.Cols(5).Caption = "Gage Capability"
            fgHistory.Cols(6).Caption = "Gage Limit"
            fgHistory.Cols(7).Caption = "Gage Result"
            fgHistory.Cols(7).Name = "GageRes"


            FormatGrid(fgHistory, 9, 9)

        Catch ex As Exception
            Throw New Exception("InitGridCalData()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub

    Private Sub fgHistory_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles fgHistory.OwnerDrawCell
        Try
            If e.Row >= fgHistory.Rows.Fixed And e.Col = fgHistory.Cols.Fixed - 1 Then
                Dim rowNumber As Integer = e.Row - fgHistory.Rows.Fixed + 1
                e.Text = rowNumber.ToString()
                Dim colName As String = "GageRes"
                Dim colIdx As Integer = fgHistory.Cols(colName).Index
                Dim pf As String = fgHistory(e.Row, colIdx)
                If pf = "FAIL" Then
                    fgHistory.Rows(e.Row).Style = fgHistory.Styles("Red")
                Else
                    fgHistory.Rows(e.Row).Style = fgHistory.Styles("Green")
                End If
            End If
        Catch ex As Exception
            MsgBox("fgHistory_OwnerDrawCell()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
End Class

Public Class GC
    Public Class SamplePoint
        Public SampleNumber As Integer
        Public Peak As Single
    End Class

End Class