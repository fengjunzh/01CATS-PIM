Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Threading
Module TestModules
	Private Delegate Sub delSubThread()
	'Private Delegate Sub delCallbackSub(result As IAsyncResult)
	'Private m_RetList As Dictionary(Of String, DataModels.RetDevice)
	'Private m_EqList As List(Of DataModels.Instrument)
	'Private m_PowerLoss As New DataModels.PowerLoss
	'Private m_CriteriaItems As New Dictionary(Of String, CATS.Model.cq_criteria_detail)

#Const PC_FAIL_STOP = 1

	Public Sub MyDelay(ByVal millSec As Short)

		If pAbortFlag = True Then Throw New AbortedException

		Threading.Thread.Sleep(millSec)

		My.Application.DoEvents()

	End Sub
#Region "Algorithm"
	Private Function ParseAlgorithmParas(modelList As List(Of CATS.Model.cq_algo_para_detail)) As DataModels.AlgorithmLimit
		Try
			Dim rnt As New DataModels.AlgorithmLimit

			For Each m In modelList
				Select Case m.algo_item.ToUpper.Trim
					Case "Lambda_CalcCompensation".ToUpper
						rnt.Lambda_CalcCompensation = m.algo_value
					Case "Lambda_CalcLimit".ToUpper
						rnt.Lambda_CalcLimit = m.algo_value
					Case "Lambda_MaxLimit".ToUpper
						rnt.Lambda_MaxLimit = m.algo_value
					Case "Lambda_PFLimit".ToUpper
						rnt.Lambda_PFLimit = m.algo_value
					Case "TwoTone_DiscardLimit".ToUpper
						rnt.TwoTone_DiscardLimit = m.algo_value
					Case "Ctrn_AvgMaxLimit".ToUpper
						rnt.Ctrn_AvgMaxLimit = m.algo_value
					Case "Ctrn_CalcCoefficient".ToUpper
						rnt.Ctrn_CalcCoefficient = m.algo_value
					Case "Ctrn_CalcMaxLimit".ToUpper
						rnt.Ctrn_CalcMaxLimit = m.algo_value
					Case "TwoTone_FilterCoefficient".ToUpper
						rnt.TwoTone_FilterCoefficient = m.algo_value
				End Select
			Next

			Return rnt

		Catch ex As Exception
			Throw New Exception("ParseAlgorithmParas()::" & ex.Message)
		End Try
	End Function
	Public Function GetAlgoParasBySpecMainId(spec_main_id As Integer) As DataModels.AlgorithmLimit
		Try

			Dim cq_algo As New CATS.BLL.cq_algo_para_detailManager
			Dim mlst As New List(Of CATS.Model.cq_algo_para_detail)

			mlst = cq_algo.SelectBySpecMainId(spec_main_id)

			Return ParseAlgorithmParas(mlst)

		Catch ex As Exception
			Throw New Exception("GetAlgoParasBySpecMainId()::" & ex.Message)
		End Try
	End Function
	Public Function GetAlgoParasByAlgoMainId(algo_main_id As Integer) As DataModels.AlgorithmLimit
		Try

			Dim cq_algo As New CATS.BLL.cq_algo_para_detailManager
			Dim mlst As New List(Of CATS.Model.cq_algo_para_detail)

			mlst = cq_algo.SelectById(algo_main_id)

			Return ParseAlgorithmParas(mlst)

		Catch ex As Exception
			Throw New Exception("GetAlgoParasByAlgoMainId()::" & ex.Message)
		End Try
	End Function

#End Region
#Const RET_MT = 1
	Public Sub StartRetThread()
		Try
			pRetThread = New ThreadRet
			Dim retType As ThreadRet.RetTypeEnum

			pRetThread.OpenRetDevice()

			retType = pRetThread.GetProductRetType

			If retType = ThreadRet.RetTypeEnum.RET Then
				Dim rmL As New List(Of CATS.Model.product_ret_map)
				If pRetThread.IsProductAccuRet Then 'AccuRet
					Dim prmBll As New CATS.BLL.product_ret_mapManager
					rmL = prmBll.SelectByProductMainId(pRTGlobal.M_product_main.id)
				End If

				pRetThread.ScanAntennaRet(pRTGlobal.M_product_main.dwtilt_type, rmL)

				If pRetThread.RetList Is Nothing Then Throw New Exception("Can't find Rets")
				pRetThread.CheckRets()
			End If

			If retType = ThreadRet.RetTypeEnum.RET Or retType = ThreadRet.RetTypeEnum.VET Then
				pRTGlobal.semporeRet.SignalLoopValue = 1

				'single thread debug


#If RET_MT = 1 Then
				Dim threadRet As New delSubThread(AddressOf pRetThread.SetTiltThread)
				threadRet.BeginInvoke(New AsyncCallback(AddressOf StopRetThread), vbNull)
#Else
				pRetThread.SetTiltThread()

#End If





			End If


		Catch ex As Exception
			Throw New Exception("StartRetThread()::" & ex.Message)
		End Try
	End Sub
	Public Sub StopRetThread(result As IAsyncResult)
		Try
			Dim retType As ThreadRet.RetTypeEnum = pRetThread.GetProductRetType

			If retType = ThreadRet.RetTypeEnum.RET Or retType = ThreadRet.RetTypeEnum.VET Then
				pRTGlobal.semporeRet.SignalLoopValue = 0

			End If

			pRetThread.CloseRetDevice()

		Catch ex As Exception
			Throw New Exception("StopRetThread()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetSemporeWhenEndTest()
		Try
			pRTGlobal.semporeRet.SignalLoopValue = 0
			pRTGlobal.semporeRet.SignalCount = 0
			pRTGlobal.semporePrompt.SignalCount = 0

		Catch ex As Exception
			Throw New Exception("ExitTestSempore()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetSemporeWhenTestExit()
		Try
			pRTGlobal.semporeRet.SignalCountDecrementOne()
			pRTGlobal.semporePrompt.SignalCountDecrementOne()
			pRTGlobal.semporeRetry.SignalCountDecrementOne()
		Catch ex As Exception
			Throw New Exception("SetTestSemporeWhenError()::" & ex.Message)
		End Try
	End Sub
	Public Function CreateCATSRunBox(phaseName As String) As CATSRunBox
		Try

			Return FormRun.AddPhaseBox(phaseName)

		Catch ex As Exception
			Throw New Exception("CreateCATSRunBox()::" & ex.Message)
		End Try

	End Function
#Const TEST_MT = 1
	Public Sub StartTestThread(phaseML As List(Of TestSpec.TestPhase))
		Try
			Dim tmpRunTest As ThreadTest

			For Each phaseM In phaseML
				Try
					tmpRunTest = New ThreadTest(phaseM)

					With tmpRunTest

						'tmpRunTest.OpenPimDevice()
						tmpRunTest.PhaseModel = phaseM

						' create tab & CATSRunBox control
						tmpRunTest.TestControl = CreateCATSRunBox(phaseM.Name)

#If TEST_MT = 1 Then
						Dim ThreadTest As New delSubThread(AddressOf tmpRunTest.RunPhaseTest)
						ThreadTest.BeginInvoke(New AsyncCallback(AddressOf StopTestThread), tmpRunTest)
#Else

						tmpRunTest.RunPhaseTest()
#End If

					End With

				Catch ex As Exception

					SetSemporeWhenTestExit()

					MsgBox("StartTest(" & phaseM.Name & ") Error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
					If MsgBox("Do you want cancel all phase test?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
						SetSemporeWhenEndTest()
						Exit For
					End If
				End Try

			Next

		Catch ex As Exception
			Throw New Exception("StartTestThread()::" & ex.Message)
		End Try
	End Sub
	Public Sub StopTestThread(result As IAsyncResult)
		Dim tmpRunTest As ThreadTest = result.AsyncState
		Try
			'Dim tmpResult As Runtime.Remoting.Messaging.AsyncResult = result
			'Dim tmpDel As delSubThread = tmpResult.AsyncDelegate
			'Dim data As Object = tmpDel.EndInvoke(tmpResult) '
			SetSemporeWhenTestExit()
#If INSTR_NORMAL_TEST = 1 Then
			tmpRunTest.ClosePimDevice()
#End If

		Catch ex As Exception
			Throw New Exception("StopTestThread(" & tmpRunTest.PhaseModel.Name & ")::" & ex.Message)
		End Try
	End Sub
	Public Sub RunTest(spec As TestSpec, testPhase As List(Of String))
		Try

			If OpenVibDevice() = False Then Throw New Exception("Open Vib device")
			If testPhase.Count = 0 Then Throw New Exception("Not select any phase.")

			pRTGlobal.semporeRet.SignalCount = testPhase.Count

			'Start Ret Thread
			StartRetThread()

			'Start Test Thread
			Dim phaseML As New List(Of TestSpec.TestPhase)
			For Each phase In testPhase
				For Each phaseModel In spec.TestPhaseList
					If phaseModel.Name.ToUpper.Trim = phase.ToUpper.Trim Then
						phaseML.Add(phaseModel)
					End If
				Next
			Next

			FormRun.Show()
			My.Application.DoEvents()

			pRTGlobal.semporePrompt.SignalCount = phaseML.Count

			StartTestThread(phaseML)


			'Load overall gridview

			'Start Prompt thread 
			Do While (pRTGlobal.semporePrompt.SignalCount > 0 Or pRTGlobal.semporeRet.SignalCount > 0)

				If pRTGlobal.semporeRetry.SignalValue <> 0 Then 'Promprt Retry
					If (pRTGlobal.semporeRetry.PromptRetryMessage) Or
						(pRTGlobal.semporePrompt.SignalValue = pRTGlobal.semporePrompt.SignalCount - 1) Or
						(pRTGlobal.semporeRet.SignalValue = pRTGlobal.semporeRet.SignalCount - 1) Then

						pPromptThread.ShowRetryPromptMessage()
					End If

				Else 'Promprt Connect
					If pRTGlobal.semporePrompt.IsPrompt Then
						pPromptThread.ShowPromptMessage()
						pRTGlobal.semporePrompt.SignalValue = 0
					End If
				End If
				My.Application.DoEvents()
			Loop

			pRTGlobal.semporeRet.SignalValue = 0

		Catch ex As Exception
			Throw New Exception("RunTest()::" & ex.Message)
		End Try
	End Sub


#Region "Get Spec"
	Public Function GetModePimTestSpec(product_main_id As Integer, mode As String) As TestSpec
		Try
			Dim cq_modes As New CATS.BLL.cq_modesManager
			Dim cq_modesList As List(Of CATS.Model.cq_modes)
			Dim ts As TestSpec = Nothing

			cq_modesList = cq_modes.SelectValidityByProductMainId(product_main_id)

			For Each cm As CATS.Model.cq_modes In cq_modesList
				If cm.mode.Trim.ToUpper = mode.Trim.ToUpper Then
					ts = New TestSpec(cm)
					Exit For
				End If
			Next

			Return ts

		Catch ex As Exception
			Throw New Exception("GetPhaseSpec()::" & ex.Message)
		End Try
	End Function

	Public Function GetSweepFreqs(ids As String) As CATS.Model.cfg_imd_sfbox
		Try
			Dim csf As New CATS.BLL.cfg_imd_sfboxManager
			'Dim mlst As New List(Of CATS.Model.cfg_imd_sfbox)
			If ids.Trim = "" Then Return Nothing

			Dim model As New CATS.Model.cfg_imd_sfbox
			Dim id As String = ids.Split(",")(0)

			Return csf.SelectById(id)


		Catch ex As Exception
			Throw New Exception("GetSweepFreqs()::" & ex.Message)
		End Try
	End Function
	Public Function GetFixedFreqs(ids As String) As CATS.Model.cfg_imd_ffbox
		Try
			If ids.Trim = "0" Then Return Nothing

			Dim model As New CATS.Model.cfg_imd_ffbox
			Dim id As String = ids.Split(",")(0)
			Dim cff As New CATS.BLL.cfg_imd_ffboxManager
			Return cff.SelectById(id)

		Catch ex As Exception
			Throw New Exception("GetFixedFreqs()::" & ex.Message)
		End Try
	End Function
	Public Function GetCustomFreqs(ids As String) As CATS.Model.cfg_imd_cfbox
		Try
			If ids.Trim = "" Then Return Nothing

			Dim model As New CATS.Model.cfg_imd_cfbox
			Dim id As String = ids.Split(",")(0)
			Dim ccf As New CATS.BLL.cfg_imd_cfboxManager

			Return ccf.SelectById(id)


		Catch ex As Exception
			Throw New Exception("GetCustomFreqs()::" & ex.Message)
		End Try
	End Function

#End Region

	Public Function GetTestSpec() As TestSpec
		Try
			Dim resp As New TestSpec

			pGui.AddStatusMsg(String.Format("Read test spec from DB ...product_main_id={0} , ", pRTGlobal.M_product_main.id), False)
			resp = TestModules.GetModePimTestSpec(pRTGlobal.M_product_main.id, pRTGlobal.product_mode)

			If resp IsNot Nothing Then
				pGui.AddStatusMsg("OK", True)
			Else
				pGui.AddStatusMsg("Not found!", True)
			End If


			Return resp

		Catch ex As Exception
			Throw New Exception("GetTestSpec()::" & ex.Message)
		End Try

	End Function
	Public Sub LoadTestPhases(spec As TestSpec)
		Try
			Dim phase(spec.TestPhaseList.Count - 1) As String
			Dim i As Short

			For i = 0 To spec.TestPhaseList.Count - 1
				phase(i) = spec.TestPhaseList(i).Name
			Next

			pGui.DefineTestSteps(phase)


		Catch ex As Exception
			Throw New Exception("LoadTestPhases()::" & ex.Message)
		End Try
	End Sub
	Public Sub LoadTestPhasesStatus(spec As TestSpec, status As List(Of CATS.Model.cq_phases_status))
		Try

			If status Is Nothing Then Return

			For Each sp In spec.TestPhaseList
				For Each st In status
					If sp.Name.Trim.ToUpper = st.phase.Trim.ToUpper Then
						If st.phase_status = "P" Then pGui.SetStepStatus(sp.Name.Trim.ToUpper, 2)
						If st.phase_status = "F" Then pGui.SetStepStatus(sp.Name.Trim.ToUpper, 3)
						If st.phase_status = "A" Then pGui.SetStepStatus(sp.Name.Trim.ToUpper, 4)
					End If
				Next
			Next

		Catch ex As Exception
			Throw New Exception("LoadTestPhasesStatus()::" & ex.Message)
		End Try
	End Sub
	Public Sub LoadTestGroups(spec As TestSpec)
		Try
			Dim group() As String
			Dim i As Short

			For Each phase In spec.TestPhaseList
				ReDim group(phase.TestGroupList.Count - 1)
				For i = 0 To phase.TestGroupList.Count - 1
					group(i) = phase.TestGroupList(i).Name
				Next
				pGui.DefineTestGroups(phase.Name, group)
			Next

		Catch ex As Exception
			Throw New Exception("LoadTestGroups()::" & ex.Message)
		End Try
	End Sub
	Private Sub InitVibDevice()
		Try

			Dim dev As New VibCtrl

			pVibCtrl = dev


		Catch ex As Exception
			Throw New Exception("InitVibDevice()::" & ex.Message)
		End Try

	End Sub
	Private Function OpenVibDevice() As Boolean
		Try
			If pAppCfg.GetVibrationConfig.Enable = True Then
				InitVibDevice()
				Return pVibCtrl.OpenDev()
			Else
				Return True
			End If

		Catch ex As Exception
			Throw New Exception("OpenVibDevice()::" & ex.Message)
		End Try
	End Function
	Private Sub CloseVibDevice()
		Try
			If pAppCfg.GetVibrationConfig.Enable = True Then
				pVibCtrl.CloseDev()
			End If

		Catch ex As Exception
			Throw New Exception("CloseVibDevice()::" & ex.Message)
		End Try
	End Sub
End Module
