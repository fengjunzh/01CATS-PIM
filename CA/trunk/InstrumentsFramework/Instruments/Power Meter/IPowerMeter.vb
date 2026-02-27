Imports System.Numerics
Public Interface IPowerMeter
    Enum PSZeroType
        INT = 0
        EXT = 1
    End Enum
    Enum PSMeasRate
        Norm = 0
        DOUB = 1
        FAST = 2
    End Enum
    Property ZeroType As PSZeroType
    ReadOnly Property Zeroing() As Boolean
    Property Continuous As Boolean
    ReadOnly Property Measure(expected_value As Integer, resolution As Decimal) As String
    Property Freq As Integer 'MHz
    Property MeasRate As PSMeasRate
    Property TriggerCount As Integer
    Function GetTrace() As TraceXY

End Interface

