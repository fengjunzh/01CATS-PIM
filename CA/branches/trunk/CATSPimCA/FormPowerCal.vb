Imports System.ComponentModel
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports AndrewIntegratedProducts.InstrumentsFramework
'Imports AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
Imports C1.Win.C1Chart
Imports C1.Win.C1FlexGrid
Imports CATS.BLL
Imports CATS.Model
Public Class FormPowerCal
    Private _pimConfigStr As String
    Private _pimBandName As String
    Private _vendorName As String
    Private startCalTime As DateTime
    Private _endCalTime As DateTime
    Private calSuccess As Boolean
    Private _dstt As String = ""
    Private ffBox As CATS.Model.cfg_imd_ffbox = Nothing
    Private Delegate Sub DlgZero()
    Private _txLPower As Decimal
    Private _txRPower As Decimal
    Private txPower As Decimal
    Private _pimModel As String
    Private _pimSN As String

    Public Property DataSeriesTooltipText As String
        Get
            Return _dstt
        End Get
        Set(value As String)
            _dstt = value
        End Set
    End Property
    Public Property PimConfigStr As String
        Get
            Return _pimConfigStr
        End Get
        Set(value As String)
            _pimConfigStr = value
        End Set
    End Property

    Public Property PimBandName As String
        Get
            Return _pimBandName
        End Get
        Set(value As String)
            _pimBandName = value
        End Set
    End Property

    Public Property VendorName As String
        Get
            Return _vendorName
        End Get
        Set(value As String)
            _vendorName = value
        End Set
    End Property

    Public Property TxLPower As Decimal
        Get
            Return _txLPower
        End Get
        Set(value As Decimal)
            _txLPower = value
        End Set
    End Property
    Public Property TxRPower As Decimal
        Get
            Return _txRPower
        End Get
        Set(value As Decimal)
            _txRPower = value
        End Set
    End Property
    Public Property EndCalTime As DateTime
        Get
            Return _endCalTime
        End Get
        Set(value As Date)
            _endCalTime = value
        End Set
    End Property

    Public Property PimModel As String
        Get
            Return _pimModel
        End Get
        Set(value As String)
            _pimModel = value
        End Set
    End Property

    Public Property PimSN As String
        Get
            Return _pimSN
        End Get
        Set(value As String)
            _pimSN = value
        End Set
    End Property

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            InitGridInstrument()
            Dim devList As List(Of String) = ScanInstruments()
            Dim r As Row
            Dim errMsg As String = ""
            For Each dev As String In devList
                If InitU2000Device(dev, errMsg) = False Then Continue For
                r = Me.fgInstrument.Rows.Add
                r(1) = pPSCtrl.IDN
                r(2) = pPSCtrl.Address
                r(3) = pPSCtrl.FW_Revision
                r(4) = pPSCtrl.Vendor
                r(5) = pPSCtrl.InstalledOptions
                r(6) = errMsg
            Next

            FormatGrid(fgInstrument, 9, 9)
            If Me.fgInstrument.Rows.Count > 0 Then
                Me.btnZero.Enabled = True
                Me.btnStartCal.Enabled = True
            End If
        Catch ex As Exception
            Me.btnZero.Enabled = False
            Me.btnStartCal.Enabled = False
            MsgBox("btnRefresh_Click()::" & ex.Message, MsgBoxStyle.Exclamation, "Refresh Instrument Exception")
        End Try
    End Sub

    Private Sub InitGridInstrument()
        Try
            'set up grid
            fgInstrument.Clear()
            fgInstrument.Cols.Fixed = 1
            fgInstrument.Cols.Count = 7
            fgInstrument.Rows.Fixed = 1
            fgInstrument.Rows.Count = 1
            fgInstrument.Cols(1).Caption = "Instrument IDN"
            fgInstrument.Cols(2).Caption = "Address"
            fgInstrument.Cols(3).Caption = "Firmware"
            fgInstrument.Cols(4).Caption = "Vendor"
            fgInstrument.Cols(5).Caption = "Installed Option"
            fgInstrument.Cols(6).Caption = "Error"

            FormatGrid(fgInstrument, 9, 9)

        Catch ex As Exception
            Throw New Exception("InitGridInstrument()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub

    Private Sub InitGridCalData()
        Try
            'set up grid
            fgCalData.Clear()
            fgCalData.Cols.Fixed = 1
            fgCalData.Cols.Count = 8
            fgCalData.Rows.Fixed = 1
            fgCalData.Rows.Count = 1
            fgCalData.Cols(1).Caption = "Tx Side"
            fgCalData.Cols(2).Caption = "Tx Power"
            fgCalData.Cols(3).Caption = "Tx Freq(MHz)"
            fgCalData.Cols(4).Caption = "Start Time"
            fgCalData.Cols(5).Caption = "End Time"
            fgCalData.Cols(6).Caption = "Average Power"
            fgCalData.Cols(7).Caption = "Output Power"

            FormatGrid(fgCalData, 9, 9)

        Catch ex As Exception
            Throw New Exception("InitGridCalData()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub InitGridHistory()
        Try
            'set up grid
            fgHistory.Clear()
            fgHistory.Cols.Fixed = 1
            fgHistory.Cols.Count = 12
            fgHistory.Rows.Fixed = 1
            fgHistory.Rows.Count = 1
            fgHistory.Cols(1).Caption = "Phase"
            fgHistory.Cols(2).Caption = "Controller"
            fgHistory.Cols(3).Caption = "PIM Analyzer SN"
            fgHistory.Cols(4).Caption = "Employee"
            fgHistory.Cols(5).Caption = "Power"
            fgHistory.Cols(6).Caption = "TX1 Power"
            fgHistory.Cols(7).Caption = "TX1 Offset"
            fgHistory.Cols(8).Caption = "TX2 Power"
            fgHistory.Cols(9).Caption = "TX2 Offset"
            fgHistory.Cols(10).Caption = "Power Meter IDN"
            fgHistory.Cols(11).Caption = "Cal Time"

            FormatGrid(fgHistory, 9, 9)

        Catch ex As Exception
            Throw New Exception("InitGridCalData()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub btnStartCal_Click(sender As Object, e As EventArgs) Handles btnStartCal.Click
        Try

            Me.btnZero.Enabled = False
            Me.btnStartCal.Enabled = False

            Me.tsslTimeLapsed.Text = "00:00:00"
            startCalTime = Now
            Timer1.Enabled = True
            Me.pbProgress.Value = 0
            Me.pbProgress.Minimum = 0
            Me.pbProgress.Maximum = 25

            InitGridCalData()
            InitChart(chartPIM, "PIM Analyzer Output Power Level", True, "Time(s)", "dBm", False)

            ' Set trigger mode to continuous
            Me.tsslStatus.Text = "Set Trigger Mode to Continuous"
            My.Application.DoEvents()
            IPSU2000.Continuous = True
            Me.pbProgress.Value += 1

            Dim freq As Integer
#Region "Measure TxL Power"
            ' Set up PIM Analyzer
#If INSTR_NORMAL_TEST Then
            InitPimDevice(gSelectedInstrument)
            SetupPimAnalyzer(freq, True)
            MyDelay(1000)
#Else
            freq = 1000
#End If
            Me.pbProgress.Value += 1

            ' Set freq
            Me.tsslStatus.Text = "Set Freq"
            My.Application.DoEvents()
            IPSU2000.Freq = freq
            Me.pbProgress.Value += 1

            Me.tsslStatus.Text = "Start to measure PIM Analzyer TxL output power level ......"

            MeasureOutputPower(freq, True)
#End Region 'Measure TxL Power

#Region "Measure TxL Power"
            ' Set up PIM Analyzer
#If INSTR_NORMAL_TEST Then
            SetupPimAnalyzer(freq, False)
            MyDelay(1000)
#Else
            freq = 2000
#End If
            Me.pbProgress.Value += 1

            ' Set freq
            Me.tsslStatus.Text = "Set Freq"
            My.Application.DoEvents()
            IPSU2000.Freq = freq
            Me.pbProgress.Value += 1

            Me.tsslStatus.Text = "Start to measure PIM Analzyer TxR output power level ......"

            MeasureOutputPower(freq, False)
#End Region 'Measure TxL Power

            ' Set default tooltips
            DataSeriesTooltipText = "Trace: {#TEXT}" & vbCrLf & "Time = {#XVAL}" & vbCrLf & "y = {#YVAL}"
            ' Set tooltip for all series
            For Each dataSeries As ChartDataSeries In chartPIM.ChartGroups(0).ChartData.SeriesList
                dataSeries.TooltipText = DataSeriesTooltipText
            Next

        Catch ex As Exception
            Timer1.Enabled = False
            MsgBox("btnStartCal_Click()::" & ex.Message, MsgBoxStyle.Exclamation, "Power Cal Exception")
        Finally
#If INSTR_NORMAL_TEST Then
            If pPimDev IsNot Nothing Then pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
            ClosePimDevice()
            Me.btnZero.Enabled = True
            Me.btnStartCal.Enabled = True
        End Try
    End Sub
    Private Sub MeasureOutputPower(freq As Decimal, txL As Boolean)
        Try
            Dim dspPoint As PointF
            Dim tmp_time As Short = 10
            Dim tmp_start As DateTime
            Dim tmp_date_diff As TimeSpan
            Dim tmp_loop_flag As Boolean = True
            tmp_start = Now
            Dim powerVal As Decimal

            Dim x As New List(Of Double)
            Dim y As New List(Of Double)

            Dim ds As ChartDataSeries = Nothing
            Dim xSec As Integer
            Do While (tmp_loop_flag)

#If INSTR_NORMAL_TEST Then
                powerVal = IPSU2000.Measure(-30, 0.1)
#Else
                powerVal = GetVirtualPowerValue()
#End If
                dspPoint.X = tmp_date_diff.TotalSeconds
                'dspPoint.X = tmp_date_diff.Milliseconds / 1000 + tmp_date_diff.Seconds + tmp_date_diff.Minutes * 60
                If dspPoint.X > tmp_time Then
                    dspPoint.X = tmp_time
                    tmp_loop_flag = False
                End If

                x.Add(Math.Round(dspPoint.X, 2))
                y.Add(Math.Round(powerVal, 2))

                Dim data As ChartData = chartPIM.ChartGroups(0).ChartData
                Dim series As ChartDataSeriesCollection = data.SeriesList
                If txL Then
                    If series.Count > 0 Then
                        ds = data(0)
                        ds.X.Clear()
                        ds.Y.Clear()
                    End If
                Else
                    If series.Count > 1 Then
                        ds = data(1)
                        ds.X.Clear()
                        ds.Y.Clear()
                    End If
                End If
                Dim serLbl As String = IIf(txL, String.Format("TxL Power@{0}MHz", freq), String.Format("TxR Power@{0}MHz", freq))
                Dim clr As Color = IIf(txL, Color.Blue, Color.Yellow)
                If ds Is Nothing Then
                    AddSerieIntoChart(chartPIM, serLbl, x, y, clr,, SymbolShapeEnum.Diamond)
                Else
                    ds.X.CopyDataIn(x.ToArray)
                    ds.Y.CopyDataIn(y.ToArray)
                End If

                MyDelay(200)

                My.Application.DoEvents()

                tmp_date_diff = Now.Subtract(tmp_start)

                If xSec = Math.Floor(dspPoint.X) Then
                    xSec += 1
                    If Me.pbProgress.Value < Me.pbProgress.Maximum Then Me.pbProgress.Value += 1
                End If
            Loop

            Me.tsslStatus.Text = "Measure Complete"

            If txL Then
                TxLPower = Math.Round(y.Average, 2)
            Else
                TxRPower = Math.Round(y.Average, 2)
            End If

            Timer1.Enabled = False

            EndCalTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            Dim r As Row = fgCalData.Rows.Add
            r(1) = IIf(txL, "TxL", "TxR")
            r(2) = txPower
            r(3) = freq
            r(4) = tmp_start
            r(5) = EndCalTime
            r(6) = IIf(txL, TxLPower, TxRPower)
            r(7) = String.Join(",", y.ToArray)
            r.UserData = "CAL0300" & EndCalTime.ToString("yyyyMMddHHmmss")
            FormatGrid(fgCalData, 9, 9)

        Catch ex As Exception
            Throw New Exception("MeasureOutputPower()::" & ex.Message)
        End Try
    End Sub

    Private Function GetVirtualPowerValue() As Single
        Try
            Dim rng As New Random
            Dim rndVal As Double = rng.NextDouble
            Dim scaleVal As Double = rndVal * 2 - 1
            Dim x As Double = 0

            x = 0.1 * scaleVal + txPower

            Threading.Thread.Sleep(222)

            'Return 43
            Return Math.Round(x, 2)

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Sub SetupPimAnalyzer(ByRef freq As Integer, txL As Boolean)
        Try
            GetffBox()
            freq = IIf(txL, ffBox.c1_freq, ffBox.c2_freq)
            pPimDev.CheckPowerVersion(CSng(IIf(txL, ffBox.c1_power, ffBox.c2_power)))
            pPimDev.ImdOrder = ffBox.imd_order
            pPimDev.SetFrequency(ffBox.c1_freq, ffBox.c2_freq)
            pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
            pPimDev.SetRFPower(ffBox.c1_power, ffBox.c2_power)
            pPimDev.RFPowerOnOff_OnePort(txL, Not txL)

        Catch ex As Exception
            Throw New Exception("SetupPimAnalyzer()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub GetffBox()
        Try
            Dim vendorId As Integer = (New CATS.BLL.instr_vendorManager).SelectByVendorName(VendorName).id
            ffBox = TestModules.GetFixedFreqs(PimBandName, vendorId)
            ffBox.c1_power = txPower
            ffBox.c2_power = txPower
        Catch ex As Exception
            Throw New Exception("GetffBox()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub FormPowerCal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.btnZero.Enabled = False
            Me.btnStartCal.Enabled = False
            Me.tsslStatus.Text = PimConfigStr
            Me.lblPimBand.Text = PimBandName
            InitGridInstrument()
            InitGridCalData()
            InitGridHistory()
            InitChart(chartPIM, "PIM Analyzer Output Power Level", True, "Time(s)", "dBm", False)
            Me.cbPower.Text = 43
        Catch ex As Exception
            MsgBox("FormPowerCal_Load()::" & ex.Message, MsgBoxStyle.Exclamation, "Form Load Exception")
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If Me.fgCalData.Rows.Count = 3 Then
            Dim calRecM As CATS.Model.rec_imd_calibration = (New CATS.BLL.rec_imd_calibrationManager).SelectBySn(fgCalData.Rows(2).UserData)
            If calRecM Is Nothing Then
                If MsgBox("Power Cal Data is not saved, do you want to save the data?", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Save Cal Data") = DialogResult.Yes Then
                    SaveCalRecord()
                End If
            End If
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Zero()
        Try
            If ffBox Is Nothing Then GetffBox()
            IPSU2000.Freq = ffBox.c1_freq
            IPSU2000.ZeroType = IPowerMeter.PSZeroType.INT
            calSuccess = IPSU2000.Zeroing
        Catch ex As Exception
            Throw New Exception("Zero()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub btnZero_Click(sender As Object, e As EventArgs) Handles btnZero.Click
        Try
            Me.btnZero.Enabled = False
            Me.btnStartCal.Enabled = False
            ' Start to cal power sensor
            Me.tsslStatus.Text = "Zeroing ......"
            startCalTime = Now
            Me.pbProgress.Value = 0
            Me.pbProgress.Maximum = 60
            My.Application.DoEvents()

            GetffBox()

            Dim dgRun As New DlgZero(AddressOf Zero)
            Dim res As IAsyncResult = dgRun.BeginInvoke(Nothing, Nothing)

            Do Until (res.IsCompleted)
                MyDelay(500)
                If Me.pbProgress.Value < Me.pbProgress.Maximum Then Me.pbProgress.Value += 1
                Dim tSpan As TimeSpan = Now.Subtract(startCalTime)
                Me.tsslTimeLapsed.Text = String.Format("{0:00}:{1:00}:{2:00}", tSpan.Hours, tSpan.Minutes, tSpan.Seconds)
            Loop

            dgRun.EndInvoke(res)

            Me.pbProgress.Value = Me.pbProgress.Maximum

            Me.tsslStatus.Text = "Zero Complete"

            My.Application.DoEvents()
        Catch ex As Exception
            Timer1.Enabled = False
        Finally
            Me.btnZero.Enabled = True
            Me.btnStartCal.Enabled = True
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim tSpan As TimeSpan = Now.Subtract(startCalTime)
        Me.tsslTimeLapsed.Text = String.Format("{0:00}:{1:00}:{2:00}", tSpan.Hours, tSpan.Minutes, tSpan.Seconds)
        My.Application.DoEvents()
    End Sub

    Private Sub cbPower_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbPower.SelectedIndexChanged
        txPower = cbPower.Text
    End Sub

    Private Sub fgCalData_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles fgCalData.OwnerDrawCell
        Try
            If e.Row >= fgCalData.Rows.Fixed And e.Col = fgCalData.Cols.Fixed - 1 Then
                Dim rowNumber As Integer = e.Row - fgCalData.Rows.Fixed + 1
                e.Text = rowNumber.ToString()
            End If
        Catch ex As Exception
            MsgBox("fgCalData_OwnerDrawCell()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveCalRecord()
        Catch ex As Exception
            MsgBox("btnSave_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub SaveCalRecord()
        Try
            If fgCalData.Rows.Count <> 3 Then MsgBox("No Cal Data") : Return
            Dim calRecM As CATS.Model.rec_imd_calibration = (New CATS.BLL.rec_imd_calibrationManager).SelectBySn(fgCalData.Rows(2).UserData)
            If calRecM IsNot Nothing Then MsgBox("Cal Data Already Saved") : Return
            Dim rec_imd_calM As New CATS.Model.rec_imd_calibration
            With rec_imd_calM
                .serial_number = "CAL0300" & EndCalTime.ToString("yyyyMMddHHmmss")
                .phase_main_id = (New CATS.BLL.phase_mainManager).SelectByPhase(PimModel).id
                AddStatusMsg("Phase ID: " & .phase_main_id)
                Dim ctrId As Integer
                Dim ctrBll As New CATS.BLL.controllerManager
                Dim ctrM As CATS.Model.controller = ctrBll.SelectByName(My.Computer.Name)
                If ctrM Is Nothing Then
                    ctrM = New CATS.Model.controller
                    With ctrM
                        .name = My.Computer.Name
                        .factory_id = (New CATS.BLL.factoryManager).SelectByFactory(pFactory).id
                    End With
                    ctrId = ctrBll.AddReturnId(ctrM)
                Else
                    ctrId = ctrM.id
                End If
                .controller_id = ctrId
                Dim imBll As New CATS.BLL.instr_mainManager
                Dim imM As CATS.Model.instr_main = imBll.SelectBySN(PimSN)
                Dim imId As Integer
                If imM Is Nothing Then
                    'imM = New CATS.Model.instr_main
                    'With imM
                    '    .instr_model_id = 1
                    '    .serial_num = PimSN
                    '    .fw_version = "v7.53"
                    '    .entry_date = Now
                    '    .instr_idn = fgInstrument(1, 1).ToString.TrimEnd
                    'End With
                    'imId = imBll.AddReturnId(imM)
                    imId = 0
                Else
                    imId = imM.id
                End If
                .pim_instr_main_id = imId
                Dim eBll As New employeeManager
                Dim eM As employee = eBll.SelectByLoginname(Environment.UserName.ToUpper)
                Dim emId As Integer
                If eM Is Nothing Then
                    eM = New employee With {
                        .login_name = Environment.UserName.ToUpper,
                        .name = Environment.UserName.ToUpper,
                        .factory_id = (New CATS.BLL.factoryManager).SelectByFactory(pFactory).id,
                        .department = "CA"
                    }
                    emId = eBll.AddReturnId(eM)
                Else
                    emId = eM.id
                End If
                .employee_id = emId
                .power = txPower
                .attenuation = 0
                .tx1_power = TxLPower
                .tx1_offset = txPower - TxLPower
                If Math.Abs(.tx1_offset) > 1.5 Then
                    MsgBox("TxL Power Offset absolute value > 1.5dB, Please check the setting!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Power Offset Warning")
                    tcPowerCal.SelectedTab = tcPowerCal.TabPages(1)
                    Return
                End If
                .tx2_power = TxLPower
                .tx2_offset = txPower - TxRPower
                If Math.Abs(.tx2_offset) > 1.5 Then
                    MsgBox("TxR Power Offset absolute value > 1.5dB, Please check the setting!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "Power Offset Warning")
                    tcPowerCal.SelectedTab = tcPowerCal.TabPages(1)
                    Return
                End If
                .power_meter_idn = fgInstrument(1, 1).ToString.TrimEnd
                .cal_type = "Automatic"
                .cal_time = EndCalTime
                .gen1 = PimSN
            End With
            Dim rec_imd_calBll As New CATS.BLL.rec_imd_calibrationManager
            rec_imd_calBll.Add(rec_imd_calM)
            MsgBox("Add Record Success")
        Catch ex As Exception
            MsgBox("SaveCalRecord()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub tcPowerCal_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles tcPowerCal.Selecting
        Try
            If e.TabPageIndex = 2 Then
                InitGridHistory()
                'Dim ctrId As Integer
                'Dim ctrBll As New CATS.BLL.controllerManager
                'Dim ctrM As CATS.Model.controller = ctrBll.SelectByName(My.Computer.Name)
                'If ctrM Is Nothing Then
                '    ctrM = New CATS.Model.controller
                '    With ctrM
                '        .name = My.Computer.Name
                '        .factory_id = (New CATS.BLL.factoryManager).SelectByFactory(pFactory).id
                '    End With
                '    ctrId = ctrBll.AddReturnId(ctrM)
                'Else
                '    ctrId = ctrM.id
                'End If

                Dim ricBll As New rec_imd_calibrationManager
                Dim ricML As List(Of rec_imd_calibration) = ricBll.SelectByPimSnAndDate(PimSN, Now.AddDays(-30))

                If ricML Is Nothing Then Return
                Dim r As Row
                For Each ric As rec_imd_calibration In ricML
                    r = fgHistory.Rows.Add
                    r(1) = (New phase_mainManager).SelectById(ric.phase_main_id).phase
                    r(2) = (New controllerManager).SelectById(ric.controller_id).name
                    r(3) = ric.gen1
                    r(4) = (New employeeManager).SelectById(ric.employee_id)
                    r(5) = ric.power
                    r(6) = ric.tx1_power
                    r(7) = ric.tx1_offset
                    r(8) = ric.tx2_power
                    r(9) = ric.tx2_offset
                    r(10) = ric.power_meter_idn
                    r(11) = ric.cal_time
                Next
                FormatGrid(fgHistory, 9, 9)
            End If
        Catch ex As Exception
            MsgBox("tcPowerCal_Selecting()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub fgHistory_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles fgHistory.OwnerDrawCell
        Try
            If e.Row >= fgHistory.Rows.Fixed And e.Col = fgHistory.Cols.Fixed - 1 Then
                Dim rowNumber As Integer = e.Row - fgHistory.Rows.Fixed + 1
                e.Text = rowNumber.ToString()
            End If
        Catch ex As Exception
            MsgBox("fgHistory_OwnerDrawCell()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
End Class