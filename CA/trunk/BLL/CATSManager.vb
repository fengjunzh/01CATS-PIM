Public Class CATSManager
    Public Sub ActivateCATS(connString As String)
        Try
            FACTS.DAL.SqlHelper.ActivateConnection(connString)
        Catch ex As Exception
            Throw New Exception("ActivateCATS()::" & ex.Message)
        End Try
    End Sub
End Class
