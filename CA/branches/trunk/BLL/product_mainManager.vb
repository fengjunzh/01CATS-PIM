Imports System
Imports System.Collections.Generic
Imports CATS

Public Class product_mainManager

  Private _dal As New CATS.DAL.product_mainService
  Public Function Add(model As Model.product_main) As Boolean
    Try

      Return _dal.Add(model)

    Catch ex As Exception
      Throw New Exception("product_main.BLL.Add()::" & ex.Message)
    End Try
  End Function
  Public Function AddReturnId(model As Model.product_main) As Integer
    Try

      Return _dal.AddReturnId(model)

    Catch ex As Exception
      Throw New Exception("product_main.BLL.AddReturnId()::" & ex.Message)
    End Try
  End Function
  Public Function Delete(id As Integer) As Boolean
    Try
      Return _dal.Delete(id)
    Catch ex As Exception
      Throw New Exception("product_main.BLL.Delete()::" & ex.Message)
    End Try
  End Function
  Public Function Update(model As Model.product_main) As Boolean
    Try
      Return _dal.Update(model)
    Catch ex As Exception
      Throw New Exception("product_main.BLL.Update()::" & ex.Message)
    End Try
  End Function
    Public Function SelectAll() As List(Of Model.product_main)
        Try
            Return _dal.SelectAll()

        Catch ex As Exception
            Throw New Exception("product_main.BLL.SelectAll()::" & ex.Message)
        End Try
    End Function

    Public Function SelectAllBySeries(series As String) As List(Of Model.product_main)
        Try
            Return _dal.SelectAllBySeries(series)

        Catch ex As Exception
            Throw New Exception("product_main.BLL.SelectAllBySeries()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As Model.product_main
    Try
      Return _dal.SelectById(id)
    Catch ex As Exception
      Throw New Exception("product_main.BLL.SelectById()::" & ex.Message)
    End Try
  End Function
    Public Function SelectByProductName(product_name As String) As Model.product_main
        Try
            Return _dal.SelectByProductName(product_name)
        Catch ex As Exception
            Throw New Exception("product_main.BLL.SelectByProductName()::" & ex.Message)
        End Try

    End Function
    Public Function SelectProductByGroup(prodGroup As String) As List(Of Model.product_main)
        Try
            Return _dal.SelectProductByGroup(prodGroup)

        Catch ex As Exception
            Throw New Exception("product_main.BLL.SelectProductByGroup()::" & ex.Message)
        End Try
    End Function
End Class
