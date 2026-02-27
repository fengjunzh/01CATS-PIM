Public Class FormRetryReason
  Public Property ReturnMsg As String
  Public Property ReturnTestType As Short = 0
  Private Sub FormRetryReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Panel1.Visible = False

            ReturnMsg = "Unknow"
            If DTFTestTime_AutoDisplay <> "" And DTFTestTime_AutoDisplay.ToUpper <> "NA" Then
                Label2.Text = "delay=" & DTFTestTime_AutoDisplay.Split(",")(0) & "," & "r=" & DTFTestTime_AutoDisplay.Split(",")(1) & "," & "rssi=" & DTFTestTime_AutoDisplay.Split(",")(2)
                Label1.Text = "Please open S document and check" & Chr(10) & ” where's problem base on this delay time(delay,r,rssi)" 'add 20190618          
                Panel1.Visible = True
            End If

            DTFTestTime_AutoDisplay = ""


        Catch ex As Exception
            Throw New Exception("RetryReason::GetDTFTime()::" & DTFTestTime & "->" & ex.Message)
        End Try


    End Sub
  Private Sub btnRetry_Click(sender As System.Object, e As System.EventArgs) Handles btnRetry.Click
    If rbRetry.Checked = True Then
      ReturnMsg = "Only retry"
    ElseIf rbReconnect.Checked = True Then
      ReturnMsg = "Re-connect"
    ElseIf rbOther.Checked = True Then
      ReturnMsg = txtOther.Text.Trim
    Else
      ReturnMsg = "Unknow"
    End If

    ReturnTestType = 0

    Me.Close()
  End Sub

  Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
    ReturnTestType = 1
    Me.Close()
  End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label2.Text = ""
        Label1.Text = "" 'add 20190618    
        Application.DoEvents()

        Dim DTFTime As String = GetDTFTime("65536", "2") 'pPimDev
        If DTFTime <> "NA" Then
            Label2.Text = DTFTime
            Label1.Text = "Please open S document and check" & Chr(10) & ” where's problem base on this delay time" 'add 20190618    
        Else
            Label2.Text = "Unknow"
            Label1.Text = "Don't get any avaiable information, please check ZULU! " 'add 20190618    
        End If
        Panel1.Visible = True
    End Sub
End Class