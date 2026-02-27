Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic
Module Common

	'This line is necessary so that the modules within this project can access the
	'functions in the "Globals" class.  I named the variable the same as the class
	'(suggested by MS Help) to make it clear in the code where the functions come
	'from.
	Public Globals_Renamed As New clsGlobals

	Public Const NL As String = vbNewLine

	Public TestObj As ITestPlan
	Public GUI As OperatorInterface
	Public TestGroups As New Collection
	Public pResults As clsTestResults
	Public pCheck As clsProcessCheck

	Public Testset_ID As String
	Public Location As String

	Public INI_File As String
	Public LimitsFile As String

	Public UUT_Type As String
	Public SW_Version As String
	Public MTR_Version As String
	Public EstimatedDuration As Integer

	Public PassingUnits As Boolean
	Public FailingUnits As Boolean
	Public FailingGroups As Boolean

	Public PC_String As String
	Public PC_Status As String
	Public CelesticaBarcode As String

	Public AbortFlag As Boolean
	Public BarcodeMatch As String
	Public inDiagnosticMode As Boolean

	Public enableRepeat As Boolean
	Public inLabMode As Boolean
	Public labModeMsg As String

	Public SAPFailSafeModeOnOff As Boolean
	Public TestDoubleLength As Boolean
	Public gPlant As String
	Public gMiiStatus As Boolean
	Public gSelectedTestPhase As String
	Public gDatabase As String
	Public gInstrMode As String
	Public gDynamic As Boolean
	Public gRetryCountMax As SByte
	Public gRetryCountRest As SByte
	Public gTestCountMax As SByte
	Public gTestCountRest As SByte
	Public Const gLowPimLoadTestSn As String = "CALPLTEST123"
	Public Const gInputLowPimLoadTestSn As String = "ICLPLTEST123"
	Public gAutomation As Boolean
	Public gPretest As Boolean
	Public gStationBarcode As String
	Public gTrigger_PIM As Short
	Public gHeartBeatInt As Short
	Public gPimFixtureReady As Boolean
End Module