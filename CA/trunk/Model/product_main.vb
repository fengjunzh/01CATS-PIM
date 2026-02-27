Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic


'product_main
<Serializable()>
Public Class product_main

  Private _id As Integer
  Private _customer_id As Integer
  Private _product_name As String
  Private _cust_product_name As String
  Private _product_ver As String
  Private _product_desc As String
  Private _family As String
  Private _validity As Boolean
  Private _sn_format As String
  Private _sn_check As Boolean
  Private _port_num As Integer
  Private _dwtilt_enable As Boolean
  Private _dwtilt_type As Integer
  Private _dwtilt_num As Integer
  Private _dwtilt_mode As String
  Private _dwtilt_pn As String
  Private _function_id As Integer
  Private _gen1 As String
  Private _gen2 As String
  Private _gen3 As String

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
  Public Property customer_id() As Integer
    Get
      Return _customer_id
    End Get
    Set(value As Integer)
      _customer_id = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property product_name() As String
    Get
      Return _product_name
    End Get
    Set(value As String)
      _product_name = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property cust_product_name() As String
    Get
      Return _cust_product_name
    End Get
    Set(value As String)
      _cust_product_name = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property product_ver() As String
    Get
      Return _product_ver
    End Get
    Set(value As String)
      _product_ver = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property product_desc() As String
    Get
      Return _product_desc
    End Get
    Set(value As String)
      _product_desc = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property family() As String
    Get
      Return _family
    End Get
    Set(value As String)
      _family = value
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
  Public Property sn_format() As String
    Get
      Return _sn_format
    End Get
    Set(value As String)
      _sn_format = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property sn_check() As Boolean
    Get
      Return _sn_check
    End Get
    Set(value As Boolean)
      _sn_check = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property port_num() As Integer
    Get
      Return _port_num
    End Get
    Set(value As Integer)
      _port_num = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property dwtilt_enable() As Boolean
    Get
      Return _dwtilt_enable
    End Get
    Set(value As Boolean)
      _dwtilt_enable = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property dwtilt_type() As Integer
    Get
      Return _dwtilt_type
    End Get
    Set(value As Integer)
      _dwtilt_type = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property dwtilt_num() As Integer
    Get
      Return _dwtilt_num
    End Get
    Set(value As Integer)
      _dwtilt_num = value
    End Set
  End Property

  ''' <summary>
  ''' id
  ''' </summary>
  Public Property dwtilt_mode() As String
    Get
      Return _dwtilt_mode
    End Get
    Set(value As String)
      _dwtilt_mode = value
    End Set
  End Property
  Public Property dwtilt_pn As String
    Get
      Return _dwtilt_pn
    End Get
    Set(value As String)
      _dwtilt_pn = value
    End Set
  End Property
  ''' <summary>
  ''' id
  ''' </summary>
  Public Property function_id() As Integer
    Get
      Return _function_id
    End Get
    Set(value As Integer)
      _function_id = value
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

	Public Overrides Function ToString() As String
		Return _product_name
	End Function

	Public Function CompareTo(value As product_main) As Boolean
		Try

			If _customer_id <> value.customer_id Then Return False
			If _product_name <> value.product_name Then Return False
			If _validity <> value.validity Then Return False
			If _port_num <> value.port_num Then Return False
			If _dwtilt_enable <> value.dwtilt_enable Then Return False
			If _dwtilt_type <> value.dwtilt_type Then Return False
			If _dwtilt_num <> value.dwtilt_num Then Return False
			If _dwtilt_mode <> value.dwtilt_mode Then Return False
			If _dwtilt_pn <> value.dwtilt_pn Then Return False

			Return True

		Catch ex As Exception
			Throw New Exception("product_main.CompareTo()::" & ex.Message)
		End Try

	End Function
End Class
