Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class product_snService

    Public Function Add(model As CATS.Model.product_sn) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
          New SqlParameter("@product_main_id", model.product_main_id),
          New SqlParameter("@product_serial_num", model.product_serial_num),
          New SqlParameter("@register_date", model.register_date),
          New SqlParameter("@validity", model.validity),
          New SqlParameter("@sn_status_id", model.sn_status_id),
          New SqlParameter("@gen1", model.gen1),
          New SqlParameter("@gen2", model.gen2),
          New SqlParameter("@gen3", model.gen3)
        }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_sn_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As CATS.Model.product_sn) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@product_main_id", model.product_main_id),
        New SqlParameter("@product_serial_num", model.product_serial_num),
        New SqlParameter("@register_date", model.register_date),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@sn_status_id", model.sn_status_id),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }
            Return SqlHelper.runProcedureWithReturnValue("proc_product_sn_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("product_sn.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_sn_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As CATS.Model.product_sn) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@product_main_id", model.product_main_id),
        New SqlParameter("@product_serial_num", model.product_serial_num),
        New SqlParameter("@register_date", model.register_date),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@sn_status_id", model.sn_status_id),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_sn_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("product_sn.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of CATS.Model.product_sn)
        Try
            Dim dt As DataTable
            Dim model As CATS.Model.product_sn
            Dim mlist As New List(Of CATS.Model.product_sn)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_product_sn_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New CATS.Model.product_sn
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As CATS.Model.product_sn
        Try
            Dim dt As DataTable
            Dim model As New CATS.Model.product_sn
            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_product_sn_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_product_sn_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function SelectBySerialNum(product_serial_num As String) As Model.product_sn
        Try
            Dim dt As DataTable
            dt = SelectByWhere("product_serial_num='" & product_serial_num & "'").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing
            'If dt.Rows.Count > 1 Then Throw New Exception("multi records")

            Return DataRowToModel(dt.Rows(0))

        Catch ex As Exception
            Throw New Exception("product_sn.DAL.SelectBySerialNum()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As CATS.Model.product_sn
        Try
            Dim model As New CATS.Model.product_sn
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("product_main_id") Then If Not (IsDBNull(row("product_main_id"))) Then model.product_main_id = Convert.ToInt32(row("product_main_id"))
            If row.Table.Columns.Contains("product_serial_num") Then If Not (IsDBNull(row("product_serial_num"))) Then model.product_serial_num = Convert.ToString(row("product_serial_num"))
            If row.Table.Columns.Contains("register_date") Then If Not (IsDBNull(row("register_date"))) Then model.register_date = Convert.ToDateTime(row("register_date"))
            If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If row.Table.Columns.Contains("sn_status_id") Then If Not (IsDBNull(row("sn_status_id"))) Then model.sn_status_id = Convert.ToInt32(row("sn_status_id"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToString(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("product_sn.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
