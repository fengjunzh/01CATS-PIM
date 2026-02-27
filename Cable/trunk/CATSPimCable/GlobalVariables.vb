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

    Public GUI As New RFPATestInterface.clsOperatorInterface

    Public pAppPath As String = "c:\cats\"
	Public pTestSystemPath As String = pAppPath & "test_system\"
	Public pTestResultPath As String = pAppPath & "test_result\"
	Public pAppIniFile As String = Application.StartupPath & "\CATSPim.ini"
	Public pAppCfgFileName As String = "CATSPimTestConfig.xml"
	Public pAppCfgFilePath As String = pTestSystemPath & pAppCfgFileName
	Public pAppCfg As CATSPimConfig.LocalConfig
    Public pCatsCfgFile As String = pTestSystemPath & "CATS.xmls"
    Public pSAPFailSafeModeCfgFileName As String = "SAPFailSafeMode.xml"
    Public pSAPFailSafeModeCfgFile As String = pTestSystemPath & "SAPFailSafeMode.xml"

    Public pTestResult As New RFPATestInterface.clsTestResults
	Public pTestPlan As New TestPlan

    Public gSAPFailSafeModeFile As String = "c:\cats\test_system\SAPFailSafeMode.xml"

    Public pFreqBoxes As New Dictionary(Of String, DataModels.FrequencyGroup)

    Public pRTP As DataModels.RTimePara

    Public pAbortFlag As Boolean


    Public pMTDisplay As DelegateDisplay.MTFormTest

    Public pFactory As String
    Public pPlantCode As String
    Public pDbConnString As String
    'Public pTestCable As TestCable

#Region "Instruments"

    Public pPimDev As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	Public pRetCtrl As AisgDevice
	Public pVibCtrl As IVibrationDevice
#End Region


End Module
