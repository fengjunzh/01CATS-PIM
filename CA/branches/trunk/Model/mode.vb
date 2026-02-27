Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'mode
<Serializable()>
Public Class mode
Private _id As Integer
Private _mode As String
Private _validity As Boolean
Private _function_id As Integer

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
    Public Property mode() As String
       Get
           Return _mode
       End Get
       Set(value As String)
          _mode = value
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
	Public Property function_id() As Integer
		Get
			Return _function_id
		End Get
		Set(value As Integer)
			_function_id = value
		End Set
	End Property

	Public Overrides Function ToString() As String
		Return _mode
	End Function
End Class
