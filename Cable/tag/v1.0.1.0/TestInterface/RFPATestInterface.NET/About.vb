Public Class About

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Label2.Text = "Version " & My.Application.Info.Version.ToString    'Application.ProductVersion
        Label2.Text = "Version " & Common.SW_Version'"Version " & System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class