Imports C1.Win.C1FlexGrid
Imports System.DirectoryServices.ActiveDirectory
Imports System.Xml.Serialization

Public Class FormTestAccessory
    Private Sub FormTestAccessory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            InitfgAccssory()
            gTestAccessoryList = CTestAccessoryList.CreateInstance(gTestAccessoryListFileName)
            If gTestAccessoryList Is Nothing Then Exit Sub
            Dim r As Row
            Dim testCableM As CATS.Model.test_cable_main
            Dim testCableBll As New CATS.BLL.test_cable_mainManager
            With gTestAccessoryList
                For Each testAcc As CTestAccessory In .TestAccessoryList
                    r = Me.fgAccessory.Rows.Add
                    r(1) = testAcc.Enable
                    r(2) = testAcc.ModelName
                    r(3) = testAcc.SerialNumber
                    r(4) = testAcc.TolerantCount
                    testCableM = testCableBll.SelectByCableSN(testAcc.SerialNumber)
                    If testCableM IsNot Nothing Then r(5) = testCableM.test_count
                    r(6) = testAcc.RegisterTime
                    r(7) = testAcc.Description
                Next
            End With
            FormatGrid(fgAccessory, 9, 9)
        Catch ex As Exception
            MsgBox("FormTestAccessory_Load()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Load Form Error")
        End Try
    End Sub
    Private Sub InitfgAccssory()
        Try
            'set up grid
            fgAccessory.Clear(ClearFlags.Content)
            fgAccessory.Cols.Fixed = 1
            fgAccessory.Cols.Count = 8
            fgAccessory.Rows.Fixed = 1
            fgAccessory.Rows.Count = 1
            fgAccessory.Cols(1).Caption = "Enable"
            fgAccessory.Cols(2).Caption = "Model Name"
            fgAccessory.Cols(3).Caption = "Serial Number"
            fgAccessory.Cols(4).Caption = "Tolerant Count"
            fgAccessory.Cols(4).Name = "Tolerant Count"
            fgAccessory.Cols(5).Caption = "Test Count"
            fgAccessory.Cols(5).Name = "Test Count"
            fgAccessory.Cols(6).Caption = "Register Time"
            fgAccessory.Cols(6).Name = "Register Time"
            fgAccessory.Cols(7).Caption = "Description"
            fgAccessory.Cols(7).Name = "Description"

            Dim cs As CellStyle = fgAccessory.Styles.Add("bool")
            cs.DataType = Type.GetType("System.Boolean")
            fgAccessory.Cols(1).Style = cs

            Dim tcmBll As New CATS.BLL.test_cable_modelManager
            Dim tcmML As List(Of CATS.Model.test_cable_model)
            tcmML = tcmBll.SelectAll()
            Dim tcmMLStr As String = String.Join("|"， tcmML)
            fgAccessory.Cols(2).ComboList = tcmMLStr

            fgAccessory.Cols(4).AllowEditing = False
            fgAccessory.Cols(5).AllowEditing = False
            fgAccessory.Cols(6).AllowEditing = False

            FormatGrid(fgAccessory, 9, 9)

        Catch ex As Exception
            Throw New Exception("InitfgAccssory()::" & ex.Message)
        End Try
    End Sub
    Private Sub FormatGrid(flexGrid As C1FlexGrid, captionFontSize As Short, normalStyleFontSize As Short)
        Try
            'adding Three-Dimensional Text to a Header Row
            Dim tdt As C1.Win.C1FlexGrid.CellStyle
            tdt = flexGrid.Styles.Add("3DText")
            tdt.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Raised
            flexGrid.Rows(0).Style = tdt

            'adding Row Numbers in a Fixed Column
            flexGrid.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw

            flexGrid.Styles.Fixed.TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            flexGrid.Styles.Fixed.Font = New Font("Arial", captionFontSize, FontStyle.Bold)

            For Each col As C1.Win.C1FlexGrid.Column In flexGrid.Cols
                col.TextAlign = TextAlignEnum.LeftCenter
            Next

            'set the border style property
            flexGrid.Styles("Normal").Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Flat
            flexGrid.Styles("Normal").Border.Color = SystemColors.ControlDarkDark
            flexGrid.Styles("Normal").Border.Color = Color.DarkGray
            flexGrid.Styles("Normal").Font = New Font("Arial", normalStyleFontSize, FontStyle.Bold)

            flexGrid.AutoSizeRows()
            flexGrid.AutoSizeCols()

        Catch ex As Exception
            Throw New Exception("FormatGrid()::" & ex.Message)
        End Try
    End Sub

    Private Sub fgAccessory_AfterEdit(sender As Object, e As RowColEventArgs) Handles fgAccessory.AfterEdit
        Try
            If e.Col = 2 Then
                Dim tcmM As CATS.Model.test_cable_model
                Dim tcmBll As New CATS.BLL.test_cable_modelManager
                tcmM = tcmBll.SelectByModelName(fgAccessory(e.Row, e.Col))
                fgAccessory(e.Row, "Tolerant Count") = tcmM.tolerant_count
            End If
            If e.Col = 3 Then
                If Not String.IsNullOrEmpty(fgAccessory(e.Row, e.Col)) Then
                    Dim tcmainM As CATS.Model.test_cable_main
                    Dim tcmainBll As New CATS.BLL.test_cable_mainManager
                    tcmainM = tcmainBll.SelectByCableSN(fgAccessory(e.Row, e.Col))
                    If tcmainM Is Nothing Then
                        fgAccessory(e.Row, "Test Count") = 0
                        fgAccessory(e.Row, "Register Time") = Now
                    Else
                        fgAccessory(e.Row, "Test Count") = tcmainM.test_count
                        fgAccessory(e.Row, "Register Time") = tcmainM.register_date_time
                        fgAccessory(e.Row, "Description") = tcmainM.descr
                    End If
                End If
            End If
            FormatGrid(fgAccessory, 9, 9)
        Catch ex As Exception
            MsgBox("fgAccessory_AfterEdit()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "After Edit Error")
        End Try
    End Sub

    Private Sub fgAccessory_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles fgAccessory.OwnerDrawCell
        Try
            If e.Row >= fgAccessory.Rows.Fixed And e.Col = fgAccessory.Cols.Fixed - 1 Then
                Dim rowNumber As Integer = e.Row - fgAccessory.Rows.Fixed + 1
                e.Text = rowNumber.ToString()
            End If
        Catch ex As Exception
            MsgBox("fgAccessory_OwnerDrawCell()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim tcmM As CATS.Model.test_cable_main
            Dim tcmBll As New CATS.BLL.test_cable_mainManager
            Dim selectedCount As Integer
            Dim r As Row
            For i As Integer = fgAccessory.Rows.Fixed To fgAccessory.Rows.Count - 1
                r = fgAccessory.Rows(i)
                If r(1) = True Then
                    selectedCount += 1
                End If
            Next

            If selectedCount > 2 Then
                MsgBox("More than 2 accessories are selected", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Selection Error")
                Return
            ElseIf selectedCount < 1 Then
                MsgBox("At least 1 accessory is selected", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Selection Error")
                Return
            End If

            If gTestAccessoryList IsNot Nothing Then gTestAccessoryList = Nothing
            gTestAccessoryList = New CTestAccessoryList
            Dim testAccessory As CTestAccessory

            For i As Integer = fgAccessory.Rows.Fixed To fgAccessory.Rows.Count - 1
                r = fgAccessory.Rows(i)
                If String.IsNullOrEmpty(r(2)) Or String.IsNullOrEmpty(r(3)) Then Continue For
                If r(1) = True Then
                    tcmM = tcmBll.SelectByCableSN(r(3))
                    If tcmM Is Nothing Then
                        tcmM = New CATS.Model.test_cable_main
                        tcmM.cable_model_id = (New CATS.BLL.test_cable_modelManager).SelectByModelName(r(2)).id
                        tcmM.cable_serial_num = r(3)
                        tcmM.test_count = r(5)
                        tcmM.register_date_time = r(6)
                        tcmM.descr = r(7)
                        tcmBll.Add(tcmM)
                    End If
                End If
                With gTestAccessoryList
                    testAccessory = New CTestAccessory
                    With testAccessory
                        .Enable = r(1)
                        .ModelName = r(2)
                        .SerialNumber = r(3)
                        .TolerantCount = r(4)
                        .RegisterTime = r(6)
                        .Description = r(7)
                    End With
                    .TestAccessoryList.Add(testAccessory)
                    .Save(gTestAccessoryListFileName)
                End With
            Next

            Close()

        Catch ex As Exception
            MsgBox("btnSave_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Save Accessory Error")
        End Try
    End Sub
End Class

Public Class CTestAccessory
    Public Enable As Boolean
    Public ModelName As String
    Public SerialNumber As String
    Public TolerantCount As Integer
    Public RegisterTime As DateTime
    Public Description As String
End Class
Public Class CTestAccessoryList
    Public TestAccessoryList As New List(Of CTestAccessory)
    Public Sub Save(ByVal filename As String)
        Try
            Dim XSerz As New XmlSerializer(Me.GetType)
            Dim StrmWt As New System.IO.StreamWriter(filename)
            XSerz.Serialize(StrmWt, Me)
            StrmWt.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Function CreateInstance(ByVal filename As String) As CTestAccessoryList
        Try
            If My.Computer.FileSystem.FileExists(filename) = False Then
                Return Nothing
            Else
                Dim _TestAccessoryList As CTestAccessoryList = Nothing
                Using StrmRd As New System.IO.StreamReader(filename)
                    Dim XSerz As New XmlSerializer(GetType(CTestAccessoryList))
                    _TestAccessoryList = CType(XSerz.Deserialize(StrmRd), CTestAccessoryList)
                    StrmRd.Close()
                End Using
                Return _TestAccessoryList
            End If
        Catch ex As Exception
            Throw New Exception(String.Format("Read file '{0}' fail!", filename) & vbNewLine & ex.Message)
        End Try
    End Function
End Class

