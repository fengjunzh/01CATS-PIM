Public Class ThreadTest
	Private m_RTPhase As New DataModels.RTimePhasePara
	Private m_testControl As CATSRunBox

	'Public m_MAlgoParas As New AlgorithmLimit
	'Public m_MEqList As List(Of DataModels.Instrument)
	'Public M_PowerLoss As New DataModels.PowerLoss
	'Public M_CriteriaItems As New Dictionary(Of String, CATS.Model.cq_criteria_detail)


	Private m_phaseModel As TestSpec.TestPhase
	Private m_devCtrl As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	Private m_testTime As DateTime
	Private m_testReport As TestReport.XmlReport
	Private WithEvents TestControlEvent As CATSRunBox
	'Private m_devPara As List(Of DataModels.Instrument)
	'Private m_PowerLoss As New DataModels.PowerLoss


	Public Sub New(phaseModel As TestSpec.TestPhase)
		Try
			m_phaseModel = phaseModel

			m_RTPhase.M_AlgoParas = GetAlgoParasBySpecMainId(phaseModel.SpecMainId)

			m_testReport = New TestReport.XmlReport(m_RTPhase)

		Catch ex As Exception
			Throw New Exception("ThreadTest.New()::" & ex.Message)
		End Try
	End Sub
	Public Property TestControl As CATSRunBox
		Get
			Return m_testControl
		End Get
		Set(value As CATSRunBox)
			m_testControl = value
		End Set
	End Property
	Public Sub RunTest() Handles TestControlEvent.RunTestHandler


	End Sub
	Public Sub AbortTest() Handles TestControlEvent.AbortTestHandler


	End Sub
	Public Sub RetryTest() Handles TestControlEvent.RetryTestHandler


	End Sub

	'Public Property PimDevice As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	'	Get
	'		Return m_devCtrl
	'	End Get
	'	Set(value As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice)
	'		m_devCtrl = value
	'	End Set
	'End Property
	'Public Property PimDeviceParameters As List(Of DataModels.Instrument)
	'	Get
	'		Return m_devPara
	'	End Get
	'	Set(value As List(Of DataModels.Instrument))
	'		m_devPara = value
	'	End Set
	'End Property
	Public Property RetList As Dictionary(Of String, DataModels.RetDevice)
		Get
			Return m_RTPhase.M_RetList
		End Get
		Set(value As Dictionary(Of String, DataModels.RetDevice))
			m_RTPhase.M_RetList = value
		End Set
	End Property
	Public Property PhaseModel As TestSpec.TestPhase
		Get
			Return m_phaseModel
		End Get
		Set(value As TestSpec.TestPhase)
			m_phaseModel = value
		End Set
	End Property
	Private Function GetVirtualImValue() As Single
		Try
			Randomize()

			Dim x As Single = CInt(580 * Rnd() + 15800) / 100

			Threading.Thread.Sleep(200)

			'Return -153
			Return Math.Round((-1) * x, 2)

		Catch ex As Exception
			Return 0
		End Try
	End Function
	Private Function GetFreqBandName() As String
		Try

			Return m_phaseModel.TestGroupList(0).TestItemList(0).cfg_imd_main.freq_band

		Catch ex As Exception
			Throw New Exception("GetFreqBandName()::" & ex.Message)
		End Try

	End Function
	Private Function GetBandParas() As CATSPimConfig.LocalConfig.InstrPara
		Try
			Dim resp As CATSPimConfig.LocalConfig.InstrPara

			resp = pAppCfg.GetInstrumentConfig(GetFreqBandName)

			Return resp

		Catch ex As Exception
			Throw New Exception("GetBandParas()::" & ex.Message)
		End Try
	End Function
	Public Sub OpenPimDevice()
		Try
			Dim resp As New List(Of DataModels.Instrument)

			Dim instrPara As CATSPimConfig.LocalConfig.InstrPara
			Dim dev As New DataModels.Instrument

			instrPara = GetBandParas()

			If instrPara.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
				Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
				devRos.Address = instrPara.Address
				devRos.Open()

				dev.Idn = devRos.ReadIDN
				dev.SerialNumber = devRos.Serial_Number
				dev.Model = devRos.Model
				dev.Hardware = ""
				dev.Firmware = devRos.FW_Revision

				devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
				devRos.FreqBand = instrPara.BandIdx
				devRos.RFPowerOnOff_TwoPorts(False)

				'devRos.Close()

				resp.Add(dev)

				m_devCtrl = devRos

			ElseIf instrPara.Vendor.Trim.ToUpper = "Summitek".ToString.ToUpper Then
				Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
				devSum.Address = instrPara.Address
				devSum.Open()

				dev.Idn = " "
				dev.SerialNumber = " "
				dev.Model = " " ' devSum.Model
				dev.Hardware = " "
				dev.Firmware = " " 'devSum.FW_Revision

				devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
				devSum.FreqBand = instrPara.BandIdx
				devSum.RFPowerOnOff_TwoPorts(False)

				'devSum.Close()

				resp.Add(dev)

				m_devCtrl = devSum

			ElseIf instrPara.Vendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
				Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
				devKae.Address = instrPara.Address
				devKae.Open()

				dev.Idn = ""
				dev.Model = devKae.Model
				dev.Hardware = ""
				dev.Firmware = devKae.FW_Revision

				devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
				devKae.FreqBand = instrPara.BandIdx
				devKae.RFPowerOnOff_TwoPorts(False)

				'devKae.Close()

				resp.Add(dev)

				m_devCtrl = devKae

			ElseIf instrPara.Vendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
				Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
				devRfl.Address = instrPara.Address
				devRfl.Open()

				dev.Idn = devRfl.ReadIDN
				dev.SerialNumber = devRfl.Serial_Number
				dev.Model = devRfl.Model
				dev.Hardware = ""
				dev.Firmware = devRfl.FW_Revision

				devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
				devRfl.FreqBand = instrPara.BandIdx
				devRfl.RFPowerOnOff_TwoPorts(False)

				'devRfl.Close()

				resp.Add(dev)

				m_devCtrl = devRfl


			End If

			m_RTPhase.M_PowerLoss.TxL = instrPara.Tx1Loss
			m_RTPhase.M_PowerLoss.TxR = instrPara.Tx2Loss
			m_RTPhase.M_PowerLoss.Rx = instrPara.RxLoss

			m_RTPhase.M_EqList = resp

		Catch ex As Exception
			Throw New Exception("OpenPimDevice()::" & ex.Message)
		End Try
	End Sub
	Public Function ClosePimDevice() As Boolean
		Try

			m_devCtrl.Close()

			Return True

		Catch ex As Exception
			Throw New Exception("ClosePimDevice()::" & ex.Message)
		End Try

	End Function
	Public Function RunPhaseTest() As Boolean
		Dim phReport As New TestReport.XmlFramework.TPhase

		Try

			Dim status As String = "P"
			Dim gpRp As New TestReport.XmlFramework.TGroup

			m_RTPhase.phase = m_phaseModel.Name
			m_RTPhase.meas_watch.Restart()
			m_RTPhase.conn_watch.Reset()
			m_RTPhase.meas_start_time = pGui.StartTestTime
			m_RTPhase.spec_main_id = m_phaseModel.SpecMainId
			m_RTPhase.phase_main_id = m_phaseModel.PhaseMainId


			m_RTPhase.M_CriteriaItems = m_phaseModel.TestCriteriaList


			For Each group In m_phaseModel.TestGroupList

				Try

					RunGroupTest(group, gpRp)

					If gpRp Is Nothing Then Throw New Exception("TestError") 'status = "E" : Exit For

				Catch ex1 As AbortedException
					Throw New AbortedException
				Catch ex As Exception
					Throw New Exception(ex.Message)
				Finally
					phReport.TestGroups.Add(gpRp)
				End Try


				If status = "P" And gpRp.GroupStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
				If status = "F" Then Exit For
#End If

			Next

			phReport.MeasStatus = status

			Return True

		Catch exa As AbortedException

			phReport.MeasStatus = "A"
			pGui.RecordResult("TestPhaseAborted", 1, 0, 0)
			Return False

		Catch ex As Exception


			phReport.MeasStatus = "E"
			pGui.RecordResult("TestPhaseError", 1, 0, 0)
			MsgBox("RunTestPhase()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False

		Finally

			m_RTPhase.meas_watch.Stop()
			m_RTPhase.meas_time = m_RTPhase.meas_watch.Elapsed.TotalSeconds
			m_RTPhase.conn_time = m_RTPhase.conn_watch.Elapsed.TotalSeconds
			m_RTPhase.meas_stop_time = Now
			m_RTPhase.total_time = DateDiff(DateInterval.Second, m_RTPhase.meas_start_time, m_RTPhase.meas_stop_time)
			WriteReport(phReport)

		End Try
	End Function
	Public Sub RunGroupTest(group As TestSpec.TestGroup, ByRef outTGroup As TestReport.XmlFramework.TGroup)
		Dim resp As New TestReport.XmlFramework.TGroup
		Dim rspItem As New List(Of TestReport.XmlFramework.TItem)

		Try

			' If cancel = True Then Return Nothing
			pGui.StartTestGroup(group.Name)

			Dim status As String = "P"

			resp.GroupMainId = group.Id
			resp.GroupName = group.Name



			For Each item In group.TestItemList

				Try

					RunItemTest(item, rspItem)

					If rspItem Is Nothing Then Throw New Exception("TestError")

				Catch ex1 As AbortedException

					Throw New AbortedException

				Catch ex As Exception

					Throw New Exception(ex.Message)

				Finally

					If rspItem IsNot Nothing Then
						For Each ti In rspItem
							resp.TestItems.Add(ti)
						Next
					End If



				End Try



				'If status = "E" Or status = "A" Then Exit For
				'If itRp.MeasStatus = "E" Or itRp.MeasStatus = "A" Then status = itRp.MeasStatus : Exit For
				If status = "P" And rspItem(rspItem.Count - 1).MeasStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
				If status = "F" Then Exit For
#End If

			Next

			resp.GroupStatus = status

			'Return resp

		Catch exa As AbortedException

			resp.GroupStatus = "A"
			pGui.RecordResult("TestGroupAborted", -1, 0, 0)
			Throw New AbortedException

		Catch ex As Exception

			'  cancel = True
			resp.GroupStatus = "E"
			pGui.RecordResult("TestGroupError", -1, 0, 0)
			Throw New Exception("RunTestGroup()::" & ex.Message)
			'Return resp

		Finally

			'RunTestGroup = resp
			outTGroup = resp
			pGui.StopTestGroup()

		End Try
	End Sub
	Public Function VirtualTestItem(item As TestSpec.TestItem) As List(Of TestReport.XmlFramework.TItem)
		Try
			Dim resp As New List(Of TestReport.XmlFramework.TItem)
			Dim respItem As New TestReport.XmlFramework.TItem

			respItem.SpecDetailId = item.spec_detail.id
			respItem.MeasString = "" ' spec.spec_detail.meas_item
			respItem.OrderIdx = item.spec_detail.order_idx
			respItem.TiltIdxs = item.spec_detail.dwtilt_idxs
			respItem.TiltAngs = item.spec_detail.dwtilt_angs

			resp.Add(respItem)

			Return resp

		Catch ex As Exception
			Throw New Exception("VirtualTestItem()::" & ex.Message)
		End Try
	End Function
	Public Sub SetRetTilt(tiltL As List(Of ThreadRet.RetTiltModel))
		Try
			For Each tilt In tiltL
				pRetThread.AddRetItem(tilt)
			Next

			pRTGlobal.semporeRet.IncrementOne()

			Do While (pRTGlobal.semporeRet.SignalValue > 0)
				Threading.Thread.Sleep(500)
			Loop

		Catch ex As Exception
			Throw New Exception("SetRetTilt()::" & ex.Message)
		End Try
	End Sub
	Public Sub RunItemTest(item As TestSpec.TestItem, ByRef outTItem As List(Of TestReport.XmlFramework.TItem))
		Dim resp As New List(Of TestReport.XmlFramework.TItem)

		Try

			Dim tiltL As New List(Of ThreadRet.RetTiltModel)
			Dim tiltItem As ThreadRet.RetTiltModel

			If item.spec_detail.message IsNot Nothing Then
				If item.spec_detail.message.Length >= 5 Then
					m_RTPhase.meas_watch.Stop()
					m_RTPhase.conn_watch.Start()
					If MsgBox(item.spec_detail.message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
						m_RTPhase.conn_watch.Stop()
						Throw New AbortedException("TestAborted")
					End If
					m_RTPhase.conn_watch.Stop()
					m_RTPhase.meas_watch.Start()
				End If
			End If


			'Set downtilt 
			tiltItem = New ThreadRet.RetTiltModel
			tiltItem.PhaseName = PhaseModel.Name
			tiltItem.RetIdx = item.spec_detail.dwtilt_idxs
			tiltItem.RetTilt = item.spec_detail.dwtilt_angs
			tiltL.Add(tiltItem)

			SetRetTilt(tiltL)

			'Run test
			'Dim frm As FormTest

			'frm = New FormTest
			'frm.TestMode = DataModels.TestMode.Test
			'frm.RetDeviceList = m_RTPhase.M_RetList
			'frm.TestItem = item
			'frm.InstrPowerLoss = m_RTPhase.M_PowerLoss
			'frm.TestCriteriaSpec = m_RTPhase.M_CriteriaItems
			'frm.ShowDialog()

			resp = RunItemTestFunc(item)






			'resp = frm.ReportItem

			If resp(resp.Count - 1).MeasStatus = "E" Then Throw New Exception("TestError")
			If resp(resp.Count - 1).MeasStatus = "A" Then Throw New AbortedException


		Catch exa As AbortedException

			If resp.Count = 0 Then resp = VirtualTestItem(item)
			resp(resp.Count - 1).MeasStatus = "A"
			resp(resp.Count - 1).MeasString = "TestAborted"

			pGui.RecordResult("TestItemAborted", -1, 0, 0)

			Throw New AbortedException()

		Catch ex As Exception

			If resp.Count = 0 Then resp = VirtualTestItem(item)
			resp(resp.Count - 1).MeasString = ex.Message '"TestError"
			resp(resp.Count - 1).MeasStatus = "E"

			pGui.RecordResult("TestItemError", -1, 0, 0)
			Throw New Exception("RunTestItem()::" & ex.Message)

		Finally

			m_RTPhase.conn_watch.Stop()
			outTItem = resp

			If item.spec_imd_detail.vibe_enable And pAppCfg.GetVibrationConfig.Enable Then
				pVibCtrl.StopVib()
			End If

		End Try
	End Sub

#Region "RunItemTestFunc"
	Private m_testTrace As New DataModels.TestTrace
	Private m_txtReport As TestReport.TxtReport
	Private m_sfReq As CATS.Model.cfg_imd_sfbox
	Private m_ffReq As CATS.Model.cfg_imd_ffbox
	Private m_cfReq As CATS.Model.cfg_imd_cfbox
	Private m_storeIdx As Short = 0

	Private m_comment As String
	Private Sub PreRun(testItem As TestSpec.TestItem)
		Try
			Dim strFileName As String
			Dim strTxtFileName As String
			Dim strPicFileName As String

			m_sfReq = TestModules.GetSweepFreqs(testItem.cfg_imd_main.sfbox_ids)
			m_ffReq = TestModules.GetFixedFreqs(testItem.cfg_imd_main.ffbox_ids)
			m_cfReq = TestModules.GetCustomFreqs(testItem.cfg_imd_main.cfbox_ids)

			m_testTrace.SweepDown = New List(Of DataModels.FrequencyPoint)
			m_testTrace.SweepUp = New List(Of DataModels.FrequencyPoint)
			m_testTrace.Sweep = New List(Of DataModels.FrequencyPoint)
			m_testTrace.TwoTone = New List(Of DataModels.FrequencyPoint)
			m_testTrace.TwoToneFilter = New List(Of DataModels.FrequencyPoint)
			m_testTrace.Lambda = New List(Of PointF)

			'clear_textbox()


			m_storeIdx += 1

			'btnRun.Enabled = False

			strFileName = pRTGlobal.barcode & "!" &
				 	m_testTime.ToString("yyyyMMddHHmmss") & "!" &
					pRTGlobal.product_mode & "!" &
					pRTGlobal.M_product_main.product_name & "!" &
					m_RTPhase.phase & "!" &
				testItem.spec_detail.meas_item & "!" &
					m_storeIdx


			strTxtFileName = strFileName & ".txt"
			strPicFileName = strFileName & ".png"

			m_txtReport = New TestReport.TxtReport(pTestResultPath & strTxtFileName)
			'm_picReport = New TestReport.PicReport(Me, pTestResultPath & strPicFileName)

			'TestReport.XmlReport.PlotPath = strPicFileName
			m_testReport.TracePath = strTxtFileName

			pAbortFlag = False

			m_testControl.StatusForeColor = Color.Black

			'm_comment = lblComment.Text
			'm_comment = ""
			If m_storeIdx = 1 Then
				m_comment = "Fresh test"
				'lblComment.Text = m_comment
			End If

			m_txtReport.WriteHead(pRTGlobal.barcode, m_RTPhase.phase,
							testItem.spec_imd_detail.port_to,
							testItem.spec_detail.dwtilt_angs,
							m_sfReq.power,
						m_RTPhase.M_RetList,
							m_comment)

			'lblStartTime.Text = Format(Now, "yyyy-MM-dd HH:mm:ss")
			'lblStopTime.Text = ""
			'lblDuration.Text = ""

		Catch ex As Exception
			Throw New Exception("FormTest.PreRun()::" & vbCrLf & ex.Message)
		End Try

	End Sub
	Private Sub PostRun()
		'lblStopTime.Text = Format(Now, "yyyy-MM-dd HH:mm:ss")
		'lblDuration.Text = DateDiff(DateInterval.Second, CType(lblStartTime.Text, DateTime), CType(lblStopTime.Text, DateTime))

		'btnRun.Enabled = True
		'btnAbort.Enabled = False
		Application.DoEvents()
		'm_picReport.SaveFormImage()

	End Sub
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
			End Select

			pointList.Add(imdPoint)

		Catch ex As Exception
			Throw New Exception("ThreadTest.AddPointToList()::" & ex.Message)
		End Try
	End Sub
	Public Function GetLambdaTrace(x As List(Of DataModels.FrequencyPoint)) As List(Of PointF)
		Try
			Dim resp As New List(Of PointF)

			Dim tp_avg As Single
			Dim tp_value As Single
			Dim tp_point As PointF
			Dim cal As New Calculate
			' Dim tp_limit As Single = DataModels.AlgorithmLimit.Lambda


			tp_avg = cal.Average(x)

			For Each p In x

				'tp_value = Math.Round((IIf(p.YData < -158, p.YData, -158) + 140) ^ 2 / Math.Abs(tp_avg - p.YData), 1)
				'tp_value = IIf(tp_value < 400, tp_value, 400)

				tp_value = Math.Round((IIf(p.YData < m_RTPhase.M_AlgoParas.Lambda_CalcLimit, p.YData, m_RTPhase.M_AlgoParas.Lambda_CalcLimit) + m_RTPhase.M_AlgoParas.Lambda_CalcCompensation) ^ 2 / Math.Abs(tp_avg - p.YData), 1)
				tp_value = IIf(tp_value < m_RTPhase.M_AlgoParas.Lambda_MaxLimit, tp_value, m_RTPhase.M_AlgoParas.Lambda_MaxLimit)


				tp_point.X = p.XData
				tp_point.Y = tp_value

				resp.Add(tp_point)

			Next

			Return resp

		Catch ex As Exception
			Throw New Exception("ThreadTest.GetLambdaTrace()::" & ex.Message)
		End Try
	End Function
	Public Function ReadImdValue() As Single
		Try

			Return m_devCtrl.ReadImd_dBc + m_RTPhase.M_PowerLoss.Rx

		Catch ex As Exception
			Throw New Exception("ThreadTest.ReadImdValue()::" & vbCrLf & ex.Message)
		End Try

	End Function
	Public Function FilterDataPoint(sweepTrace As List(Of DataModels.FrequencyPoint),
							   timeTrace As List(Of DataModels.FrequencyPoint)) As List(Of DataModels.FrequencyPoint)
		Try
			Dim tmpStd As Single
			Dim tmpAvg As Single
			Dim rnt As New List(Of DataModels.FrequencyPoint)
			Dim cal As New Calculate

			tmpStd = cal.Stdev(sweepTrace)
			tmpAvg = cal.Average(timeTrace)

			For Each p In timeTrace
				If p.YData >= tmpAvg - m_RTPhase.M_AlgoParas.TwoTone_FilterCoefficient * tmpStd And p.YData <= tmpAvg + m_RTPhase.M_AlgoParas.TwoTone_FilterCoefficient * tmpStd Then
					rnt.Add(p)
				End If
			Next

			Return rnt

		Catch ex As Exception
			Throw New Exception("ThreadTest.FilterDataPoint()::" & vbCrLf & ex.Message)
		End Try
	End Function
	Private Sub TwoToneFrequencyTest(cycle As Short, seriesId As Short, ffReq As CATS.Model.cfg_imd_ffbox)
		Try

			'If abort = True Then Return DataModels.TestStatus.Abort


			Dim tmp_time As Short = ffReq.duration_sec
			Dim tmp_start As DateTime

			Dim rxFreq As Single
			Dim rxValue As Single
			Dim dspPoint As PointF
			Dim imdPoints As New List(Of DataModels.FrequencyPoint)


			Dim tmp_date_diff As TimeSpan
			'Dim tmp_test_value As Single
			Dim tmp_loop_flag As Boolean = True
			Dim cal As New Calculate

			rxFreq = ffReq.imd_freq


#If INSTR_NORMAL_TEST Then
			m_devCtrl.ImdOrder = ffReq.imd_order
			m_devCtrl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
			m_devCtrl.SetRFPower(ffReq.c1_power + m_RTPhase.M_PowerLoss.TxL, ffReq.c2_power + m_RTPhase.M_PowerLoss.TxR)
			m_devCtrl.RFPowerOnOff_OnePort(True, True)
			m_devCtrl.SetFrequency(ffReq.c1_freq, ffReq.c2_freq)
			m_devCtrl.CorrectRFPower_TwoPort()
#End If
			MyDelay(200)

			tmp_start = Now

			m_txtReport.WriteTwoToneHead(cycle)

			Do While (tmp_loop_flag)

				'If pAbortFlag = True Then
				'  abort = True
				'  Return DataModels.TestStatus.Abort
				'End If


#If INSTR_NORMAL_TEST Then
				rxValue = ReadImdValue()
#Else
				rxValue = GetVirtualImValue()
#End If

				dspPoint.X = tmp_date_diff.Milliseconds / 1000 + tmp_date_diff.Seconds + tmp_date_diff.Minutes * 60
				If dspPoint.X > tmp_time Then
					dspPoint.X = tmp_time
					tmp_loop_flag = False
				End If

				If rxValue < m_RTPhase.M_AlgoParas.TwoTone_DiscardLimit Then rxValue = m_RTPhase.M_AlgoParas.TwoTone_DiscardLimit

				dspPoint.Y = rxValue
				'pMTDisplay.CRDisplayImdValue = rxValue

				'add_data_point_list(_fixed_data_points, Series, dspPoint.X, tmp_test_value, freq_point.txl, freq_point.txr, tmp_rx_freq, "T")
				AddPointToList(imdPoints, 1, ffReq.c1_freq, ffReq.c2_freq, ffReq.imd_freq, dspPoint.X, dspPoint.Y, "T")

				'cross_thread_refresh_chart_point(1, tmp_point, tmp_data_points)
				m_testControl.AddPointToChart2Tone(seriesId, dspPoint)

				m_txtReport.WriteTestPoint("F" & cycle, imdPoints(imdPoints.Count - 1))
				tmp_date_diff = Now.Subtract(tmp_start)
				'My.Application.DoEvents()

				MyDelay(5)

			Loop

			m_txtReport.WriteFileTerminal()

			m_testTrace.TwoToneFilter = FilterDataPoint(m_testTrace.Sweep, m_testTrace.TwoTone)

			Dim tmpAvg, tmpStd As Single
			tmpAvg = cal.Average(m_testTrace.TwoTone)
			tmpStd = cal.Stdev(m_testTrace.TwoTone)

			m_testControl.AddStdTraceToChart2Tone(tmpAvg, tmpStd)


		Catch ex As AbortedException
			Throw New AbortedException("TwoToneFrequencyTest()::" & ex.Message)

		Catch ex As Exception
			Throw New Exception("TwoToneFrequencyTest()::" & ex.Message)
		Finally

			Try
#If INSTR_NORMAL_TEST Then
				m_devCtrl.RFPowerOnOff_OnePort(False, False)
#End If
			Catch ex As Exception
				MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
			End Try

		End Try
	End Sub

	Private Sub SweepFrequencyTest(cycle As Short, sfReq As CATS.Model.cfg_imd_sfbox) 'As DataModels.TestStatus
		Try

			'If pAbortFlag = True Then Return DataModels.TestStatus.Abort


			Dim tmp_start As DateTime = Now
			Dim txlFreq As Single
			Dim txrFreq As Single
			Dim rxFreq As Single
			Dim rxValue As Single

			Dim imdPoints As New List(Of DataModels.FrequencyPoint)
			Dim samplePoint As PointF
			Dim markPoint As New DataModels.FrequencyPoint
			Dim cal As New Calculate

			If m_ffReq IsNot Nothing Then
				markPoint.TxlFreq = m_ffReq.c1_freq
				markPoint.TxrFreq = m_ffReq.c2_freq
				markPoint.RxFreq = m_ffReq.imd_freq
			End If

			'Dim dspPoint As PointF


#If INSTR_NORMAL_TEST Then
			m_devCtrl.ImdOrder = sfReq.imd_order
			m_devCtrl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
			m_devCtrl.SetRFPower(sfReq.power + m_RTPhase.M_PowerLoss.TxL, sfReq.power + m_RTPhase.M_PowerLoss.TxR)

			m_devCtrl.RFPowerOnOff_OnePort(True, True)
			MyDelay(500)
#End If

			m_txtReport.WriteSweepFreqHead()


			txrFreq = sfReq.ufreq_fixed

#If INSTR_NORMAL_TEST Then
			m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
			m_devCtrl.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
			MyDelay(50)
#End If

			If txrFreq > 0 Then
				For txlFreq = sfReq.ufreq_start To sfReq.ufreq_stop Step sfReq.ufreq_step

					'If pAbortFlag = True Then
					'  abort = True
					'  Return DataModels.TestStatus.Abort
					'End If

#If INSTR_NORMAL_TEST Then
				m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
				m_devCtrl.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
				MyDelay(10)
#End If

					rxFreq = cal.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)

#If INSTR_NORMAL_TEST Then

				rxValue = ReadImdValue()

#Else
					rxValue = GetVirtualImValue()
#End If

					samplePoint.X = rxFreq
					samplePoint.Y = rxValue
					'pMTDisplay.CRDisplayImdValue = rxValue


					' add_data_point_list(imdPoints, 1, tmp_rx_freq, tmp_test_value, tmp_txl_freq, tmp_txr_freq, tmp_rx_freq, "U")
					AddPointToList(imdPoints, 1, txlFreq, txrFreq, rxFreq, samplePoint.X, samplePoint.Y, "U")

					'cross_thread_refresh_chart_point(2, tmp_point, tmp_data_points)
					'pMTDisplay.CRRefreshChartFreqsweepPoint(1, dspPoint, imdPoints)


					m_testControl.AddPointToChartSweep(1, samplePoint, markPoint)

					m_txtReport.WriteTestPoint("SU", imdPoints(imdPoints.Count - 1))

					'My.Application.DoEvents()

				Next txlFreq
			End If

			' end up sweep


			' start down sweep
			txlFreq = sfReq.dfreq_fixed

#If INSTR_NORMAL_TEST Then
			m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, txlFreq)
			m_devCtrl.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
#End If
			If txlFreq > 0 Then
				For txrFreq = sfReq.dfreq_start To sfReq.dfreq_stop Step sfReq.dfreq_step * -1
					'If pAbortFlag = True Then
					'  abort = True
					'  Return DataModels.TestStatus.Abort
					'End If

#If INSTR_NORMAL_TEST Then
				m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, txrFreq)
				m_devCtrl.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
#End If
					MyDelay(10)

					rxFreq = cal.GetImFreq(txlFreq, txrFreq, sfReq.imd_side, sfReq.imd_order)
#If INSTR_NORMAL_TEST Then
				rxValue = ReadImdValue()
#Else
					rxValue = GetVirtualImValue()
#End If

					samplePoint.X = rxFreq
					samplePoint.Y = rxValue

					'pMTDisplay.CRDisplayImdValue = rxValue

					AddPointToList(imdPoints, 2, txlFreq, txrFreq, rxFreq, samplePoint.X, samplePoint.Y, "D")

					'pMTDisplay.CRRefreshChartFreqsweepPoint(2, samplePoint, imdPoints)
					m_testControl.AddPointToChartSweep(2, samplePoint, markPoint)

					m_txtReport.WriteTestPoint("SD", imdPoints(imdPoints.Count - 1))

				Next txrFreq
			End If

			m_txtReport.WriteFileTerminal()

		Catch exa As AbortedException

			Throw New AbortedException("SweepFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			Throw New Exception("SweepFrequencyTest()::" & ex.Message)

		Finally

			Try
#If INSTR_NORMAL_TEST Then
				m_devCtrl.RFPowerOnOff_OnePort(False, False)
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

			Dim rxValue As Single
			Dim imdPoints As New List(Of DataModels.FrequencyPoint)
			Dim dspPoint As PointF

#If INSTR_NORMAL_TEST Then
			m_devCtrl.ImdOrder = cfReq.imd_order
			m_devCtrl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
			m_devCtrl.SetRFPower(cfReq.c1_power + m_RTPhase.M_PowerLoss.TxL, cfReq.c1_power + m_RTPhase.M_PowerLoss.TxR)

			m_devCtrl.RFPowerOnOff_OnePort(True, True)
			MyDelay(500)
#End If

			txlFreqs = cfReq.c1_freqs.Split(",")
			txrFreqs = cfReq.c2_freqs.Split(",")
			rxFreqs = cfReq.imd_freqs.Split(",")

			If txlFreqs.GetUpperBound(0) <> txrFreqs.GetUpperBound(0) Then
				Throw New Exception("The freq number of txl is different from txr.")
			End If
			If txlFreqs.GetUpperBound(0) <> rxFreqs.GetUpperBound(0) Then
				Throw New Exception("The freq number of rx is different from tx.")
			End If

			Dim tmpf As DataModels.FrequencyPoint

			For fi = txlFreqs.GetLowerBound(0) To txlFreqs.GetUpperBound(0)
				tmpf = New DataModels.FrequencyPoint
				tmpf.TxlFreq = txlFreqs(fi)
				tmpf.TxrFreq = txrFreqs(fi)
				tmpf.RxFreq = rxFreqs(fi)
				trxFreq.Add(tmpf)
			Next

			For Each trxf In trxFreq
#If INSTR_NORMAL_TEST Then
				m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA, trxf.TxlFreq)
				m_devCtrl.SetFrequency(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB, trxf.TxrFreq)
				m_devCtrl.CorrectRFPower_TwoPort()
				MyDelay(50)
#End If


#If INSTR_NORMAL_TEST Then
				rxValue = ReadImdValue()
#Else
        rxValue = GetVirtualImValue()
#End If

				dspPoint.X = trxf.RxFreq
				dspPoint.Y = rxValue
				' pMTDisplay.CRDisplayImdValue = rxValue


				' add_data_point_list(imdPoints, 1, tmp_rx_freq, tmp_test_value, tmp_txl_freq, tmp_txr_freq, tmp_rx_freq, "U")
				AddPointToList(imdPoints, 1, trxf.TxlFreq, trxf.TxrFreq, trxf.RxFreq, dspPoint.X, dspPoint.Y, "C")

			Next


		Catch exa As AbortedException

			Throw New AbortedException("CustomFrequencyTest()::" & exa.Message)

		Catch ex As Exception
			'#If INSTR_NORMAL_TEST Then
			'      pInstrument.RFPowerOnOff_OnePort(False, False)
			'#End If
			Throw New Exception("CustomFrequencyTest()::" & ex.Message)

		Finally

			Try
#If INSTR_NORMAL_TEST Then
				m_devCtrl.RFPowerOnOff_OnePort(False, False)
#End If
			Catch ex As Exception
				MsgBox("The program disconnect with the imd instrument." & vbCrLf & "Please turn off the RF Power manually !", vbExclamation + MsgBoxStyle.OkOnly)
			End Try
		End Try
	End Sub
	Private Function DisplaySweepPointIn2ToneChart() As DataModels.FrequencyPoint
		Try

			If m_ffReq Is Nothing Then Return Nothing
			Dim cal As New Calculate

			'#Region "Display sweep freq point"
			'Display sweep freq test value of selected fix freq point in 2tone chart area"
			'在点频测试图形区域中显示已选择点的扫频测试值"
			Dim resp As New DataModels.FrequencyPoint

			resp.TxlFreq = m_ffReq.c1_freq
			resp.TxrFreq = m_ffReq.c2_freq
			resp.RxFreq = m_ffReq.imd_freq
			resp = cal.FindPoint(m_testTrace.Sweep, resp)
			If resp IsNot Nothing Then m_testControl.MarkPointInChart2Tone(resp)

			Return resp


		Catch ex As Exception
			Throw New Exception("FormTest.DisplaySweepPointIn2ToneChart()::" & ex.Message)
		End Try
	End Function
#End Region
	Private Function RunItemTestFunc(testItem As TestSpec.TestItem) As List(Of TestReport.XmlFramework.TItem)
		Try
			Dim resp As New List(Of TestReport.XmlFramework.TItem)

			Dim TestCount As Int16 = 3
			Dim TestRec As Int16 = 1
			Dim FExitDo As Boolean = False


			Do While (FExitDo = False)
				TestRec += 1
				Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
				Dim meas_status As String = ""

				Try

					Dim pr As New TestReport.ReportModule

					PreRun(testItem)
					m_testControl.InitChart(m_sfReq, m_ffReq, testItem.spec_detail.limit_low, testItem.spec_detail.limit_up)

					'sweep test
					SweepFrequencyTest(1, m_sfReq)

					'display sweep point
					Dim fpoint As DataModels.FrequencyPoint = DisplaySweepPointIn2ToneChart()

					'2 tone test
					If m_ffReq IsNot Nothing Then

						TwoToneFrequencyTest(1, 1, m_ffReq)

						m_testTrace.Lambda = GetLambdaTrace(m_testTrace.TwoToneFilter)

						m_testControl.MarkPointInChartLamda(2, m_testTrace.TwoToneFilter, fpoint)
						m_testControl.SetTraceToChartLambda(1, m_testTrace.Lambda)

					End If

					crList = pr.GetMeasCriteriaList(m_testTrace, testItem, m_RTPhase.M_CriteriaItems)

					pr.PrintTResultDcf(crList)

					pr.PrintTResultGui(m_testControl, crList)

					meas_status = "N"

				Catch exa As AbortedException

					meas_status = "A"
					MsgBox("FormTest.btnRun_Click()::" & exa.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

				Catch ex As Exception

					meas_status = "E"

					MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

				Finally
					Try
						Dim tpItem As TestReport.XmlFramework.TItem
						tpItem = m_testReport.WriteTestItem(testItem, m_testTrace, crList, meas_status)
						tpItem.MeasString = m_comment
						resp.Add(tpItem)

						m_testControl.SetTestStatus(tpItem.MeasStatus)
						PostRun()

						If tpItem.MeasStatus = "P" Then FExitDo = True : Exit Try
						If TestRec > TestCount Then FExitDo = True : Exit Try

						FormRetryReason.ShowDialog()
						If FormRetryReason.ReturnTestType = 1 Then FExitDo = True : Exit Try 'click exit
						m_comment = FormRetryReason.ReturnMsg
						'lblComment.Text = m_comment

					Catch ex As Exception
						meas_status = "E"
						MsgBox("FormTest.btnRun_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
					End Try
				End Try


			Loop

			Return resp
			'	If TestMode = DataModels.TestMode.Test Then btnExit.PerformClick()

		Catch ex As Exception
			Throw New Exception("RunAllTest()::" & ex.Message)
		End Try
	End Function
	Private Function WriteReport(phReport As TestReport.XmlFramework.TPhase) As Boolean
		Try
			Dim rltReport As New TestReport.XmlFramework.Report
			Dim strFilePath As String = "rtmp.xml"

			With m_RTPhase
				.meas_status = phReport.MeasStatus
			End With

			With rltReport
				.Type = 0
				.ConnString = pDbConnString
				.Head = m_testReport.WriteHead()
				.AssyParts = m_testReport.WriteAssyParts(m_RTPhase.M_RetList)
				.TestInstruments = m_testReport.WriteInstrument(m_RTPhase.M_EqList)
				.TestPhase = phReport
			End With

			If m_testReport.WriteReport(rltReport, pTestResultPath & strFilePath) = True Then
				Dim objEncryptor As New Encryptor
				Dim strDataFileName As String

				strDataFileName = pRTGlobal.barcode & "!" &
				  m_RTPhase.meas_start_time.ToString("yyyyMMddHHmmss") & "!" &
				  pRTGlobal.product_mode & "!" &
				  pRTGlobal.M_product_main.product_name & "!" &
				  m_RTPhase.phase & ".dat"

				objEncryptor.EncryptFile(pTestResultPath & strFilePath, pTestResultPath & strDataFileName)
				IO.File.Delete(pTestResultPath & strFilePath)

			End If

			Return True

		Catch ex As Exception
			MsgBox("WriteReport()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False
		End Try
	End Function
End Class
