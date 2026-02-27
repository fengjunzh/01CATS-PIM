Public Class GetSN

    Property DevSnForPim As String
    '  Public DevSnForPim As String
    Private Sub GetSN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Scan_sel.Checked = True
        Manual_sel.Visible = False
        Panel2.Visible = False
        Panel1.Location = New Point(61, 40)
        OK_InputSn.Visible = False
        Me.Size = New Size(431, 150)
    End Sub

    Private Sub OK_InputSn_Click(sender As Object, e As EventArgs) Handles OK_InputSn.Click

        'If Manual_sel.Checked = True Then

        'If CheckCode_Input.Text.Trim <> CheckCode_Disp.Text.Trim Then '区分大小写
        '    MsgBox("Error，Please input CheckCode again！", MsgBoxStyle.OkOnly)
        '    CheckCode_Input.Text = ""
        '    Return
        'End If

        'If InputGetSn.Text.Trim.Length <= 3 Then
        '        MsgBox("Error，Please input SN again！", MsgBoxStyle.OkOnly)
        '        Return
        '    End If
        '    DevSnForPim = InputGetSn.Text
        '    Me.Close()
        'End If
    End Sub

    Private _dt As DateTime = DateTime.Now
    Private interval_time As Integer = 80

    Private Sub InputGetSn_KeyUp(sender As Object, e As KeyEventArgs) Handles InputGetSn.KeyUp

        If Scan_sel.Checked = True Then
            If (UserInput_IsEnabled(interval_time)) Then '符合输入要求

                If e.KeyCode = 13 Then
                    If InputGetSn.Text.Trim.Length <= 3 Or Not (InputGetSn.Text.Contains(CheckPhaseForPowCalSN)) Then
                        MsgBox("Error，Please input SN again！", MsgBoxStyle.OkOnly)
                        Return
                    End If
                    DevSnForPim = InputGetSn.Text.Replace(CheckPhaseForPowCalSN, "")
                    Me.Close()
                    FormConfig.Start_Cal.PerformClick() ' add by tony  20200316
                End If
            Else
                InputGetSn.Text = ""
            End If
        End If
    End Sub

    Private Function UserInput_IsEnabled(ByVal Max_Time As Integer) As Boolean

        If InputGetSn.Text.Trim.Length <= 1 Then
            _dt = DateTime.Now
            Return True
        End If

        Dim tempDt As DateTime = DateTime.Now
        Dim ts As TimeSpan = tempDt.Subtract(_dt)  'tempDt-_dt

        If ts.TotalMilliseconds > Max_Time Then
            Return False
        Else
            _dt = tempDt
            Return True
        End If

    End Function

    Public Function GenerateRandom(ByVal Length As Integer) As String

        Dim newRandom As System.Text.StringBuilder = New System.Text.StringBuilder(Length)

        Dim constant() As String = Nothing
        Dim strtemp As String = "a,b,c,d,e,f,g,h,i,j,k,m,n,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,D,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,X,Y,Z,0,1,2,3,4,5,6,7,8,9"
        constant = strtemp.Split(",")

        Dim rd As Random = New Random()
        Dim i As Integer
        For i = 1 To Length
            newRandom.Append(constant(rd.Next(constant.Length)))
        Next

        Return newRandom.ToString()

    End Function

    Private Sub Manual_sel_CheckedChanged(sender As Object, e As EventArgs) Handles Manual_sel.CheckedChanged
        If Manual_sel.Checked = True Then

            Panel2.Visible = True
            OK_InputSn.Visible = True
            Me.Size = New Size(440, 320)
            Panel1.Location = New Point(61, 150)
            Panel2.Location = New Point(61, 40)
            InputGetSn.Text = ""
            CheckCode_Input.Text = ""
            CheckCode_Disp.Text = GenerateRandom(20)
        Else
            Scan_sel.Checked = True
            Panel2.Visible = False
            Panel1.Location = New Point(61, 40)
            OK_InputSn.Visible = False
            Me.Size = New Size(440, 150)
            InputGetSn.Text = ""
        End If
    End Sub

    Private Sub GetSN_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FormConfig.instr_sn_Temp = DevSnForPim
    End Sub

End Class