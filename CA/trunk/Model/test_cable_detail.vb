Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'test_cable_detail
<Serializable()>
Public Class test_cable_detail
Private _id As Integer
Private _test_cable_main_id As Integer
Private _product_serial_num As String
Private _phase_main_id As Integer
Private _controller_name As String
Private _login_name As String
Private _factory As String
Private _test_count As Integer
Private _date_time As DateTime
Private _gen1 As Integer
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
    Public Property test_cable_main_id() As Integer
       Get
           Return _test_cable_main_id
       End Get
       Set(value As Integer)
          _test_cable_main_id = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property product_serial_num() As String
       Get
           Return _product_serial_num
       End Get
       Set(value As String)
          _product_serial_num = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property phase_main_id() As Integer
       Get
           Return _phase_main_id
       End Get
       Set(value As Integer)
          _phase_main_id = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property controller_name() As String
       Get
           Return _controller_name
       End Get
       Set(value As String)
          _controller_name = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property login_name() As String
       Get
           Return _login_name
       End Get
       Set(value As String)
          _login_name = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property factory() As String
       Get
           Return _factory
       End Get
       Set(value As String)
          _factory = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property test_count() As Integer
       Get
           Return _test_count
       End Get
       Set(value As Integer)
          _test_count = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property date_time() As DateTime
       Get
           Return _date_time
       End Get
       Set(value As DateTime)
          _date_time = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property gen1() As Integer
       Get
           Return _gen1
       End Get
       Set(value As Integer)
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
End Class
