Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Public Class cq_groupsService
    Public Function SelectAllBySpecMainId(spec_main_id As Integer) As List(Of FACTS.Model.cq_groups)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.cq_groups
            Dim mlist As New List(Of FACTS.Model.cq_groups)

            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@spec_main_id", spec_main_id)}

            dt = SqlHelper.runProcedureWithDataset("proc_cq_groups_Select", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_groups.DAL.SelectAllBySpecMainId()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.cq_groups
        Try
            Dim model As New FACTS.Model.cq_groups
            If IsNothing(row) Then Return Nothing
            If Not (IsDBNull(row("spec_main_id"))) Then model.spec_main_id = Convert.ToInt32(row("spec_main_id"))
            If Not (IsDBNull(row("group_main_id"))) Then model.group_main_id = Convert.ToInt32(row("group_main_id"))
            If Not (IsDBNull(row("order_idx"))) Then model.order_idx = Convert.ToInt32(row("order_idx"))
            If Not (IsDBNull(row("group_id"))) Then model.group_id = Convert.ToInt32(row("group_id"))
            If Not (IsDBNull(row("group_name"))) Then model.group_name = Convert.ToString(row("group_name"))
            If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If Not (IsDBNull(row("validation_date"))) Then model.validation_date = Convert.ToDateTime(row("validation_date"))

            Return model

        Catch ex As Exception
            Throw New Exception("cq_groups.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function

End Class
