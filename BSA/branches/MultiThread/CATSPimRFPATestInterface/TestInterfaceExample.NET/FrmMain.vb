Option Strict Off
Option Explicit On
Friend Class FrmMain
	Inherits System.Windows.Forms.Form
	
	Private Sub FrmMain_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.ShowInTaskbar = False
        Me.Visible = False
        Call MyTestPlan.InitializeGui()
    End Sub
	
	Private Sub FrmMain_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		Dim frm As System.Windows.Forms.Form
		For	Each frm In My.Application.OpenForms
			frm.Close()
		Next frm
	End Sub
End Class