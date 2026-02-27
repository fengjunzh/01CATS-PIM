Public Class FormDebug
	Private m_PowerLoss As New DataModels.PowerLoss
	Private Sub loadFreqBands()
		Try
			Dim dt As DataTable

			dt = pAppCfg.GetInstruments()

			lstFreqBands.Items.Clear()

			For Each fb As DataRow In dt.Rows
				If fb.Item("Enable") = True Then
					lstFreqBands.Items.Add(fb.Item("BandName"))
				End If
			Next

		Catch ex As Exception
			Throw New Exception("LoadFreqBands()::" & ex.Message)
		End Try
	End Sub
	Private Sub initPimDevice(bandName As String)
		Try
			Dim resp As New List(Of DataModels.Instrument)

			Dim instrPara As CATSPimConfig.LocalConfig.InstrPara
			Dim dev As New DataModels.Instrument
			Dim iv As CATS.Model.instr_vendor
			Dim ivBll As New CATS.BLL.instr_vendorManager

			instrPara = pAppCfg.GetInstrumentConfig(bandName)
			pTestCable = New TestCable(instrPara.CableSerNum)

			iv = ivBll.SelectByVendorName(instrPara.Vendor.Trim)
			If iv Is Nothing Then Throw New Exception("Can not find vendor <" & instrPara.Vendor.Trim & ">")
			pRTP.instr_vendor_id = iv.id

            If instrPara.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = instrPara.Address
                pPimDev = devRos
            ElseIf instrPara.Vendor.Trim.ToUpper = "Summitek".ToString.ToUpper Then
                Dim devSum As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devSum.Address = instrPara.Address
                pPimDev = devSum
            ElseIf instrPara.Vendor.Trim.ToUpper = "Kaelus".ToString.ToUpper Then
                Dim devKae As New AndrewIntegratedProducts.InstrumentsFramework.KaelusBPIMDevice
                devKae.Address = instrPara.Address
                pPimDev = devKae
            ElseIf instrPara.Vendor.Trim.ToUpper = "Rflight".ToString.ToUpper Then
                Dim devRfl As New AndrewIntegratedProducts.InstrumentsFramework.RflightDevice
                devRfl.Address = instrPara.Address
                pPimDev = devRfl
            ElseIf instrPara.Vendor.Trim.ToUpper = "JoinCom".ToString.ToUpper Then
                Dim devJoc As New AndrewIntegratedProducts.InstrumentsFramework.JoinCom
                'devJoc.Address = instrPara.Address

                devJoc.Address = instrPara.Address.Split(CChar(":"))(0).Trim  'add by tony 20190529
                devJoc.Port_Select = instrPara.Address.Split(CChar(":"))(1).Trim 'add by tony 20190529
                If devJoc.Port_Select <> 1 And devJoc.Port_Select <> 2 Then Throw New Exception("InitPimDevice():: JoinCom Port selection") 'add by tony 20190529

                pPimDev = devJoc
            ElseIf instrPara.Vendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then
                Dim devZUl As New AndrewIntegratedProducts.InstrumentsFramework.ZuluPIM
                devZUl.Address = instrPara.Address
                pPimDev = devZUl
            End If

            pPimDev.Open()

            If instrPara.Vendor.Trim.ToUpper = "ZULU".ToString.ToUpper Then pPimDev.FreqBandSet = instrPara.BandName

            pPimDev.SetTestMode(AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice.enumTESTMODE.REFMODE)
			pPimDev.FreqBand = instrPara.BandIdx
			pPimDev.RFPowerOnOff_TwoPorts(False)
			'pPimDev.Close()

			m_PowerLoss.TxL = instrPara.Tx1Loss
			m_PowerLoss.TxR = instrPara.Tx2Loss
			m_PowerLoss.Rx = instrPara.RxLoss

		Catch ex As Exception
			Throw New Exception("InitPimDevice()::" & ex.Message)
		End Try
	End Sub
	Private Function openPimDevice() As Boolean
		Try

			Return pPimDev.Open()

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
	Private Sub FormDebug_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            loadFreqBands()

            AccuracyValue.Text = AccuracyValue.Items(0)

            SpeedValue.Text = SpeedValue.Items(2)

        Catch ex As Exception

        End Try
	End Sub
	Private Function getTestItem(cfg_imd_main_id As Integer, limit As Single) As TestSpec.TestItem
		Try
			Dim rntItem As New TestSpec.TestItem
			Dim mdSpecdetail As New CATS.Model.spec_detail

			With mdSpecdetail
				.limit_low = -199
				.limit_up = limit
				.meas_item = "DEBUGTEST"
			End With
			With rntItem
				.spec_detail = mdSpecdetail
				.spec_imd_detail.cfg_imd_main_id = cfg_imd_main_id
				.cfg_imd_main = (New CATS.BLL.cfg_imd_mainManager).SelectById(cfg_imd_main_id)
			End With

			Return rntItem

		Catch ex As Exception
			Throw New Exception("getTestItem()::" & ex.Message)
		End Try
	End Function
	Private Function getCriteriaItems(criteria_main_id As Integer) As Dictionary(Of String, CATS.Model.cq_criteria_detail)
		Try
			Dim rnt As New Dictionary(Of String, CATS.Model.cq_criteria_detail)
			Dim cq_criteria_detailManager As New CATS.BLL.cq_criteria_detailManager
			Dim cq_criteriaList As List(Of CATS.Model.cq_criteria_detail) = cq_criteria_detailManager.SelectById(criteria_main_id)

			If cq_criteriaList IsNot Nothing Then
				For Each cq_ct As CATS.Model.cq_criteria_detail In cq_criteriaList
					rnt.Add(cq_ct.criteria_item.ToUpper.Trim, cq_ct)
				Next
			End If

			Return rnt

		Catch ex As Exception
			Throw New Exception("getCriteriaItems()::" & ex.Message)
		End Try
	End Function
	Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
		Try
			Dim tmpFreqband As String
			Dim tmpLimit As Single
			Dim mdFreqband As New CATS.Model.cfg_imd_freq_band
			Dim mgrFreqband As New CATS.BLL.cfg_imd_freq_bandManager
			Dim mdDebugpara As New CATS.Model.imd_debug_para
			Dim mgrDebugpara As New CATS.BLL.imd_debug_paraManager

			If IsNumeric(txtLimit.Text.Trim) = True Then
				tmpLimit = CSng(txtLimit.Text.Trim)
			Else
				MsgBox("Please input limit!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
				Exit Sub
			End If

			tmpFreqband = lstFreqBands.SelectedItem.ToString.Trim.ToUpper

			mdFreqband = mgrFreqband.SelectByBandname(tmpFreqband)
			mdDebugpara = mgrDebugpara.SelectByCfgImdFreqBandId(mdFreqband.id)

			initPimDevice(tmpFreqband)

			'openPimDevice()

			'Run test
			Dim frm As FormTest

			frm = New FormTest
			pRTP.AlgoParas = GetAlgoParasByAlgoMainId(mdDebugpara.algo_para_main_id)
			pRTP.product_mode = pAppCfg.GetTestMode


			pRTP.M_product_main = New CATS.Model.product_main
			pRTP.M_product_main.product_name = "DEBUG"

			pRTP.phase = tmpFreqband

			frm.TestMode = DataModels.TestMode.Debug
			frm.RetDeviceList = Nothing
			frm.TestItem = getTestItem(mdDebugpara.cfg_imd_main_id, tmpLimit)
			frm.InstrPowerLoss = m_PowerLoss
			frm.TestCriteriaSpec = getCriteriaItems(mdDebugpara.criteria_main_id)
			frm.PSFReq = TestModules.GetSweepFreqs(frm.TestItem.cfg_imd_main, pRTP.instr_vendor_id)
			frm.PFFReq = TestModules.GetFixedFreqs(frm.TestItem.cfg_imd_main, pRTP.instr_vendor_id)
			frm.PCFReq = TestModules.GetCustomFreqs(frm.TestItem.cfg_imd_main, pRTP.instr_vendor_id)

			frm.ShowDialog()

		Catch ex As Exception
			MsgBox("btnStart_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

		Finally
			ClosePimDevice()
		End Try

	End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    'DTF=======================================================================================================
    Private Sub Start_Calibration_Click(sender As Object, e As EventArgs) Handles Start_Calibration.Click
        Try

            Dim tmpFreqband As String
            Dim tmpLimit As Single
            Dim mdFreqband As New CATS.Model.cfg_imd_freq_band
            Dim mgrFreqband As New CATS.BLL.cfg_imd_freq_bandManager
            Dim mdDebugpara As New CATS.Model.imd_debug_para
            Dim mgrDebugpara As New CATS.BLL.imd_debug_paraManager

            If IsNumeric(txtLimit.Text.Trim) = True Then
                tmpLimit = CSng(txtLimit.Text.Trim)
            Else
                MsgBox("Please input limit!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Exit Sub
            End If

            tmpFreqband = lstFreqBands.SelectedItem.ToString.Trim.ToUpper

            mdFreqband = mgrFreqband.SelectByBandname(tmpFreqband)
            mdDebugpara = mgrDebugpara.SelectByCfgImdFreqBandId(mdFreqband.id)

            initPimDevice(tmpFreqband)


            Start_Calibration.Enabled = False
            pPimDev.DTF_Cal()
        Catch ex As Exception
            Start_Calibration.Enabled = True
            Throw New Exception("DTF_Cal()::" & ex.Message)
        End Try

        Start_Calibration.Enabled = True

    End Sub

    Private Sub StartTest_DTF_Click(sender As Object, e As EventArgs) Handles StartTest_DTF.Click

        Try

            Dim tmpFreqband As String
            Dim tmpLimit As Single
            Dim mdFreqband As New CATS.Model.cfg_imd_freq_band
            Dim mgrFreqband As New CATS.BLL.cfg_imd_freq_bandManager
            Dim mdDebugpara As New CATS.Model.imd_debug_para
            Dim mgrDebugpara As New CATS.BLL.imd_debug_paraManager

            If IsNumeric(txtLimit.Text.Trim) = True Then
                tmpLimit = CSng(txtLimit.Text.Trim)
            Else
                MsgBox("Please input limit!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                Exit Sub
            End If

            tmpFreqband = lstFreqBands.SelectedItem.ToString.Trim.ToUpper

            mdFreqband = mgrFreqband.SelectByBandname(tmpFreqband)
            mdDebugpara = mgrDebugpara.SelectByCfgImdFreqBandId(mdFreqband.id)

            initPimDevice(tmpFreqband)

            Dim Accurate As String, speed As String
            StartTest_DTF.Enabled = False
            DTP_Time.Text = "?"

            Select Case SpeedValue.Text.Trim
                Case "Fast"
                    speed = "8"
                Case "Middle Fast"
                    speed = "4"
                Case "Median"
                    speed = "2"
                Case "Slow"
                    speed = "1"
                Case Else
                    StartTest_DTF.Enabled = True
                    Throw New Exception("StartTest_DTF_Click -> SpeedValue::" & SpeedValue.Text.Trim)
            End Select

            Select Case AccuracyValue.Text.Trim
                Case "High"
                    Accurate = "65536"
                Case "Median"
                    Accurate = "32768"
                Case Else
                    StartTest_DTF.Enabled = True
                    Throw New Exception("StartTest_DTF_Click -> AccuracyValue::" & SpeedValue.Text.Trim)
            End Select

            '  Accurate = AccuracyValue.Text.Trim

            DTP_Time.Text = GetDTFTime(Accurate, speed) ' pPimDev.GetDTP_Time(Accurate, speed)

        Catch ex As Exception
            StartTest_DTF.Enabled = True
            Throw New Exception("StartTest_DTF_Click -> GetDTP_Time()::" & ex.Message)
        End Try
        StartTest_DTF.Enabled = True
    End Sub

End Class