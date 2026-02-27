Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Collections.Generic
Public Class frmNewCable
    Private m_fileName As String = gTestSystemPath & "InputCables.xml"
    Private Sub frmNewCable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim _InputCable As InputCable
        If File.Exists(m_fileName) = True Then
            _InputCable = New InputCable(m_fileName)
            Dim acscSN, acscOLM, acscCoreNum, acscWO, acscTestLen, acscTemp As New AutoCompleteStringCollection
            For Each inputCable As CInputCable In _InputCable.Input_Cable_List
                acscSN.Add(inputCable.Cable.Serial_Number)
                acscOLM.Add(inputCable.Cable.Original_Length_M)
                acscCoreNum.Add(inputCable.Cable.Core_Number)
                acscWO.Add(inputCable.Cable.Work_Order)
                acscTestLen.Add(inputCable.Cable.Test_Length_M)
                acscTemp.Add(inputCable.Cable.Temperature_C)
            Next
            Me.txtCableNumber.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtCableNumber.AutoCompleteCustomSource = acscSN
            Me.txtCableNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtOriginalLength.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtOriginalLength.AutoCompleteCustomSource = acscOLM
            Me.txtOriginalLength.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtCoreNumber.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtCoreNumber.AutoCompleteCustomSource = acscCoreNum
            Me.txtCoreNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.txtWorkOrder.AutoCompleteSource = AutoCompleteSource.CustomSource
            Me.txtWorkOrder.AutoCompleteCustomSource = acscWO
            Me.txtWorkOrder.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        End If
        Try
            Me.cbCableType.Items.AddRange(TestObj.LoadAllCableType.ToArray)
        Catch ex As Exception
            MsgBox("FormEnterCable Load()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            NewCable = GetUserInput()
            If NewCable Is Nothing Then Return
            Dim _InputCable As InputCable
            If File.Exists(m_fileName) = False Then
                _InputCable = New InputCable(NewCable)
            Else
                _InputCable = New InputCable(m_fileName, NewCable)
            End If

            _InputCable.SaveXML(m_fileName)

            Me.DialogResult = DialogResult.OK
            'Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub CheckSN()
        Try
            If Me.txtCableNumber.Text = "" Then
                MsgBox("Cable number can't be empty", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                Me.txtCableNumber.Focus()
                Return
            End If
            Dim isFormatRight As Boolean = True

            Dim cableNumber As String = Me.txtCableNumber.Text
            If Not cableNumber.Contains("-") Then
                isFormatRight = False
            Else
                Dim splitString As String() = cableNumber.Split("-")
                If splitString.Length = 2 Then
                    If splitString(0).Length <> 5 Then
                        isFormatRight = False
                    End If
                Else
                    isFormatRight = False
                End If
            End If
            Dim pattern As String = "(\d{5})-(\d{2})(\D*)"
            Dim ma As Match = Regex.Match(cableNumber, pattern, RegexOptions.Compiled)
            If ma.Success = False Or isFormatRight = False Then
                If MessageBox.Show("SN: " & cableNumber & " format seems wrong, would you like to continue?", "Check SN Format", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    Me.txtCableNumber.Focus()
                    Me.txtCableNumber.SelectAll()
                    Return
                End If
            End If
            DbCable = TestObj.CheckSN(cableNumber)
            If DbCable IsNot Nothing Then
                If MsgBox("Cable Number: " & DbCable.Serial_Number & vbCrLf & "Cable Type: " & DbCable.Part_Number & vbCrLf & "already exists, do you want to retest?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = DialogResult.No Then
                    Me.txtCableNumber.Focus()
                    Me.txtCableNumber.SelectAll()
                    Return
                End If
                Me.cbCableType.Text = DbCable.Part_Number
                Me.txtOriginalLength.Text = DbCable.Original_Length_M
                Me.txtCoreNumber.Text = DbCable.Core_Number
                Me.txtWorkOrder.Text = DbCable.Work_Order
                Me.cbCableType.Enabled = False
                Me.txtOriginalLength.Enabled = False
                Me.txtCoreNumber.Enabled = False
                Me.txtWorkOrder.Enabled = False
                Me.cbTestConnector.Select()
                phaseExtendCable = TestObj.LoadPhaseExtendCable(DbCable.Serial_Number, DbCable.Part_Number)
                If phaseExtendCable IsNot Nothing Then
                    Me.cbTestConnector.Text = phaseExtendCable.test_connector
                    Me.cbTestConnector.Select()
                End If
            Else
                Dim dbCableInput As frmNewCable.CableInput = TestObj.GetDbCableInput(cableNumber)
                If dbCableInput IsNot Nothing Then
                    Me.cbCableType.Text = dbCableInput.cable_type
                    Me.txtOriginalLength.Text = dbCableInput.original_length
                    Me.txtCoreNumber.Text = dbCableInput.core_number
                    Me.txtWorkOrder.Text = dbCableInput.work_order
                    Me.cbTestConnector.Text = ""
                    Me.cbTestConnector.Focus()
                Else
                    Me.cbCableType.Text = ""
                    Me.txtOriginalLength.Text = ""
                    Me.txtCoreNumber.Text = ""
                    Me.txtWorkOrder.Text = ""
                    Me.cbCableType.Enabled = True
                    Me.txtOriginalLength.Enabled = True
                    Me.txtCoreNumber.Enabled = True
                    Me.txtWorkOrder.Enabled = True
                    Me.cbCableType.Focus()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("frmNewCable.CheckSN()::" & ex.Message)
        End Try
    End Sub

    Private Sub txtCableNumber_Leave(sender As Object, e As EventArgs) Handles txtCableNumber.Leave
        Try
            If cbCableType.Focused Then
                CheckSN()
            End If
        Catch ex As Exception
            MsgBox("frmNewCable.txtCableNumber_Leave()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Function GetUserInput() As Cable
        Try
            Dim resp As New Cable
            Dim cableNumber As String
            Dim cableType As String
            Dim originalLengthM As Decimal
            Dim coreNumber As String
            Dim workOrder As String
            Dim testConnector As String
            Dim testLengthM As Decimal = 0
            Dim temperatureC As Decimal = 0
            cableNumber = Me.txtCableNumber.Text
            If cbCableType.SelectedItem Is Nothing Then
                MsgBox("please choose a cable type", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                cbCableType.Focus()
                Return Nothing
            Else
                cableType = cbCableType.SelectedItem.ToString
            End If

            If txtOriginalLength.Text = "" Then
                MsgBox("please enter length", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                txtOriginalLength.Focus()
                Return Nothing
            Else
                If Decimal.TryParse(Me.txtOriginalLength.Text, originalLengthM) = False Then
                    MsgBox("please enter correct length", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                    txtOriginalLength.Focus()
                    txtOriginalLength.SelectAll()
                    Return Nothing
                End If
            End If

            If txtCoreNumber.Text = "" Then
                MsgBox("please enter core number", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                txtCoreNumber.Focus()
                Return Nothing
            Else
                coreNumber = txtCoreNumber.Text
            End If

            If txtWorkOrder.Text = "" Then
                MsgBox("please enter work order number", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                txtWorkOrder.Focus()
                Return Nothing
            Else
                workOrder = txtWorkOrder.Text
            End If

            If cbTestConnector.Text = "" Then
                MsgBox("please enter test connector", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
                cbTestConnector.Select()
                Return Nothing
            Else
                testConnector = cbTestConnector.Text
            End If
            If phaseExtendCable Is Nothing Then
                testLengthM = originalLengthM
            Else
                testLengthM = phaseExtendCable.test_length_m
            End If
            With resp
                resp.Serial_Number = cableNumber
                resp.Part_Number = cableType
                resp.Original_Length_M = originalLengthM
                resp.Core_Number = coreNumber
                resp.Work_Order = workOrder
                resp.Test_Connector = testConnector
                resp.Test_Length_M = testLengthM
                resp.Temperature_C = temperatureC
            End With

            Return resp

        Catch ex As Exception
            Throw New Exception("frmNewCable.GetUserInput()::" & ex.Message)
        End Try
    End Function

    Private Sub txtWorkOrder_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkOrder.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Or e.KeyChar = ChrW(22) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox("frmNewCable.txtWorkOrder_KeyPress()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub txtOriginalLengthFt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOriginalLength.KeyPress
        Try
            If Char.IsDigit(e.KeyChar) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(46) Or e.KeyChar = ChrW(22) Then
                If e.KeyChar = "." And InStr(txtOriginalLength.Text, ".") > 0 Then
                    e.Handled = True
                Else
                    e.Handled = False
                End If
            Else
                e.Handled = True
            End If
        Catch ex As Exception
            MsgBox("frmNewCable.txtOriginalLengthFt_KeyPress()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Class MeasPhaseExtendCable
        Public meas_phase_id As Integer
        Public test_connector As String
        Public test_length_m As Decimal
        Public temp_c As Decimal
        Public notes As String
    End Class
    Public Class CableInput
        Public cable_number As String
        Public cable_type As String
        Public original_length As Integer
        Public core_number As String
        Public work_order As String
    End Class
    Private Sub cbCableType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCableType.SelectedIndexChanged
        Try
            Me.cbTestConnector.Items.Clear()
            Me.cbTestConnector.Text = ""
            Me.cbTestConnector.Items.AddRange(TestObj.LoadTestConnector(Me.cbCableType.Text).ToArray)
        Catch ex As Exception
            MsgBox("frmNewCable.cbCableType_SelectedIndexChanged()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cbTestConnector_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbTestConnector.KeyPress
        Try
            If e.KeyChar = Chr(13) Then
                e.Handled = True
                SendKeys.Send("{TAB}")
            End If
        Catch ex As Exception
            MsgBox("frmNewCable.cbTestConnector_KeyPress()::" & ex.Message， MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub txtCableNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCableNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.cbCableType.Focus()
        End If
    End Sub

    Private Sub cbCableType_KeyDown(sender As Object, e As KeyEventArgs) Handles cbCableType.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.txtOriginalLength.Focus()
        End If
    End Sub

    Private Sub txtOriginalLength_KeyDown(sender As Object, e As KeyEventArgs) Handles txtOriginalLength.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.txtCoreNumber.Focus()
        End If
    End Sub

    Private Sub txtCoreNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCoreNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.txtWorkOrder.Focus()
        End If
    End Sub

    Private Sub txtWorkOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWorkOrder.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.cbTestConnector.Focus()
        End If
    End Sub

    Private Sub cbTestConnector_KeyDown(sender As Object, e As KeyEventArgs) Handles cbTestConnector.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.OK_Button.Focus()
        End If
    End Sub
End Class

Public Class CInputCable
    Public Cable As Cable
    Public id As Integer
End Class
Public Class InputCable

    Public Input_Cable_List As List(Of CInputCable)
    Public Sub New(newCable As Cable)
        Dim newInputCable As New CInputCable
        newInputCable.id = 1
        newInputCable.Cable = newCable
        Input_Cable_List = New List(Of CInputCable)
        Input_Cable_List.Add(newInputCable)
    End Sub
    Public Sub New(ByVal filename As String, newCable As Cable)
        Try
            Dim XDoc As New XmlDocument
            XDoc.Load(filename)
            Dim InputCableNode As XmlNode = XDoc.DocumentElement.SelectSingleNode("/InputCable")
            Input_Cable_List = New List(Of CInputCable)
            Dim newInputCable As CInputCable
            If InputCableNode.ChildNodes.Count > 0 Then
                For Each nd As XmlNode In InputCableNode.ChildNodes
                    newInputCable = New CInputCable
                    newInputCable.id = Integer.Parse(nd.Attributes("Number").Value)
                    newInputCable.Cable = New Cable(nd)
                    Input_Cable_List.Add(newInputCable)
                Next
            End If
            If Input_Cable_List.Count > 9 Then
                Input_Cable_List.RemoveAt(0)
            End If
            newInputCable = New CInputCable
            newInputCable.id = Input_Cable_List.FindLast(Function(o) o.id > 0).id + 1
            newInputCable.Cable = newCable
            Input_Cable_List.Add(newInputCable)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub New(ByVal filename As String)
        Try
            Dim XDoc As New XmlDocument
            XDoc.Load(filename)
            Dim InputCableNode As XmlNode = XDoc.DocumentElement.SelectSingleNode("/InputCable")
            Input_Cable_List = New List(Of CInputCable)
            Dim newInputCable As CInputCable
            If InputCableNode.ChildNodes.Count > 0 Then
                For Each nd As XmlNode In InputCableNode.ChildNodes
                    newInputCable = New CInputCable
                    newInputCable.id = Integer.Parse(nd.Attributes("Number").Value)
                    newInputCable.Cable = New Cable(nd)
                    Input_Cable_List.Add(newInputCable)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub SaveXML(ByVal filename As String)
        Try
            Dim XDoc As New XmlDocument
            Dim rtnd As XmlNode = XDoc.CreateElement("InputCable")
            Dim att As XmlAttribute = XDoc.CreateAttribute("date")
            att.Value = Now
            rtnd.Attributes.Append(att)
            XDoc.AppendChild(rtnd)


            For Each inputCable As CInputCable In Input_Cable_List
                Dim idNode As XmlNode = XDoc.CreateElement("ID")
                att = XDoc.CreateAttribute("Number")
                att.Value = inputCable.id
                idNode.Attributes.Append(att)
                rtnd.AppendChild(idNode)

                idNode.AppendChild(AddPara(XDoc, "SN", inputCable.Cable.Serial_Number))
                idNode.AppendChild(AddPara(XDoc, "Original_Length_M", inputCable.Cable.Original_Length_M))
                idNode.AppendChild(AddPara(XDoc, "Core_Number", inputCable.Cable.Core_Number))
                idNode.AppendChild(AddPara(XDoc, "Work_Order", inputCable.Cable.Work_Order))
                idNode.AppendChild(AddPara(XDoc, "Test_Length_M", inputCable.Cable.Test_Length_M))
                idNode.AppendChild(AddPara(XDoc, "Test_Connector", inputCable.Cable.Test_Connector))
                idNode.AppendChild(AddPara(XDoc, "Temperature", inputCable.Cable.Temperature_C))
            Next


            XDoc.Save(filename)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function AddPara(ByVal XDoc As XmlDocument, ByVal key As String, ByVal value As String) As XmlNode
        Try
            Dim nd As XmlNode = XDoc.CreateElement("Cable")

            Dim att As XmlAttribute = XDoc.CreateAttribute("Key")
            att.Value = key
            nd.Attributes.Append(att)

            att = XDoc.CreateAttribute("Value")
            att.Value = value
            nd.Attributes.Append(att)
            Return nd
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class