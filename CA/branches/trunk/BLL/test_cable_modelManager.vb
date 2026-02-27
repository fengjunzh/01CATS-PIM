Imports System
Imports System.Collections.Generic
Imports FACTS

Public Class test_cable_modelManager
    Private _dal As New FACTS.DAL.test_cable_modelService
    Public Function Add(model As Model.test_cable_model) As Boolean
      Try

        Return _dal.Add(model)

      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.Add()::"& ex.Message)
      End Try
    End Function
   Public Function AddReturnId(model As Model.test_cable_model) As Integer
      Try

        Return _dal.AddReturnId(model)

      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.AddReturnId()::" & ex.Message)
      End Try
    End Function
  Public Function Delete(id As Integer) As Boolean
      Try
        Return _dal.Delete(id)
      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.Delete()::" & ex.Message)
      End Try
    End Function
   Public Function Update(model As Model.test_cable_model) As Boolean
      Try
        Return _dal.Update(model)
      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.Update()::" & ex.Message)
      End Try
    End Function
    Public Function SelectAll() As List(Of Model.test_cable_model)
      Try
        Return _dal.SelectAll()

      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.SelectAll()::" & ex.Message)
      End Try
    End Function
    Public Function SelectById(id As Integer) As Model.test_cable_model
       Try
        Return _dal.SelectById(id)
      Catch ex As Exception
        Throw New Exception("test_cable_model.BLL.SelectById()::" & ex.Message)
      End Try
    End Function

  End Class
