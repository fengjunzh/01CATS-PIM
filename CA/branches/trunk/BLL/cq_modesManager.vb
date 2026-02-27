Imports System
Imports System.Collections.Generic
Imports CATS
Public Class cq_modesManager

  Private _dal As New CATS.DAL.cq_modesService
  Public Function SelectValidityByProductMainId(product_main_id As Integer) As List(Of CATS.Model.cq_modes)
    Try
      Return _dal.SelectValidityByProductMainId(product_main_id)
    Catch ex As Exception
      Throw New Exception("cq_modesManager.BLL.SelectValidityByProductMainId()::" & ex.Message)
    End Try
  End Function
    Public Function SelectAllByProductMainId(product_main_id As Integer) As List(Of Model.cq_modes)
        Try

            Return _dal.SelectAllByProductMainId(product_main_id)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductMainId()::" & ex.Message)
        End Try
    End Function
    Public Function SelectAllByProductModeId(product_mode_id As Integer) As Model.cq_modes
        Try

            Return _dal.SelectByProductModeId(product_mode_id)

        Catch ex As Exception
            Throw New Exception("cq_modesManager.BLL.SelectAllByProductModeId()::" & ex.Message)
        End Try
    End Function

End Class
