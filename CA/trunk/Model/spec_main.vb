Imports System
Imports System.Data
Imports System.Text
Imports System.Collections.Generic

'spec_main
<Serializable()>
Public Class spec_main
Private _id As Integer
Private _phase_main_id As Integer
Private _only_test As Boolean
Private _manual_enable As Boolean
Private _allow_cust_limit As Boolean
Private _script As String
Private _invalidate_phase_ids As String
Private _spec_desc As String
Private _spec_revision As String
Private _spec_version As String
Private _change_note As String
Private _validity As Boolean
Private _validation_date As DateTime
Private _descr As String
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
    Public Property only_test() As Boolean
       Get
           Return _only_test
       End Get
       Set(value As Boolean)
          _only_test = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property manual_enable() As Boolean
       Get
           Return _manual_enable
       End Get
       Set(value As Boolean)
          _manual_enable = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property allow_cust_limit() As Boolean
       Get
           Return _allow_cust_limit
       End Get
       Set(value As Boolean)
          _allow_cust_limit = value
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
    Public Property invalidate_phase_ids() As String
       Get
           Return _invalidate_phase_ids
       End Get
       Set(value As String)
          _invalidate_phase_ids = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property spec_desc() As String
       Get
           Return _spec_desc
       End Get
       Set(value As String)
          _spec_desc = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property spec_revision() As String
       Get
           Return _spec_revision
       End Get
       Set(value As String)
          _spec_revision = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property spec_version() As String
       Get
           Return _spec_version
       End Get
       Set(value As String)
          _spec_version = value
       End Set
    End Property

''' <summary>
''' id
''' </summary>
    Public Property change_note() As String
       Get
           Return _change_note
       End Get
       Set(value As String)
          _change_note = value
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

	Public Function CompareTo(value As spec_main) As Boolean
		Try
			If validity <> value.validity Then Return False
			If gen1 <> value.gen1 Then Return False
			If gen2 <> value.gen2 Then Return False

			Return True

		Catch ex As Exception
			Throw New Exception("spec_main.CompareTo()" & ex.Message)
		End Try

	End Function
End Class
