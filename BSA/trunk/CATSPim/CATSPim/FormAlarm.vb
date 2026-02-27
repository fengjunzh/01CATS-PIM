Public Class FormAlarm
	Public Result As DialogResult
	Private _ShowModeEnable As Boolean = False
	Private _ShowTestStationEnable As Boolean = False
	Private _ShowMessage As String
	Private Sub FormAlarm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Dim str As String = ""

		If _ShowModeEnable = True Then
			str = "The current test mode
          is not <" & _ShowMessage & ">."
		End If

		If _ShowTestStationEnable = True Then
			str = "The current test station  
          is not <" & _ShowMessage & ">."
		End If

		Label1.Text = str

	End Sub
	Public WriteOnly Property ShowModeEnable As Boolean
		Set(value As Boolean)
			_ShowModeEnable = value
			_ShowTestStationEnable = Not value
		End Set
	End Property
	Public WriteOnly Property ShowTestStationEnable As Boolean
		Set(value As Boolean)
			_ShowTestStationEnable = value
			_ShowModeEnable = Not value
		End Set
	End Property
	Public WriteOnly Property ShowMessage As String
		Set(value As String)
			_ShowMessage = value
		End Set
	End Property

	Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
		Result = DialogResult.Yes
		Me.Close()

	End Sub

	Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
		Result = DialogResult.No
		Me.Close()
	End Sub
End Class