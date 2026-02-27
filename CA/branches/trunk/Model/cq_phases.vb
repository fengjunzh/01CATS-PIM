Imports System.Xml.Serialization
Imports System
Public Class cq_phases
	Public mode_spec_id As Integer
	Public product_mode_id As Integer
	Public spec_main_id As Integer
	Public order_idx As Integer
	Public validity As Boolean
	Public validation_date As DateTime
	Public validation As Boolean
	Public phase_main_id As Integer
	Public phase As String
	Public function_id As Integer
	Public func_name As String
	Public class_id As Integer
	Public spec_main_model As New FACTS.Model.spec_main
	Public Overrides Function ToString() As String
        Return phase
    End Function

    Public Function CompareTo(value As cq_phases) As Boolean
		Try
			Return spec_main_model.CompareTo(value.spec_main_model)
		Catch ex As Exception
			Throw New Exception("cq_phases.Compare()::" & ex.Message)
		End Try

	End Function
End Class
