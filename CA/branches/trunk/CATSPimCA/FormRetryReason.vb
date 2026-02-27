Public Class FormRetryReason
  Public Property ReturnMsg As String
  Public Property ReturnTestType As Short = 0
  Private Sub FormRetryReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ReturnMsg = "Unknow"
  End Sub
  Private Sub btnRetry_Click(sender As System.Object, e As System.EventArgs) Handles btnRetry.Click
    If rbRetry.Checked = True Then
      ReturnMsg = "Only retry"
    ElseIf rbReconnect.Checked = True Then
      ReturnMsg = "Re-connect"
    ElseIf rbOther.Checked = True Then
      ReturnMsg = txtOther.Text.Trim
    Else
      ReturnMsg = "Unknow"
    End If

    ReturnTestType = 0

    Me.Close()
  End Sub

  Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
    ReturnTestType = 1
    Me.Close()
  End Sub
End Class