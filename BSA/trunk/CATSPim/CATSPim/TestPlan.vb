Imports RFPATestInterface
Imports System.IO
Imports System.Xml.Serialization

Public Class TestPlan
	Implements RFPATestInterface.ITestPlan
	Private m_spec As New TestSpec
	Public Sub InitializeGUI()
		Dim szProduction(0) As String
		Dim mode As String

		pGui.AssignTestPlan(Me)

		' get productions from xml file //pXmlPathTestProduction
		'szProduction = GetTestProductions()
		'pGui.DefineProductsMenuAry(szProduction)

		pGui.INI_File = pAppIniFile

		'INSTR_NORMAL_TEST
		'pGui.UUT_Type = pRFPAInterfaceGlobals.Read_INI_Field("General", "LastModel", szProduction(0))
		'pGui.UUT_Type = "ANT"
		pGui.SW_Name = "STS PIM"
		pGui.SW_Version = Application.ProductVersion
		mode = pAppCfg.GetTestMode.ToString
		pGui.ModeName = mode
		pRTP.product_mode = mode

		pGui.MTR_Version = "1.0"
		pGui.FixtureID = "1.0"

		pGui.repeatEnabled = False
        ' pGui.flashWhenTestDone = True
        pGui.DefineToolsMenu("DeviceSetup", "CalibrateDate", "PowerCalibrateData")
        pGui.DefineTroubleshootMenu("DebugTest", "RETController", "VibController")

		pGui.ShowSplash(3)
		pGui.ShowInterface()

		LoadModes()
        TestModules.LoadPhaseStation(mode)

        '=======================Frank==========================
        pGui.connStr = GlobalVariables.pDbConnString
        '=================================================
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
            If ItemName = "DeviceSetup" Then
                FormConfig.Show()
            ElseIf ItemName = "CalibrateDate" Then
                CalibrationRecord.Show()
            ElseIf ItemName = "PowerCalibrateData" Then
                PowerCalData.Show()
            End If
        ElseIf MenuName = "Troubleshoot" Then
            If ItemName = "DebugTest" Then
				FormDebug.ShowDialog()
			ElseIf ItemName = "RETController" Then
				FormRetController.Show()
			ElseIf ItemName = "VibController" Then
				FormVibController.Show()
			End If
		End If
	End Sub
	Public Sub PostTest() Implements ITestPlan.PostTest
		'Throw New NotImplementedException()
	End Sub
	Public Sub PreTest(TestPhase As String, ByRef Barcode As String, ByRef Cancel As Boolean) Implements ITestPlan.PreTest
		pAbortFlag = False

	End Sub
	Public Sub RunTest(Barcode As String, TestPhase As String, ByRef Cancel As Boolean) Implements ITestPlan.RunTest

        Try

            '========================获取当前Phase 的spec
            Dim phase_Cal_spec As New TestSpec.TestPhase
            For Each p In m_spec.TestPhaseList
                If p.Name.ToUpper.Trim = TestPhase.ToUpper.Trim Then
                    phase_Cal_spec = p
                    Exit For
                End If
            Next
            ' phase_Cal_spec.TestGroupList(0).TestItemList(0).spec_detail.limit_up
            '========================

            'debug
            'If PN_Cal = "LOW-PIM-LOAD" Then
            '    If LowPim_Load_Spec_Enable = True Then
            '        GetLowPim_Load_Spec.Show()
            '        GetLowPim_Load_Spec.SetPhase(TestPhase)
            '        Return
            '    End If
            '    For Each TP1 In phase_Cal_spec.TestGroupList
            '        For Each TP2 In TP1.TestItemList
            '            TP2.spec_detail.limit_up = LowPim_Load_Spec
            '        Next
            '    Next
            'End If



#If Not DEBUG Then
            '============add check Cal funciton
            If PN_Cal = "-110DBM_PIM_STD" Or PN_Cal = "LOW-PIM-LOAD" Then

                If pRTP.M_phase_station_main.phase_station.Trim.ToUpper <> "FINALTEST" Then '必须在此条件下校准
                    MsgBox("Need to Calibration Under FinalTest mode ,please check !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Return
                End If

                If PN_Cal = "LOW-PIM-LOAD" Then
                    If LowPim_Load_Spec_Enable = True Then
                        GetLowPim_Load_Spec.Show()
                        GetLowPim_Load_Spec.SetPhase(TestPhase)
                        Return
                    End If
                    For Each TP1 In phase_Cal_spec.TestGroupList
                        For Each TP2 In TP1.TestItemList
                            TP2.spec_detail.limit_up = LowPim_Load_Spec
                        Next
                    Next
                End If

            Else
                If CalibrationRecord.CalEnable = True Then ' True 校准功能激活，False 校准功能关闭
                    If CalibrationRecord.check_Cal(TestPhase.ToUpper.Trim, CSng(phase_Cal_spec.TestGroupList(0).TestItemList(0).spec_detail.limit_up)) = False Then
                        MsgBox("Need to Calibration for " & TestPhase.ToUpper.Trim & " now,please check !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        CalibrationRecord.Show()
                        Return
                    End If
                End If
            End If
            '================
#End If

            'check chamberID 0228 2019
            ' pGui.ChamberIDdisplay("Chamber18")
            ''If pAppCfg.GetChamberIDCheck.Enable Then
            ''    ChamberID = "Unknow"
            ''    ChamberID = Check_PIMChamberID()
            ''    pGui.ChamberIDdisplay(ChamberID)  ' ' add by tony for monitor door  
            ''Else
            ''    pGui.ChamberIDdisplay_Disable()
            ''    ChamberID = "  "
            ''End If


            Dim phase As New TestSpec.TestPhase

            pRTP.barcode = Barcode

            pRTP.product_mode_id = m_spec.ProductModeId

            For Each p In m_spec.TestPhaseList
                If p.Name.ToUpper.Trim = TestPhase.ToUpper.Trim Then
                    phase = p
                    Exit For
                End If
            Next

            pRTP.AlgoParas = GetAlgoParasBySpecMainId(phase.SpecMainId)


            If CheckPhaseStation(m_spec.ProductModeId, pRTP.M_phase_station_main) = False Then
                pGui.AddStatusMsg("Not find test_station = " & pRTP.M_phase_station_main.phase_station, True)
                MsgBox("Not find Test Station -- " & pRTP.M_phase_station_main.phase_station & " ." & vbCrLf &
                    "- Product -- " & pRTP.M_product_main.product_name & vbCrLf &
                    "- MODE -- " & pRTP.product_mode, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return
            End If

            If pRTP.M_phase_station_main.phase_station.Trim.ToUpper <> "FINALTEST" Then
                Dim frm As New FormAlarm
                frm.ShowTestStationEnable = True
                frm.ShowMessage = "FINALTEST"
                frm.ShowDialog()
                If frm.Result = DialogResult.No Then Return
            Else
                If pFactory = "GOA" And pRTP.product_mode <> "PROD" Then
                    Dim frm As New FormAlarm
                    frm.ShowModeEnable = True
                    frm.ShowMessage = "PROD"
                    frm.ShowDialog()
                    If frm.Result = DialogResult.No Then Return
                End If
            End If

            If MiiFactory.CheckMiiRouting(pRTP.M_phase_station_main, Barcode) = False Then Return


            If TestModules.RunTestPhase(phase, Cancel) = False Then
                pGui.RecordResult("PhaseTest", -1, 0, 0)
            End If


        Catch ex As Exception
            MsgBox("RunTest()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try


	End Sub

	Public Sub RunTestGroup(TestGroup As String, Barcode As String, TestPhase As String) Implements ITestPlan.RunTestGroup

	End Sub
    Public Function CheckSN(SN As String, PN As String) As Boolean Implements ITestPlan.CheckSN
        'pRTP.product_mode = pAppCfg.GetTestMode

        pGui.ClearStatusMsg()

        Try

#Const PC_CHECKPNSN = 0

            Dim product_mainManager As New CATS.BLL.product_mainManager
            Dim product_main As CATS.Model.product_main

#If PC_CHECKPNSN = 1 Then
      pGui.AddStatusMsg(String.Format("Find SN '{0}' in DB ... ", SN), False)
      Dim product_snManager As New CATS.BLL.product_snManager
      Dim product_sn As CATS.Model.product_sn = product_snManager.SelectBySerialNum(SN.ToUpper)
      If product_sn Is Nothing Then
        pGui.AddStatusMsg("Not Found!")
        Return False
      Else
        pGui.AddStatusMsg(String.Format("product_main_id = {0}, ", product_sn.product_main_id), False)
      End If
      product_main = product_mainManager.SelectById(product_sn.product_main_id)
#Else
            Dim product_snManager As New CATS.BLL.product_snManager
            Dim product_sn As CATS.Model.product_sn = product_snManager.SelectBySerialNum(SN.ToUpper)
            If product_sn Is Nothing Then
                product_main = product_mainManager.SelectByProductName(PN)
            Else
                product_main = product_mainManager.SelectById(product_sn.product_main_id)
            End If
#End If



            '#If CHECKPNSN = 1 Then
            '      Dim product_main As CATS.Model.product_main = product_mainManager.SelectById(product_sn.product_main_id)
            '#Else
            '      product_main = product_mainManager.SelectByProductName(PN)
            '#End If

            If product_main Is Nothing Then
                MsgBox("Not found this product, please confirm it!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            pGui.AddStatusMsg(String.Format("Input SN = {0}, Db PN = {1}", SN, product_main.product_name))
            If product_main.product_name.ToUpper <> PN.ToUpper Then
                pGui.AddStatusMsg(String.Format("Input PN = {0}, Mismatch!", PN.ToUpper))
                Return False
            End If

            pRTP.M_product_main = product_main

            m_spec = TestModules.GetTestSpec()
            If m_spec Is Nothing Then Return False

            LoadTestPhases(m_spec)
            LoadTestGroups(m_spec)

            '============add check Cal funciton
            PN_Cal = PN ' 给后面校准时用
            '============


            Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            lstPhaseStatus = cq_phstatus.SelectAll(PN, SN, pRTP.product_mode, pRTP.M_phase_station_main.id)
            LoadTestPhasesStatus(m_spec, lstPhaseStatus)

            If CheckPhaseStation(m_spec.ProductModeId, pRTP.M_phase_station_main) = False Then
                pGui.AddStatusMsg("Not find test_station = " & pRTP.M_phase_station_main.phase_station, True)
                MsgBox("Not find Test Station -- " & pRTP.M_phase_station_main.phase_station & " ." & vbCrLf &
                    "- Product -- " & pRTP.M_product_main.product_name & vbCrLf &
                    "- MODE -- " & pRTP.product_mode, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            End If

            LowPim_Load_Spec_Enable = True '20200316
            Return True

        Catch ex As Exception

            MsgBox("GUI.CheckSN()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False

        End Try

    End Function

    Public Function CheckSNSAP(ByVal SN As String, ByVal PN As String) As Boolean Implements ITestPlan.CheckSNSAP


#If DEBUG Then
        Return True ' Debug Mode 下不检查
#End If


        Try
            '只检查antenna----------------------------------------------------------------------------

#Const PC_CHECKPNSN = 0

            Dim product_mainManager0 As New CATS.BLL.product_mainManager
            Dim product_main0 As CATS.Model.product_main

#If PC_CHECKPNSN = 1 Then
      pGui.AddStatusMsg(String.Format("Find SN '{0}' in DB ... ", SN), False)
      Dim product_snManager As New CATS.BLL.product_snManager
      Dim product_sn As CATS.Model.product_sn = product_snManager.SelectBySerialNum(SN.ToUpper)
      If product_sn Is Nothing Then
        pGui.AddStatusMsg("Not Found!")
        Return False
      Else
        pGui.AddStatusMsg(String.Format("product_main_id = {0}, ", product_sn.product_main_id), False)
      End If
      product_main = product_mainManager.SelectById(product_sn.product_main_id)
#Else
            Dim product_snManager0 As New CATS.BLL.product_snManager
            Dim product_sn0 As CATS.Model.product_sn = product_snManager0.SelectBySerialNum(SN.ToUpper)
            If product_sn0 Is Nothing Then
                product_main0 = product_mainManager0.SelectByProductName(PN)
            Else
                product_main0 = product_mainManager0.SelectById(product_sn0.product_main_id)
            End If
#End If

            If product_main0 Is Nothing Then
                MsgBox("Not found this product, please confirm it!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Return False
            Else
                If product_main0.family.ToUpper <> "ANTENNA" Then Return True
            End If

            '----------------------------------------------------------------------------
            Dim cs As New SAPBridge.Service.CheckSerialNumber
            Dim req As New SAPBridge.Template.Request.CheckSerialNumber
            Dim rsp As SAPBridge.Template.Response.CheckSerialNumber

            req.SerialNumber = SN.Trim 'ok
            req.Material = PN.Trim 'ok
            req.Plant = pFactoryCode.Trim 'ok
            req.OrderId = "" 'ok

            rsp = cs.VerifyValidInSystem(req)

            'Debug.Print(rsp.Items(0).Message) 'Production order 6002351597 not found; please supply a valid prod order

            'Label1.Text = rsp.Items(0).Message
            'Label8.Text = rsp.Items(0).Type

            pGui.AddStatusMsg("Checked SN with SAP----->" & Now, True)

            If rsp.Items(0).Type.ToUpper = "S" Then
                pGui.AddStatusMsg(String.Format("Input PlantCode = {0},  SN = {1}, PN = {2} checked successfully in SAP", pFactoryCode, SN, PN))
                pGui.AddStatusMsg(rsp.Items(0).Message)
                Return True
            Else
                pGui.AddStatusMsg(String.Format("Input PlantCode = {0},  SN = {1}, PN = {2} checked fail in SAP", pFactoryCode, SN, PN))
                pGui.AddStatusMsg(rsp.Items(0).Message)
                Return False
            End If

        Catch ex As Exception
            pGui.AddStatusMsg(String.Format("Input PlantCode = {0},  SN = {1}, PN = {2} checked fail in SAP", pFactoryCode, SN, PN))
            ' pGui.AddStatusMsg(rsp.Items(0).Message)
            MsgBox(ex.Message)
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

			For Each cqpmps In cqpmpsML
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

			If pAppCfg.SaveTestMode(modeM.mode) = False Then
				MsgBox("Set mode configuration error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
				Return False
			End If

			pRTP.product_mode = modeM.mode
			pGui.ModeName = modeM.mode
			TestModules.LoadPhaseStation(modeM.mode)

			Return True

		Catch ex As Exception
			MsgBox("Set mode()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False
		End Try
	End Function
End Class
