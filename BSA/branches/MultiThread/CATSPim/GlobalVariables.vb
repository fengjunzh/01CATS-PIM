Module GlobalVariables

	Public pGui As New RFPATestInterface.clsOperatorInterface
	Public pAppPath As String = "c:\cats\"
	Public pTestSystemPath As String = pAppPath & "test_system\"
	Public pTestResultPath As String = pAppPath & "test_result\"
	Public pAppIniFile As String = Application.StartupPath & "\CATSPim.ini"
	Public pAppCfgFile As String = pTestSystemPath & "CATSPimTestConfig.xml"
	Public pAppCfg As CATSPimConfig.LocalConfig
	Public pCatsCfgFile As String = pTestSystemPath & "CATS.xmls"

	Public pTestResult As New RFPATestInterface.clsTestResults
	Public pTestPlan As New TestPlan

	Public pFreqBoxes As New Dictionary(Of String, DataModels.FrequencyGroup)
	Public pRTGlobal As New DataModels.RTimeGlobalPara
	Public pAbortFlag As Boolean

	Public pMTDisplay As DelegateDisplay.MTFormTest

	Public pFactory As String
	Public pDbConnString As String

#Region "Instruments"

	'Public pPimDev As AndrewIntegratedProducts.InstrumentsFramework.IIMDDevice
	'Public pRetCtrl As AisgDevice
	Public pRetThread As ThreadRet
	Public pPromptThread As ThreadPrompt
	Public pVibCtrl As IVibrationDevice
#End Region




End Module
