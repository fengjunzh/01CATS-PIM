Imports System
Imports System.Collections.Generic
Imports CATS

Public Class spec_detailManager
	Private _dal As New FACTS.DAL.spec_detailService
	Public Function Add(model As Model.spec_detail) As Boolean
		Try

			Return _dal.Add(model)

		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.Add()::" & ex.Message)
		End Try
	End Function
	Public Function AddReturnId(model As Model.spec_detail) As Integer
		Try

			Return _dal.AddReturnId(model)

		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.AddReturnId()::" & ex.Message)
		End Try
	End Function
	Public Function Delete(id As Integer) As Boolean
		Try
			Return _dal.Delete(id)
		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.Delete()::" & ex.Message)
		End Try
	End Function
	Public Function Update(model As Model.spec_detail) As Boolean
		Try
			Return _dal.Update(model)
		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.Update()::" & ex.Message)
		End Try
	End Function
	Public Function SelectAll() As List(Of Model.spec_detail)
		Try
			Return _dal.SelectAll()

		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.SelectAll()::" & ex.Message)
		End Try
	End Function
	Public Function SelectById(id As Integer) As Model.spec_detail
		Try
			Return _dal.SelectById(id)
		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.SelectById()::" & ex.Message)
		End Try
	End Function
	Public Function SelectAllByGroupMainId(group_main_id As Integer) As List(Of Model.spec_detail)
		Try
			Return _dal.SelectAllByGroupMainId(group_main_id)
		Catch ex As Exception
			Throw New Exception("spec_detail.BLL.SelectAllByGroupMainId()::" & ex.Message)
		End Try
	End Function
End Class
