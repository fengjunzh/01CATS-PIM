Public Class FormRun
	Public Sub ClearTabPage()
		Try
			tc1.TabPages.Clear()
		Catch ex As Exception

		End Try
	End Sub
	Public Sub ClearPhaseBox()
		Try

		Catch ex As Exception

		End Try
	End Sub
	Public Function AddPhaseBox(name As String) As CATSRunBox
		Try
			Dim tp As TabPage
			Dim uctrl As New CATSRunBox

			uctrl.Left = 10
			uctrl.Top = 10
			'uctrl.Anchor = AnchorStyles.Left And AnchorStyles.Right And AnchorStyles.Bottom And AnchorStyles.Top
			uctrl.Dock = DockStyle.Fill

			tp = New TabPage
			tp.Text = name
			tp.Controls.Add(uctrl)
			tc1.TabPages.Add(tp)
			My.Application.DoEvents()
			Return uctrl

		Catch ex As Exception
			Throw New Exception("FormRun.AddPhaseBox()::" & ex.Message)
		End Try
	End Function

	Private Sub FormRun_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub
End Class