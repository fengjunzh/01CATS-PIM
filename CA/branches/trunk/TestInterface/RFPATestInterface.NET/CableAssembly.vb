Public Class CableAssembly
    Private _serialNumber As String
    Private _plant As String
    Private _orderNumber As String
    Private _partNumber As String
    Private _family As String
    Private _orderDate As String
    Private _length As String
    Private _uOM As String
    Public Sub New()

    End Sub
    Public Sub New(product As SerialNumInq.Product)
        With product
            _serialNumber = .SerialNumber
            _partNumber = .PartNumber
            _plant = .Plant
            _orderNumber = Right(.OrderNumber, 10).Trim
            _orderDate = .OrderDate
            _length = .Length
            _uOM = .UOM
        End With
    End Sub
    Public Property SerialNumber As String
        Get
            Return _serialNumber
        End Get
        Set(value As String)
            _serialNumber = value
        End Set
    End Property

    Public Property Plant As String
        Get
            Return _plant
        End Get
        Set(value As String)
            _plant = value
        End Set
    End Property

    Public Property OrderNumber As String
        Get
            Return _orderNumber
        End Get
        Set(value As String)
            _orderNumber = value
        End Set
    End Property

    Public Property PartNumber As String
        Get
            Return _partNumber
        End Get
        Set(value As String)
            _partNumber = value
        End Set
    End Property
    Public Property Family As String
        Get
            Return _family
        End Get
        Set(value As String)
            _family = value
        End Set
    End Property
    Public Property OrderDate As String
        Get
            Return _orderDate
        End Get
        Set(value As String)
            _orderDate = value
        End Set
    End Property

    Public Property Length As String
        Get
            Return _length
        End Get
        Set(value As String)
            _length = value
        End Set
    End Property

    Public Property UOM As String
        Get
            Return _uOM
        End Get
        Set(value As String)
            _uOM = value
        End Set
    End Property

    Public Function CompareTo(value As CableAssembly) As Boolean
        Try

            If _partNumber <> value.PartNumber Then Return False
            If _orderNumber <> value.OrderNumber Then Return False

            Return True

        Catch ex As Exception
            Throw New Exception("CableAssembly.CompareTo()::" & ex.Message)
        End Try

    End Function
End Class
