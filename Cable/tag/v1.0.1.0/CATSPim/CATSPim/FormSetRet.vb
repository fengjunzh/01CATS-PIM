Public Class FormSetRet
  Public WriteOnly Property ShowDownTilt As String
    Set(value As String)
      Label1.Text = "Setting downtilt to " & value & " ..."
    End Set
  End Property

  Private Sub FormSetRet_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    My.Application.DoEvents()
  End Sub
End Class