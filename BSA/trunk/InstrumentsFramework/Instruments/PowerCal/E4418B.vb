
Imports NationalInstruments.Visa
Imports System.Threading
Imports System.Windows.Forms

Public Class E4418B
    Inherits PowerMeterInitializtion
    Implements PowerCal

    Private m_VendorID As String
    Private m_ProductID As String
    Private m_SN As String
    Private m_address As String
    Private m_IDN As String

    Private E4418B As MessageBasedSession

    Public ReadOnly Property VendorID As String Implements PowerCal.VendorID
        Get
            Return m_VendorID
        End Get
    End Property

    Public ReadOnly Property ProductID As String Implements PowerCal.ProductID
        Get
            Return m_ProductID
        End Get
    End Property

    Public ReadOnly Property SN As String Implements PowerCal.SN
        Get
            Return m_SN
        End Get
    End Property

    Public ReadOnly Property IDN As String Implements PowerCal.IDN
        Get
            Return m_IDN
        End Get
    End Property

    Public Property address As String Implements PowerCal.Address
        Get
            Return m_address
        End Get
        Set(ByVal value As String)
            m_address = value
        End Set
    End Property

    Public Function Open（ByVal DeviceName As String） As Boolean Implements PowerCal.open   'OK
        Try
            Dim DeviceList As New ResourceManager
            Dim DevIDN As String
            E4418B = CType(DeviceList.Open(DeviceName), MessageBasedSession)
            If E4418B.ResourceName <> DeviceName Then Throw New Exception("Open fail!")
            Thread.Sleep(500)

            E4418B.RawIO.Write("*IDN?" & vbLf)
            DevIDN = E4418B.RawIO.ReadString()
            If DevIDN.ToUpper.Contains("E4418") Or DevIDN.ToUpper.Contains("N1913") Then
                E4418B.TimeoutMilliseconds = 5000
                MsgBox("Open success -->> " & DevIDN & "Please continue.", MsgBoxStyle.OkOnly)
                m_IDN = DevIDN
                Return True
            Else
                MsgBox("Open Fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If

            'If DeviceName.ToUpper.Contains("ASRL") Then 'if change and different from your PC, will error!
            '    E4418B.RawIO.Write("SYST:COMM:SER:BAUD 9600" & vbLf)
            '    Thread.Sleep(200)
            '    E4418B.RawIO.Write("SYST:COMM:SER:BIT 8" & vbLf)
            '    Thread.Sleep(200)
            '    E4418B.RawIO.Write("SYST:COMM:SER:SBIT 1" & vbLf)
            '    Thread.Sleep(200)
            '    E4418B.RawIO.Write("SYST:COMM:SER:PAR NONE" & vbLf)
            'End If

            'E4418B.TimeoutMilliseconds = 5000
            'MsgBox("Open success！　please continue.", MsgBoxStyle.OkOnly)
            ' Return True
        Catch ex As Exception
            Throw New Exception("Open error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Close（） As Boolean Implements PowerCal.Close 'OK
        Try
            E4418B.Dispose()
            MsgBox("Close success！　please continue.", MsgBoxStyle.OkOnly)
            Return True
        Catch ex As Exception
            Throw New Exception("Close error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Reset() As Boolean Implements PowerCal.Reset 'OK
        Try
            E4418B.RawIO.Write("*RST;*CLS" & vbLf)
            Thread.Sleep(1000)
            E4418B.RawIO.Write("syst:pres" & vbLf)
            Thread.Sleep(2000)
            E4418B.RawIO.Write("syst:err?" & vbLf)
            If E4418B.RawIO.ReadString().Contains("No error") Then
                MsgBox("Reset success！　please continue.", MsgBoxStyle.OkOnly)
                Return True
            Else
                MsgBox("Reset Fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Reset error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Configure(ByVal freq As Single) As Boolean Implements PowerCal.Configure 'ok
        Try
            E4418B.RawIO.Write("sens:freq " & freq & "MHz" & vbLf) ' 频率设定，大概设下就好了，对测试结果没啥影响的
            ' MsgBox("Setting success！　please continue.", MsgBoxStyle.OkOnly)
            Return True

        Catch ex As Exception
            Throw New Exception("Setting error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function cal(ByVal freq As Single) As Boolean Implements PowerCal.Cal 'ok
        Try
            E4418B.RawIO.Write("*RST;*CLS" & vbLf)
            Thread.Sleep(1000)
            E4418B.RawIO.Write("syst:pres" & vbLf)
            Thread.Sleep(2000)

            E4418B.RawIO.Write("sens:freq " & freq & "MHz" & vbLf) ' 频率设定，大概设下就好了，对测试结果没啥影响的
            Thread.Sleep(300)
            'E4418B.RawIO.Write("Trig:sour imm") '设定触发源bus/int/ext/hold/imm，preset 后就是IMM
            'E4418B.RawIO.Write("Trig:sour bus") '设定触发源bus/int/ext/hold/imm

            ' E4418B.RawIO.Write("cal:zero:type int")

            'E4418B.RawIO.Write("syst:err?")
            'If Not E4418B.RawIO.ReadString().Contains("No error") Then
            '    MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
            '    Return False
            'End If


            'E4418B.RawIO.Write("cal:zero:auto once") ' zeroing
            'MsgBox("Zeoring,please check there is no power applied to the power sendor！　please continue.", MsgBoxStyle.OkOnly)

            'E4418B.RawIO.Write("cal:auto once") ' calibrating
            'MsgBox("Calibrating,please make power sendor connect to POWER REF！　please continue.", MsgBoxStyle.OkOnly)

            E4418B.TimeoutMilliseconds = 30000
            MsgBox("Calibrating,please make power sendor connect to POWER REF！　please continue.", MsgBoxStyle.OkOnly)
            E4418B.RawIO.Write("cal?" & vbLf) '开始归零和校准
            If E4418B.RawIO.ReadString().Contains("0") Then
                E4418B.RawIO.Write("syst:err?" & vbLf)
                If Not E4418B.RawIO.ReadString().Contains("No error") Then
                    MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                    Return False
                End If
                E4418B.TimeoutMilliseconds = 5000
                E4418B.RawIO.Write("INIT:CONT ON" & vbLf) ' 连续出发模式，时刻处于待触发模式，没有闲置状态，触发一次就直接测试一次，
                Thread.Sleep(300)
                'E4418B.RawIO.Write("INIT:CONT Off") '单次触发模式
                E4418B.RawIO.Write("syst:err?" & vbLf)
                If E4418B.RawIO.ReadString().Contains("No error") Then
                    MsgBox("Calibrating success！　please continue.", MsgBoxStyle.OkOnly)
                    Return True
                Else
                    MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                    Return False
                End If
                'Return True
            Else
                E4418B.TimeoutMilliseconds = 5000
                MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch ex As Exception
            E4418B.TimeoutMilliseconds = 5000
            Throw New Exception("Calibrating error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Meaurement() As Single Implements PowerCal.Meaurement

        Try

            'E4418B.RawIO.Write("trig") 'ok  for Trig:sour bus & INIT:CONT ON
            'E4418B.RawIO.Write("init") 'ok  for Trig:sour imm & INIT:CONT OFF
            'Thread.Sleep(500)
            E4418B.RawIO.Write("fetc?" & vbLf) 'ok  for Trig:sour imm & INIT:CONT ON

            'E4418B.RawIO.Write("CONF") '置待触发状态，for Trig:sour imm & INIT:CONT OFF
            'E4418B.RawIO.Write("READ?")

            ' E4418B.RawIO.Write("meas?")
            Thread.Sleep(40)
            Meaurement = CSng(E4418B.RawIO.ReadString().Replace(vbLf, ""))
            Thread.Sleep(20)
            ' Meaurement = CSng(Math.Round(Meaurement, 3))
            Meaurement = CSng(Format(Meaurement, "0.000"))

            Return Meaurement
        Catch ex As Exception
            Throw New Exception("Read data error , please check !." & ex.Message)
            Return 0
        End Try

    End Function

End Class
