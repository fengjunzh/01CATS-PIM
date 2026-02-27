Option Strict Off
Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Public Class modeService

    Public Function Add(model As FACTS.Model.mode) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
    New SqlParameter("@mode", model.mode),
    New SqlParameter("@validity", model.validity),
    New SqlParameter("@function_id", model.function_id)
  }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_mode_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("mode.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As FACTS.Model.mode) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
  New SqlParameter("@mode", model.mode),
  New SqlParameter("@validity", model.validity),
  New SqlParameter("@function_id", model.function_id)
}
            Return SqlHelper.runProcedureWithReturnValue("proc_mode_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("mode.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_mode_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("mode.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As FACTS.Model.mode) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
  New SqlParameter("@id", model.id),
  New SqlParameter("@mode", model.mode),
  New SqlParameter("@validity", model.validity),
  New SqlParameter("@function_id", model.function_id)
}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_mode_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("mode.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of FACTS.Model.mode)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.mode
            Dim mlist As New List(Of FACTS.Model.mode)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_mode_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New FACTS.Model.mode
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("mode.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As FACTS.Model.mode
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.mode
            Dim parameters As SqlParameter() = {
                              New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_mode_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("mode.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByModeName(mode_name As String) As FACTS.Model.mode
        Try
            Dim dt As DataTable
            Dim model As New FACTS.Model.mode

            dt = SelectByWhere("mode = '" & mode_name & "'").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("mode.DAL.SelectByModeName()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_mode_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("mode.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.mode
        Try
            Dim model As New FACTS.Model.mode
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("mode") Then If Not (IsDBNull(row("mode"))) Then model.mode = Convert.ToString(row("mode"))
            If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If row.Table.Columns.Contains("function_id") Then If Not (IsDBNull(row("function_id"))) Then model.function_id = Convert.ToInt32(row("function_id"))
            Return model
        Catch ex As Exception
            Throw New Exception("mode.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
