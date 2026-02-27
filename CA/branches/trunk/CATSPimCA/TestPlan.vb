Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Xml.Serialization
Imports CATS
Imports CATS.BLL
Imports CATS.Model
Imports Keyence.AutoID.SDK
Imports MIIBridge
Imports Opc.Ua
Imports Opc.Ua.Client
Imports OpcUaUtility
Imports RFPATestInterface
Imports Sunisoft.IrisSkin

Public Class TestPlan
    Implements RFPATestInterface.ITestPlan

    Private m_spec As New TestSpec
    Private _MODE As String = "PROD"
    Private applicationPath As String = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
    Private configPath As String
    Private Const SYSTEM_SETUP_FILENAME As String = "SystemSetup.json"
    Private Shared systemSetup As New SystemSetup()
    Private Shared jsonHandler As New JsonHandler()
    Private Delegate Sub dlgConnectPlcServer()


    Public Sub InitializeGUI()
        Try
            Dim szProduction(0) As String
            Dim mode As String
            'TestPlan赋值给RFPATestInterface的Common.TestObj(全局变量)
            GUI.AssignTestPlan(Me)

            '启动目录\CATSPim.ini
            GUI.INI_File = pAppIniFile

            'clsOperation的只写属性赋值 = "STS PIM"
            GUI.SW_Name = "STS PIM"
            '给RFPAInterface的全局变量SW_Version赋值
            GUI.SW_Version = Application.ProductVersion
            mode = "PROD" ' pAppCfg.GetTestMode.ToString

            GUI.MTR_Version = "1.0"
            GUI.FixtureID = "1.0"

            GUI.repeatEnabled = False
            GUI.DefineToolsMenu("Device Setup", "Gage R&&R", "Test Accessory")
            GUI.DefineSAPMenu("SAP Fail Safe")
            GUI.DefineTroubleshootMenu("VibController", "Connect Device", "SR-X100W Debugger")

            GUI.ShowInterface()

            GUI.InitProgressBar()
            GUI.ProgressMaxValue = 60
            configPath = Path.Combine(applicationPath, SYSTEM_SETUP_FILENAME)
            LoadSystemSetup()
            Dim dgConnectPlc As New dlgConnectPlcServer(AddressOf ConnectOpcUaAsync)
            Dim thResult As IAsyncResult = dgConnectPlc.BeginInvoke(Nothing, Nothing)

            Do While Not thResult.AsyncWaitHandle.WaitOne(1000)
                GUI.ProgressBarValue += 1
                Application.DoEvents()
            Loop
            dgConnectPlc.EndInvoke(thResult)
            If GUI.ProgressBarValue < GUI.ProgressMaxValue Then GUI.ProgressBarValue = 60

            GUI.Plant = pFactory
            GUI.MiiStatus = gPimTestConfig.MII.Enable
            GUI.Pretest_PIM = gPimTestConfig.Pretest
            GUI.Automation = gPimTestConfig.Automation
            If gPimTestConfig.Automation Then mode = "PROD_AUTO"

            GUI.ModeName = mode
            pRTP.product_mode = mode

            pRTP.factory = pFactory

            '向RFPAInterface的frmInterface的TSDP_Mode下拉列表框添加所有mode
            '包括PROD、NPI、RD、REL、DOE和Retest
            TestModules.LoadModes()
            '向RFPAInterface的frmInterface的TSDP_PhaseStation下拉列表框添加参数mode模式下的所有PhaseStation
            '并赋值给pRTP.M_phase_station_main
            TestModules.LoadPhaseStation(mode)
            ' MenuClick("PRODUCTS", szProduction(szProduction.GetLowerBound(0)))
            'LoadSystemSetup()
            'Await ConnectOpcUaAsync()
        Catch ex As Exception
            Throw New Exception("InitializeGUI()::" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub AbortTest() Implements ITestPlan.AbortTest
        pAbortFlag = True
    End Sub
    Public Function ReadScan() As String Implements ITestPlan.ReadScan
        Try
            If gPimTestConfig.SnReader = False Then Return Nothing
            Return ReadScanner.Trim
        Catch ex As Exception
            Throw New Exception("TestPlan.ReadScan()::" & ex.Message)
        End Try
    End Function
    Public Sub ExitInterface(ByRef Cancel As Short) Implements ITestPlan.ExitInterface
        FormMain.Close()
        End
    End Sub
    Public Sub MenuClick(MenuName As String, ByRef ItemName As String) Implements ITestPlan.MenuClick
        If MenuName = "Tools" Then
            If ItemName = "Device Setup" Then
                Dim frmPimTestConfig = New FormConfig(gPimCfgFileName, gPimTestConfig)
                frmPimTestConfig.ShowDialog()
            End If
            If ItemName = "Gage R&&R" Then
                Dim frmGRR As Form = New FormGRR()
                frmGRR.ShowDialog()
            End If
            If ItemName = "Test Accessory" Then
                Dim frmTestAccessory As Form = New FormTestAccessory()
                frmTestAccessory.ShowDialog()
            End If
        ElseIf MenuName = "Troubleshoot" Then
            If ItemName = "VibController" Then
                FormVibController.ShowDialog()
            End If
            If ItemName = "Connect Device" Then
                FormConnectDevice.ShowDialog()
            End If
            If ItemName = "SR-X100 Debugger" Then
                FormSRX100Debugger.ShowDialog()
            End If
        ElseIf MenuName = "SAP" Then
            If ItemName = "SAP Fail Safe" Then
                FormSAPFailSafeMode.ShowDialog()
            End If
        End If
    End Sub
    Public Sub PostTest() Implements ITestPlan.PostTest
        AddStatusMsg(String.Format("Test End"))
    End Sub
    Public Sub PreTest(TestPhase As String, ByRef Barcode As String, ByRef Cancel As Boolean) Implements ITestPlan.PreTest
        pAbortFlag = False
    End Sub
    Public Sub RunTest(Barcode As String, TestPhase As String, ByRef Cancel As Boolean) Implements ITestPlan.RunTest
        Try
            If gNeedTest = False Then Exit Sub

            Dim phase As New TestSpec.TestPhase

            pRTP.barcode = Barcode

            pRTP.product_mode_id = m_spec.ProductModeId
            ' Pick the phase selected in frmInterface to test
            For Each p As TestSpec.TestPhase In m_spec.TestPhaseList
                If p.Name.ToUpper.Trim = TestPhase.ToUpper.Trim Then
                    phase = p
                    Exit For
                End If
            Next

            pRTP.AlgoParas = GetAlgoParasBySpecMainId(phase.SpecMainId)

            ' Check if pRTP.M_phase_station_main.phase_station contains in table product_mode_phase_station 
            ' under m_spec.productModeId
            If CheckPhaseStation(m_spec.ProductModeId, pRTP.M_phase_station_main) = False Then
                AddStatusMsg("Not find test_station = " & pRTP.M_phase_station_main.phase_station)
                MsgBox("Not find Test Station -- " & pRTP.M_phase_station_main.phase_station & " ." & vbCrLf &
                    "- Product -- " & pRTP.M_product_main.product_name & vbCrLf &
                    "- MODE -- " & pRTP.product_mode, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            ' Run phase test, pass phase selected to this procedure
            If TestModules.RunTestPhase(phase, Cancel) = False Then
                GUI.RecordResult("PhaseTest", -1, 0, 0)
            End If
        Catch ex As Exception
            MsgBox("RunTest()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub

    Public Sub RunTestGroup(TestGroup As String, Barcode As String, TestPhase As String) Implements ITestPlan.RunTestGroup

    End Sub
    Public Function CheckSN(ByRef CA As CableAssembly, isSecondSn As Boolean) As Boolean Implements ITestPlan.CheckSN
        Try
            If isSecondSn Then
                pRTP.two_sn_exist = True
            Else
                GUI.ClearStatusMsg()
            End If

            Dim SN As String = CA.SerialNumber
            Dim PN As String

            If SN.Length = 7 Then
                Dim serialYear As String = DateTime.Today.Year.ToString.Substring(2)
                SN = serialYear & pPlantCode & SN
                CA.SerialNumber = SN
            End If

            If isSecondSn Then
                pRTP.serialNumber2 = SN
            End If

            AddStatusMsg(String.Format("Looking for SN = {0} in DB!", SN))

            If SN.ToUpper.Trim = gLowPimLoadTestSn Then
                CA.OrderNumber = "6001234567"
                CA.PartNumber = "LOW_PIM_LOAD"
                CA.Length = 1
                CA.UOM = "FEET"
            ElseIf SN.ToUpper.Trim = gInputLowPimLoadTestSn Then
                CA.OrderNumber = "6007654321"
                CA.PartNumber = "LOW_PIM_LOAD_IC"
                CA.Length = 1
                CA.UOM = "FEET"
            Else
                If gSAPFailSafeMode.SAPFailSafeModeOnOff = False Then

                    AddStatusMsg(String.Format("Looking up SAP data......", SN))
                    Dim sw As New Stopwatch
                    sw.Start()

                    Dim objSerialInq As New SerialNumInq.SerialInq
                    Dim Product As SerialNumInq.Product = objSerialInq.GetProduct(SN)
                    CA = New CableAssembly(Product)

                    sw.Stop()
                    AddStatusMsg(String.Format("OK Time Cost {0} ns", sw.ElapsedMilliseconds))

                    '若通过SAPReqURL未找到跳线信息，则返回序列号不存在信息给frmInterface的txtStatus
                    If CA Is Nothing Then
                        AddStatusMsg(String.Format("Not found SN = {0} in SAP DB, pleas check label!", SN))
                        Return False
                    End If
                Else
                    AddStatusMsg(String.Format("SAP fail safe mode is ON!"))
                    CA.OrderNumber = gSAPFailSafeMode.WorkOrder
                    CA.PartNumber = gSAPFailSafeMode.PartNumber
                    CA.Length = gSAPFailSafeMode.Length
                    CA.UOM = gSAPFailSafeMode.UOM
                End If
            End If

            If CheckMiiRouting(SN) = False Then Return False

            pRTP.length = CA.Length
            PN = CA.PartNumber
            pRTP.work_order = CA.OrderNumber

            Dim product_mainManager As New CATS.BLL.product_mainManager
            Dim product_main As CATS.Model.product_main

            Dim product_snManager As New CATS.BLL.product_snManager
            Dim product_sn As CATS.Model.product_sn = product_snManager.SelectBySerialNum(SN.ToUpper)
            If product_sn Is Nothing Then
                '若通过序列号在表product_sn中查找序列号未找到（该序列号未测试过）
                '则按照PN查找并返回product_main
                product_main = product_mainManager.SelectByProductName(PN)
                AddStatusMsg(String.Format("Input SN = {0}, DB PN = {1}", SN, PN))
            Else
                '若通过序列号在表product_sn中找到序列号（该序列号测试过）
                '则按照product_sn的product_main_id查找并返回product_main
                product_main = product_mainManager.SelectById(product_sn.product_main_id)
                AddStatusMsg(String.Format("Input SN = {0} already exists in DB with PN = {1} ", SN, product_main.product_name))
            End If

            '若按照PN仍未找到product_main，则返回产品信息不存在（意味着产品没有被维护）
            If product_main Is Nothing Then
                MsgBox("Not found this product, please confirm it!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            '若找到product_main，则检查SAP查询返回的PN是否与数据库存储的PN匹配
            If product_main.product_name.ToUpper <> PN.ToUpper Then
                AddStatusMsg(String.Format("SAP PN = {0}, DB PN = {1}, Mismatch!", PN.ToUpper, product_main.product_name))
                Return False
            End If

            pRTP.M_product_main = product_main
            pRTP.family = product_main.family
            CA.Family = product_main.family

            If CheckLPLTest(SN) = False Then
                MsgBox("Low PIM Load Test is not excecuted successfully within 12 hours, Stop Test!", MsgBoxStyle.Exclamation, "Low PIM Load Verify")
                AddStatusMsg("Low PIM Load Test is not excecuted successfully within 12 hours, Stop Test!")
                Return False
            End If

            'get ca product inforamtion like length etc.
            Dim product_ca_mainManager As New CATS.BLL.product_ca_mainManager
            Dim product_ca_main As CATS.Model.product_ca_main
            product_ca_main = product_ca_mainManager.SelectByProductMainId(product_main.id)

            If product_ca_main Is Nothing Then
                AddStatusMsg(String.Format("PN = {0} dosen't belong to CA product!", PN.ToUpper))
                Return False
            End If

            'check if length input is wrong
            Dim lengthFt As Decimal
            If CA.UOM.ToUpper = "FEET" OrElse CA.UOM.ToUpper = "FT" Then
                lengthFt = Decimal.Parse(pRTP.length)
            Else
                lengthFt = Math.Round(Decimal.Parse(pRTP.length) * 3.281, 3)
            End If
            If lengthFt > product_ca_main.Length_max_feet Or lengthFt < product_ca_main.Length_min_feet Then
                AddStatusMsg(String.Format("SAP Length = {0}, DB Length = {1}, Mismatch!", lengthFt, product_ca_main.Length_feet))
                Return False
            End If

            If pRTP.product_mode.StartsWith("PROD") Then
                Dim dynamic As Boolean = IIf(product_ca_main.Gen2 Is Nothing, False, product_ca_main.Gen2)
                GUI.Dynamic = dynamic Or product_main.product_name.EndsWith("-D") Or product_main.product_name.EndsWith("-CHT")
                If pRTP.family = "Infrastructure" Then If lengthFt < 3.3 Then GUI.Dynamic = False
                gPimTestConfig.Vibration.Enable = GUI.Dynamic
            Else
                GUI.Dynamic = gPimTestConfig.Vibration.Enable
            End If
            If gPimTestConfig.Automation Then gPimTestConfig.Vibration.Enable = False
            My.Application.DoEvents()

            If ProductStartupFolder().ToUpper = "Debug".ToUpper Then
                'GUI.Dynamic = False
                'gSaveFile = False
            End If

#If INSTR_NORMAL_TEST Then
            If pPlantCode = "IN02" Then
                gTestAccessoryList = CTestAccessoryList.CreateInstance(gTestAccessoryListFileName)
                If gTestAccessoryList Is Nothing Then
                    Dim frmTestAccessory As Form = New FormTestAccessory()
                    frmTestAccessory.ShowDialog()
                End If
                gTestAccessoryList = CTestAccessoryList.CreateInstance(gTestAccessoryListFileName)
                If gTestAccessoryList Is Nothing Then
                    MsgBox("Please config your test accesorry")
                    Return False
                End If
                Dim tcmM As CATS.Model.test_cable_main
                Dim tcmBll As New CATS.BLL.test_cable_mainManager
                Dim maxCount As Integer
                Dim tolerantCount As Integer
                For Each testAcc In gTestAccessoryList.TestAccessoryList
                    If testAcc.Enable = False Then Continue For
                    tcmM = tcmBll.SelectByCableSN(testAcc.SerialNumber)
                    If tcmM Is Nothing Then
                        MsgBox(String.Format("The test accessory SN <{0}> is not found in DB, please config it", testAcc.SerialNumber), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Search Test Accessory")
                        Dim frmTestAccessory As Form = New FormTestAccessory()
                        frmTestAccessory.ShowDialog()
                        Return False
                    End If
                    If tcmM.test_count > testAcc.TolerantCount Then
                        MsgBox(String.Format("The test accessory SN <{0}> test count is over tolerant count, please change your test accessory and conifg it", testAcc.SerialNumber), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Tolerance Counter")
                        Dim frmTestAccessory As Form = New FormTestAccessory()
                        frmTestAccessory.ShowDialog()
                        Return False
                    End If
                    If tcmM.test_count > maxCount Then maxCount = tcmM.test_count
                    tolerantCount = testAcc.TolerantCount
                Next
                GUI.Counter(tolerantCount) = maxCount
            End If
#End If

            GUI.TestDoubleLength = IIf(product_ca_main Is Nothing, False, product_ca_main.Gen1)

            ' Prohibit product to be tested more than 3 times
            If pRTP.product_mode.StartsWith("PROD") OrElse pRTP.product_mode = "RETEST" Then
                If CheckSNTestCount(SN) = False Then Return False
            End If
            ' Get MLock product each port SNs
            LoadRLTestData(SN, PN, "RL_ISO")

            Return True

        Catch ex As Exception
            MsgBox("TestPlan.CheckSN()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function
    Private Function CheckLPLTest(SN As String)
        Try
            If gPimTestConfig.Automation Then Return True
            If ProductStartupFolder().ToUpper = "Debug".ToUpper Then Return True
            If SN = gLowPimLoadTestSn Or SN = gInputLowPimLoadTestSn Then Return True
            Dim lplSn As String = gLowPimLoadTestSn
            If pRTP.family = "Input Cable" Then lplSn = gInputLowPimLoadTestSn
            Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            lstPhaseStatus = cq_phstatus.SelectAllPhaseStatusBySnAndDateTime(lplSn, Now.AddDays(-2), Now, pFactory, "PROD", "FinalTest")
            Dim ctrM As CATS.Model.controller
            If lstPhaseStatus IsNot Nothing Then
                For Each phStatus In lstPhaseStatus
                    If phStatus.phase.Contains("PIM") Then
                        Dim measPhaseM As meas_phase = (New meas_phaseManager).SelectById(phStatus.meas_phase_id)
                        ctrM = (New controllerManager).SelectById(measPhaseM.controller_id)
                        If ctrM.name <> My.Computer.Name Then Continue For
                        If phStatus.phase_status = "P" And Now.Subtract(phStatus.start_datetime).TotalHours < 12.0 Then Return True
                    End If
                Next
            End If
            Return False
        Catch ex As Exception
            Throw New Exception("TestPlan.CheckSN.CheckLPLTest()::" & ex.Message)
        End Try
    End Function
    Public Function ProductStartupFolder() As String
        Dim name As String = Application.StartupPath.Substring(Application.StartupPath.LastIndexOf("\") + 1)
        Return name
    End Function
    Private Function CheckMiiRouting(serialNumber As String) As Boolean
        Try
            If gPimTestConfig.MII.Enable = False Then Return True

            Dim miiMain As New MIIBridge.Controller
            Dim currStation As String

            miiMain.EnableDebugLog = True
            currStation = miiMain.GetWorkStation(serialNumber， pFactory, Environment.UserName)

            AddStatusMsg("Start to check MII routing ...", True)
            If currStation = "STSP01" Then
                GUI.AddStatusMsg(" OK", False)
                Return True
            Else
                GUI.AddStatusMsg(" Fail,", False)
                GUI.AddStatusMsg(" SN = " & serialNumber, False)
                GUI.AddStatusMsg(", MII station = " & currStation, False)

                MsgBox("This Product Is Not In Correct Station!" & vbCrLf &
                        "- Serial Number = " & serialNumber & vbCrLf &
                        "- MII Station = " & currStation, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("CheckMiiRouting()::" & ex.Message)
        End Try

    End Function
    Private Function CheckSNTestCount(ByVal SN As String) As Boolean
        Try
            If SN.ToUpper.Trim = gLowPimLoadTestSn Then Return True
            If SN.ToUpper.Trim = gInputLowPimLoadTestSn Then Return True
            If pRTP.M_product_main.family = "SBT" Then Return True
            If ProductStartupFolder().ToUpper = "Debug".ToUpper Then Return True
            If gPimTestConfig.Automation Then Return True

            Dim phaseStatusList As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatusBll As New CATS.BLL.cq_phases_statusManager
            phaseStatusList = cq_phstatusBll.SelectAllPhaseStatusBySn(SN, pRTP.factory, pRTP.product_mode, pRTP.M_phase_station_main.phase_station)
            If phaseStatusList IsNot Nothing Then
                Dim query = From phaseStatus In phaseStatusList
                            Where phaseStatus.phase.Contains("PIM")
                gTestCount = query.Count + 1
            Else
                gTestCount = 1
            End If

            If pRTP.M_product_main.product_name.StartsWith("ML") Or pRTP.M_product_main.family = "MLOCK" Then
                GUI.TestCountMax = 2
            Else
                GUI.TestCountMax = 3
            End If
            GUI.TestCountRest = GUI.TestCountMax - gTestCount + 1

            If gTestCount = 1 Then
                AddStatusMsg(String.Format("Input SN = {0}, Fresh Test", SN))
            Else
                Select Case gTestCount
                    Case 2
                        AddStatusMsg(String.Format("Input SN = {0}, Second Test", SN))
                    Case 3
                        If pRTP.M_product_main.product_name.StartsWith("ML") Or pRTP.M_product_main.family = "MLOCK" Then
                            AddStatusMsg(String.Format("Input SN = {0}, Third Test, STOP TEST!", SN))
                            Return False
                        Else
                            AddStatusMsg(String.Format("Input SN = {0}, Third Test", SN))
                        End If
                    Case Else
                        AddStatusMsg(String.Format("Input SN = {0}, Test Times = {1}, STOP TEST!", SN, gTestCount))
                        Return False
                End Select
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("CheckSNTestCount" & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' Load all sub SNs with MLOCK product main SN
    ''' </summary>
    ''' <param name="SN"></param>
    ''' <param name="PN"></param>
    ''' <param name="phase"></param>
    Private Sub LoadRLTestData(ByVal SN As String, ByVal PN As String, ByVal phase As String)
        If pRTP.M_product_main.family.ToUpper <> "MLOCK" Then Exit Sub
        gMLOCK_SN_List = New List(Of String)
        Dim status As Model.cq_phases_status = Nothing
        Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
        Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager

        lstPhaseStatus = cq_phstatus.SelectAll(PN, SN, "PROD", 2)

        If lstPhaseStatus IsNot Nothing Then
            status = lstPhaseStatus.Find(Function(o) o.phase = phase)
        End If

        If status Is Nothing Then Exit Sub

        Dim meas_groupManager As New BLL.meas_groupManager()
        Dim lstGp As List(Of Model.meas_group) = meas_groupManager.SelectBy_meas_phase_id(status.meas_phase_id)
        If lstGp Is Nothing Then Exit Sub

        Dim meas_detailManager As New BLL.meas_detailManager()
        For Each measG As Model.meas_group In lstGp
            Dim lstIt As List(Of Model.meas_detail) = meas_detailManager.SelectBy_meas_group_id(measG.id)
            Dim meas_detail_id As Integer = lstIt.First.id
            Dim meas_detail_extendManager As New BLL.meas_detail_extendManager
            Dim meas_detail_extendM As Model.meas_detail_extend = meas_detail_extendManager.SelectByMeasDetailId(meas_detail_id)
            If meas_detail_extendM IsNot Nothing Then
                If gMLOCK_SN_List Is Nothing Then gMLOCK_SN_List = New List(Of String)
                If meas_detail_extendM.m3 IsNot Nothing Then gMLOCK_SN_List.Add(meas_detail_extendM.m3)
            End If
        Next
    End Sub
    Public Sub LoadPimTestData(SN As String, PN As String, Phase As String) Implements ITestPlan.LoadPimTestData
        Try
            gNeedTest = True
            Dim status As Model.cq_phases_status = Nothing
            Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager

            lstPhaseStatus = cq_phstatus.SelectAll(PN, SN, pRTP.product_mode, 2)

            If lstPhaseStatus Is Nothing Then Return

            'Check LPL lastest test of this PC
            Dim tmp_lstPhaseStatus As New List(Of CATS.Model.cq_phases_status)
            If SN = gLowPimLoadTestSn Or SN = gInputLowPimLoadTestSn Then
                lstPhaseStatus = cq_phstatus.SelectAllPhaseStatusBySnAndDateTime(SN, Now.AddDays(-2), Now, pFactory, "PROD", "FinalTest")
                Dim ctrM As CATS.Model.controller
                If lstPhaseStatus IsNot Nothing Then
                    For Each phStatus In lstPhaseStatus
                        If phStatus.phase.Contains("PIM") Then
                            Dim measPhaseM As meas_phase = (New meas_phaseManager).SelectById(phStatus.meas_phase_id)
                            ctrM = (New controllerManager).SelectById(measPhaseM.controller_id)
                            If ctrM.name = My.Computer.Name Then
                                tmp_lstPhaseStatus.Add(phStatus)
                            End If
                        End If
                    Next
                End If
                If tmp_lstPhaseStatus.Count > 0 Then
                    Dim query = From ph In tmp_lstPhaseStatus
                                Order By ph.start_datetime Descending
                    status = query.FirstOrDefault
                End If
            Else
                If lstPhaseStatus IsNot Nothing Then
                    status = lstPhaseStatus.Find(Function(o) o.phase = Phase)
                End If
            End If

            Dim dlgRes As DialogResult

            If status Is Nothing Then
                If lstPhaseStatus IsNot Nothing Then
                    Dim query = From ph In lstPhaseStatus
                                Where ph.phase.StartsWith("PIM")
                                Order By ph.start_datetime Descending
                    status = query.FirstOrDefault
                End If
                If status Is Nothing Then Exit Sub
            End If

            If Not gPimTestConfig.Automation Then
                dlgRes = MessageBox.Show(String.Format("The PIM Last Test Record:" & vbCrLf & vbCrLf &
                                       "SN: <{0}>" & vbCrLf &
                                       "Test Phase: <{1}>" & vbCrLf &
                                       "Test Time <{2}>," & vbCrLf &
                                       "Test Status: <{3}>," & vbCrLf & vbCrLf &
                                       "Do you still want to retest it?", SN, status.phase, status.start_datetime, status.phase_status), "Retest", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)

                If dlgRes = DialogResult.No Then gNeedTest = False
            End If

            Dim meas_groupManager As New BLL.meas_groupManager()
            Dim lstGp As List(Of Model.meas_group) = meas_groupManager.SelectBy_meas_phase_id(status.meas_phase_id)
            If lstGp Is Nothing Then Exit Sub

            Dim meas_detailManager As New BLL.meas_detailManager()
            Dim meas_traceManager As New BLL.meas_traceManager()
            Dim traceList As List(Of Model.meas_trace)
            Dim testTrace As New DataModels.TestTrace
            Dim testPhase As TestSpec.TestPhase = Nothing
            Dim testGroup As TestSpec.TestGroup = Nothing
            Dim testItem As Model.cq_spec_imd_details = Nothing
            ' Pick the phase selected in frmInterface to test
            For Each p As TestSpec.TestPhase In m_spec.TestPhaseList
                If p.Name.ToUpper.Trim = Phase.ToUpper.Trim Then
                    testPhase = p
                    Exit For
                End If
            Next
            If testPhase Is Nothing Then Return
            pRTP.AlgoParas = GetAlgoParasBySpecMainId(testPhase.SpecMainId)
            For Each measG As Model.meas_group In lstGp
                Call GUI.DisplayBlankRow()
                testGroup = testPhase.TestGroupList.Find(Function(o) o.Id = measG.group_main_id)
                If testGroup Is Nothing Then Continue For
                Dim lstIt As List(Of Model.meas_detail) = meas_detailManager.SelectBy_meas_group_id(measG.id)
                For Each detail In lstIt
                    testItem = testGroup.TestItemList.Find(Function(o) o.spec_detail.id = detail.spec_detail_id)
                    If testItem Is Nothing Then Continue For
                    Select Case measG.group_status
                        Case "P"
                            GUI.SetGroupStatus(testGroup.Name, 2)
                        Case "F"
                            GUI.SetGroupStatus(testGroup.Name, 3)
                    End Select

                    traceList = meas_traceManager.SelectBy_meas_detial_id(detail.id)
                    If traceList IsNot Nothing Then
                        AddPointToList(testTrace, traceList)
                    End If

                    If testTrace.TwoTone.Count > 0 Then
                        testTrace.TwoToneFilter = Calculate.FilterDataPoint(testTrace.TwoTone)
                        testTrace.Lambda = Calculate.LambdaTrace(testTrace.TwoToneFilter)
                        Dim pr As New TestReport.ReportModule
                        Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
                        crList = pr.GetMeasCriteriaList(testTrace, testItem, testPhase.TestCriteriaList)
                        pr.PrintTResultDcf(crList)
                    Else
                        Dim pr As New TestReport.ReportModule
                        Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
                        crList = pr.GetMeasCriteriaList(testTrace, testItem, testPhase.TestCriteriaList)
                        pr.PrintTResultDcf(crList)
                    End If
                Next
            Next

        Catch ex As Exception
            Throw New Exception("LoadPimTestData() - " & ex.Message)
        End Try
    End Sub
    Private Sub AddPointToList(ByRef testTrace As DataModels.TestTrace, traceList As List(Of Model.meas_trace))
        Try
            Dim x1(), y1() As String
            Dim x2(), x3() As String
            Dim len As Integer
            Dim imdPoint As DataModels.FrequencyPoint
            'Dim lambdaPoint As PointF
            For Each trace As Model.meas_trace In traceList
                x1 = trace.x1_data.Split(",")
                x2 = trace.x1_data.Split(",")
                x3 = trace.x1_data.Split(",")
                y1 = trace.y1_data.Split(",")
                len = Math.Min(x1.Length, y1.Length)
                Select Case trace.trace_name
                    Case "2Tone"
                        For i As Integer = 0 To len - 1
                            imdPoint = New DataModels.FrequencyPoint
                            imdPoint.XData = Single.Parse(x1(i))
                            imdPoint.YData = Single.Parse(y1(i))
                            testTrace.TwoTone.Add(imdPoint)
                        Next
                    Case "SweepDown"
                        For i As Integer = 0 To len - 1
                            imdPoint = New DataModels.FrequencyPoint
                            imdPoint.TxlFreq = Single.Parse(x1(i))
                            imdPoint.TxrFreq = Single.Parse(x2(i))
                            imdPoint.RxFreq = Single.Parse(x3(i))
                            imdPoint.XData = Single.Parse(x1(i))
                            imdPoint.YData = Single.Parse(y1(i))
                            testTrace.SweepDown.Add(imdPoint)
                        Next
                    Case "SweepUp"
                        For i As Integer = 0 To len - 1
                            imdPoint = New DataModels.FrequencyPoint
                            imdPoint.TxlFreq = Single.Parse(x1(i))
                            imdPoint.TxrFreq = Single.Parse(x2(i))
                            imdPoint.RxFreq = Single.Parse(x3(i))
                            imdPoint.XData = Single.Parse(x1(i))
                            imdPoint.YData = Single.Parse(y1(i))
                            testTrace.SweepUp.Add(imdPoint)
                        Next
                    Case "STS-C1", "STS-C2", "STS-C3"
                        For i As Integer = 0 To len - 1
                            imdPoint = New DataModels.FrequencyPoint
                            imdPoint.TxlFreq = Single.Parse(x1(i))
                            imdPoint.TxrFreq = Single.Parse(x2(i))
                            imdPoint.RxFreq = Single.Parse(x3(i))
                            imdPoint.XData = Single.Parse(x1(i))
                            imdPoint.YData = Single.Parse(y1(i))
                            If testTrace.StsC.ContainsKey(i) = False Then
                                testTrace.StsC.Add(i, New List(Of DataModels.FrequencyPoint))
                            End If
                            testTrace.StsC(i).Add(imdPoint)
                        Next
                    Case "Lambda"
                        'For i As Integer = 0 To len - 1
                        '    lambdaPoint = New PointF
                        '    lambdaPoint.X = Single.Parse(x1(i))
                        '    lambdaPoint.Y = Single.Parse(y1(i))
                        '    testTrace.Lambda.Add(lambdaPoint)
                        'Next
                End Select
            Next

        Catch ex As Exception
            Throw New Exception("FormTest.AddPointToList()::" & ex.Message)
        End Try
    End Sub
    Public Function LoadPhaseStatus(SN As String, PN As String) As Boolean Implements ITestPlan.LoadPhaseStatus
        Try
            '读取已经测试的Test Step状态并返回给LstTestStep
            Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            lstPhaseStatus = cq_phstatus.SelectAll(PN, SN, pRTP.product_mode, pRTP.M_phase_station_main.id)
            LoadTestPhasesStatus(m_spec, lstPhaseStatus)

            '检查TSDP_PhaseStation所选的PhaseStation是否已经添加到product_mode_phase_station表中
            If CheckPhaseStation(m_spec.ProductModeId, pRTP.M_phase_station_main) = False Then
                AddStatusMsg("Not find test_station = " & pRTP.M_phase_station_main.phase_station)
                MsgBox("Not find Test Station -- " & pRTP.M_phase_station_main.phase_station & " ." & vbCrLf &
                    "- Product -- " & pRTP.M_product_main.product_name & vbCrLf &
                    "- MODE -- " & pRTP.product_mode, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            Return True

        Catch ex As Exception
            MsgBox("TestPlan.LoadPhaseStatus()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    Public Function LoadSpec() As Boolean Implements ITestPlan.LoadSpec
        Try
            '根据product_main和pRTP.product_mode获取产品规格信息
            m_spec = TestModules.GetTestSpec()
            If m_spec Is Nothing Then
                AddStatusMsg(String.Format("Product = {0} @ Mode = {1} spec not found", pRTP.M_product_main.product_name, pRTP.product_mode, pRTP.phase))
                Return False
            End If

            If m_spec.TestPhaseList.Count = 0 Then
                AddStatusMsg(String.Format("Product = {0} @ Mode = {1} spec not found", pRTP.M_product_main.product_name, pRTP.product_mode, pRTP.phase))
                Return False
            End If


            Dim phase(m_spec.TestPhaseList.Count - 1) As String
            Dim i As Short = 0

            For i = 0 To m_spec.TestPhaseList.Count - 1
                phase(i) = m_spec.TestPhaseList(i).Name
            Next

            Dim phaseString As String = String.Join("/", phase)

            AddStatusMsg(String.Format("Product = {0} @ Mode = {1} PIM spec found, Phases = {2}", pRTP.M_product_main.product_name, pRTP.product_mode, phaseString))

            Dim tmpTestSpec As New TestSpec
            tmpTestSpec.ProductModeId = m_spec.ProductModeId
            tmpTestSpec.Validate = m_spec.Validate
            Dim phaseName As String = ""
            LoadMatchedSpec(tmpTestSpec, phaseName)

            m_spec = tmpTestSpec

            AddStatusMsg(String.Format("Instrument configuration is {0}", phaseName))

            If m_spec.TestPhaseList.Count = 0 Then
                AddStatusMsg(String.Format("Product = {0} @ Mode = {1} not found matched instrument configuration", pRTP.M_product_main.product_name, pRTP.product_mode))
                Return False
            Else
                AddStatusMsg(String.Format("Product = {0} @ Mode = {1} found matched instrument configuration", pRTP.M_product_main.product_name, pRTP.product_mode))
            End If

            LoadTestPhases(m_spec)
            LoadTestGroups(m_spec)

            Return True

        Catch ex As Exception
            MsgBox("TestPlan.LoadSpec()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function
    Private Sub LoadMatchedSpec(ByRef tmpTestSpec As TestSpec, ByRef phaseName As String)
        Try
            Dim pim700LSelected As Boolean = False
            Dim pim700USelected As Boolean = False
            For Each testPhase As TestSpec.TestPhase In m_spec.TestPhaseList
                phaseName = testPhase.Name.ToUpper
                For Each instr As CInstrument In gSelectedInstrumentList
                    If phaseName = "PIM700LU" Then
                        If instr.Model = "PIM700L" Then
                            pim700LSelected = True
                        End If
                        If instr.Model = "PIM700U" Then
                            pim700USelected = True
                        End If
                        If pim700LSelected AndAlso pim700USelected Then
                            tmpTestSpec.TestPhaseList.Add(testPhase)
                            Return
                        End If
                    Else
                        If instr.Model = phaseName Then
                            tmpTestSpec.TestPhaseList.Add(testPhase)
                            Return
                        End If
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception("Not found matched spec" & ex.Message)
        End Try
    End Sub
    Public Sub MenuPhaseStation(PhaseStation As Object) Implements ITestPlan.MenuPhaseStationClick
        Try
            Dim cqmpsM As CATS.Model.cq_mode_phase_station = CType(PhaseStation, CATS.Model.cq_mode_phase_station)
            pRTP.M_phase_station_main = cqmpsM.M_phase_station_main
        Catch ex As Exception
            MsgBox("GUI.MenuPhaseStation()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Function CheckPhaseStation(product_mode_id As Integer, phase_station_main As CATS.Model.phase_station_main) As Boolean
        Try
            If phase_station_main Is Nothing Then Return False

            Dim cqpmpsBll As New CATS.BLL.cq_product_mode_phase_stationManager
            Dim cqpmpsML As List(Of CATS.Model.cq_product_mode_phase_station)

            cqpmpsML = cqpmpsBll.SelectAllByProductModeId(product_mode_id, True, True)

            If cqpmpsML Is Nothing Then Return False

            For Each cqpmps As CATS.Model.cq_product_mode_phase_station In cqpmpsML
                If cqpmps.M_phase_station_main.id = phase_station_main.id And
                    cqpmps.M_phase_station_main.phase_station.ToUpper.Trim = phase_station_main.phase_station.ToUpper.Trim Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            Throw New Exception("CheckPhaseStation()::" & ex.Message)
        End Try

    End Function

    Public Function MenuModeClick(Mode As Object) As Boolean Implements ITestPlan.MenuModeClick
        Try

            Dim frmPwd As New FormPassword

            frmPwd.ShowDialog()
            If frmPwd.Password <> "cats0001" Then
                MsgBox("Wrong password!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            Dim modeM As CATS.Model.mode
            modeM = CType(Mode, CATS.Model.mode)

            gPimTestConfig.TestMode.ModeName = modeM.mode
            gPimTestConfig.Save(gPimCfgFileName)

            pRTP.product_mode = modeM.mode
            GUI.ModeName = modeM.mode
            TestModules.LoadPhaseStation(modeM.mode)

            Return True

        Catch ex As Exception
            MsgBox("Set mode()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function
#Region "OPC UA Operation"
    Private Sub LoadSystemSetup()
        Try
            If gPimTestConfig.Automation = False Then Return
            If Not File.Exists(configPath) Then
                systemSetup = New SystemSetup With {
                .OutputFolder = Path.Combine(applicationPath, "Output"),
                .ApplicationLogFolder = Path.Combine(applicationPath, "AppLog"),
                .SerialPortConfigs = New List(Of SerialPortConfig) From {
                New SerialPortConfig("COM1", 115200, 8, Parity.None, StopBits.One),
                New SerialPortConfig("COM2", 115200, 8, Parity.None, StopBits.One)}}

                ' 储存设定(json)
                Dim saved As Boolean = jsonHandler.SaveConfig(configPath, systemSetup)
                'AddStatusMsg(String.Format("Save System Setting {0}!", saved))
            Else
                systemSetup = jsonHandler.LoadConfig(configPath)
                AddStatusMsg("Load the system configuration file .... OK")
            End If
        Catch ex As Exception
            Throw New Exception("SystemSetup()::" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Async Function ConnectOpcUaAsync() As Task Implements ITestPlan.ConnectOpcUaAsync
        Try
            If gPimTestConfig.Automation = False Then Return

            If gOpcUaClient IsNot Nothing AndAlso gOpcUaClient.IsConnected Then
                Return
            Else
                GUI.Status = "PLC Disconnected"
            End If

            ' 如果已有连接，先断开
            If gOpcUaClient IsNot Nothing Then
                Await gOpcUaClient.DisconnectAsync()
            End If

            gOpcUaClient = New OpcUaUtility.OpcUaClient(GUI, "192.168.20.32", "4840")

            If ProductStartupFolder().ToUpper = "Debug".ToUpper Then
                gOpcUaClient = New OpcUaUtility.OpcUaClient(GUI, "10.124.71.10", "4840")
            End If

            Await gOpcUaClient.InitializeAsync()

            If gOpcUaClient IsNot Nothing And gOpcUaClient.IsConnected Then
                AddStatusMsg("PLC Connect Success!")
                GUI.Status = "PLC Connected"

                GUI.SendBeatHeart()

                If ProductStartupFolder().ToUpper = "Debug".ToUpper Then
                    Dim miStatus = gOpcUaClient.Subscription("|var|CODESYS Control Win V3 x64.Application.GVL.trig_PIM1", AddressOf GUI.StationStatusChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                    Dim miBarcode = gOpcUaClient.Subscription("|var|CODESYS Control Win V3 x64.Application.GVL.S11testCode", AddressOf GUI.StationBarcodeChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                    Dim miPimFixtureReady = gOpcUaClient.Subscription("|var|CODESYS Control Win V3 x64.Application.GVL.PIM1FixtureReadyInt", AddressOf GUI.PimFixtureReadyStatusChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                Else
                    Dim miStatus = gOpcUaClient.Subscription(IIf(gPimTestConfig.Pretest, OpcUaConst.PIM1StationTriggerInt, OpcUaConst.PIM2StationTriggerInt), AddressOf GUI.StationStatusChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                    Dim miBarcode = gOpcUaClient.Subscription(IIf(gPimTestConfig.Pretest, OpcUaConst.PIM1StationBarcodeStr, OpcUaConst.PIM2StationBarcodeStr), AddressOf GUI.StationBarcodeChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                    Dim miPimFixtureReady = gOpcUaClient.Subscription(IIf(gPimTestConfig.Pretest, OpcUaConst.PIM1FixtureReadyInt, OpcUaConst.PIM2FixtureReadyInt), AddressOf GUI.PimFixtureReadyStatusChanged, samplingIntervalMs:=100, queueSize:=20, discardOldest:=True)
                End If

                'Dim miHeartBeat = gOpcUaClient.Subscription(IIf(gPimTestConfig.Pretest, OpcUaConst.PIM1StationHeartBeatInt, OpcUaConst.PIM2StationHeartBeatInt), AddressOf GUI.HeartBeatIntChanged, samplingIntervalMs:=100, queueSize:=20)
                'GUI.MonitorPLCHeartBeat()
            End If
        Catch ex As Exception
            Throw New Exception("ConnectOpcUaAsync()::" & vbCrLf & ex.Message)
        End Try
    End Function
    Public Async Sub ReadPlcAsync() Implements ITestPlan.ReadPlcAsync
        Try
            If gPimTestConfig.Automation = False Then Return
            If gOpcUaClient Is Nothing Then
                Await ConnectOpcUaAsync()
                Threading.Thread.Sleep(1000)
            End If

            If Not gOpcUaClient.IsConnected Then
                Await ConnectOpcUaAsync()
                Threading.Thread.Sleep(1000)
                If Not gOpcUaClient.IsConnected Then
                    Throw New Exception("OPC is not connected!, please restart the program or contact engineer")
                End If
            End If

            Dim pimTrig As String
            If ProductStartupFolder.ToUpper = "Debug".ToUpper Then
                pimTrig = "|var|CODESYS Control Win V3 x64.Application.GVL.PIM1FixtureReadyInt"
            Else
                pimTrig = If(gPimTestConfig.Pretest, OpcUaConst.PIM1FixtureReadyInt, OpcUaConst.PIM2FixtureReadyInt)
            End If


            Dim testKeys As String() = {pimTrig}

            Dim resp As ReadResponse = Await gOpcUaClient.AsyncReadValue(testKeys)

            For i As Integer = 0 To resp.Results.Count - 1
                Dim dv = resp.Results(i)
                Dim req = gOpcUaClient.GetReadValueIdCollection(testKeys)(i)

                ' 真实的 NodeId（OPC UA 格式，如 "ns=2;s=Device1.Tag1"）
                Dim nodeIdText As String = If(req.NodeId?.ToString(), "(null)")
                ' 如果你希望同时打印“别名”与 NodeId：
                Dim aliasName As String = If(i < testKeys.Length, testKeys(i), "(no alias)")
                Dim keyLabel As String = $"{aliasName} - {nodeIdText}"

                If StatusCode.IsGood(dv.StatusCode) Then
                    If dv.WrappedValue.TypeInfo?.ValueRank = -1 Then
                        AddStatusMsg($"{keyLabel} : {dv.Value}")
                    ElseIf dv.WrappedValue.TypeInfo?.ValueRank = 1 AndAlso TypeOf dv.Value Is Array Then
                        Dim arr = CType(dv.Value, Array)
                        Dim idx As Integer = 0
                        For Each v In arr
                            AddStatusMsg($"{keyLabel}][{idx} : {v}")
                            idx += 1
                        Next
                    Else
                        AddStatusMsg($"{keyLabel} (rank={dv.WrappedValue.TypeInfo?.ValueRank}) : {dv.Value}")
                    End If
                Else
                    AddStatusMsg($"{keyLabel} read failed : {dv.StatusCode}")
                End If
                GUI.PimFixtureReady = dv.Value
            Next

        Catch ex As Exception
            Throw New Exception("ReadPlcAsync()::" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Async Sub WritePlcAsync(values As Dictionary(Of String, Object)) Implements ITestPlan.WritePlcAsync
        Try
            If gPimTestConfig.Automation = False Then Return

            'If gOpcUaClient Is Nothing Or Not gOpcUaClient.IsConnected Then
            '    Await ConnectOpcUaAsync()
            '    Threading.Thread.Sleep(1000)
            '    If gOpcUaClient Is Nothing Or Not gOpcUaClient.IsConnected Then
            '        Throw New Exception("OPC is not connected!, please restart the program or contact engineer")
            '    End If
            'End If

            Await gOpcUaClient.AsyncWriteValue(values)

        Catch ex As Exception
            Throw New Exception("WritePlcAsync()::" & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub ClickRunButton() Implements ITestPlan.ClickRunButton
        FormTest.btnRun.PerformClick()
    End Sub
#End Region
End Class
