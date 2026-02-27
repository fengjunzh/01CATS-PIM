Option Strict Off
Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Partial Public Class product_mainService

    Public Function Add(model As CATS.Model.product_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
          New SqlParameter("@customer_id", model.customer_id),
          New SqlParameter("@product_name", model.product_name),
          New SqlParameter("@cust_product_name", model.cust_product_name),
          New SqlParameter("@product_ver", model.product_ver),
          New SqlParameter("@product_desc", model.product_desc),
          New SqlParameter("@family", model.family),
          New SqlParameter("@validity", model.validity),
          New SqlParameter("@sn_format", model.sn_format),
          New SqlParameter("@sn_check", model.sn_check),
          New SqlParameter("@port_num", model.port_num),
          New SqlParameter("@dwtilt_enable", model.dwtilt_enable),
          New SqlParameter("@dwtilt_type", model.dwtilt_type),
          New SqlParameter("@dwtilt_num", model.dwtilt_num),
          New SqlParameter("@dwtilt_mode", model.dwtilt_mode),
          New SqlParameter("@dwtilt_pn", model.dwtilt_pn),
          New SqlParameter("@function_id", model.function_id),
          New SqlParameter("@gen1", model.gen1),
          New SqlParameter("@gen2", model.gen2),
          New SqlParameter("@gen3", model.gen3)
        }
            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_main_Add", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("product_main.DAL.Add()::" & ex.Message)
        End Try
    End Function
    Public Function AddReturnId(model As CATS.Model.product_main) As Integer
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@customer_id", model.customer_id),
        New SqlParameter("@product_name", model.product_name),
        New SqlParameter("@cust_product_name", model.cust_product_name),
        New SqlParameter("@product_ver", model.product_ver),
        New SqlParameter("@product_desc", model.product_desc),
        New SqlParameter("@family", model.family),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@sn_format", model.sn_format),
        New SqlParameter("@sn_check", model.sn_check),
        New SqlParameter("@port_num", model.port_num),
        New SqlParameter("@dwtilt_enable", model.dwtilt_enable),
        New SqlParameter("@dwtilt_type", model.dwtilt_type),
        New SqlParameter("@dwtilt_num", model.dwtilt_num),
        New SqlParameter("@dwtilt_mode", model.dwtilt_mode),
        New SqlParameter("@dwtilt_pn", model.dwtilt_pn),
        New SqlParameter("@function_id", model.function_id),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }
            Return SqlHelper.runProcedureWithReturnValue("proc_product_main_Add", parameters, rowsAffected)
        Catch ex As Exception
            Throw New Exception("product_main.DAL.AddReturnId()::" & ex.Message)
        End Try
    End Function
    Public Function Delete(id As Integer) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_main_DeleteRow", parameters)
            Return IIf(rowsAffected > 0, True, False)

        Catch ex As Exception
            Throw New Exception("product_main.DAL.Delete()::" & ex.Message)
        End Try
    End Function
    Public Function Update(model As CATS.Model.product_main) As Boolean
        Try
            Dim rowsAffected As Integer
            Dim parameters As SqlParameter() = {
        New SqlParameter("@id", model.id),
        New SqlParameter("@customer_id", model.customer_id),
        New SqlParameter("@product_name", model.product_name),
        New SqlParameter("@cust_product_name", model.cust_product_name),
        New SqlParameter("@product_ver", model.product_ver),
        New SqlParameter("@product_desc", model.product_desc),
        New SqlParameter("@family", model.family),
        New SqlParameter("@validity", model.validity),
        New SqlParameter("@sn_format", model.sn_format),
        New SqlParameter("@sn_check", model.sn_check),
        New SqlParameter("@port_num", model.port_num),
        New SqlParameter("@dwtilt_enable", model.dwtilt_enable),
        New SqlParameter("@dwtilt_type", model.dwtilt_type),
        New SqlParameter("@dwtilt_num", model.dwtilt_num),
        New SqlParameter("@dwtilt_mode", model.dwtilt_mode),
        New SqlParameter("@dwtilt_pn", model.dwtilt_pn),
        New SqlParameter("@function_id", model.function_id),
        New SqlParameter("@gen1", model.gen1),
        New SqlParameter("@gen2", model.gen2),
        New SqlParameter("@gen3", model.gen3)
      }

            rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_product_main_Update", parameters)
            Return IIf(rowsAffected > 0, True, False)
        Catch ex As Exception
            Throw New Exception("product_main.DAL.Update()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAll() As List(Of CATS.Model.product_main)
        Try
            Dim dt As DataTable
            Dim model As CATS.Model.product_main
            Dim mlist As New List(Of CATS.Model.product_main)

            Dim parameters As SqlParameter() = {}
            dt = SqlHelper.runProcedureWithDataset("proc_product_main_SelectAll", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New CATS.Model.product_main
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("product_main.DAL.SelectAll()::" & ex.Message)
        End Try
    End Function
    Public Function SelectById(id As Integer) As CATS.Model.product_main
        Try
            Dim dt As DataTable
            Dim model As New CATS.Model.product_main
            Dim parameters As SqlParameter() = {
                              New SqlParameter("@id", id)}

            dt = SqlHelper.runProcedureWithDataset("proc_product_main_SelectRow", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("product_main.DAL.SelectById()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByProductName(product_name As String) As CATS.Model.product_main
        Try
            Dim dt As DataTable
            Dim model As New CATS.Model.product_main
            'Dim parameters As SqlParameter() = {
            '                  New SqlParameter("@product_name", product_name)}

            dt = SelectByWhere("product_name = '" & product_name & "'").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))
            Return model

        Catch ex As Exception
            Throw New Exception("product_main.DAL.SelectByProductName()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_product_main_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("product_main.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllBySeries(series As String) As List(Of CATS.Model.product_main)
        Try
            Dim dt As DataTable
            Dim model As CATS.Model.product_main
            Dim mlist As New List(Of CATS.Model.product_main)

            dt = SelectByWhere("product_name like '" & series & "' Order By product_name desc, family asc").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New CATS.Model.product_main
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("SelectAllBySeries.DAL.SelectAllCAPart()::" & ex.Message)
        End Try
    End Function
    Public Function SelectProductByGroup(prodGroup As String) As List(Of CATS.Model.product_main)
        Try
            Dim dt As DataTable
            Dim model As CATS.Model.product_main
            Dim mlist As New List(Of CATS.Model.product_main)

            Dim parameters As SqlParameter() =
            {
                New SqlParameter("@ProdGroup", prodGroup)
            }
            dt = SqlHelper.runProcedureWithDataset("select_product_by_group", parameters, "dt").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = New CATS.Model.product_main
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("product_main.DAL.SelectProductByGroup()::" & ex.Message)
        End Try
    End Function

    Public Function DataRowToModel(row As DataRow) As CATS.Model.product_main

        Try
            Dim model As New CATS.Model.product_main
            If IsNothing(row) Then Return Nothing
            If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
            If row.Table.Columns.Contains("customer_id") Then If Not (IsDBNull(row("customer_id"))) Then model.customer_id = Convert.ToInt32(row("customer_id"))
            If row.Table.Columns.Contains("product_name") Then If Not (IsDBNull(row("product_name"))) Then model.product_name = Convert.ToString(row("product_name"))
            If row.Table.Columns.Contains("cust_product_name") Then If Not (IsDBNull(row("cust_product_name"))) Then model.cust_product_name = Convert.ToString(row("cust_product_name"))
            If row.Table.Columns.Contains("product_ver") Then If Not (IsDBNull(row("product_ver"))) Then model.product_ver = Convert.ToString(row("product_ver"))
            If row.Table.Columns.Contains("product_desc") Then If Not (IsDBNull(row("product_desc"))) Then model.product_desc = Convert.ToString(row("product_desc"))
            If row.Table.Columns.Contains("family") Then If Not (IsDBNull(row("family"))) Then model.family = Convert.ToString(row("family"))
            If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If row.Table.Columns.Contains("sn_format") Then If Not (IsDBNull(row("sn_format"))) Then model.sn_format = Convert.ToString(row("sn_format"))
            If row.Table.Columns.Contains("sn_check") Then If Not (IsDBNull(row("sn_check"))) Then model.sn_check = Convert.ToBoolean(row("sn_check"))
            If row.Table.Columns.Contains("port_num") Then If Not (IsDBNull(row("port_num"))) Then model.port_num = Convert.ToInt32(row("port_num"))
            If row.Table.Columns.Contains("dwtilt_enable") Then If Not (IsDBNull(row("dwtilt_enable"))) Then model.dwtilt_enable = Convert.ToBoolean(row("dwtilt_enable"))
            If row.Table.Columns.Contains("dwtilt_type") Then If Not (IsDBNull(row("dwtilt_type"))) Then model.dwtilt_type = Convert.ToInt32(row("dwtilt_type"))
            If row.Table.Columns.Contains("dwtilt_num") Then If Not (IsDBNull(row("dwtilt_num"))) Then model.dwtilt_num = Convert.ToInt32(row("dwtilt_num"))
            If row.Table.Columns.Contains("dwtilt_mode") Then If Not (IsDBNull(row("dwtilt_mode"))) Then model.dwtilt_mode = Convert.ToString(row("dwtilt_mode"))
            If row.Table.Columns.Contains("dwtilt_pn") Then If Not (IsDBNull(row("dwtilt_pn"))) Then model.dwtilt_pn = Convert.ToString(row("dwtilt_pn"))
            If row.Table.Columns.Contains("function_id") Then If Not (IsDBNull(row("function_id"))) Then model.function_id = Convert.ToInt32(row("function_id"))
            If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToString(row("gen1"))
            If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
            If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
            Return model
        Catch ex As Exception
            Throw New Exception("product_main.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function
End Class
