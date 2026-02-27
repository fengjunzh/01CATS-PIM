Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class spec_mainService
    Public Function Add(model As FACTS.Model.spec_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
          New SqlParameter("@phase_main_id", model.phase_main_id),
          New SqlParameter("@only_test", model.only_test),
          New SqlParameter("@manual_enable", model.manual_enable),
          New SqlParameter("@allow_cust_limit", model.allow_cust_limit),
          New SqlParameter("@script", model.script),
          New SqlParameter("@invalidate_phase_ids", model.invalidate_phase_ids),
          New SqlParameter("@spec_desc", model.spec_desc),
          New SqlParameter("@spec_revision", model.spec_revision),
          New SqlParameter("@spec_version", model.spec_version),
          New SqlParameter("@change_note", model.change_note),
          New SqlParameter("@validity", model.validity),
          New SqlParameter("@validation_date", model.validation_date),
          New SqlParameter("@descr", model.descr),
          New SqlParameter("@gen1", model.gen1),
          New SqlParameter("@gen2", model.gen2),
          New SqlParameter("@gen3", model.gen3)
        }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_main_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("spec_main.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.spec_main) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@phase_main_id", model.phase_main_id),
        New SqlParameter("@only_test", model.only_test),
        New SqlParameter("@manual_enable", model.manual_enable),
        New SqlParameter("@allow_cust_limit", model.allow_cust_limit),
        New SqlParameter("@script", model.script),
        New SqlParameter("@invalidate_phase_ids", model.invalidate_phase_ids),
        New SqlParameter("@spec_desc", model.spec_desc),
        New SqlParameter("@spec_revision", model.spec_revision),
        New SqlParameter("@spec_version", model.spec_version),
        New SqlParameter("@change_note", model.change_note),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@validation_date", model.validation_date),
        New SqlParameter("@descr", model.descr),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }
            Return SqlHelper.runProcedureWithReturnValue("proc_spec_main_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("spec_main.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_main_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("spec_main.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As FACTS.Model.spec_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@phase_main_id", model.phase_main_id),
        New SqlParameter("@only_test", model.only_test),
        New SqlParameter("@manual_enable", model.manual_enable),
        New SqlParameter("@allow_cust_limit", model.allow_cust_limit),
        New SqlParameter("@script", model.script),
        New SqlParameter("@invalidate_phase_ids", model.invalidate_phase_ids),
        New SqlParameter("@spec_desc", model.spec_desc),
        New SqlParameter("@spec_revision", model.spec_revision),
        New SqlParameter("@spec_version", model.spec_version),
        New SqlParameter("@change_note", model.change_note),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@validation_date", model.validation_date),
        New SqlParameter("@descr", model.descr),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_mainUpdate", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("spec_main.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.spec_main)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.spec_main
            Dim mlist As New List(Of FACTS.Model.spec_main)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_spec_main_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.spec_main
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("spec_main.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.spec_main
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.spec_main
            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_spec_main_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("spec_main.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_spec_main_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("spec_main.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.spec_main
        Try
            Dim model As New FACTS.Model.spec_main
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("phase_main_id") Then If Not (IsDBNull(row("phase_main_id"))) Then model.phase_main_id = Convert.ToInt32(row("phase_main_id"))
            If row.Table.Columns.Contains("only_test") Then If Not (IsDBNull(row("only_test"))) Then model.only_test = Convert.ToBoolean(row("only_test"))
            If row.Table.Columns.Contains("manual_enable") Then If Not (IsDBNull(row("manual_enable"))) Then model.manual_enable = Convert.ToBoolean(row("manual_enable"))
            If row.Table.Columns.Contains("allow_cust_limit") Then If Not (IsDBNull(row("allow_cust_limit"))) Then model.allow_cust_limit = Convert.ToBoolean(row("allow_cust_limit"))
            If row.Table.Columns.Contains("script") Then If Not (IsDBNull(row("script"))) Then model.script = Convert.ToString(row("script"))
            If row.Table.Columns.Contains("invalidate_phase_ids") Then If Not (IsDBNull(row("invalidate_phase_ids"))) Then model.invalidate_phase_ids = Convert.ToString(row("invalidate_phase_ids"))
            If row.Table.Columns.Contains("spec_desc") Then If Not (IsDBNull(row("spec_desc"))) Then model.spec_desc = Convert.ToString(row("spec_desc"))
            If row.Table.Columns.Contains("spec_revision") Then If Not (IsDBNull(row("spec_revision"))) Then model.spec_revision = Convert.ToString(row("spec_revision"))
            If row.Table.Columns.Contains("spec_version") Then If Not (IsDBNull(row("spec_version"))) Then model.spec_version = Convert.ToString(row("spec_version"))
            If row.Table.Columns.Contains("change_note") Then If Not (IsDBNull(row("change_note"))) Then model.change_note = Convert.ToString(row("change_note"))
            If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If row.Table.Columns.Contains("validation_date") Then If Not (IsDBNull(row("validation_date"))) Then model.validation_date = Convert.ToDateTime(row("validation_date"))
            If row.Table.Columns.Contains("descr") Then If Not (IsDBNull(row("descr"))) Then model.descr = Convert.ToString(row("descr"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToString(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("spec_main.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
