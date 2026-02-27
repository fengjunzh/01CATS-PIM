Imports RFPATestInterface
Imports System.IO
Imports System.Xml.Serialization

Public Class TestPlan
	Implements RFPATestInterface.ITestPlan
	Private m_spec As New TestSpec
	Public Sub InitializeGUI()
		Dim szProduction(0) As String

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
		pGui.ModeName = pAppCfg.GetTestMode.ToString
		pGui.MTR_Version = "1.0"
		pGui.FixtureID = "1.0"

		pGui.repeatEnabled = False
		' pGui.flashWhenTestDone = True
		pGui.DefineToolsMenu("DeviceSetup")
		pGui.DefineTroubleshootMenu("DebugTest")

		pGui.ShowSplash(3)
		pGui.ShowInterface()

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
			End If
		ElseIf MenuName = "Troubleshoot" Then
			If ItemName = "DebugTest" Then
				FormDebug.ShowDialog()
			End If
		End If
	End Sub
	Public Sub PostTest() Implements ITestPlan.PostTest
		'Throw New NotImplementedException()
	End Sub
	Public Sub RunTestGroup(TestGroup As String, Barcode As String, TestPhase As String) Implements ITestPlan.RunTestGroup
		Throw New NotImplementedException()
	End Sub
	Public Function CheckSN(SN As String, PN As String) As Boolean Implements ITestPlan.CheckSN
		pRTGlobal.product_mode = pAppCfg.GetTestMode

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

			pRTGlobal.M_product_main = product_main

			m_spec = TestModules.GetTestSpec()
			If m_spec Is Nothing Then Return False

			LoadTestPhases(m_spec)
			LoadTestGroups(m_spec)


			Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
			Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
			lstPhaseStatus = cq_phstatus.SelectAll(PN, SN, pRTGlobal.product_mode)
			LoadTestPhasesStatus(m_spec, lstPhaseStatus)

			Return True


		Catch ex As Exception

			MsgBox("GUI.CheckSN()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False

		End Try

	End Function

	Public Sub RunTest(Barcode As String, TestPhase As List(Of String), ByRef Cancel As Boolean) Implements ITestPlan.RunTest
		Try
			Dim phase As New TestSpec.TestPhase

			pRTGlobal.barcode = Barcode
			pRTGlobal.product_mode_id = m_spec.ProductModeId
			TestModules.RunTest(m_spec, TestPhase)


		Catch ex As Exception
			MsgBox("RunTest()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub

	Public Sub PreTest(TestPhase As List(Of String), ByRef Barcode As String, ByRef Cancel As Boolean) Implements ITestPlan.PreTest
		pAbortFlag = False
	End Sub

	Public Sub Calibrate(teststep As String) Implements ITestPlan.Calibrate

	End Sub

	Public Sub LoadTestData(SN As String, PN As String, teststep As String) Implements ITestPlan.LoadTestData

	End Sub
End Class
