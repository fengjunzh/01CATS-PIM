Imports System.Windows.Forms.DataVisualization.Charting
Public Class CATSRunBox
	'Sweep Up Max dBc
	'Sweep Down Max dBc
	'2-Tone Max dBc
	'2-Tone Average dBc
	'ƛ_Percent: %
	'Ctrn_Std: 
	'UpDown dB
	'FailureMode
	Public Event RunTestHandler()
	Public Event AbortTestHandler()
	Public Event RetryTestHandler()
	Private Sub InitGvTestresultRows()
		Try
			With gvTestresult
				.Rows.Add("Sweep Up Max", "", "dBc")
				.Rows.Add("Sweep Down Max", "", "dBc")
				.Rows.Add("2-Tone Max", "", "dBc")
				.Rows.Add("2-Tone Avg", "", "dBc")
				.Rows.Add("ƛ_Percent", "", "%")
				.Rows.Add("Ctrn_Std")
				.Rows.Add("UpDown", "", "dB")
				.Rows.Add("FailureMode")
			End With

		Catch ex As Exception

		End Try
	End Sub
#Region "Set Gui value"
	Private Sub SetCellValue(row As Integer, cell As Integer, value As Single, status As String)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Integer, Integer, Single, String)(AddressOf SetCellValue), row, cell, value, status)
			Else
				gvTestresult.Rows(row).Cells(cell).Value = Math.Round(value, 1)
				gvTestresult.Rows(row).Cells(cell).Style.ForeColor = IIf(status.Trim.ToUpper = "P", Color.Black, Color.Red)
			End If

		Catch ex As Exception
			Throw New Exception("SetCellValue()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_SweepUpMax(value As Single, status As String)
		Try
			SetCellValue(0, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_SweepUpMax()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_SweepDownMax(value As Single, status As String)
		Try
			SetCellValue(1, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_SweepDownMax()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_2ToneMax(value As Single, status As String)
		Try
			SetCellValue(2, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_2ToneMax()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_2ToneAvg(value As Single, status As String)
		Try
			SetCellValue(3, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_2ToneAvg()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_LambdaPercent(value As Single, status As String)
		Try
			SetCellValue(4, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_LambdaPercent()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_CtrnStd(value As Single, status As String)
		Try
			SetCellValue(5, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_CtrnStd()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_GapUpDown(value As Single, status As String)
		Try
			SetCellValue(6, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_GapUpDown()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetValue_FailureMode(value As Single, status As String)
		Try
			SetCellValue(7, 1, value, status)
		Catch ex As Exception
			Throw New Exception("CATSRunBox.SetValue_FailureMode()::" & ex.Message)
		End Try
	End Sub
#End Region

	Private Sub CATSRunBox_Load(sender As Object, e As System.EventArgs) Handles Me.Load

		InitGvTestresultRows()
		'SetTablePanelRowSpan()

	End Sub
	Private Sub SetTablePanelRowSpan()
		Try
			Dim c As Control = Me.TableLayoutPanel1.GetControlFromPosition(0, 1)

			TableLayoutPanel1.SetRowSpan(c, 2)

		Catch ex As Exception

		End Try
	End Sub
	Private Sub btnRunTest_Click(sender As Object, e As EventArgs) Handles btnRunTest.Click
		RaiseEvent RunTestHandler()
	End Sub
	Private Sub InitChartSweep(sfReq As CATS.Model.cfg_imd_sfbox, ll As Single, ul As Single)
		Try
			If InvokeRequired Then
				Dim iasyr As IAsyncResult = Me.BeginInvoke(New Action(Of CATS.Model.cfg_imd_sfbox, Single, Single)(AddressOf InitChartSweep), sfReq, ll, ul)
				Me.EndInvoke(iasyr)
			Else
				Dim tp_axis As DataModels.AxisMaxMin
				Dim cal As New Calculate

				With ChartSweepFreq

					.Series(0).Points.Clear() 'limit
					.Series(1).Points.Clear() 'up
					.Series(2).Points.Clear()  'down

					.ChartAreas(0).AxisY.MajorGrid.Interval = 10
					.ChartAreas(0).AxisY.Minimum = -190
					.ChartAreas(0).AxisY.Maximum = -120

					tp_axis = cal.GetImRange(sfReq)

					.ChartAreas(0).AxisX.Minimum = Math.Round(tp_axis.Min, 1)
					.ChartAreas(0).AxisX.Maximum = Math.Round(tp_axis.Max, 1)

					ChartSweepFreq.Series(0).Points.AddXY(ChartSweepFreq.ChartAreas(0).AxisX.Minimum, ul)
					ChartSweepFreq.Series(0).Points.AddXY(ChartSweepFreq.ChartAreas(0).AxisX.Maximum, ul)
					My.Application.DoEvents()

				End With

			End If
		Catch ex As Exception
			Throw New Exception("CATSRunBox.InitChartSweep()::" & ex.Message)
		End Try
	End Sub
	Private Sub InitChart2Tone(ffReq As CATS.Model.cfg_imd_ffbox, ll As Single, ul As Single)
		Try
			If InvokeRequired Then
				Dim iasyr As IAsyncResult = Me.BeginInvoke(New Action(Of CATS.Model.cfg_imd_ffbox, Single, Single)(AddressOf InitChart2Tone), ffReq, ll, ul)
				Me.EndInvoke(iasyr)
			Else
				If ffReq Is Nothing Then Return 'Only sweep test

				With ChartSweepTime

					.Series(0).Points.Clear() 'limit
					.Series(1).Points.Clear() '2 tone 
					.Series(2).Points.Clear()  ' selected point
					.Series(3).Points.Clear() 'min
					.Series(4).Points.Clear() 'max


					'.ChartAreas(0).AxisY.Interval = 10
					.ChartAreas(0).AxisY.MajorGrid.Interval = 10
					.ChartAreas(0).AxisY.Minimum = -190
					.ChartAreas(0).AxisY.Maximum = -120

					.ChartAreas(0).AxisX.Minimum = 0
					.ChartAreas(0).AxisX.Maximum = ffReq.duration_sec
					'.ChartAreas(0).AxisX2.Enabled = AxisEnabled.True

					My.Application.DoEvents()
					.Series(0).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Minimum, ul)
					.Series(0).Points.AddXY(ChartSweepTime.ChartAreas(0).AxisX.Maximum, ul)
					My.Application.DoEvents()

				End With

			End If
		Catch ex As Exception
			Throw New Exception("CATSRunBox.InitChart2Tone()::" & ex.Message)
		End Try
	End Sub
	Private Sub InitChartLambda(ffReq As CATS.Model.cfg_imd_ffbox, ll As Single, ul As Single)
		Try
			If InvokeRequired Then
				Dim iasyr As IAsyncResult = Me.BeginInvoke(New Action(Of CATS.Model.cfg_imd_ffbox, Single, Single)(AddressOf InitChartLambda), ffReq, ll, ul)
				Me.EndInvoke(iasyr)
			Else
				If ffReq Is Nothing Then Return 'Only sweep test

				With ChartLamda

					.Series(0).Points.Clear() 'limit
					.Series(1).Points.Clear() ' 2 tone
					.Series(2).Points.Clear()  'selected point
					.ChartAreas(0).AxisY.MajorGrid.Interval = 50
					.ChartAreas(0).AxisY.Minimum = 0
					.ChartAreas(0).AxisY.Maximum = 400

					.ChartAreas(0).AxisX.Minimum = 0
					.ChartAreas(0).AxisX.Maximum = ffReq.duration_sec
					'.ChartAreas(0).AxisX2.Enabled = AxisEnabled.True

					My.Application.DoEvents()
					.Series(0).Points.AddXY(ChartLamda.ChartAreas(0).AxisX.Minimum, 200)
					.Series(0).Points.AddXY(ChartLamda.ChartAreas(0).AxisX.Maximum, 200)
					My.Application.DoEvents()

				End With

			End If
		Catch ex As Exception
			Throw New Exception("CATSRunBox.InitChart2Tone()::" & ex.Message)
		End Try
	End Sub
	Public Sub InitChart(sfReq As CATS.Model.cfg_imd_sfbox,
						 ffReq As CATS.Model.cfg_imd_ffbox,
						 ll As Single,
						 ul As Single)
		Try

			If sfReq IsNot Nothing Then InitChartSweep(sfReq, ll, ul)
			If ffReq IsNot Nothing Then InitChart2Tone(ffReq, ll, ul)
			If ffReq IsNot Nothing Then InitChartLambda(ffReq, ll, ul)
			My.Application.DoEvents()

		Catch ex As Exception
			Throw New Exception("CATSRunBox.InitChart()::" & ex.Message)
		End Try


	End Sub
#Region "LabelStatus"
	Public WriteOnly Property StatusText() As String
		Set(value As String)
			lblStatus.Text = Text
		End Set
	End Property
	Public WriteOnly Property StatusForeColor() As Color
		Set(value As Color)
			lblStatus.ForeColor = value
		End Set
	End Property
	Public Sub SetTestStatus(stat As String)
		Try
			If stat = "P" Then
				StatusText = "Pass"
				StatusForeColor = Color.Green
			ElseIf stat = "F" Then
				StatusText = "Fail"
				StatusForeColor = Color.Red
			ElseIf stat = "E" Then
				StatusText = "Error"
				StatusForeColor = Color.Red
			ElseIf stat = "A" Then
				StatusText = "Abort"
				StatusForeColor = Color.Orange
			End If

		Catch ex As Exception

		End Try
	End Sub
#End Region

#Region "ChartLamda"
	Public Sub MarkPointInChartLamda(seriesId As Short, trace As List(Of DataModels.FrequencyPoint), point As DataModels.FrequencyPoint)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Short, List(Of DataModels.FrequencyPoint), DataModels.FrequencyPoint)(AddressOf MarkPointInChartLamda), seriesId, trace, point)
			Else
				Dim tp_avg As Single
				Dim tp_value As Single
				Dim tp_x As Single
				Dim cal As New Calculate

				tp_avg = cal.Average(trace)

				'tmp_value = Math.Round((tmp_avg + 140) ^ 2 / Math.Abs(tmp_avg - point.y), 1)
				tp_value = Math.Round((IIf(point.YData < -158, point.YData, -158) + 140) ^ 2 / Math.Abs(tp_avg - point.YData), 1)
				tp_value = IIf(tp_value < 400, tp_value, 400)


				With ChartLamda
					Dim tmp_point As New DataPoint

					.Series(seriesId).Font = New Font("Arial", 9, FontStyle.Bold)
					tp_x = (.ChartAreas(0).AxisX.Maximum - .ChartAreas(0).AxisX.Minimum) / 2

					'tmp_point.SetValueXY((series - 2) * tmp_x, tmp_value)
					tmp_point.SetValueXY(tp_x, tp_value)
					tmp_point.Label = tp_value
					tmp_point.LabelForeColor = Color.Black
					tmp_point.MarkerSize = 7
					.Series(seriesId).Points.Add(tmp_point)

				End With
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.MarkPointInChartLamda()::" & ex.Message)
		End Try
	End Sub
	Private Sub AddPointToChartLamda(seriesId As Short, point As PointF)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Short, PointF)(AddressOf AddPointToChartLamda), seriesId, point)
			Else
				ChartLamda.Series(seriesId).Points.AddXY(point.X, point.Y)
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.AddPointToChartLamda()::" & ex.Message)
		Finally
		End Try
	End Sub
	Public Sub SetTraceToChartLambda(seriesId As Short, trace As List(Of PointF))
		Try
			If InvokeRequired = True Then
				Me.BeginInvoke(New Action(Of Short, List(Of PointF))(AddressOf SetTraceToChartLambda), seriesId, trace)
			Else
				For Each p In trace
					ChartLamda.Series(seriesId).Points.AddXY(p.X, p.Y)
				Next
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.AddTraceToChartLambda()::" & ex.Message)
		End Try
	End Sub

#End Region

#Region "ChartSweep"

	Private Sub MarkPointInChartSweep(seriesId As Short, markPoint As DataModels.FrequencyPoint, ByVal color As Color)
		Try
			If InvokeRequired = True Then
				Me.BeginInvoke(New Action(Of Short, DataModels.FrequencyPoint, System.Drawing.Color)(AddressOf MarkPointInChartSweep), seriesId, markPoint, color)
			Else
				If markPoint Is Nothing Then Return
				With ChartSweepFreq.Series(seriesId)
					.Font = New Font("Arial", 10, FontStyle.Bold)
					For Each tmp_point As DataPoint In .Points
						If tmp_point.XValue = markPoint.XData And tmp_point.YValues(0) = markPoint.YData Then
							tmp_point.MarkerStyle = DataVisualization.Charting.MarkerStyle.Diamond
							tmp_point.Label = CInt(markPoint.XData) & "," & Math.Round(markPoint.YData, 1)
							tmp_point.LabelForeColor = color
							tmp_point.MarkerSize = 10
							tmp_point.MarkerColor = color
						End If
					Next
				End With
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.MarkPointInChartSweep()::" & ex.Message)
		End Try
	End Sub
	Public Sub AddPointToChartSweep(seriesId As Short, point As PointF, markPoint As DataModels.FrequencyPoint)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Short, PointF, DataModels.FrequencyPoint)(AddressOf AddPointToChartSweep), seriesId, point, markPoint)
			Else
				ChartSweepFreq.Series(seriesId).Points.AddXY(point.X, point.Y)
				MarkPointInChartSweep(seriesId, markPoint, Color.Red)
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.AddPointToChartSweep()::" & ex.Message)
		End Try
	End Sub


#End Region

#Region "Chart2Tone"
	Public Sub AddPointToChart2Tone(seriesId As Short, point As PointF)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Short, PointF)(AddressOf AddPointToChart2Tone), seriesId, point)
			Else
				ChartSweepTime.Series(seriesId).Points.AddXY(point.X, point.Y)
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.AddPointToChart2Tone()::" & ex.Message)
		Finally
		End Try
	End Sub
	Public Sub MarkPointInChart2Tone(point As DataModels.FrequencyPoint)
		Try
			If Me.InvokeRequired Then
				Me.BeginInvoke(New Action(Of DataModels.FrequencyPoint)(AddressOf MarkPointInChart2Tone), point)
			Else
				Dim tmpX As Single
				Dim chartPoint As New DataPoint

				With ChartSweepTime
					.Series(4).Font = New Font("Arial", 9, FontStyle.Bold)
					tmpX = (.ChartAreas(0).AxisX.Maximum - .ChartAreas(0).AxisX.Minimum)
					chartPoint.SetValueXY(tmpX / 2, point.YData)
					chartPoint.Label = CInt(point.RxFreq) & "MHz," & point.YData & "dBc"
					chartPoint.LabelForeColor = Color.Black
					chartPoint.MarkerSize = 7
					.Series(4).Points.Add(chartPoint)
					Application.DoEvents()
				End With
			End If


		Catch ex As Exception
			Throw New Exception("CATSRunBox.MarkPointInChart2Tone()::" & ex.Message)
		End Try
	End Sub

	Public Sub AddStdTraceToChart2Tone(avg As Single, std As Single)
		Try
			If InvokeRequired Then
				Me.BeginInvoke(New Action(Of Single, Single)(AddressOf AddStdTraceToChart2Tone), avg, std)
			Else
				With ChartSweepTime
					.Series(2).Points.AddXY(.ChartAreas(0).AxisX.Minimum, avg - 6 * std)
					.Series(2).Points.AddXY(.ChartAreas(0).AxisX.Maximum, avg - 6 * std)
					.Series(3).Points.AddXY(.ChartAreas(0).AxisX.Minimum, avg + 6 * std)
					.Series(3).Points.AddXY(.ChartAreas(0).AxisX.Maximum, avg + 6 * std)
				End With
				Application.DoEvents()
			End If

		Catch ex As Exception
			Throw New Exception("CATSRunBox.AddStdTraceToChart2Tone()::" & ex.Message)
		End Try
	End Sub
	Private Sub CATSRunBox_Resize(sender As Object, e As EventArgs) Handles Me.Resize

	End Sub

#End Region

End Class
