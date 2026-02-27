Public Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Label2.Text = "Version " & My.Application.Info.Version.ToString    'Application.ProductVersion
        Label2.Text = "Version " & Common.SW_Version'"Version " & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub About_HelpButtonClicked(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked
        Try
            Dim fileName As String = String.Format("{0}\007Bulk Cable CATS PIM Testing Program User Manual.pdf", "\\sdcnas01\sts_cats\CATS User Manual\Bulk Cable")

            If System.IO.File.Exists(fileName) = True Then
                Process.Start(fileName)
            Else
                MsgBox("File Does Not Exist")
            End If
        Catch ex As Exception
            MsgBox("Show Help File()::" & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Help")
        End Try
    End Sub
End Class