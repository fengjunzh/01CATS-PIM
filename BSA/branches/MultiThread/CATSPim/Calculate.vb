Public Class Calculate
	Public Function GetImFreq(tx1 As Single, tx2 As Single, imSide As String, imOrder As Short) As Single
		Try
			Dim lvalue As Short
			Dim rvalue As Short

			lvalue = imOrder - (imOrder - 1) / 2
			rvalue = lvalue - 1

			If imSide.Trim.ToUpper = "L" Then
				Return Math.Round(tx1 * lvalue - tx2 * rvalue, 1)
			Else
				Return Math.Round(tx2 * lvalue - tx1 * rvalue, 1)
			End If

		Catch ex As Exception
			Throw New Exception("TestClass.Calculate.GetImFreq()::" & ex.Message)
		End Try
	End Function
	Public Function GetTxRange(value As CATS.Model.cfg_imd_sfbox) As DataModels.AxisMaxMin
		Try
			Dim resp As New DataModels.AxisMaxMin

			resp.Max = 0
			resp.Min = 99999

			resp.Max = CInt(Math.Max(Math.Max(value.ufreq_start, value.ufreq_stop), Math.Max(value.dfreq_start, value.dfreq_stop)))
			resp.Min = CInt(Math.Min(Math.Min(value.ufreq_start, value.ufreq_stop), Math.Min(value.dfreq_start, value.dfreq_stop)))

			Return resp

		Catch ex As Exception
			Throw New Exception("TestClass.Calculate.GetTxRange()::" & ex.Message)
		End Try
	End Function
	Public Function GetImRange(value As CATS.Model.cfg_imd_sfbox) As DataModels.AxisMaxMin
		Try
			Dim resp As New DataModels.AxisMaxMin

			Dim tp_txFreq As Single
			Dim tp_rxFreq As Single


			resp.Max = 0
			resp.Min = 99999

			If value.dfreq_start > 0 Then
				For tp_txFreq = value.dfreq_start To value.dfreq_stop Step value.dfreq_step * -1
					tp_rxFreq = GetImFreq(value.dfreq_fixed, tp_txFreq, value.imd_side, value.imd_order)
					resp.Max = Math.Max(resp.Max, tp_rxFreq)
					resp.Min = Math.Min(resp.Min, tp_rxFreq)
				Next
			End If

			If value.ufreq_start > 0 Then
				For tp_txFreq = value.ufreq_start To value.ufreq_stop Step value.ufreq_step
					tp_rxFreq = GetImFreq(tp_txFreq, value.ufreq_fixed, value.imd_side, value.imd_order)
					resp.Max = Math.Max(resp.Max, tp_rxFreq)
					resp.Min = Math.Min(resp.Min, tp_rxFreq)
				Next
			End If


			If resp.Max > Int(resp.Max) Then resp.Max = Int(resp.Max) + 1
			If resp.Min > Int(resp.Min) Then resp.Min = Int(resp.Min)

			Return resp

		Catch ex As Exception
			Throw New Exception("TestClass.Calculate.GetTxRange()::" & ex.Message)
		End Try
	End Function
	Public Function Average(x As List(Of DataModels.FrequencyPoint)) As Single
		Return Math.Round(x.Average(Function(t) t.YData), 3)
	End Function
	Public Function PeakPeak(x As List(Of DataModels.FrequencyPoint)) As Single
		Return Math.Round((x.Max(Function(t) t.YData) - x.Min(Function(t) t.YData)), 3)
	End Function
	Public Function Stdev(x As List(Of DataModels.FrequencyPoint)) As Single
		Dim tmpAvg As Single = Average(x)
		Dim tmpSum As Double = 0

		For Each p As DataModels.FrequencyPoint In x
			tmpSum += (p.YData - tmpAvg) ^ 2
		Next

		Return (tmpSum / (x.Count - 1)) ^ 0.5

	End Function
	Public Function Max(x As List(Of DataModels.FrequencyPoint)) As Single
		Return Math.Round(x.Max(Function(t) t.YData), 3)
	End Function
	Public Function Min(x As List(Of DataModels.FrequencyPoint)) As Single
		Return Math.Round(x.Min(Function(t) t.YData), 3)
	End Function
	Public Function SweepGap(ux As List(Of DataModels.FrequencyPoint), lx As List(Of DataModels.FrequencyPoint)) As Single
		Try
			Dim tmpMax As Single
			Dim tmpValue As Single

			For Each u As DataModels.FrequencyPoint In ux
				For Each l As DataModels.FrequencyPoint In lx
					If u.RxFreq = l.RxFreq Then
						tmpValue = Math.Abs(u.YData - l.YData)
						If tmpValue > tmpMax Then tmpMax = Math.Round(tmpValue, 3)
						Exit For
					End If
				Next
			Next
			Return tmpMax
		Catch ex As Exception
			Return -1
		End Try

	End Function

	Public Function LambdaPercent(x As List(Of PointF), limit As Single) As Single
		Try
			Dim tp_x As Integer
			Dim tp_y As Integer

			tp_y = x.Count

			For Each p In x
				If p.Y < limit Then tp_x += 1
			Next

			Return Math.Round(tp_x / tp_y, 3)

		Catch ex As Exception
			Throw New Exception("TestClass.Calculate.LambdaPercent()::" & ex.Message)
		End Try

	End Function
	Public Function CtrnStdev(trace As List(Of DataModels.FrequencyPoint),
									 baseLine As Single,
									 coefficient As Single,
									Optional limit As Single = 400) As Decimal
		Try
			Dim tp_avg As Single = Average(trace)
			Dim tp_std As Single = Stdev(trace)
			Dim tp_value As Single

			'tp_value = IIf(tp_avg > -140, 0, (tp_avg + 140) ^ 2 * 2 / tp_std * 0.3246)
			'tp_value = IIf(tp_avg > (-1 * pRTP.AlgoParas.Ctrn_AvgMaxLimit), 0, (tp_avg + pRTP.AlgoParas.Ctrn_AvgMaxLimit) ^ 2 * 2 / tp_std * pRTP.AlgoParas.Ctrn_CalcCoefficient)
			tp_value = IIf(tp_avg > (-1 * baseLine), 0, (tp_avg + baseLine) ^ 2 * 2 / tp_std * coefficient)


			'Return Math.Round(IIf(tp_value > 400, 400, tp_value), 1)
			Return Math.Round(IIf(tp_value > limit, limit, tp_value), 1)

		Catch ex As Exception
			Throw New Exception("TestClass.Calculate.CtrnStdev()::" & ex.Message)
		End Try

	End Function



#Region "Find point"
	Public Function FindPoint(pointList As List(Of DataModels.FrequencyPoint), point As DataModels.FrequencyPoint) As DataModels.FrequencyPoint
		Try

			For Each p As DataModels.FrequencyPoint In pointList
				If Math.Round(p.TxlFreq, 1) = Math.Round(point.TxlFreq, 1) And
		  Math.Round(p.TxrFreq, 1) = Math.Round(point.TxrFreq, 1) And
		  Math.Round(p.RxFreq, 1) = Math.Round(point.RxFreq, 1) Then
					Return p
				End If
			Next

			Return Nothing

		Catch ex As Exception
			Throw New Exception("FindPoint()::" & ex.Message)
		End Try

	End Function
	Public Function FindMinPoint(pointList As List(Of DataModels.FrequencyPoint)) As DataModels.FrequencyPoint
		Try
			'Dim rnt As DataModels.FrequencyPoint = pointList(0)

			'For Each p As DataModels.FrequencyPoint In pointList
			'  If p.yData < p.yData Then rnt = p
			'Next

			'Return rnt

			Return pointList.Find(Function(p1) p1.YData = pointList.Min(Function(p2) p2.YData))


		Catch ex As Exception
			Throw New Exception("FindPointMin(List)::" & ex.Message)
		End Try

	End Function
	Public Function FindMinPoint(pointList As List(Of DataModels.FrequencyPoint), limit As Single) As DataModels.FrequencyPoint
		Try
			'找大于-170的最小值，如果值都小于-170，则找最大值
			Dim rnt As DataModels.FrequencyPoint
			Dim tmpList As New List(Of DataModels.FrequencyPoint)

			For Each p As DataModels.FrequencyPoint In pointList
				If p.YData > limit Then tmpList.Add(p)  '将大于-170的测试值添加到 tmp_lst
			Next

			If tmpList.Count > 1 Then  '如果存在 >-170的值，找到一个最小值
				rnt = tmpList(0)
				For Each p As DataModels.FrequencyPoint In tmpList
					If p.YData < rnt.YData Then rnt = p
				Next
				Return rnt
			Else
				'如果不存在 >-170的值，在所有值中找最大值
				'rnt = data_points(0)
				'For Each item As st_freq_point In data_points
				'  If item.y > rnt.y Then rnt = item
				'Next
				'Return rnt
				Return FindMaxPoint(pointList)
			End If

		Catch ex As Exception
			Throw New Exception("FindMinPoint(List,Single)::" & ex.Message)
		End Try

	End Function
	Public Function FindMinPoint(pointList As List(Of DataModels.FrequencyPoint), freqLibs As List(Of Single), limit As Single) As DataModels.FrequencyPoint
		Try
			Dim rnt As DataModels.FrequencyPoint
			Dim tmpList As New List(Of DataModels.FrequencyPoint)

			For Each p As DataModels.FrequencyPoint In pointList
				For Each freq In freqLibs
					If Math.Round(p.RxFreq, 1) = Math.Round(freq, 1) Then
						tmpList.Add(p)
					End If
				Next
			Next

			If tmpList.Count >= 1 Then
				rnt = tmpList(0)
				For Each p As DataModels.FrequencyPoint In tmpList
					If p.YData < rnt.YData Then rnt = p
				Next
				Return rnt
			Else
				Return FindMinPoint(pointList, limit)
			End If

		Catch ex As Exception
			Throw New Exception("FindMinPoint(List,List,Single)::" & ex.Message)
		End Try

	End Function

	Public Function FindMaxPoint(pointList As List(Of DataModels.FrequencyPoint)) As DataModels.FrequencyPoint
		Try

			'Dim rnt As DataModels.FrequencyPoint = pointList(0)

			'For Each p As DataModels.FrequencyPoint In pointList
			'  If p.yData > rnt.yData Then rnt = p
			'Next
			'Return rnt

			Return pointList.Find(Function(p1) p1.YData = pointList.Max(Function(p2) p2.YData))


		Catch ex As Exception
			Throw New Exception("FindMaxPoint(List)::" & ex.Message)
		End Try
	End Function
	Public Function FindMaxPoint(pointList As List(Of DataModels.FrequencyPoint), limit As Single) As DataModels.FrequencyPoint
		Try
			'找大于-170的最小值，如果值都小于-170，则找最大值
			Dim rnt As DataModels.FrequencyPoint
			Dim tmpList As New List(Of DataModels.FrequencyPoint)

			For Each p As DataModels.FrequencyPoint In pointList
				If p.YData > limit Then tmpList.Add(p)
			Next

			If tmpList.Count >= 1 Then
				rnt = tmpList(0)
				For Each p As DataModels.FrequencyPoint In tmpList
					If p.YData > rnt.YData Then rnt = p
				Next
				Return rnt
			Else
				Return FindMaxPoint(pointList)
			End If

		Catch ex As Exception
			Throw New Exception("FindMaxPoint(List,Single)::" & ex.Message)
		End Try

	End Function
	Public Function FindMaxPoint(pointList As List(Of DataModels.FrequencyPoint), freq_libs As List(Of Single), limit As Single) As DataModels.FrequencyPoint
		Try

			Dim rnt As DataModels.FrequencyPoint
			Dim tmpList As New List(Of DataModels.FrequencyPoint)

			For Each p As DataModels.FrequencyPoint In pointList
				For Each freq In freq_libs
					If p.RxFreq = freq Then
						tmpList.Add(p)
					End If
				Next
			Next

			If tmpList.Count > 1 Then
				rnt = tmpList(0)
				For Each p As DataModels.FrequencyPoint In tmpList
					If p.YData > rnt.YData Then rnt = p
				Next
				Return rnt
			Else
				Return FindMaxPoint(pointList, limit)
			End If

		Catch ex As Exception
			Throw New Exception("FindMaxPoint(List,List,Single)::" & ex.Message)
		End Try

	End Function

#End Region
End Class
