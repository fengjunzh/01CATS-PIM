
Imports NationalInstruments.Visa

Public Interface PowerCal

    ReadOnly Property VendorID As String
    ReadOnly Property ProductID As String
    ReadOnly Property SN As String
    ReadOnly Property Address As String
    ReadOnly Property IDN As String
    Function open（ByVal DeviceName As String） As Boolean
    Function Close（） As Boolean
    Function Reset（） As Boolean
    Function Configure（ByVal freq As Single） As Boolean
    Function Cal（ByVal freq As Single） As Boolean
    Function Meaurement() As Single

End Interface

Public Class PowerMeterInitializtion
    Public Function Initializtion（） As String()
        Try
            Dim USBList As New ResourceManager
            Dim resources As Object = USBList.Find("(ASRL|GPIB|TCPIP|USB)?*")
            Return CType(resources, String())
        Catch ex As Exception
            MsgBox("Initializtion() error for power cal :" & ex.Message)
            Return Nothing
        End Try
    End Function

End Class


