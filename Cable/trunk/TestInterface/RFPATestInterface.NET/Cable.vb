Imports System.Xml
Public Class Cable
    Private _serial_Number As String
    Private _part_Number As String
    Private _core_Number As String
    Private _work_Order As String
    Private _test_Connector As String
    Private _original_length_m As Decimal
    Private _test_Length_M As Decimal
    Private _temperature_c As Decimal
    Public Sub New()

    End Sub
    Public Sub New(ByVal NdId As XmlNode)
        For Each nd As XmlNode In NdId
            If nd.Name = "Cable" Then
                Dim keystr As String = nd.Attributes("Key").Value
                Dim valuestr As String = nd.Attributes("Value").Value
                Select Case keystr
                    Case "SN"
                        Serial_Number = valuestr.ToUpper
                    Case "Original_Length_M"
                        Original_Length_M = Decimal.Parse(valuestr)
                    Case "Core_Number"
                        Core_Number = valuestr.ToUpper
                    Case "Work_Order"
                        Work_Order = valuestr.ToUpper
                    Case "Test_Length_M"
                        Test_Length_M = Decimal.Parse(valuestr)
                    Case "Temperature"
                        Temperature_C = Decimal.Parse(valuestr)
                End Select
            End If
        Next
    End Sub

    Public Property Serial_Number As String
        Get
            Return _serial_Number
        End Get
        Set(value As String)
            _serial_Number = value
        End Set
    End Property

    Public Property Part_Number As String
        Get
            Return _part_Number
        End Get
        Set(value As String)
            _part_Number = value
        End Set
    End Property

    Public Property Core_Number As String
        Get
            Return _core_Number
        End Get
        Set(value As String)
            _core_Number = value
        End Set
    End Property

    Public Property Work_Order As String
        Get
            Return _work_Order
        End Get
        Set(value As String)
            _work_Order = value
        End Set
    End Property

    Public Property Test_Connector As String
        Get
            Return _test_Connector
        End Get
        Set(value As String)
            _test_Connector = value
        End Set
    End Property

    Public Property Original_Length_M As Decimal
        Get
            Return _original_length_m
        End Get
        Set(value As Decimal)
            _original_length_m = value
        End Set
    End Property

    Public Property Temperature_C As Decimal
        Get
            Return _temperature_c
        End Get
        Set(value As Decimal)
            _temperature_c = value
        End Set
    End Property

    Public Property Test_Length_M As Decimal
        Get
            Return _test_Length_M
        End Get
        Set(value As Decimal)
            _test_Length_M = value
        End Set
    End Property
End Class
