Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("clsMeasurement_NET.clsMeasurement")> Public Class clsMeasurement
	
	Public name As String
	Public MeasuredValue As Double
	Public MeasuredString As String
	Public NumericType As Boolean
	Public LowerLimit As Double
	Public UpperLimit As Double
	Public Passed As Boolean
	Public TestGroup As String
	Public Units As String
	Public MeasurementTime As String 'Gen4
	Public Gen5 As String
End Class