Imports System.IO
Imports System.Xml
Public Class FormSAPFailSafeMode
    Private Sub chkSAPFailSafeModeOnOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkSAPFailSafeModeOnOff.CheckedChanged
        If chkSAPFailSafeModeOnOff.Checked = True Then
            Me.txtWorkOrder.Enabled = True
            Me.cmbPartNumber.Enabled = True
            Me.txtLength.Enabled = True
            Me.cboUOM.Enabled = True
        Else
            Me.txtWorkOrder.Enabled = False
            Me.cmbPartNumber.Enabled = False
            Me.txtLength.Enabled = False
            Me.cboUOM.Enabled = False
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FormSAPFailSafteMode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LoadCAPart()
            gSAPFailSafeMode = SAPFailSafeMode.CreateInstance(gSAPFailSafeModeFile)
            If gSAPFailSafeMode IsNot Nothing Then
                Me.chkSAPFailSafeModeOnOff.Checked = gSAPFailSafeMode.SAPFailSafeModeOnOff
                Me.txtWorkOrder.Text = gSAPFailSafeMode.WorkOrder
                Me.cmbPartNumber.Text = gSAPFailSafeMode.PartNumber
                Me.txtLength.Text = gSAPFailSafeMode.Length
                Me.cboUOM.Text = gSAPFailSafeMode.UOM
            Else
                Me.chkSAPFailSafeModeOnOff.Checked = False
            End If
        Catch ex As Exception
            MsgBox("FormSAPFailSafteMode_Load()::" & ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            gSAPFailSafeMode = New SAPFailSafeMode

            With gSAPFailSafeMode
                .SAPFailSafeModeOnOff = Me.chkSAPFailSafeModeOnOff.Checked
                .WorkOrder = Me.txtWorkOrder.Text
                .PartNumber = Me.cmbPartNumber.Text
                .UOM = Me.cboUOM.Text
                If Me.txtLength.Text = "" Then
                    .Length = 0
                Else
                    .Length = Convert.ToSingle(Me.txtLength.Text)
                End If
            End With
            gSAPFailSafeMode.Save(gSAPFailSafeModeFile)
            MsgBox("Success!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            GUI.SAPFailSafeModeOnOff = gSAPFailSafeMode.SAPFailSafeModeOnOff

        Catch ex As Exception
            MsgBox("SAPFailSafeMode.btnSave_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Sub LoadCAPart()
        Try
            Dim lstPartNumber As List(Of CATS.Model.product_main)
            Dim pmBll As New CATS.BLL.product_mainManager
            lstPartNumber = pmBll.SelectProductByGroup("CA")
            If lstPartNumber Is Nothing Then Return
            Me.cmbPartNumber.DataSource = lstPartNumber
        Catch ex As Exception
            Throw New Exception("SAPFailSafeMode()::LoadCAPartNumber()::" & ex.Message)
        End Try
    End Sub
End Class

Public Class SAPFailSafeMode
    Public SAPFailSafeModeOnOff As Boolean
    Public WorkOrder As String
    Public PartNumber As String
    Public Length As Single
    Public UOM As String

    Public Sub Save(ByVal fileName As String)
        Try
            Dim sw As New StreamWriter(fileName)
            Dim xmlSer As New Serialization.XmlSerializer(Me.GetType)
            xmlSer.Serialize(sw, Me)
            sw.Close()
        Catch ex As Exception
            Throw New Exception("SAPFailSafeMode.Save()::" & ex.Message)
        End Try
    End Sub

    Public Shared Function CreateInstance(ByVal fileName As String) As SAPFailSafeMode
        Try
            If File.Exists(fileName) = False Then Return Nothing
            Dim _SAPFailSafeMode As SAPFailSafeMode
            Dim xmlSer As New Serialization.XmlSerializer(GetType(SAPFailSafeMode))
            Using sr As New StreamReader(fileName)
                _SAPFailSafeMode = xmlSer.Deserialize(sr)
                sr.Close()
            End Using
            Return _SAPFailSafeMode
        Catch ex As Exception
            Return Nothing
            'Throw New Exception("SAPFailSafeMode.CreateInstance()::" & ex.Message)
        End Try
    End Function
End Class