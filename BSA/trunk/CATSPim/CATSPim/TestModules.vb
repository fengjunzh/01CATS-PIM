Imports System.Windows.Forms.DataVisualization.Charting
Module TestModules
	Private m_RetList As Dictionary(Of String, DataModels.RetDevice)
	Private m_EqList As List(Of DataModels.Instrument)
	Private m_PowerLoss As New DataModels.PowerLoss
    Private m_CriteriaItems As New Dictionary(Of String, CATS.Model.cq_criteria_detail)
    Private pretest_setItem As New List(Of String) '20180816

#Const PC_FAIL_STOP = 0

    Public Sub MyDelay(ByVal millSec As Short)

		If pAbortFlag = True Then Throw New AbortedException

		Threading.Thread.Sleep(millSec)

		My.Application.DoEvents()

	End Sub

	Public Function GetVirtualImValue() As Single
		Try
			Randomize()

			Dim x As Single = CInt(580 * Rnd() + 14800) / 100

			Threading.Thread.Sleep(200)

			'Return -153
			Return Math.Round((-1) * x, 2)

		Catch ex As Exception
			Return 0
		End Try
	End Function
	Private Function FindRetId(RetMaps As List(Of CATS.Model.product_ret_map), key As String)
		Try
			If RetMaps Is Nothing Then Throw New Exception("No Ret Map in DB.")
			If RetMaps.Count = 0 Then Throw New Exception("No Ret Map  in DB.")

			For Each ret In RetMaps
				If ret.band_id.ToUpper.Trim = key.ToUpper.Trim Then
					Return ret.ret_idx
				End If
			Next

			Throw New Exception("Not found Ret Map of <" & key & ">")

		Catch ex As Exception
			Throw New Exception("ScanAntennaRet()::" & ex.Message)
		End Try
	End Function
	Public Function ScanAntennaRet(AntBarcode As String, RETPN As Short, product_main_id As Integer) As Dictionary(Of String, DataModels.RetDevice)
		Try
			Dim rmL As New List(Of CATS.Model.product_ret_map)
			Dim prmBll As New CATS.BLL.product_ret_mapManager
			rmL = prmBll.SelectByProductMainId(pRTP.M_product_main.id)

			Return ScanAntennaRet(AntBarcode, RETPN, rmL)

		Catch ex As Exception
			Throw New Exception("ScanAntennaRet(string,short,integer)::" & ex.Message)
		End Try
	End Function
	Public Function ScanAntennaRet(AntBarcode As String, RetPN As Short, RetMaps As List(Of CATS.Model.product_ret_map)) As Dictionary(Of String, DataModels.RetDevice)
		Try
			Dim resp As New Dictionary(Of String, DataModels.RetDevice)

			Dim tmpRet As DataModels.RetDevice
			Dim tmpDev As AisgDevice.stDeviceInfo
			Dim addrCount As Int16
			Dim addrI As Int16
			Dim subAddrI As Int16
			Dim dev As an3gppxLib.DeviceData
			Dim band_id As String = ""
			Dim ret_idx As String
			Dim rpId As Integer

			AntBarcode = AntBarcode.Trim.ToUpper

			addrCount = pRetCtrl.ScanDevice()
			If addrCount = 0 Then Return Nothing

			For addrI = 1 To addrCount

				tmpDev = pRetCtrl.GetDeviceInfo(addrI)

				If tmpDev.DevType = AisgDevice.DeviceType.SingleRet Then
					Dim pn As String = pRetCtrl.GetPartNumber(addrI)
					If pn.Trim.ToUpper = "BEAM_MASTER" Then Continue For

					dev = pRetCtrl.GetSingleDeviceInfo(addrI)
					tmpRet = New DataModels.RetDevice
					tmpRet.RetSn = tmpDev.SerialNumber
					tmpRet.AntennaSn = dev.AntennaSerialNumber 'tmpDev.SerialNumber
					tmpRet.AntModel = dev.AntennaModelNumber
					tmpRet.FwVersion = tmpDev.Firmware
					tmpRet.HwVersion = tmpDev.Hardware
					tmpRet.Type = tmpDev.DevType

					tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, 1)
					tmpRet.Tilt.GetCurrentTilt()

					pGui.AddStatusMsg(String.Format("RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
				  addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)

                    If RetPN = 3 Or tmpRet.Tilt.RetPartNumber.ToString.ToUpper.Contains("ACCURET") Then 'AccuRET

                        band_id = tmpRet.AntModel.ToString.Substring(tmpRet.AntModel.ToString.LastIndexOf("-") + 1)
                        ret_idx = FindRetId(RetMaps, band_id)

                        resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)

                    ElseIf RetPN = 4 Then

                        If pn.Trim.ToUpper = "COMMRET2S" Then
                            band_id = Right(tmpRet.RetSn, 2)
                            ret_idx = FindRetId(RetMaps, band_id)
                            resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)
                        Else
                            rpId = tmpRet.RetSn.ToUpper.Trim.IndexOf(AntBarcode) + AntBarcode.Length
                            band_id = tmpRet.RetSn.Substring(rpId)
                            ret_idx = FindRetId(RetMaps, band_id)
                            resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)
                        End If

                    Else
						resp.Add(tmpRet.RetSn & "." & Right(tmpRet.RetSn, 1), tmpRet)
					End If

				ElseIf tmpDev.DevType = AisgDevice.DeviceType.MultiRet Then
					For subAddrI = 1 To tmpDev.RetSubNumber

						tmpRet = New DataModels.RetDevice
						dev = pRetCtrl.GetMultiDeviceInfo(addrI, subAddrI)
						tmpRet.RetSn = tmpDev.SerialNumber 'dev.AntennaSerialNumber
						tmpRet.AntennaSn = dev.AntennaSerialNumber
						tmpRet.AntModel = dev.AntennaModelNumber
						tmpRet.FwVersion = tmpDev.Firmware
						tmpRet.HwVersion = tmpDev.Hardware
						tmpRet.Type = tmpDev.DevType

						tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, subAddrI)
						tmpRet.Tilt.GetCurrentTilt()
						resp.Add(tmpRet.RetSn & "." & subAddrI, tmpRet)

						pGui.AddStatusMsg(String.Format(" RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
										addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)

					Next

				End If
			Next

			Return resp

		Catch ex As Exception
			Throw New Exception("ScanAntennaRet(string,short,List(Of CATS.Model.product_ret_map))::" & ex.Message)
		End Try

	End Function
	Public Function ParseAlgorithmParas(modelList As List(Of CATS.Model.cq_algo_para_detail)) As DataModels.AlgorithmLimit
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

	'Public Function GetSweepFreqs(ids As String) As CATS.Model.cfg_imd_sfbox
	'  Try
	'    Dim csf As New CATS.BLL.cfg_imd_sfboxManager
	'    'Dim mlst As New List(Of CATS.Model.cfg_imd_sfbox)
	'    If ids.Trim = "" Then Return Nothing

	'    Dim model As New CATS.Model.cfg_imd_sfbox
	'    Dim id As String = ids.Split(",")(0)

	'    Return csf.SelectById(id)


	'  Catch ex As Exception
	'    Throw New Exception("GetSweepFreqs()::" & ex.Message)
	'  End Try
	'End Function
	'Public Function GetSweepFreqs(descr As String, vendor_id As Integer) As CATS.Model.cfg_imd_sfbox
	'	Try
	'		Dim resp As New CATS.Model.cfg_imd_sfbox
	'		Dim csf As New CATS.BLL.cfg_imd_sfboxManager
	'		'Dim mlst As New List(Of CATS.Model.cfg_imd_sfbox)
	'		If descr.Trim = "" Then Return Nothing

	'		Dim model As New CATS.Model.cfg_imd_sfbox

	'		resp = csf.SelectByDescrVendorId(descr.Trim, vendor_id)
	'		If resp Is Nothing Then Throw New Exception("Not find <" & descr & ">")

	'		If descr.ToUpper.Contains("STS-L") Or descr.ToUpper.Contains("STS-K") Then
	'			If resp.dfreq_step = 0 Or resp.ufreq_step = 0 Then Throw New Exception("test freq configuration is error <" & descr & "> in DB")
	'		ElseIf descr.ToUpper.Contains("STS-J") Then
	'			If resp.dfreq_step = 0 And resp.ufreq_step = 0 Then Throw New Exception("test freq configuration is error <" & descr & "> in DB")
	'		End If

	'		Return resp

	'	Catch ex As Exception
	'		Throw New Exception("GetSweepFreqs()::" & ex.Message)
	'	End Try
	'End Function
	Private Function CheckSweepFreqs(sfBox As CATS.Model.cfg_imd_sfbox, freq_descr As String) As Boolean
		Try
			Dim strFd As String = freq_descr.ToUpper.Trim

            If strFd.Contains("STS-J") Or strFd.Contains("STS-M") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 And sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-K") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 Or sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-L") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 Or sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-A") Then
                If sfBox Is Nothing Then Return True
				Return False
			End If

			Return True

		Catch ex As Exception
			Throw New Exception("CheckSweepFreqs()::" & ex.Message)
		End Try
	End Function
	Public Function GetSweepFreqs(cimM As CATS.Model.cfg_imd_main, vendor_id As Integer) As CATS.Model.cfg_imd_sfbox
		Try
			Dim resp As New CATS.Model.cfg_imd_sfbox
			Dim csf As New CATS.BLL.cfg_imd_sfboxManager
			If cimM Is Nothing Then Return Nothing

			resp = csf.SelectByCfgImdMainIdVendorId(cimM.id, vendor_id)

			If CheckSweepFreqs(resp, cimM.descr) = False Then Throw New Exception("test freq configuration is error <" & cimM.descr & "> in DB")

			Return resp

		Catch ex As Exception
			Throw New Exception("GetSweepFreqs()::" & ex.Message)
		End Try
	End Function
	'Public Function GetFixedFreqs(ids As String) As CATS.Model.cfg_imd_ffbox
	'   Try
	'     If ids.Trim = "0" Then Return Nothing

	'     Dim model As New CATS.Model.cfg_imd_ffbox
	'     Dim id As String = ids.Split(",")(0)
	'     Dim cff As New CATS.BLL.cfg_imd_ffboxManager
	'     Return cff.SelectById(id)

	'   Catch ex As Exception
	'     Throw New Exception("GetFixedFreqs()::" & ex.Message)
	'   End Try
	' End Function
	'Public Function GetFixedFreqs(descr As String, vendor_id As Integer) As CATS.Model.cfg_imd_ffbox
	'	Try
	'		Dim resp As CATS.Model.cfg_imd_ffbox
	'		If descr.Trim = "" Then Return Nothing

	'		Dim model As New CATS.Model.cfg_imd_ffbox
	'		Dim cff As New CATS.BLL.cfg_imd_ffboxManager
	'		resp = cff.SelectByDescrVendorId(descr, vendor_id)


	'		If descr.ToUpper.Contains("STS-L") Then
	'			If resp Is Nothing Then
	'				Throw New Exception("test freq configuration is error <" & descr & "> in DB")
	'			End If
	'		End If

	'		Return resp

	'	Catch ex As Exception
	'		Throw New Exception("GetFixedFreqs()::" & ex.Message)
	'	End Try
	'End Function

	Private Function CheckFixedFreqs(ffBox As CATS.Model.cfg_imd_ffbox, freq_descr As String) As Boolean
		Try
			Dim strFd As String = freq_descr.ToUpper.Trim

			If strFd.Contains("STS-L") Then
				If ffBox Is Nothing Then Return False
			ElseIf strFd.Contains("STS-A") Then
				If ffBox Is Nothing Then Return True
				Return False
			End If

			Return True

		Catch ex As Exception
			Throw New Exception("CheckFixedFreqs()::" & ex.Message)
		End Try
	End Function
	Public Function GetFixedFreqs(cimM As CATS.Model.cfg_imd_main, vendor_id As Integer) As CATS.Model.cfg_imd_ffbox
		Try
			Dim resp As CATS.Model.cfg_imd_ffbox
			If cimM Is Nothing Then Return Nothing

			Dim cff As New CATS.BLL.cfg_imd_ffboxManager
			resp = cff.SelectByCfgImdMainIdVendorId(cimM.id, vendor_id)

			If CheckFixedFreqs(resp, cimM.descr) = False Then Throw New Exception("test freq configuration is error <" & cimM.descr & "> in DB")
			Return resp

		Catch ex As Exception
			Throw New Exception("GetFixedFreqs()::" & ex.Message)
		End Try
	End Function
	'Public Function GetCustomFreqs(ids As String) As CATS.Model.cfg_imd_cfbox
	'  Try
	'    If ids.Trim = "" Then Return Nothing

	'    Dim model As New CATS.Model.cfg_imd_cfbox
	'    Dim id As String = ids.Split(",")(0)
	'    Dim ccf As New CATS.BLL.cfg_imd_cfboxManager

	'    Return ccf.SelectById(id)


	'  Catch ex As Exception
	'    Throw New Exception("GetCustomFreqs()::" & ex.Message)
	'  End Try
	'End Function
	'Public Function GetCustomFreqs(descr As String, vendor_id As Integer) As CATS.Model.cfg_imd_cfbox
	'	Try
	'		If descr.Trim = "" Then Return Nothing

	'		Dim model As New CATS.Model.cfg_imd_cfbox
	'		Dim ccf As New CATS.BLL.cfg_imd_cfboxManager

	'		Return ccf.SelectByDescrVendorId(descr, vendor_id)


	'	Catch ex As Exception
	'		Throw New Exception("GetCustomFreqs()::" & ex.Message)
	'	End Try
	'End Function
	Private Function CheckCustomFreqs(cfBox As CATS.Model.cfg_imd_cfbox, freq_descr As String) As Boolean
		Try
			Dim strFd As String = freq_descr.ToUpper.Trim

			If strFd.Contains("STS-A") Then
				If cfBox Is Nothing Then Return False

				Dim c1FreqArr() As String = cfBox.c1_freqs.Split(",")
				Dim c2FreqArr() As String = cfBox.c2_freqs.Split(",")
				Dim imdfreqArr() As String = cfBox.imd_freqs.Split(",")

				If c1FreqArr.Count <> c2FreqArr.Count Or c1FreqArr.Count <> imdfreqArr.Count Then
					Throw New Exception("test freqs configuration is error " & vbCrLf &
										"c1_freqs <" & cfBox.c1_freqs & ">" & vbCrLf &
										"c2_freqs <" & cfBox.c2_freqs & ">" & vbCrLf &
										"imd_freqs <" & cfBox.imd_freqs & ">")

				End If

				Dim f As Integer
				Dim freq As Decimal
				For f = 0 To c1FreqArr.GetUpperBound(0)
					freq = Calculate.GetImFreq(c1FreqArr(f), c2FreqArr(f), cfBox.imd_side, cfBox.imd_order)
					If freq <> imdfreqArr(f) Then Throw New Exception("test freqs configuration is error " & vbCrLf &
																	   "c1_freq= " & c1FreqArr(f) & " , c2_freq= " & c2FreqArr(f) & " , imd_freq= " & imdfreqArr(f))

				Next

			End If

			Return True

		Catch ex As Exception
			Throw New Exception("CheckCustomFreqs()::" & ex.Message)
		End Try
	End Function
	Public Function GetCustomFreqs(cimM As CATS.Model.cfg_imd_main, vendor_id As Integer) As CATS.Model.cfg_imd_cfbox
		Try
			If cimM Is Nothing Then Return Nothing

			Dim resp As New CATS.Model.cfg_imd_cfbox
			Dim ccf As New CATS.BLL.cfg_imd_cfboxManager

			resp = ccf.SelectByCfgImdMainIdVendorId(cimM.id, vendor_id)
			If CheckCustomFreqs(resp, cimM.descr) = False Then Throw New Exception("test freq configuration Is error <" & cimM.descr & "> in DB")

			Return resp

		Catch ex As Exception
			Throw New Exception("GetCustomFreqs()::" & ex.Message)
		End Try
	End Function
#End Region

	Public Function GetTestSpec() As TestSpec
		Try
			Dim resp As New TestSpec

			pGui.AddStatusMsg(String.Format("Read test spec from DB ...product_main_id={0} , ", pRTP.M_product_main.id), False)
			resp = TestModules.GetModePimTestSpec(pRTP.M_product_main.id, pRTP.product_mode)

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
            Cal_phasenamelist.Clear() '//add Cal

            For i = 0 To spec.TestPhaseList.Count - 1
                phase(i) = spec.TestPhaseList(i).Name
                Cal_phasenamelist.Add(phase(i)) '//add Cal
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
						If st.phase_status = "R" Then pGui.SetStepStatus(sp.Name.Trim.ToUpper, 5)
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
	Public Sub LoadPhaseStation(mode As String)
		Try
			Dim cqmpsML As New List(Of CATS.Model.cq_mode_phase_station)
			Dim cqmpsBll As New CATS.BLL.cq_mode_phase_stationManager
			Dim cqmpsM As CATS.Model.cq_mode_phase_station

			cqmpsML = cqmpsBll.SelectAllByMode(mode, True, True)

			If cqmpsML Is Nothing Then
				pRTP.M_phase_station_main = Nothing
				cqmpsM = Nothing
				MsgBox("Not find any test station in MODE<" & mode & ">.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
			Else
				cqmpsM = cqmpsML.Find(Function(o) o.M_mode_phase_station.default_selection = True)
				pRTP.M_phase_station_main = cqmpsM.M_phase_station_main
			End If

			pGui.DefinePhaseStationMenu(cqmpsML, cqmpsM)

		Catch ex As Exception
			Throw New Exception("LoadPhaseStation()::" & ex.Message)
		End Try
	End Sub
	Public Sub LoadModes()
		Try
			Dim modeML As List(Of CATS.Model.mode)
			Dim mode As New CATS.BLL.modeManager
			modeML = mode.SelectAll

			pGui.DefineModeMenu(modeML)


		Catch ex As Exception
			Throw New Exception("LoadMode()::" & ex.Message)
		End Try
	End Sub
	Private Function WriteReport(phReport As TestReport.XmlFramework.TPhase) As Boolean
		Try
			Dim rltReport As New TestReport.XmlFramework.Report
			Dim strFilePath As String = "rtmp.xml"

			With pRTP
				.meas_status = phReport.MeasStatus
			End With

			With rltReport
				.Type = 0
				.ConnString = pDbConnString
				.TestMisc = TestReport.XmlReport.WriteTestMisc
				.Head = TestReport.XmlReport.WriteHead()
				.AssyParts = TestReport.XmlReport.WriteAssyParts(m_RetList)
				.TestInstruments = TestReport.XmlReport.WriteInstrument(m_EqList)
				.TestPhase = phReport
			End With

			If TestReport.XmlReport.WriteReport(rltReport, pTestResultPath & strFilePath) = True Then
				Dim objEncryptor As New Encryptor
                Dim strDataFileName As String
                ' DataSavedAsDAT = ""

                strDataFileName = pRTP.barcode & "!" &
				  pRTP.meas_start_time.ToString("yyyyMMddHHmmss") & "!" &
				  pRTP.product_mode & "!" &
				  pRTP.M_product_main.product_name & "!" &
				  pRTP.phase & ".dat"

				objEncryptor.EncryptFile(pTestResultPath & strFilePath, pTestResultPath & strDataFileName)
				IO.File.Delete(pTestResultPath & strFilePath)

                ' DataSavedAsDAT = pTestResultPath & strDataFileName ' 测试数据.dat的保存路径

            End If

			Return True

		Catch ex As Exception
			MsgBox("WriteReport()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False
		End Try
	End Function
	Private Function OpenRetDevice() As Short
		Try
			Dim retPara As CATSPimConfig.LocalConfig.RetPara

			If pRTP.M_product_main.dwtilt_enable = True And (pRTP.M_product_main.dwtilt_type >= 2) Then

				If pRTP.M_phase_station_main.ret_validity = False Then Return 1

				retPara = pAppCfg.GetRetConfig()

				pRetCtrl = New AisgDevice(retPara.Address)
				If pRetCtrl.OpenDevice = False Then Throw New Exception("Open Ret device")

				Return 0

			Else

				Return 1

			End If

		Catch ex As Exception
			Throw New Exception("InitRetDevice()::" & ex.Message)
		End Try

	End Function
	Private Function CloseRetDevice() As Boolean
		Try
			If pRTP.M_product_main.dwtilt_enable = True And pRTP.M_product_main.dwtilt_type >= 2 Then
				Return pRetCtrl.CloseDevice()
			End If
			Return True
		Catch ex As Exception
			Throw New Exception("CloseRetDevice()::" & ex.Message)
		End Try

	End Function
    Private Function InitPimDevice(bandName As String) As List(Of DataModels.Instrument)
        Try
            Dim resp As New List(Of DataModels.Instrument)

            Dim instrPara As CATSPimConfig.LocalConfig.InstrPara
            Dim dev As New DataModels.Instrument
            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager

            instrPara = pAppCfg.GetInstrumentConfig(bandName)

            If instrPara.DevSN = "" Or instrPara.DevSN Is Nothing Then Throw New Exception("Don't read avaiable PIM Device SN, please check!")

            pTestCable = New TestCable(instrPara.CableSerNum)

            If pTestCable.IsAvaliable = False Then
                MsgBox("The usage count of cable #" & instrPara.CableSerNum & " has more than " & pTestCable.TolerantNum & " times, " & vbCrLf &
                    "In order to improve the test stability , please change the cable!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
            End If

            iv = ivBll.SelectByVendorName(instrPara.Vendor.Trim)
            If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & instrPara.Vendor.Trim & ">")
            pRTP.instr_vendor_id = iv.id

#If Not DEBUG Then


            If instrPara.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = instrPara.Address
                devRos.Open()

                dev.Idn = devRos.ReadIDN
                dev.SerialNumber = instrPara.DevSN    '   devRos.Serial_Number校准时就获取
                dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
                dev.Hardware = " "
                dev.Firmware = devRos.FW_Revision

                devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRos.FreqBand = instrPara.BandIdx
                devRos.RFPowerOnOff_TwoPorts(False)

                'devRos.Close()

                resp.Add(dev)

                pPimDev = devRos

            ElseIf instrPara.Vendor.Trim.ToUpper = "Summitek".ToString.ToUpper Or instrPara.Vendor.Trim.ToUpper = "SI-A".ToString.ToUpper Then
                Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devSum.Address = instrPara.Address
                devSum.Open()

                dev.Idn = " "
                dev.SerialNumber = instrPara.DevSN    '   devSum.Serial_Number
                dev.Model = devSum.Model
                dev.Hardware = " "
                dev.Firmware = " " 'devSum.FW_Revision

                devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devSum.FreqBand = instrPara.BandIdx
                devSum.RFPowerOnOff_TwoPorts(False)

                'devSum.Close()

                resp.Add(dev)

                pPimDev = devSum

                '=======================================================
            ElseIf instrPara.Vendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then
                Dim devZUl As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                devZUl.Address = instrPara.Address
                devZUl.Open()
                devZUl.FreqBandSet = instrPara.BandName

                dev.Idn = "Commscope Zulu " & devZUl.FilterBandName & "_" & devZUl.Serial_Number
                dev.Model = devZUl.Model
                dev.SerialNumber = instrPara.DevSN    '   devZUl.Serial_Number
                dev.Hardware = "1.0"
                dev.Firmware = devZUl.FW_Revision

                devZUl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devZUl.RFPowerOnOff_TwoPorts(False)

                'devZUl.Close()

                resp.Add(dev)

                pPimDev = devZUl

                '=======================================================================

            ElseIf instrPara.Vendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                devKae.Address = instrPara.Address
                devKae.Open()

                dev.Idn = " "
                dev.Model = devKae.Model
                dev.SerialNumber = instrPara.DevSN    '   devKae.Serial_Number
                dev.Hardware = " "
                dev.Firmware = " "

                devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devKae.FreqBand = instrPara.BandIdx
                devKae.RFPowerOnOff_TwoPorts(False)

                'devKae.Close()

                resp.Add(dev)

                pPimDev = devKae

            ElseIf instrPara.Vendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                devRfl.Address = instrPara.Address
                devRfl.Open()

                dev.Idn = devRfl.ReadIDN
                dev.SerialNumber = instrPara.DevSN    '   devRfl.Serial_Number
                dev.Model = devRfl.Model
                dev.Hardware = " "
                dev.Firmware = devRfl.FW_Revision

                devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRfl.FreqBand = instrPara.BandIdx
                devRfl.RFPowerOnOff_TwoPorts(False)

                'devRfl.Close()

                resp.Add(dev)

                pPimDev = devRfl

            ElseIf instrPara.Vendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                'devJoc.Address = instrPara.Address

                devJoc.Address = instrPara.Address.Split(CChar(":"))(0).Trim  'add by tony 20190529
                devJoc.Port_Select = instrPara.Address.Split(CChar(":"))(1).Trim 'add by tony 20190529
                If devJoc.Port_Select <> 1 and devJoc.Port_Select <> 2 Then Throw New Exception("InitPimDevice():: JoinCom Port selection") 'add by tony 20190529

                devJoc.Open()

                dev.Idn = devJoc.ReadIDN
                    dev.SerialNumber = instrPara.DevSN    '   devJoc.Serial_Number
                    dev.Model = devJoc.Model
                    dev.Hardware = " "
                    dev.Firmware = devJoc.FW_Revision

                    devJoc.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                    devJoc.FreqBand = instrPara.BandIdx
                    devJoc.RFPowerOnOff_TwoPorts(False)

                    'devRfl.Close()

                    resp.Add(dev)

                    pPimDev = devJoc

                End If
#End If

            m_PowerLoss.TxL = instrPara.Tx1Loss
            m_PowerLoss.TxR = instrPara.Tx2Loss
            m_PowerLoss.Rx = instrPara.RxLoss

            Return resp

        Catch ex As Exception
            Throw New Exception("InitPimDevice()::" & ex.Message)
        End Try
    End Function

    '' add by tony ======================================================================================================
    Public Function Check_PIMChamberDoorClosed() ' add by tony for monitor door  

        If pVibCtrl.ReadInput(1) Then
            ' Door is closed
        Else
            Do Until pVibCtrl.ReadInput(1)
                Dim result As DialogResult = MessageBox.Show("Please CLOSE PIM Chamber Door.", "Verify Door is Closed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)
                If result = DialogResult.Retry Then
                    pVibCtrl.ReadInput(1)
                Else
                    'Cancel
                    Return False
                End If
            Loop
        End If

        Return True
    End Function

    Public Function Check_PIMChamberID() As String ' add by tony for chamber ID  
        Try
            If pVibCtrl.ReadInputForChamberID = 0 Then
                Throw New Exception("Check_PIMChamberID():: connect the input 24V error")
            Else
                Return "Chamber" & CStr(pVibCtrl.ReadInputForChamberID)
            End If

        Catch ex As Exception
            Throw New Exception("Check_PIMChamberID()::" & ex.Message)
        End Try


    End Function

    Public Function Check_PIMChamberVibration() ' add by tony for monitor door  

        Threading.Thread.Sleep(1000)

        If pVibCtrl.ReadInput(11) Then
            ' Vibration is working
        Else
            Do Until pVibCtrl.ReadInput(11)
                Dim result As DialogResult = MessageBox.Show("Chamber Vibration happen error, please check and retry.", "Check Chamber Vibration", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)
                If result = DialogResult.Retry Then
                    pVibCtrl.ReadInput(11)
                Else
                    'Cancel
                    Return False
                End If
            Loop
        End If

        Return True
    End Function


    '=====================================================================================================
    Private Sub InitVibDevice(JsbBoard As String)
		Try

			Dim dev As New VibCtrl(System.Enum.Parse(GetType(VibCtrl.VibJsbBoard), JsbBoard))

			pVibCtrl = dev

        Catch ex As Exception
			Throw New Exception("InitVibDevice()::" & ex.Message)
		End Try

	End Sub
	Private Function OpenVibDevice() As Boolean
		Try
			Dim vibCfg As CATSPimConfig.LocalConfig.VibrationPara = pAppCfg.GetVibrationConfig

			Dim jsbBoard As String = vibCfg.Address.ToString.ToUpper.Trim
            If jsbBoard <> "JSB31" And jsbBoard <> "JSB336" And jsbBoard <> "SEALEVEL8222" Then
                vibCfg.Address = "JSB31"
                pAppCfg.SaveVibrationConfig(vibCfg)
            End If

            If vibCfg.Enable = True Or pAppCfg.GetDoorCheck.Enable = True Or pAppCfg.GetChamberIDCheck.Enable = True Or pAppCfg.GetChamberVibrationCheck.Enable = True Then
                InitVibDevice(vibCfg.Address)
                Return pVibCtrl.OpenDev(vibCfg.COMPORT)
            Else
                Return True
			End If

		Catch ex As Exception
			Throw New Exception("OpenVibDevice()::" & ex.Message)
		End Try
	End Function
    Public Sub CloseVibDevice()
        Try
            If pAppCfg.GetVibrationConfig.Enable = True Or pAppCfg.GetDoorCheck.Enable = True Or pAppCfg.GetChamberIDCheck.Enable = True Or pAppCfg.GetChamberVibrationCheck.Enable = True Then
                pVibCtrl.CloseDev()
            End If

        Catch ex As Exception
            Throw New Exception("CloseVibDevice()::" & ex.Message)
        End Try
    End Sub
    Private Function OpenPimDevice() As Boolean
		Try
			'Dim instrPara As CATSPimConfig.LocalConfig.InstrPara

			'instrPara = pAppCfg.GetInstrumentConfig(bandName)

			Return pPimDev.Open()

			'pInstrument.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
			'pInstrument.ImdOrder = 3
			'pInstrument.FreqBand = instrCfg.band_idx
			'pInstrument.SetRFPower(stepPara.carrier_power + instrCfg.tx_loss, stepPara.carrier_power + instrCfg.tx_loss)
			'pInstrument.RFPowerOnOff_TwoPorts(False)

		Catch ex As Exception
			Throw New Exception("OpenPimDevice()::" & ex.Message)
		End Try

	End Function
	Private Function ClosePimDevice() As Boolean
		Try

			pPimDev.Close()

			Return True

		Catch ex As Exception
			Throw New Exception("ClosePimDevice()::" & ex.Message)
		End Try

	End Function
	Private Function CheckInstrumentOnLine() As Boolean

		Try

			Return True
		Catch ex As Exception

			Return False
		End Try

	End Function
	Private Sub CheckRets()
		Try

            'check count
            pGui.AddStatusMsg("Check Ret number ... , ", False)
            If m_RetList.Count <> pRTP.M_product_main.dwtilt_num Then
                pGui.AddStatusMsg("Fail")
                Throw New Exception("Rets number do Not match to Db")
            Else
                pGui.AddStatusMsg("OK", True)
            End If

            'check model
            pGui.AddStatusMsg("Check Ret model ... ,", False)
			For Each r In m_RetList
				pGui.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, r.Value.AntModel), False)
                'If r.Value.AntModel.Trim.ToUpper = pRTP.M_product_main.dwtilt_pn.Trim.ToUpper Then
                If r.Value.AntModel.Trim.ToUpper.Contains(pRTP.M_product_main.dwtilt_pn.Trim.ToUpper) Or
                        r.Value.AntModel.Trim.ToUpper.Contains("AAFID") Or r.Value.AntModel.Trim.ToUpper.Contains("NNH8H8") Or ' NNH8H8
                    r.Value.AntennaSn.Trim.Replace("-", "").Contains(pRTP.M_product_main.dwtilt_pn.Trim.ToUpper) Then
                    pGui.AddStatusMsg("OK", True)
                Else
                    pGui.AddStatusMsg("Fail", True)
					Throw New Exception("Rets model do not match to product")
				End If
			Next

            ' check sn
            If PN_Cal.Contains("NNH8H8") Then ' NNH8H8
            Else
                pGui.AddStatusMsg("Check Ret sn ... ,", False)
                For Each r In m_RetList
                    pGui.AddStatusMsg(String.Format("Ret[{0}], SN [{1}] , ", r.Key, r.Value.AntennaSn), False)
                    If r.Value.AntennaSn.Trim.ToUpper.Contains(pRTP.barcode.Trim.ToUpper) Or r.Value.RetSn.ToUpper.Contains("NKFI") Or r.Value.RetSn.ToUpper.Contains("NKFR") Then
                        pGui.AddStatusMsg("OK", True)
                    Else
                        pGui.AddStatusMsg("Fail", True)
                        Throw New Exception("Rets sn do Not match to product")
                    End If
                Next
            End If

            'check multiy/single
            pGui.AddStatusMsg("Check Ret single/multi ... ,", False)
			For Each r In m_RetList
				pGui.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, CType(r.Value.Type, AisgDevice.DeviceType).ToString), False)
				If IIf(r.Value.Type = AisgDevice.DeviceType.SingleRet, "S", "M") = pRTP.M_product_main.dwtilt_mode.Trim.ToUpper Then
					pGui.AddStatusMsg("OK", True)
				Else
					pGui.AddStatusMsg("Fail", True)
					Throw New Exception("Rets type miss match to Db")
				End If
			Next

		Catch ex As Exception
			Throw New Exception("CheckRets()::" & ex.Message)
		End Try

	End Sub
	Private Function getCriteriaList(ps_type As Integer, proto As Dictionary(Of String, CATS.Model.cq_criteria_detail)) As Dictionary(Of String, CATS.Model.cq_criteria_detail)
		Try
			If ps_type = 0 Then Return proto

			Dim cmBll As New CATS.BLL.criteria_mainManager
			Dim cmML As List(Of CATS.Model.criteria_main)
			Dim cmM As CATS.Model.criteria_main

			cmML = cmBll.SelectAll()
			If cmML Is Nothing Then Throw New Exception("Not find any test criteria")

            'cmM = cmML.Find(Function(o) o.descr.ToUpper.Contains("STS-A"))
            'If cmM Is Nothing Then Throw New Exception("Not find STS-A criteria")
            cmM = cmML.Find(Function(o) o.descr.ToUpper.Contains("LAMBDA[0,1]"))
            If cmM Is Nothing Then Throw New Exception("Not find LAMBDA[0,1] criteria")

            Dim cqcdm As New CATS.BLL.cq_criteria_detailManager
			Dim cqcdmML As List(Of CATS.Model.cq_criteria_detail)

			cqcdmML = cqcdm.SelectById(cmM.id)
            If cqcdmML Is Nothing Then Throw New Exception("Not find any criteria item of LAMBDA[0,1]")
            Dim resp As New Dictionary(Of String, CATS.Model.cq_criteria_detail)

			For Each cqcdmM In cqcdmML
                resp.Add(cqcdmM.criteria_item.ToUpper.Trim, cqcdmM)
            Next

			Return resp

		Catch ex As Exception
			Throw New Exception("getCriteriaList()::" & ex.Message)
		End Try
	End Function
	Public Function RunTestPhase(phase As TestSpec.TestPhase, ByRef cancel As Boolean) As Boolean
		Dim phReport As New TestReport.XmlFramework.TPhase

		Try

			Dim status As String = "P"
			Dim gpRp As New TestReport.XmlFramework.TGroup

			pRTP.phase = phase.Name
			pRTP.MeasWatch.Restart()
			pRTP.ConnWatch.Reset()
			pRTP.meas_start_time = pGui.StartTestTime
			pRTP.spec_main_id = phase.SpecMainId
			pRTP.phase_main_id = phase.PhaseMainId

			m_CriteriaItems = getCriteriaList(pRTP.M_phase_station_main.meas_type, phase.TestCriteriaList)
            'm_CriteriaItems = phase.TestCriteriaList

            If OpenVibDevice() = False Then Throw New Exception("Open Vib device")


            m_EqList = InitPimDevice(phase.TestGroupList(0).TestItemList(0).cfg_imd_main.freq_band) '初始化PIM 设备


            '=========================Get Chamber ID ,添加到仪器设备中并直接上传
            'check chamberID 0228 2019
            ' pGui.ChamberIDdisplay("Chamber18")
            If pAppCfg.GetChamberIDCheck.Enable Then
                Dim dev_chamber As New DataModels.Instrument
                ChamberID = "Unknow"
                ChamberID = Check_PIMChamberID()

                pGui.ChamberIDdisplay(ChamberID)  ' ' add by tony for monitor door  

                dev_chamber.Model = "ChamberPIM"
                dev_chamber.SerialNumber = ChamberID
                dev_chamber.Hardware = ""
                dev_chamber.Firmware = ""
                dev_chamber.Idn = "Chamber PIM Test"
                m_EqList.Add(dev_chamber)
            Else
                pGui.ChamberIDdisplay_Disable()
                ChamberID = "  "
            End If

            '====草稿
            'Dim dev_chamber0 As New DataModels.Instrument
            'dev_chamber0.Model = "ChamberPIM"
            'dev_chamber0.SerialNumber = “Chamber11”
            'dev_chamber0.Hardware = ""
            'dev_chamber0.Firmware = ""
            'dev_chamber0.Idn = "Chamber PIM Test"
            'm_EqList.Add(dev_chamber0)
            '==============================================

            '-------NOKIA NNH8H8 电调连接提示 开始 20190708
            Dim TiltCheckID_NNH8H8 As String = ""
            If PN_Cal.Contains("NNH8H8") Then
                Dim TT As Integer = CInt(phase.Name.Replace("PIM", "").Replace("L", "").Replace("U", "").Replace("LU", "").Replace("W", "").Replace("N", ""))
                If TT <= 1500 Then
                    TiltCheckID_NNH8H8 = "CP000"
                    If MsgBox("Please connect the Low Band RET ! ", MsgBoxStyle.Question + vbYesNo, "Connect tilt") = MsgBoxResult.No Then Throw New Exception("Testing was cancelled")
                Else
                    TiltCheckID_NNH8H8 = "NKFI"
                    If MsgBox("Please connect the High Band RET ! ", MsgBoxStyle.Question + vbYesNo, "Connect tilt") = MsgBoxResult.No Then Throw New Exception("Testing was cancelled")
                End If
            End If
            '-------NOKIA NNH8H8 电调连接提示 结束

            m_RetList = New Dictionary(Of String, DataModels.RetDevice)

            If OpenRetDevice() = 0 Then
                m_RetList = ScanAntennaRet(pRTP.barcode, pRTP.M_product_main.dwtilt_type, pRTP.M_product_main.id)
                If m_RetList Is Nothing Then Throw New Exception("Can't find Rets")
                CheckRets() '20190708 改
            End If

            '-------NOKIA NNH8H8 电调连接是否正确 开始20190708
            If PN_Cal.Contains("NNH8H8") Then
                For Each r In m_RetList
                    pGui.AddStatusMsg(String.Format("Ret[{0}], SN [{1}] , ", r.Key, r.Value.RetSn), False)
                    If r.Value.RetSn.Trim.ToUpper.Contains(TiltCheckID_NNH8H8.Trim.ToUpper) Then
                        pGui.AddStatusMsg("OK", True)
                    Else
                        pGui.AddStatusMsg("Fail", True)
                        Throw New Exception("Rets sn do Not match the test frequecncy band")
                    End If
                Next
            End If
            '-------NOKIA NNH8H8 电调连接是否正确 结束

            pretest_setItem.Clear() ' 20180816
            For Each group In phase.TestGroupList
				Try
					If pRTP.M_phase_station_main.meas_type = 1 Then  'pre test
                        If phase.Name.Contains("LU") Then
                            RunTestGroup(group, 2, gpRp)
                        Else
                            '=============================================Pretest update by xtx
                            If pretest_setItem.Contains(group.Name.Split("_")(0)) Then Continue For ' 20180816
                            pretest_setItem.Add(group.Name.Split("_")(0)) '' 20180816
                            '============================================
                            RunTestGroup(group, 1, gpRp)
						End If
					Else
						RunTestGroup(group, 0, gpRp)
					End If

					If gpRp Is Nothing Then Throw New Exception("TestError") 'status = "E" : Exit For

				Catch exf As FailedException
					Throw New FailedException
				Catch exa As AbortedException
					Throw New AbortedException
				Catch ex As Exception
					Throw New Exception(ex.Message)
				Finally
					phReport.TestGroups.Add(gpRp)
				End Try


				If status = "P" And gpRp.GroupStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
        If status = "F" Then Exit For
#End If

			Next

            phReport.MeasStatus = status


#If Not DEBUG Then
            '===========================================================add  Cal funciton
            If status = “P” And PN_Cal = "-110DBM_PIM_STD" Then
                If CalibrationRecord.INI_Write_Cal(phase.Name.ToUpper.Trim, Environment.MachineName, Environment.UserName, "0", "2") = False Then
                    MsgBox("Error! Please calibrate this band with -110DBM_PIM_STD again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End If
                CalibrationRecord.Show()

            End If

            If status = “P” And PN_Cal = "LOW-PIM-LOAD" Then
                If CalibrationRecord.INI_Write_Cal(phase.Name.ToUpper.Trim, Environment.MachineName, Environment.UserName, LowPim_Load_Spec, "1") = False Then
                    MsgBox("Error! Please calibrate this band with LOW-PIM-LOAD again !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End If
                CalibrationRecord.Show()
            End If
            '======================================================================
#End If


            Return True

		Catch exf As FailedException
			phReport.MeasStatus = "F"
			pGui.RecordResult("TestPhaseFailed", 1, 0, 0)
			Return False

		Catch exa As AbortedException

			phReport.MeasStatus = "A"
			pGui.RecordResult("TestPhaseAborted", 1, 0, 0)
			Return False

		Catch ex As Exception


			phReport.MeasStatus = "E"
			pGui.RecordResult("TestPhaseError", 1, 0, 0)
			MsgBox("RunTestPhase()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			Return False

		Finally

            Try
                pRTP.MeasWatch.Stop()
                pRTP.meas_time = pRTP.MeasWatch.Elapsed.TotalSeconds
                pRTP.conn_time = pRTP.ConnWatch.Elapsed.TotalSeconds
                pRTP.meas_stop_time = Now
                pRTP.total_time = DateDiff(DateInterval.Second, pRTP.meas_start_time, pRTP.meas_stop_time)
                WriteReport(phReport)
                MiiFactory.UpdateMiiRouting() 'for Mii

                If m_RetList IsNot Nothing Then
                    If m_RetList.Count > 0 Then
                        If m_RetList(m_RetList.Keys.First).Tilt.RetPartNumber.Trim.ToUpper.Contains("COMMRET2") = False Then
                            If MsgBox("Do you want to move RET tilts to default or min angles ? ", MsgBoxStyle.Question + vbYesNo, "Move to shipment tilt") = MsgBoxResult.Yes Then
                                SetDownTilts(pRTP.M_product_main.gen1, pRTP.M_product_main.gen2)
                            End If
                        End If
                    End If
                End If

                pTestCable.StoreConsumedNum(phase.PhaseMainId, pRTP.barcode)

            Catch exf As FailedException
                Throw New Exception("RunTestPhase()::" & exf.Message)
            Catch ex As Exception
                Throw New Exception("RunTestPhase()::" & ex.Message)
            Finally

                Try
                        CloseVibDevice()
                    Catch ex As Exception

                    End Try
                    Try
                        ClosePimDevice()
                    Catch ex As Exception

                    End Try
                    Try
                        CloseRetDevice()
                    Catch ex As Exception

                    End Try
                End Try
            End Try
	End Function
	Public Sub RunTestGroup(group As TestSpec.TestGroup, reqTestitemNumber As Short, ByRef outTGroup As TestReport.XmlFramework.TGroup)
		Dim resp As New TestReport.XmlFramework.TGroup
		Dim rspItem As New List(Of TestReport.XmlFramework.TItem)

		Try

			pGui.StartTestGroup(group.Name)

			Dim status As String = "P"
			Dim tiCount As Short = 0

			resp.GroupMainId = group.Id
			resp.GroupName = group.Name

            For Each item In group.TestItemList '这里切换700L/U

                Try
                    RunTestItem(item, rspItem)
                    If rspItem Is Nothing Then Throw New Exception("TestError")
                Catch exf As FailedException
                    Throw New FailedException(exf.Message)
                Catch exa As AbortedException
                    Throw New AbortedException
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    If rspItem IsNot Nothing Then
                        For Each ti In rspItem
                            resp.TestItems.Add(ti)
                        Next
                    End If
                End Try

                If status = "P" And rspItem(rspItem.Count - 1).MeasStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
        If status = "F" Then Exit For
#End If
                tiCount += 1
                If reqTestitemNumber = tiCount Then Exit For
                'If pRTP.M_phase_station_main.meas_type = 1 Then Exit For 'for pre test

            Next

            resp.GroupStatus = status

		Catch exf As FailedException
			resp.GroupStatus = "F"
			pGui.RecordResult("TestGroupFailed", -1, 0, 0)
			Throw New FailedException(exf.Message)
		Catch exa As AbortedException

			resp.GroupStatus = "A"
			pGui.RecordResult("TestGroupAborted", -1, 0, 0)
			Throw New AbortedException

		Catch ex As Exception

			'  cancel = True
			resp.GroupStatus = "E"
			pGui.RecordResult("TestGroupError", -1, 0, 0)
			Throw New Exception("RunTestGroup()::" & ex.Message)
			'Return resp

		Finally

			'RunTestGroup = resp
			outTGroup = resp
			pGui.StopTestGroup()

		End Try
	End Sub
	Public Function VirtualTestItem(item As TestSpec.TestItem) As List(Of TestReport.XmlFramework.TItem)
		Try
			Dim resp As New List(Of TestReport.XmlFramework.TItem)
			Dim respItem As New TestReport.XmlFramework.TItem

			respItem.SpecDetailId = item.spec_detail.id
			respItem.MeasString = "" ' spec.spec_detail.meas_item
			respItem.OrderIdx = item.spec_detail.order_idx
			respItem.TiltIdxs = item.spec_detail.dwtilt_idxs
			respItem.TiltAngs = item.spec_detail.dwtilt_angs

			resp.Add(respItem)

			Return resp

		Catch ex As Exception
			Throw New Exception("VirtualTestItem()::" & ex.Message)
		End Try
	End Function
	Private Function getTestitemCfgImdMain(ps_type As Integer, cim As CATS.Model.cfg_imd_main) As CATS.Model.cfg_imd_main
		Try
			If ps_type = 0 Then Return cim 'not equal pre_test

            Dim descr As String = cim.descr.ToUpper.Trim
            Temp_PIM_descr = descr
            descr = descr.Replace("STS-M", "STS-K")
            descr = descr.Replace("STS-J", "STS-K")
            descr = descr.Replace("STS-A", "STS-K")
            descr = descr.Replace("STS-L", "STS-K")
            descr = descr.Replace("STS-T", "STS-K")

            Dim cimBll As New CATS.BLL.cfg_imd_mainManager
			Dim cimML As List(Of CATS.Model.cfg_imd_main)
			Dim cimM As CATS.Model.cfg_imd_main

			cimML = cimBll.SelectByCfgImdFreqbandId(cim.cfg_imd_freq_band_id)
			cimM = cimML.Find(Function(o) o.descr = descr)
			If cimM Is Nothing Then Throw New Exception("not find freq band of " & descr & " in DB")

			Return cimM

		Catch ex As Exception
			Throw New Exception("getTestitemCfgImdMain()::" & ex.Message)
		End Try
	End Function
	Public Sub RunTestItem(item As TestSpec.TestItem, ByRef outTItem As List(Of TestReport.XmlFramework.TItem))
		Dim resp As New List(Of TestReport.XmlFramework.TItem)
		'Dim tiFinal As New TestReport.XmlFramework.TItem
		Try

			'resp.SpecDetailId = item.spec_detail.id
			'resp.OrderIdx = item.spec_detail.order_idx

			If item.spec_detail.message IsNot Nothing Then
				If item.spec_detail.message.Length >= 5 Then
					pRTP.MeasWatch.Stop()
					pRTP.ConnWatch.Start()
					If MsgBox(item.spec_detail.message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
						pRTP.ConnWatch.Stop()
						Throw New AbortedException("TestAborted")
					End If
					pRTP.ConnWatch.Stop()
					pRTP.MeasWatch.Start()
				End If
			End If

            '确认chamber door is closed or not
            '#If Not DEBUG Then
            If pAppCfg.GetDoorCheck.Enable Then If Check_PIMChamberDoorClosed() = False Then Throw New Exception("  Chamber door is open now ,Please check!") ' ' add by tony for monitor door  
            '#End If

            If pRTP.M_product_main.dwtilt_enable And pRTP.M_phase_station_main.ret_validity = True Then
				If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET
					If pRetCtrl.CheckOnLine = False Then
						m_RetList = ScanAntennaRet(pRTP.barcode, pRTP.M_product_main.dwtilt_type, pRTP.M_product_main.id)
					End If
				End If
			End If


			SetDownTilts(item.spec_detail.dwtilt_idxs, item.spec_detail.dwtilt_angs)

			Dim frm As FormTest

			frm = New FormTest
			frm.TestMode = DataModels.TestMode.Test
			frm.RetDeviceList = m_RetList

			frm.InstrPowerLoss = m_PowerLoss
			frm.TestCriteriaSpec = m_CriteriaItems

			item.cfg_imd_main = getTestitemCfgImdMain(pRTP.M_phase_station_main.meas_type, item.cfg_imd_main)
			frm.TestItem = item

			frm.PSFReq = TestModules.GetSweepFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)
			frm.PFFReq = TestModules.GetFixedFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)
			frm.PCFReq = TestModules.GetCustomFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)

            'If pRTP.M_phase_station_main.meas_type = 1 Then frm.PFFReq = Nothing
            'If pRTP.M_phase_station_main.meas_type = 1 Then frm.PCFReq.test_num = 1
            ' If pRTP.M_phase_station_main.meas_type = 1 Then if frm.PCFReq IsNot Nothing Then frm.PCFReq.test_num = 1

            frm.ShowDialog()

            resp = frm.ReportItem

			If resp(resp.Count - 1).MeasStatus = "E" Then Throw New Exception("TestError")
			If resp(resp.Count - 1).MeasStatus = "A" Then Throw New AbortedException

		Catch exf As FailedException
			If resp.Count = 0 Then resp = VirtualTestItem(item)
			resp(resp.Count - 1).MeasStatus = "F"
			resp(resp.Count - 1).MeasString = exf.Message
			pGui.RecordResult("TestItemFailed", -1, 0, 0)
			Throw New FailedException(exf.Message)

		Catch exa As AbortedException

			If resp.Count = 0 Then resp = VirtualTestItem(item)
			resp(resp.Count - 1).MeasStatus = "A"
			resp(resp.Count - 1).MeasString = "TestAborted"

			pGui.RecordResult("TestItemAborted", -1, 0, 0)

			Throw New AbortedException()

		Catch ex As Exception

			If resp.Count = 0 Then resp = VirtualTestItem(item)
			resp(resp.Count - 1).MeasString = ex.Message '"TestError"
			resp(resp.Count - 1).MeasStatus = "E"

			pGui.RecordResult("TestItemError", -1, 0, 0)
			Throw New Exception("RunTestItem()::" & ex.Message)

		Finally

			pRTP.ConnWatch.Stop()
			outTItem = resp

		End Try
	End Sub
	Private Sub SetRetDownTilt(tiltIdxs As String, downTilts As String)
		Dim frm As New FormSetRet

		Try
			Dim tpidx() As String
			Dim tpdwtilt() As String
			Dim fSet As Boolean = False
			Dim fltTilt As Decimal

			If tiltIdxs Is Nothing Then tiltIdxs = ""
			If downTilts Is Nothing Then downTilts = ""

			tpidx = tiltIdxs.Split(",")
			tpdwtilt = downTilts.Split(",")
			If tpidx.Count <> tpdwtilt.Count Then
				pGui.AddStatusMsg(String.Format("RET idx {0} miss match downtilt{1}.", tiltIdxs, downTilts), True)
				Throw New Exception(String.Format("RET idx {0} miss match downtilt {1}.", tiltIdxs, downTilts))
			End If

			Dim intCount As Integer = 0
			frm.Show()

			For Each dev In m_RetList

				If tiltIdxs.Trim = "" Then
					frm.ProgressMaxValue = m_RetList.Count
					fltTilt = dev.Value.Tilt.GetMinTilt
					pGui.AddStatusMsg(String.Format("Setting RET idx={0},downtilt={1} ... , ", Right(dev.Key.ToString, 1), fltTilt), False)
					dev.Value.Tilt.SetTilt(fltTilt)
					pGui.AddStatusMsg("Ok")
					intCount += 1
					frm.ProgressValue = intCount
					My.Application.DoEvents()
					fSet = True
				Else
					frm.ProgressMaxValue = tpidx.Count
					For id As Short = 0 To tpidx.Count - 1
						If tpidx(id) = 0 Then pGui.AddStatusMsg("Ret Idx=0, No Ret.") : fSet = True
						If Right(dev.Key.ToString, 1) = tpidx(id) Then
							pGui.AddStatusMsg(String.Format("Setting RET idx={0},downtilt={1} ... , ", tpidx(id), tpdwtilt(id)), False)
							dev.Value.Tilt.SetTilt(tpdwtilt(id))
							pGui.AddStatusMsg("Ok")
							intCount += 1
							frm.ProgressValue = intCount
							My.Application.DoEvents()
							fSet = True
						End If
					Next
				End If
			Next

			If fSet = False Then Throw New Exception("Mismatch RET ID")
			My.Application.DoEvents()

		Catch exf As FailedException
			Throw New FailedException(exf.Message)
		Catch ex As Exception
			Throw New Exception("SetRetDownTilt()::" & ex.Message)
		Finally
			frm.Close()
		End Try
	End Sub
	Private Sub SetVetDownTilt(tiltIdxs As String, downTilts As String)
		Try
			Dim tpidx() As String
			Dim tpdwtilt() As String
			Dim msg As String = ""

			tpidx = tiltIdxs.Split(",")
			tpdwtilt = downTilts.Split(",")
			If tpidx.Count <> tpdwtilt.Count Then
				pGui.AddStatusMsg(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts), True)
				Throw New Exception(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts))
			End If

			msg += "Please manually turn the Downtilt to specified angle :" & vbCrLf

			For id As Short = 0 To tpidx.Count - 1
				msg += String.Format(" Tilt# {0}, Angle ={1}", tpidx(id), tpdwtilt(id)) & vbCrLf
			Next

			MsgBox(msg, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)

			pGui.AddStatusMsg("OK")

		Catch ex As Exception
			Throw New Exception("SetVetDownTilt()::" & ex.Message)
		End Try
	End Sub

    'Private Sub SetDownTilts(def_ret_idxs As String, def_ret_angles As String)
    '	Try
    '		Dim ir As Integer
    '		Dim ret_dev As DataModels.RetDevice
    '		Dim key As String, pIndex As Integer

    '		'para: idx(1)->tilt(0);idx(2)->tilt(0) ...
    '		If pRTP.M_product_main.dwtilt_enable = True Then
    '			If pRTP.M_phase_station_main.ret_validity = False Then Return
    '			If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET

    '				If def_ret_idxs = "" Or def_ret_angles = "" Then
    '					For Each ret In m_RetList
    '						ret.Value.Tilt.SetTilt(ret.Value.Tilt.GetMinTilt)
    '					Next
    '				Else
    '					Dim ret_idx() As String = def_ret_idxs.Split(",")
    '					Dim ret_angle() As String = def_ret_angles.Split(",")

    '					If ret_idx.Count <> ret_angle.Count Then Throw New Exception("Mismatch <" & def_ret_idxs & "> with <" & def_ret_angles & ">")
    '					For ir = 0 To ret_idx.Count - 1
    '						ret_dev = Nothing
    '						For Each ret In m_RetList
    '							key = ret.Key
    '							pIndex = key.IndexOf(".")
    '							If ret_idx(ir) = key.Substring(pIndex + 1) Then
    '								ret_dev = ret.Value
    '								Exit For
    '							End If
    '						Next
    '						If ret_dev Is Nothing Then Throw New Exception("Not find key<" & ret_idx(ir) & "> in RetDevices")
    '						ret_dev.Tilt.SetTilt(ret_angle(ir))
    '					Next
    '				End If
    '			End If
    '		End If

    '	Catch ex As Exception
    '		Throw New Exception("SetDownTilts()::" & ex.Message)
    '	End Try
    'End Sub





    'Public Sub SetDownTilts(item As TestSpec.TestItem)
    '	Try

    '		If pRTP.M_product_main.dwtilt_enable = True Then
    '			If pRTP.M_phase_station_main.ret_validity = False Then Return

    '			If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET
    '				SetRetDownTilt(item.spec_detail.dwtilt_idxs, item.spec_detail.dwtilt_angs)
    '			ElseIf pRTP.M_product_main.dwtilt_type = 1 Then 'VET
    '				SetVetDownTilt(item.spec_detail.dwtilt_idxs, item.spec_detail.dwtilt_angs)
    '			Else
    '				Throw New Exception("RET is enable, but not RET or VET  or ...")
    '			End If
    '		End If


    '	Catch ex As Exception
    '		Throw New Exception("SetDownTilts()::" & ex.Message)
    '	End Try
    'End Sub
    Public Sub SetDownTilts(ret_idxs As String, ret_angles As String)
        Try

            If pRTP.M_product_main.dwtilt_enable = True Then
                If pRTP.M_phase_station_main.ret_validity = False Then Return

                If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET
                    SetRetDownTilt(ret_idxs, ret_angles)
                ElseIf pRTP.M_product_main.dwtilt_type = 1 Then 'VET
                    SetVetDownTilt(ret_idxs, ret_angles)
                Else
                    Throw New Exception("RET is enable, but not RET or VET  or ...")
                End If
            End If


        Catch exf As FailedException
            Throw New FailedException(exf.Message)

        Catch ex As Exception
            Throw New Exception("SetDownTilts()::" & ex.Message)
        End Try
    End Sub


    '====================================================================================DTF 
    Public Function GetDTFTime(Accurate As String, speed As String) As String

        Dim DTFTime As String

        Try
            ' Dim Stopwatch As New System.Diagnostics.Stopwatch() ''''
            ' Stopwatch.Start() ''''

            FormDTFDisplay.Show()
            Application.DoEvents()
            DTFTime = pPimDev.GetDTP_Time(Accurate, speed) 'pPimDev
            FormDTFDisplay.Close()
            Application.DoEvents()

            '  Stopwatch.Stop() ''''
            '  Dim seconds As Integer = Stopwatch.Elapsed.TotalSeconds ''''
            ' MsgBox(seconds) ''''

        Catch ex As Exception
            FormDTFDisplay.Close()
            Throw New Exception("TestModules::GetDTFTime()::" & ex.Message)
        End Try

        Return DTFTime ' NA 或者 Delay=1.465465ns

    End Function


    Public Sub FailDoGetDTFTime(TestRec As Integer, meas_status As String)

        DTFTestTime = "" '默认值 DTF
        If TestRec = 2 And meas_status = "F" Then ' only for Pass
            DTFTestTime = GetDTFTime("65536", "2") 'pPimDev' DTF test， 时间或者NA
            If DTFTestTime.Contains("delay=") Then
                DTFTestTime = DTFTestTime.Replace("delay=", "").Replace("r=", "").Replace("rssi=", "").Replace(" ", "")
            Else
                DTFTestTime = ""
            End If
        End If

    End Sub


End Module
