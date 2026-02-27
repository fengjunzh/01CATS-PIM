Imports System.Data
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

            'pTestCable = New TestCable(instrPara.CableSerNum)

            iv = ivBll.SelectByVendorName(instrPara.Vendor.Trim)
            If iv Is Nothing Then Throw New Exception("Can not find vendor <" & instrPara.Vendor.Trim & ">")
            pRTP.instr_vendor_id = iv.id

            If instrPara.Vendor.Trim.ToUpper = "Rosenberger".ToString.ToUpper Then
                Dim devRos As New AndrewIntegratedProducts.InstrumentsFramework.RosenbergerDevice
                devRos.Address = instrPara.Address
                pPimDev = devRos
            ElseIf instrPara.Vendor.Trim.ToUpper = "Zulu".ToString.ToUpper Then
                Dim devZulu As New AndrewIntegratedProducts.InstrumentsFramework.SummitekDevice
                devZulu.Address = instrPara.Address
                pPimDev = devZulu
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
            End If

            pPimDev.Open()
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
            pRTP.product_mode = "PROD" 'pAppCfg.GetTestMode


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
End Class