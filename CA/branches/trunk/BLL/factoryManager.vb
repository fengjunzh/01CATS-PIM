Imports System
Imports System.Collections.Generic
Imports FACTS

Public Class factoryManager

    Private _dal As New FACTS.DAL.factoryService
    Public Function Add(model As Model.factory) As Boolean
        Try

            Return _dal.Add(model)

        Catch ex As Exception
            Throw New Exception("factory.BLL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As Model.factory) As Integer
        Try

            Return _dal.AddReturnId(model)

        Catch ex As Exception
            Throw New Exception("factory.BLL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Return _dal.Delete(id)
        Catch ex As Exception
            Throw New Exception("factory.BLL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As Model.factory) As Boolean
        Try
            Return _dal.Update(model)
        Catch ex As Exception
            Throw New Exception("factory.BLL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of Model.factory)
        Try
            Return _dal.SelectAll()

        Catch ex As Exception
            Throw New Exception("factory.BLL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As Model.factory
        Try
            Return _dal.SelectById(id)
        Catch ex As Exception
            Throw New Exception("factory.BLL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByFactory(factory As String) As Model.factory
        Try
            Return _dal.SelectByFactory(factory)
        Catch ex As Exception
            Throw New Exception("factory.BLL.SelectByFactory()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByName(factoryName As String) As Model.factory
        Try
            Return _dal.SelectByName(factoryName)
        Catch ex As Exception
            Throw New Exception("factory.BLL.SelectByName()::" & ex.Message)
        End Try

    End Function
End Class
