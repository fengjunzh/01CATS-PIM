Imports System
Imports System.Collections.Generic
Imports CATS

Public Class product_snManager

  Private _dal As New CATS.DAL.product_snService
  Public Function Add(model As Model.product_sn) As Boolean
    Try

      Return _dal.Add(model)

    Catch ex As Exception
      Throw New Exception("product_sn.BLL.Add()::" & ex.Message)
    End Try
  End Function
  Public Function AddReturnId(model As Model.product_sn) As Integer
    Try

      Return _dal.AddReturnId(model)

    Catch ex As Exception
      Throw New Exception("product_sn.BLL.AddReturnId()::" & ex.Message)
    End Try
  End Function
  Public Function Delete(id As Integer) As Boolean
    Try
      Return _dal.Delete(id)
    Catch ex As Exception
      Throw New Exception("product_sn.BLL.Delete()::" & ex.Message)
    End Try
  End Function
  Public Function Update(model As Model.product_sn) As Boolean
    Try
      Return _dal.Update(model)
    Catch ex As Exception
      Throw New Exception("product_sn.BLL.Update()::" & ex.Message)
    End Try
  End Function
  Public Function SelectAll() As List(Of Model.product_sn)
    Try
      Return _dal.SelectAll()

    Catch ex As Exception
      Throw New Exception("product_sn.BLL.SelectAll()::" & ex.Message)
    End Try
  End Function
  Public Function SelectById(id As Integer) As Model.product_sn
    Try
      Return _dal.SelectById(id)
    Catch ex As Exception
      Throw New Exception("product_sn.BLL.SelectById()::" & ex.Message)
    End Try
  End Function
  Public Function SelectBySerialNum(product_serial_num As String) As Model.product_sn
    Try
      Return _dal.SelectBySerialNum(product_serial_num)
    Catch ex As Exception
      Throw New Exception("product_sn.BLL.SelectBySerialNum()::" & ex.Message)
    End Try

  End Function
End Class
