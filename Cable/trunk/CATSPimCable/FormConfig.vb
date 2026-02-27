Imports System.IO.Ports
Public Class FormConfig
    'Private Sub LoadTestMode()
    '	Try
    '		Dim testMode As String = pAppCfg.GetTestMode
    '		Dim dBTestModes As List(Of CATS.Model.mode)
    '		Dim mode As New CATS.BLL.modeManager
    '		dBTestModes = mode.SelectAll

    '		For Each m In dBTestModes
    '			cbMode.Items.Add(m.mode.Trim.ToUpper)
    '		Next
    '		cbMode.Text = testMode


    '	Catch ex As Exception
    '		Throw New Exception("DeviceSetup.LoadRetPara()::" & ex.Message)
    '	End Try
    'End Sub
    Private Sub LoadRetPara()
        Try
            Dim tpPara As CATSPimConfig.LocalConfig.RetPara

            tpPara = pAppCfg.GetRetConfig()

            'ckRetEnable.Checked = tpPara.Enable

            cbRetAddress.Items.Add("COM1")
            cbRetAddress.Items.Add("USBdATC200-LITE-USB")
            cbRetAddress.Text = tpPara.Address


        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadRetPara()::" & ex.Message)
        End Try
    End Sub
    Private Sub LoadVibrationPara()
        Try
            cbVibAddress.Items.Clear()

            Dim tpPara As CATSPimConfig.LocalConfig.VibrationPara

            For Each t In System.Enum.GetValues(GetType(VibCtrl.VibCtrlBoard))
                cbVibAddress.Items.Add(t.ToString)
            Next

            tpPara = pAppCfg.GetVibrationConfig()

            'ckRetEnable.Checked = tpPara.Enable
            ckVibEnable.Checked = tpPara.Enable
            'cbRetAddress.Items.Add("COM1")
            'cbRetAddress.Items.Add("USBdATC200-LITE-USB")


            cbVibAddress.Text = tpPara.Address


        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadVibrationPara()::" & ex.Message)
        End Try
    End Sub
    Private Sub LoadInstrmentsPara()
        Try

            dgvInstrs.AutoGenerateColumns = False

            Dim fband As New CATS.BLL.cfg_imd_freq_bandManager
            Dim fbandModel As New List(Of CATS.Model.cfg_imd_freq_band)
            fbandModel = fband.SelectAll()

            Dim ivBll As New CATS.BLL.instr_vendorManager
            Dim ivmL As List(Of CATS.Model.instr_vendor)
            ivmL = ivBll.SelectAll

            Dim pmBll As New CATS.BLL.phase_mainManager
            Dim pmModel As New List(Of CATS.Model.phase_main)
            pmModel = pmBll.SelectAll()

            Dim c1 As DataGridViewComboBoxColumn = dgvInstrs.Columns(1)
            Dim c2 As DataGridViewComboBoxColumn = dgvInstrs.Columns(2)
            Dim c3 As DataGridViewComboBoxColumn = dgvInstrs.Columns(3)
            'Dim c4 As DataGridViewComboBoxColumn = dgvInstrs.Columns(4)

            'c4.Items.Clear()

            'For Each com As String In System.IO.Ports.SerialPort.GetPortNames
            '    c4.Items.Add(com)
            'Next

            'For i As Integer = 1 To 24
            '    c4.Items.Add("COM" & i.ToString)
            'Next

            For Each fb As CATS.Model.cfg_imd_freq_band In fbandModel
                c1.Items.Add(fb.freq_band)
            Next

            For Each pm As CATS.Model.phase_main In pmModel
                c2.Items.Add(pm.phase)
            Next

            For Each ivm As CATS.Model.instr_vendor In ivmL
                c3.Items.Add(ivm.name)
            Next

            dgvInstrs.DataSource = pAppCfg.GetInstruments

        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadInstrmentsPara()::" & ex.Message)
        End Try
    End Sub
    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'LoadTestMode()
            'LoadRetPara()
            LoadInstrmentsPara()
            LoadVibrationPara()
        Catch ex As Exception
            MsgBox("DeviceSetup.FormLoad()::" & ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim dt As DataTable
            Dim rp As New CATSPimConfig.LocalConfig.RetPara
            Dim vp As New CATSPimConfig.LocalConfig.VibrationPara

            rp.Address = cbRetAddress.Text.ToUpper.Trim
            rp.Enable = True
            If pAppCfg.SaveRetConfig(rp) = False Then
                MsgBox("Save Ret test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            End If

            vp.Enable = ckVibEnable.Checked
            vp.Address = cbVibAddress.Text.ToUpper.Trim
            If pAppCfg.SaveVibrationConfig(vp) = False Then
                MsgBox("Save vibration test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            End If

            Dim rows As Integer = Me.dgvInstrs.Rows.Count
            For i As Integer = 0 To rows - 1
                If dgvInstrs.Rows(i).Cells(0).Value = True Then
                    If String.IsNullOrEmpty(Me.dgvInstrs.Rows(i).Cells("BandIdx").Value.ToString) Then
                        MsgBox("Band Index must not be empty.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        Me.dgvInstrs.CurrentCell = Me.dgvInstrs.Rows(i).Cells("BandIdx")
                        Return
                    End If
                End If
            Next

            dt = dgvInstrs.DataSource

            Dim dr() As DataRow = dt.Select("Enable = 'True'")
            If dr.Count = 0 Then
                MsgBox("Can't find PIM device configuration, please check at least one PIM device.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            If dr.Count = 2 Then
                If Not ((dr(0).Field(Of String)("Model").Contains("PIM700L") Or dr(0).Field(Of String)("Model").Contains("PIM700U")) AndAlso
                    (dr(1).Field(Of String)("Model").Contains("PIM700L") Or dr(1).Field(Of String)("Model").Contains("PIM700U"))) Then
                    MsgBox("Find multiple PIM device configuration, you can only check one PIM device except PIM700LU.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Return
                End If
            End If

            If dr.Count > 2 Then
                MsgBox("Find multiple PIM device configuration, you can only check one PIM device except PIM700LU.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            If pAppCfg.SaveInstruments(dt) = False Then
                MsgBox("Save instruments test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            MsgBox("Success!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)

        Catch ex As Exception
            MsgBox("DeviceSetup.btnSave_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub dgvInstrs_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvInstrs.CellMouseClick
        Try
            If e.ColumnIndex = 4 Then
                Dim cell As DataGridViewCell = dgvInstrs.Rows(e.RowIndex).Cells(e.ColumnIndex)
                Dim ComPorts() As String = System.IO.Ports.SerialPort.GetPortNames()
                If ComPorts.Count = 1 Then
                    cell.Value = ComPorts(0)
                ElseIf ComPorts.Count = 2 Then
                    cell.Value = ComPorts(1)
                End If
            End If
        Catch ex As Exception
            MsgBox("FormConfig.dgvInstrs_CellMouseClick()" & ex.Message, MsgBoxStyle.Exclamation & MsgBoxStyle.OkOnly)
        End Try

    End Sub
End Class