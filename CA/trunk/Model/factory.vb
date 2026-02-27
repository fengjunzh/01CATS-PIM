Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'factory
<Serializable()>
Public Class factory
Private _id As Integer
Private _name As String
Private _code As String
Private _descr As String

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
    Public Property name() As String
       Get
           Return _name
       End Get
       Set(value As String)
          _name = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property code() As String
       Get
           Return _code
       End Get
       Set(value As String)
          _code = value
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
End Class
