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

    'Public pMode As String = "PROD"
    Public pGui As New RFPATestInterface.clsOperatorInterface

    Public pAppPath As String = "c:\cats\"
	Public pTestSystemPath As String = pAppPath & "test_system\"
	Public pTestResultPath As String = pAppPath & "test_result\"
	Public pAppIniFile As String = Application.StartupPath & "\CATSPim.ini"
	Public pAppCfgFileName As String = "CATSPimTestConfig.xml"
	Public pAppCfgFilePath As String = pTestSystemPath & pAppCfgFileName
	Public pAppCfg As CATSPimConfig.LocalConfig
	Public pCatsCfgFile As String = pTestSystemPath & "CATS.xmls"

	Public pTestResult As New RFPATestInterface.clsTestResults
    Public pTestPlan As New TestPlan
    '=========================================================================

    Public Cal_phasenamelist As New List(Of String)
    Public PN_Cal As String
    Public LowPim_Load_Spec As Single
    Public LowPim_Load_Spec_Enable As Boolean


    '=========================================================================
    'Public pXmlReport As TestReport.XmlReport
    'Public pXmlFile As New TestReport.XmlFramework

    'Public pTestSpec As TestSpec
    Public pFreqBoxes As New Dictionary(Of String, DataModels.FrequencyGroup)
	'Public pCriteriaItems As New List(Of CATS.Model.cq_criteria_detail)
	Public pRTP As DataModels.RTimePara
	' Public pTestGroups As Dictionary(Of Short, GlobalModules.TestGroupPara)
	Public pAbortFlag As Boolean
	'Public pErrorFlag As Boolean

	Public pMTDisplay As DelegateDisplay.MTFormTest


    '#Const PC_CHECKPNSN = 0

    Public pFactory As String
    Public pFactoryCode As String
    Public pFactoryID As String
    Public pDbConnString As String

    Public pTestCable As TestCable
    '==========================================Frank
    Public CurPCName As String

    '==========================================
    Public CheckPhaseForPowCalSN As String
    Public ChamberID As String

    Public Temp_PIM_descr As String   'add for pretest of STS-M

    Public DTFTestTime As String   'add for pretest of STS-M
    Public DTFTestTime_AutoDisplay As String = ""   'add for DTF

#Region "Instruments"

    Public pPimDev As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	Public pRetCtrl As AisgDevice
    Public pVibCtrl As IVibrationDevice
    Public PowerMeasDev As AndrewIntegratedProducts.InstrumentsFramework.PowerCal ' add by tony for Power calibration
    Public PowerMeasFrq As String ' add by tony for Power calibration
#End Region




End Module
