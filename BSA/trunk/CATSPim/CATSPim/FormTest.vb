Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Runtime.InteropServices
Imports System.IO
Public Class FormTest
	'Declare Function BitBlt Lib "gdi32" Alias "BitBlt" (ByVal hDestDC As IntPtr， ByVal x As Integer， ByVal y As Integer， ByVal nWidth As Integer， ByVal nHeight As Integer， ByVal hSrcDC As IntPtr， ByVal xSrc As Integer， ByVal ySrc As Integer， ByVal dwRop As Integer) As Boolean
	Private Delegate Sub FixFrequencyTestThread(cycle As Short, seriesId As Short, ffReq As CATS.Model.cfg_imd_ffbox)
	Private Delegate Sub SweepFrequencyTestThread(cycle As Short, sfReq As CATS.Model.cfg_imd_sfbox) 'As DataModels.TestStatus
	Private Delegate Sub CustomFrequencyTestThread(cycle As Short, cfReq As CATS.Model.cfg_imd_cfbox) 'As DataModels.TestStatus

	Private m_reportItem As New List(Of TestReport.XmlFramework.TItem)
	Private m_testMode As DataModels.TestMode   ' 0-test,1-debug
	Private m_testCriteriaSpec As Dictionary(Of String, CATS.Model.cq_criteria_detail)
	Private m_testItem As CATS.Model.cq_spec_imd_details
	Private m_testTrace As New DataModels.TestTrace
	Private m_instrPowerLoss As DataModels.PowerLoss
	Private m_retDeviceList As Dictionary(Of String, DataModels.RetDevice)
	'Private m_testResult As TestModules.XmlReport
	Private m_txtReport As TestReport.TxtReport
	Private m_picReport As TestReport.PicReport
	Private m_comment As String
	Private m_storeIdx As Short = 0


	'Private _fileName As String
	Private m_testTime As DateTime

	Private m_sfReq As CATS.Model.cfg_imd_sfbox
	Private m_ffReq As CATS.Model.cfg_imd_ffbox
	Private m_cfReq As CATS.Model.cfg_imd_cfbox

	'Private m_testResult As TestReport.XmlFramework.TItem
	Public Property ReportItem As List(Of TestReport.XmlFramework.TItem)
		Get
			Return m_reportItem
		End Get
		Set(value As List(Of TestReport.XmlFramework.TItem))
			m_reportItem = value
		End Set
	End Property
	Public Property InstrPowerLoss() As DataModels.PowerLoss
		Get
			Return m_instrPowerLoss
		End Get
		Set(value As DataModels.PowerLoss)
			m_instrPowerLoss = value
		End Set
	End Property
	Public Property TestMode As DataModels.TestMode
		Get
			Return m_testMode
		End Get
		Set(value As DataModels.TestMode)

			m_testMode = value

			If value = DataModels.TestMode.Test Then
				txtSn.Enabled = False
			Else
				txtSn.Enabled = True
				pRTP.barcode = ""
			End If

			txtSn.Text = pRTP.barcode

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
	Public Property TestCriteriaSpec As Dictionary(Of String, CATS.Model.cq_criteria_detail)
		Get
			Return m_testCriteriaSpec
		End Get
		Set(value As Dictionary(Of String, CATS.Model.cq_criteria_detail))
			m_testCriteriaSpec = value
		End Set
	End Property
	Public Property RetDeviceList() As Dictionary(Of String, DataModels.RetDevice)
		Get
			Return m_retDeviceList
		End Get
		Set(value As Dictionary(Of String, DataModels.RetDevice))
			m_retDeviceList = value
		End Set
	End Property
	Public Function ReadImdValue() As Single
		Try

			Return pPimDev.ReadImd_dBc + InstrPowerLoss.Rx

		Catch ex As Exception
			Throw New Exception("FormTest::ReadImdValue()::" & vbCrLf & ex.Message)
		End Try

	End Function
	Public ReadOnly Property TestTrace() As DataModels.TestTrace

		Get
			Return m_testTrace
		End Get

	End Property
	Public Property PSFReq As CATS.Model.cfg_imd_sfbox
		Get
			Return m_sfReq
		End Get
		Set(value As CATS.Model.cfg_imd_sfbox)
			m_sfReq = value
		End Set
	End Property
	Public Property PFFReq As CATS.Model.cfg_imd_ffbox
		Get
			Return m_ffReq
		End Get
		Set(value As CATS.Model.cfg_imd_ffbox)
			m_ffReq = value
		End Set
	End Property
	Public Property PCFReq As CATS.Model.cfg_imd_cfbox
		Get
			Return m_cfReq
		End Get
		Set(value As CATS.Model.cfg_imd_cfbox)
			m_cfReq = value
		End Set
	End Property
	Private Sub AddPointToList(ByRef pointList As List(Of DataModels.FrequencyPoint),
									seriesId As Short, txlFreq As Single, txrFreq As Single, rxFreq As Single,
							   xData As Single, yData As Single, descr As String)
		Try

			Dim imdPoint As New DataModels.FrequencyPoint

			imdPoint.SeriesId = seriesId
			imdPoint.TxlFreq = txlFreq
			imdPoint.TxrFreq = txrFreq
			imdPoint.RxFreq = rxFreq
			imdPoint.XData = xData
			imdPoint.YData = yData
			'imdPoint.descr = "U"

			Select Case descr
				Case "T"
					m_testTrace.TwoTone.Add(imdPoint)
				Case "U"
					m_testTrace.SweepUp.Add(imdPoint)
					m_testTrace.Sweep.Add(imdPoint)
				Case "D"
					m_testTrace.SweepDown.Add(imdPoint)
					m_testTrace.Sweep.Add(imdPoint)
				Case "C"
					If m_testTrace.StsA.ContainsKey(seriesId) = False Then
						m_testTrace.StsA.Add(seriesId, New List(Of DataModels.FrequencyPoint))
					End If
					m_testTrace.StsA(seriesId).Add(imdPoint)
			End Select

			pointList.Add(imdPoint)

		Catch ex As Exception
			Throw New Exception("FormTest.AddPointToList()::" & ex.Message)
		End Try
	End Sub
	Private Sub FormTest_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


		lb_item.Text = TestItem.spec_detail.meas_item
		m_testTime = pGui.StartTestTime
		lb_down_tilt.Text = TestItem.spec_detail.dwtilt_angs

		'If m_ffReq Is Nothing Then
		'	gbTimeSweep.Visible = False
		'	gbTimeSweepLamda.Visible = False
		'	g2.Visible = False
		'Else
		'	gbTimeSweep.Visible = True
		'	gbTimeSweepLamda.Visible = True
		'	g2.Visible = True
		'End If

		My.Application.DoEvents()
		Me.Show()

		If TestMode = DataModels.TestMode.Test Then btnRun.PerformClick()

	End Sub
	Private Sub TwoToneFrequencyTest(cycle As Short, seriesId As Short, ffReq As CATS.Model.cfg_imd_ffbox)
		Try

            'If abort = True Then Return DataModels.TestStatus.Abort
            Dim tmp_time As Short = ffReq.duration_sec
			Dim tmp_start As DateTime

			Dim rxFreq As Single
			Dim rxValue As Single
			Dim dspPoint As PointF
			Dim imdPoints As New List(Of DataModels.FrequencyPoint)
			Dim txSetPower(1) As Double
			Dim txGetPower(1) As Double

			Dim tmp_date_diff As TimeSpan
			'Dim tmp_test_value As Single
			Dim tmp_loop_flag As Boolean = True

			rxFreq = ffReq.imd_freq


            '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
			pPimDev.ImdOrder = ffReq.imd_order
			pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)



			txSetPower(0) = ffReq.c1_power + InstrPowerLoss.TxL
			txSetPower(1) = ffReq.c2_power + InstrPowerLoss.TxR
			pPimDev.SetRFPower(txSetPower(0), txSetPower(1))
			pPimDev.RFPowerOnOff_OnePort(True, True)
			txGetPower = pPimDev.GetRFPower
			If (Math.Abs(txSetPower(0) - txGetPower(0))) > 0.5 Or (Math.Abs(txSetPower(1) - txGetPower(1)) > 0.5) Then
				Throw New Exception("The Tx output power may be a problem, Write() = " & txSetPower(0) & "," & txSetPower(1) & "; read = " & txGetPower(0) & "," & txGetPower(1))
			End If


            'pPimDev.SetRFPower(ffReq.c1_power + InstrPowerLoss.TxL, ffReq.c2_power + InstrPowerLoss.TxR)
            'pPimDev.RFPowerOnOff_OnePort(True, True)
            pPimDev.SetFrequency(ffReq.c1_freq, ffReq.c2_freq)
                       pPimDev.RFPowerRampUp(ffReq.c1_freq, ffReq.c2_freq, "OVERTIME",,, ffReq.duration_sec)'tt
            pPimDev.CorrectRFPower_TwoPort()
            pPimDev.SetPIMFreqMHz(ffReq.imd_freq) ' add by tony
#End If
            MyDelay(200)

			tmp_start = Now

			m_txtReport.WriteTwoToneHead(cycle)

            Do While (tmp_loop_flag)

                'If pAbortFlag = True Then
                '  abort = True
                '  Return DataModels.TestStatus.Abort
                'End If


                '#If INSTR_NORMAL_TEST Then

                '确认chamber door is closed or not
                '#If Not DEBUG Then             
                If pAppCfg.GetDoorCheck.Enable Then If Check_PIMChamberDoorClosed() = False Then Throw New Exception("  Chamber door is open now ,Please check!") ' ' add by tony for monitor door  
                '#End If


#If Not DEBUG Then
                rxValue = ReadImdValue()
#Else
                rxValue = GetVirtualImValue()
#End If

                dspPoint.X = tmp_date_diff.Milliseconds / 1000 + tmp_date_diff.Seconds + tmp_date_diff.Minutes * 60
                If dspPoint.X > tmp_time Then
                    dspPoint.X = tmp_time
                    tmp_loop_flag = False
                End If

                If rxValue < pRTP.AlgoParas.TwoTone_DiscardLimit Then rxValue = pRTP.AlgoParas.TwoTone_DiscardLimit

                dspPoint.Y = rxValue
                pMTDisplay.CRDisplayImdValue = rxValue

                'add_data_point_list(_fixed_data_points, Series, dspPoint.X, tmp_test_value, freq_point.txl, freq_point.txr, tmp_rx_freq, "T")
                AddPointToList(imdPoints, 1, ffReq.c1_freq, ffReq.c2_freq, ffReq.imd_freq, dspPoint.X, dspPoint.Y, "T")

                'cross_thread_refresh_chart_point(1, tmp_point, tmp_data_points)
                pMTDisplay.CRRefreshChartTimesweepPoint(seriesId, dspPoint, imdPoints)

                m_txtReport.WriteTestPoint("F" & cycle, imdPoints(imdPoints.Count - 1))
                tmp_date_diff = Now.Subtract(tmp_start)
                'My.Application.DoEvents()

                MyDelay(5)

            Loop
#If Not DEBUG Then
            pPimDev.ClearEnd() 'tt
#End If

            m_txtReport.WriteFileTerminal()

			m_testTrace.TwoToneFilter = Calculate.FilterDataPoint(m_testTrace.TwoTone)


		Catch ex As AbortedException
			Throw New AbortedException("TwoToneFrequencyTest()::" & ex.Message)

		Catch ex As Exception
			Throw New Exception("TwoToneFrequencyTest()::" & ex.Message)
		Finally

			Try
				'#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
				pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
			Catch ex As Exception
				MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
			End Try

		End Try
	End Sub

	Private Sub SweepFrequencyTest(cycle As Short, sfReq As CATS.Model.cfg_imd_sfbox) 'As DataModels.TestStatus
		Try

			'If pAbortFlag = True Then Return DataModels.TestStatus.Abort
			'If pAbortFlag = True Then Return DataModels.TestStatus.Abort

			Dim tmp_start As DateTime = Now
			Dim txlFreq As Single
			Dim txrFreq As Single
			Dim rxFreq As Single
			Dim rxValue As Single
			Dim txSetPower(1) As Double
			Dim txGetPower(1) As Double

			Dim imdPoints As New List(Of DataModels.FrequencyPoint)
            Dim dspPoint As PointF

            Dim PretestCount As Integer


            '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
            pPimDev.ImdOrder = sfReq.imd_order
			pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)

			txSetPower(0) = sfReq.power + InstrPowerLoss.TxL
			txSetPower(1) = sfReq.power + InstrPowerLoss.TxR
            pPimDev.SetRFPower(txSetPower(0), txSetPower(1))
            pPimDev.RFPowerOnOff_OnePort(True, True)
            txGetPower = pPimDev.GetRFPower
            If (Math.Abs(txSetPower(0) - txGetPower(0))) > 0.5 Or (Math.Abs(txSetPower(1) - txGetPower(1)) > 0.5) Then
                Throw New Exception("The Tx output power may be a problem, Write() = " & txSetPower(0) & "," & txSetPower(1) & "; read = " & txGetPower(0) & "," & txGetPower(1))
            End If
            MyDelay(500)
#End If


			m_txtReport.WriteSweepFreqHead()


			txrFreq = sfReq.ufreq_fixed

			If txrFreq <> 0 Then

                '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
				pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
				pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
                MyDelay(100)
               pPimDev.RFPowerRampUp(sfReq.ufreq_start, sfReq.ufreq_fixed, "UPSWEEP", sfReq.ufreq_stop, sfReq.ufreq_step) 'Huang Hua tt
#End If
                PretestCount = 0
                For txlFreq = sfReq.ufreq_start To sfReq.ufreq_stop Step sfReq.ufreq_step

                    '确认chamber door is closed or not
                    '#If Not DEBUG Then
                    ' If FormConfig.CheckDoor.Checked = True Then
                    If pAppCfg.GetDoorCheck.Enable Then If Check_PIMChamberDoorClosed() = False Then Throw New Exception("  Chamber door is open now ,Please check!") ' ' add by tony for monitor door  

                    '#End If

                    PretestCount = PretestCount + 1
                    If pRTP.M_phase_station_main.meas_type = 1 Then
                        If PretestCount = 2 Then txlFreq = sfReq.ufreq_start + CInt((sfReq.ufreq_stop - sfReq.ufreq_start) / 2)
                        If PretestCount = 3 Then txlFreq = sfReq.ufreq_stop
                    End If

                    'If pAbortFlag = True Then
                    '  abort = True
                    '  Return DataModels.TestStatus.Abort
                    'End If

                    '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
					pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
					MyDelay(10)
#End If

                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)

                    '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
                    pPimDev.SetPIMFreqMHz(rxFreq) 'add by tony
                    rxValue = ReadImdValue()

#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue
                    pMTDisplay.CRDisplayImdValue = rxValue


                    ' add_data_point_list(imdPoints, 1, tmp_rx_freq, tmp_test_value, tmp_txl_freq, tmp_txr_freq, tmp_rx_freq, "U")
                    AddPointToList(imdPoints, 1, txlFreq, txrFreq, rxFreq, dspPoint.X, dspPoint.Y, "U")

                    'cross_thread_refresh_chart_point(2, tmp_point, tmp_data_points)
                    pMTDisplay.CRRefreshChartFreqsweepPoint(1, dspPoint, imdPoints)

                    m_txtReport.WriteTestPoint("SU", imdPoints(imdPoints.Count - 1))

                    'My.Application.DoEvents()

                Next txlFreq
            End If

#If Not DEBUG Then
            pPimDev.ClearEnd() 'tt
#End If
            ' end up sweep


            ' start down sweep
            txlFreq = sfReq.dfreq_fixed

            If txlFreq <> 0 Then

                '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
				pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
                pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
             pPimDev.RFPowerRampUp(sfReq.dfreq_start, sfReq.dfreq_fixed, "DOWNSWEEP", sfReq.dfreq_stop, -sfReq.dfreq_step) 'tt

#End If
                PretestCount = 0
                For txrFreq = sfReq.dfreq_start To sfReq.dfreq_stop Step sfReq.dfreq_step * -1

                    '确认chamber door is closed or not
                    '#If Not DEBUG Then             
                    If pAppCfg.GetDoorCheck.Enable Then If Check_PIMChamberDoorClosed() = False Then Throw New Exception("  Chamber door is open now ,Please check!") ' ' add by tony for monitor door  
                    '#End If

                    PretestCount = PretestCount + 1
                    If pRTP.M_phase_station_main.meas_type = 1 Then
                        If PretestCount = 2 Then txrFreq = sfReq.dfreq_start - CInt((sfReq.dfreq_start - sfReq.dfreq_stop) / 2)
                        If PretestCount = 3 Then txrFreq = sfReq.dfreq_stop
                    End If

                    'If pAbortFlag = True Then
                    '  abort = True
                    '  Return DataModels.TestStatus.Abort
                    'End If

                    '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
					pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
#End If
                    MyDelay(10)

                    rxFreq = Calculate.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)

                    '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
                    pPimDev.SetPIMFreqMHz(rxFreq) 'add by tony
                    rxValue = ReadImdValue()
#Else
                    rxValue = GetVirtualImValue()
#End If

                    dspPoint.X = rxFreq
                    dspPoint.Y = rxValue

                    pMTDisplay.CRDisplayImdValue = rxValue

                    AddPointToList(imdPoints, 2, txlFreq, txrFreq, rxFreq, dspPoint.X, dspPoint.Y, "D")

                    pMTDisplay.CRRefreshChartFreqsweepPoint(2, dspPoint, imdPoints)

                    m_txtReport.WriteTestPoint("SD", imdPoints(imdPoints.Count - 1))



                Next txrFreq

                'CR_Test_refresh_freqsweep_chart_point(tmp_data_points)  '图形中重新设置最大、最小点（-170） 'disable at ver4.4
            End If



#If Not DEBUG Then
            pPimDev.ClearEnd() 'tt

#End If


            m_txtReport.WriteFileTerminal()


            'end down sweep

            'Return DataModels.TestStatus.Normal

        Catch exa As AbortedException

			Throw New AbortedException("SweepFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			'#If INSTR_NORMAL_TEST Then
			'      pInstrument.RFPowerOnOff_OnePort(False, False)
			'#End If
			Throw New Exception("SweepFrequencyTest()::" & ex.Message)

		Finally

			Try
				'#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
				pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
			Catch ex As Exception
				MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
			End Try
		End Try
	End Sub
	Private Sub CustomFrequencyTest(cycle As Short, cfReq As CATS.Model.cfg_imd_cfbox) 'As DataModels.TestStatus
		Try
			Dim tmp_start As DateTime = Now
			Dim txlFreqs() As String
			Dim txrFreqs() As String
			Dim rxFreqs() As String
			Dim trxFreq As New List(Of DataModels.FrequencyPoint)
			Dim tmpf As DataModels.FrequencyPoint

			Dim rxValue As Single
			Dim imdPoints As New List(Of DataModels.FrequencyPoint)
			Dim dspPoint As PointF

			'#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
			pPimDev.ImdOrder = cfReq.imd_order
			pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
            pPimDev.SetRFPower(cfReq.c1_power + InstrPowerLoss.TxL, cfReq.c2_power + InstrPowerLoss.TxR)

            pPimDev.RFPowerOnOff_OnePort(True, True)
      MyDelay(300)
#End If

      txlFreqs = cfReq.c1_freqs.Split(",")
			txrFreqs = cfReq.c2_freqs.Split(",")
			rxFreqs = cfReq.imd_freqs.Split(",")


            For fi = txlFreqs.GetLowerBound(0) To txlFreqs.GetUpperBound(0)
                tmpf = New DataModels.FrequencyPoint
                tmpf.TxlFreq = txlFreqs(fi)
                tmpf.TxrFreq = txrFreqs(fi)
                tmpf.RxFreq = rxFreqs(fi)
                trxFreq.Add(tmpf)
            Next


#If Not DEBUG Then
            pPimDev.RFPowerRampUp(txlFreqs(0), txrFreqs(0), "OVERTIME") 
#End If


            For j = 1 To cfReq.test_num

				For Each trxf In trxFreq
                    '#If INSTR_NORMAL_TEST Then

                    '确认chamber door is closed or not
                    '#If Not DEBUG Then
                    If pAppCfg.GetDoorCheck.Enable Then If Check_PIMChamberDoorClosed() = False Then Throw New Exception("  Chamber door is open now ,Please check!") ' ' add by tony for monitor door  
                    '#End If

#If Not DEBUG Then
					pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, trxf.TxlFreq)
                    pPimDev.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, trxf.TxrFreq)
                    pPimDev.CorrectRFPower_TwoPort()
                    pPimDev.SetPIMFreqMHz(trxf.RxFreq) 'add by tony   

                    MyDelay(50)
#End If

                    '#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
                    rxValue = ReadImdValue()
#Else
                    rxValue = GetVirtualImValue()
					pMTDisplay.CRDisplayImdValue = rxValue
#End If

					dspPoint.X = trxf.RxFreq
					dspPoint.Y = rxValue

					AddPointToList(imdPoints, j, trxf.TxlFreq, trxf.TxrFreq, trxf.RxFreq, dspPoint.X, dspPoint.Y, "C")

                    pMTDisplay.CRRefreshChartCustompPoint(j, dspPoint, imdPoints)

					m_txtReport.WriteTestPoint("STS-A" & j, imdPoints(imdPoints.Count - 1))

				Next
			Next

		Catch exa As AbortedException
			Throw New AbortedException("CustomFrequencyTest()::" & exa.Message)
		Catch ex As Exception
			Throw New Exception("CustomFrequencyTest()::" & ex.Message)
		Finally

			Try
				'#If INSTR_NORMAL_TEST Then
#If Not DEBUG Then
				pPimDev.RFPowerOnOff_OnePort(False, False)
#End If
			Catch ex As Exception
				MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
			End Try
		End Try
	End Sub


	Private Sub clear_textbox()
		txt_2tone_max.Clear()
		'txt_2tone_max_avg.Clear()
		txt_2tone_lambda_percent.Clear()
		'txt_2tone_max_pp.Clear()
		txt_2tone_std.Clear()
		'txt_2tone_max_std.Clear()
		txt_2tone_max.Clear()
		txt_2tone_lambda_percent.Clear()
		txt_2tone_std.Clear()

		'txt_gap_max.Clear()
		'txt_2tone_gap.Clear()
		txt_gap_updown.Clear()

		txt_swp_dw_max.Clear()
		'txt_swp_dw_maxerr.Clear()
		'txt_swp_dw_r1.Clear()
		'txt_swp_dw_r2.Clear()

		txt_swp_up_max.Clear()
		'txt_swp_up_maxerr.Clear()
		'txt_swp_up_r1.Clear()
		'txt_swp_up_r2.Clear()

		lblStatus.Text = ""

	End Sub
	Private Sub PreRun()
		Try
			Dim strFileName As String
			Dim strTxtFileName As String
			Dim strPicFileName As String

			lblStartTime.Text = Format(Now, "yyyy-MM-dd HH:mm:ss")
			lblStopTime.Text = ""
            lblDuration.Text = ""

            Application.DoEvents()
            If lblStartTime.Text.Trim = "0" Then Throw New Exception("FormTest.PreRun()::" & vbCrLf & "lblStartTime should be the present time , not 0, please check")

            'm_sfReq = TestModules.GetSweepFreqs(TestItem.cfg_imd_main.sfbox_ids)
            'm_ffReq = TestModules.GetFixedFreqs(TestItem.cfg_imd_main.ffbox_ids)
            'm_cfReq = TestModules.GetCustomFreqs(TestItem.cfg_imd_main.cfbox_ids)

            m_testTrace.SweepDown = New List(Of DataModels.FrequencyPoint)
			m_testTrace.SweepUp = New List(Of DataModels.FrequencyPoint)
			m_testTrace.Sweep = New List(Of DataModels.FrequencyPoint)
			m_testTrace.TwoTone = New List(Of DataModels.FrequencyPoint)
			m_testTrace.TwoToneFilter = New List(Of DataModels.FrequencyPoint)
			m_testTrace.Lambda = New List(Of PointF)
			m_testTrace.StsA = New Dictionary(Of String, List(Of DataModels.FrequencyPoint))

			clear_textbox()

			pRTP.barcode = txtSn.Text.Trim.ToUpper

			m_storeIdx += 1

			btnRun.Enabled = False

			strFileName = pRTP.barcode & "!" &
						  m_testTime.ToString("yyyyMMddHHmmss") & "!" &
						  pRTP.product_mode & "!" &
						  pRTP.M_product_main.product_name & "!" &
						  pRTP.phase & "!" &
						  m_testItem.spec_detail.meas_item & "!" &
						  m_storeIdx


			strTxtFileName = strFileName & ".txt"
			strPicFileName = strFileName & ".png"

			m_txtReport = New TestReport.TxtReport(pTestResultPath & strTxtFileName)
			m_picReport = New TestReport.PicReport(Me, pTestResultPath & strPicFileName)

			TestReport.XmlReport.PlotPath = strPicFileName
			TestReport.XmlReport.TracePath = strTxtFileName

			pAbortFlag = False

			lblStatus.ForeColor = Color.Black

			If m_storeIdx = 1 Then
				m_comment = "Fresh test"
				lblComment.Text = m_comment
			End If


			Dim power As Single
			If m_sfReq IsNot Nothing Then
				power = m_sfReq.power
			End If
			If m_sfReq Is Nothing And m_cfReq IsNot Nothing Then
				power = m_cfReq.c1_power
			End If
			m_txtReport.WriteHead(pRTP.barcode,
								  m_testItem.spec_imd_detail.port_to,
								  m_testItem.spec_detail.dwtilt_angs,
								  power,
								  m_retDeviceList,
								  m_comment)


		Catch ex As Exception
			Throw New Exception("FormTest.PreRun()::" & vbCrLf & ex.Message)
		End Try

	End Sub
	Private Sub PostRun()
        lblStopTime.Text = Format(Now, "yyyy-MM-dd HH:mm:ss")

        If lblStartTime.Text <> “0” Then lblDuration.Text = DateDiff(DateInterval.Second, CType(lblStartTime.Text, DateTime), CType(lblStopTime.Text, DateTime))

        btnRun.Enabled = True
		'btnAbort.Enabled = False
		Application.DoEvents()
        If m_picReport IsNot Nothing Then m_picReport.SaveFormImage()

    End Sub
	Private Sub MTSweepFrequencyTest()
		Try
			Dim thSwpFreqTest As New SweepFrequencyTestThread(AddressOf SweepFrequencyTest)
			Dim thResult As IAsyncResult
			' Dim tmpReturn As DataModels.TestStatus


			thResult = thSwpFreqTest.BeginInvoke(1, m_sfReq, Nothing, Nothing)
			Do While Not (thResult.AsyncWaitHandle.WaitOne(100))
				My.Application.DoEvents()
			Loop
			'tmpReturn =
			thSwpFreqTest.EndInvoke(thResult)
			'If tmpReturn = DataModels.TestStatus.Abort Then Throw New Exception("FormTest.btnRun_Click()::SweepTestAbort")

		Catch exa As AbortedException

			Throw New AbortedException("MTSweepFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			Throw New Exception("MTSweepFrequencyTest()::" & ex.Message)
		End Try


	End Sub
	Private Sub MTCustomFrequencyTest()
		Try
			Dim thCustFreqTest As New CustomFrequencyTestThread(AddressOf CustomFrequencyTest)
			Dim thResult As IAsyncResult
			' Dim tmpReturn As DataModels.TestStatus


			thResult = thCustFreqTest.BeginInvoke(1, m_cfReq, Nothing, Nothing)
			Do While Not (thResult.AsyncWaitHandle.WaitOne(100))
				My.Application.DoEvents()
			Loop
			'tmpReturn =
			thCustFreqTest.EndInvoke(thResult)
			'If tmpReturn = DataModels.TestStatus.Abort Then Throw New Exception("FormTest.btnRun_Click()::SweepTestAbort")

		Catch exa As AbortedException

			Throw New AbortedException("MTCustomFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			Throw New Exception("MTCustomFrequencyTest()::" & ex.Message)
		End Try


	End Sub
	Private Function DisplaySweepPointIn2ToneChart() As DataModels.FrequencyPoint
		Try

			If m_ffReq Is Nothing Then Return Nothing

			'#Region "Display sweep freq point"
			'Display sweep freq test value of selected fix freq point in 2tone chart area"
			'在点频测试图形区域中显示已选择点的扫频测试值"
			Dim resp As New DataModels.FrequencyPoint

			resp.TxlFreq = m_ffReq.c1_freq
			resp.TxrFreq = m_ffReq.c2_freq
			resp.RxFreq = m_ffReq.imd_freq
			resp = Calculate.FindPoint(m_testTrace.Sweep, resp)
			If resp IsNot Nothing Then
				pMTDisplay.DisplaySweepPoint(resp)
				'pMTDisplay.DisplayLamdaPoint(2, m_testTrace.TwoToneFilter, tp_2tonePoint)
			End If

			Return resp

			'#End Region

		Catch ex As Exception
			Throw New Exception("FormTest.DisplaySweepPointIn2ToneChart()::" & ex.Message)
		End Try
	End Function
	Private Sub MTTwoToneFrequencyTest()
		Try
			Dim thfixFreqTest As New FixFrequencyTestThread(AddressOf TwoToneFrequencyTest)
			Dim thResult As IAsyncResult
			Dim tmpReturn As DataModels.TestStatus
			Dim tmpAvg As Single
			Dim tmpStd As Single

			thResult = thfixFreqTest.BeginInvoke(1, 1, m_ffReq, Nothing, Nothing)
			Do While Not (thResult.AsyncWaitHandle.WaitOne(100))
				My.Application.DoEvents()
			Loop
			'tmpReturn =
			thfixFreqTest.EndInvoke(thResult)
			If tmpReturn = DataModels.TestStatus.Abort Then Throw New Exception("FormTest.btnRun_Click()::2ToneTestAbort")

			tmpAvg = Calculate.Average(m_testTrace.TwoTone)
			tmpStd = Calculate.Stdev(m_testTrace.TwoTone)

			With ChartSweepTime
				.Series(2).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Minimum, tmpAvg - 6 * tmpStd)
				.Series(2).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Maximum, tmpAvg - 6 * tmpStd)
				.Series(3).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Minimum, tmpAvg + 6 * tmpStd)
				.Series(3).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Maximum, tmpAvg + 6 * tmpStd)
			End With

		Catch exa As AbortedException
			Throw New AbortedException("MTTwoToneFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			Throw New Exception("MTTwoToneFrequencyTest()::" & ex.Message)
		End Try
	End Sub
	Private Sub RunAllTest()
        Try
            Dim TestCount As Int16 = 3
            Dim TestRec As Int16 = 1
            Dim FExitDo As Boolean = False
            Dim fpoint As New DataModels.FrequencyPoint

            Do While (FExitDo = False)
                pTestCable.AddConsumedNum(1)
                TestRec += 1
                Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
                Dim meas_status As String = ""

                Try

#If Not DEBUG Then
                    pPimDev.FreqBand = pAppCfg.GetInstrumentConfig(m_testItem.cfg_imd_main.freq_band).BandIdx  'for PIM700LU， 只有RSBG/Rflight运行有用，其他都没有用
                    pPimDev.FreqBandSet = pAppCfg.GetInstrumentConfig(m_testItem.cfg_imd_main.freq_band).BandName ' FOR zulu add 700LU,只有zulu运行有用，其他都没有用

                    '20181123 700LU Cal offset  分别提取700L和700U的offset=================================
                    Dim instrPara1 As CATSPimConfig.LocalConfig.InstrPara
                    Dim m_PowerLoss1 As New DataModels.PowerLoss
                    instrPara1 = pAppCfg.GetInstrumentConfig(m_testItem.cfg_imd_main.freq_band)
                    m_PowerLoss1.TxL = instrPara1.Tx1Loss
                    m_PowerLoss1.TxR = instrPara1.Tx2Loss
                    m_PowerLoss1.Rx = instrPara1.RxLoss
                    InstrPowerLoss = m_PowerLoss1
                    '======================================================================================

#End If
                    Dim x As Integer = pAppCfg.GetInstrumentConfig(m_testItem.cfg_imd_main.freq_band).BandIdx
                    If m_testItem.spec_imd_detail.vibe_enable And pAppCfg.GetVibrationConfig.Enable Then
                        If pRTP.M_phase_station_main.phase_station.Trim.ToUpper <> "PRETEST" Then
                            pVibCtrl.StartVib()
                        End If
                    End If


                    Dim pr As New TestReport.ReportModule


                    PreRun()
                    pMTDisplay = New DelegateDisplay.MTFormTest(Me)
                    pMTDisplay.InitializeGuiChart(m_sfReq, m_ffReq, m_cfReq, m_testItem.spec_detail.limit_low, m_testItem.spec_detail.limit_up)


                    'sweep test
                    If m_sfReq IsNot Nothing Then
                        MTSweepFrequencyTest()
                        'display sweep point
                        fpoint = DisplaySweepPointIn2ToneChart()
                    End If


                    '2 tone test
                    If m_ffReq IsNot Nothing Then
                        MTTwoToneFrequencyTest()
                        m_testTrace.Lambda = Calculate.LambdaTrace(m_testTrace.TwoToneFilter)
                        pMTDisplay.DisplayLamdaPoint(2, m_testTrace.TwoToneFilter, fpoint)
                        pMTDisplay.RefreshLambdaChart(1, m_testTrace.Lambda)
                    End If

                    If m_cfReq IsNot Nothing Then
                        MTCustomFrequencyTest()
                    End If

                    crList = pr.GetMeasCriteriaList(m_testTrace, m_testItem, m_testCriteriaSpec)

                    pr.PrintTResultDcf(crList)

                    pr.PrintTResultGui(Me, crList)

                    meas_status = "N"

                Catch exa As AbortedException

                    meas_status = "A"
                    MsgBox("FormTest.btnRun_Click()::" & exa.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

                Catch ex As Exception

                    meas_status = "E"

                    MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

                Finally
                    Try
                        'DTFTestTime = "" '默认值 DTF
                        'If TestRec = 2 And meas_status <> "P" And meas_status <> "N" Then ' only for Pass
                        '    DTFTestTime = GetDTFTime("65536", "2") 'pPimDev' DTF test， 时间或者NA
                        '    If DTFTestTime.Contains("delay=") Then
                        '        DTFTestTime = DTFTestTime.Replace("delay=", "")
                        '    Else
                        '        DTFTestTime = ""
                        '    End If
                        'End If


                        Dim tpItem As TestReport.XmlFramework.TItem
                        tpItem = TestReport.XmlReport.WriteTestItem(m_testItem, m_testTrace, crList, meas_status, TestRec)  'DTF
                        tpItem.MeasString = m_comment
                        m_reportItem.Add(tpItem)

                        RefreshStatusLabel(tpItem.MeasStatus)
                        PostRun()

                        If m_testItem.spec_imd_detail.vibe_enable And pAppCfg.GetVibrationConfig.Enable Then
                            If pRTP.M_phase_station_main.phase_station.Trim.ToUpper <> "PRETEST" Then
                                pVibCtrl.StopVib()
                            End If
                        End If

                        If tpItem.MeasStatus = "P" Then
                            FExitDo = True
                            Exit Try
                        End If

                        If TestRec > TestCount Then
                            FExitDo = True
                            Exit Try
                        End If

                        FormRetryReason.ShowDialog()
                        If FormRetryReason.ReturnTestType = 1 Then FExitDo = True : Exit Try 'click exit
                        m_comment = FormRetryReason.ReturnMsg
                        lblComment.Text = m_comment

                    Catch ex As Exception
                        meas_status = "E"
                        MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    End Try
                End Try

            Loop

            If TestMode = DataModels.TestMode.Test Then btnExit.PerformClick()

        Catch ex As Exception
            '=================================================解决拉电调异常时直接退出不关闭8222
            If m_testItem.spec_imd_detail.vibe_enable And pAppCfg.GetVibrationConfig.Enable Then
                If pRTP.M_phase_station_main.phase_station.Trim.ToUpper <> "PRETEST" Then
                    pVibCtrl.StopVib()
                End If
            End If
            CloseVibDevice()
            '==================================================

            Throw New Exception("RunAllTest()::" & ex.Message)
        End Try

    End Sub
	Private Sub btnRun_Click(sender As System.Object, e As System.EventArgs) Handles btnRun.Click
		Try
			RunAllTest()
		Catch ex As Exception
			MsgBox("btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
		'Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
		'Dim meas_status As String = ""

		'Try

		'  Dim pr As New TestReport.ReportModule

		'  PreRun()

		'  pMTDisplay = New DelegateDisplay.MTFormTest(Me)

		'  pMTDisplay.InitializeGuiChart(m_sfReq, m_ffReq, m_testItem.spec_detail.limit_low, m_testItem.spec_detail.limit_up)



		'  'sweep test
		'  MTSweepFrequencyTest()

		'  'display sweep point
		'  Dim fpoint As New DataModels.FrequencyPoint
		'  fpoint = DisplaySweepPointIn2ToneChart()

		'  '2 tone test
		'  MTTwoToneFrequencyTest()

		'  m_testTrace.Lambda = Calculate.LambdaTrace(m_testTrace.TwoToneFilter)

		'  pMTDisplay.DisplayLamdaPoint(2, m_testTrace.TwoToneFilter, fpoint)
		'  pMTDisplay.RefreshLambdaChart(1, m_testTrace.Lambda)

		'  crList = pr.GetMeasCriteriaList(m_testTrace, m_testItem, m_testCriteriaSpec)

		'  pr.PrintTResultDcf(crList)

		'  pr.PrintTResultGui(Me, crList)

		'  meas_status = "N"

		'Catch exa As AbortedException

		'  meas_status = "A"
		'  MsgBox("FormTest.btnRun_Click()::" & exa.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

		'Catch ex As Exception

		'  meas_status = "E"

		'  MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

		'Finally
		'  Try
		'    Dim tpItem As TestReport.XmlFramework.TItem
		'    tpItem = TestReport.XmlReport.WriteTestItem(m_testItem, m_testTrace, crList, meas_status)

		'    m_reportItem.Add(tpItem)

		'    RefreshStatusLabel(tpItem.MeasStatus)
		'    PostRun()
		'    If tpItem.MeasStatus = "P" Then
		'      If TestMode = DataModels.TestMode.Test Then btnExit.PerformClick()
		'    Else
		'      If m_storeIdx >= 5 Then btnExit.PerformClick()
		'      FormRetryReason.ShowDialog()
		'      If FormRetryReason.ReturnTestType = 1 Then btnExit.PerformClick()
		'      m_comment = FormRetryReason.ReturnMsg
		'      lblComment.Text = m_comment
		'    End If


		'  Catch ex As Exception
		'    meas_status = "E"
		'    MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		'  End Try
		'End Try

	End Sub
	Private Sub RefreshStatusLabel(stat As String)
		Try
			If stat = "P" Then
				lblStatus.Text = "Pass"
				lblStatus.ForeColor = Color.Green
				'btnExit.PerformClick()
			ElseIf stat = "F" Then
				lblStatus.Text = "Fail"
				lblStatus.ForeColor = Color.Red
			ElseIf stat = "E" Then
				lblStatus.Text = "Error"
				lblStatus.ForeColor = Color.Red
			ElseIf stat = "A" Then
				lblStatus.Text = "Abort"
				lblStatus.ForeColor = Color.Orange
			End If

		Catch ex As Exception

		End Try
	End Sub
	'  Private Sub btnRun_Click(sender As System.Object, e As System.EventArgs) Handles btnRun.Click
	'    Dim tp_status As Short = 1

	'    Try

	'      PreRun()

	'      'pGui.StartTestGroup(_testItem)

	'      pMTDisplay.InitializeGuiChart(m_sfReq, m_ffReq, m_testItem.spec_detail.limit_low, m_testItem.spec_detail.limit_up)

	'      Dim thSwpFreqTest As New SweepFrequencyTestThread(AddressOf SweepFrequencyTest)
	'      Dim thfixFreqTest As New FixFrequencyTestThread(AddressOf FixFrequencyTest)
	'      Dim thResult As IAsyncResult
	'      Dim tmpReturn As DataModels.TestStatus
	'      Dim tmpAvg As Single
	'      Dim tmpStd As Single
	'      Dim pr As New TestReport.ReportModule
	'      Dim crList As Dictionary(Of String, CATS.Model.meas_criteria)



	'#Region "Sweep Test"
	'      Try
	'        thResult = thSwpFreqTest.BeginInvoke(1, m_sfReq, pAbortFlag, Nothing, Nothing)
	'        Do While Not (thResult.AsyncWaitHandle.WaitOne(100))
	'          My.Application.DoEvents()
	'        Loop
	'        tmpReturn = thSwpFreqTest.EndInvoke(pAbortFlag, thResult)
	'        If tmpReturn = DataModels.TestStatus.Abort Then Throw New Exception("FormTest.btnRun_Click()::SweepTestAbort")
	'      Catch ex As Exception
	'        Throw New Exception("FormTest.btnRun_Click()::SweepTest()::" & ex.Message)
	'      End Try
	'#End Region

	'#Region "Display sweep freq point"
	'      'Display sweep freq test value of selected fix freq point in 2tone chart area"
	'      '在点频测试图形区域中显示已选择点的扫频测试值"
	'      Dim tp_2tonePoint As New DataModels.FrequencyPoint
	'      tp_2tonePoint.TxlFreq = m_ffReq.c1_freq
	'      tp_2tonePoint.TxrFreq = m_ffReq.c2_freq
	'      tp_2tonePoint.RxFreq = m_ffReq.imd_freq
	'      tp_2tonePoint = TestModules.FindPoint(m_testTrace.Sweep, tp_2tonePoint)
	'      If tp_2tonePoint IsNot Nothing Then pMTDisplay.DisplaySweepPoint(tp_2tonePoint)
	'      ' display_sweep_point(tmp_fixed_freq.normal)

	'#End Region

	'#Region "2 Tone Test"
	'      Try

	'        thResult = thfixFreqTest.BeginInvoke(1, 1, m_ffReq, pAbortFlag, Nothing, Nothing)
	'        Do While Not (thResult.AsyncWaitHandle.WaitOne(100))
	'          My.Application.DoEvents()
	'        Loop
	'        tmpReturn = thfixFreqTest.EndInvoke(thResult)
	'        If tmpReturn = DataModels.TestStatus.Abort Then Throw New Exception("FormTest.btnRun_Click()::2ToneTestAbort")

	'        tmpAvg = TestClass.Calculate.Average(m_testTrace.TwoTone)
	'        tmpStd = TestClass.Calculate.Stdev(m_testTrace.TwoTone)

	'        With ChartSweepTime
	'          .Series(2).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Minimum, tmpAvg - 6 * tmpStd)
	'          .Series(2).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Maximum, tmpAvg - 6 * tmpStd)
	'          .Series(3).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Minimum, tmpAvg + 6 * tmpStd)
	'          .Series(3).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Maximum, tmpAvg + 6 * tmpStd)
	'        End With
	'      Catch ex As Exception
	'        Throw New Exception("FormTest.btnRun_Click()::2ToneTest()::" & ex.Message)
	'      End Try


	'#End Region

	'#Region "Lambda Test"
	'      pMTDisplay.DisplayLamdaPoint(2, m_testTrace.TwoToneFilter, tp_2tonePoint)

	'      m_testTrace.Lambda = TestClass.Calculate.LambdaTrace(m_testTrace.TwoToneFilter)
	'      pMTDisplay.RefreshLambdaChart(1, m_testTrace.Lambda)


	'      crList = pr.GetMeasCriteriaList(m_testTrace, m_testItem, m_testSpecPhase.TestCriteriaList)
	'      pr.PrintTResultDcf(crList)
	'      pr.PrintTResultGui(Me, crList)

	'      tp_status = crList.Count * -1
	'      For Each cs As KeyValuePair(Of String, CATS.Model.meas_criteria) In crList
	'        If cs.Value.meas_status.Trim.ToUpper = "P" Then tp_status += 1
	'      Next

	'      'tp_status = IIf(tp_status = crList.Count, 1, 0)

	'#End Region




	'    Catch ex As Exception

	'      MsgBox("FormTest.RunTest()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

	'    Finally

	'      m_testResult = New TestReport.XmlFramework.TItem
	'      With m_testResult
	'        .MeasString = m_testItem.spec_detail.meas_item
	'        .MeasStatus = IIf(pAbortFlag = True, "A", IIf(tp_status = 0, "P", "F"))
	'        .MeasValue =
	'      End With

	'      If tp_status = True Then
	'        lblStatus.Text = "Pass"
	'        lblStatus.ForeColor = Color.Green
	'        btnExit.PerformClick()
	'      Else
	'        lblStatus.Text = "Fail"
	'        lblStatus.ForeColor = Color.Red
	'      End If

	'      PostRun()
	'      'SaveTestReport

	'    End Try

	'  End Sub
	Private Sub btnAbort_Click(sender As System.Object, e As System.EventArgs) Handles btnAbort.Click
		pAbortFlag = True
	End Sub
	'Private Sub SaveImage(filepath As String)
	'  Me.WindowState = FormWindowState.Maximized
	'  Me.TopMost = True
	'  My.Application.DoEvents()
	'  'Threading.Thread.Sleep(500)
	'  'My.Application.DoEvents()
	'  'Dim image As Bitmap = New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
	'  Dim image As Bitmap = New Bitmap(Me.Width, Me.Height)
	'  Dim imgGraphics As Graphics = Graphics.FromImage(image)

	'  imgGraphics.CopyFromScreen(0, 0, 0, 0, Me.Size)
	'  '  imgGraphics.CopyFromScreen(0, 0, 0, 0, New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
	'  'imgGraphics.CopyFromScreen(0, 0, 0, 0, New Size(Screen.AllScreens(0).Bounds.Width, Screen.AllScreens(0).Bounds.Height))
	'  image.Save(filepath, Drawing.Imaging.ImageFormat.Png)
	'  Me.TopMost = False
	'End Sub
	'Private Sub SaveFormImage(filepath As String)
	'  Me.WindowState = FormWindowState.Maximized

	'  Me.TopMost = True
	'  'Me.Focus()
	'  My.Application.DoEvents()

	'  'Dim rect As Rectangle = New Rectangle

	'  'rect = Screen.GetWorkingArea(Me)


	'  '创建一个以当前窗体为模板的图象
	'  Dim g1 As Graphics = Me.CreateGraphics

	'  '创建以窗体大小为标准的位图 
	'  Dim MyBMP As Bitmap = New Bitmap(Me.Width, Me.Height, g1) '定义位图的大小


	'  '创建一个位图Bitmap绘图图面
	'  Dim g2 As Graphics = Graphics.FromImage(MyBMP)

	'  '得到窗体的DC（句柄）
	'  Dim dc1 As IntPtr = g1.GetHdc()

	'  '得到Bitmap的DC 
	'  Dim dc2 As IntPtr = g2.GetHdc()
	'  '复制图块的光栅操作码
	'  Dim SRCCOPY As Integer = 13369376
	'  '调用此API函数，实现窗体捕获
	'  'BitBlt(dc2, intX, intY, intWidth, intHeight, dc1, intLeft, intRight, SRCCOPY);//13369376

	'  'BitBlt(hDcSave, 0, 0, Me.Width, Me.Height, hDcSrc, 0, 0, SRCCOPY) ';//13369376
	'  BitBlt(dc2, 0, 0, Me.Width, Me.Height, dc1, 0, 0, SRCCOPY) ';//13369376
	'  '释放掉屏幕的DC
	'  g1.ReleaseHdc(dc1)
	'  '释放掉Bitmap的DC 
	'  g2.ReleaseHdc(dc2)
	'  '以JPG文件格式来保存
	'  MyBMP.Save(filepath, Drawing.Imaging.ImageFormat.Png) 'Application.StartupPath + "\\
	'  Me.TopMost = False
	'End Sub

	Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
		Try

			'If pAbortFlag = True Then
			'  pGui.RecordResult("AbortTest", 1, 0, 0)
			'End If
			Me.Close()

		Catch ex As Exception

		End Try
	End Sub
	Private Sub ChartLayoutStsA()
		Try

			gbFreqSweep.Visible = False
			gbTimeSweep.Visible = False
			gbTimeSweepLamda.Visible = False
			g1.Visible = False
			g2.Visible = False
			g4.Visible = False
			g5.Visible = True



			gbSTSA.Left = 10
			gbSTSA.Top = 50
			gbSTSA.Width = (Me.Width - 30) * 5 / 6
			gbSTSA.Height = Me.Height - 100

			gbResult.Left = gbSTSA.Left + gbSTSA.Width + 10
			gbResult.Top = gbSTSA.Top
			gbResult.Width = (Me.Width - 30) / 6
			gbResult.Height = gbSTSA.Height

			Dim x, y As Single
			x = (gbResult.Width - g1.Width) / 2
			y = 20

			g5.Left = x
			g5.Top = y

			g3.Left = x
			g3.Top = y + g1.Height + 10


		Catch ex As Exception

		End Try
	End Sub
	Private Sub ChartLayoutStsL()
		Try


			gbTimeSweep.Visible = True
			gbTimeSweepLamda.Visible = True
			gbSTSA.Visible = False
			g2.Visible = True
			g5.Visible = False

			gbFreqSweep.Left = 10
			gbFreqSweep.Top = 50
			gbFreqSweep.Width = (Me.Width - 40) / 2
			gbFreqSweep.Height = (Me.Height - 100) / 2

			gbTimeSweep.Left = gbFreqSweep.Width + 20
			gbTimeSweep.Top = 50
			gbTimeSweep.Width = gbFreqSweep.Width
			gbTimeSweep.Height = gbFreqSweep.Height

			gbTimeSweepLamda.Left = gbTimeSweep.Left
			gbTimeSweepLamda.Top = 50 + gbFreqSweep.Height
			gbTimeSweepLamda.Width = gbFreqSweep.Width
			gbTimeSweepLamda.Height = gbFreqSweep.Height

			gbResult.Left = 10
			gbResult.Top = 50 + gbFreqSweep.Height
			gbResult.Width = gbFreqSweep.Width
			gbResult.Height = gbFreqSweep.Height
			My.Application.DoEvents()

			Dim x, y As Single
			x = (gbResult.Width - g1.Width * 3) / 4
			y = (gbResult.Height - g1.Height - g4.Height) / 3

			g1.Left = x
			g1.Top = y

			g2.Left = 2 * x + g1.Width
			g2.Top = y

			g3.Left = 3 * x + g1.Width + g2.Width
			g3.Top = y

			g4.Left = g1.Left
			g4.Top = g3.Top + g3.Height + 10
			g4.Width = g3.Left - g1.Left + g1.Width

		Catch ex As Exception

		End Try
	End Sub
	Private Sub ChartLayoutStsJK()
		Try
			gbTimeSweep.Visible = False
			gbTimeSweepLamda.Visible = False
			gbSTSA.Visible = False
			g2.Visible = False
			g5.Visible = False

			gbFreqSweep.Left = 10
			gbFreqSweep.Top = 50
			gbFreqSweep.Width = (Me.Width - 30) * 5 / 6
			gbFreqSweep.Height = Me.Height - 100

			gbResult.Left = gbFreqSweep.Left + gbFreqSweep.Width + 10
			gbResult.Top = gbFreqSweep.Top
			gbResult.Width = (Me.Width - 30) / 6
			gbResult.Height = gbFreqSweep.Height

			Dim x, y As Single
			x = (gbResult.Width - g1.Width) / 2
			y = 20

			g1.Left = x
			g1.Top = y

			g3.Left = x
			g3.Top = y + g1.Height + 10

			g4.Left = x
			g4.Top = g3.Top + g3.Height + 10
			g4.Width = g1.Width


		Catch ex As Exception

		End Try
	End Sub
	Private Sub ChartLayout()

		Dim fdescr As String = ""

		Try
			If m_testItem Is Nothing Then Return

			fdescr = m_testItem.cfg_imd_main.descr.ToUpper.Trim

            If fdescr.Contains("STS-L") Or fdescr.Contains("STS-T") Then
                ChartLayoutStsL()
            ElseIf fdescr.Contains("STS-J") Or fdescr.Contains("STS-K") Or fdescr.Contains("STS-M") Then
                ChartLayoutStsJK()
            ElseIf fdescr.Contains("STS-A") Then
                ChartLayoutStsA()
            Else
                Throw New Exception("ChartLayout()::" & "Not found " & fdescr)
			End If

		Catch ex As Exception
			Throw New Exception("Not found " & fdescr)
		End Try
	End Sub
	'Private Sub ChartLayout()

	'	If m_ffReq IsNot Nothing Then
	'		gbFreqSweep.Left = 10
	'		gbFreqSweep.Top = 50
	'		gbFreqSweep.Width = (Me.Width - 40) / 2
	'		gbFreqSweep.Height = (Me.Height - 100) / 2

	'		ChartSweepFreq.Left = 5
	'		ChartSweepFreq.Top = 15
	'		ChartSweepFreq.Width = gbFreqSweep.Width - 10
	'		ChartSweepFreq.Height = gbFreqSweep.Height - 20

	'		gbTimeSweep.Left = gbFreqSweep.Width + 20
	'		gbTimeSweep.Top = 50
	'		gbTimeSweep.Width = gbFreqSweep.Width
	'		gbTimeSweep.Height = gbFreqSweep.Height

	'		ChartSweepTime.Left = 5
	'		ChartSweepTime.Top = 15
	'		ChartSweepTime.Width = gbTimeSweep.Width - 10
	'		ChartSweepTime.Height = gbTimeSweep.Height - 20

	'		gbTimeSweepLamda.Left = gbTimeSweep.Left
	'		gbTimeSweepLamda.Top = 50 + gbFreqSweep.Height
	'		gbTimeSweepLamda.Width = gbFreqSweep.Width
	'		gbTimeSweepLamda.Height = gbFreqSweep.Height
	'		ChartLamda.Left = 5
	'		ChartLamda.Top = 15
	'		ChartLamda.Width = gbTimeSweepLamda.Width - 10
	'		ChartLamda.Height = gbTimeSweepLamda.Height - 20

	'		gbResult.Left = 10
	'		gbResult.Top = 50 + gbFreqSweep.Height
	'		gbResult.Width = gbFreqSweep.Width
	'		gbResult.Height = gbFreqSweep.Height
	'		My.Application.DoEvents()

	'		Dim x, y As Single
	'		x = (gbResult.Width - g1.Width * 3) / 4
	'		y = (gbResult.Height - g1.Height - g4.Height) / 3

	'		g1.Left = x
	'		g1.Top = y

	'		g2.Left = 2 * x + g1.Width
	'		g2.Top = y

	'		g3.Left = 3 * x + g1.Width + g2.Width
	'		g3.Top = y

	'		g4.Left = g1.Left
	'		g4.Top = g3.Top + g3.Height + 10
	'		g4.Width = g3.Left - g1.Left + g1.Width


	'	Else
	'		My.Application.DoEvents()
	'		gbFreqSweep.Left = 10
	'		gbFreqSweep.Top = 50
	'		gbFreqSweep.Width = (Me.Width - 30) * 5 / 6
	'		gbFreqSweep.Height = Me.Height - 100

	'		'ChartSweepFreq.Left = 5
	'		'ChartSweepFreq.Top = 15
	'		'ChartSweepFreq.Width = gbFreqSweep.Width - 10
	'		'ChartSweepFreq.Height = gbFreqSweep.Height - 20

	'		gbResult.Left = gbFreqSweep.Left + gbFreqSweep.Width + 10
	'		gbResult.Top = gbFreqSweep.Top
	'		gbResult.Width = (Me.Width - 30) / 6
	'		gbResult.Height = gbFreqSweep.Height

	'		Dim x, y As Single
	'		x = (gbResult.Width - g1.Width) / 2
	'		y = 20 '(gbResult.Height - g1.Height - g2.Height - g3.Height) / 4

	'		g1.Left = x
	'		g1.Top = y
	'		'g1.Width = gbResult.Width - 20


	'		g3.Left = x
	'		g3.Top = y + g1.Height + 10

	'		g4.Left = x
	'		g4.Top = g3.Top + g3.Height + 10
	'		g4.Width = g1.Width


	'	End If


	'End Sub
	Private Sub FormTest_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
		Try
			ChartLayout()
		Catch ex As Exception
			MsgBox("FormTest_SizeChanged()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try

	End Sub


End Class