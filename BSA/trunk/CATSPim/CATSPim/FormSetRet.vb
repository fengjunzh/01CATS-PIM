Public Class FormSetRet
	Public WriteOnly Property ProgressMaxValue As Integer
		Set(value As Integer)
			ProgressBar1.Maximum = value
			My.Application.DoEvents()
		End Set
	End Property
	Public WriteOnly Property ProgressValue As Integer
		Set(value As Integer)
			ProgressBar1.Value = value
			My.Application.DoEvents()
		End Set
	End Property

	Private Sub FormSetRet_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
		My.Application.DoEvents()
	End Sub
End Class