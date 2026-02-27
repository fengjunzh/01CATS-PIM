Imports System.Windows.Forms.DataVisualization.Charting
Public Class DelegateDisplay

	Public Class MTFormTest

		Private Delegate Sub m_delChartInitialize()
		Private Delegate Sub m_delChartSetDataPointFocus(point As DataModels.FrequencyPoint, color As Color)
		Private Delegate Sub m_delChartRefreshDataPoint(seriesId As Short, point As PointF, pointList As List(Of DataModels.FrequencyPoint))
		Private Delegate Sub m_delChartRefreshDataPoint2(seriesId As Short, point As PointF)

		'Private Delegate Sub m_delChartRefreshDataPoint_(data_points As List(Of DataModels.FrequencyPoint))
		Private Delegate Sub m_delSetControlText(msg As String)

		'Private m_form As FormTest
		'Public Sub New(formMain As FormTest)
		'	m_form = formMain
		'End Sub
		'Private Sub SetMarker(ByVal point As DataModels.FrequencyPoint, ByVal color As Color)
		'	Try

		'		With m_form.ChartSweepFreq.Series(point.SeriesId)
		'			.Font = New Font("Arial", 10, FontStyle.Bold)
		'			For Each tmp_point As DataPoint In .Points
		'				If tmp_point.XValue = point.XData And tmp_point.YValues(0) = point.YData Then
		'					tmp_point.MarkerStyle = DataVisualization.Charting.MarkerStyle.Diamond
		'					tmp_point.Label = CInt(point.XData) & "," & Math.Round(point.YData, 1)
		'					tmp_point.LabelForeColor = color
		'					tmp_point.MarkerSize = 10
		'					tmp_point.MarkerColor = color
		'				End If
		'			Next
		'		End With
		'	Catch ex As Exception
		'		Throw New Exception("MTFormTest.SetMarker()::" & ex.Message)
		'	End Try
		'End Sub
		''Private Sub DisplayImdValue(msg As String)
		''	m_form.lblStatus.Text = Math.Round(CType(msg, Single), 2)
		''End Sub
		''Public WriteOnly Property CRDisplayImdValue As String
		''	Set(value As String)
		''		If m_form.lblStatus.InvokeRequired = False Then
		''			DisplayImdValue(value)
		''		Else
		''			Dim delRun As New m_delSetControlText(AddressOf DisplayImdValue)
		''			Dim tmp_result As IAsyncResult

		''			tmp_result = m_form.lblStatus.BeginInvoke(delRun, value)

		''			m_form.lblStatus.EndInvoke(tmp_result)

		''		End If
		''	End Set
		''End Property
		'Private Sub RefreshChartFreqsweepPoint(seriesId As Short, point As PointF, pointList As List(Of DataModels.FrequencyPoint))
		'	Try

		'		Dim fPoint As DataModels.FrequencyPoint
		'		Dim focPoint As New DataModels.FrequencyPoint

		'		With m_form.ChartSweepFreq.Series(seriesId)

		'			.Points.AddXY(point.X, point.Y)

		'			If m_form.PFFReq IsNot Nothing Then
		'				focPoint.TxlFreq = m_form.PFFReq.c1_freq
		'				focPoint.TxrFreq = m_form.PFFReq.c2_freq
		'				focPoint.RxFreq = m_form.PFFReq.imd_freq

		'				fPoint = Calculate.FindPoint(pointList, focPoint)

		'				If fPoint IsNot Nothing Then SetMarker(fPoint, Color.Red)
		'			End If
		'		End With

		'	Catch ex As Exception
		'		Throw New Exception("MTFormTest.SetMarker()::" & ex.Message)
		'	End Try
		'End Sub
		'Public Sub CRRefreshChartFreqsweepPoint(seriesId As Short, point As PointF, pointList As List(Of DataModels.FrequencyPoint))

		'	Try
		'		If m_form.ChartSweepFreq.InvokeRequired = False Then

		'			RefreshChartFreqsweepPoint(seriesId, point, pointList)

		'		Else

		'			Dim delRun As New m_delChartRefreshDataPoint(AddressOf RefreshChartFreqsweepPoint)
		'			Dim tmResult As IAsyncResult

		'			tmResult = m_form.ChartSweepFreq.BeginInvoke(delRun, seriesId, point, pointList)

		'			m_form.ChartSweepFreq.EndInvoke(tmResult)

		'		End If

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.CRRefreshFreqsweepChartPoint()::" & ex.Message)
		'	End Try
		'End Sub
		'Private Sub RefreshChartTimesweepPoint(seriesId As Short, point As PointF, pointList As List(Of DataModels.FrequencyPoint))
		'	Try

		'		With m_form.ChartSweepTime.Series(seriesId)

		'			.Points.AddXY(point.X, point.Y)
		'		End With

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.RefreshChartTimesweepPoint()::" & ex.Message)
		'	Finally
		'	End Try
		'End Sub

		'Public Sub CRRefreshChartTimesweepPoint(seriesId As Short, point As PointF, pointList As List(Of DataModels.FrequencyPoint))

		'	Try
		'		If m_form.ChartSweepTime.InvokeRequired = False Then
		'			RefreshChartTimesweepPoint(seriesId, point, pointList)
		'		Else
		'			Dim delRun As New m_delChartRefreshDataPoint(AddressOf RefreshChartTimesweepPoint)
		'			Dim tmResult As IAsyncResult

		'			tmResult = m_form.ChartSweepTime.BeginInvoke(delRun, seriesId, point, pointList)
		'			m_form.ChartSweepTime.EndInvoke(tmResult)

		'		End If

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.CRRefreshChartTimesweepPoint()::" & ex.Message)
		'	End Try
		'End Sub
		'Public Sub DisplaySweepPoint(point As DataModels.FrequencyPoint)
		'	Try
		'		Dim tmpX As Single

		'		With m_form.ChartSweepTime

		'			Dim chartPoint As New DataPoint

		'			.Series(4).Font = New Font("Arial", 9, FontStyle.Bold)
		'			tmpX = (.ChartAreas(0).AxisX.Maximum - .ChartAreas(0).AxisX.Minimum)
		'			chartPoint.SetValueXY(tmpX / 2, point.YData)
		'			chartPoint.Label = CInt(point.RxFreq) & "MHz," & point.YData & "dBc"
		'			chartPoint.LabelForeColor = Color.Black
		'			chartPoint.MarkerSize = 7
		'			.Series(4).Points.Add(chartPoint)

		'			My.Application.DoEvents()

		'		End With

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.DisplaySweepPoint()::" & ex.Message)
		'	End Try
		'End Sub
		'Public Sub DisplayLamdaPoint(seriesId As Short, trace As List(Of DataModels.FrequencyPoint), point As DataModels.FrequencyPoint)
		'	Try

		'		Dim tp_avg As Single
		'		Dim tp_value As Single
		'		Dim tp_x As Single

		'		tp_avg = Calculate.Average(trace)

		'		'tmp_value = Math.Round((tmp_avg + 140) ^ 2 / Math.Abs(tmp_avg - point.y), 1)
		'		tp_value = Math.Round((IIf(point.YData < -158, point.YData, -158) + 140) ^ 2 / Math.Abs(tp_avg - point.YData), 1)
		'		tp_value = IIf(tp_value < 400, tp_value, 400)


		'		With m_form.ChartLamda
		'			Dim tmp_point As New DataPoint

		'			.Series(seriesId).Font = New Font("Arial", 9, FontStyle.Bold)
		'			tp_x = (.ChartAreas(0).AxisX.Maximum - .ChartAreas(0).AxisX.Minimum) / 2

		'			'tmp_point.SetValueXY((series - 2) * tmp_x, tmp_value)
		'			tmp_point.SetValueXY(tp_x, tp_value)
		'			tmp_point.Label = tp_value
		'			tmp_point.LabelForeColor = Color.Black
		'			tmp_point.MarkerSize = 7
		'			.Series(seriesId).Points.Add(tmp_point)

		'			My.Application.DoEvents()

		'		End With

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.DisplayLamdaPoint()::" & ex.Message)
		'	End Try
		'End Sub
		'Private Sub RefreshChartlamdaPoint(seriesId As Short, point As PointF)
		'	Try
		'		With m_form.ChartLamda.Series(seriesId)

		'			Dim tmp_point As PointF = point

		'			.Points.AddXY(point.X, point.Y)

		'		End With

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.RefreshChartlamdaPoint()::" & ex.Message)
		'	Finally
		'	End Try
		'End Sub
		'Public Sub CRRefreshChartlamdaPoint(seriesId As Short, point As PointF)

		'	Try
		'		If m_form.ChartLamda.InvokeRequired = False Then
		'			RefreshChartlamdaPoint(seriesId, point)
		'		Else
		'			Dim delRun As New m_delChartRefreshDataPoint2(AddressOf RefreshChartlamdaPoint)
		'			Dim tmp_result As IAsyncResult

		'			tmp_result = m_form.ChartLamda.BeginInvoke(delRun, seriesId, point)

		'			m_form.ChartLamda.EndInvoke(tmp_result)

		'		End If

		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.CRRefreshChartlamdaPoint()::" & ex.Message)
		'	End Try
		'End Sub
		'Public Sub RefreshLambdaChart(seriesId As Short, trace As List(Of PointF))
		'	Try

		'		For Each p In trace

		'			CRRefreshChartlamdaPoint(seriesId, p)

		'		Next


		'	Catch ex As Exception
		'		Throw New Exception("TestModules.MThreadFormTest.RefreshLambdaChart()::" & ex.Message)
		'	End Try
		'End Sub
	End Class
End Class
