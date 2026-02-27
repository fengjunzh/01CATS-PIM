Public Class FormConnectDevice
    Private Sub FormConnectDevice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If InitCableTester() = False Then Throw New Exception("Initialize connect device failure!")
        Catch ex As Exception
            MsgBox("FormConnectDevice_Load() - " & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Init Connect Device")
        End Try
    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Try
            Dim distanceMoved As Integer
            Insert("LEFT", 4, 1860, distanceMoved)
        Catch ex As Exception
            MsgBox("btnInsert_Click() - " & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Insert")
        End Try
    End Sub

    Private Sub btnGoHome_Click(sender As Object, e As EventArgs) Handles btnGoHome.Click
        Try
            If gCableTester Is Nothing Then Throw New Exception("Cable tester is not initialized!")
            Dim distanceMoved As Integer
            GoHome("LEFT", 4, distanceMoved)
            GoHome("RIGHT", 4, distanceMoved)
        Catch ex As Exception
            MsgBox("btnGoHome_Click() - " & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Go Home")
        End Try
    End Sub

    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        Try
            If InitCableTester() = False Then Throw New Exception("Initialize connect device failure!")
        Catch ex As Exception
            MsgBox("btnInit_Click() - " & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Init Connect Device")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            'CloseCableTester()
        Catch ex As Exception
            MsgBox("btnGoHome_Click() - " & ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation, "Go Home")
        End Try
    End Sub
End Class