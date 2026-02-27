Imports System
Imports System.Collections.Generic
Imports FACTS
Public Class cq_phasesManager

    Private _dal As New FACTS.DAL.cq_phasesService
    Public Function SelectAllByProductModeId(product_mode_id As Integer) As List(Of Model.cq_phases)
        Try

            Return _dal.SelectAllByProductModeId(product_mode_id)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeId(int)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdAll(product_mode_id As Integer) As List(Of FACTS.Model.cq_phases)
        Try
            Return _dal.SelectAllByProductModeIdAll(product_mode_id)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeIdAll(int)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeId(product_mode_id As Integer, validity As Boolean, validation As Boolean) As List(Of Model.cq_phases)
        Try

            Return _dal.SelectAllByProductModeId(product_mode_id, validity, validation)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeId(int,bool,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdValidity(product_mode_id As Integer, validity As Boolean) As List(Of Model.cq_phases)
        Try

            Return _dal.SelectAllByProductModeIdValidity(product_mode_id, validity)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeIdValidity(int,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeIdValidation(product_mode_id As Integer, validation As Boolean) As List(Of Model.cq_phases)
        Try

            Return _dal.SelectAllByProductModeIdValidation(product_mode_id, validation)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeIdValidation(int,bool)::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductMode(mode As String, product_main_id As Integer) As List(Of FACTS.Model.cq_phases)
        Try

            Return _dal.SelectAllByProductMode(mode, product_main_id)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductMode()::" & ex.Message)
        End Try
    End Function
End Class
