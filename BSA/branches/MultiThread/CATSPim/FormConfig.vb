Public Class FormConfig
  Private Sub LoadTestMode()
    Try
      Dim testMode As String = pAppCfg.GetTestMode
      Dim dBTestModes As List(Of CATS.Model.mode)
      Dim mode As New CATS.BLL.modeManager
      dBTestModes = mode.SelectAll

      For Each m In dBTestModes
        cbMode.Items.Add(m.mode.Trim.ToUpper)
      Next
      cbMode.Text = testMode


    Catch ex As Exception
      Throw New Exception("DeviceSetup.LoadRetPara()::" & ex.Message)
    End Try
  End Sub
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
      Dim tpPara As CATSPimConfig.LocalConfig.VibrationPara

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

      Dim c1 As DataGridViewComboBoxColumn = dgvInstrs.Columns(1)
      Dim c2 As DataGridViewComboBoxColumn = dgvInstrs.Columns(3)

      For Each fb In fbandModel
        c1.Items.Add(fb.freq_band)
      Next

      c2.Items.Add("Rosenberger")
      c2.Items.Add("Summitek")
      c2.Items.Add("Rflight")


      dgvInstrs.DataSource = pAppCfg.GetInstruments


    Catch ex As Exception
      Throw New Exception("DeviceSetup.LoadInstrmentsPara()::" & ex.Message)
    End Try
  End Sub
  Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      LoadTestMode()
      LoadRetPara()
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
      Dim mode As String

      mode = cbMode.Text.Trim.ToUpper
      If pAppCfg.SaveTestMode(mode) = False Then
        MsgBox("Save mode configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
      End If
      pGui.ModeName = mode

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

      dt = dgvInstrs.DataSource
      If pAppCfg.SaveInstruments(dt) = False Then
        MsgBox("Save instruments test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
      End If

      MsgBox("Success!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)

    Catch ex As Exception
      MsgBox("DeviceSetup.btnSave_Click::" & ex.Message)
    End Try
  End Sub

  Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
    Me.Close()
  End Sub
End Class