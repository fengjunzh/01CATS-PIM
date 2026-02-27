
Public Class FormVibController
	Dim m_dev As IVibrationDevice
	'Private m_dev As New VibDevice
	'Private stat_timer1 As Boolean = False
	'Private stat_timer2 As Boolean = False
	'Private stat_timer3 As Boolean = False
	'Private stat_timer4 As Boolean = False
	Private stat_on As Boolean = False
	Private Delegate Sub DelVibFlash(para As vibCallParas)

	Private Class vibCallParas
		Public ckBox As CheckBox
		Public bitNum As Short
	End Class
	Public Sub MyDelay(millSec As Integer)
		For i = 1 To millSec / 50
			Threading.Thread.Sleep(50)
			My.Application.DoEvents()
		Next
	End Sub
	Private Sub FormVibController_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try
            If cbVibDevice.Text.ToUpper.Trim = "SEALEVEL8222" Then
                vib8222COM.Visible = True
            Else
                vib8222COM.Visible = False
            End If

            cbVibDevice.Items.Clear()
			For Each t In System.Enum.GetValues(GetType(VibCtrl.VibJsbBoard))
				cbVibDevice.Items.Add(t.ToString)
			Next


		Catch ex As Exception

		End Try
	End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        If vib8222COM.Visible = True And vib8222COM.Text = "" Then MsgBox("Please select COMPort for vibration board.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) : Return

        Try
            Dim vibBoard As String = cbVibDevice.SelectedItem.ToString
            If vibBoard = "" Then MsgBox("Please select vibration board.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) : Return

            gbLeap.Enabled = True

            If vibBoard = "JSB31" Then gbLeap.Enabled = False
            m_dev = New VibCtrl(System.Enum.Parse(GetType(VibCtrl.VibJsbBoard), vibBoard))
            m_dev.OpenDev(vib8222COM.Text.ToUpper.Trim)
            btnOpen.Enabled = False
            btnClose.Enabled = True

            btnStart.Enabled = True
            btnStop.Enabled = True

        Catch ex As Exception
            MsgBox("Open vibration device." & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            m_dev.CloseDev()
            btnOpen.Enabled = True
            btnClose.Enabled = False
            btnStart.Enabled = False
            btnStop.Enabled = False
        Catch ex As Exception
            MsgBox("Close vibration device." & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub

	Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
		Me.Close()
	End Sub
	Private Sub StartFlash(vibPara As vibCallParas)
		'Dim vibPara As vibCallParas = CType(para, vibCallParas)
		Do While stat_on
            If stat_on Then m_dev.CloseRelay(vibPara.bitNum)
            'If vibPara.ckBox.InvokeRequired = True Then
            '	vibPara.ckBox.BeginInvoke(Sub() vibPara.ckBox.ImageIndex = 1)
            '	My.Application.DoEvents()
            'Else
            '	vibPara.ckBox.ImageIndex = 1
            'End If

            'MyDelay(1000)
            'Threading.Thread.Sleep(1000)
            If stat_on Then Threading.Thread.Sleep(90)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)

            If stat_on Then m_dev.OpenRelay(vibPara.bitNum)
            'If vibPara.ckBox.InvokeRequired = True Then
            '	vibPara.ckBox.BeginInvoke(Sub() vibPara.ckBox.ImageIndex = 0)
            '	My.Application.DoEvents()
            'Else
            '	vibPara.ckBox.ImageIndex = 0
            'End If
            ' Threading.Thread.Sleep(500)
            If stat_on Then Threading.Thread.Sleep(90)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            If stat_on Then Threading.Thread.Sleep(100)
            My.Application.DoEvents()
		Loop

	End Sub
	Private Sub StartTimer(vibPara As vibCallParas)

		'Dim tc As New Threading.TimerCallback(AddressOf StartFlash)
		'Dim t As New Threading.Timer(tc, vibPara, 0, 500)
		Try
			Dim thVib As New DelVibFlash(AddressOf StartFlash)
			'Dim thResult As IAsyncResult

			thVib.BeginInvoke(vibPara, Nothing, Nothing)

		Catch ex As Exception
			MsgBox(ex.Message)
		End Try

	End Sub
	Private Sub SetRelayFlash(control As CheckBox, bitNum As Short)
		Try
			If control.Checked = True Then
				m_dev.CloseRelay(bitNum)
				control.ImageIndex = 1
				My.Application.DoEvents()
				Threading.Thread.Sleep(1000)
				m_dev.OpenRelay(bitNum)
				control.ImageIndex = 0
				My.Application.DoEvents()
			End If
		Catch ex As Exception

		End Try
	End Sub
	Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
		Try
			Timer1.Enabled = True

			Dim vp As vibCallParas

			stat_on = True
			If ckRelay1.Checked = True Then
				vp = New vibCallParas
				vp.ckBox = ckRelay1
				vp.bitNum = 1
				StartTimer(vp)
			End If

			If ckRelay2.Checked = True Then
				MyDelay(100)
				vp = New vibCallParas
				vp.ckBox = ckRelay2
				vp.bitNum = 2
				StartTimer(vp)
			End If

			If ckRelay3.Checked = True Then
				MyDelay(100) '
				vp = New vibCallParas
				vp.ckBox = ckRelay3
				vp.bitNum = 3
				StartTimer(vp)
			End If

			If ckRelay4.Checked = True Then
				MyDelay(100)
				vp = New vibCallParas
				vp.ckBox = ckRelay4
				vp.bitNum = 4
				StartTimer(vp)
			End If

			ckRelay1.Enabled = False
			ckRelay2.Enabled = False
			ckRelay3.Enabled = False
			ckRelay4.Enabled = False

            'btnStart.Enabled = False
            'btnStop.Enabled = True

        Catch ex As Exception
			MsgBox("Start Vibration()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub

	Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try

            stat_on = False
            Timer1.Enabled = False
            Threading.Thread.Sleep(200)

            m_dev.StopVib()



            'btnStart.Enabled = True
            'btnStop.Enabled = False


            ckRelay1.Enabled = True
            ckRelay2.Enabled = True
            ckRelay3.Enabled = True
            ckRelay4.Enabled = True

        Catch ex As Exception
            MsgBox("Stop Vibration()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub

	Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
		Timer1.Enabled = False
		If ckRelay8.Checked = True Then
            If stat_on Then m_dev.StartVib()
        Else
            If stat_on Then m_dev.StopVib()
        End If

		Timer1.Enabled = True
	End Sub

    Private Sub cbVibDevice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVibDevice.SelectedIndexChanged
        If cbVibDevice.Text.ToUpper.Trim = "SEALEVEL8222" Then
            vib8222COM.Visible = True
            vib8222COM.Text = "COM1"
        Else
            vib8222COM.Visible = False
            vib8222COM.Text = "NA"
        End If
    End Sub
End Class