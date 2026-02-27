Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class factoryService

    Public Function Add(model As FACTS.Model.factory) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
          New SqlParameter("@name", model.name),
          New SqlParameter("@code", model.code),
          New SqlParameter("@descr", model.descr)
        }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_factory_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("factory.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.factory) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@name", model.name),
        New SqlParameter("@code", model.code),
        New SqlParameter("@descr", model.descr)
      }
            Return SqlHelper.runProcedureWithReturnValue("proc_factory_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("factory.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_factory_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("factory.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As FACTS.Model.factory) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@name", model.name),
        New SqlParameter("@code", model.code),
        New SqlParameter("@descr", model.descr)
      }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_factory_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("factory.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.factory)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.factory
            Dim mlist As New List(Of FACTS.Model.factory)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_factory_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.factory
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("factory.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.factory
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.factory
            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_factory_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("factory.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByFactory(factory As String) As FACTS.Model.factory
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.factory

            dt = SelectByWhere("name = '" & factory & "'").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing
            If dt.Rows.Count > 1 Then Throw New Exception("find more than one factory")

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("factory.DAL.SelectByFactory()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByName(factoryName As String) As Model.factory
        Try
            Dim dt As DataTable
            dt = SelectByWhere("name='" & factoryName & "'").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            Return DataRowToModel(dt.Rows(0))

        Catch ex As Exception
            Throw New Exception("factory.DAL.SelectByName()::" & ex.Message)
        End Try

    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_factory_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("factory.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.factory
        Try
            Dim model As New FACTS.Model.factory
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("name") Then If Not (IsDBNull(row("name"))) Then model.name = Convert.ToString(row("name"))
            If row.Table.Columns.Contains("code") Then If Not (IsDBNull(row("code"))) Then model.code = Convert.ToString(row("code"))
            If row.Table.Columns.Contains("descr") Then If Not (IsDBNull(row("descr"))) Then model.descr = Convert.ToString(row("descr"))
            Return model
        Catch ex As Exception
            Throw New Exception("factory.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
