Imports System
Imports System.Collections.Generic
Imports FACTS

Public Class test_cable_mainManager
    Private _dal As New FACTS.DAL.test_cable_mainService
    Public Function Add(model As Model.test_cable_main) As Boolean
      Try

        Return _dal.Add(model)

      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.Add()::"& ex.Message)
      End Try
    End Function
   Public Function AddReturnId(model As Model.test_cable_main) As Integer
      Try

        Return _dal.AddReturnId(model)

      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.AddReturnId()::" & ex.Message)
      End Try
    End Function
  Public Function Delete(id As Integer) As Boolean
      Try
        Return _dal.Delete(id)
      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.Delete()::" & ex.Message)
      End Try
    End Function
   Public Function Update(model As Model.test_cable_main) As Boolean
      Try
        Return _dal.Update(model)
      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.Update()::" & ex.Message)
      End Try
    End Function
    Public Function SelectAll() As List(Of Model.test_cable_main)
      Try
        Return _dal.SelectAll()

      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.SelectAll()::" & ex.Message)
      End Try
    End Function
    Public Function SelectById(id As Integer) As Model.test_cable_main
       Try
        Return _dal.SelectById(id)
      Catch ex As Exception
        Throw New Exception("test_cable_main.BLL.SelectById()::" & ex.Message)
      End Try
    End Function
    Public Function SelectByCableSN(cable_serial_num As String) As FACTS.Model.test_cable_main
        Try
            Return _dal.SelectByCableSN(cable_serial_num)
        Catch ex As Exception
            Throw New Exception("test_cable_main.BLL.SelectByCableSN()::" & ex.Message)
        End Try
    End Function
End Class
