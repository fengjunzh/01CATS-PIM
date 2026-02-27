Imports System
Imports System.Collections.Generic

Public Class test_cable_detailManager
    Private _dal As New FACTS.DAL.test_cable_detailService
    Public Function Add(model As Model.test_cable_detail) As Boolean
      Try

        Return _dal.Add(model)

      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.Add()::"& ex.Message)
      End Try
    End Function
   Public Function AddReturnId(model As Model.test_cable_detail) As Integer
      Try

        Return _dal.AddReturnId(model)

      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.AddReturnId()::" & ex.Message)
      End Try
    End Function
  Public Function Delete(id As Integer) As Boolean
      Try
        Return _dal.Delete(id)
      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.Delete()::" & ex.Message)
      End Try
    End Function
   Public Function Update(model As Model.test_cable_detail) As Boolean
      Try
        Return _dal.Update(model)
      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.Update()::" & ex.Message)
      End Try
    End Function
    Public Function SelectAll() As List(Of Model.test_cable_detail)
      Try
        Return _dal.SelectAll()

      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.SelectAll()::" & ex.Message)
      End Try
    End Function
    Public Function SelectById(id As Integer) As Model.test_cable_detail
       Try
        Return _dal.SelectById(id)
      Catch ex As Exception
        Throw New Exception("test_cable_detail.BLL.SelectById()::" & ex.Message)
      End Try
    End Function

  End Class
