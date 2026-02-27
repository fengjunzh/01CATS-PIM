Imports AndrewIntegratedProducts.InstrumentsFramework
Imports NationalInstruments.Visa
Imports System.Threading
Imports System.Windows.Forms

Public Class U2001A_PowerSensor
    Inherits PowerMeterInitializtion
    Implements PowerCal

    Private m_VendorID As String
    Private m_ProductID As String
    Private m_SN As String
    Private m_address As String
    Private m_IDN As String

    Private Dev_U2001A As MessageBasedSession

    Public Sub ReadIDN()
        Dim s As String = m_address.Replace("::", "|")
        m_VendorID = s.Split(CChar("|"))(1)
        m_ProductID = s.Split(CChar("|"))(2)
        m_SN = s.Split(CChar("|"))(3)
    End Sub
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

    Public Function Open（ByVal DeviceName As String） As Boolean Implements PowerCal.open
        Try
            Dim USBList As New ResourceManager
            Dim DevIDN As String
            Dev_U2001A = CType(USBList.Open(DeviceName), MessageBasedSession)
            If Dev_U2001A.ResourceName <> DeviceName Then Throw New Exception("Open fail!")
            Thread.Sleep(500)

            Dev_U2001A.RawIO.Write("*IDN?" & vbLf)
            DevIDN = Dev_U2001A.RawIO.ReadString()
            If DevIDN.ToUpper.Contains("U200") Or DevIDN.ToUpper.Contains("U204") Then
                Dev_U2001A.TimeoutMilliseconds = 5000
                MsgBox("Open success -->> " & DevIDN & "Please continue.", MsgBoxStyle.OkOnly)
                m_IDN = DevIDN
                Return True
            Else
                MsgBox("Open Fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If

            'Dev_U2001A.TimeoutMilliseconds = 5000
            'MsgBox("Open success！　please continue.", MsgBoxStyle.OkOnly)
            'Return True

        Catch ex As Exception
            Throw New Exception("Open error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Close（） As Boolean Implements PowerCal.Close
        Try
            Dev_U2001A.Dispose()
            MsgBox("Close success！　please continue.", MsgBoxStyle.OkOnly)
            Return True
        Catch ex As Exception
            Throw New Exception("Close error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Reset() As Boolean Implements PowerCal.Reset
        Try
            Dev_U2001A.RawIO.Write("*RST;*CLS")
            Dev_U2001A.RawIO.Write("syst:err?")
            If Dev_U2001A.RawIO.ReadString().Contains("No error") Then
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

    Public Function Configure(ByVal freq As Single) As Boolean Implements PowerCal.Configure
        Try
            Dev_U2001A.RawIO.Write("sens:freq " & freq & "MHz") ' 频率设定，大概设下就好了，对测试结果没啥影响的
            ' MsgBox("Setting success！　please continue.", MsgBoxStyle.OkOnly)
            Return True

        Catch ex As Exception
            Throw New Exception("Setting error , please check !." & ex.Message)
            Return False
        End Try
    End Function


    Public Function cal(ByVal freq As Single) As Boolean Implements PowerCal.Cal
        Try
            Dev_U2001A.RawIO.Write("*RST;*CLS")
            Thread.Sleep(1000)
            Dev_U2001A.RawIO.Write("syst:pres")
            Thread.Sleep(2000)

            Dev_U2001A.RawIO.Write("sens:freq " & freq & "MHz") ' 频率设定，大概设下就好了，对测试结果没啥影响的
            'Dev_U2001A.RawIO.Write("Trig:sour imm") '设定触发源bus/int/ext/hold/imm，preset 后就是IMM
            'Dev_U2001A.RawIO.Write("Trig:sour bus") '设定触发源bus/int/ext/hold/imm

            Dev_U2001A.RawIO.Write("cal:zero:type int")

            Dev_U2001A.RawIO.Write("syst:err?")
            If Not Dev_U2001A.RawIO.ReadString().Contains("No error") Then
                MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If

            Dev_U2001A.RawIO.Write("cal") '开始归零
            Dev_U2001A.RawIO.Write("syst:err?")
            If Not Dev_U2001A.RawIO.ReadString().Contains("No error") Then
                MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If

            Dev_U2001A.RawIO.Write("*OPC?")
            Dev_U2001A.TimeoutMilliseconds = 30000

            Dim success As Integer = CInt(Dev_U2001A.RawIO.ReadString())
            If success = 1 Then
                Dev_U2001A.TimeoutMilliseconds = 5000
                Dev_U2001A.RawIO.Write("INIT:CONT ON") ' 连续出发模式，时刻处于待触发模式，没有闲置状态，触发一次就直接测试一次，
                'Dev_U2001A.RawIO.Write("INIT:CONT Off") '单次触发模式
                Dev_U2001A.RawIO.Write("syst:err?")
                If Dev_U2001A.RawIO.ReadString().Contains("No error") Then
                    MsgBox("Calibrating success！　please continue.", MsgBoxStyle.OkOnly)
                    Return True
                Else
                    MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                    Return False
                End If
                'Return True
            Else
                Dev_U2001A.TimeoutMilliseconds = 5000
                MsgBox("Calibrating fail！　please continue.", MsgBoxStyle.OkOnly)
                Return False
            End If
        Catch ex As Exception
            Dev_U2001A.TimeoutMilliseconds = 5000
            Throw New Exception("Calibrating error , please check !." & ex.Message)
            Return False
        End Try
    End Function

    Public Function Meaurement() As Single Implements PowerCal.Meaurement

        Try

            'Dev_U2001A.RawIO.Write("trig") 'ok  for Trig:sour bus & INIT:CONT ON
            'Dev_U2001A.RawIO.Write("init") 'ok  for Trig:sour imm & INIT:CONT OFF
            'Thread.Sleep(500)
            Dev_U2001A.RawIO.Write("fetc?") 'ok  for Trig:sour imm & INIT:CONT ON

            'Dev_U2001A.RawIO.Write("CONF") '置待触发状态，for Trig:sour imm & INIT:CONT OFF
            'Dev_U2001A.RawIO.Write("READ?")

            ' Dev_U2001A.RawIO.Write("meas?")
            Thread.Sleep(40)
            Meaurement = CSng(Dev_U2001A.RawIO.ReadString().Replace(vbLf, ""))
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
