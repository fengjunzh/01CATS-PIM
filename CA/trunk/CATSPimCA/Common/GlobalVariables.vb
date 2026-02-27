Module GlobalVariables


#Region "Database Operation"
    'Public Class CATSDb
    '  Public Shared cq_criteria_detailManager As New CATS.BLL.cq_criteria_detailManager
    '  Public Shared group_mainManager As New CATS.BLL.group_mainManager
    '  Public Shared cq_spec_imd_detailsManager As New CATS.BLL.cq_spec_imd_detailsManager
    '  Public Shared cfg_imd_sfboxManager As New CATS.BLL.cfg_imd_sfboxManager
    '  Public Shared cfg_imd_ffboxManager As New CATS.BLL.cfg_imd_ffboxManager
    '  Public Shared cfg_imd_cfboxManager As New CATS.BLL.cfg_imd_cfboxManager

    '  Public Shared cfg_spara_mainManager As New CATS.BLL.cfg_spara_mainManager
    'End Class

#End Region

    Public GUI As New RFPATestInterface.OperatorInterface

    Public pAppPath As String = "c:\cats\"
	Public pTestSystemPath As String = pAppPath & "test_system\"
	Public pTestResultPath As String = pAppPath & "test_result\"
    Public pAppIniFile As String = Application.StartupPath & "\CATSPim.ini"
    Public gPimTestConfig As CPimTestConfig
    Public gPimCfgFileName As String = Application.StartupPath & "\CATSPimTestConfig.xml"
    Public gTestAccessoryList As CTestAccessoryList
    Public gTestAccessoryListFileName As String = Application.StartupPath & "\TestAccessoryList.xml"
    Public pCatsCfgFile As String = pTestSystemPath & "CATS_CA.xmls"
    Public pSAPFailSafeMode As SAPFailSafeMode
    Public pSAPFailSafeModeCfgFileName As String = "SAPFailSafeMode.xml"
    Public pSAPFailSafeModeCfgFile As String = pTestSystemPath & "SAPFailSafeMode.xml"

    Public pTestResult As New RFPATestInterface.clsTestResults
	Public pTestPlan As New TestPlan

    Public gSAPFailSafeMode As SAPFailSafeMode = Nothing
    Public gSAPFailSafeModeFile As String = "c:\cats\test_system\SAPFailSafeMode.xml"

    Public pFreqBoxes As New Dictionary(Of String, DataModels.FrequencyGroup)

    Public pRTP As DataModels.RTimePara

    Public pAbortFlag As Boolean

    Public pMTDisplay As DelegateDisplay.MTFormTest

    Public pFactory As String
    Public pPlantCode As String
    Public pDbConnString As String
    Public gMLOCK_SN_List As List(Of String)
    Public gTestCount As Integer
    Public gDiscard_value As Double = -200
    Public gFFTestTimes As SByte
    Public gCFTestTimes As SByte
    Public gAdjacent2PVar As Decimal
    Public gGetPassVirtualVale As Boolean
    Public gSaveFile As Boolean = True
    Public Const gLowPimLoadTestSn As String = "CALPLTEST123"
    Public Const gInputLowPimLoadTestSn As String = "ICLPLTEST123"
    Public gLastMeasStopTime As String = Nothing
    Public gNeedTest As Boolean
    Public gConstPimPowerCalIntervalHour As Integer = 12
    Public gTestCable As TestCable
    Public gOpcUaClient As OpcUaUtility.OpcUaClient
    Public gPimFixtureReady As Boolean

#Region "Instruments"
    Public pPimDev As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	Public pRetCtrl As AisgDevice
    Public pVibCtrl As IVibrationDevice
    Public pPSCtrl As AndrewIntegratedProducts.InstrumentsFramework.Instrument
    Public gCableTester As cable_tester_driver.driver
    Public gSelectedInstrumentList As List(Of CInstrument)
    Public gSelectedInstrument As CInstrument
#End Region


End Module
