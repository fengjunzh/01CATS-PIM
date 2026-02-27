Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Partial Public Class test_cable_mainService
    Public Function Add(model As FACTS.Model.test_cable_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@cable_model_id", model.cable_model_id),
        New SqlParameter("@cable_serial_num", model.cable_serial_num),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@register_date_time", model.register_date_time),
        New SqlParameter("@descr", model.descr),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_main_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.test_cable_main) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@cable_model_id", model.cable_model_id),
        New SqlParameter("@cable_serial_num", model.cable_serial_num),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@register_date_time", model.register_date_time),
        New SqlParameter("@descr", model.descr),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }
            Return SqlHelper.runProcedureWithReturnValue("proc_test_cable_main_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_main_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As FACTS.Model.test_cable_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@cable_model_id", model.cable_model_id),
        New SqlParameter("@cable_serial_num", model.cable_serial_num),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@register_date_time", model.register_date_time),
        New SqlParameter("@descr", model.descr),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_main_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.test_cable_main)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.test_cable_main
            Dim mlist As New List(Of FACTS.Model.test_cable_main)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_main_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.test_cable_main
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.test_cable_main
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.test_cable_main
            Dim parameters As SqlParameter() = {
                        New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_main_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByCableSN(cable_serial_num As String) As FACTS.Model.test_cable_main
        Try
            Dim dt As DataTable

            dt = SelectByWhere("cable_serial_num='" & cable_serial_num & "'").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            Return DataRowToModel(dt.Rows(0))


        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.SelectByCableSN()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
            New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_test_cable_main_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.test_cable_main
        Try
            Dim model As New FACTS.Model.test_cable_main
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("cable_model_id") Then If Not (IsDBNull(row("cable_model_id"))) Then model.cable_model_id = Convert.ToInt32(row("cable_model_id"))
            If row.Table.Columns.Contains("cable_serial_num") Then If Not (IsDBNull(row("cable_serial_num"))) Then model.cable_serial_num = Convert.ToString(row("cable_serial_num"))
            If row.Table.Columns.Contains("test_count") Then If Not (IsDBNull(row("test_count"))) Then model.test_count = Convert.ToInt32(row("test_count"))
            If row.Table.Columns.Contains("register_date_time") Then If Not (IsDBNull(row("register_date_time"))) Then model.register_date_time = Convert.ToDateTime(row("register_date_time"))
            If row.Table.Columns.Contains("descr") Then If Not (IsDBNull(row("descr"))) Then model.descr = Convert.ToString(row("descr"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToInt32(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("test_cable_main.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
