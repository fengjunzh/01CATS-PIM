Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic
Imports System.Xml.Serialization

'spec_detail
<Serializable()>
Public Class spec_detail

    Private _id As Integer
    Private _group_main_id As Integer
    Private _order_idx As Integer
    Private _meas_item As String
    Private _limit_low As Decimal
    Private _limit_up As Decimal
    Private _limit_string As String
    Private _limit_unit As String
    Private _cust_limit_low As Decimal
    Private _cust_limit_up As Decimal
    Private _cust_limit_unit As String
    Private _cust_limit_string As String
    Private _meas_required As Boolean
    Private _dwtilt_idxs As String
    Private _dwtilt_angs As String
    Private _script As String
    Private _message As String
    Private _validity As Boolean
    Private _validation_date As DateTime
    Private _invalidation_date As DateTime
    Private _gen1 As String
    Private _gen2 As String
    Private _gen3 As String
    Private _sub_unit As Integer
    Private _fiber As Integer
    Private _wave_length As Integer

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property group_main_id() As Integer
        Get
            Return _group_main_id
        End Get
        Set(value As Integer)
            _group_main_id = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property order_idx() As Integer
        Get
            Return _order_idx
        End Get
        Set(value As Integer)
            _order_idx = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property meas_item() As String
        Get
            Return _meas_item
        End Get
        Set(value As String)
            _meas_item = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property limit_low() As Decimal
        Get
            Return _limit_low
        End Get
        Set(value As Decimal)
            _limit_low = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property limit_up() As Decimal
        Get
            Return _limit_up
        End Get
        Set(value As Decimal)
            _limit_up = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property limit_string() As String
        Get
            Return _limit_string
        End Get
        Set(value As String)
            _limit_string = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property limit_unit() As String
        Get
            Return _limit_unit
        End Get
        Set(value As String)
            _limit_unit = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property cust_limit_low() As Decimal
        Get
            Return _cust_limit_low
        End Get
        Set(value As Decimal)
            _cust_limit_low = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property cust_limit_up() As Decimal
        Get
            Return _cust_limit_up
        End Get
        Set(value As Decimal)
            _cust_limit_up = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property cust_limit_unit() As String
        Get
            Return _cust_limit_unit
        End Get
        Set(value As String)
            _cust_limit_unit = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property cust_limit_string() As String
        Get
            Return _cust_limit_string
        End Get
        Set(value As String)
            _cust_limit_string = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property meas_required() As Boolean
        Get
            Return _meas_required
        End Get
        Set(value As Boolean)
            _meas_required = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property script() As String
        Get
            Return _script
        End Get
        Set(value As String)
            _script = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property message() As String
        Get
            Return _message
        End Get
        Set(value As String)
            _message = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property validity() As Boolean
        Get
            Return _validity
        End Get
        Set(value As Boolean)
            _validity = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property validation_date() As DateTime
        Get
            Return _validation_date
        End Get
        Set(value As DateTime)
            _validation_date = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property invalidation_date() As DateTime
        Get
            Return _invalidation_date
        End Get
        Set(value As DateTime)
            _invalidation_date = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property gen1() As String
        Get
            Return _gen1
        End Get
        Set(value As String)
            _gen1 = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property gen2() As String
        Get
            Return _gen2
        End Get
        Set(value As String)
            _gen2 = value
        End Set
    End Property

    ''' <summary>
    ''' id
    ''' </summary>
    Public Property gen3() As String
        Get
            Return _gen3
        End Get
        Set(value As String)
            _gen3 = value
        End Set
    End Property

    Public Property Sub_unit As Integer
        Get
            Return _sub_unit
        End Get
        Set(value As Integer)
            _sub_unit = value
        End Set
    End Property

    Public Property Fiber As Integer
        Get
            Return _fiber
        End Get
        Set(value As Integer)
            _fiber = value
        End Set
    End Property

    Public Property Wave_length As Integer
        Get
            Return _wave_length
        End Get
        Set(value As Integer)
            _wave_length = value
        End Set
    End Property

    Public Function CompareTo(value As spec_detail) As Boolean
        Try

            If _order_idx <> value.order_idx Then Return False
            If _meas_item <> value.meas_item Then Return False
            If _limit_low <> value.limit_low Then Return False
            If _limit_up <> value.limit_up Then Return False
            If _limit_string <> value.limit_string Then Return False
            If _limit_unit <> value.limit_unit Then Return False
            If _sub_unit <> value.Sub_unit Then Return False
            If _fiber <> value.Fiber Then Return False
            If _wave_length <> value.Wave_length Then Return False
            If _meas_required <> value.meas_required Then Return False
            If _message <> value.message Then Return False

            Return True

        Catch ex As Exception
            Throw New Exception("spec_detail.CompareTo()::" & ex.Message)
        End Try

    End Function
End Class
