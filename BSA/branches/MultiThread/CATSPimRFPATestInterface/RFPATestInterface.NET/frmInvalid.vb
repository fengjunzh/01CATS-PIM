Option Strict Off
Option Explicit On
Friend Class frmInvalid
	Inherits System.Windows.Forms.Form
	
	
	Private Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
	
	Const HWND_TOPMOST As Short = -1
	Const SWP_NOMOVE As Short = &H2s
	Const SWP_NOSIZE As Short = &H1s
	
	Private Sub frmInvalid_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim lngWindowPosition As Integer
		
		lngWindowPosition = SetWindowPos(Me.Handle.ToInt32, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
	End Sub
	
	Private Sub OKButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles OKButton.Click
		Me.Visible = False
	End Sub
End Class