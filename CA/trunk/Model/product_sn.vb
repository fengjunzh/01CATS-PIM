Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'product_sn
<Serializable()>
Public Class product_sn
Private _id As Integer
Private _product_main_id As Integer
Private _product_serial_num As String
Private _register_date As DateTime
Private _validity As Boolean
Private _sn_status_id As Integer
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
    Public Property product_main_id() As Integer
       Get
           Return _product_main_id
       End Get
       Set(value As Integer)
          _product_main_id = value
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
    Public Property register_date() As DateTime
       Get
           Return _register_date
       End Get
       Set(value As DateTime)
          _register_date = value
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
    Public Property sn_status_id() As Integer
       Get
           Return _sn_status_id
       End Get
       Set(value As Integer)
          _sn_status_id = value
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
End Class
