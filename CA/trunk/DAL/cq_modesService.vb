Option Strict Off
Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Public Class cq_modesService
    ''' <summary>
    ''' validation =true,validity =true
    ''' </summary>
    ''' <param name="product_main_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectValidityByProductMainId(product_main_id As Integer) As List(Of FACTS.Model.cq_modes)
        Try

            Dim dt As DataTable
            Dim model As Model.cq_modes
            Dim mlist As New List(Of FACTS.Model.cq_modes)

            dt = SelectByWhere("product_main_id='" & product_main_id & "' and product_mode.validation=1 and product_mode.validity=1").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_models.DAL.SelectValidityByProductMainId()::" & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' validation =true,validity = true or false
    ''' </summary>
    ''' <param name="product_main_id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectAllByProductMainId(product_main_id As Integer) As List(Of FACTS.Model.cq_modes)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.cq_modes
            Dim mlist As New List(Of FACTS.Model.cq_modes)

            dt = SelectByWhere("product_main_id='" & product_main_id & "' and product_mode.validation=1 and product_mode.validity =1").Tables("dt")

            If dt.Rows.Count = 0 Then Return Nothing

            For Each row As DataRow In dt.Rows
                model = DataRowToModel(row)
                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_models.DAL.SelectAllByProductMainId()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByProductModeId(product_mode_id As Integer) As FACTS.Model.cq_modes
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.cq_modes

            dt = SelectByWhere("product_mode.id='" & product_mode_id & "' and product_mode.validation=1 and product_mode.validity =1").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            model = DataRowToModel(dt.Rows(0))

            Return model

        Catch ex As Exception
            Throw New Exception("cq_models.DAL.SelectAllByProductMainId()::" & ex.Message)
        End Try
    End Function
    Public Function SelectByWhere(whereString As String) As DataSet
        Try
            Dim ds As DataSet

            Dim parameters As SqlParameter() = {
                  New SqlParameter("@where", whereString)}

            ds = SqlHelper.runProcedureWithDataset("proc_cq_modes_SelectByWhere", parameters, "dt")

            Return ds

        Catch ex As Exception
            Throw New Exception("cq_modes.DAL.SelectByWhere()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.cq_modes
        Try
            Dim model As New FACTS.Model.cq_modes
            If IsNothing(row) Then Return Nothing
            If Not (IsDBNull(row("product_mode_id"))) Then model.product_mode_id = Convert.ToInt32(row("product_mode_id"))
            If Not (IsDBNull(row("product_main_id"))) Then model.product_main_id = Convert.ToInt32(row("product_main_id"))
            If Not (IsDBNull(row("mode_id"))) Then model.mode_id = Convert.ToInt32(row("mode_id"))
            If Not (IsDBNull(row("order_idx"))) Then model.order_idx = Convert.ToInt32(row("order_idx"))
            If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If Not (IsDBNull(row("validation_date"))) Then model.validation_date = Convert.ToDateTime(row("validation_date"))
            If Not (IsDBNull(row("validation"))) Then model.validation = Convert.ToBoolean(row("validation"))
            If Not (IsDBNull(row("function_id"))) Then model.function_id = Convert.ToInt32(row("function_id"))
            If Not (IsDBNull(row("mode"))) Then model.mode = Convert.ToString(row("mode"))

            Return model

        Catch ex As Exception
            Throw New Exception("cq_models.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function

End Class
