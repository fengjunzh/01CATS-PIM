Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'test_cable_model
<Serializable()>
Public Class test_cable_model
Private _id As Integer
Private _model_name As String
Private _tolerant_count As Integer
Private _validity As Boolean
Private _validation As Boolean
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
    Public Property model_name() As String
       Get
           Return _model_name
       End Get
       Set(value As String)
          _model_name = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property tolerant_count() As Integer
       Get
           Return _tolerant_count
       End Get
       Set(value As Integer)
          _tolerant_count = value
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
    Public Property validation() As Boolean
       Get
           Return _validation
       End Get
       Set(value As Boolean)
          _validation = value
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
