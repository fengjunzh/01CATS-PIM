Imports C1.Win.C1Chart
Imports C1.Win.C1FlexGrid

Module GlobalFunctions
    Public Sub FormatGrid(flexGrid As C1FlexGrid, captionFontSize As Short, normalStyleFontSize As Short)
        Try
            'adding Three-Dimensional Text to a Header Row
            Dim tdt As C1.Win.C1FlexGrid.CellStyle
            tdt = flexGrid.Styles.Add("3DText")
            tdt.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
            flexGrid.Rows(0).Style = tdt

            'adding Row Numbers in a Fixed Column
            flexGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw

            flexGrid.Styles.Fixed.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            flexGrid.Styles.Fixed.Font = New Font("Arial", captionFontSize, FontStyle.Bold)

            For Each col As C1.Win.C1FlexGrid.Column In flexGrid.Cols
                col.TextAlign = TextAlignEnum.LeftCenter
            Next

            'set the border style property
            flexGrid.Styles("Normal").Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Flat
            flexGrid.Styles("Normal").Border.Color = Color.DarkGray
            flexGrid.Styles("Normal").Font = New Font("Arial", normalStyleFontSize, FontStyle.Regular)

            flexGrid.AutoSizeRows()
            flexGrid.AutoSizeCols()

        Catch ex As Exception
            Throw New Exception("FormatGrid()::" & ex.Message)
        End Try
    End Sub
    Public Sub InitChart(chart As C1Chart, headerText As String, showLegend As Boolean, Optional xText As String = "Frequency(MHz)", Optional yText As String = "Amplitude(dB)", Optional showFooter As Boolean = True, Optional isPolar As Boolean = False)
        Try
            ' Clear the existing data
            chart.ChartGroups(0).ChartData.SeriesList.Clear()
            chart.ChartGroups(1).ChartData.SeriesList.Clear()
            chart.ChartLabels.LabelsCollection.Clear()
            If isPolar Then
                chart.ChartGroups(0).ChartType = Chart2DTypeEnum.Polar
            Else
                chart.ChartGroups(0).ChartType = Chart2DTypeEnum.XYPlot
            End If
            Dim ax As C1.Win.C1Chart.Axis = chart.ChartArea.AxisX
            ax.ValueLabels.Clear()
            Dim ay As C1.Win.C1Chart.Axis = chart.ChartArea.AxisY
            ay.ValueLabels.Clear()

            chart.ChartArea.Margins.Left = 10
            chart.ChartArea.Margins.Top = 10
            chart.ChartArea.Margins.Right = 10
            chart.ChartArea.Margins.Bottom = 10

            ' Setup header
            chart.Header.Style.BackColor = Color.Bisque
            chart.Header.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.RaisedBevel
            chart.Header.Style.Border.Thickness = 1
            chart.Header.Style.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            chart.Header.Text = headerText
            chart.Header.Visible = True

            ' Setup footer
            chart.Footer.Style.BackColor = Color.Bisque
            chart.Footer.Style.ForeColor = Color.Red
            chart.Footer.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.RaisedBevel
            chart.Footer.Style.Border.Thickness = 1
            chart.Footer.Style.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            chart.Footer.Text = "Selection: none"
            chart.Footer.Visible = showFooter

            ' Setup chart area
            chart.ChartArea.Style.BackColor = Color.Black
            chart.ChartArea.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.None
            chart.ChartArea.Style.Border.Thickness = 0
            'chart.ChartArea.Style.GradientStyle = C1.Win.C1Chart.GradientStyleEnum.Vertical
            'chart.ChartArea.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.Fillet
            'chart.ChartArea.Size = New Size(chart.Width - 100, chart.ChartArea.Size.Height)

            ' Setup legend
            chart.Legend.Style.BackColor = Color.PaleGreen
            chart.Legend.Style.Border.BorderStyle = C1.Win.C1Chart.BorderStyleEnum.Groove
            chart.Legend.Style.Border.Thickness = 2
            chart.Legend.Style.Border.Color = Color.LightBlue
            chart.Legend.Style.HorizontalAlignment = C1.Win.C1Chart.AlignHorzEnum.Center
            chart.Legend.Style.Font = New Font("Tahoma", 10)
            chart.Legend.Visible = showLegend

            ' Reset axes limits
            If isPolar Then
                'chart.ChartArea.AxisX.AutoMin = False
                'chart.ChartArea.AxisX.AutoMax = False
                'chart.ChartArea.AxisX.Min = -90
                'chart.ChartArea.AxisX.Max = 90
                chart.ChartGroups(0).Polar.Start = 90
            Else
                chart.ChartArea.AxisX.AutoMin = True
                chart.ChartArea.AxisX.AutoMax = True
            End If

            chart.ChartArea.AxisY.AutoMin = True
            chart.ChartArea.AxisY.AutoMax = True

            chart.ChartArea.AxisX.Text = xText
            chart.ChartArea.AxisY.Text = yText

            chart.ChartArea.AxisX.GridMajor.Visible = True
            chart.ChartArea.AxisY.GridMajor.Visible = True
            'chart.ChartArea.AxisX.GridMinor.Visible = True
            'chart.ChartArea.AxisY.GridMinor.Visible = True
            chart.ChartArea.AxisX.GridMajor.Color = Color.Gray
            chart.ChartArea.AxisY.GridMajor.Color = Color.Gray
            'chart.ChartArea.AxisX.GridMinor.Color = Color.Black
            'chart.ChartArea.AxisY.GridMinor.Color = Color.Black

            ' Enable tooltip
            chart.ToolTip.Enabled = True

        Catch ex As Exception
            Throw New Exception("FrmDiagnosticReport.InitChart()::" & ex.Message)
        End Try
    End Sub
    Public Sub AddSerieIntoChart(ByVal chart As C1Chart, ByVal serieLabel As String, ByVal xDataList As List(Of Double), ByVal yDataList As List(Of Double), ByVal lineStyleColor As Color, ByVal Optional lineStyleThickness As Integer = 2, ByVal Optional symbolStyleShape As SymbolShapeEnum = SymbolShapeEnum.None)
        Try
            Dim cdsc As ChartDataSeriesCollection = chart.ChartGroups(0).ChartData.SeriesList
            Dim cds As ChartDataSeries = cdsc.AddNewSeries()
            cds.Label = serieLabel
            cds.LineStyle.Thickness = lineStyleThickness
            cds.LineStyle.Color = lineStyleColor
            cds.SymbolStyle.Shape = symbolStyleShape
            cds.SymbolStyle.OutlineColor = Color.Green
            cds.SymbolStyle.Color = Color.Green
            cds.SymbolStyle.OutlineWidth = 2
            cds.SymbolStyle.Size = 3
            cds.X.CopyDataIn(xDataList.ToArray)
            cds.Y.CopyDataIn(yDataList.ToArray)
            cds.Tag = 1
            cds = Nothing
        Catch ex As Exception
            Throw New Exception("AddNewSerie()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Public Sub AddSerieIntoChart(ByVal chart As C1Chart, ByVal serieLabel As String, ByVal xData As Array, ByVal yData As Array, ByVal lineStyleColor As Color, ByVal Optional lineStyleThickness As Integer = 2)
        Try
            Dim data As ChartData = chart.ChartGroups(0).ChartData
            Dim series As ChartDataSeriesCollection = data.SeriesList
            Dim newSerie As ChartDataSeries = series.AddNewSeries()
            newSerie.Label = serieLabel
            newSerie.Tag = serieLabel
            newSerie.LineStyle.Thickness = lineStyleThickness
            newSerie.LineStyle.Color = lineStyleColor
            newSerie.SymbolStyle.Size = 10
            newSerie.SymbolStyle.OutlineColor = Color.Red
            newSerie.SymbolStyle.OutlineWidth = 2
            newSerie.X.CopyDataIn(xData)
            newSerie.Y.CopyDataIn(yData)
            newSerie = Nothing
        Catch ex As Exception
            Throw New Exception("AddSerieIntoChart()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
End Module
