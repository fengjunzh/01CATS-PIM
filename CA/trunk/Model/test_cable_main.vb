Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'test_cable_main
<Serializable()>
Public Class test_cable_main
Private _id As Integer
Private _cable_model_id As Integer
Private _cable_serial_num As String
Private _test_count As Integer
Private _register_date_time As DateTime
Private _descr As String
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
    Public Property cable_model_id() As Integer
       Get
           Return _cable_model_id
       End Get
       Set(value As Integer)
          _cable_model_id = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property cable_serial_num() As String
       Get
           Return _cable_serial_num
       End Get
       Set(value As String)
          _cable_serial_num = value
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
    Public Property register_date_time() As DateTime
       Get
           Return _register_date_time
       End Get
       Set(value As DateTime)
          _register_date_time = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property descr() As String
       Get
           Return _descr
       End Get
       Set(value As String)
          _descr = value
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
