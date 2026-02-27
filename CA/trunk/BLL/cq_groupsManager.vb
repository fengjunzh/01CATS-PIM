Imports System
Imports System.Collections.Generic
Imports CATS
Public Class cq_groupsManager

  Private _dal As New CATS.DAL.cq_groupsService
  Public Function SelectAllBySpecMainId(spec_main_id As Integer) As List(Of Model.cq_groups)
    Try

      Return _dal.SelectAllBySpecMainId(spec_main_id)

    Catch ex As Exception
      Throw New Exception("cq_groups.BLL.SelectAllBySpecMainId()::" & ex.Message)
    End Try
  End Function

End Class
