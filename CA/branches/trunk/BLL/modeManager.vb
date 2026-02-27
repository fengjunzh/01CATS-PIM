Imports System
Imports System.Collections.Generic
Imports FACTS

Public Class modeManager

    Private _dal As New FACTS.DAL.modeService
    Public Function Add(model As Model.mode) As Boolean
    Try

      Return _dal.Add(model)

    Catch ex As Exception
      Throw New Exception("mode.BLL.Add()::" & ex.Message)
    End Try
  End Function
  Public Function AddReturnId(model As Model.mode) As Integer
    Try

      Return _dal.AddReturnId(model)

    Catch ex As Exception
      Throw New Exception("mode.BLL.AddReturnId()::" & ex.Message)
    End Try
  End Function
  Public Function Delete(id As Integer) As Boolean
    Try
      Return _dal.Delete(id)
    Catch ex As Exception
      Throw New Exception("mode.BLL.Delete()::" & ex.Message)
    End Try
  End Function
  Public Function Update(model As Model.mode) As Boolean
    Try
      Return _dal.Update(model)
    Catch ex As Exception
      Throw New Exception("mode.BLL.Update()::" & ex.Message)
    End Try
  End Function
  Public Function SelectAll() As List(Of Model.mode)
    Try
      Return _dal.SelectAll()

    Catch ex As Exception
      Throw New Exception("mode.BLL.SelectAll()::" & ex.Message)
    End Try
  End Function
  Public Function SelectById(id As Integer) As Model.mode
    Try
      Return _dal.SelectById(id)
    Catch ex As Exception
      Throw New Exception("mode.BLL.SelectById()::" & ex.Message)
    End Try
  End Function
    Public Function SelectByModeName(mode_name As String) As FACTS.Model.mode
        Try
            Return _dal.SelectByModeName(mode_name)
        Catch ex As Exception
            Throw New Exception("mode.BLL.SelectByModeName()::" & ex.Message)
        End Try
    End Function
End Class
