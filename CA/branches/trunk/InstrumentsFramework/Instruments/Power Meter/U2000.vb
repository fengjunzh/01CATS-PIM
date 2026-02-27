Public Class U2000
    Inherits SCPIInstrument
    Implements IPowerMeter

    Private Function OneToTrue(ByVal isOne As String) As Boolean
        If isOne.EndsWith("1") Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function TrueToON(ByVal isTrue As Boolean) As String
        If isTrue Then
            Return "ON"
        Else
            Return "OFF"
        End If
    End Function

    Public Function GetTrace() As TraceXY Implements IPowerMeter.GetTrace
        Dim PreviousTimeOut As Double
        Dim PreviousBufferSize As Integer = 1024
        Dim resTrace As New TraceXY
        Try
            MyBase.SendSCPIComand(":FORM:BORD SWAP;")
            MyBase.SendSCPIComand(":FORM:DATA ASC;")
            MyBase.SendSCPIComand(":SENSe:DETector:FUNCtion NORMal")
            MyBase.SendSCPIComand(":TRIGger:SOURce INTernal")
            MyBase.SendSCPIComand(":TRACe:STATe 1")
            MyBase.SendSCPIComand(":SENSe:TRACe:TIME 0.001")
            MyBase.SendSCPIComand(":SENSe:TRACe:OFFSet:TIME 0.0002")
            Dim resStr As String = MyBase.SendSCPIComand(":TRACe:DATA? MRES")


            Return resTrace
        Catch ex As Exception
            Throw New Exception("U2000.GetTrace()::" & vbCrLf & " at " & ex.Message)
        Finally
            MyBase.Timeout = PreviousTimeOut
            Me.ClearError()
        End Try
    End Function

    Public Property ZeroType As IPowerMeter.PSZeroType Implements IPowerMeter.ZeroType
        Get
            Try
                Return [Enum].Parse(GetType(IPowerMeter.PSZeroType), MyBase.SendSCPIComand(String.Format(":CAL:ZERO:TYPE?")))
            Catch ex As Exception
                Throw New Exception("U2000.Get Zero Type()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Get
        Set(value As IPowerMeter.PSZeroType)
            Try
                MyBase.SendSCPIComand(String.Format(":CAL:ZERO:TYPE {0}", value))
            Catch ex As Exception
                Throw New Exception("U2000.Set Zero Type()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Set
    End Property

    Public ReadOnly Property Zeroing As Boolean Implements IPowerMeter.Zeroing
        Get
            Dim tempTimeOut As Integer
            Try
                tempTimeOut = MyBase.Timeout
                MyBase.Timeout = 100000
                Dim val As String = MyBase.SendSCPIComand(":CAL:ALL?;*OPC?")
                'Dim val As String = MyBase.SendSCPIComand(":CAL:ALL?")
                Return Not OneToTrue(val.Split(";")(0))
            Catch ex As Exception
                Throw New Exception("U2000.Zeroing()::" & vbCrLf & " at " & ex.Message)
            Finally
                MyBase.Timeout = tempTimeOut
            End Try
        End Get
    End Property

    Public Property Continuous As Boolean Implements IPowerMeter.Continuous
        Get
            Try
                Return OneToTrue(MyBase.SendSCPIComand(":INIT:CONT?"))
            Catch ex As Exception
                Throw New Exception("U2000.Continuous.Get()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Get
        Set(value As Boolean)
            Try
                MyBase.SendSCPIComand(String.Format(":INIT:CONT:ALL {0}", TrueToON(value)))
            Catch ex As Exception
                Throw New Exception("U2000.Continuous.Set()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Set
    End Property
    Public ReadOnly Property Measure(expected_value As Integer, resolution As Decimal) As String Implements IPowerMeter.Measure
        Get
            Dim tempTimeOut As Integer
            Try
                MyBase.Timeout = 30000
                'MyBase.SendSCPIComand(String.Format(":FORMat:READings:DATA ASC"))
                Return MyBase.SendSCPIComand(":FETCh?")
            Catch ex As Exception
                Throw New Exception("U2000.Measure.Get()::" & vbCrLf & " at " & ex.Message)
            Finally
                MyBase.Timeout = tempTimeOut
            End Try
        End Get
    End Property

    Public Property Freq As Integer Implements IPowerMeter.Freq
        Get
            Try
                Return (MyBase.SendSCPIComand(":SENSe:FREQuency?")) / 10 ^ 6
            Catch ex As Exception
                Throw New Exception("U2000.Freq.Get()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Get
        Set(value As Integer)
            Try
                MyBase.SendSCPIComand(String.Format(":SENSe:FREQuency:FIXed {0}", value * 10 ^ 6))
            Catch ex As Exception
                Throw New Exception("U2000.Freq.Set()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Set
    End Property

    Public Property MeasRate As IPowerMeter.PSMeasRate Implements IPowerMeter.MeasRate
        Get
            Try
                Return [Enum].Parse(GetType(IPowerMeter.PSMeasRate), MyBase.SendSCPIComand(String.Format(":SENS:MRATR?")))
            Catch ex As Exception
                Throw New Exception("U2000.MeasRate.Get()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Get
        Set(value As IPowerMeter.PSMeasRate)
            Try
                MyBase.SendSCPIComand(String.Format(":SENS:MRATR {0}", value))
            Catch ex As Exception
                Throw New Exception("U2000.MeasRate.Set()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Set
    End Property

    Public Property TriggerCount As Integer Implements IPowerMeter.TriggerCount
        Get
            Try
                Return MyBase.SendSCPIComand(":TRIGger:SEQuence:COUNt?")
            Catch ex As Exception
                Throw New Exception("U2000.Freq.Get()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Get
        Set(value As Integer)
            Try
                MyBase.SendSCPIComand(String.Format(":TRIGger:SEQuence:COUNt {0}", value))
            Catch ex As Exception
                Throw New Exception("U2000.Freq.Set()::" & vbCrLf & " at " & ex.Message)
            End Try
        End Set
    End Property
End Class
