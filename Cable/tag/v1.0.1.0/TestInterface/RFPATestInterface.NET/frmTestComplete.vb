Option Strict Off
Option Explicit On
Friend Class frmTestComplete
	Inherits System.Windows.Forms.Form

Private again As Boolean

    Public Function TestComplete(ByVal Passed As Boolean, ByVal enableRepeat As Boolean) As Boolean
        If Passed Then
            Me.LabelPassFail.Text = "PASS"
            Me.LabelPassFail.ForeColor = System.Drawing.Color.LimeGreen
            CmdAction.Focus()
        Else
            Me.LabelPassFail.Text = "FAIL"
            Me.LabelPassFail.ForeColor = System.Drawing.Color.Red
        End If

        'center form
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Me.Width) \ 2
        Me.Top = (System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Me.Height) \ 2
        cmdRepeat.Visible = enableRepeat
        Me.ShowDialog()
        TestComplete = again
    End Function

    Private Sub frmTestComplete_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CmdAction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdAction.Click
        again = False
        Me.Hide()
    End Sub

    Private Sub CmdRepeat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRepeat.Click
        again = True
        Me.Hide()
    End Sub
End Class