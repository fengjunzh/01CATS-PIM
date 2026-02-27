Imports System.Xml.Serialization
Imports System.IO
Public Class TestReport
	Public Class XmlFramework
		Public Class Report
			Public Type As Short
			Public ConnString As String
			Public Head As New THead
			Public AssyParts As New List(Of TAssyPart)
			Public TestInstruments As New List(Of TInstrument)
			Public TestPhase As New TPhase
		End Class
		Public Class THead
			Public SerialNumber As String
			Public ProductMainId As Integer
			Public ProductModeId As Integer
			Public ProductMode As String
			Public SpecMainId As Integer
			Public PhaseMainId As Integer
			Public PhaseName As String
			Public SoftwareRev As String
			Public MeasStartTime As DateTime
			Public MeasStopTime As DateTime
			Public MeasStatus As String
			Public ConnectTime As Single
			Public MeasTime As Single
			Public SetupTime As Single
			Public TotalTime As Single
			Public UserName As String
			Public Factory As String
			Public Controller As String
		End Class
		<XmlType("Part")>
		Public Class TAssyPart
			Public Index As Short
			Public Model As String
			Public SerialNumber As String
			Public Hardware As String
			Public Firmware As String
			Public Mode As String
			Public TiltMin As String
			Public TiltMax As String
		End Class
		<XmlType("Instrument")>
		Public Class TInstrument
			Public Model As String
			Public SerialNumber As String
			Public Hardware As String
			Public Firmware As String
			Public Idn As String
		End Class
		Public Class TPhase
			<XmlIgnore()>
			Public PhaseMainId As Integer

			<XmlIgnore()>
			Public PhaseName As String

			<XmlIgnore()>
			Public MeasStatus As String

			<XmlElement("TestGroup")>
			Public TestGroups As New List(Of TGroup)
		End Class
		<Serializable()>
		Public Class TGroup
			Public GroupMainId As Integer
			Public GroupName As String
			Public GroupStatus As String

			<XmlElement("TestItem")>
			Public TestItems As New List(Of TItem)
		End Class

		<XmlType("Item")>
		Public Class TCriteriaItem
			Public CriteriaDetailId As Integer
			Public Descr As String
			Public LL As Single
			Public UL As Single
			Public Unit As String
			Public Value As Single
			Public Status As String
		End Class
		<XmlType("Trace")>
		Public Class TestTrace
			Public Index As Short
			Public TraceName As String
			Public TraceX1 As String
			Public TraceX2 As String
			Public TraceX3 As String
			Public TraceX4 As String
			Public TraceY1 As String
			Public TraceY2 As String
			Public TraceY3 As String
			Public TraceY4 As String
		End Class
		Public Class TExtend
			<XmlElement>
			Public MeasValue As New List(Of String)
		End Class
		Public Class TItem
			Public SpecDetailId As Integer
			Public OrderIdx As Short
			Public TiltIdxs As String
			Public TiltAngs As String
			Public MeasValue As Single
			Public MeasString As String
			Public MeasStatus As String
			Public PlotPath As String
			Public TracePath As String


			Public TestExtend As New TExtend

			'<XmlElement("Traces")>
			Public Traces As New List(Of TestTrace)

			'<XmlElement("CriteriaItems")>
			Public CriteriaItems As List(Of TCriteriaItem)

		End Class
		'<Serializable()>
		'Public Class TFramwork
		'  Public Type As String
		'  Public Head As New THead
		'  Public AssyParts As New Dictionary(Of Short, TAssyPart)
		'  Public TestInstruments As New List(Of TInstrument)
		'  Public TestPhase As New TPhase
		'End Class

		'Public Function SaveTestResult(filePath As String, testMemory As TFramwork) As Boolean
		'  Try
		'    Dim sw As New StreamWriter(filePath)

		'    Dim serializer As XmlSerializer = New XmlSerializer(GetType(TFramwork))

		'    serializer.Serialize(sw, testMemory)

		'    sw.Close()


		'  Catch ex As Exception

		'  End Try

		'End Function
	End Class

#Region "ReportModule"
	Public Class ReportModule
		Private _M_AlgoParas As DataModels.AlgorithmLimit
		Public Sub New()

			_M_AlgoParas = New DataModels.AlgorithmLimit

		End Sub
		Public Function GetMeasCriteriaList(trace As DataModels.TestTrace,
										   item As CATS.Model.cq_spec_imd_details,
										   tcrList As Dictionary(Of String, CATS.Model.cq_criteria_detail)
										   ) As Dictionary(Of String, CATS.Model.meas_criteria)
			Try

				Dim resp As New Dictionary(Of String, CATS.Model.meas_criteria)
				Dim tp_model As CATS.Model.meas_criteria
				Dim tp_cq_model As CATS.Model.cq_criteria_detail
				Dim tp_crItem As String
				Dim tp_prItem As String
				Dim tp_value As Single
				Dim tp_fmode As Short
				Dim tp_stapim As Boolean = True
				Dim tp_stastb As Boolean = True
				Dim cal As New Calculate

				'#Region "TIMESWEEP_MAX"
				If trace.TwoToneFilter.Count > 0 Then

					tp_crItem = "TIMESWEEP_MAX"
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.Max(trace.TwoToneFilter)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
						tp_stapim = tp_stapim And IIf(.meas_status = "P", True, False)
					End With

					resp.Add(tp_prItem, tp_model)
				End If

				'#End Region

				If trace.TwoToneFilter.Count > 0 Then
					'#Region "TIMESWEEP_AVG"
					tp_crItem = "TIMESWEEP_AVG"
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.Average(trace.TwoToneFilter)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
					End With

					resp.Add(tp_prItem, tp_model)
				End If
				'#End Region

				'#Region "DOWNSWEEP_MAX"
				tp_crItem = "DOWNSWEEP_MAX"
				If trace.SweepDown.Count > 0 Then
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.Max(trace.SweepDown)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
						tp_stapim = tp_stapim And IIf(.meas_status = "P", True, False)
					End With

					resp.Add(tp_prItem, tp_model)
				End If

				'#End Region

				'#Region "UPSWEEP_MAX"
				tp_crItem = "UPSWEEP_MAX"
				If trace.SweepUp.Count > 0 Then
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.Max(trace.SweepUp)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
						tp_stapim = tp_stapim And IIf(.meas_status = "P", True, False)
					End With

					resp.Add(tp_prItem, tp_model)
				End If

				'#End Region
				If trace.TwoToneFilter.Count > 0 Then
					'#Region "LAMBDA_PERCENT"
					tp_crItem = "LAMBDA_PERCENT"
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.LambdaPercent(trace.Lambda, _M_AlgoParas.Lambda_PFLimit)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
						tp_stastb = tp_stapim And IIf(.meas_status = "P", True, False)
					End With

					resp.Add(tp_prItem, tp_model)
				End If
				'#End Region

				If trace.TwoToneFilter.Count > 0 Then
					'#Region "CCTRTN_STDEV"
					tp_crItem = "CTRN_STDEV"
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.CtrnStdev(trace.TwoToneFilter, _M_AlgoParas.Ctrn_AvgMaxLimit, _M_AlgoParas.Ctrn_CalcCoefficient)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
					End With

					resp.Add(tp_prItem, tp_model)
				End If
				'#End Region

				'#Region "GAP_UP_DOWN"
				tp_crItem = "GAP_UPDOWN"
				If trace.SweepDown.Count > 0 Then
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = cal.SweepGap(trace.SweepUp, trace.SweepDown)
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
					End With

					resp.Add(tp_prItem, tp_model)
				End If

				'#End Region

				'#Region "FAILURE_MODE"

				If trace.TwoToneFilter.Count > 0 Then
					tp_fmode = 0
					If tp_stapim = False Then tp_fmode += 1
					If tp_stastb = False Then tp_fmode += 2

					tp_crItem = "FAILURE_MODE"
					tp_cq_model = tcrList(tp_crItem)
					tp_prItem = item.spec_detail.meas_item & "_" & tp_crItem
					tp_value = tp_fmode
					tp_model = New CATS.Model.meas_criteria

					With tp_model
						.criteria_detail_id = tp_cq_model.criteria_detail_id
						.meas_item = tp_prItem
						.meas_value = tp_value
						.meas_unit = tp_cq_model.limit_unit
						If tcrList(tp_crItem).limit_inherit = True Then
							.meas_ll = item.spec_detail.limit_low
							.meas_ul = item.spec_detail.limit_up
						Else
							.meas_ll = tp_cq_model.limit_low
							.meas_ul = tp_cq_model.limit_up
						End If
						.meas_status = IIf((.meas_value >= .meas_ll And .meas_value <= .meas_ul), "P", "F")
					End With

					resp.Add(tp_prItem, tp_model)
				End If

				'#End Region

				Return resp


			Catch ex As Exception
				Throw New Exception("TestClass.PrintResult.GetMeasCriteriaList()::" & ex.Message)
			End Try

		End Function

		Public Sub PrintTResultDcf(meas_criteriaList As Dictionary(Of String, CATS.Model.meas_criteria))
			Try

				For Each p As KeyValuePair(Of String, CATS.Model.meas_criteria) In meas_criteriaList
					pGui.RecordResult(p.Value.meas_item, p.Value.meas_value, p.Value.meas_ll, p.Value.meas_ul)
				Next

			Catch ex As Exception
				Throw New Exception("TestClass.PrintResult.PrintTestResultDcf()::" & ex.Message)
			End Try

		End Sub

		Public Sub PrintTResultGui(control As CATSRunBox, meas_criteriaList As Dictionary(Of String, CATS.Model.meas_criteria))
			Try

				For Each p As KeyValuePair(Of String, CATS.Model.meas_criteria) In meas_criteriaList

					If p.Key.ToString.Contains("TIMESWEEP_MAX") Then
						'Form.txt_2tone_max.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_2tone_max.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_2ToneMax(p.Value.meas_value, p.Value.meas_status)

					ElseIf p.Key.ToString.Contains("TIMESWEEP_AVG") Then
						'Form.txt_2tone_avg.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_2tone_avg.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_2ToneAvg(p.Value.meas_value, p.Value.meas_status)

					ElseIf p.Key.ToString.Contains("DOWNSWEEP_MAX") Then

						'Form.txt_swp_dw_max.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_swp_dw_max.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_SweepDownMax(p.Value.meas_value, p.Value.meas_status)

					ElseIf p.Key.ToString.Contains("UPSWEEP_MAX") Then

						'Form.txt_swp_up_max.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_swp_up_max.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_SweepUpMax(p.Value.meas_value, p.Value.meas_status)
					ElseIf p.Key.ToString.Contains("LAMBDA_PERCENT") Then

						'Form.txt_2tone_lambda_percent.Text = Math.Round(p.Value.meas_value, 3) * 100
						'Form.txt_2tone_lambda_percent.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_LambdaPercent(p.Value.meas_value, p.Value.meas_status)
					ElseIf p.Key.ToString.Contains("CTRN_STDEV") Then

						'Form.txt_2tone_std.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_2tone_std.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_CtrnStd(p.Value.meas_value, p.Value.meas_status)
					ElseIf p.Key.ToString.Contains("GAP_UPDOWN") Then

						'Form.txt_gap_updown.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_gap_updown.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_GapUpDown(p.Value.meas_value, p.Value.meas_status)
					ElseIf p.Key.ToString.Contains("FAILURE_MODE") Then

						'Form.txt_failure_mode.Text = Math.Round(p.Value.meas_value, 1)
						'Form.txt_failure_mode.ForeColor = IIf(p.Value.meas_status = "P", Color.Black, Color.Red)
						control.SetValue_FailureMode(p.Value.meas_value, p.Value.meas_status)

						'Select Case p.Value.meas_value
						'	Case 0
						'		Form.lbl_fail_desc.Text = "Pim Pass" & vbCrLf & "Stability Pass"
						'	Case 1
						'		Form.lbl_fail_desc.Text = "Pim Fail" & vbCrLf & "Stability Pass"
						'	Case 2
						'		Form.lbl_fail_desc.Text = "Pim Pass" & vbCrLf & "Stability Fail"
						'	Case 3
						'		Form.lbl_fail_desc.Text = "Pim Fail" & vbCrLf & "Stability Fail"
						'End Select


					End If


				Next
				My.Application.DoEvents()
			Catch ex As Exception
				Throw New Exception("TestClass.PrintResult.PrintTResultGui()::" & ex.Message)
			End Try

		End Sub
	End Class

#Region "TxtReport"
	Public Class TxtReport
		Private m_filePath As String
		Public Sub New(filePath As String)
			m_filePath = filePath
		End Sub
		Public Sub WriteHead(barcode As String, phase As String, portNum As Short, downtilt As String, carrierPower As Single,
								retList As Dictionary(Of String, DataModels.RetDevice), comment As String)

			Dim sw As IO.StreamWriter = Nothing

			Try
				Dim tmpDefault As String
				Dim tmpLine As String

				Dim tmp_id As Short = 1



				sw = New IO.StreamWriter(m_filePath, True)

				sw.WriteLine("model;mode;testband;barcode;portnum;downtilt")
				sw.WriteLine(pRTGlobal.M_product_main.product_name & ";" &
							  pRTGlobal.product_mode & ";" &
							  phase & ";" &
							  pRTGlobal.barcode & ";" &
							  portNum & ";" &
							  downtilt)
				sw.WriteLine()

				tmpDefault = barcode & ";" & carrierPower & "dBm;"

				sw.WriteLine("id;ant_barcode;carrier_power;tilt_sn;down_tilt")

				If retList Is Nothing Then
					tmpLine = "id0" & ";" & tmpDefault & "" & ";" & ""
					sw.WriteLine(tmpLine)
				Else
					For Each tmp_ret As KeyValuePair(Of String, DataModels.RetDevice) In retList
						tmpLine = "id" & tmp_id & ";" & tmpDefault & tmp_ret.Value.RetSn & ";" & tmp_ret.Value.Tilt.GetCurrentTilt()
						sw.WriteLine(tmpLine)
						tmp_id += 1
					Next
				End If

				sw.WriteLine(vbCrLf)
				sw.WriteLine("Test Comment:" & comment)
				sw.Close()

			Catch ex As Exception

				sw.Close()
				Throw New Exception("TestModules.TxtReport.WriteHead()::" & ex.Message)
			End Try

		End Sub
		Public Sub WriteSweepFreqHead()
			Try
				Dim sw As New IO.StreamWriter(m_filePath, True)

				sw.WriteLine("sweep freq test")
				sw.WriteLine("Time:" & CType(pGui.StartTestTime, DateTime).ToString("yyyy-MM-dd hh:mm:ss"))
				sw.WriteLine("sweep cycle;down_tilt;tx1 freq;tx2 freq;rx freq;rx freq;imd value")
				sw.Close()

			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteSweepFreqHead()::" & ex.Message)
			End Try

		End Sub
		Public Sub WriteTwoToneHead(cycle As Byte)
			Try

				Dim sw As New IO.StreamWriter(m_filePath, True)

				sw.WriteLine("fix freq test -- cycle " & cycle)
				sw.WriteLine("Time:" & CType(pGui.StartTestTime, DateTime).ToString("yyyy-MM-dd hh:mm:ss"))
				sw.WriteLine("fix freq cycle;down_tilt;tx1 freq;tx2 freq;rx freq;time;imd value")
				sw.Close()

			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteTwoToneHead()::" & ex.Message)
			End Try

		End Sub
		Public Sub WriteLamdaHead(cycle As Byte)
			Try
				Dim sw As New IO.StreamWriter(m_filePath, True)

				sw.WriteLine("lambda test  (< limit value )-- cycle " & cycle)
				sw.WriteLine("Time:" & Now.ToString("yyyy-MM-dd hh:mm:ss"))
				'sw.WriteLine()
				sw.WriteLine("fix freq cycle;down_tilt;tx1 freq;tx2 freq;rx freq;time;lambda value")
				sw.Close()

			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteLamdaHead()::" & ex.Message)
			End Try

		End Sub
		Public Sub WriteTestPoint(style As String, point As DataModels.FrequencyPoint)
			Try

				Dim sw As New IO.StreamWriter(m_filePath, True)

				If Not style.Contains("F") Then point.XData = CInt(point.XData)
				sw.WriteLine(style & ";;" & CInt(point.TxlFreq) & ";" & CInt(point.TxrFreq) & ";" &
							 CInt(point.RxFreq) & ";" & Math.Round(point.XData, 3) & ";" & Math.Round(point.YData, 2))
				sw.Close()

			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteTestPoint()::" & ex.Message)
			End Try

		End Sub
		Public Sub WriteTestResult(testItem As String, ll As Single, ul As Single, testValue As Single)
			Try
				Dim sw As New IO.StreamWriter(m_filePath, True)
				sw.WriteLine("Test result;" & testItem & ";" & ll & ";" & ul & ";" & testValue)
				sw.Close()
			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteTestResult()::" & ex.Message)
			End Try
		End Sub
		Public Sub WriteFileTerminal()
			Try
				Dim sw As New IO.StreamWriter(m_filePath, True)

				sw.WriteLine()
				' sw.WriteLine("Time:" & Now.ToString("yyyy-MM-dd hh:mm:ss"))
				sw.WriteLine("==================================================")
				sw.Close()

			Catch ex As Exception
				Throw New Exception("TestModules.TxtReport.WriteFileTerminal()::" & ex.Message)
			End Try
		End Sub
	End Class

#End Region

#Region "PicReport"
	Public Class PicReport

		Declare Function BitBlt Lib "gdi32" Alias "BitBlt" (ByVal hDestDC As IntPtr， ByVal x As Integer， ByVal y As Integer， ByVal nWidth As Integer， ByVal nHeight As Integer， ByVal hSrcDC As IntPtr， ByVal xSrc As Integer， ByVal ySrc As Integer， ByVal dwRop As Integer) As Boolean
		Private m_form As Windows.Forms.Form
		Private m_picPath As String
		Public Sub New(form As Windows.Forms.Form, picPath As String)
			m_form = form
			m_picPath = picPath
		End Sub
		Public Sub SaveImage()
			m_form.WindowState = FormWindowState.Maximized
			m_form.TopMost = True
			My.Application.DoEvents()
			'Threading.Thread.Sleep(500)
			'My.Application.DoEvents()
			'Dim image As Bitmap = New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
			Dim image As Bitmap = New Bitmap(m_form.Width, m_form.Height)
			Dim imgGraphics As Graphics = Graphics.FromImage(image)

			imgGraphics.CopyFromScreen(0, 0, 0, 0, m_form.Size)
			'  imgGraphics.CopyFromScreen(0, 0, 0, 0, New Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
			'imgGraphics.CopyFromScreen(0, 0, 0, 0, New Size(Screen.AllScreens(0).Bounds.Width, Screen.AllScreens(0).Bounds.Height))
			image.Save(m_picPath, Drawing.Imaging.ImageFormat.Png)
			m_form.TopMost = False
		End Sub
		Public Sub SaveFormImage()
			m_form.WindowState = FormWindowState.Maximized

			m_form.TopMost = True
			'Me.Focus()
			My.Application.DoEvents()

			'Dim rect As Rectangle = New Rectangle

			'rect = Screen.GetWorkingArea(Me)


			'创建一个以当前窗体为模板的图象
			Dim g1 As Graphics = m_form.CreateGraphics

			'创建以窗体大小为标准的位图 
			Dim MyBMP As Bitmap = New Bitmap(m_form.Width, m_form.Height, g1) '定义位图的大小


			'创建一个位图Bitmap绘图图面
			Dim g2 As Graphics = Graphics.FromImage(MyBMP)

			'得到窗体的DC（句柄）
			Dim dc1 As IntPtr = g1.GetHdc()

			'得到Bitmap的DC 
			Dim dc2 As IntPtr = g2.GetHdc()
			'复制图块的光栅操作码
			Dim SRCCOPY As Integer = 13369376
			'调用此API函数，实现窗体捕获
			'BitBlt(dc2, intX, intY, intWidth, intHeight, dc1, intLeft, intRight, SRCCOPY);//13369376

			'BitBlt(hDcSave, 0, 0, Me.Width, Me.Height, hDcSrc, 0, 0, SRCCOPY) ';//13369376
			BitBlt(dc2, 0, 0, m_form.Width, m_form.Height, dc1, 0, 0, SRCCOPY) ';//13369376
			'释放掉屏幕的DC
			g1.ReleaseHdc(dc1)
			'释放掉Bitmap的DC 
			g2.ReleaseHdc(dc2)
			'以JPG文件格式来保存
			MyBMP.Save(m_picPath, Drawing.Imaging.ImageFormat.Png) 'Application.StartupPath + "\\
			m_form.TopMost = False
		End Sub
	End Class

#End Region

#Region "XmlReport"
	Public Class XmlReport
		Private m_TracePath As String
		Private m_PlotPath As String
		Private m_RTPhase As New DataModels.RTimePhasePara
		Public Sub New(phaseRunTime As DataModels.RTimePhasePara)
			m_RTPhase = phaseRunTime
		End Sub
		Public Property TracePath As String
			Get
				Return m_TracePath
			End Get
			Set(value As String)
				m_TracePath = value
			End Set
		End Property
		Public Property PlotPath As String
			Get
				Return m_PlotPath
			End Get
			Set(value As String)
				m_PlotPath = value
			End Set
		End Property
		Public Function WriteHead() As TestReport.XmlFramework.THead
			Try
				Dim resp As New TestReport.XmlFramework.THead

				With resp

					.Controller = My.Computer.Name
					.UserName = Environment.UserName.ToString.ToUpper
					.Factory = pFactory  'pGui.Factory '"ASZ"
					.PhaseMainId = m_RTPhase.phase_main_id
					.PhaseName = m_RTPhase.phase
					.ProductMainId = pRTGlobal.M_product_main.id
					.ProductModeId = pRTGlobal.product_mode_id
					.ProductMode = pRTGlobal.product_mode
					.SerialNumber = pRTGlobal.barcode
					.SoftwareRev = Application.ProductVersion
					.SpecMainId = m_RTPhase.spec_main_id
					.MeasStatus = m_RTPhase.meas_status
					.MeasStartTime = m_RTPhase.meas_start_time.ToString("yyyy-MM-dd HH:mm:ss")
					.MeasStopTime = m_RTPhase.meas_stop_time.ToString("yyyy-MM-dd HH:mm:ss")
					.SetupTime = m_RTPhase.setup_time
					.MeasTime = m_RTPhase.meas_time
					.TotalTime = m_RTPhase.total_time
					.ConnectTime = m_RTPhase.conn_time

				End With

				'm_report.Type = 0
				'm_report.Head = resp

				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.WriteHead()::" & ex.Message)
			End Try
		End Function
		Public Function WriteAssyParts(assyList As Dictionary(Of String, DataModels.RetDevice)) As List(Of TestReport.XmlFramework.TAssyPart)
			Try
				Dim resp As New List(Of TestReport.XmlFramework.TAssyPart)
				Dim model As TestReport.XmlFramework.TAssyPart

				If assyList Is Nothing Then Return Nothing

				For Each k As KeyValuePair(Of String, DataModels.RetDevice) In assyList

					model = New TestReport.XmlFramework.TAssyPart

					model.Index = Right(k.Key, 1)
					model.Model = k.Value.AntModel
					model.Firmware = k.Value.FwVersion
					model.Hardware = k.Value.HwVersion
					model.SerialNumber = k.Value.RetSn
					model.Mode = IIf(k.Value.Type = AisgDevice.DeviceType.SingleRet, "S", "M")
					model.TiltMin = k.Value.Tilt.MinTilt
					model.TiltMax = k.Value.Tilt.MaxTilt

					resp.Add(model)

				Next

				'm_report.AssyParts = resp

				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.WriteAssyParts()::" & ex.Message)
			End Try

		End Function
		Public Function WriteInstrument(equipmentList As List(Of DataModels.Instrument)) As List(Of TestReport.XmlFramework.TInstrument)
			Try
				Dim resp As New List(Of TestReport.XmlFramework.TInstrument)
				Dim ti As TestReport.XmlFramework.TInstrument

				If equipmentList Is Nothing Then Return Nothing

				For Each eq In equipmentList
					ti = New TestReport.XmlFramework.TInstrument
					ti.Model = eq.Model
					ti.SerialNumber = eq.SerialNumber
					ti.Firmware = eq.Firmware
					ti.Hardware = eq.Hardware
					ti.Idn = eq.Idn
					resp.Add(ti)
				Next

				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.WriteInstruments()::" & ex.Message)
			End Try
		End Function
		Private Function AddTestTrace(index As Short,
									 name As String,
									 trace As List(Of DataModels.FrequencyPoint)) As TestReport.XmlFramework.TestTrace
			Try
				Dim resp As New TestReport.XmlFramework.TestTrace

				If trace Is Nothing Then Return Nothing

				resp.Index = index
				resp.TraceName = name
				resp.TraceX1 = String.Join(",", trace.Select(Function(o) o.XData))
				resp.TraceY1 = String.Join(",", trace.Select(Function(o) o.YData))

				If name.ToString.ToUpper = "2TONE" Then
					resp.TraceX2 = CInt(trace(0).TxlFreq)
					resp.TraceX3 = CInt(trace(0).TxrFreq)
					resp.TraceX4 = CInt(trace(0).RxFreq)
				Else
					resp.TraceX2 = String.Join(",", trace.Select(Function(o) CInt(o.TxlFreq)))
					resp.TraceX3 = String.Join(",", trace.Select(Function(o) CInt(o.TxrFreq)))
					'resp.TraceX4 = String.Join(",", trace.Select(Function(o) CInt(o.RxFreq)))
				End If


				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.AddTestItem()::" & ex.Message)
			End Try
		End Function
		Private Function AddTestTrace(index As Short,
									 name As String,
									 trace As List(Of PointF)) As TestReport.XmlFramework.TestTrace
			Try
				Dim resp As New TestReport.XmlFramework.TestTrace

				If trace Is Nothing Then Return Nothing

				resp.Index = index
				resp.TraceName = name
				resp.TraceX1 = String.Join(",", trace.Select(Function(o) o.X))
				resp.TraceY1 = String.Join(",", trace.Select(Function(o) o.Y))

				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.AddTestTrace()::" & ex.Message)
			End Try
		End Function
		Private Function AddTestExtend(trace As DataModels.TestTrace) As TestReport.XmlFramework.TExtend
			Try

				Dim resp As New TestReport.XmlFramework.TExtend
				Dim tp_p, tp_pt As DataModels.FrequencyPoint
				Dim cal As New Calculate
				'Dim tp_ext As TestReport.XmlFramework.TExtend

				If trace.Sweep.Count = 0 Then Return Nothing
				tp_p = cal.FindMaxPoint(trace.Sweep)

				If trace.TwoToneFilter.Count > 0 Then
					tp_pt = cal.FindMaxPoint(trace.TwoToneFilter)
					tp_p = IIf(tp_p.YData > tp_pt.YData, tp_p, tp_pt)
				End If

				resp.MeasValue.Add(tp_p.YData)
				'tp_ext = New TestReport.XmlFramework.TExtend
				'tp_ext.MeasValue = tp_p.YData
				'resp.Add(tp_ext)


				resp.MeasValue.Add(tp_p.RxFreq)
				'tp_ext = New TestReport.XmlFramework.TExtend
				'tp_ext.MeasValue = tp_p.RxFreq
				'resp.Add(tp_ext)

				resp.MeasValue.Add(tp_p.TxlFreq)
				'tp_ext = New TestReport.XmlFramework.TExtend
				'tp_ext.MeasValue = tp_p.TxlFreq
				'resp.Add(tp_ext)

				resp.MeasValue.Add(tp_p.TxrFreq)
				'tp_ext = New TestReport.XmlFramework.TExtend
				'tp_ext.MeasValue = tp_p.TxrFreq
				'resp.Add(tp_ext)

				Return resp

			Catch ex As Exception
				Throw New Exception("XmlReport.AddTestExtend()::" & ex.Message)
			End Try
		End Function
		Private Function AddTestCriteria(criteriaList As Dictionary(Of String, CATS.Model.meas_criteria), ByRef measStatus As String) As List(Of TestReport.XmlFramework.TCriteriaItem)
			Try
				Dim resp As New List(Of TestReport.XmlFramework.TCriteriaItem)
				Dim meas_criteria As TestReport.XmlFramework.TCriteriaItem
				Dim tp_status As Boolean = True

				'resp = Nothing

				If measStatus <> "N" Then measStatus = measStatus : Return Nothing
				If criteriaList Is Nothing Then measStatus = "E" : Return Nothing

				For Each c In criteriaList
					meas_criteria = New TestReport.XmlFramework.TCriteriaItem
					meas_criteria.CriteriaDetailId = c.Value.criteria_detail_id
					meas_criteria.Descr = c.Value.meas_item
					meas_criteria.LL = c.Value.meas_ll
					meas_criteria.UL = c.Value.meas_ul
					meas_criteria.Unit = c.Value.meas_unit
					meas_criteria.Status = c.Value.meas_status
					meas_criteria.Value = c.Value.meas_value
					tp_status = tp_status And IIf(c.Value.meas_status = "P", True, False)
					resp.Add(meas_criteria)
				Next

				measStatus = IIf(tp_status = True, "P", "F")

				Return resp

			Catch ex As Exception

				measStatus = "E"
				Return Nothing
				'Throw New Exception("AddTestCriteria()::" & ex.Message)
			End Try
		End Function
		Public Function WriteTestItem(spec As CATS.Model.cq_spec_imd_details,
									  trace As DataModels.TestTrace,
									  criterialList As Dictionary(Of String, CATS.Model.meas_criteria),
									  testStatus As String
									   ) As TestReport.XmlFramework.TItem
			Try


				Dim resp As New TestReport.XmlFramework.TItem
				Dim tpTrcs As New List(Of TestReport.XmlFramework.TestTrace)
				Dim tpExtends As TestReport.XmlFramework.TExtend
				Dim tpCrs As New List(Of TestReport.XmlFramework.TCriteriaItem)
				Dim tpStatus As String = testStatus

				'trace
				tpTrcs.Add(AddTestTrace(1, "SweepUp", trace.SweepUp))
				tpTrcs.Add(AddTestTrace(2, "SweepDown", trace.SweepDown))

				If trace.TwoToneFilter.Count > 0 Then
					tpTrcs.Add(AddTestTrace(3, "2Tone", trace.TwoToneFilter))
					tpTrcs.Add(AddTestTrace(4, "Lambda", trace.Lambda))
				End If

				'extend
				tpExtends = AddTestExtend(trace)

				'criteria
				tpCrs = AddTestCriteria(criterialList, tpStatus)

				With resp

					.SpecDetailId = spec.spec_detail.id
					.MeasString = "" ' spec.spec_detail.meas_item
					.OrderIdx = spec.spec_detail.order_idx
					.TiltIdxs = spec.spec_detail.dwtilt_idxs
					.TiltAngs = spec.spec_detail.dwtilt_angs
					.MeasValue = tpExtends.MeasValue(0)

					.MeasStatus = tpStatus 'IIf(testStatus = DataModels.TestStatus.Normal, tpStatus, testStatus)
					.TracePath = m_TracePath
					.PlotPath = m_PlotPath

					.Traces = tpTrcs '// trace
					.TestExtend = tpExtends  '// extend
					.CriteriaItems = tpCrs

				End With

				'm_report.TestPhase.TestGroups(spec.spec_detail.group_main_id).TestItems.Add(resp)

				Return resp

			Catch ex As Exception
				Return Nothing
			End Try

		End Function


		Public Function WriteReport(reportXml As TestReport.XmlFramework.Report, filePath As String) As Boolean
			Try

				Dim sz As New XmlSerializer(GetType(TestReport.XmlFramework.Report))
				Dim wr As New System.IO.StreamWriter(filePath)
				sz.Serialize(wr, reportXml)
				wr.Close()
				Return True
			Catch ex As Exception
				Throw New Exception("XmlReport.WriteReport()::" & ex.Message)
			End Try
		End Function
	End Class
#End Region




#End Region
End Class
