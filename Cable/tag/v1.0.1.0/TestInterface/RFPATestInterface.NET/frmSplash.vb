Option Strict Off
Option Explicit On
Friend Class frmSplash
	Inherits System.Windows.Forms.Form
	
	Private Sub frmSplash_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		Me.Close()
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub frmSplash_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		lblVersion.Text = "Version " & Common.SW_Version
		lblProductName.Text = "CATS Test Software" 'Common.UUT_Type & " Test SW"
		Me.Visible = True
	End Sub

	Private Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
		Me.Close()
    End Sub

    Private Sub Frame1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Frame1.Click
        Me.Close()
    End Sub

    Private Sub Frame1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Frame1.Enter

    End Sub
End Class