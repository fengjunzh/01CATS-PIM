Imports RFPATestInterface
Imports System.IO
Imports System.Xml.Serialization
Public Class TestPlan
    Implements RFPATestInterface.ITestPlan

    Private m_spec As New TestSpec
    Private _MODE As String = "PROD"
    Private m_MostRecentPhaseStatus_List As List(Of CATS.Model.cq_phases_status)
    Private m_MostRecentPimPhaseStatus As CATS.Model.cq_phases_status
    Private m_MostRecentGroupStatus_List As List(Of CATS.Model.meas_group)
    Private m_cq_spec_meas_detail_list As List(Of CATS.Model.cq_spec_imd_details)
    Private m_meas_detail_list As List(Of CATS.Model.meas_detail)
    Private m_meas_trace_list As List(Of CATS.Model.meas_trace)
    Private m_testTrace As New DataModels.TestTrace
    Private m_testTrace_dic As Dictionary(Of DataModels.TestEnd, DataModels.TestTrace)
    Private m_comment_OH As String
    Private m_comment_HO As String
    Private m_sweep_up_max_OH As Decimal
    Private m_sweep_down_max_OH As Decimal
    Private m_sweep_gap_OH As Decimal
    Private m_sweep_up_max_HO As Decimal
    Private m_sweep_down_max_HO As Decimal
    Private m_sweep_gap_HO As Decimal
    Private m_start_datetime As DateTime
    Private m_stop_datetime As DateTime
    Public Sub InitializeGUI()
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
        '给RFPAInterface的frmInterface的下拉列表框TSDP_Mode的Text属性赋值，让下拉菜单显示mode
        GUI.ModeName = mode
        pRTP.product_mode = mode

        GUI.MTR_Version = "1.0"

        GUI.repeatEnabled = False
        ' GUI.flashWhenTestDone = True
        GUI.DefineToolsMenu("Device Setup", "Gage R&&R")
        'GUI.DefineSAPMenu("SAP Fail Safe")
        'GUI.DefineTroubleshootMenu("DebugTest", "VibController") ', "RETController", "VibController")

        GUI.ShowSplash(3)
        GUI.ShowInterface()

        '向RFPAInterface的frmInterface的TSDP_Mode下拉列表框添加所有mode
        '包括PROD、NPI、RD、REL、DOE和Retest
        TestModules.LoadModes()
        '向RFPAInterface的frmInterface的TSDP_PhaseStation下拉列表框添加参数mode模式下的所有PhaseStation
        '并赋值给pRTP.M_phase_station_main
        TestModules.LoadPhaseStation(mode)
        ' MenuClick("PRODUCTS", szProduction(szProduction.GetLowerBound(0)))
    End Sub
    Public Sub AbortTest() Implements ITestPlan.AbortTest
        pAbortFlag = True
    End Sub

    Public Sub ExitInterface(ByRef Cancel As Short) Implements ITestPlan.ExitInterface
        End
    End Sub
    Public Sub MenuClick(MenuName As String, ByRef ItemName As String) Implements ITestPlan.MenuClick
        If MenuName = "Tools" Then
            If ItemName = "Device Setup" Then
                FormConfig.ShowDialog()
            End If
            If ItemName = "Gage R&&R" Then
                Dim frmGRR As Form = New FormGRR()
                frmGRR.ShowDialog()
            End If
            'ElseIf MenuName = "Troubleshoot" Then
            '    If ItemName = "DebugTest" Then
            '        FormDebug.ShowDialog()
            '    ElseIf ItemName = "RETController" Then
            '        FormRetController.Show()
            '    ElseIf ItemName = "VibController" Then
            '        FormVibController.ShowDialog()
            '    End If
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
    Public Function CheckSN(bkCable As Cable, lastTestLengthM As Decimal) As Boolean Implements ITestPlan.CheckSN
        Try

            GUI.ClearStatusMsg()

            Dim SN As String = bkCable.Serial_Number
            Dim PN As String = bkCable.Part_Number

            pRTP.length = bkCable.Original_Length_M
            pRTP.lastTestLengthM = lastTestLengthM
            pRTP.work_order = bkCable.Work_Order
            pRTP.core_number = bkCable.Core_Number
            pRTP.temperature = bkCable.Temperature_C
            pRTP.test_connector = bkCable.Test_Connector

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

            Return True

        Catch ex As Exception
            MsgBox("TestPlan.CheckSN()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function
    Private Function CheckSNTestCount(ByVal SN As String) As Boolean
        Try
            If pRTP.product_mode = _MODE Then
                Dim phaseStatusList As List(Of CATS.Model.cq_phases_status)
                Dim cq_phstatusBll As New CATS.BLL.cq_phases_statusManager
                phaseStatusList = cq_phstatusBll.SelectAllPhaseStatusBySn(SN, pRTP.factory, _MODE, pRTP.M_phase_station_main.phase_station)
                If phaseStatusList IsNot Nothing Then
                    Dim query = From phaseStatus In phaseStatusList
                                Where phaseStatus.phase <> "RL_ISO"
                    If query.Count = 0 Then
                        AddStatusMsg(String.Format("Input SN = {0}, Fresh Test", SN))
                    Else
                        Select Case query.Count
                            Case 1
                                AddStatusMsg(String.Format("Input SN = {0}, Second Test", SN))
                            Case 2
                                AddStatusMsg(String.Format("Input SN = {0}, Third Test", SN))
                            Case Else
                                AddStatusMsg(String.Format("Input SN = {0}, Test Times = {1}, STOP TEST!", SN, phaseStatusList.Count))
                                Return False
                        End Select
                    End If
                Else
                    AddStatusMsg(String.Format("Input SN = {0}, Fresh Test", SN))
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("CheckSNTestCount" & ex.Message)
        End Try
    End Function

    Public Function LoadPhaseStatus(SN As String, PN As String) As Boolean Implements ITestPlan.LoadPhaseStatus
        Try
            '读取已经测试的Test Step状态并返回给LstTestStep
            AddStatusMsg(String.Format("Start to load phase status..."))

            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            m_MostRecentPhaseStatus_List = cq_phstatus.SelectAll(PN, SN, pRTP.product_mode, pRTP.M_phase_station_main.id)
            If m_MostRecentPhaseStatus_List Is Nothing Then m_MostRecentPimPhaseStatus = Nothing : Return False
            LoadTestPhasesStatus(m_spec, m_MostRecentPhaseStatus_List)

            m_MostRecentPimPhaseStatus = m_MostRecentPhaseStatus_List.Find(Function(o) o.phase = m_spec.TestPhaseList(0).Name)
            If m_MostRecentPimPhaseStatus Is Nothing Then
                AddStatusMsg(String.Format("The most recent phase is {0}, not match {1}", m_MostRecentPhaseStatus_List.First.phase, m_spec.TestPhaseList(0).Name))
                Return False
            End If

            'Dim meas_phaseM As CATS.Model.meas_phase
            'Dim meas_phaseBll As New CATS.BLL.meas_phaseManager
            'meas_phaseM = meas_phaseBll.SelectById(m_MostRecentPimPhaseStatus.meas_phase_id)
            'If meas_phaseM Is Nothing Then Return False
            'If m_spec.TestPhaseList(0).SpecMainId <> meas_phaseM.spec_main_id Then Return False

            AddStatusMsg(String.Format("Load the most recent phase status with id = {0}", m_MostRecentPimPhaseStatus.meas_phase_id))

            '检查TSDP_PhaseStation所选的PhaseStation是否已经添加到product_mode_phase_station表中
            If CheckPhaseStation(m_spec.ProductModeId, pRTP.M_phase_station_main) = False Then
                AddStatusMsg("Not find test_station = " & pRTP.M_phase_station_main.phase_station)
                MsgBox("Not find Test Station -- " & pRTP.M_phase_station_main.phase_station & " ." & vbCrLf &
                    "- Product -- " & pRTP.M_product_main.product_name & vbCrLf &
                    "- MODE -- " & pRTP.product_mode, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If
            AddStatusMsg(String.Format("Load the most recent status done"))
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
            If m_spec Is Nothing Or m_spec.TestPhaseList.Count = 0 Then
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

            Dim dr() As DataRow = pAppCfg.GetInstruments().Select("Enable = 'True'")
            Dim isPim700LChecked As Boolean = False
            Dim isPim700UChecked As Boolean = False
            Dim pimConfiguredTestPhase As String = ""

            For Each testPhase As TestSpec.TestPhase In m_spec.TestPhaseList
                Dim phaseName As String = testPhase.Name.ToUpper
                For Each row As DataRow In dr
                    pimConfiguredTestPhase = row.Field(Of String)("Model")
                    If phaseName = "PIM700LU" Then
                        If row.Field(Of String)("Model") = "PIM700L" Then
                            isPim700LChecked = True
                        End If
                        If row.Field(Of String)("Model") = "PIM700U" Then
                            isPim700UChecked = True
                        End If
                        If isPim700LChecked AndAlso isPim700UChecked Then
                            tmpTestSpec.TestPhaseList.Add(testPhase)
                            Exit For
                        End If
                    Else
                        If row.Field(Of String)("Model") = phaseName Then
                            tmpTestSpec.TestPhaseList.Add(testPhase)
                            Exit For
                        End If
                    End If
                Next
            Next

            m_spec = tmpTestSpec

            AddStatusMsg(String.Format("Instrument configuration is {0}", pimConfiguredTestPhase))

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
    Public Function LoadAllCableType() As List(Of String) Implements ITestPlan.LoadAllCableType
        Try
            Dim resp As New List(Of String)
            Dim pmML As List(Of CATS.Model.product_main)
            Dim pmBll As New CATS.BLL.product_mainManager
            pmML = pmBll.SelectAll()
            If pmML Is Nothing Then Return Nothing
            Dim query = From Product In pmML
                        Where Product.family = "Cable"
                        Order By Product.product_name Descending
            For Each product In query
                resp.Add(product.product_name)
            Next
            Return resp
        Catch ex As Exception
            Throw New Exception("LoadAllCableType()::" & ex.Message)
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

            If pAppCfg.SaveTestMode(modeM.mode) = False Then
                MsgBox("Set mode configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            pRTP.product_mode = modeM.mode
            GUI.ModeName = modeM.mode
            TestModules.LoadPhaseStation(modeM.mode)

            Return True

        Catch ex As Exception
            MsgBox("Set mode()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    Public Function CheckSN(SN As String) As Cable Implements ITestPlan.CheckSN
        Try
            pRTP.barcode = SN
            Dim resp As New Cable

            Dim cq_product_infosBll As New CATS.BLL.cq_product_infosManager
            Dim cq_product_infos As CATS.Model.cq_product_infos

            cq_product_infos = cq_product_infosBll.SelectByProductSn(SN)

            If cq_product_infos Is Nothing Then Return Nothing

            With resp
                resp.Serial_Number = SN
                resp.Original_Length_M = cq_product_infos.M_product_cable.Original_length_m
                resp.Part_Number = cq_product_infos.M_product_main.product_name
                resp.Core_Number = cq_product_infos.M_product_cable.Core_number
                resp.Work_Order = cq_product_infos.M_product_wo.wo_descr
            End With

            Return resp

        Catch ex As Exception
            Throw New Exception("TestPlan.CheckSN()::" & ex.Message)
        End Try
    End Function

    Public Function LoadPhaseExtendCable(SN As String, PN As String) As frmNewCable.MeasPhaseExtendCable Implements ITestPlan.LoadPhaseExtendCable
        Try
            AddStatusMsg(String.Format("Start to load most recenet phase status ..."))

            Dim resp As New frmNewCable.MeasPhaseExtendCable
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            ' Return most recent RL_ISO and PIM* phase status list
            Dim MostRecentPhaseStatus_List As List(Of CATS.Model.cq_phases_status) = cq_phstatus.SelectMostRecentPhaseStatus(PN, SN, pRTP.product_mode, pRTP.M_phase_station_main.id)
            If MostRecentPhaseStatus_List Is Nothing Then
                AddStatusMsg(String.Format("Not found any phase status"))
                Return Nothing
            End If

            ' Find the last test phase status regardless of whether it is PIM or Spara phase
            Dim query = From phaseStatus In MostRecentPhaseStatus_List
                        Order By phaseStatus.start_datetime Descending

            Dim MostRecentPhaseStatus = query.First

            AddStatusMsg(String.Format("Found the most recent phase status with id = {0}", MostRecentPhaseStatus.meas_phase_id))

            Dim lastMeasPhaseExtendCable As CATS.Model.meas_phase_extend_cable
            Dim mpecBll As New CATS.BLL.meas_phase_extend_cableManager
            lastMeasPhaseExtendCable = mpecBll.SelectByMeasPhaseId(MostRecentPhaseStatus.meas_phase_id)
            If lastMeasPhaseExtendCable Is Nothing Then Return Nothing

            With resp
                resp.meas_phase_id = lastMeasPhaseExtendCable.Meas_phase_id
                resp.test_length_m = lastMeasPhaseExtendCable.Test_Length_m
                resp.test_connector = lastMeasPhaseExtendCable.Test_connector
                resp.temp_c = lastMeasPhaseExtendCable.Temperature_c
                resp.notes = lastMeasPhaseExtendCable.Notes
            End With

            AddStatusMsg(String.Format("Load the most recent phase status done"))

            Return resp

        Catch ex As Exception
            Throw New Exception("TestPlan.LoadPhaseExtendCable()::" & ex.Message)
        End Try
    End Function
    Public Function GetDbCableInput(SN As String) As frmNewCable.CableInput Implements ITestPlan.GetDbCableInput
        Try
            AddStatusMsg(String.Format("Start to load cable number: {0} input by production line ...", SN))

            Dim resp As New frmNewCable.CableInput
            Dim cableInputBll As New CATS.BLL.cable_inputManager
            Dim cableInputDbM As CATS.Model.cable_input
            cableInputDbM = cableInputBll.SelectByCableNumber(SN)
            If cableInputDbM Is Nothing Then
                AddStatusMsg(String.Format("Not found cable number: {0} input by production line", SN))
                Return Nothing
            End If

            With resp
                resp.cable_number = cableInputDbM.Cable_number
                resp.cable_type = cableInputDbM.Cable_type
                resp.original_length = cableInputDbM.Original_length
                resp.core_number = cableInputDbM.Core_number
                resp.work_order = cableInputDbM.Work_order
            End With

            AddStatusMsg(String.Format("Load cable number: {0} input by production line done", SN))

            Return resp

        Catch ex As Exception
            Throw New Exception("TestPlan.GetDbCableInput()::" & ex.Message)
        End Try
    End Function
    Public Function LoadGroupStatus() As Boolean Implements ITestPlan.LoadGroupStatus
        Try
            If m_MostRecentPimPhaseStatus Is Nothing Then Return False

            AddStatusMsg(String.Format("Start to load group status..."))

            Dim meas_groupBll As New CATS.BLL.meas_groupManager
            m_MostRecentGroupStatus_List = meas_groupBll.SelectBy_meas_phase_id(m_MostRecentPimPhaseStatus.meas_phase_id)
            If m_MostRecentGroupStatus_List Is Nothing Then Return False
            Dim group_mainM As CATS.Model.group_main
            Dim group_mainBll As New CATS.BLL.group_mainManager
            For Each meas_group In m_MostRecentGroupStatus_List
                AddStatusMsg(String.Format("Load the most recent group status with id = {0}", meas_group.id))
                Dim groupStatus As Short
                Select Case meas_group.group_status
                    Case "P"
                        groupStatus = 2
                    Case "F"
                        groupStatus = 3
                    Case "A"
                        groupStatus = 4
                    Case "E"
                        groupStatus = 5
                End Select
                group_mainM = group_mainBll.SelectById(meas_group.group_main_id)
                GUI.SetGroupStatus(group_mainM.group_name, groupStatus)
            Next

            AddStatusMsg(String.Format("Load the most recent group status done"))

            Return True
        Catch ex As Exception
            Throw New Exception("TestPlan.LoadGroupStatus()::" & ex.Message)
        End Try
    End Function

    Public Function LoadIMeasDetailReport() As Boolean Implements ITestPlan.LoadMeasDetail
        Try
            AddStatusMsg(String.Format("Start to load measure detail status..."))
            m_testTrace_dic = New Dictionary(Of DataModels.TestEnd, DataModels.TestTrace)
            Dim meas_detailBll As New CATS.BLL.meas_detailManager
            Dim meas_traceBll As New CATS.BLL.meas_traceManager
            Dim cq_spec_meas_detailBll As New CATS.BLL.cq_spec_imd_detailsManager
            If m_MostRecentGroupStatus_List Is Nothing Then Return False
            Dim pr As New TestReport.ReportModule
            Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
            Dim portName As String
            For Each meas_group In m_MostRecentGroupStatus_List
                portName = m_spec.TestPhaseList(0).TestGroupList.Find(Function(o) o.Id = meas_group.group_main_id).Name
                Select Case meas_group.group_status
                    Case "A"
                        If portName.Contains("PORT1") Then
                            GUI.RecordResult("OH Test Aborted", -1, 0, 0)
                        Else
                            GUI.RecordResult("HO Test Aborted", -1, 0, 0)
                        End If
                        GUI.DisplayBlankRow()
                    Case "E"
                        If portName.Contains("PORT1") Then
                            GUI.RecordResult("OH Test Error", -1, 0, 0)
                        Else
                            GUI.RecordResult("HO Test Error", -1, 0, 0)
                        End If
                        GUI.DisplayBlankRow()
                End Select
                InitializeTestTrace()
                m_cq_spec_meas_detail_list = cq_spec_meas_detailBll.SelectAllByGroupMainId(meas_group.group_main_id)
                m_meas_detail_list = meas_detailBll.SelectBy_meas_group_id(meas_group.id)
                If m_meas_detail_list Is Nothing Then Continue For
                Dim query = From meas_detail In m_meas_detail_list
                            Where meas_detail.meas_phase_id = m_MostRecentPimPhaseStatus.meas_phase_id And meas_detail.last_test_flag = True
                If query.Count = 0 Then Continue For
                For Each meas_detail In query
                    AddStatusMsg(String.Format("Load the most recent measure detail status with id = {0}", meas_detail.id))
                    m_meas_trace_list = meas_traceBll.SelectBy_meas_detial_id(meas_detail.id)
                Next
                If m_meas_trace_list Is Nothing Then Continue For
                Dim imdPoints As New List(Of DataModels.FrequencyPoint)
                For Each meas_trace In m_meas_trace_list
                    AddPointToList(meas_trace)
                Next
                For Each testItem In m_cq_spec_meas_detail_list
                    crList = pr.GetMeasCriteriaList(m_testTrace, testItem, m_spec.TestPhaseList.Find(Function(o) o.Name = m_MostRecentPimPhaseStatus.phase).TestCriteriaList)
                    pr.PrintTResultDcf(crList)
                    GUI.DisplayBlankRow()
                Next
                If portName.Contains("PORT1") Then
                    m_comment_OH = query.First.meas_string
                    For Each p As KeyValuePair(Of String, CATS.Model.meas_criteria) In crList
                        If p.Key.Contains("UPSWEEP_MAX") Then
                            m_sweep_up_max_OH = p.Value.meas_value
                        End If
                        If p.Key.Contains("DOWNSWEEP_MAX") Then
                            m_sweep_down_max_OH = p.Value.meas_value
                        End If
                        If p.Key.Contains("GAP_UPDOWN") Then
                            m_sweep_gap_OH = p.Value.meas_value
                        End If
                    Next
                    If m_testTrace_dic.ContainsKey(0) = False Then
                        m_testTrace_dic.Add(DataModels.TestEnd.OH, m_testTrace)
                    End If
                ElseIf portName.Contains("PORT2") Then
                    m_comment_HO = query.First.meas_string
                    For Each p As KeyValuePair(Of String, CATS.Model.meas_criteria) In crList
                        If p.Key.Contains("UPSWEEP_MAX") Then
                            m_sweep_up_max_HO = p.Value.meas_value
                        End If
                        If p.Key.Contains("DOWNSWEEP_MAX") Then
                            m_sweep_down_max_HO = p.Value.meas_value
                        End If
                        If p.Key.Contains("GAP_UPDOWN") Then
                            m_sweep_gap_HO = p.Value.meas_value
                        End If
                    Next
                    If m_testTrace_dic.ContainsKey(1) = False Then
                        m_testTrace_dic.Add(DataModels.TestEnd.HO, m_testTrace)
                    End If
                End If
            Next
            AddStatusMsg(String.Format("Load the most recent measure detail status done"))
            Return True
        Catch ex As Exception
            Throw New Exception("TestPlan.LoadItemReport()::" & ex.Message)
        End Try
    End Function
    Private Function GetInstrumentVendorId(bandName As String) As Integer
        Try
            Dim instrPara As CATSPimConfig.LocalConfig.InstrPara
            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager
            instrPara = pAppCfg.GetInstrumentConfig(bandName)
            iv = ivBll.SelectByVendorName(instrPara.Vendor.Trim)
            If iv Is Nothing Then Return -1
            Return iv.id
        Catch ex As Exception
            Throw New Exception("TestPlan.GetInstrumentVendorId()::" & ex.Message)
        End Try
    End Function
    Private Sub InitializeTestTrace()
        Try
            m_testTrace = New DataModels.TestTrace
            m_testTrace.SweepDown = New List(Of DataModels.FrequencyPoint)
            m_testTrace.SweepUp = New List(Of DataModels.FrequencyPoint)
            m_testTrace.Sweep = New List(Of DataModels.FrequencyPoint)
            m_testTrace.TwoTone = New List(Of DataModels.FrequencyPoint)
            m_testTrace.TwoToneFilter = New List(Of DataModels.FrequencyPoint)
            m_testTrace.Lambda = New List(Of PointF)
            m_testTrace.StsA = New Dictionary(Of String, List(Of DataModels.FrequencyPoint))
            m_testTrace.StsC = New Dictionary(Of String, List(Of DataModels.FrequencyPoint))
        Catch ex As Exception
            Throw New Exception("TestPlan.InitializeTestTrace()::" & ex.Message)
        End Try
    End Sub
    Private Sub AddPointToList(testTrace As CATS.Model.meas_trace)
        Try
            Dim imdPointRxFreq() As String = testTrace.x1_data.Split(",")
            Dim imdPointTxlFreq() As String = testTrace.x2_data.Split(",")
            Dim imdPointTxrFreq() As String = testTrace.x3_data.Split(",")
            Dim imdPointYData() As String = testTrace.y1_data.Split(",")

            Dim seriesId As Integer = testTrace.trace_idx
            For i As Integer = 0 To imdPointRxFreq.Length - 1
                Dim imdPoint As New DataModels.FrequencyPoint
                imdPoint.SeriesId = testTrace.trace_idx
                imdPoint.TxlFreq = imdPointTxlFreq(i)
                imdPoint.TxrFreq = imdPointTxrFreq(i)
                imdPoint.RxFreq = imdPointRxFreq(i)
                imdPoint.XData = imdPointRxFreq(i)
                imdPoint.YData = imdPointYData(i)

                If testTrace.trace_name = "SweepUp" Then
                    m_testTrace.SweepUp.Add(imdPoint)
                    m_testTrace.Sweep.Add(imdPoint)
                ElseIf testTrace.trace_name = "SweepDown" Then
                    m_testTrace.SweepDown.Add(imdPoint)
                    m_testTrace.Sweep.Add(imdPoint)
                ElseIf testTrace.trace_name.Contains("STS-A") Then
                    If m_testTrace.StsA.ContainsKey(seriesId) = False Then
                        m_testTrace.StsA.Add(seriesId, New List(Of DataModels.FrequencyPoint))
                    End If
                    m_testTrace.StsA(seriesId).Add(imdPoint)
                ElseIf testTrace.trace_name.Contains("STS-C") Then
                    If m_testTrace.StsC.ContainsKey(seriesId) = False Then
                        m_testTrace.StsC.Add(seriesId, New List(Of DataModels.FrequencyPoint))
                    End If
                    m_testTrace.StsC(seriesId).Add(imdPoint)
                ElseIf testTrace.trace_name = "2Tone" Then
                    m_testTrace.TwoTone.Add(imdPoint)
                End If
            Next
        Catch ex As Exception
            Throw New Exception("TestPlan.AddPointToList()::" & ex.Message)
        End Try
    End Sub
    Public Function LoadTestConnector(CableType As String) As List(Of String) Implements ITestPlan.LoadTestConnector
        Try
            Dim resp As New List(Of String)
            Dim ccML As List(Of CATS.Model.cable_connector)
            Dim ccBll As New CATS.BLL.cable_connectorManager
            ccML = ccBll.SelectByCableType(CableType)
            If ccML Is Nothing Then Return Nothing
            Dim query = From ccM In ccML
                        Where ccM.Gen3 = "Cable"
                        Order By ccM.Part_number
            For Each connector In query
                resp.Add(connector.Part_number)
            Next
            Return resp
        Catch ex As Exception
            Throw New Exception("TestPlan.GetTestConnector()::" & ex.Message)
        End Try
    End Function
    Public Function LoadPimPlot() As Boolean Implements ITestPlan.LoadPimPlot
        Try
            If m_testTrace_dic.Count = 0 Then Return False
            AddStatusMsg(String.Format("Start to load PIM trace..."))

            Dim sfBox As CATS.Model.cfg_imd_sfbox
            Dim ffBox As CATS.Model.cfg_imd_ffbox
            Dim cfBox As CATS.Model.cfg_imd_cfbox
            Dim instrVendorId As Integer
            For Each testItem In m_cq_spec_meas_detail_list
                instrVendorId = GetInstrumentVendorId(testItem.cfg_imd_main.freq_band)
                If instrVendorId < 0 Then Continue For
                sfBox = GetSweepFreqs(testItem.cfg_imd_main, instrVendorId)
                ffBox = GetFixedFreqs(testItem.cfg_imd_main, instrVendorId)
                cfBox = GetCustomFreqs(testItem.cfg_imd_main, instrVendorId)
                Dim frm As New FormPimPlot
                frm.SF_Box = sfBox
                frm.FF_Box = ffBox
                frm.CF_Box = cfBox
                frm.TestTrace = m_testTrace_dic
                frm.TestItem = testItem
                frm.Comment_oh = m_comment_OH
                frm.Comment_ho = m_comment_HO
                frm.Sweep_up_max_OH = m_sweep_up_max_OH
                frm.Sweep_down_max_OH = m_sweep_down_max_OH
                frm.Sweep_gap_OH = m_sweep_gap_OH
                frm.Sweep_up_max_HO = m_sweep_up_max_HO
                frm.Sweep_down_max_HO = m_sweep_down_max_HO
                frm.Sweep_gap_HO = m_sweep_gap_HO
                frm.Start_datetime = m_start_datetime
                frm.Stop_datetime = m_stop_datetime
                frm.ShowDialog()
            Next
            Return True
        Catch ex As Exception
            Throw New Exception("TestPlan.LoadPimPlot()::" & ex.Message)
        End Try
    End Function
End Class
