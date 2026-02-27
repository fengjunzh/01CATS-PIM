Public Class FormPassword
	Public Password As String
	Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
		Try
			Password = txtPassword.Text.Trim
			Me.Close()

		Catch ex As Exception
			MsgBox("btnOK()" & vbCrLf & " at " & ex.Message)
		End Try
	End Sub

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Password = ""
		Me.Close()
	End Sub

	Private Sub FormPassword_Load(sender As Object, e As EventArgs) Handles Me.Load
		txtPassword.Text = ""
	End Sub

	Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
		If e.KeyChar = Chr(13) Then
			btnOK.Focus()
		End If
	End Sub
End Class