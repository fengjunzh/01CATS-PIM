Public Class TraceXY
    Public _Points As New List(Of TraceXYPoint)
    Public Name As String
    Public NumPoints As Integer
    Public StartFreq As Double
    Public StopFreq As Double
    Public Sub Add(ByVal point As TraceXYPoint)
        _Points.Add(point)
    End Sub
    Public Sub New()

    End Sub
    Public Sub New(x As String, y As String)
        Dim xStr As String() = x.Split(",")
        Dim yStr As String() = y.Split(",")
        Dim len As Integer = Math.Min(xStr.Length, yStr.Length)
        Dim point As TraceXYPoint
        For i As Integer = 0 To len - 1
            point = New TraceXYPoint
            point.X = Double.Parse(xStr(i))
            point.Y = Double.Parse(yStr(i))
            Me.Add(point)
        Next
    End Sub

    ReadOnly Property XValues() As Double()
        Get
            Dim res(_Points.Count - 1) As Double
            For i As Integer = 0 To res.Length - 1
                res(i) = _Points(i).X
            Next
            Return res
        End Get
    End Property

    ReadOnly Property YRealValues(ByVal digitals As Integer) As Double()
        Get
            Dim res(_Points.Count - 1) As Double
            For i As Integer = 0 To res.Length - 1
                res(i) = Math.Round(_Points(i).Y.Real, digitals)
            Next
            Return res
        End Get
    End Property

    ReadOnly Property YImagValues() As Double()
        Get
            Dim res(_Points.Count - 1) As Double
            For i As Integer = 0 To res.Length - 1
                res(i) = _Points(i).Y.Imaginary
            Next
            Return res
        End Get
    End Property
    Public Property Points As List(Of TraceXYPoint)
        Get
            Return _Points
        End Get
        Set(value As List(Of TraceXYPoint))
            _Points = value
        End Set
    End Property
End Class
Public Class TraceXYPoint
    Public X As Double
    Public Y As System.Numerics.Complex
    Public Sub New(ByVal Xin As Double, ByVal Yin As System.Numerics.Complex)
        X = Xin
        Y = Yin
    End Sub
    Public Sub New()
    End Sub
End Class