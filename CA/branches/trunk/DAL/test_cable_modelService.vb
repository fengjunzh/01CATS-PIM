Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class test_cable_modelService
    Public Function Add(model As FACTS.Model.test_cable_model) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
          New SqlParameter("@id", model.id),
          New SqlParameter("@model_name", model.model_name),
          New SqlParameter("@tolerant_count", model.tolerant_count),
          New SqlParameter("@validity", model.validity),
          New SqlParameter("@validation", model.validation),
          New SqlParameter("@date_time", model.date_time),
          New SqlParameter("@gen1", model.gen1),
          New SqlParameter("@gen2", model.gen2),
          New SqlParameter("@gen3", model.gen3)
              }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_model_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.test_cable_model) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@model_name", model.model_name),
        New SqlParameter("@tolerant_count", model.tolerant_count),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@validation", model.validation),
        New SqlParameter("@date_time", model.date_time),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }
            Return SqlHelper.runProcedureWithReturnValue("proc_test_cable_model_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_model_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As FACTS.Model.test_cable_model) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@model_name", model.model_name),
        New SqlParameter("@tolerant_count", model.tolerant_count),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@validation", model.validation),
        New SqlParameter("@date_time", model.date_time),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_model_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.test_cable_model)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.test_cable_model
            Dim mlist As New List(Of FACTS.Model.test_cable_model)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_model_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.test_cable_model
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.test_cable_model
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.test_cable_model
            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_model_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_test_cable_model_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.test_cable_model
        Try
            Dim model As New FACTS.Model.test_cable_model
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("model_name") Then If Not (IsDBNull(row("model_name"))) Then model.model_name = Convert.ToString(row("model_name"))
            If row.Table.Columns.Contains("tolerant_count") Then If Not (IsDBNull(row("tolerant_count"))) Then model.tolerant_count = Convert.ToInt32(row("tolerant_count"))
            If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If row.Table.Columns.Contains("validation") Then If Not (IsDBNull(row("validation"))) Then model.validation = Convert.ToBoolean(row("validation"))
            If row.Table.Columns.Contains("date_time") Then If Not (IsDBNull(row("date_time"))) Then model.date_time = Convert.ToDateTime(row("date_time"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToInt32(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("test_cable_model.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
