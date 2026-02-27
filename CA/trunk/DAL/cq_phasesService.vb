Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Public Class cq_phasesService
    'Public Function SelectAllByProductModeId(product_mode_id As Integer) As List(Of CATS.Model.cq_phases)
    '  Try
    '    Dim dt As DataTable
    '    Dim model As CATS.Model.cq_phases
    '    Dim mlist As New List(Of CATS.Model.cq_phases)

    '    Dim parameters As SqlParameter() = {
    '                            New SqlParameter("@product_mode_id", product_mode_id)}

    '    dt = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByProductModeId", parameters, "dt").Tables("dt")
    '    If dt.Rows.Count = 0 Then Return Nothing

    '    For Each row As DataRow In dt.Rows
    '      model = DataRowToModel(row)
    '      mlist.Add(model)
    '    Next

    '    Return mlist

    '  Catch ex As Exception
    '    Throw New Exception("cq_phases.DAL.SelectAllByProductModeId()::" & ex.Message)
    '  End Try
    'End Function
    Public Function SelectAllByProductModeId(product_mode_id As Integer, validity As Boolean, validation As Boolean) As List(Of FACTS.Model.cq_phases)
        Try
            Dim dt1 As DataTable

            Dim model As FACTS.Model.cq_phases
            Dim mlist As New List(Of FACTS.Model.cq_phases)

            Dim parameters As SqlParameter() = {
                              New SqlParameter("@validity", validity),
                              New SqlParameter("@validation", validation),
                              New SqlParameter("@product_mode_id", product_mode_id)
            }

            dt1 = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByProdModeId", parameters, "dt").Tables("dt")
            If dt1.Rows.Count = 0 Then Return Nothing

            Dim spec_main_model As FACTS.Model.spec_main
            Dim spec_main_bll As New spec_mainService


            For Each row As DataRow In dt1.Rows

                model = DataRowToModel(row)

                spec_main_model = New FACTS.Model.spec_main
                spec_main_model = spec_main_bll.SelectById(model.spec_main_id)
                model.spec_main_model = spec_main_model

                mlist.Add(model)

            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductModeId(int,bool,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdAll(product_mode_id As Integer) As List(Of FACTS.Model.cq_phases)
        Try
            Dim dt1 As DataTable

            Dim model As FACTS.Model.cq_phases
            Dim mlist As New List(Of FACTS.Model.cq_phases)

            Dim parameters As SqlParameter() = {
                                  New SqlParameter("@product_mode_id", product_mode_id)
                  }

            dt1 = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByProdModeIdAll", parameters, "dt").Tables("dt")
            If dt1.Rows.Count = 0 Then Return Nothing

            Dim spec_main_model As FACTS.Model.spec_main
            Dim spec_main_bll As New spec_mainService


            For Each row As DataRow In dt1.Rows

                model = DataRowToModel(row)

                spec_main_model = New FACTS.Model.spec_main
                spec_main_model = spec_main_bll.SelectById(model.spec_main_id)
                model.spec_main_model = spec_main_model

                mlist.Add(model)

            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductModeIdAll(int,bool,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeId(product_mode_id As Integer) As List(Of FACTS.Model.cq_phases)
        Try

            Return SelectAllByProductModeId(product_mode_id, True, True)

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductModeId(int)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdValidity(product_mode_id As Integer, validity As Boolean) As List(Of FACTS.Model.cq_phases)
        Try
            Dim dt1 As DataTable

            Dim model As FACTS.Model.cq_phases
            Dim mlist As New List(Of FACTS.Model.cq_phases)

            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@validity", validity),
                                    New SqlParameter("@product_mode_id", product_mode_id)
                  }

            dt1 = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByProdModeIdValidity", parameters, "dt").Tables("dt")
            If dt1.Rows.Count = 0 Then Return Nothing

            Dim spec_main_model As FACTS.Model.spec_main
            Dim spec_main_bll As New spec_mainService


            For Each row As DataRow In dt1.Rows

                model = DataRowToModel(row)

                spec_main_model = New FACTS.Model.spec_main
                spec_main_model = spec_main_bll.SelectById(model.spec_main_id)
                model.spec_main_model = spec_main_model

                mlist.Add(model)

            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductModeIdValidity(int,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdValidation(product_mode_id As Integer, validation As Boolean) As List(Of FACTS.Model.cq_phases)
        Try
            Dim dt1 As DataTable

            Dim model As FACTS.Model.cq_phases
            Dim mlist As New List(Of FACTS.Model.cq_phases)

            Dim parameters As SqlParameter() = {
                                    New SqlParameter("@validation", validation),
                                    New SqlParameter("@product_mode_id", product_mode_id)
                  }

            dt1 = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByProdModeIdValidation", parameters, "dt").Tables("dt")
            If dt1.Rows.Count = 0 Then Return Nothing

            Dim spec_main_model As FACTS.Model.spec_main
            Dim spec_main_bll As New spec_mainService


            For Each row As DataRow In dt1.Rows

                model = DataRowToModel(row)

                spec_main_model = New FACTS.Model.spec_main
                spec_main_model = spec_main_bll.SelectById(model.spec_main_id)
                model.spec_main_model = spec_main_model

                mlist.Add(model)

            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductModeIdValidation(int,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductMode(mode As String, product_main_id As Integer) As List(Of FACTS.Model.cq_phases)
        Try
            Dim dt As DataTable
            Dim model As FACTS.Model.cq_phases
            Dim mlist As New List(Of FACTS.Model.cq_phases)

            Dim parameters As SqlParameter() = {
               New SqlParameter("@mode", mode),
               New SqlParameter("@product_main_id", product_main_id)
            }

            dt = SqlHelper.runProcedureWithDataset("proc_cq_phases_SelectByModeProduct", parameters, "dt").Tables("dt")
            If dt.Rows.Count = 0 Then Return Nothing

            Dim spec_main_model As FACTS.Model.spec_main
            Dim spec_main_bll As New spec_mainService

            For Each row As DataRow In dt.Rows
                model = DataRowToModel(row)
                spec_main_model = New FACTS.Model.spec_main
                spec_main_model = spec_main_bll.SelectById(model.spec_main_id)
                model.spec_main_model = spec_main_model

                mlist.Add(model)
            Next

            Return mlist

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.SelectAllByProductMode()::" & ex.Message)
        End Try
    End Function
    Public Function DataRowToModel(row As DataRow) As FACTS.Model.cq_phases
        Try
            Dim model As New FACTS.Model.cq_phases

            If IsNothing(row) Then Return Nothing

            If Not (IsDBNull(row("mode_spec_id"))) Then model.mode_spec_id = Convert.ToInt32(row("mode_spec_id"))
            If Not (IsDBNull(row("product_mode_id"))) Then model.product_mode_id = Convert.ToInt32(row("product_mode_id"))
            If Not (IsDBNull(row("spec_main_id"))) Then model.spec_main_id = Convert.ToInt32(row("spec_main_id"))
            If Not (IsDBNull(row("order_idx"))) Then model.order_idx = Convert.ToInt32(row("order_idx"))
            If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
            If Not (IsDBNull(row("validation_date"))) Then model.validation_date = Convert.ToDateTime(row("validation_date"))
            If Not (IsDBNull(row("validation"))) Then model.validation = Convert.ToBoolean(row("validation"))
            If Not (IsDBNull(row("phase_main_id"))) Then model.phase_main_id = Convert.ToInt32(row("phase_main_id"))
            If Not (IsDBNull(row("phase"))) Then model.phase = Convert.ToString(row("phase"))
            If Not (IsDBNull(row("function_id"))) Then model.function_id = Convert.ToInt32(row("function_id"))
            If Not (IsDBNull(row("class_id"))) Then model.class_id = Convert.ToInt32(row("class_id"))

            Return model

        Catch ex As Exception
            Throw New Exception("cq_phases.DAL.DataRowToModel()::" & ex.Message)
        End Try
    End Function

End Class
