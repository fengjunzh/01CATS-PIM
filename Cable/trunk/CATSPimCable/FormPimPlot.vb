Imports CATSPIM
Imports C1.Win.C1Chart
Public Class FormPimPlot
    Private m_testTrace As New Dictionary(Of DataModels.TestEnd, DataModels.TestTrace)
    Private m_testItem As CATS.Model.cq_spec_imd_details
    Private m_sfReq As CATS.Model.cfg_imd_sfbox
    Private m_ffReq As CATS.Model.cfg_imd_ffbox
    Private m_cfReq As CATS.Model.cfg_imd_cfbox
    Private m_serial_number As String
    Private m_part_number As String
    Private m_comment_oh As String
    Private m_comment_ho As String
    Private m_sweep_up_max_OH As Decimal
    Private m_sweep_down_max_OH As Decimal
    Private m_sweep_gap_OH As Decimal
    Private m_sweep_up_max_HO As Decimal
    Private m_sweep_down_max_HO As Decimal
    Private m_sweep_gap_HO As Decimal
    Private m_start_datetime As DateTime
    Private m_stop_datetime As DateTime
    Private _dstt As String = ""

    Public Property TestTrace As Dictionary(Of DataModels.TestEnd, DataModels.TestTrace)
        Get
            Return m_testTrace
        End Get
        Set(value As Dictionary(Of DataModels.TestEnd, DataModels.TestTrace))
            m_testTrace = value
        End Set
    End Property
    Public Property TestItem As CATS.Model.cq_spec_imd_details
        Get
            Return m_testItem
        End Get
        Set(ByVal value As CATS.Model.cq_spec_imd_details)
            m_testItem = value
        End Set
    End Property

    Public Property SF_Box As CATS.Model.cfg_imd_sfbox
        Get
            Return m_sfReq
        End Get
        Set(value As CATS.Model.cfg_imd_sfbox)
            m_sfReq = value
        End Set
    End Property
    Public Property FF_Box As CATS.Model.cfg_imd_ffbox
        Get
            Return m_ffReq
        End Get
        Set(value As CATS.Model.cfg_imd_ffbox)
            m_ffReq = value
        End Set
    End Property
    Public Property CF_Box As CATS.Model.cfg_imd_cfbox
        Get
            Return m_cfReq
        End Get
        Set(value As CATS.Model.cfg_imd_cfbox)
            m_cfReq = value
        End Set
    End Property

    Private Property DataSeriesTooltipText() As String
        Get
            Return _dstt
        End Get
        Set(value As String)
            If _dstt <> value Then
                _dstt = value
            End If
        End Set
    End Property

    Public Property Serial_number As String
        Get
            Return m_serial_number
        End Get
        Set(value As String)
            m_serial_number = value
        End Set
    End Property

    Public Property Part_number As String
        Get
            Return m_part_number
        End Get
        Set(value As String)
            m_part_number = value
        End Set
    End Property

    Public Property Comment_oh As String
        Get
            Return m_comment_oh
        End Get
        Set(value As String)
            m_comment_oh = value
        End Set
    End Property

    Public Property Comment_ho As String
        Get
            Return m_comment_ho
        End Get
        Set(value As String)
            m_comment_ho = value
        End Set
    End Property

    Public Property Sweep_up_max_OH As Decimal
        Get
            Return m_sweep_up_max_OH
        End Get
        Set(value As Decimal)
            m_sweep_up_max_OH = value
            Me.txtSweepUpMaxOH.Text = value
            If value > TestItem.spec_detail.limit_up Then
                Me.txtSweepUpMaxOH.ForeColor = Color.Red
            End If
        End Set
    End Property

    Public Property Sweep_down_max_OH As Decimal
        Get
            Return m_sweep_down_max_OH
        End Get
        Set(value As Decimal)
            m_sweep_down_max_OH = value
            Me.txtSweepDownMaxOH.Text = value
            If value > TestItem.spec_detail.limit_up Then
                Me.txtSweepDownMaxOH.ForeColor = Color.Red
            End If
        End Set
    End Property

    Public Property Sweep_gap_OH As Decimal
        Get
            Return m_sweep_gap_OH
        End Get
        Set(value As Decimal)
            m_sweep_gap_OH = value
            Me.txtSweepGapOH.Text = value
        End Set
    End Property

    Public Property Sweep_up_max_HO As Decimal
        Get
            Return m_sweep_up_max_HO
        End Get
        Set(value As Decimal)
            m_sweep_up_max_HO = value
            Me.txtSweepUpMaxHO.Text = value
            If value > TestItem.spec_detail.limit_up Then
                Me.txtSweepUpMaxHO.ForeColor = Color.Red
            End If
        End Set
    End Property

    Public Property Sweep_down_max_HO As Decimal
        Get
            Return m_sweep_down_max_HO
        End Get
        Set(value As Decimal)
            m_sweep_down_max_HO = value
            Me.txtSweepDownMaxHO.Text = value
            If value > TestItem.spec_detail.limit_up Then
                Me.txtSweepDownMaxHO.ForeColor = Color.Red
            End If
        End Set
    End Property

    Public Property Sweep_gap_HO As Decimal
        Get
            Return m_sweep_gap_HO
        End Get
        Set(value As Decimal)
            m_sweep_gap_HO = value
            Me.txtSweepGapHO.Text = value
        End Set
    End Property

    Public Property Start_datetime As Date
        Get
            Return m_start_datetime
        End Get
        Set(value As Date)
            m_start_datetime = value
            Me.lblStartTime.Text = value
        End Set
    End Property

    Public Property Stop_datetime As Date
        Get
            Return m_stop_datetime
        End Get
        Set(value As Date)
            m_stop_datetime = value
            Me.lblStopTime.Text = value
        End Set
    End Property

    Private Sub FormPimPlot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtSN.Text = pRTP.barcode
            txtPN.Text = pRTP.M_product_main.product_name
            lblCommentOH.Text = Comment_oh
            lblCommentHO.Text = Comment_ho

            Dim testDuration As TimeSpan
            testDuration = Stop_datetime.Subtract(Start_datetime)
            Me.lblDuration.Text = testDuration.TotalSeconds
            My.Application.DoEvents()
            Me.Show()

            RunAllTest()

        Catch ex As Exception
            MsgBox("FormPimPlot.FormPimPlot_Load()::" & ex.Message)
        End Try
    End Sub
    Private Sub RunAllTest()
        Try
            InitializeGuiChart()

            'sweep test
            Dim ul As Single = TestItem.spec_detail.limit_up
            If SF_Box IsNot Nothing Then
                PlotSfPimTrace(ul)
            End If
            If FF_Box IsNot Nothing Then
                PlotSfPimTrace(ul)
            End If
            If CF_Box IsNot Nothing Then
                PlotCfPimTrace(ul)
            End If
            AddStatusMsg(String.Format("Load PIM trace done"))
        Catch ex As Exception
            Throw New Exception("FormPimPlot.RunAllTest()::" & ex.Message)
        End Try
    End Sub
    Private Sub PlotSfPimTrace(ul As Decimal)
        Try
            If m_testTrace Is Nothing Then Return
            If m_testTrace.Count = 0 Then Return


            Dim OHSweetUpX_List As New List(Of Single)
            Dim OHSweetUpY_List As New List(Of Single)
            Dim OHSweetDownX_List As New List(Of Single)
            Dim OHSweetDownY_List As New List(Of Single)

            Dim OHMaxValue As Double = m_testTrace(0).SweepUp(0).YData
            Dim OHMinValue As Double = m_testTrace(0).SweepUp(0).YData

            For Each point In m_testTrace(0).SweepUp
                If point.YData > OHMaxValue Then OHMaxValue = point.YData
                If point.YData < OHMinValue Then OHMinValue = point.YData
                OHSweetUpX_List.Add(point.XData)
                OHSweetUpY_List.Add(point.YData)
            Next
            For Each point In m_testTrace(0).SweepDown
                If point.YData > OHMaxValue Then OHMaxValue = point.YData
                If point.YData < OHMinValue Then OHMinValue = point.YData
                OHSweetDownX_List.Add(point.XData)
                OHSweetDownY_List.Add(point.YData)
            Next


            chartOH.ChartArea.AxisY.Text = "PIM(dBc)"
            chartHO.ChartArea.AxisY.Text = "PIM(dBc)"
            ' Add the RL series
            Dim dataOH As ChartData = chartOH.ChartGroups(0).ChartData
            Dim seriesOH As ChartDataSeriesCollection = dataOH.SeriesList
            Dim dataHO As ChartData = chartHO.ChartGroups(0).ChartData
            Dim seriesHO As ChartDataSeriesCollection = dataHO.SeriesList

            If m_testTrace(0).SweepUp.Count > 0 Then
                chartOH.ChartArea.AxisX.Text = "Frequency(MHz)"
                gbChartOH.Text = "[ OH Swept Freq Trace ]"
                ' Plot PIM OH Trace
                Dim OHSweepUpSeries As ChartDataSeries = seriesOH.AddNewSeries()
                OHSweepUpSeries.Label = "OH Sweep Up"
                OHSweepUpSeries.LineStyle.Thickness = 2
                OHSweepUpSeries.LineStyle.Color = Color.Blue
                OHSweepUpSeries.SymbolStyle.Shape = SymbolShapeEnum.Diamond
                OHSweepUpSeries.SymbolStyle.Color = Color.Blue
                OHSweepUpSeries.X.CopyDataIn(OHSweetUpX_List.ToArray)
                OHSweepUpSeries.Y.CopyDataIn(OHSweetUpY_List.ToArray)
                OHSweepUpSeries = Nothing
            End If

            If m_testTrace(0).SweepDown.Count > 0 Then
                ' Plot PIM OH Trace
                Dim OHSweepDownSeries As ChartDataSeries = seriesOH.AddNewSeries()
                OHSweepDownSeries.Label = "OH Sweep Down"
                OHSweepDownSeries.LineStyle.Thickness = 2
                OHSweepDownSeries.LineStyle.Color = Color.DarkGreen
                OHSweepDownSeries.SymbolStyle.Shape = SymbolShapeEnum.Diamond
                OHSweepDownSeries.SymbolStyle.Color = Color.DarkGreen
                OHSweepDownSeries.X.CopyDataIn(OHSweetDownX_List.ToArray)
                OHSweepDownSeries.Y.CopyDataIn(OHSweetDownY_List.ToArray)
                OHSweepDownSeries = Nothing
            End If

            ' Reset OH axis Y scale
            If ul > OHMaxValue Then OHMaxValue = ul
            If ul < OHMinValue Then OHMinValue = ul
            chartOH.ChartArea.AxisY.Max = Math.Round(OHMaxValue, 0) + 10
            chartOH.ChartArea.AxisY.Min = Math.Round(OHMinValue, 0) - 10

            If m_testTrace.Count > 1 Then
                Dim HOSweetUpX_List As New List(Of Single)
                Dim HOSweetUpY_List As New List(Of Single)
                Dim HOSweetDownX_List As New List(Of Single)
                Dim HOSweetDownY_List As New List(Of Single)
                Dim HOMaxValue As Double = m_testTrace(1).SweepUp(0).YData
                Dim HOMinValue As Double = m_testTrace(1).SweepUp(0).YData
                For Each point In m_testTrace(1).SweepUp
                    If point.YData > HOMaxValue Then HOMaxValue = point.YData
                    If point.YData < HOMinValue Then HOMinValue = point.YData
                    HOSweetUpX_List.Add(point.XData)
                    HOSweetUpY_List.Add(point.YData)
                Next
                For Each point In m_testTrace(1).SweepDown
                    If point.YData > HOMaxValue Then HOMaxValue = point.YData
                    If point.YData < HOMinValue Then HOMinValue = point.YData
                    HOSweetDownX_List.Add(point.XData)
                    HOSweetDownY_List.Add(point.YData)
                Next
                If m_testTrace(1).SweepUp.Count > 0 Then
                    chartHO.ChartArea.AxisX.Text = "Frequency(MHz)"
                    gbChartHO.Text = "[ HO Swept Freq Trace ]"
                    ' Plot PIM HO Trace
                    Dim HOSweepUpSeries As ChartDataSeries = seriesHO.AddNewSeries()
                    HOSweepUpSeries.Label = "HO Sweep Up"
                    HOSweepUpSeries.LineStyle.Thickness = 2
                    HOSweepUpSeries.LineStyle.Color = Color.Blue
                    HOSweepUpSeries.SymbolStyle.Shape = SymbolShapeEnum.Diamond
                    HOSweepUpSeries.SymbolStyle.Color = Color.Blue
                    HOSweepUpSeries.X.CopyDataIn(HOSweetUpX_List.ToArray)
                    HOSweepUpSeries.Y.CopyDataIn(HOSweetUpY_List.ToArray)
                    HOSweepUpSeries = Nothing
                End If


                If m_testTrace(1).SweepDown.Count > 0 Then
                    ' Plot PIM HO Trace
                    Dim HOSweepDownSeries As ChartDataSeries = seriesHO.AddNewSeries()
                    HOSweepDownSeries.Label = "HO Sweep Down"
                    HOSweepDownSeries.LineStyle.Thickness = 2
                    HOSweepDownSeries.LineStyle.Color = Color.DarkGreen
                    HOSweepDownSeries.SymbolStyle.Shape = SymbolShapeEnum.Diamond
                    HOSweepDownSeries.SymbolStyle.Color = Color.DarkGreen
                    HOSweepDownSeries.X.CopyDataIn(HOSweetDownX_List.ToArray)
                    HOSweepDownSeries.Y.CopyDataIn(HOSweetDownY_List.ToArray)
                    HOSweepDownSeries = Nothing
                End If

                ' Reset HO axis Y scale
                If ul > HOMaxValue Then HOMaxValue = ul
                If ul < HOMinValue Then HOMinValue = ul
                chartHO.ChartArea.AxisY.Max = Math.Round(HOMaxValue, 0) + 10
                chartHO.ChartArea.AxisY.Min = Math.Round(HOMinValue, 0) - 10
            End If

            ' Plot OH upper limit line
            Dim OHUpperLimitSeries As ChartDataSeries = seriesOH.AddNewSeries()
            Dim xSpec As Double() = {chartOH.ChartGroups(0).ChartData.MinX, chartOH.ChartGroups(0).ChartData.MaxX}
            Dim ySpec As Double() = {ul, ul}
            OHUpperLimitSeries.Label = "Upper Limit"
            OHUpperLimitSeries.X.CopyDataIn(xSpec)
            OHUpperLimitSeries.Y.CopyDataIn(ySpec)
            OHUpperLimitSeries.SymbolStyle.Shape = SymbolShapeEnum.None
            OHUpperLimitSeries.LineStyle.Pattern = LinePatternEnum.Solid
            OHUpperLimitSeries.LineStyle.Color = Color.Red
            OHUpperLimitSeries.LineStyle.Thickness = 2
            ' Plot HO upper limit
            Dim HOUpperLimitSeries As ChartDataSeries = seriesHO.AddNewSeries()
            HOUpperLimitSeries.Label = "Upper Limit"
            HOUpperLimitSeries.X.CopyDataIn(xSpec)
            HOUpperLimitSeries.Y.CopyDataIn(ySpec)
            HOUpperLimitSeries.SymbolStyle.Shape = SymbolShapeEnum.None
            HOUpperLimitSeries.LineStyle.Pattern = LinePatternEnum.Solid
            HOUpperLimitSeries.LineStyle.Color = Color.Red
            HOUpperLimitSeries.LineStyle.Thickness = 2


            ' Set default tooltips
            DataSeriesTooltipText = "Trace: {#TEXT}" & vbCrLf & "x = {#XVAL}" & vbCrLf & "y = {#YVAL}"
            ' Set tooltip for all series
            For Each dataSeries As ChartDataSeries In chartOH.ChartGroups(0).ChartData.SeriesList
                dataSeries.TooltipText = DataSeriesTooltipText
            Next
            For Each dataSeries As ChartDataSeries In chartHO.ChartGroups(0).ChartData.SeriesList
                dataSeries.TooltipText = DataSeriesTooltipText
            Next
        Catch ex As Exception
            Throw New Exception("FormPimPlot.PlotSfPimTrace()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub PlotFfPimTrace(ul As Decimal)
        Try
            If m_testTrace Is Nothing Then Return
            If m_testTrace.Count = 0 Then Return

        Catch ex As Exception
            Throw New Exception("FormPimPlot.PlotFfPimTrace()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub PlotCfPimTrace(ul As Decimal)
        Try
            If m_testTrace Is Nothing Then Return
            If m_testTrace.Count = 0 Then Return
        Catch ex As Exception
            Throw New Exception("FormPimPlot.PlotCfPimTrace()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub

    Private Sub AddPointToList(ByRef pointList As List(Of DataModels.FrequencyPoint), point As DataModels.FrequencyPoint)
        Try

            Dim imdPoint As New DataModels.FrequencyPoint

            imdPoint.SeriesId = point.SeriesId
            imdPoint.TxlFreq = point.TxlFreq
            imdPoint.TxrFreq = point.TxrFreq
            imdPoint.RxFreq = point.RxFreq
            imdPoint.XData = point.XData
            imdPoint.YData = point.YData

            pointList.Add(imdPoint)

        Catch ex As Exception
            Throw New Exception("FormPimPlot.AddPointToList()::" & ex.Message)
        End Try
    End Sub

    Private Sub InitializeGuiChart()
        Try
            InitChart(chartOH)
            InitChart(chartHO)
        Catch ex As Exception
            Throw New Exception("FormPimPlot.InitializeGuiChart()::" & ex.Message)
        End Try

    End Sub
    Private Sub InitChart(chart As C1Chart)
        Try
            ' Clear the existing data
            chart.ChartGroups(0).ChartData.SeriesList.Clear()
            chart.ChartLabels.LabelsCollection.Clear()

            ' Setup chart style
            chart.Style.BackColor = Color.Gainsboro
            chart.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.None
            chart.Style.Border.Thickness = 0

            ' Setup header
            chart.Header.Style.BackColor = Color.Bisque
            chart.Header.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.RaisedBevel
            chart.Header.Style.Border.Thickness = 1
            'chart.Header.Style.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            'chart.Header.Text = "RL"
            'chart.Header.Visible = True


            ' Setup chart area
            chart.ChartArea.Style.BackColor = Color.Gainsboro
            chart.ChartArea.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.None
            chart.ChartArea.Style.Border.Thickness = 0
            'chart.ChartArea.Size = New Size(chart.Width - 100, chart.ChartArea.Size.Height)

            ' Setup legend
            chart.Legend.Style.BackColor = Color.PaleGreen
            chart.Legend.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.Groove
            chart.Legend.Style.Border.Thickness = 2
            chart.Legend.Style.Border.Color = Color.LightBlue
            chart.Legend.Style.HorizontalAlignment = C1.Win.C1Chart.AlignHorzEnum.Center
            chart.Legend.Style.Font = New Font("Tahoma", 10)
            chart.Legend.Visible = True

            chart.ChartArea.AxisX.GridMajor.Visible = True
            chart.ChartArea.AxisY.GridMajor.Visible = True
            'chart.ChartArea.AxisX.GridMinor.Visible = True
            'chart.ChartArea.AxisY.GridMinor.Visible = True
            chart.ChartArea.AxisX.GridMajor.Color = Color.Black
            chart.ChartArea.AxisY.GridMajor.Color = Color.Black
            'chart.ChartArea.AxisX.GridMinor.Color = Color.Black
            'chart.ChartArea.AxisY.GridMinor.Color = Color.Black

            ' Enable tooltip
            chart.ToolTip.Enabled = True

            ' Reset axes limits
            chart.ChartArea.AxisX.AutoMax = True
            chart.ChartArea.AxisX.AutoMin = True
            chart.ChartArea.AxisY.AutoMax = True
            chart.ChartArea.AxisY.AutoMin = True
        Catch ex As Exception
            Throw New Exception("FormPimPlot.InitChart()::" & ex.Message)
        End Try
    End Sub


    Private Sub FormPimPlot_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        Try
            ChartLayout()
        Catch ex As Exception
            MsgBox("FromPimPlot.FormPimPlot_SizeChanged()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Sub ChartLayout()

        Dim fdescr As String = ""

        Try
            If m_testItem Is Nothing Then Return

            fdescr = m_testItem.cfg_imd_main.descr.ToUpper.Trim

            If fdescr.Contains("STS-L") Then
                ChartLayoutStsL()
            ElseIf fdescr.Contains("STS-J") Or fdescr.Contains("STS-K") Then
                ChartLayoutStsJK()
            ElseIf fdescr.Contains("STS-A") Or fdescr.Contains("STS-C") Then
                ChartLayoutStsAC(fdescr)
            ElseIf fdescr.Contains("STS-T") Then
                ChartLayoutStsT()
            Else
                Throw New Exception("ChartLayout()::" & "Not found " & fdescr)
            End If

            Me.SplitContainer1.SplitterDistance = (Me.Size.Height - Me.layoutTop.Size.Height) / 2

        Catch ex As Exception
            Throw New Exception("Not found " & fdescr)
        End Try
    End Sub
    Private Sub ChartLayoutStsAC(fdescr As String)
        Try

            gbChartOH.Visible = False
            'gbTimeSweep.Visible = False
            'gbTimeSweepLamda.Visible = False
            'gbSTSA.Visible = True
            gbSweepOH.Visible = False
            gb2ToneOH.Visible = False
            gbStsAOH.Visible = True

            'If fdescr.Contains("STS-A") Then
            '    g5.Text = "STS-A"
            '    gbSTSA.Text = "[ STS-A ]"
            'Else
            '    g5.Text = "STS-C"
            '    gbSTSA.Text = "[ STS-C ]"
            'End If


            My.Application.DoEvents()

            'gbSTSA.Left = 10
            'gbSTSA.Top = 50
            'gbSTSA.Width = (Me.Width - 30) * 5 / 6
            'gbSTSA.Height = Me.Height - 100

            'gbResult.Left = gbSTSA.Left + gbSTSA.Width + 10
            'gbResult.Top = gbSTSA.Top
            'gbResult.Width = (Me.Width - 30) / 6
            'gbResult.Height = gbSTSA.Height

            Dim x, y As Single
            x = (gbResult.Width - gbSweepOH.Width) / 2
            y = 20

            gbStsAOH.Left = x
            gbStsAOH.Top = y

            My.Application.DoEvents()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ChartLayoutStsT()
        Try


            'gbTimeSweep.Visible = True
            'gbTimeSweepLamda.Visible = True
            gbChartOH.Visible = False
            'gbSTSA.Visible = False
            gbSweepOH.Visible = False
            gb2ToneOH.Visible = True
            gbStsAOH.Visible = False

            'gbTimeSweep.Left = 20
            'gbTimeSweep.Top = 50
            'gbTimeSweep.Width = (Me.Width - 30) * 5 / 6
            'gbTimeSweep.Height = (Me.Height - 80) * 2 / 3
            'gbTimeSweep.Height = gbFreqSweep.Height

            'gbTimeSweepLamda.Left = 20
            'gbTimeSweepLamda.Top = 40 + gbTimeSweep.Height
            'gbTimeSweepLamda.Width = (Me.Width - 30) * 5 / 6
            'gbTimeSweepLamda.Height = (Me.Height - 80) / 3

            'gbResult.Left = gbTimeSweep.Left + gbTimeSweep.Width + 10
            'gbResult.Top = gbTimeSweep.Top
            gbResult.Width = (Me.Width - 30) / 6
            'gbResult.Height = gbTimeSweep.Height * 1.5

            Dim x, y As Single
            x = (gbResult.Width - gb2ToneOH.Width) / 2
            y = 20

            gb2ToneOH.Left = x
            gb2ToneOH.Top = y

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ChartLayoutStsL()
        Try


            'gbTimeSweep.Visible = True
            'gbTimeSweepLamda.Visible = True
            'gbSTSA.Visible = False
            gb2ToneOH.Visible = True
            gbStsAOH.Visible = False

            gbChartOH.Left = 10
            gbChartOH.Top = 50
            gbChartOH.Width = (Me.Width - 40) / 2
            gbChartOH.Height = (Me.Height - 100) / 2

            'gbTimeSweep.Left = gbFreqSweep.Width + 20
            'gbTimeSweep.Top = 50
            'gbTimeSweep.Width = gbFreqSweep.Width
            'gbTimeSweep.Height = gbFreqSweep.Height

            'gbTimeSweepLamda.Left = gbTimeSweep.Left
            'gbTimeSweepLamda.Top = 50 + gbFreqSweep.Height
            'gbTimeSweepLamda.Width = gbFreqSweep.Width
            'gbTimeSweepLamda.Height = gbFreqSweep.Height

            gbResult.Left = 10
            gbResult.Top = 50 + gbChartOH.Height
            gbResult.Width = gbChartOH.Width
            gbResult.Height = gbChartOH.Height
            My.Application.DoEvents()

            Dim x, y As Single
            x = (gbResult.Width - gbSweepOH.Width * 3) / 4
            y = (gbResult.Height - gbSweepOH.Height) / 3

            gbSweepOH.Left = x
            gbSweepOH.Top = y

            gb2ToneOH.Left = 2 * x + gbSweepOH.Width
            gb2ToneOH.Top = y


        Catch ex As Exception

        End Try
    End Sub
    Private Sub ChartLayoutStsJK()
        Try
            gb2ToneOH.Visible = False
            gbStsAOH.Visible = False

            Dim x, y As Single
            x = (gbResult.Width - gbSweepOH.Width) / 2
            y = 20

            gbSweepOH.Left = x
            gbSweepOH.Top = y

            gbSweepHO.Left = x
            gbSweepHO.Top = gbSweepOH.Top + gbSweepOH.Height + 10

            gbTestTime.Left = x
            gbTestTime.Top = gbSweepHO.Top + gbSweepHO.Height + 10
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class