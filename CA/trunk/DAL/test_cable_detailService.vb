Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class test_cable_detailService
    Public Function Add(model As FACTS.Model.test_cable_detail) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@test_cable_main_id", model.test_cable_main_id),
        New SqlParameter("@product_serial_num", model.product_serial_num),
        New SqlParameter("@phase_main_id", model.phase_main_id),
        New SqlParameter("@controller_name", model.controller_name),
        New SqlParameter("@login_name", model.login_name),
        New SqlParameter("@factory", model.factory),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@date_time", model.date_time),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_detail_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.test_cable_detail) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@test_cable_main_id", model.test_cable_main_id),
        New SqlParameter("@product_serial_num", model.product_serial_num),
        New SqlParameter("@phase_main_id", model.phase_main_id),
        New SqlParameter("@controller_name", model.controller_name),
        New SqlParameter("@login_name", model.login_name),
        New SqlParameter("@factory", model.factory),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@date_time", model.date_time),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }
            Return SqlHelper.runProcedureWithReturnValue("proc_test_cable_detail_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_detail_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
          Throw New Exception("test_cable_detail.DAL.Delete()::" & ex.Message)
        End Try
      End Function
    Public Function Update(model As FACTS.Model.test_cable_detail) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@test_cable_main_id", model.test_cable_main_id),
        New SqlParameter("@product_serial_num", model.product_serial_num),
        New SqlParameter("@phase_main_id", model.phase_main_id),
        New SqlParameter("@controller_name", model.controller_name),
        New SqlParameter("@login_name", model.login_name),
        New SqlParameter("@factory", model.factory),
        New SqlParameter("@test_count", model.test_count),
        New SqlParameter("@date_time", model.date_time),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
            }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_test_cable_detail_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.test_cable_detail)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.test_cable_detail
            Dim mlist As New List(Of FACTS.Model.test_cable_detail)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_detail_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.test_cable_detail
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.test_cable_detail
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.test_cable_detail
            Dim parameters As SqlParameter() = {
                        New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_test_cable_detail_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
    Try
      Dim ds As DataSet

      Dim parameters As SqlParameter() = {
            New SqlParameter("@where", whereString) }

      ds = SqlHelper.runProcedureWithDataset("proc_test_cable_detail_SelectByWhere", parameters, "dt")

      Return ds

    Catch ex As Exception
          Throw New Exception("test_cable_detail.DAL.SelectByWhere()::" & ex.Message)
    End Try
  End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.test_cable_detail
        Try
            Dim model As New FACTS.Model.test_cable_detail
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("test_cable_main_id") Then If Not (IsDBNull(row("test_cable_main_id"))) Then model.test_cable_main_id = Convert.ToInt32(row("test_cable_main_id"))
            If row.Table.Columns.Contains("product_serial_num") Then If Not (IsDBNull(row("product_serial_num"))) Then model.product_serial_num = Convert.ToString(row("product_serial_num"))
            If row.Table.Columns.Contains("phase_main_id") Then If Not (IsDBNull(row("phase_main_id"))) Then model.phase_main_id = Convert.ToInt32(row("phase_main_id"))
            If row.Table.Columns.Contains("controller_name") Then If Not (IsDBNull(row("controller_name"))) Then model.controller_name = Convert.ToString(row("controller_name"))
            If row.Table.Columns.Contains("login_name") Then If Not (IsDBNull(row("login_name"))) Then model.login_name = Convert.ToString(row("login_name"))
            If row.Table.Columns.Contains("factory") Then If Not (IsDBNull(row("factory"))) Then model.factory = Convert.ToString(row("factory"))
            If row.Table.Columns.Contains("test_count") Then If Not (IsDBNull(row("test_count"))) Then model.test_count = Convert.ToInt32(row("test_count"))
            If row.Table.Columns.Contains("date_time") Then If Not (IsDBNull(row("date_time"))) Then model.date_time = Convert.ToDateTime(row("date_time"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToInt32(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("test_cable_detail.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
