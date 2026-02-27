Imports System.Xml.Serialization
<XmlRoot("CATS")>
Public Class CATSConfig
    Private _Property As New PropertyElement
    Private _Factory As New FactoryElement
    Private _TestBench As New TestBenchElement
    Private _DataBase As New DataBaseElement

    <XmlElement("Property")>
    Public Property Property_ As PropertyElement
        Get
            Return _Property
        End Get
        Set(value As PropertyElement)
            _Property = value
        End Set
    End Property
    Public Property Factory As FactoryElement
        Get
            Return _Factory
        End Get
        Set(value As FactoryElement)
            _Factory = value
        End Set
    End Property
    Public Property TestBench As TestBenchElement
        Get
            Return _TestBench
        End Get
        Set(value As TestBenchElement)
            _TestBench = value
        End Set
    End Property
    Public Property DataBase As DataBaseElement
        Get
            Return _DataBase
        End Get
        Set(value As DataBaseElement)
            _DataBase = value
        End Set
    End Property


End Class
<XmlType("Property")>
Public Class PropertyElement

    Private _CreateTime As DateTime

    Public Property CreateTime As DateTime
        Get
            Return _CreateTime
        End Get
        Set(value As DateTime)
            _CreateTime = value
        End Set
    End Property

End Class

<XmlType("Factory")>
Public Class FactoryElement

    Private _Location As String

    Public Property Location As String
        Get
            Return _Location
        End Get
        Set(value As String)
            _Location = value
        End Set
    End Property

End Class

<XmlType("TestBench")>
Public Class TestBenchElement
    Private _Mode As String

    Public Property Mode As String
        Get
            Return _Mode
        End Get
        Set(value As String)
            _Mode = value
        End Set
    End Property
End Class

<XmlType("DataBase")>
Public Class DataBaseElement
    Private _ConnString As String

    Public Property ConnString As String
        Get
            Return _ConnString
        End Get
        Set(value As String)
            _ConnString = value
        End Set
    End Property
End Class


