Imports System.Text.RegularExpressions
Imports System.IO
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
            Dim tpPara As CATSPimConfig.LocalConfig.VibrationPara

            For Each t In System.Enum.GetValues(GetType(VibCtrl.VibJsbBoard))
                cbVibAddress.Items.Add(t.ToString)
            Next

            tpPara = pAppCfg.GetVibrationConfig()

            'ckRetEnable.Checked = tpPara.Enable
            ckVibEnable.Checked = tpPara.Enable
            'cbRetAddress.Items.Add("COM1")
            'cbRetAddress.Items.Add("USBdATC200-LITE-USB")

            cbVibAddress.Text = tpPara.Address
            vib8222COM.Text = tpPara.COMPORT

        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadVibrationPara()::" & ex.Message)
        End Try
    End Sub
    Private Sub LoadInstrmentsPara()
        Try
            dgvInstrs.AutoGenerateColumns = False

            Dim fband As New CATS.BLL.cfg_imd_freq_bandManager
            Dim fbandModel As New List(Of CATS.Model.cfg_imd_freq_band)
            Dim ivBll As New CATS.BLL.instr_vendorManager
            Dim ivmL As List(Of CATS.Model.instr_vendor)

            fbandModel = fband.SelectAll()

            Dim c1 As DataGridViewComboBoxColumn = dgvInstrs.Columns(1)
            Dim c2 As DataGridViewComboBoxColumn = dgvInstrs.Columns(3)

            For Each fb In fbandModel
                c1.Items.Add(fb.freq_band)
            Next

            ivmL = ivBll.SelectAll
            For Each ivm In ivmL
                c2.Items.Add(ivm.name)
            Next
            'c2.Items.Add("Rosenberger")
            'c2.Items.Add("Summitek")
            'c2.Items.Add("Rflight")

            dgvInstrs.DataSource = pAppCfg.GetInstruments


        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadInstrmentsPara()::" & ex.Message)
        End Try
    End Sub
    Private Sub LoadMIIPara()
        Try
            Dim pcM As CATSPimConfig.LocalConfig.ProcessCheckPara
            pcM = pAppCfg.GetProcessCheck
            ckbProcesscheck.Checked = pcM.Enable

            'Dim tuM As CATSPimConfig.LocalConfig.TestUpdatePara
            'tuM = pAppCfg.GetTestupdate
            'ckbTestupdate.Checked = tuM.Enable


        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadMIIPara()::" & ex.Message)
        End Try
    End Sub

    Private Sub LoadCheckDoor() ' add by tony 0731  
        Try
            Dim Cd As CATSPimConfig.LocalConfig.ChamberDoor
            Cd = pAppCfg.GetDoorCheck
            CheckDoor.Checked = Cd.Enable

        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadCheckDoorPara()::" & ex.Message)
        End Try
    End Sub

    Private Sub LoadCheckChamberID() ' add by tony 0228
        Try
            Dim Cd As CATSPimConfig.LocalConfig.ChamberID
            Cd = pAppCfg.GetChamberIDCheck
            CheckChamberID.Checked = Cd.Enable

        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadLoadCheckChamberIDPara()::" & ex.Message)
        End Try
    End Sub

    Private Sub LoadCheckChamberVibration()
        Try
            Dim Cv As CATSPimConfig.LocalConfig.ChamberVibration
            Cv = pAppCfg.GetChamberVibrationCheck
            CheckVibration.Checked = Cv.Enable

        Catch ex As Exception
            Throw New Exception("DeviceSetup.LoadLoadCheckChamberVibrationPara()::" & ex.Message)
        End Try
    End Sub

    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            'LoadTestMode()
            LoadRetPara()
            LoadInstrmentsPara()
            LoadVibrationPara()
            LoadMIIPara()
            LoadCheckDoor()
            LoadCheckChamberID()
            LoadCheckChamberVibration()
            Me.Width = 855 '778
            Me.Height = 656
            Panel_Cal.Visible = False

            If cbVibAddress.Text.ToUpper.Trim = "SEALEVEL8222" Then
                vib8222COM.Visible = True
            Else
                vib8222COM.Visible = False
            End If

            '     CheckDoor.Enabled = False ' 将来导入需要屏蔽这行时需要强制使用。


            If pFactoryCode.ToUpper.Trim = "CN10".ToUpper Then
                If Environment.UserName.ToUpper.Trim = "zhyin".ToUpper Or Environment.UserName.ToUpper.Trim = "tx1006".ToUpper Or Environment.UserName.ToUpper.Trim = "XW1032".ToUpper Then  'Or Environment.UserName.ToUpper.Trim = "wji".ToUpper Or Environment.UserName.ToUpper.Trim = "jz8000".ToUpper Or Environment.UserName.ToUpper.Trim = "chyan".ToUpper Then
                    CheckChamberID.Enabled = True
                Else
                    CheckChamberID.Enabled = False
                End If

                If Environment.UserName.ToUpper.Trim = "zhyin".ToUpper Or Environment.UserName.ToUpper.Trim = "tx1006".ToUpper Or Environment.UserName.ToUpper.Trim = "chyan".ToUpper Then
                    OutPowersettingvalue.Enabled = True
                Else
                    OutPowersettingvalue.Enabled = False
                End If

            End If


        Catch ex As Exception
            MsgBox("DeviceSetup.FormLoad()::" & ex.Message)
        End Try

    End Sub

    Private Function savesetting()
        Try
            Dim dt As DataTable
            Dim rp As New CATSPimConfig.LocalConfig.RetPara
            Dim vp As New CATSPimConfig.LocalConfig.VibrationPara
            Dim Cd As New CATSPimConfig.LocalConfig.ChamberDoor
            Dim Ci As New CATSPimConfig.LocalConfig.ChamberID
            Dim Cv As New CATSPimConfig.LocalConfig.ChamberVibration
            'Dim mode As String

            limitup_outrange = 1.5

            'mode = cbMode.Text.Trim.ToUpper
            'If pAppCfg.SaveTestMode(mode) = False Then
            '	MsgBox("Save mode configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            'End If

            '===========================add xtx===================================================================
            For i = 0 To dgvInstrs.Rows.Count - 2
                '  If Trim(dgvInstrs.Rows(i).Cells(1).Value.ToString) <> "" And dgvInstrs.Rows(i).Cells(0).Value = True Then

                If Not (dgvInstrs.Rows(i).Cells(1).Value.Equals(DBNull.Value)) And Not (dgvInstrs.Rows(i).Cells(0).Value.Equals(DBNull.Value)) Then
                    If dgvInstrs.Rows(i).Cells(0).Value = True Then

                        If Trim(dgvInstrs.Rows(i).Cells(6).Value.ToString) = "" Then
                            MsgBox("Fail", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        If Trim(dgvInstrs.Rows(i).Cells(7).Value.ToString) = "" Then
                            MsgBox("Fail", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        If Trim(dgvInstrs.Rows(i).Cells(8).Value.ToString) = "" Then
                            MsgBox("Fail", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        If Trim(dgvInstrs.Rows(i).Cells(10).Value.ToString) = "" Then 'add by tony
                            MsgBox("Fail", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        'TxL
                        If CSng(Trim(dgvInstrs.Rows(i).Cells(6).Value)) >= limitup_outrange Or CSng(Trim(dgvInstrs.Rows(i).Cells(6).Value)) <= limitdown_outrange Then
                            MsgBox(Trim(dgvInstrs.Rows(i).Cells(1).Value) & "--> TxL Loss value is out of range, please check.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        'TxR
                        If CSng(Trim(dgvInstrs.Rows(i).Cells(7).Value)) >= limitup_outrange Or CSng(Trim(dgvInstrs.Rows(i).Cells(7).Value)) <= limitdown_outrange Then
                            MsgBox(Trim(dgvInstrs.Rows(i).Cells(1).Value) & "--> TxR Loss value is out of range, please check.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If
                        'Rx
                        If CSng(Trim(dgvInstrs.Rows(i).Cells(8).Value)) >= limitup_outrange Or CSng(Trim(dgvInstrs.Rows(i).Cells(8).Value)) <= limitdown_outrange Then
                            MsgBox(Trim(dgvInstrs.Rows(i).Cells(1).Value) & "--> Rx Loss value is out of range, please check.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            Return False
                        End If

                    End If
                End If
            Next
            '==============================================================================================

            rp.Address = cbRetAddress.Text.ToUpper.Trim
            rp.Enable = True
            If pAppCfg.SaveRetConfig(rp) = False Then
                MsgBox("Save Ret test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            vp.Enable = ckVibEnable.Checked
            vp.Address = cbVibAddress.Text.ToUpper.Trim
            vp.COMPORT = vib8222COM.Text.ToUpper.Trim
            If pAppCfg.SaveVibrationConfig(vp) = False Then
                MsgBox("Save vibration test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            '==================================add by tony
            Cd.Enable = CheckDoor.Checked
            If pAppCfg.SaveDoorCheck(Cd) = False Then
                MsgBox("Save Chamber Door configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            Ci.Enable = CheckChamberID.Checked
            If pAppCfg.SaveChamberIDCheck(Ci) = False Then
                MsgBox("Save ChamberID configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            Cv.Enable = CheckVibration.Checked
            If pAppCfg.SaveChamberVibrationCheck(Cv) = False Then
                MsgBox("Save ChamberVibration configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If
            '==================================================

            dt = dgvInstrs.DataSource
            If pAppCfg.SaveInstruments(dt) = False Then
                MsgBox("Save instruments test configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            Dim pcM As New CATSPimConfig.LocalConfig.ProcessCheckPara
            pcM.Enable = ckbProcesscheck.Checked

            pAppCfg.SaveProcessCheck(pcM)

            'Dim tuM As New CATSPimConfig.LocalConfig.TestUpdatePara
            'tuM.Enable = ckbTestupdate.Checked
            'pAppCfg.SaveTestupdate(tuM)


            'pGui.ModeName = mode
            'TestModules.LoadPhaseStation(mode)

            MsgBox("Saved successfully!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            Return True

        Catch ex As Exception
            MsgBox("DeviceSetup.btnSave_Click::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If savesetting() = False Then MsgBox("Configure file Saved Fail, please check your selection or unfinished items!", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    '==================================================================================================================================================================================================================
    'Power Calibration Start ,add by tony 20180719==============================================================================================================================================================================

    Private PowerMeasDevlist As New AndrewIntegratedProducts.InstrumentsFramework.PowerMeterInitializtion
    Private Caladdress As String
    Private success_action As Boolean = True
    Private limitup_procedure As Single = 1
    Private limitdown_procedure As Single = -0.4

    Private limitup_outrange As Single = 1.5
    Private limitdown_outrange As Single = -1.5

    Dim GetallFF As List(Of CATS.Model.cfg_imd_ffbox)

    Private pahse_main_name As String = ""
    Private instr_sn As String = ""
    Public instr_sn_Temp As String = ""
    Private Cal_data As CATS.Model.rec_imd_calibration
    Private TempIDN As String

    Private dev As DataModels.Instrument

    Dim Reset_700LU_enable As Boolean = True
    Dim InputSN_enable As Boolean = False

    Public Shared Function GetNumbers(ByVal str As String) As String
        Return Regex.Replace(str, "[a-z]", "", RegexOptions.IgnoreCase).Trim() '搜索str字符串中的"a到z的"字符，将其替换为空，忽略大小写
    End Function
    Private Sub CalibrateStart_Click(sender As Object, e As EventArgs) Handles CalibrateStart.Click
        Try

            OpenUSBDev.Enabled = True
            CloseDev.Enabled = False

            If Not (dgvInstrs.CurrentRow.Cells(1).Value.Equals(DBNull.Value)) Then
                If dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700L" Then
                    PowerMeasFrq = "700L"
                ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700U" Then
                    PowerMeasFrq = "700U"
                Else
                    PowerMeasFrq = GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "")
                End If
            End If

            PowCalFre.Text = PowerMeasFrq

            '1548, 1239
            '2504, 1239
            Me.Width = 1336 '1254
            Me.Height = 656

            Panel_Cal.Visible = True

#If Not DEBUG Then

            Dim s As String
            For Each s In PowerMeasDevlist.Initializtion()
                If s.Contains("INSTR") Then USBDeviceList.Items.Add(s)
            Next
#End If

            If USBDeviceList.Items.Count = 0 Then
                MsgBox("Don't find any Powersensor/meter(USB/GPIB/ASRL) device , please check !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Else
                USBDeviceList.SelectedIndex = 0
            End If

            CalibrateStart.Visible = False
            writeNULL()

            GetallFF = GetFixedFreqs0() ' 获取所有点频的频点

            If pFactoryCode.ToUpper.Trim = "CN10" Then
                AttenuationValue.Text = AttenuationValue.Items(2)
            Else
                AttenuationValue.Text = AttenuationValue.Items(0)
            End If

        Catch ex As Exception

            MsgBox("Error , please check !." & ex.ToString)

        End Try

    End Sub

    Private Sub USBDeviceList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles USBDeviceList.SelectedIndexChanged

        Caladdress = USBDeviceList.SelectedItem

        If Caladdress.ToUpper.Contains("USB") Then
            Dim devUSB As New AndrewIntegratedProducts.InstrumentsFramework.U2001A_PowerSensor
            devUSB.address = Caladdress
            devUSB.ReadIDN()
            PowerMeasDev = devUSB

        ElseIf Caladdress.ToUpper.Contains("GPIB") Then
            Dim devGPIB As New AndrewIntegratedProducts.InstrumentsFramework.E4418B
            devGPIB.address = Caladdress
            PowerMeasDev = devGPIB

        ElseIf Caladdress.ToUpper.Contains("ASRL") Then
            Dim devASRL As New AndrewIntegratedProducts.InstrumentsFramework.E4418B
            devASRL.address = Caladdress
            PowerMeasDev = devASRL

        End If

    End Sub

    Private Sub SettingDevice_Click(sender As Object, e As EventArgs) Handles SettingDevice.Click
        Try
            'writeNULL()
            PowerMeterCal_Message.Show()
            PowerMeterCal_Message.BackColor = Color.Yellow
            PowerMeterCal_Message.CalMessage.Items.Add(" Calibrting, please wait......")
            My.Application.DoEvents()

            PowerMeasDev.Cal(GetNumbers(Trim(PowCalFre.Text.Replace("L", "").Replace("U", ""))))

            PowerMeterCal_Message.BackColor = Color.Green
            PowerMeterCal_Message.CalMessage.Items.Add(" Calibrating success！　please continue.")
            My.Application.DoEvents()
            System.Threading.Thread.Sleep(1000)
            PowerMeterCal_Message.Hide()

        Catch ex As Exception
            PowerMeterCal_Message.BackColor = Color.Red
            PowerMeterCal_Message.CalMessage.Items.Add(" Calibrating Fail!  please continue.")
            My.Application.DoEvents()
            System.Threading.Thread.Sleep(1000)
            PowerMeterCal_Message.Close()
            Throw New Exception("Calibration error , please check !." & ex.Message)
            ' CalibratingDisplay.Visible = False
        End Try
    End Sub

    Private Sub OpenUSBDev_Click(sender As Object, e As EventArgs) Handles OpenUSBDev.Click
        ' writeNULL()
        Try
            If AttenuationValue.Text.Trim = "" Then
                MsgBox("Open Fail！　please select attenuation Value and continue.", MsgBoxStyle.OkOnly)
                Return
            End If

            OpenUSBDev.Enabled = False
            CloseDev.Enabled = True
            If PowerMeasDev.open(PowerMeasDev.Address) = True Then
                TempIDN = PowerMeasDev.IDN
            Else
                MsgBox("Open Fail！　please check and continue.", MsgBoxStyle.OkOnly)
            End If

        Catch ex As Exception
            MsgBox("Open Fail！　please check and continue.", MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Private Sub ResetDec_Click(sender As Object, e As EventArgs) Handles ResetDec.Click
        'writeNULL()
        PowerMeasDev.Reset()
    End Sub

    Private Sub CloseDev_Click(sender As Object, e As EventArgs) Handles CloseDev.Click
        'writeNULL()
        OpenUSBDev.Enabled = True
        CloseDev.Enabled = False
        PowerMeasDev.Close()
    End Sub

    '获取text 中频率信息
    'Private Sub GetF1_F2_Freq(ByRef F1_freq As Single, ByRef F2_freq As Single)

    '    Try
    '        Dim line As String
    '        Dim sr As StreamReader = New StreamReader(Application.StartupPath & "\" & "ZULUFreq_Cal.txt", System.Text.Encoding.Default)
    '        Do While sr.Peek() > 0
    '            line = sr.ReadLine()
    '            If line.Split(CChar("|"))(0).Trim = dgvInstrs.CurrentRow.Cells(1).Value.ToString.Trim Then
    '                F1_freq = line.Split(CChar("|"))(1).Trim
    '                F2_freq = line.Split(CChar("|"))(2).Trim
    '                Exit Do
    '            End If
    '            line = ""
    '        Loop
    '        sr.Close()
    '        sr = Nothing

    '    Catch ex As Exception
    '        Throw New Exception("Calibration error----Get Freq for ZULU , please check !." & ex.Message)
    '    End Try
    'End Sub

    Public Function GetFixedFreqs0() As List(Of CATS.Model.cfg_imd_ffbox) '获取 2tone的所有设备的频率点
        Try
            Dim resp As List(Of CATS.Model.cfg_imd_ffbox)
            Dim cff As New CATS.BLL.cfg_imd_ffboxManager
            resp = cff.SelectAll
            Return resp
        Catch ex As Exception
            Throw New Exception("GetFixedFreqs()::" & ex.Message)
        End Try
    End Function

    Private Function GetF1_F2_Freq(ByRef F1_freq As Single, ByRef F2_freq As Single) As Boolean
        F1_freq = 0
        F2_freq = 0
        ''----------------获取设备ID
        Dim ivBll As New CATS.BLL.instr_vendorManager
        Dim iv As CATS.Model.instr_vendor
        iv = ivBll.SelectByVendorName(dgvInstrs.CurrentRow.Cells(3).Value.ToString.Trim)
        If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & dgvInstrs.CurrentRow.Cells(3).Value.ToString.Trim & ">")

        Dim resp As CATS.Model.cfg_imd_ffbox
        Try
            If GetallFF IsNot Nothing Then
                For Each resp In GetallFF
                    If resp.band_name = dgvInstrs.CurrentRow.Cells(1).Value.ToString.Trim And resp.vendor_id = iv.id Then
                        F1_freq = resp.c1_freq
                        F2_freq = resp.c2_freq
                        Return True
                    End If
                Next
            End If

            Return False

        Catch ex As Exception
            Throw New Exception("GetFixedFreqs()::" & ex.Message)
            Return False
        End Try
    End Function

    Private Sub Reset_700LU()
        If Reset_700LU_enable = True Then
            For i = 0 To dgvInstrs.Rows.Count - 2
                If Not (dgvInstrs.Rows(i).Cells(1).Value.Equals(DBNull.Value)) Then
                    If dgvInstrs.Rows(i).Cells(1).Value.ToString = "LTE700L" Or dgvInstrs.Rows(i).Cells(1).Value.ToString = "LTE700U" Then
                        dgvInstrs.Rows(i).Cells(6).Value = ""
                        dgvInstrs.Rows(i).Cells(7).Value = ""
                        dgvInstrs.Rows(i).Cells(8).Value = ""
                        dgvInstrs.Rows(i).Cells(10).Value = ""

                        '===========================================================================================================================
                        If dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700L" Then
                            For j = 0 To dgvInstrs.Rows.Count - 2
                                If dgvInstrs.Rows(j).Cells(1).Value.ToString = "LTE700U" Then dgvInstrs.Rows(j).Cells(0).Value = False
                            Next
                        ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700U" Then
                            For k = 0 To dgvInstrs.Rows.Count - 2
                                If dgvInstrs.Rows(k).Cells(1).Value.ToString = "LTE700L" Then dgvInstrs.Rows(k).Cells(0).Value = False
                            Next
                        End If
                        '================================================================================================================================

                    End If
                End If
            Next
            Reset_700LU_enable = False
            Application.DoEvents()
        End If
    End Sub

    'start calibration
    Private Sub Start_Cal_Click(sender As Object, e As EventArgs) Handles Start_Cal.Click

        Dim PIMDeviceVendor As String
        Dim PIMDeviceAddress As String
        Dim PIMDeviceID As String
        ' Dim PIMDeviceSerialNum As String

        Dim f1_freq As Single = 0
        Dim f2_freq As Single = 0

        pahse_main_name = ""
        instr_sn = ""

        dev = New DataModels.Instrument
        Cal_data = New CATS.Model.rec_imd_calibration ' 清空
        Cal_data.power_meter_idn = TempIDN '功率计的IDN

        '==================================================================================================================
        If PowerCal_SN.Text.ToUpper.Contains("CAL03") And Trim(PowerCal_SN.Text.ToUpper).Length < 8 Then
            Cal_data.serial_number = PowerCal_SN.Text.ToUpper + Format(Now(), "yyyyMMddHHmmss")
        Else
            MsgBox("Please input SN again to start power calibration !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            PowerCal_SN.Text = ""
        End If

        If Trim(Cal_data.serial_number).Length > 30 Then ' 判断下SN的长度不能超过30，  20181123
            MsgBox("SN length should be < 30, Please input SN again  !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            PowerCal_SN.Text = ""
        End If
        '==================================================================================================================

        'correct the SN for power cal+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        CheckPhaseForPowCalSN = "Unknow"
        If Not (dgvInstrs.CurrentRow.Cells(1).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(0).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(3).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(4).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(5).Value.Equals(DBNull.Value)) Then
            If dgvInstrs.CurrentRow.Cells(0).Value = True And Trim(dgvInstrs.CurrentRow.Cells(1).Value.ToString) <> "" Then
                If dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700L" Or dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700U" Then
                    CheckPhaseForPowCalSN = "PIM" & GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "") & "LU" & "E"
                Else
                    CheckPhaseForPowCalSN = "PIM" & GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "") & "E"
                End If
            Else
                MsgBox("Please select test band , please check !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If
        Else
            MsgBox("Please check your selection !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return
        End If
        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



        Try
            If Sel_Automatic.Checked = True Then '自动初始化选择
                F1_sel.Checked = True
                F2_sel.Checked = False
                Cal_data.cal_type = "Automatic"
            Else
                Cal_data.cal_type = "manual" '手动获取序列号
#If Not DEBUG Then
                If instr_sn_Temp = "" Then
                    MsgBox("Please input the PIM device SN by manual,  then  Continue.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    GetSN.Show()
                    GetSN.DevSnForPim = ""
                    Return
                Else
                    instr_sn = instr_sn_Temp
                    ' dev.Model = instr_sn
                End If
#Else
                instr_sn="Debug0000001"
#End If

            End If


            '=================
            Dim OK700LU As Boolean = False
            Dim phaselist As String
            Cal_phasenamelist.Clear() '//add Cal


            For i = 0 To dgvInstrs.Rows.Count - 2
                If Not (dgvInstrs.Rows(i).Cells(1).Value.Equals(DBNull.Value)) And Not (dgvInstrs.Rows(i).Cells(0).Value.Equals(DBNull.Value)) Then
                    If dgvInstrs.Rows(i).Cells(0).Value = True And Trim(dgvInstrs.Rows(i).Cells(1).Value.ToString) <> "" Then

                        If dgvInstrs.Rows(i).Cells(1).Value.ToString = "LTE700L" Then
                            If OK700LU = False Then
                                phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "") & "LU"
                                OK700LU = True
                                Cal_phasenamelist.Add(phaselist) '//add Cal
                            End If
                        ElseIf dgvInstrs.Rows(i).Cells(1).Value.ToString = "LTE700U" Then
                            If OK700LU = False Then
                                phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "") & "LU"
                                OK700LU = True
                                Cal_phasenamelist.Add(phaselist) '//add Cal
                            End If

                        ElseIf dgvInstrs.Rows(i).Cells(1).Value.ToString = "LTE700W" Then
                            phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "") & "W"
                            Cal_phasenamelist.Add(phaselist) '//add Cal

                        ElseIf dgvInstrs.Rows(i).Cells(1).Value.ToString = "DCS1800N" Then
                            phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "") & "N"
                            Cal_phasenamelist.Add(phaselist) '//add Cal

                        ElseIf dgvInstrs.Rows(i).Cells(1).Value.ToString = "AWS1900" Then
                            phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "") & "AWS"
                            Cal_phasenamelist.Add(phaselist) '//add Cal

                        Else
                            phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "")
                            Cal_phasenamelist.Add(phaselist) '//add Cal
                        End If
                        '  phaselist = "PIM" & GetNumbers(dgvInstrs.Rows(i).Cells(1).Value.ToString).Replace("-", "")
                        'Cal_phasenamelist.Add(phaselist) '//add Cal
                    End If
                End If
            Next



            If Sel_Automatic.Checked = True Then PowerMeterCal_Message.Show()
            PowerMeterCal_Message.BackColor = Color.Yellow
            PowerMeterCal_Message.CalMessage.Items.Add(" Get all frequecy list...... ")
            For i = 0 To Cal_phasenamelist.Count - 1
                PowerMeterCal_Message.CalMessage.Items.Add(Cal_phasenamelist.Item(i))
            Next
            PowerMeterCal_Message.CalMessage.Items.Add("")
            PowerMeterCal_Message.CalMessage.Items.Add("Set frequency success!")
            PowerMeterCal_Message.CalMessage.Items.Add("")
            My.Application.DoEvents()
            '=================

            '---------------------------------------------------------------------------------
            If Not (dgvInstrs.CurrentRow.Cells(1).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(0).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(3).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(4).Value.Equals(DBNull.Value)) And Not (dgvInstrs.CurrentRow.Cells(5).Value.Equals(DBNull.Value)) Then
                If dgvInstrs.CurrentRow.Cells(0).Value = True And Trim(dgvInstrs.CurrentRow.Cells(1).Value.ToString) <> "" Then
                    If dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700L" Then
                        PowerMeasFrq = "700L"
                        Reset_700LU() '防止700LU只校准一个

                    ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700U" Then
                        PowerMeasFrq = "700U"
                        Reset_700LU() '防止700LU只校准一个
                    ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700W" Then
                        PowerMeasFrq = "700W"
                    ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "DCS1800N" Then
                        PowerMeasFrq = "1800N"
                    ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "AWS1900" Then
                        PowerMeasFrq = "1900AWS"
                    Else
                        PowerMeasFrq = GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "")
                    End If
                    ' PowerMeasFrq = GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "")
                    PowCalFre.Text = PowerMeasFrq
                    PIMDeviceVendor = dgvInstrs.CurrentRow.Cells(3).Value.ToString '确定选的是什么设备
                    PIMDeviceAddress = dgvInstrs.CurrentRow.Cells(4).Value.ToString '确定选的是什么设备地址
                    PIMDeviceID = dgvInstrs.CurrentRow.Cells(5).Value.ToString '确定选的是什么设备频段
                Else
                    MsgBox("Please select test band , please check !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Return
                End If
            Else
                MsgBox("Please check your selection !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            PowerMeterCal_Message.CalMessage.Items.Add(" Get current calibration info...... ")
            PowerMeterCal_Message.CalMessage.Items.Add("Frequency band :" & PowCalFre.Text)
            PowerMeterCal_Message.CalMessage.Items.Add("PIMDeviceVendor :" & PIMDeviceVendor)
            PowerMeterCal_Message.CalMessage.Items.Add("PIMDeviceAddress :" & PIMDeviceAddress)
            PowerMeterCal_Message.CalMessage.Items.Add("PIMDeviceBandID :" & PIMDeviceID)
            PowerMeterCal_Message.CalMessage.Items.Add("")
            My.Application.DoEvents()
            '-----------------------------------------------------------------------------------

            If PIMDeviceVendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then 'Or PIMDeviceVendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then 'ZULU  和 紫光 功率校准，最大值是44（offset 最大值 1）
                limitup_outrange = 1
            Else
                limitup_outrange = 1.5
            End If


            'ZULU不校准，直接保存0开始-----------------------------------------------------------------------------------
            ''If PIMDeviceVendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then
            ''    Sel_Manual.Checked = True
            ''    dgvInstrs.CurrentRow.Cells(6).Value = 0
            ''        dgvInstrs.CurrentRow.Cells(7).Value = 0
            ''        dgvInstrs.CurrentRow.Cells(8).Value = 0

            ''        If savesetting() = False Then '保存校准的offset值
            ''            MsgBox("Error! When save offset,  Please calibrate this band with PowerMeter again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            ''            Return
            ''        End If
            ''        PowerMeterCal_Message.CalMessage.Items.Add("Saved the offset value.")
            ''        My.Application.DoEvents()


            ''        Dim phase As String '= "PIM" & Trim(PowCalFre.Text)
            ''        If Trim(PowCalFre.Text).ToString = "700L" Then
            ''            phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "LU"
            ''        ElseIf Trim(PowCalFre.Text).ToString = "700U" Then
            ''            phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "LU"
            ''        Else
            ''            phase = "PIM" & Trim(PowCalFre.Text).Replace("-", "")
            ''        End If
            ''        If CalibrationRecord.INI_Write_Cal(phase.ToUpper.Trim, Environment.MachineName, Environment.UserName, "3") = False Then
            ''            MsgBox("Error! Please calibrate this band with PowerMeter again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            ''            Return
            ''        End If

            ''        PowerMeterCal_Message.CalMessage.Items.Add("Saved the calibration record, and OK！")
            ''        My.Application.DoEvents()

            ''        '==================提示优化
            ''        If Trim(PowCalFre.Text).ToString = "700L" Then
            ''            phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString) & "L"
            ''        ElseIf Trim(PowCalFre.Text).ToString = "700U" Then
            ''            phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString) & "U"
            ''        End If
            ''        '==================
            ''        MsgBox("Calibration Success for " & phase & "！　please continue.", MsgBoxStyle.OkOnly)
            ''        CalibrationRecord.Show()

            ''        PowerMeterCal_Message.BackColor = Color.Green
            ''        PowerMeterCal_Message.CalMessage.Items.Add("Calibration Success for " & phase & "！　please continue.")
            ''        My.Application.DoEvents()
            ''        System.Threading.Thread.Sleep(1000)
            ''        PowerMeterCal_Message.Close()
            ''        Return
            ''    End If
            'ZULU不校准，直接保存0结束-------------------------------------------------------------------------------------------------

#If Not DEBUG Then
            SettingFreq()
#End If

            If Sel_Automatic.Checked = True Then '仅仅打开设备，0  , 有设备的时候使用，手动不需要
                If ConfigPIMDevice(0, PIMDeviceVendor， PIMDeviceAddress, PIMDeviceID) = False Then  '设定PIM 设备F1输出功率并输出

                    If InputSN_enable = True Then '自动都不到后手动获取序列号
                        MsgBox("Please input the PIM device SN by manual,  then Click 'Start' again to Continue.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        InputSN_enable = False
                        GetSN.Show()
                        GetSN.DevSnForPim = ""
                        Return
                    End If

                    MsgBox("Error! Please check the PIM device is ok or not , or use manual to try again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Return
                End If
            End If

            If F1_sel.Checked = True Then
                'automation
                If Sel_Automatic.Checked = True Then

                    '针对ZULU,这里需要进行频率设置后再on/off, 所有设备都可以使用
                    ' If PIMDeviceVendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then
                    If GetF1_F2_Freq(f1_freq, f2_freq) = False Then
                        MsgBox("Error! when get F1_freq and F2_freq , and please try again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        Return
                    End If
                    pPimDev.SetFrequency(f1_freq, f2_freq) ' 这里需要设下频率才能切换通道，针对ZULU
                    ' End If

                    If ConfigPIMDevice(1, PIMDeviceVendor， PIMDeviceAddress, PIMDeviceID) = False Then  '设定PIM 设备F1输出功率并输出
                        MsgBox("Error! Please check the PIM device is ok or not , or use manual to try again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        Return
                    End If
                Else
                    MsgBox("Please check the power setting and only output F1！ Then continue.", MsgBoxStyle.OkOnly)
                End If

#If Not DEBUG Then
                Original_F1.Text = Getoriginalvalue()

#Else
                Original_F1.Text = 43
#End If

                If Not success_action Then
                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    Return
                End If

                Offset_F1.Text = Format((CSng(Trim(OutPowersettingvalue.Text)) - CSng(Trim(Original_F1.Text))), "0.0")
                ' Offset_F1.Text = Math.Round(CSng(Calibration_F1.Text - Original_F1.Text), 3)
                Calibration_F1.Text = Format((CSng(Trim(OutPowersettingvalue.Text)) + CSng(Trim(Offset_F1.Text))), "0.0")
                'TxL
                dgvInstrs.CurrentRow.Cells(6).Value = Offset_F1.Text
                'TxL
                If CSng(Trim(dgvInstrs.CurrentRow.Cells(6).Value)) >= limitup_outrange Or CSng(Trim(dgvInstrs.CurrentRow.Cells(6).Value)) <= limitdown_outrange Then
                    MsgBox(Trim(dgvInstrs.CurrentRow.Cells(1).Value) & "--> TxL Loss value is out of range, please check.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    dgvInstrs.CurrentRow.Cells(6).Value = 0
                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    Return
                End If
                'manual
                If Sel_Manual.Checked = True Then
                    MsgBox("Calibration Success for F1 ！　please continue.", MsgBoxStyle.OkOnly)
                Else
                    F1_sel.Checked = False
                    F2_sel.Checked = True
                End If

                PowerMeterCal_Message.CalMessage.Items.Add("Calibration Success for F1！")
                PowerMeterCal_Message.CalMessage.Items.Add("")
                My.Application.DoEvents()

            End If

            If F2_sel.Checked = True Then
                If Sel_Automatic.Checked = True Then
                    If ConfigPIMDevice(2, PIMDeviceVendor， PIMDeviceAddress, PIMDeviceID) = False Then '设定PIM 设备F2输出功率并输出
                        MsgBox("Error! Please check the PIM device is ok or not , or use manual to try again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        Return
                    End If
                Else
                    MsgBox("Please check the power setting and only output F2！ Then continue.", MsgBoxStyle.OkOnly)
                End If

#If Not DEBUG Then
                Original_F2.Text = Getoriginalvalue()
#Else
                Original_F2.Text = 43
#End If

                If Not success_action Then
                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    Return
                End If

                Offset_F2.Text = Format((CSng(Trim(OutPowersettingvalue.Text)) - CSng(Trim(Original_F2.Text))), "0.0")
                'Offset_F2.Text = Math.Round(CSng(Calibration_F2.Text - Original_F2.Text), 3)
                Calibration_F2.Text = Format((CSng(Trim(OutPowersettingvalue.Text)) + CSng(Trim(Offset_F2.Text))), "0.0")
                'TxR
                dgvInstrs.CurrentRow.Cells(7).Value = Offset_F2.Text
                'TxR
                If CSng(Trim(dgvInstrs.CurrentRow.Cells(7).Value)) >= limitup_outrange Or CSng(Trim(dgvInstrs.CurrentRow.Cells(7).Value)) <= limitdown_outrange Then
                    MsgBox(Trim(dgvInstrs.CurrentRow.Cells(1).Value) & "--> TxR Loss value is out of range, please check.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    dgvInstrs.CurrentRow.Cells(7).Value = 0
                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    Return
                End If
                If Sel_Manual.Checked = True Then
                    MsgBox("Calibration Success for F2！　please continue.", MsgBoxStyle.OkOnly)
                Else
                    F1_sel.Checked = True
                    F2_sel.Checked = False
                End If
                PowerMeterCal_Message.CalMessage.Items.Add("Calibration Success for F2！")
                PowerMeterCal_Message.CalMessage.Items.Add("")
                My.Application.DoEvents()
            End If

            '保存校准信息和时间
            If Offset_F1.Text <> "null" And Offset_F2.Text <> "null" Then

                If Sel_Automatic.Checked = True Then ' 自动时
                    If CheckCalibrateResult() = False Then
                        MsgBox("Error! Please calibrate this band with PowerMeter again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        pPimDev.RFPowerOnOff_OnePort(False, False)
                        ' pPimDev.Close()
                        Return
                    End If '确认校准结果

                    pPimDev.RFPowerOnOff_OnePort(False, False)
                    pPimDev.SetRFPower(CDbl(Trim(OutPowersettingvalue.Text)), CDbl(Trim(OutPowersettingvalue.Text))) ' 设置power =43dBm

                    PowerMeterCal_Message.CalMessage.Items.Add("Check the power again, and OK！")
                    My.Application.DoEvents()
                End If

                'Rx
                ' dgvInstrs.CurrentRow.Cells(8).Value = Format((CSng(Offset_F1.Text) + CSng(Offset_F2.Text)) / 2, "0.0")
                dgvInstrs.CurrentRow.Cells(8).Value = 0

                'DevSeriseNum
                dgvInstrs.CurrentRow.Cells(10).Value = instr_sn

                '==========================
                'If savesetting() = False Then '保存校准的offset值
                '    MsgBox("Error! When save offset,  Please calibrate this band with PowerMeter again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                '    Return
                'End If
                'PowerMeterCal_Message.CalMessage.Items.Add("Saved the offset value.")
                '=============================
                My.Application.DoEvents()

                Dim phase As String '= "PIM" & Trim(PowCalFre.Text)
                If Trim(PowCalFre.Text).ToString = "700L" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "LU"
                ElseIf Trim(PowCalFre.Text).ToString = "700U" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "LU"
                ElseIf Trim(PowCalFre.Text).ToString = "700W" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "W"
                ElseIf Trim(PowCalFre.Text).ToString = "1800N" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "N"
                ElseIf Trim(PowCalFre.Text).ToString = "1900AWS" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString).Replace("-", "") & "AWS"
                Else
                    phase = "PIM" & Trim(PowCalFre.Text).Replace("-", "")
                End If

                If CalibrationRecord.INI_Write_Cal(phase.ToUpper.Trim, Environment.MachineName, Environment.UserName, "0", "3") = False Then
                    MsgBox("Error! Please calibrate this band with PowerMeter again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Return
                End If

                pahse_main_name = phase

                If add_caldata() = True Then
                    ' MsgBox("Save data to server success")
                Else
                    MsgBox("Save data to server fail")
                End If

                instr_sn_Temp = ""

                PowerMeterCal_Message.CalMessage.Items.Add("Saved the calibration record, and OK！")
                My.Application.DoEvents()

                '==================提示优化
                If Trim(PowCalFre.Text).ToString = "700L" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString) & "L"
                ElseIf Trim(PowCalFre.Text).ToString = "700U" Then
                    phase = "PIM" & GetNumbers(Trim(PowCalFre.Text).ToString) & "U"
                End If
                '==================
                MsgBox("Finished to calibrate for " & phase & "！　please continue, and please Click 'Save' before exit", MsgBoxStyle.OkOnly)
                CalibrationRecord.Show()

                PowerMeterCal_Message.BackColor = Color.Green
                PowerMeterCal_Message.CalMessage.Items.Add("Finished to calibrate for " & phase & "！　please continue, and please Click 'Save' before exit")
                My.Application.DoEvents()
                System.Threading.Thread.Sleep(1000)
                PowerMeterCal_Message.Close()

                Return
            End If

        Catch ex As Exception
            Throw New Exception("Calibration error , please check !." & ex.Message)
            Return
        Finally
            If Sel_Automatic.Checked = True Then pPimDev.Close()


        End Try

    End Sub

    '读原始值
    Private Function Getoriginalvalue() As Single ' 读5次取平均，只要有一次超出范围就fail,  -1.4/2， 超出范围说明设备有问题了，误差太大了
        Try
            success_action = True
            Dim i As Integer = 0
            Dim value As Single = 0
            Dim tempvalue As Single = 0
            Dim LimitUp As Single = CSng(OutPowersettingvalue.Text) + limitup_outrange
            Dim Limitdown As Single = CSng(OutPowersettingvalue.Text) + limitdown_outrange

            Do Until i = 3
                tempvalue = PowerMeasDev.Meaurement() + CSng(AttenuationValue.Text)
                If tempvalue < LimitUp And tempvalue > Limitdown Then
                    i = i + 1
                    value = value + tempvalue
                Else
                    MsgBox("Test value out of range ! --> " & tempvalue & " > “ & LimitUp & " Or “ & tempvalue & " < “ & Limitdown, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    ' MsgBox("Test value out of spec , please check !.")
                    i = 1
                    success_action = False
                    Return 0
                End If
            Loop
            ' Return Math.Round(value / 5, 3)
            Return Format(value / 3, "0.0")

        Catch ex As Exception
            success_action = False
            Throw New Exception("Getoriginalvalue error , please check !." & ex.Message)
            Return 0
        End Try

    End Function

    Private Sub Cal_Done_Click(sender As Object, e As EventArgs) Handles Cal_Done.Click
        Try
            Me.Width = 778
            Me.Height = 656
            ' Me.Size = New Size(1548, 1239)
            Panel_Cal.Visible = False
            CalibrateStart.Visible = True
            USBDeviceList.Items.Clear()
        Catch ex As Exception
            Throw New Exception("Done error , please check !." & ex.Message)
        End Try
    End Sub

    Private Sub dgvInstrs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInstrs.CellClick
        Try
            If CalibrateStart.Visible = False And Not (dgvInstrs.CurrentRow.Cells(1).Value.Equals(DBNull.Value)) Then
                If dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700L" Then
                    PowerMeasFrq = "700L"
                ElseIf dgvInstrs.CurrentRow.Cells(1).Value.ToString = "LTE700U" Then
                    PowerMeasFrq = "700U"
                Else
                    PowerMeasFrq = GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString).Replace("-", "")
                End If
                ' PowerMeasFrq = GetNumbers(dgvInstrs.CurrentRow.Cells(1).Value.ToString)
                PowCalFre.Text = PowerMeasFrq
                writeNULL()

            End If
        Catch ex As Exception
            Throw New Exception("Select test band error , please check !." & ex.Message)
            Return
        End Try

    End Sub

    Private Sub writeNULL()
        Original_F1.Text = "null"
        Calibration_F1.Text = "null"
        Offset_F1.Text = "null"
        Original_F2.Text = "null"
        Calibration_F2.Text = "null"
        Offset_F2.Text = "null"
        Finally_F1.Text = "null"
        Finally_F2.Text = "null"
    End Sub

    Private Sub PowCalFre_TextChanged(sender As Object, e As EventArgs) Handles PowCalFre.TextChanged, OutPowersettingvalue.TextChanged
        writeNULL()
    End Sub

    Private Sub ReCal_Click(sender As Object, e As EventArgs) Handles ReCal.Click
        writeNULL()
    End Sub

    'add PIM device control start=================================================================================================================

    '设定PIM 设备输出功率并输出,准备校准
    Dim IMDdelaytime As Integer = 3000
    Private Function ConfigPIMDevice(ByVal Port As Integer, ByVal PIMDeviceVendor As String， ByVal PIMDeviceAddress As String, ByVal PIMDeviceID As String) As Boolean '设定PIM 设备F1输出功率并输出

        If Port = 0 Then
            Try

                If PIMDeviceVendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                    Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                    devRos.Address = PIMDeviceAddress
                    devRos.Open()

                    dev.Idn = devRos.ReadIDN
                    dev.SerialNumber = devRos.Serial_Number
                    dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
                    dev.Hardware = ""
                    dev.Firmware = devRos.FW_Revision

                    devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devRos.FreqBand = PIMDeviceID
                    devRos.RFPowerOnOff_TwoPorts(False)

                    'devRos.Close()

                    '‘  resp.Add(dev)
                    IMDdelaytime = 3000
                    pPimDev = devRos

                    '=======================================================
                ElseIf PIMDeviceVendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then
                    Dim devZUl As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                    devZUl.Address = PIMDeviceAddress
                    devZUl.Open()
                    devZUl.FreqBandSet = dgvInstrs.CurrentRow.Cells(1).Value.ToString '切换频段

                    dev.Idn = "Commscope Zulu " & devZUl.FilterBandName & "_" & devZUl.Serial_Number
                    dev.Model = devZUl.Model
                    dev.SerialNumber = devZUl.Serial_Number
                    dev.Hardware = "1.0"
                    dev.Firmware = devZUl.FW_Revision

                    devZUl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devZUl.RFPowerOnOff_TwoPorts(False)

                    'devZUl.Close()

                    'resp.Add(dev)

                    pPimDev = devZUl

                    '  MsgBox(pPimDev.GetDTP_Time, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

                    '=======================================================================

                ElseIf PIMDeviceVendor.Trim.ToUpper = "Summitek".ToString.ToUpper Or PIMDeviceVendor.Trim.ToUpper = "SI-A".ToString.ToUpper Then
                    Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                    devSum.Address = PIMDeviceAddress
                    devSum.Open()

                    dev.Idn = " "
                    dev.SerialNumber = devSum.Serial_Number
                    dev.Model = devSum.Model
                    dev.Hardware = " "
                    dev.Firmware = " " 'devSum.FW_Revision

                    devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devSum.FreqBand = PIMDeviceID
                    devSum.RFPowerOnOff_TwoPorts(False)

                    'devSum.Close()

                    ''  resp.Add(dev)
                    IMDdelaytime = 3000
                    pPimDev = devSum

                ElseIf PIMDeviceVendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                    Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                    devKae.Address = PIMDeviceAddress
                    devKae.Open()

                    dev.Idn = ""
                    dev.Model = devKae.Model
                    dev.SerialNumber = devKae.Serial_Number
                    dev.Hardware = ""
                    dev.Firmware = ""

                    devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devKae.FreqBand = PIMDeviceID
                    devKae.RFPowerOnOff_TwoPorts(False)

                    'devKae.Close()

                    ''  resp.Add(dev)
                    IMDdelaytime = 3000
                    pPimDev = devKae

                ElseIf PIMDeviceVendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                    Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                    devRfl.Address = PIMDeviceAddress
                    devRfl.Open()

                    dev.Idn = devRfl.ReadIDN
                    dev.SerialNumber = devRfl.Serial_Number
                    dev.Model = devRfl.Model
                    dev.Hardware = ""
                    dev.Firmware = devRfl.FW_Revision

                    devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devRfl.FreqBand = PIMDeviceID
                    devRfl.RFPowerOnOff_TwoPorts(False)

                    'devRfl.Close()

                    '' resp.Add(dev)
                    IMDdelaytime = 3000
                    pPimDev = devRfl

                ElseIf PIMDeviceVendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                    Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                    ' devJoc.Address = PIMDeviceAddress

                    devJoc.Address = PIMDeviceAddress.Split(CChar(":"))(0).Trim  'add by tony 20190529
                    devJoc.Port_Select = PIMDeviceAddress.Split(CChar(":"))(1).Trim 'add by tony 20190529
                    If devJoc.Port_Select <> 1 And devJoc.Port_Select <> 2 Then Throw New Exception("InitPimDevice():: JoinCom Port selection") 'add by tony 20190529

                    devJoc.Open()

                    dev.Idn = devJoc.ReadIDN
                    dev.SerialNumber = devJoc.Serial_Number
                    dev.Model = devJoc.Model
                    dev.Hardware = ""
                    dev.Firmware = devJoc.FW_Revision

                    devJoc.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devJoc.FreqBand = PIMDeviceID
                    devJoc.RFPowerOnOff_TwoPorts(False)

                    'devRfl.Close()

                    ''  resp.Add(dev)
                    IMDdelaytime = 3000
                    pPimDev = devJoc
                Else
                    Return False
                End If

                '=======================================================================
                If dev.SerialNumber = "" Or dev.SerialNumber Is Nothing Then

                    If instr_sn_Temp = "" Then
                        InputSN_enable = True
                        Return False
                    Else
                        instr_sn = instr_sn_Temp
                        ' If dev.Model = "" Or dev.Model Is Nothing Then dev.Model = instr_sn
                    End If
                Else
                    instr_sn = dev.SerialNumber
                End If

                '=======================================================================

                Return True

            Catch ex As Exception
                Throw New Exception("PIM power output error , please check !." & ex.Message)
                Return False
            End Try
        Else
            Try
                pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                pPimDev.SetRFPower(CDbl(Trim(OutPowersettingvalue.Text)), CDbl(Trim(OutPowersettingvalue.Text))) ' 设置power =43dBm


                If Port = 1 Then
                    pPimDev.RFPowerOnOff_OnePort(True, False)  '单独打开一个通道
                    System.Threading.Thread.Sleep(IMDdelaytime)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
                    System.Threading.Thread.Sleep(100)
                ElseIf Port = 2 Then
                    pPimDev.RFPowerOnOff_OnePort(False, True)
                    System.Threading.Thread.Sleep(IMDdelaytime)
                    pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
                    System.Threading.Thread.Sleep(100)
                End If

                'System.Threading.Thread.Sleep(IMDdelaytime)
                Return True

            Catch ex As Exception
                Throw New Exception("PIM power output error , please check !." & ex.Message)
                Return False
            End Try
        End If
    End Function

    Private Function CheckCalibrateResult() As Boolean '确认校准结果

        pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
        pPimDev.SetRFPower(CDbl(Trim(Calibration_F1.Text)), CDbl(Trim(Calibration_F2.Text))) ' 设置power 校准后的设定值

        pPimDev.RFPowerOnOff_OnePort(True, False)  '单独打开一个通道
        System.Threading.Thread.Sleep(IMDdelaytime)
        pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTA)
        System.Threading.Thread.Sleep(100)
        If CheckCalibratelvalue(1) = False Then Return False

        pPimDev.RFPowerOnOff_OnePort(False, True)
        System.Threading.Thread.Sleep(IMDdelaytime)
        pPimDev.CorrectRFPower(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumRFPORTS.PORTB)
        System.Threading.Thread.Sleep(100)
        If CheckCalibratelvalue(2) = False Then Return False

        Return True
    End Function

    Private Function CheckCalibratelvalue(ByVal Port As Integer) As Boolean ' 读5次取平均，只要有一次超出规格就fail,  -0.4/1
        Try
            Dim i As Integer = 0
            Dim value As Single = 0
            Dim tempvalue As Single = 0
            Dim LimitUp As Single = CSng(OutPowersettingvalue.Text) + limitup_procedure  '1
            Dim Limitdown As Single = CSng(OutPowersettingvalue.Text) + limitdown_procedure  '-0.4

            Do Until i = 3
                tempvalue = PowerMeasDev.Meaurement() + CSng(AttenuationValue.Text)
                If tempvalue < LimitUp And tempvalue > Limitdown Then
                    i = i + 1
                    value = value + tempvalue
                Else
                    pPimDev.RFPowerOnOff_OnePort(False, False) '只要超过规格，先关闭功率，再提示。
                    MsgBox("Test value out of range ! --> " & tempvalue & " > “ & LimitUp & " Or “ & tempvalue & " < “ & Limitdown, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    i = 1
                    Return False
                End If
            Loop

            'Return Format(value / 5, "0.0")
            If Port = 1 Then Finally_F1.Text = Format(value / 3, "0.0")
            If Port = 2 Then Finally_F2.Text = Format(value / 3, "0.0")

            Return True

        Catch ex As Exception
            Throw New Exception("Calibration fail , please check !." & ex.Message)
            Return False
        End Try

    End Function

    Private Sub SettingFreq()
        Try
            PowerMeasDev.Configure(GetNumbers(Trim(PowCalFre.Text.Replace("L", "").Replace("U", ""))))
        Catch ex As Exception
            Throw New Exception("Set frequcncy error for sensor , please check !." & ex.Message)
        End Try
    End Sub

    Private Sub cbVibAddress_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbVibAddress.SelectedIndexChanged
        If cbVibAddress.Text.ToUpper.Trim = "SEALEVEL8222" Then
            vib8222COM.Visible = True
            vib8222COM.Text = "COM1"
        Else
            vib8222COM.Visible = False
            vib8222COM.Text = "NA"
        End If
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    'Dim f1_freq As Single = 0
    '    'Dim f2_freq As Single = 0
    '    ' GetF1_F2_Freq(F1_freq, F2_freq)

    '    GetFixedFreqs0()
    'End Sub
    'add PIM device control end=================================================================================================================


    'add save calibration data to server start==================================================================================================
    Private Function get_phase_main_id() As Integer '只查，所以有外键约束也没关系，这里phase_main 是主键 ok
        Try
            Dim pp As New CATS.Model.phase_main
            Dim pp_ph As New CATS.BLL.phase_mainManager
            pp = pp_ph.SelectByPhase(pahse_main_name)

            If pp IsNot Nothing Then
                Return pp.id
            Else
                Throw New Exception("Don't get phase_Main_ID from Database")
                ' Return 0 'ok                
            End If
        Catch ex As Exception
            Throw New Exception("PowerCal:get_phase_main_id:: " & ex.Message)
            'Return 0
        End Try
    End Function

    Private Function get_controller_id() As Integer '没有外键约束，ok
        Try
            Dim pp As New CATS.Model.controller
            Dim tp As New CATS.Model.controller
            Dim pp_ph As New CATS.BLL.controllerManager

            pp = pp_ph.SelectByName(Environment.MachineName)

            If pp IsNot Nothing Then
                Return pp.id
            Else
                tp.name = Environment.MachineName
                tp.location = ""
                tp.owner_id = 0 '
                tp.factory_id = pFactoryID
                tp.gen1 = ""
                tp.gen2 = ""
                tp.gen3 = ""
                If pp_ph.Add(tp) = True Then
                    pp = pp_ph.SelectByName(Environment.MachineName)
                    If pp IsNot Nothing Then
                        Return pp.id
                    Else
                        Throw New Exception("Don't get controller_ID_0 from Database")
                        'Return 1
                    End If
                Else
                    Throw New Exception("Don't get controller_ID_1 from Database")
                    'Return 1
                End If
            End If

        Catch ex As Exception
            Throw New Exception("PowerCal:get_controller_id:: " & ex.Message)
            ' Return 1
        End Try
    End Function


    '   dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
    Private Function Getinstr_model_id(ByVal ModelName As String) As Integer

        Try
            Dim instr_model0 As New CATS.Model.instr_model ' 主表
            Dim instr_model1 As List(Of CATS.Model.instr_model)
            Dim instr_model2 As New CATS.BLL.instr_modelManager

            If ModelName Is Nothing Then ModelName = "Unknown"

            instr_model1 = instr_model2.SelectAll()
            If instr_model1 IsNot Nothing Then
                For Each mm In instr_model1
                    If mm.name = ModelName Then Return mm.id
                Next
            Else
                Throw New Exception("Don't get instr_model_0 informaiton from Database")
                ' Return 1
            End If

            instr_model0.name = ModelName
            instr_model0.model_num = ""
            instr_model0.start_freq = 0
            instr_model0.stop_freq = 0
            instr_model0.type = ""
            instr_model0.instr_vendor_id = 0

            If instr_model2.Add(instr_model0) = True Then

                instr_model1 = instr_model2.SelectAll()
                If instr_model1 IsNot Nothing Then
                    For Each mm In instr_model1
                        If mm.name = ModelName Then Return mm.id
                    Next
                    Throw New Exception("Don't get instr_model_3 informaiton from Database")
                Else
                    Throw New Exception("Don't get instr_model_1 informaiton from Database")
                    ' Return 1
                End If
            Else
                Throw New Exception("Don't get instr_model_2 informaiton from Database")
                'Return 1
            End If

        Catch ex As Exception
            Throw New Exception("PowerCal:get instr_model:: " & ex.Message)
            ' Return 1
        End Try
    End Function

    Private Function get_pim_instr_main_id() As Integer 'ok

        Try
            Dim pp As New List(Of CATS.Model.instr_main) ' 外键，与instr_model有外键约束关系，
            Dim pp_ph As New CATS.BLL.instr_mainManager
            Dim tp As New CATS.Model.instr_main

            pp = pp_ph.SelectBySN(instr_sn)

            If pp IsNot Nothing Then
                Return pp(0).id
            Else
                ' If dev.Idn Is Nothing Then dev.Idn = instr_sn'

                tp.instr_model_id = Getinstr_model_id(dev.Model) '新设备头一次使用时，先校准，需要保存信息到数据库，
                tp.serial_num = instr_sn 'dev.SerialNumber
                tp.instr_num = ""
                tp.fw_version = dev.Firmware
                tp.hw_version = dev.Hardware
                tp.entry_date = Now
                tp.location = ""
                tp.status = ""
                tp.employee_id = 0
                tp.instr_idn = dev.Idn
                If pp_ph.Add(tp) = True Then
                    pp = pp_ph.SelectBySN(instr_sn)
                    If pp IsNot Nothing Then
                        Return pp(0).id
                    Else
                        Throw New Exception("Don't get_pim_instr_main_ID_0 informaiton from Database")
                        ' Return 1
                    End If
                Else
                    Throw New Exception("Don't get_pim_instr_main_ID_1 informaiton from Database")
                    ' Return 1
                End If
            End If

        Catch ex As Exception
            Throw New Exception("PowerCal:get_pim_instr_main_id:: " & ex.Message)
            ' Return 1
        End Try

    End Function


    'pFactory
    Private Function get_factory_id(ByVal factoryName As String) As Integer 'ok
        Try
            Dim pp As New CATS.Model.factory
            Dim tp As New CATS.Model.factory
            Dim pp_ph As New CATS.BLL.factoryManager

            pp = pp_ph.SelectByName(factoryName)

            If pp IsNot Nothing Then
                Return pp.id
            Else
                tp.name = factoryName
                If pp_ph.Add(tp) = True Then
                    pp = pp_ph.SelectByName(factoryName)
                    If pp IsNot Nothing Then
                        Return pp.id
                    Else
                        Throw New Exception("Don't get_factory_ID_0 informaiton from Database")
                        'Return 1
                    End If
                Else
                    Throw New Exception("Don't get_factory_ID_1 informaiton from Database")
                    ' Return 1
                End If
            End If

        Catch ex As Exception
            Throw New Exception("PowerCal:get_factory_id:: " & ex.Message)
            'Return 1
        End Try

    End Function


    Private Function get_employee_id() As Integer 'ok
        Try
            Dim pp As New CATS.Model.employee ' 与factory 有外键约束关系，要留意
            Dim tp As New CATS.Model.employee
            Dim pp_ph As New CATS.BLL.employeeManager

            pp = pp_ph.SelectByLoginname(Environment.UserName)

            If pp IsNot Nothing Then
                Return pp.id
            Else
                tp.emp_num = ""
                tp.name = ""
                tp.login_name = Environment.UserName
                tp.permission = 0
                tp.department = ""
                tp.pwd = ""
                tp.user_level = ""
                tp.factory_id = get_factory_id(pFactory) 'pFactoryID ' 与factory 有外键约束关系。
                tp.gen1 = ""
                tp.gen2 = ""
                tp.gen3 = ""
                If pp_ph.Add(tp) = True Then
                    pp = pp_ph.SelectByLoginname(Environment.UserName)
                    If pp IsNot Nothing Then
                        Return pp.id
                    Else
                        Throw New Exception("Don't get_employee_ID_0 informaiton from Database")
                        ' Return 1
                    End If
                Else
                    Throw New Exception("Don't get_employee_ID_1 informaiton from Database")
                    'Return 1
                End If
            End If

        Catch ex As Exception
            Throw New Exception("PowerCal:get_employee_id:: " & ex.Message)
            'Return 1
        End Try

    End Function

    'Environment.MachineName, Environment.UserName
    Private Function add_caldata() As Boolean
        Try


            With Cal_data

                .phase_main_id = get_phase_main_id()  'pahse_main_name   'ok PIM700
                .controller_id = get_controller_id()     '电脑名
                .pim_instr_main_id = get_pim_instr_main_id() ' 仪器信息
                .employee_id = get_employee_id()  '操作员

                '----------debug use------------------------------

                ''.serial_number = "Cal0300201811230987" 'ok              
                ''.tx1_power = 3.0 'ok  
                ''.tx1_offset = 4.0 'ok  
                ''.tx2_power = 5.0 'ok  
                ''.tx2_offset = 6.0 'ok  
                ''.power_meter_idn = "E4800" 'ok
                ''.cal_type = "manual" 'ok

                '-------------------------------------------------

                .power = OutPowersettingvalue.Text.Trim
                .attenuation = AttenuationValue.Text.Trim
                .tx1_power = Original_F1.Text.Trim
                .tx1_offset = Offset_F1.Text.Trim
                .tx2_power = Original_F2.Text.Trim
                .tx2_offset = Offset_F2.Text.Trim
                .cal_time = CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))
                .gen1 = ""
                .gen2 = ""
                .gen3 = ""
            End With

            Dim Cal_data_add As New CATS.BLL.rec_imd_calibrationManager
            Return Cal_data_add.Add(Cal_data)

        Catch ex As Exception
            Throw New Exception("add_caldata():: " & ex.Message)
        End Try

    End Function

    Public Function GetDevSN()

        GetSN.Show()
        GetSN.DevSnForPim = ""


        'Dim LabelSN As String

        'Dim c As Boolean = False

        'Do
        '    LabelSN = InputBox("Please Input DevSerialNumber:", "DevSerialNumber Input Box", "")
        '    If Len(LabelSN) > 3 Then c = True '假设设备的SN长度大于3
        '    'If LabelSN = "12345" Then Exit Do '  
        'Loop Until c

        'GetDevSN = LabelSN

        'Application.DoEvents()

        Return 1

    End Function


    'add by tony update for dgvInstrs(datagridview)
    Private Sub dgvInstrs_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvInstrs.CurrentCellDirtyStateChanged

        Try
            If dgvInstrs.IsCurrentCellDirty Then
                dgvInstrs.CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If

        Catch ex As Exception
            MsgBox("Error::dgvInstrs_CurrentCellDirtyStateChanged")
        End Try

    End Sub

    Private Sub dgvInstrs_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInstrs.CellValueChanged

        Try
            If e.ColumnIndex = 1 And e.RowIndex >= 0 Then
                dgvInstrs.Rows(e.RowIndex).Cells(6).Value = ""
                dgvInstrs.Rows(e.RowIndex).Cells(7).Value = ""
                dgvInstrs.Rows(e.RowIndex).Cells(8).Value = ""
                dgvInstrs.Rows(e.RowIndex).Cells(10).Value = ""
            End If

        Catch ex As Exception
            ' MsgBox("Read configure error::dgvInstrs_CellValueChanged")
        End Try
    End Sub

    Private Sub Sel_Manual_CheckedChanged(sender As Object, e As EventArgs) Handles Sel_Manual.CheckedChanged
        instr_sn_Temp = ""
    End Sub

    'add save calibration data to server end===============================

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    pahse_main_name = "PIM700"
    '    Cal_data = New CATS.Model.rec_imd_calibration ' 清空

    '    If add_caldata() = True Then
    '        MsgBox("Save data to server success")
    '    Else
    '        MsgBox("Save data to server fail")
    '    End If

    'End Sub


End Class