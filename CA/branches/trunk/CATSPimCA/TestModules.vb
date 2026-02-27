Imports System.Data
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms.DataVisualization.Charting
Imports AndrewIntegratedProducts.InstrumentsFramework
Imports CATS
Imports CATS.BLL
Imports CATS.Model
Imports CATSPIM.TestSpec
Imports Keyence.AutoID.SDK
Imports MIIBridge
Imports RFPATestInterface
Imports OpcUaUtility
Imports Opc.Ua.Client
Imports Opc.Ua

Module TestModules
    Private m_RetList As Dictionary(Of String, DataModels.RetDevice)
    Private m_EqList As List(Of DataModels.Instrument)
    Private m_PowerLoss As New DataModels.PowerLoss
    Private m_CriteriaItems As New Dictionary(Of String, CATS.Model.cq_criteria_detail)
    Private logfile As String = "log.txt"
    Private m_TwoTone_CfgM As CATS.Model.cfg_imd_main
    Private Delegate Sub delegateUserControl(str As String)
    Private m_reader As ReaderAccessor = New ReaderAccessor()
    Private frm As FormTest

    Public IPSU2000 As IPowerMeter

    Public Property TwoTone_CfgM As Model.cfg_imd_main
        Get
            Return m_TwoTone_CfgM
        End Get
        Set(value As Model.cfg_imd_main)
            m_TwoTone_CfgM = value
        End Set
    End Property

#Const PC_FAIL_STOP = 0

    Public Sub MyDelay(ByVal millSec As Short)

        If pAbortFlag = True Then Throw New AbortedException

        Threading.Thread.Sleep(millSec)

        My.Application.DoEvents()

    End Sub


    Public Function GetVirtualImValue() As Single
        Try
            Randomize()
            Dim x As Single = 0

            If gGetPassVirtualVale Then
                x = CInt(1000 * Rnd() + 17500) / 100
            Else
                x = CInt(2800 * Rnd() + 13500) / 100
            End If

            Threading.Thread.Sleep(111)

            Return Math.Round((-1) * x, 2)

        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function FindRetId(RetMaps As List(Of CATS.Model.product_ret_map), key As String)
        Try
            If RetMaps Is Nothing Then Throw New Exception("No Ret Map in DB.")
            If RetMaps.Count = 0 Then Throw New Exception("No Ret Map  in DB.")

            For Each ret As CATS.Model.product_ret_map In RetMaps
                If ret.band_id.ToUpper.Trim = key.ToUpper.Trim Then
                    Return ret.ret_idx
                End If
            Next

            Throw New Exception("Not found Ret Map of <" & key & ">")

        Catch ex As Exception
            Throw New Exception("ScanAntennaRet()::" & ex.Message)
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
                    dev = pRetCtrl.GetSingleDeviceInfo(addrI)
                    tmpRet = New DataModels.RetDevice
                    tmpRet.RetSn = tmpDev.SerialNumber
                    tmpRet.AntennaSn = dev.AntennaSerialNumber 'tmpDev.SerialNumber
                    tmpRet.AntModel = dev.AntennaModelNumber
                    'tmpRet.RetModel = 
                    tmpRet.FwVersion = tmpDev.Firmware
                    tmpRet.HwVersion = tmpDev.Hardware
                    tmpRet.Type = tmpDev.DevType

                    tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, 1)

                    GUI.AddStatusMsg(String.Format("RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
                  addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)

                    If RetPN = 3 Or tmpRet.Tilt.RetPartNumber.ToString.ToUpper.Contains("ACCURET") Then 'AccuRET
                        'If RetMaps Is Nothing Then Throw New Exception("The AccuRET  no map parameters in DB.")
                        'If RetMaps.Count = 0 Then Throw New Exception("The AccuRET  no map parameters  in DB.")

                        band_id = tmpRet.AntModel.ToString.Substring(tmpRet.AntModel.ToString.LastIndexOf("-") + 1)
                        'ret_idx = RetMaps.Find(Function(o) o.band_id.ToUpper.Trim = band_id.ToUpper.Trim).ret_idx
                        ret_idx = FindRetId(RetMaps, band_id)

                        resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)

                    ElseIf RetPN = 4 Then
                        'If RetMaps Is Nothing Then Throw New Exception("The cRET  no map parameters in DB.")
                        'If RetMaps.Count = 0 Then Throw New Exception("The cRET  no map parameters in DB.")


                        rpId = tmpRet.RetSn.ToUpper.Trim.IndexOf(AntBarcode) + AntBarcode.Length
                        band_id = tmpRet.RetSn.Substring(rpId)
                        'ret_idx = RetMaps.Find(Function(o) o.band_id.Trim.ToUpper = band_id.Trim.ToUpper).ret_idx
                        ret_idx = FindRetId(RetMaps, band_id)
                        resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)

                    Else
                        resp.Add(tmpRet.RetSn & "." & Right(tmpRet.RetSn, 1), tmpRet)
                    End If

                ElseIf tmpDev.DevType = AisgDevice.DeviceType.MultiRet Then

                    For subAddrI = 1 To tmpDev.RetSubNumber

                        If RetPN = 4 Then
                            Throw New Exception("Can not support cRET mulitiRET type now.")

                            tmpRet = New DataModels.RetDevice

                            dev = pRetCtrl.GetMultiDeviceInfo(addrI, subAddrI)
                            tmpRet.RetSn = tmpDev.SerialNumber 'dev.AntennaSerialNumber
                            tmpRet.AntennaSn = dev.AntennaSerialNumber
                            tmpRet.AntModel = dev.AntennaModelNumber
                            tmpRet.FwVersion = tmpDev.Firmware
                            tmpRet.HwVersion = tmpDev.Hardware
                            tmpRet.Type = tmpDev.DevType

                            'If RetMaps Is Nothing Then Throw New Exception("The cRET  no map parameters in DB.")
                            'If RetMaps.Count = 0 Then Throw New Exception("The cRET  no map parameters in DB.")

                            rpId = tmpRet.RetSn.Trim.ToUpper.IndexOf(AntBarcode) + AntBarcode.Length
                            'band_id = tmpRet.RetSn.Substring(rpId)
                            band_id = tmpRet.RetSn.Substring(rpId)
                            'ret_idx = RetMaps.Find(Function(o) o.band_id = band_id).ret_idx
                            ret_idx = FindRetId(RetMaps, band_id)

                            tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, subAddrI)
                            resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)

                        Else

                            tmpRet = New DataModels.RetDevice

                            dev = pRetCtrl.GetMultiDeviceInfo(addrI, subAddrI)
                            tmpRet.RetSn = tmpDev.SerialNumber 'dev.AntennaSerialNumber
                            tmpRet.AntennaSn = dev.AntennaSerialNumber
                            tmpRet.AntModel = dev.AntennaModelNumber
                            tmpRet.FwVersion = tmpDev.Firmware
                            tmpRet.HwVersion = tmpDev.Hardware
                            tmpRet.Type = tmpDev.DevType

                            tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, subAddrI)
                            resp.Add(tmpRet.RetSn & "." & subAddrI, tmpRet)

                            GUI.AddStatusMsg(String.Format(" RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
                                            addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)
                        End If

                    Next

                End If
            Next

            Return resp

        Catch ex As Exception
            Throw New Exception("ScanAntennaRet()::" & ex.Message)
        End Try

    End Function
    Public Function ParseAlgorithmParas(modelList As List(Of CATS.Model.cq_algo_para_detail)) As DataModels.AlgorithmLimit
        Try
            Dim rnt As New DataModels.AlgorithmLimit

            For Each m As CATS.Model.cq_algo_para_detail In modelList
                Select Case m.algo_item.ToUpper.Trim
                    Case "Lambda_CalcCompensation".ToUpper
                        'rnt.Lambda_CalcCompensation = m.algo_value
                        rnt.Lambda_CalcCompensation = 145
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

            If strFd.Contains("STS-J") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 And sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-K") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 Or sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-L") Then
                If sfBox Is Nothing Then Return False
                If sfBox.dfreq_step = 0 Or sfBox.ufreq_step = 0 Then Return False

            ElseIf strFd.Contains("STS-A") Or strFd.Contains("STS-B") Or strFd.Contains("STS-C") Then
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
    Public Function GetSweepFreqs(freq_desc As String, vendor_id As Integer) As CATS.Model.cfg_imd_sfbox
        Try
            Dim resp As New CATS.Model.cfg_imd_sfbox
            Dim csf As New CATS.BLL.cfg_imd_sfboxManager

            resp = csf.SelectByDescrVendorId(freq_desc, vendor_id)

            If CheckSweepFreqs(resp, freq_desc) = False Then Throw New Exception("test freq configuration is error <" & freq_desc & "> in DB")

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
            ElseIf strFd.Contains("STS-A") Or strFd.Contains("STS-B") Or strFd.Contains("STS-C") Then
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
    Public Function GetFixedFreqs(bandName As String, vendor_id As Integer) As CATS.Model.cfg_imd_ffbox
        Try
            Dim resp As CATS.Model.cfg_imd_ffbox

            Dim cff As New CATS.BLL.cfg_imd_ffboxManager
            resp = cff.SelectByBandNameVendorId(bandName, vendor_id)

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

            If strFd.Contains("STS-A") Or strFd.Contains("STS-B") Or strFd.Contains("STS-C") Then
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

            AddStatusMsg(String.Format("Reading test spec from DB..."))
            resp = TestModules.GetModePimTestSpec(pRTP.M_product_main.id, pRTP.product_mode)

            Return resp

        Catch ex As Exception
            Throw New Exception("GetTestSpec()::" & ex.Message)
        End Try

    End Function
    Public Sub LoadTestPhases(spec As TestSpec)
        Try
            Dim phase(spec.TestPhaseList.Count - 1) As String
            Dim i As Short = 0

            For i = 0 To spec.TestPhaseList.Count - 1
                phase(i) = spec.TestPhaseList(i).Name
            Next

            GUI.DefineTestSteps(phase)

        Catch ex As Exception
            Throw New Exception("LoadTestPhases()::" & ex.Message)
        End Try
    End Sub
    Public Sub LoadTestPhasesStatus(spec As TestSpec, status As List(Of CATS.Model.cq_phases_status))
        Try

            If status Is Nothing Then Return

            For Each sp As TestSpec.TestPhase In spec.TestPhaseList
                For Each st As CATS.Model.cq_phases_status In status
                    If sp.Name.Trim.ToUpper = st.phase.Trim.ToUpper Then
                        If st.phase_status = "P" Then GUI.SetStepStatus(sp.Name.Trim.ToUpper, 2)
                        If st.phase_status = "F" Then GUI.SetStepStatus(sp.Name.Trim.ToUpper, 3)
                        If st.phase_status = "A" Then GUI.SetStepStatus(sp.Name.Trim.ToUpper, 4)
                        If st.phase_status = "E" Then GUI.SetStepStatus(sp.Name.Trim.ToUpper, 5)
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

            For Each phase As TestSpec.TestPhase In spec.TestPhaseList
                ReDim group(phase.TestGroupList.Count - 1)
                For i = 0 To phase.TestGroupList.Count - 1
                    group(i) = phase.TestGroupList(i).Name
                Next
                GUI.DefineTestGroups(phase.Name, group)
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

            GUI.DefinePhaseStationMenu(cqmpsML, cqmpsM)

        Catch ex As Exception
            Throw New Exception("LoadPhaseStation()::" & ex.Message)
        End Try
    End Sub
    Public Sub LoadModes()
        Try
            Dim modeML As List(Of CATS.Model.mode)
            Dim mode As New CATS.BLL.modeManager
            modeML = mode.SelectAll

            GUI.DefineModeMenu(modeML)


        Catch ex As Exception
            Throw New Exception("LoadMode()::" & ex.Message)
        End Try
    End Sub
    Private Function WriteReport(phReport As TestReport.XmlFramework.TPhase, serialNumber As String) As Boolean
        Try
#If INSTR_NORMAL_TEST Then
#Else
            'Return True
#End If
            Dim rltReport As New TestReport.XmlFramework.Report
            Dim strFilePath As String = "rtmp.xml"

            With pRTP
                .meas_status = phReport.MeasStatus
            End With

            With rltReport
                .Type = 0
                .ConnString = pDbConnString
                .Head = TestReport.XmlReport.WriteHead(serialNumber)
                .AssyParts = TestReport.XmlReport.WriteAssyParts(m_RetList)
                .TestInstruments = TestReport.XmlReport.WriteInstrument(m_EqList)
                .TestPhase = phReport
            End With

            If TestReport.XmlReport.WriteReport(rltReport, pTestResultPath & strFilePath) = True Then
                Dim objEncryptor As New Encryptor
                Dim strDataFileName As String

                strDataFileName = serialNumber & "!" &
                  pRTP.meas_start_time.ToString("yyyyMMddHHmmss") & "!" &
                  pRTP.product_mode & "!" &
                  pRTP.M_product_main.product_name & "!" &
                  pRTP.phase & ".dat"

                objEncryptor.EncryptFile(pTestResultPath & strFilePath, pTestResultPath & strDataFileName)
                IO.File.Delete(pTestResultPath & strFilePath)

            End If

            Return True

        Catch ex As Exception
            MsgBox("WriteReport()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End Try
    End Function

    Public Function InitVirtualPimDevice(bandName As String) As List(Of DataModels.Instrument)
        Try
            Dim resp As New List(Of DataModels.Instrument)
            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager

            gSelectedInstrument = gSelectedInstrumentList.Find(Function(o) o.FreqBand = bandName)
            If gSelectedInstrument Is Nothing Then Throw New Exception("Wrong  Config!")
            ' Get instrument vendor model by vendor name
            iv = ivBll.SelectByVendorName(gSelectedInstrument.Vendor.Trim)
            If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & gSelectedInstrument.Vendor.Trim & ">")
            pRTP.instr_vendor_id = iv.id

            Dim dev As New DataModels.Instrument
            dev.Idn = "Rosenberger Virtual PIM Device,Serial No. 010IM-A3815,Firmware v7.53 [2015-05-11]"
            dev.SerialNumber = "010IM-A3815"
            dev.Model = "Rosenberger Virtual PIM Device"
            dev.Hardware = ""
            dev.Firmware = "v7.53"

            resp.Add(dev)

            Return resp

        Catch ex As Exception
            Throw New Exception("InitVirtualPimDevice()::" & ex.Message)
        End Try
    End Function
    Public Function InitU2000Device(address As String, ByRef errMsg As String) As Boolean
        Try
            AddStatusMsg(String.Format("Open Power Sendor @{0} ", address))
            Dim scpiInstr As New SCPIInstrument
            scpiInstr.Address = address
            If scpiInstr.Open = False Then Throw New Exception(" fail to open " & scpiInstr.Address)
            Select Case scpiInstr.Model
                Case "U2000A", "U2000B", "U2000H", "U2001A", "U2001B", "U2001H", "U2002A", "U2002H", "U2004A"
                    pPSCtrl = New U2000
                Case Else
                    Return False
                    'Throw New Exception(String.Format("Type <{0}> not support!", pPSCtrl.Model))
            End Select
            pPSCtrl.Address = address
            If pPSCtrl.Open = False Then Throw New Exception(" fail to open " & pPSCtrl.Address)
            scpiInstr = CType(pPSCtrl, SCPIInstrument)
            scpiInstr.Reset()
            scpiInstr.Preset()
            errMsg = scpiInstr.GetError()

            AddStatusMsg("Open Power Sendor ... OK")

            IPSU2000 = CType(pPSCtrl, IPowerMeter)

            Return True
        Catch ex As Exception
            Throw New Exception("Initialize Power Meter()::" & ex.Message)
        End Try
    End Function
    Public Function ScanInstruments() As List(Of String)
        Try
            Dim scpiInstr As New SCPIInstrument
            Dim devStr As List(Of String) = scpiInstr.FindDevices()
            Return devStr
        Catch ex As Exception
            Throw New Exception("ScanInstruments()::" & ex.Message)
        End Try
    End Function

    Public Function InitPimDevice(instr As CInstrument) As List(Of DataModels.Instrument)
        Try
            Dim resp As New List(Of DataModels.Instrument)
            Dim dev As New DataModels.Instrument
            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager

            gSelectedInstrument = instr
            If gSelectedInstrument Is Nothing Then Throw New Exception("Wrong  Config!")
            iv = ivBll.SelectByVendorName(gSelectedInstrument.Vendor.Trim)
            If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & gSelectedInstrument.Vendor.Trim & ">")
            pRTP.instr_vendor_id = iv.id

            If gSelectedInstrument.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = gSelectedInstrument.Address
                devRos.Open()

                dev.Idn = devRos.ReadIDN
                dev.SerialNumber = devRos.Serial_Number
                dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
                dev.Hardware = ""
                dev.Firmware = devRos.FW_Revision

                devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                If Not pRTP.phase = "PIM700LU" Then
                    devRos.FreqBand = gSelectedInstrument.BandIdx
                End If
                devRos.RFPowerOnOff_TwoPorts(False)

                'devRos.Close()

                resp.Add(dev)

                pPimDev = devRos

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Zulu".ToString.ToUpper Then
                Dim devZulu As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                devZulu.Address = gSelectedInstrument.Address
                devZulu.Open()
                devZulu.FreqBandSet = gSelectedInstrument.FreqBand

                dev.Idn = "Commscope Zulu " & devZulu.FilterBandName & "_" & devZulu.Serial_Number

                If dev.Idn.ToUpper.Contains("Mismatch for the current Master with Filter".ToUpper) Then Throw New Exception("InitPimDevice()::" & "Mismatch for the current Master with Filter")

                dev.Model = devZulu.Model
                dev.SerialNumber = devZulu.Serial_Number
                dev.Hardware = ""
                dev.Firmware = devZulu.FW_Revision

                devZulu.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)

                devZulu.RFPowerOnOff_TwoPorts(False)

                'devZulu.Close()

                resp.Add(dev)

                pPimDev = devZulu

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Summitek".ToString.ToUpper Then
                Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devSum.Address = gSelectedInstrument.Address
                devSum.Open()

                dev.Idn = " "
                dev.SerialNumber = devSum.Serial_Number
                dev.Model = devSum.Model
                dev.Hardware = " "
                dev.Firmware = " " 'devSum.FW_Revision

                devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devSum.FreqBand = gSelectedInstrument.BandIdx
                devSum.RFPowerOnOff_TwoPorts(False)

                'devSum.Close()

                resp.Add(dev)

                pPimDev = devSum

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                devKae.Address = gSelectedInstrument.Address
                devKae.Open()

                dev.Idn = " "
                dev.Model = devKae.Model
                dev.SerialNumber = devKae.Serial_Number
                dev.Hardware = " "
                dev.Firmware = " "

                devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devKae.FreqBand = gSelectedInstrument.BandIdx
                devKae.RFPowerOnOff_TwoPorts(False)

                'devKae.Close()

                resp.Add(dev)

                pPimDev = devKae

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                devRfl.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim  'add by tony 20190529
                devRfl.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim 'add by tony 20190529
                If devRfl.Port_Select < 1 Or devRfl.Port_Select > 2 Then Throw New Exception("InitPimDevice():: Rflight Port selection") 'add by tony 20190529
                devRfl.Open()

                dev.Idn = devRfl.ReadIDN
                dev.SerialNumber = devRfl.Serial_Number
                dev.Model = devRfl.Model
                dev.Hardware = ""
                dev.Firmware = devRfl.FW_Revision

                devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRfl.FreqBand = gSelectedInstrument.BandIdx
                devRfl.RFPowerOnOff_TwoPorts(False)

                'devRfl.Close()

                resp.Add(dev)

                pPimDev = devRfl

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                devJoc.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim
                devJoc.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim
                If devJoc.Port_Select < 1 And devJoc.Port_Select > 2 Then Throw New Exception("InitPimDevice():: JointCom Port selection wrong")
                devJoc.Open()

                dev.Idn = devJoc.ReadIDN
                dev.SerialNumber = devJoc.Serial_Number
                dev.Model = devJoc.Model
                dev.Hardware = ""
                dev.Firmware = devJoc.FW_Revision

                devJoc.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devJoc.FreqBand = gSelectedInstrument.BandIdx
                devJoc.RFPowerOnOff_TwoPorts(False)

                'devJoc.Close()

                resp.Add(dev)

                pPimDev = devJoc
            End If

            m_PowerLoss.TxL = gSelectedInstrument.TxLLoss
            m_PowerLoss.TxR = gSelectedInstrument.TxRLoss
            m_PowerLoss.Rx = 0

            Return resp

        Catch ex As Exception
            AddStatusMsg(String.Format("Initialize Equipment Failure!"))
            Throw New Exception("InitPimDevice()::" & ex.Message)
        End Try
    End Function
    Public Function InitPimDevice(bandName As String) As List(Of DataModels.Instrument)
        Try
            Dim resp As New List(Of DataModels.Instrument)
            Dim dev As New DataModels.Instrument
            Dim iv As CATS.Model.instr_vendor
            Dim ivBll As New CATS.BLL.instr_vendorManager

            gSelectedInstrument = gSelectedInstrumentList.Find(Function(o) o.FreqBand = bandName)
            If gSelectedInstrument Is Nothing Then Throw New Exception("Wrong  Config!")
            iv = ivBll.SelectByVendorName(gSelectedInstrument.Vendor.Trim)
            If iv Is Nothing Then Throw New Exception("Can Not find vendor <" & gSelectedInstrument.Vendor.Trim & ">")
            pRTP.instr_vendor_id = iv.id

            If gSelectedInstrument.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = gSelectedInstrument.Address
                devRos.Open()

                dev.Idn = devRos.ReadIDN
                dev.SerialNumber = devRos.Serial_Number
                dev.Model = IIf(devRos.Model Is Nothing, " ", devRos.Model)
                dev.Hardware = ""
                dev.Firmware = devRos.FW_Revision

                devRos.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                If Not pRTP.phase = "PIM700LU" Then
                    devRos.FreqBand = gSelectedInstrument.BandIdx
                End If
                devRos.RFPowerOnOff_TwoPorts(False)

                'devRos.Close()

                resp.Add(dev)

                pPimDev = devRos

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Zulu".ToString.ToUpper Then
                Dim devZulu As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                devZulu.Address = gSelectedInstrument.Address
                devZulu.Open()
                devZulu.FreqBandSet = gSelectedInstrument.FreqBand

                dev.Idn = "Commscope Zulu " & devZulu.FilterBandName & "_" & devZulu.Serial_Number

                If dev.Idn.ToUpper.Contains("Mismatch for the current Master with Filter".ToUpper) Then Throw New Exception("InitPimDevice()::" & "Mismatch for the current Master with Filter")

                dev.Model = devZulu.Model
                dev.SerialNumber = devZulu.Serial_Number
                dev.Hardware = ""
                dev.Firmware = devZulu.FW_Revision

                devZulu.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)

                devZulu.RFPowerOnOff_TwoPorts(False)

                'devZulu.Close()

                resp.Add(dev)

                pPimDev = devZulu

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Summitek".ToString.ToUpper Then
                Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devSum.Address = gSelectedInstrument.Address
                devSum.Open()

                dev.Idn = " "
                dev.SerialNumber = devSum.Serial_Number
                dev.Model = devSum.Model
                dev.Hardware = " "
                dev.Firmware = " " 'devSum.FW_Revision

                devSum.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devSum.FreqBand = gSelectedInstrument.BandIdx
                devSum.RFPowerOnOff_TwoPorts(False)

                'devSum.Close()

                resp.Add(dev)

                pPimDev = devSum

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                devKae.Address = gSelectedInstrument.Address
                devKae.Open()

                dev.Idn = " "
                dev.Model = devKae.Model
                dev.SerialNumber = devKae.Serial_Number
                dev.Hardware = " "
                dev.Firmware = " "

                devKae.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devKae.FreqBand = gSelectedInstrument.BandIdx
                devKae.RFPowerOnOff_TwoPorts(False)

                'devKae.Close()

                resp.Add(dev)

                pPimDev = devKae

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                devRfl.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim  'add by tony 20190529
                devRfl.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim 'add by tony 20190529
                If devRfl.Port_Select < 1 Or devRfl.Port_Select > 2 Then Throw New Exception("InitPimDevice():: Rflight Port selection") 'add by tony 20190529
                devRfl.Open()

                dev.Idn = devRfl.ReadIDN
                dev.SerialNumber = devRfl.Serial_Number
                dev.Model = devRfl.Model
                dev.Hardware = ""
                dev.Firmware = devRfl.FW_Revision

                devRfl.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devRfl.FreqBand = gSelectedInstrument.BandIdx
                devRfl.RFPowerOnOff_TwoPorts(False)

                'devRfl.Close()

                resp.Add(dev)

                pPimDev = devRfl

            ElseIf gSelectedInstrument.Vendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                devJoc.Address = gSelectedInstrument.Address.Split(CChar(":"))(0).Trim
                devJoc.Port_Select = gSelectedInstrument.Address.Split(CChar(":"))(1).Trim
                If devJoc.Port_Select < 1 And devJoc.Port_Select > 2 Then Throw New Exception("InitPimDevice():: JointCom Port selection wrong")
                devJoc.Open()

                dev.Idn = devJoc.ReadIDN
                dev.SerialNumber = devJoc.Serial_Number
                dev.Model = devJoc.Model
                dev.Hardware = ""
                dev.Firmware = devJoc.FW_Revision

                devJoc.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
                devJoc.FreqBand = gSelectedInstrument.BandIdx
                devJoc.RFPowerOnOff_TwoPorts(False)

                'devJoc.Close()

                resp.Add(dev)

                pPimDev = devJoc
            End If

            m_PowerLoss.TxL = gSelectedInstrument.TxLLoss
            m_PowerLoss.TxR = gSelectedInstrument.TxRLoss
            m_PowerLoss.Rx = 0

            Return resp

        Catch ex As Exception
            AddStatusMsg(String.Format("Initialize Equipment Failure!"))
            Throw New Exception("InitPimDevice()::" & ex.Message)
        End Try
    End Function
    Private Sub InitVibDevice(JsbBoard As String)
        Try

            Dim dev As New VibCtrl(System.Enum.Parse(GetType(VibCtrl.VibCtrlBoard), JsbBoard))

            pVibCtrl = dev


        Catch ex As Exception
            Throw New Exception("InitVibDevice()::" & ex.Message)
        End Try

    End Sub
    Private Function OpenVibDevice() As Boolean
        Try
            Dim jsbBoard As String = gPimTestConfig.Vibration.Address.ToString.ToUpper.Trim
            If jsbBoard <> "JSB31" And jsbBoard <> "JSB336" And jsbBoard <> "JSB340" And jsbBoard <> "SEADACLITE_8112" Then
                gPimTestConfig.Vibration.Address = "JSB340"
                gPimTestConfig.Save(gPimCfgFileName)
            End If

            If gPimTestConfig.Vibration.Enable = True Then
                InitVibDevice(gPimTestConfig.Vibration.Address)
                Return pVibCtrl.Open()
            Else
                Return True
            End If

        Catch ex As Exception
            Throw New Exception("OpenVibDevice()::" & ex.Message)
        End Try
    End Function
    Public Function InitCableTester() As Boolean
        Try
            gCableTester = New cable_tester_driver.driver
            Dim status As Integer
            status = gCableTester.getUsbHandles
            If status <> 1 Then
                AddStatusMsg("Error: " + status.ToString)
                Return False
            End If
            Dim distanceMoved As Integer
            GoHome("LEFT", 4, distanceMoved)
            GoHome("RIGHT", 4, distanceMoved)
            Return True
        Catch ex As Exception
            Throw New Exception("InitCableTester() - " & ex.Message)
        End Try
    End Function
    Public Sub GoHome(ByVal motor As String, ByVal speedIdx As Integer, ByRef distanceMoved As Integer)
        Try
            Dim status As Integer
            status = gCableTester.goHome(motor, speedIdx, distanceMoved)
            If status <> 1 Then
                AddStatusMsg("Error: " + status.ToString)
            End If
        Catch ex As Exception
            Throw New Exception("GoHome() - " & ex.Message)
        End Try
    End Sub
    Public Sub Insert(ByVal motor As String, ByVal speedIdx As Integer, ByVal distance As Integer, ByRef distanceMoved As Integer)
        Try
            Dim status As Integer
            status = gCableTester.insert(motor, speedIdx, distance, distanceMoved)
            If status <> 1 Then
                AddStatusMsg("Error: " + status.ToString)
            End If
        Catch ex As Exception
            Throw New Exception("Insert() - " & ex.Message)
        End Try
    End Sub
    Private Sub CloseVibDevice()
        Try
            If gPimTestConfig.Vibration.Enable = True Then
                pVibCtrl.Close()
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
    Public Function ClosePimDevice() As Boolean
        Try
#If INSTR_NORMAL_TEST Then
            If pPimDev IsNot Nothing Then pPimDev.Close()
#End If
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
            GUI.AddStatusMsg("Check Ret number ... , ", False)
            If m_RetList.Count <> pRTP.M_product_main.dwtilt_num Then
                GUI.AddStatusMsg("Fail")
                Throw New Exception("Rets number do Not match to Db")
            Else
                GUI.AddStatusMsg("OK", True)
            End If

            'check model
            GUI.AddStatusMsg("Check Ret model ... ,", False)
            For Each r As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                GUI.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, r.Value.AntModel), False)
                'If r.Value.AntModel.Trim.ToUpper = pRTP.M_product_main.dwtilt_pn.Trim.ToUpper Then
                If r.Value.AntModel.Trim.ToUpper.Contains(pRTP.M_product_main.dwtilt_pn.Trim.ToUpper) Then
                    GUI.AddStatusMsg("OK", True)
                Else
                    GUI.AddStatusMsg("Fail", True)
                    Throw New Exception("Rets model do not match to product")
                End If
            Next

            ' check sn
            GUI.AddStatusMsg("Check Ret sn ... ,", False)
            For Each r As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                GUI.AddStatusMsg(String.Format("Ret[{0}], SN [{1}] , ", r.Key, r.Value.AntennaSn), False)
                If r.Value.AntennaSn.Trim.ToUpper.Contains(pRTP.barcode.Trim.ToUpper) Then
                    GUI.AddStatusMsg("OK", True)
                Else
                    GUI.AddStatusMsg("Fail", True)
                    Throw New Exception("Rets sn do Not match to product")
                End If
            Next

            'check multiy/single
            GUI.AddStatusMsg("Check Ret single/multi ... ,", False)
            For Each r As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                GUI.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, CType(r.Value.Type, AisgDevice.DeviceType).ToString), False)
                If IIf(r.Value.Type = AisgDevice.DeviceType.SingleRet, "S", "M") = pRTP.M_product_main.dwtilt_mode.Trim.ToUpper Then
                    GUI.AddStatusMsg("OK", True)
                Else
                    GUI.AddStatusMsg("Fail", True)
                    Throw New Exception("Rets type miss match to Db")
                End If
            Next

        Catch ex As Exception
            Throw New Exception("CheckRets()::" & ex.Message)
        End Try

    End Sub
    Private Sub SetDownTilts(def_ret_idxs As String, def_ret_angles As String)
        Try
            Dim ir As Integer
            Dim ret_dev As DataModels.RetDevice
            Dim key As String, pIndex As Integer

            'para: idx(1)->tilt(0);idx(2)->tilt(0) ...
            If pRTP.M_product_main.dwtilt_enable = True Then
                If pRTP.M_phase_station_main.ret_validity = False Then Return
                If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET

                    If def_ret_idxs = "" Or def_ret_angles = "" Then
                        For Each ret As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                            ret.Value.Tilt.SetTilt(ret.Value.Tilt.MinTilt)
                        Next
                    Else
                        Dim ret_idx() As String = def_ret_idxs.Split(",")
                        Dim ret_angle() As String = def_ret_angles.Split(",")

                        If ret_idx.Count <> ret_angle.Count Then Throw New Exception("Mismatch <" & def_ret_idxs & "> with <" & def_ret_angles & ">")
                        For ir = 0 To ret_idx.Count - 1
                            ret_dev = Nothing
                            For Each ret As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                                key = ret.Key
                                pIndex = key.IndexOf(".")
                                If ret_idx(ir) = key.Substring(pIndex + 1) Then
                                    ret_dev = ret.Value
                                    Exit For
                                End If
                            Next
                            If ret_dev Is Nothing Then Throw New Exception("Not find key<" & ret_idx(ir) & "> in RetDevices")
                            ret_dev.Tilt.SetTilt(ret_angle(ir))
                        Next
                    End If
                End If
            End If

        Catch ex As Exception
            Throw New Exception("SetDownTilts()::" & ex.Message)
        End Try
    End Sub
    Public Function CheckCableCount(cable_seril_num As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function RunTestPhase(phase As TestSpec.TestPhase, ByRef cancel As Boolean) As Boolean
        Dim phReport As New TestReport.XmlFramework.TPhase
        Try
            Dim status As String = "P"
            Dim gpRp As New TestReport.XmlFramework.TGroup


            pRTP.phase = phase.Name

            pRTP.meas_start_time = GUI.StartTestTime
            pRTP.spec_main_id = phase.SpecMainId
            pRTP.phase_main_id = phase.PhaseMainId

            'pRTP.cable_consume_count = 0

            m_CriteriaItems = phase.TestCriteriaList
            If OpenVibDevice() = False Then Throw New Exception("Open Vib device")

#If INSTR_NORMAL_TEST Then
            'read sn from scanner and check with input sn
            If pRTP.barcode <> "CALPLTEST123" And gPimTestConfig.SnReader = True Then
                AddStatusMsg("Start to read SN from Keyence scanner...")
                Dim readSN As String = ReadScanner().Trim
                If pRTP.barcode <> readSN Then Throw New Exception(String.Format("The reading SN of cable assembly under test is <{0}>, the SN input is <{1}>, Mismtach!, Stop Test!", readSN, pRTP.barcode))
            End If
#End If
#If INSTR_NORMAL_TEST Then

            AddStatusMsg(String.Format("Start to Initialize Equipment..."))
            Dim equipmentWatch As New Stopwatch
            equipmentWatch.Start()
            m_EqList = InitPimDevice(phase.TestGroupList(0).TestItemList(0).cfg_imd_main.freq_band)

            If m_EqList(0).SerialNumber.Length = 0 Then
                If gSelectedInstrument.SerialNumber.Length = 0 Then
                    Throw New Exception("The reading instrument serial number is empty, please input instrument SN manually at device config")
                Else
                    m_EqList(0).SerialNumber = gSelectedInstrument.SerialNumber
                End If
            End If

            If CheckIfGrrExpired() Then Throw New Exception(String.Format("Equipment GRR Expired!"))
            equipmentWatch.Stop()
            AddStatusMsg(String.Format("Initialize Equipment Done, Time Cost = {0:0.0} seconds", equipmentWatch.Elapsed.TotalSeconds))
#Else
            m_EqList = InitVirtualPimDevice(phase.TestGroupList(0).TestItemList(0).cfg_imd_main.freq_band)
            If CheckIfGrrExpired() Then Throw New Exception(String.Format("Equipment GRR Expired!"))
#End If

            '因product_main表中的enable字段设定为false且dwtilt_type字段设定为0
            '所以OpenRetDevice()返回1，后续所有过程不执行。 
            'If OpenRetDevice() = 0 Then
            '    Dim rmL As New List(Of CATS.Model.product_ret_map)
            '    Dim prmBll As New CATS.BLL.product_ret_mapManager
            '    rmL = prmBll.SelectByProductMainId(pRTP.M_product_main.id)
            '    m_RetList = ScanAntennaRet(pRTP.barcode, pRTP.M_product_main.dwtilt_type, rmL)
            '    If m_RetList Is Nothing Then Throw New Exception("Can't find Rets")
            '    CheckRets()
            'End If

            If pPlantCode = "IN02" Then
                If CheckPowerCal() = False Then
                    MsgBox("Power Cal is not excecuted successfully within 12 hours, Stop Test!", MsgBoxStyle.Exclamation, "Power Cal Verify")
                    AddStatusMsg("Power Cal is not excecuted successfully within 12 hours, Stop Test!")
                    Return False
                End If
            End If

            AddStatusMsg(String.Format("Test Start..."))

            pRTP.MeasWatch.Restart()

            Dim phaseStatus As Model.cq_phases_status = Nothing
            Dim lstGp As List(Of Model.meas_group) = Nothing
            Dim lstPhaseStatus As List(Of CATS.Model.cq_phases_status)
            Dim cq_phstatus As New CATS.BLL.cq_phases_statusManager
            Dim groupNum As Integer = 0
            Dim measG As CATS.Model.meas_group
            Dim meas_detailManager As New BLL.meas_detailManager()
            Dim meas_traceManager As New BLL.meas_traceManager()
            Dim traceList As List(Of Model.meas_trace)
            Dim testTrace As New DataModels.TestTrace
            Dim testItem As Model.cq_spec_imd_details = Nothing
            For Each group As TestSpec.TestGroup In phase.TestGroupList
                Try
                    groupNum += 1
                    If gMLOCK_SN_List IsNot Nothing Then
                        If gMLOCK_SN_List.Count > 0 And pRTP.M_product_main.family = "MLOCK" Then
                            If groupNum = 1 Then
                                lstPhaseStatus = cq_phstatus.SelectAll(pRTP.M_product_main.product_name, pRTP.barcode, pRTP.product_mode, 2)

                                If lstPhaseStatus IsNot Nothing Then
                                    phaseStatus = lstPhaseStatus.Find(Function(o) o.phase = phase.Name)
                                End If

                                If phaseStatus IsNot Nothing Then
                                    Dim meas_groupManager As New BLL.meas_groupManager()
                                    lstGp = meas_groupManager.SelectBy_meas_phase_id(phaseStatus.meas_phase_id)
                                End If
                            End If
                            If lstGp IsNot Nothing Then
                                measG = lstGp.Find(Function(o) o.group_main_id = group.Id)
                                If measG IsNot Nothing AndAlso measG.group_status = "P" Then
                                    If MessageBox.Show(String.Format("Group <{0}> already test pass, do you still want to retest it?", group.Name), "Retest Group", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                                        gpRp = New TestReport.XmlFramework.TGroup
                                        gpRp.GroupMainId = group.Id
                                        gpRp.GroupName = group.Name
                                        gpRp.GroupStatus = "P"
                                        'phReport.TestGroups.Add(gpRp)
                                        Dim lstIt As List(Of Model.meas_detail) = meas_detailManager.SelectBy_meas_group_id(measG.id)
                                        For Each detail In lstIt
                                            testItem = group.TestItemList.Find(Function(o) o.spec_detail.id = detail.spec_detail_id)
                                            If testItem Is Nothing Then Continue For
                                            traceList = meas_traceManager.SelectBy_meas_detial_id(detail.id)
                                            AddPointToList(testTrace, traceList)
                                            testTrace.TwoToneFilter = Calculate.FilterDataPoint(testTrace.TwoTone)
                                            testTrace.Lambda = Calculate.LambdaTrace(testTrace.TwoToneFilter)
                                            Dim pr As New TestReport.ReportModule
                                            Dim crList As New Dictionary(Of String, CATS.Model.meas_criteria)
                                            crList = pr.GetMeasCriteriaList(testTrace, testItem, phase.TestCriteriaList)
                                            'pr.PrintTResultDcf(crList)
                                            Dim tpItem As TestReport.XmlFramework.TItem
                                            tpItem = TestReport.XmlReport.WriteTestItem(testItem, testTrace, crList, "N")
                                            tpItem.MeasString = "Load test"
                                            gpRp.TestItems.Add(tpItem)
                                        Next
                                        Continue For
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'GUI.WritePlcValues(numTestPhases.PreparedReady)

                    ' show connection prompt message
                    'Dim message As String
                    'If gMLOCK_SN_List Is Nothing Then
                    '    message = String.Format("Please Connect Cable Assembly To PIM Test Set")
                    'Else
                    '    If gMLOCK_SN_List.Count > groupNum - 1 Then
                    '        message = String.Format("Please Connect MLOCK PORT <{0}> To PIM Test Set", gMLOCK_SN_List(groupNum - 1))
                    '    Else
                    '        message = String.Format("Please Connect Cable Assembly To PIM Test Set")
                    '    End If
                    'End If

                    pRTP.ConnWatch.Restart()

                    'If MsgBox(Message, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "Connection") = MsgBoxResult.Cancel Then
                    '    pRTP.ConnWatch.Stop()
                    '    Throw New AbortedException("TestAborted")
                    'End If

                    pRTP.ConnWatch.Stop()

                    'GUI.PimFixtureReady = False

                    'Do While Not GUI.PimFixtureReady
                    '    'pTestPlan.ReadPlcAsync()
                    '    MyDelay(100)
                    'Loop


                    AddStatusMsg(String.Format("Start Test Group <{0}> ...", group.Name))

                    RunTestGroup(group, gpRp)

                    If gpRp Is Nothing Then Throw New Exception("TestError") 'status = "E" : Exit For

                Catch ex1 As AbortedException
                    Throw New AbortedException
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                Finally
                    phReport.TestGroups.Add(gpRp)
                    GUI.WritePlcValues(numTestPhases.FinallyResetTrigger)
                End Try


                If status = "P" And gpRp.GroupStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
        If status = "F" Then Exit For
#End If

            Next

            phReport.MeasStatus = status

            Return True

        Catch exa As AbortedException

            gSaveFile = False
            phReport.MeasStatus = "A"
            GUI.RecordResult("TestPhaseAborted", 1, 0, 0)
            Return False

        Catch ex As Exception

            gSaveFile = False
            phReport.MeasStatus = "E"
            GUI.RecordResult("TestPhaseError", 1, 0, 0)
            MsgBox("RunTestPhase()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False

        Finally

            Try
                pRTP.MeasWatch.Stop()
                pRTP.meas_time = pRTP.MeasWatch.Elapsed.TotalSeconds
                pRTP.conn_time = pRTP.ConnWatch.Elapsed.TotalSeconds
                AddStatusMsg(String.Format("Test Cycle Time = {0:0.0} s, Connection Time = {1:0.0}", pRTP.meas_time, pRTP.conn_time))
                pRTP.meas_stop_time = Now
                If pRTP.barcode = gInputLowPimLoadTestSn Or pRTP.barcode = gLowPimLoadTestSn Then
                    gLastMeasStopTime = Nothing
                Else
                    gLastMeasStopTime = pRTP.meas_stop_time.ToString("yyyy-MM-dd HH:mm:ss")
                End If
                pRTP.total_time = DateDiff(DateInterval.Second, pRTP.meas_start_time, pRTP.meas_stop_time)
                If gSaveFile Then
                    WriteReport(phReport, pRTP.barcode)
                    If phReport.MeasStatus = "P" Then
                        UpdateMiiRouting(pRTP.barcode)
                    Else
                        If pRTP.M_product_main.product_name.StartsWith("ML") Or pRTP.M_product_main.family = "MLOCK" Then
                            If gTestCount = 2 Then
                                UpdateMiiRouting(pRTP.barcode)
                            End If
                        Else
                            If gTestCount = 3 Then
                                UpdateMiiRouting(pRTP.barcode)
                            End If
                        End If
                    End If

                    If pRTP.two_sn_exist Then
                        WriteReport(phReport, pRTP.serialNumber2)
                        If phReport.MeasStatus = "P" Then
                            UpdateMiiRouting(pRTP.serialNumber2)
                        Else
                            If pRTP.M_product_main.product_name.StartsWith("ML") Or pRTP.M_product_main.family = "MLOCK" Then
                                If gTestCount = 2 Then
                                    UpdateMiiRouting(pRTP.serialNumber2)
                                End If
                            Else
                                If gTestCount = 3 Then
                                    UpdateMiiRouting(pRTP.serialNumber2)
                                End If
                            End If
                        End If
                    End If
                End If

                GUI.TestCountRest = GUI.TestCountMax - gTestCount

#If INSTR_NORMAL_TEST Then
                If pPlantCode = "IN02" Then
                    Dim tcmM As CATS.Model.test_cable_main
                    Dim tcmBll As New CATS.BLL.test_cable_mainManager
                    For Each testAcc In gTestAccessoryList.TestAccessoryList
                        If testAcc.Enable = False Then Continue For
                        tcmM = tcmBll.SelectByCableSN(testAcc.SerialNumber)
                        tcmM.test_count += 1
                        tcmBll.Update(tcmM)
                    Next
                End If
#End If

                Dim log As String = vbCrLf & pRTP.meas_start_time & vbCrLf & pRTP.barcode & vbCrLf & GUI.LogMessage
                SaveLogToFile(log, logfile)
                'SetDownTilts(pRTP.M_product_main.gen1, pRTP.M_product_main.gen2)

                'pTestCable.StoreConsumedNum(phase.PhaseMainId, pRTP.barcode)
            Catch ex As Exception
                GUI.WritePlcValues(numTestPhases.Err)
                Throw New Exception("RunTestPhase()::" & ex.Message)
            Finally
                CloseVibDevice()
                ClosePimDevice()
            End Try
        End Try
    End Function
    Private Sub SaveLogToFile(ByVal log As String, ByVal filename As String)
        Dim MaxLine As Long = 10000

        If File.Exists(filename) Then
            Dim ls() As String = File.ReadAllLines(filename)
            If ls.Length > MaxLine Then
                File.WriteAllLines(filename, ls.Skip(MaxLine \ 2).ToArray)
            End If
        End If
        My.Computer.FileSystem.WriteAllText(filename, log, True)

    End Sub
    Private Function CheckPowerCal()
        Try
            If pTestPlan.ProductStartupFolder().ToUpper = "Debug".ToUpper Then Return True
            Dim ctrId As Integer
            Dim ctrBll As New CATS.BLL.controllerManager
            Dim ctrM As CATS.Model.controller = ctrBll.SelectByName(My.Computer.Name)
            If ctrM Is Nothing Then
                ctrM = New CATS.Model.controller
                With ctrM
                    .name = My.Computer.Name
                    .factory_id = (New CATS.BLL.factoryManager).SelectByFactory(pFactory).id
                End With
                ctrId = ctrBll.AddReturnId(ctrM)
            Else
                ctrId = ctrM.id
            End If

            Dim ricBll As New rec_imd_calibrationManager
            Dim ricM As rec_imd_calibration = ricBll.SelectByPimSnCtrId(gSelectedInstrument.SerialNumber, ctrId)
            If ricM Is Nothing Then Return False
            If Now.Subtract(ricM.cal_time).TotalHours > 12 Then Return False
            Return True
        Catch ex As Exception
            Throw New Exception("TestPlan.CheckSN.CheckPowerCal()::" & ex.Message)
        End Try
    End Function
    Private Sub AddPointToList(ByRef testTrace As DataModels.TestTrace, traceList As List(Of Model.meas_trace))
        Try
            Dim x1(), y1() As String
            Dim len As Integer
            Dim imdPoint As DataModels.FrequencyPoint
            'Dim lambdaPoint As PointF
            For Each trace As Model.meas_trace In traceList
                x1 = trace.x1_data.Split(",")
                y1 = trace.y1_data.Split(",")
                len = Math.Min(x1.Length, y1.Length)
                Select Case trace.trace_name
                    Case "2Tone"
                        For i As Integer = 0 To len - 1
                            imdPoint = New DataModels.FrequencyPoint
                            imdPoint.TxlFreq = Single.Parse(trace.x2_data)
                            imdPoint.TxrFreq = Single.Parse(trace.x3_data)
                            imdPoint.RxFreq = Single.Parse(trace.x4_data)
                            imdPoint.XData = Single.Parse(x1(i))
                            imdPoint.YData = Single.Parse(y1(i))
                            testTrace.TwoTone.Add(imdPoint)
                        Next
                        'Case "SweepDown"
                        '    For i As Integer = 0 To len - 1
                        '        imdPoint = New DataModels.FrequencyPoint
                        '        imdPoint.TxlFreq = Single.Parse(x1(i))
                        '        imdPoint.TxrFreq = Single.Parse(x2(i))
                        '        imdPoint.RxFreq = Single.Parse(x3(i))
                        '        imdPoint.XData = Single.Parse(x1(i))
                        '        imdPoint.YData = Single.Parse(y1(i))
                        '        testTrace.SweepDown.Add(imdPoint)
                        '    Next
                        'Case "SweepUp"
                        '    For i As Integer = 0 To len - 1
                        '        imdPoint = New DataModels.FrequencyPoint
                        '        imdPoint.TxlFreq = Single.Parse(x1(i))
                        '        imdPoint.TxrFreq = Single.Parse(x2(i))
                        '        imdPoint.RxFreq = Single.Parse(x3(i))
                        '        imdPoint.XData = Single.Parse(x1(i))
                        '        imdPoint.YData = Single.Parse(y1(i))
                        '        testTrace.SweepUp.Add(imdPoint)
                        '    Next
                End Select
            Next

        Catch ex As Exception
            Throw New Exception("FormTest.AddPointToList()::" & ex.Message)
        End Try
    End Sub
    Private Sub UpdateMiiRouting(SN As String)
        Try
            If gPimTestConfig.MII.Enable = False Then Return

            Dim miiMain As New MIIBridge.Controller
            Dim reqs As New MIIBridge.Request
            Dim resp As MIIBridge.Response

            miiMain.EnableDebugLog = True
            reqs.Factory = pFactory
            reqs.TestUser = Environment.UserName.ToString.ToUpper
            reqs.SerialNumber = SN
            reqs.DbConnectionString = pDbConnString
            reqs.TestMode = pRTP.product_mode
            reqs.ProductName = pRTP.M_product_main.product_name
            reqs.PhaseStationId = pRTP.M_phase_station_main.id

            Dim ms As New MIIBridge.MeasPhase

            ms.Phase = pRTP.phase
            ms.PhaseStatus = pRTP.meas_status
            ms.StartDateTime = pRTP.meas_start_time
            ms.TotalTime = pRTP.meas_time

            reqs.CurrentMeasPhases = ms

            Try
                resp = miiMain.UpdateStatus(reqs)
                If resp.Status = Status.Error Or resp.Status = Status.Failure Then
                    AddStatusMsg("MII update station , response - " & resp.ErrorMessage)
                    AddStatusMsg("MII update station , retry ... ")
                    MyDelay(500)
                    resp = miiMain.UpdateStatus(reqs)
                    If resp.Status = Status.Error Or resp.Status = Status.Failure Then
                        AddStatusMsg("MII update station error - " & resp.ErrorMessage)
                        MsgBox("MII update station error - " & resp.ErrorMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    Else
                        AddStatusMsg("MII update station , response - " & resp.ErrorMessage)
                    End If
                Else
                    AddStatusMsg("MII update station , response - " & resp.ErrorMessage)
                End If

            Catch ex As Exception
                AddStatusMsg("MII update station error - " & ex.Message)
                MsgBox("MII update station error - " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            End Try


        Catch ex As Exception
            Throw New Exception("UpdateMiiRouting()::" & ex.Message)
        End Try


    End Sub
    Public Sub RunTestGroup(group As TestSpec.TestGroup, ByRef outTGroup As TestReport.XmlFramework.TGroup)
        Dim resp As New TestReport.XmlFramework.TGroup
        Dim rspItem As New List(Of TestReport.XmlFramework.TItem)

        Try

            ' If cancel = True Then Return Nothing  
            GUI.StartTestGroup(group.Name)

            Dim status As String = "P"

            resp.GroupMainId = group.Id
            resp.GroupName = group.Name



            For Each item As TestSpec.TestItem In group.TestItemList

                Try

                    RunTestItem(item, rspItem)

                    If rspItem Is Nothing Then Throw New Exception("TestError")

                Catch ex1 As AbortedException

                    Throw New AbortedException

                Catch ex As Exception

                    Throw New Exception(ex.Message)

                Finally

                    If rspItem IsNot Nothing Then
                        For Each ti As TestReport.XmlFramework.TItem In rspItem
                            resp.TestItems.Add(ti)
                        Next
                    End If



                End Try

                'If status = "E" Or status = "A" Then Exit For
                'If itRp.MeasStatus = "E" Or itRp.MeasStatus = "A" Then status = itRp.MeasStatus : Exit For
                If status = "P" And rspItem(rspItem.Count - 1).MeasStatus = "F" Then status = "F"

#If PC_FAIL_STOP = 1 Then
        If status = "F" Then Exit For
#End If

                If pRTP.M_phase_station_main.meas_type = 1 Then Exit For 'for pre test

            Next

            resp.GroupStatus = status

            'Return resp

        Catch exa As AbortedException

            resp.GroupStatus = "A"
            GUI.RecordResult("TestGroupAborted", -1, 0, 0)
            Throw New AbortedException

        Catch ex As Exception

            '  cancel = True
            resp.GroupStatus = "E"
            GUI.RecordResult("TestGroupError", -1, 0, 0)
            Throw New Exception("RunTestGroup()::" & ex.Message)
            'Return resp

        Finally

            'RunTestGroup = resp
            outTGroup = resp
            GUI.StopTestGroup()

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

    Public Sub RunTestItem(item As TestSpec.TestItem, ByRef outTItem As List(Of TestReport.XmlFramework.TItem))
        Dim resp As New List(Of TestReport.XmlFramework.TItem)
        Try
            'Run test

            frm = New FormTest
            frm.TestMode = DataModels.TestMode.Test
            frm.RetDeviceList = m_RetList
            frm.TestItem = item
            frm.InstrPowerLoss = m_PowerLoss
            frm.TestCriteriaSpec = m_CriteriaItems

            ' Get frequency box(config) by selecting cfg_imd_main.id and pRTP.instr_vendor_id
            frm.PSFReq = TestModules.GetSweepFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)
            frm.PFFReq = TestModules.GetFixedFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)
            frm.PCFReq = TestModules.GetCustomFreqs(item.cfg_imd_main, pRTP.instr_vendor_id)

#If INSTR_NORMAL_TEST = 1 Then
            If pRTP.phase = "PIM700LU" Then
                If gSelectedInstrument.Vendor.ToUpper = "Zulu".ToUpper Then
                    pPimDev.FreqBandSet = gSelectedInstrument.FreqBand
                Else
                    pPimDev.FreqBand = gSelectedInstrument.BandIdx
                End If
            End If
#End If

            If pRTP.M_phase_station_main.meas_type = 1 Then frm.PFFReq = Nothing


            frm.ShowDialog()

            resp = frm.ReportItem

            If resp(resp.Count - 1).MeasStatus = "E" Then Throw New Exception("TestError")
            If resp(resp.Count - 1).MeasStatus = "A" Then Throw New AbortedException


        Catch exa As AbortedException

            If resp.Count = 0 Then resp = VirtualTestItem(item)
            resp(resp.Count - 1).MeasStatus = "A"
            resp(resp.Count - 1).MeasString = "TestAborted"

            GUI.RecordResult("TestItemAborted", -1, 0, 0)

            Throw New AbortedException()

        Catch ex As Exception


            If resp.Count = 0 Then resp = VirtualTestItem(item)
            resp(resp.Count - 1).MeasString = ex.Message '"TestError"
            resp(resp.Count - 1).MeasStatus = "E"

            GUI.RecordResult("TestItemError", -1, 0, 0)
            Throw New Exception("RunTestItem()::" & ex.Message)

        Finally

            outTItem = resp

        End Try
    End Sub
    Private Sub SetRetDownTilt(tiltIdxs As String, downTilts As String)
        Try
            Dim tpidx() As String
            Dim tpdwtilt() As String
            Dim fSet As Boolean = False

            tpidx = tiltIdxs.Split(",")
            tpdwtilt = downTilts.Split(",")
            If tpidx.Count <> tpdwtilt.Count Then
                GUI.AddStatusMsg(String.Format("RET idx {0} miss match downtilt{1}.", tiltIdxs, downTilts), True)
                Throw New Exception(String.Format("RET idx {0} miss match downtilt {1}.", tiltIdxs, downTilts))
            End If

            For Each dev As KeyValuePair(Of String, DataModels.RetDevice) In m_RetList
                For id As Short = 0 To tpidx.Count - 1
                    'If dev.Value.Type = AisgDevice.DeviceType.SingleRet Then
                    If tpidx(id) = 0 Then
                        GUI.AddStatusMsg("Ret Idx=0, No Ret.")
                        fSet = True
                    End If

                    If Right(dev.Key.ToString, 1) = tpidx(id) Then
                        Dim frm As New FormSetRet
                        Try
                            GUI.AddStatusMsg(String.Format("Setting RET idx={0},downtilt={1} ... , ", tpidx(id), tpdwtilt(id)), False)
                            frm.ShowDownTilt = tpdwtilt(id)
                            frm.Show()
                            My.Application.DoEvents()
                            dev.Value.Tilt.SetTilt(tpdwtilt(id))
                            GUI.AddStatusMsg("Ok")
                            fSet = True
                        Catch ex As Exception
                            Throw New Exception(ex.Message)
                        Finally
                            frm.Close()
                        End Try
                    End If
                    'Else

                    'End If
                    'If Right(dev.Key.ToString, 1) = tpidx(id) Then
                    'Dim frm As New FormSetRet
                    'Try
                    '  GUI.AddStatusMsg(String.Format("Setting RET idx={0},downtilt={1} ... , ", tpidx(id), tpdwtilt(id)), False)
                    '  frm.ShowDownTilt = tpdwtilt(id)
                    '  frm.Show()
                    '  dev.Value.Tilt.SetTilt(tpdwtilt(id))
                    '  GUI.AddStatusMsg("Ok")
                    'Catch ex As Exception
                    '  MsgBox("SetRetDownTilt()::" & ex.Message)
                    'Finally
                    '  frm.Close()
                    'End Try
                    'Return True
                    'End If
                Next
            Next

            If fSet = False Then Throw New Exception("Mismatch RET ID")
            ' GUI.AddStatusMsg("Fail")

            'Return False

        Catch ex As Exception
            'Return False
            Throw New Exception("SetRetDownTilt()::" & ex.Message)
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
                GUI.AddStatusMsg(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts), True)
                Throw New Exception(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts))
            End If

            msg += "Please manually turn the Downtilt to specified angle :" & vbCrLf

            For id As Short = 0 To tpidx.Count - 1
                msg += String.Format(" Tilt# {0}, Angle ={1}", tpidx(id), tpdwtilt(id)) & vbCrLf
            Next

            MsgBox(msg, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)

            GUI.AddStatusMsg("OK")

        Catch ex As Exception
            Throw New Exception("SetVetDownTilt()::" & ex.Message)
        End Try
    End Sub
    Public Sub SetDownTilts(item As TestSpec.TestItem)
        Try

            If pRTP.M_product_main.dwtilt_enable = True Then
                If pRTP.M_phase_station_main.ret_validity = False Then Return

                If pRTP.M_product_main.dwtilt_type >= 2 Then 'RET
                    SetRetDownTilt(item.spec_detail.dwtilt_idxs, item.spec_detail.dwtilt_angs)
                ElseIf pRTP.M_product_main.dwtilt_type = 1 Then 'VET
                    SetVetDownTilt(item.spec_detail.dwtilt_idxs, item.spec_detail.dwtilt_angs)
                Else
                    Throw New Exception("RET is enable, but not RET or VET  or ...")
                End If
            End If


        Catch ex As Exception
            Throw New Exception("SetDownTilts()::" & ex.Message)
        End Try
    End Sub
    Public Sub AddStatusMsg(Msg As String, Optional newLine As Boolean = True)
        If GUI Is Nothing Then Exit Sub
        Try
            GUI.AddStatusMsg(Msg, newLine)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function ProductStartupFolder() As String
        Dim name As String = Application.StartupPath.Substring(Application.StartupPath.LastIndexOf("\") + 1)
        Return name
    End Function
    Private Function CheckIfGrrExpired() As Boolean
        Try
            If m_EqList Is Nothing Then Return True
            If m_EqList.Count = 0 Then Return True
            If gPimTestConfig.Automation = True Then Return False
            If ProductStartupFolder().ToUpper = "Debug".ToUpper Then Return False

            Dim GC_Procedure_List As List(Of CATS.Model.GC_Procedure)

            Dim GC_Procedure_Bll As New CATS.BLL.GC_ProcedureManager

            Dim AnalyzerSN As String = m_EqList(0).SerialNumber

            If AnalyzerSN Is Nothing Then Return False
            If AnalyzerSN.Length = 0 Then Return False

            GC_Procedure_List = GC_Procedure_Bll.SelectMostRecent(AnalyzerSN)

            If GC_Procedure_List Is Nothing Then
                Return True
            End If

            Dim query = From gc In GC_Procedure_List
                        Where gc.AnalyzerModel = gSelectedInstrument.Model

            If query.Count > 0 Then
                Dim GC_Procedure_Model As CATS.Model.GC_Procedure = query.First
                Dim GCProcedureDatePerformed As DateTime = GC_Procedure_Model.TimeCompleted.ToString("MM/dd/yyyy hh:mm:ss tt")
                Dim GageCapability As Decimal = GC_Procedure_Model.PTRatio
                Dim temp As DateTime = Now
                Dim expiry As DateTime = GC_Procedure_Model.TimeCompleted.AddDays(14)
                If temp < expiry Then
                    If GageCapability < 30 Then
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("GC.CheckIfGrrExpired()::" & ex.Message)
        End Try
    End Function
    Public Function HiPowerCheck() As Boolean
        Try
            For Each eq In m_EqList
                If eq.SerialNumber = "010IM-A3815" Or eq.SerialNumber = "010IM-A4651" Or eq.SerialNumber = "010IM-A4729" Then
                    AddStatusMsg(String.Format("Instrument SN = <{0}> allow to test at high power level up to 46dBm", eq.SerialNumber))
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Throw New Exception("HiPowerCheck()::" & ex.Message)
        End Try
    End Function
    Public Function ReadScanner() As String
        Try
            m_reader.Connect(AddressOf ReceivedDataAction)
            Return ReceivedDataWrite(m_reader.ExecCommand("LON"))
        Catch ex As Exception
            Throw New Exception("ReadScanner()::" & ex.Message)
        End Try
    End Function
    Private Sub ReceivedDataAction(data As Byte())
        'Define received data actions here.Defined actions work asynchronously.
        '"ReceivedDataWrite" works when reading data was received.
        Dim dgRun As New delegateUserControl(AddressOf ReceivedDataWrite)
        Dim res As IAsyncResult = dgRun.BeginInvoke(System.Text.Encoding.ASCII.GetString(data), Nothing, Nothing)
    End Sub
    Private Function ReceivedDataWrite(receivedData As String) As String
        AddStatusMsg(String.Format("The reading SN from SR-X100 is <{0}>", receivedData))
        Return receivedData
    End Function
End Module
