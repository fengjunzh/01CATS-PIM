Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports System.Threading
Imports System.Threading.Tasks

Public Interface ITestPlan

    'Public Event RunTest(ByVal Barcode As String, ByVal TestPhase As String, Cancel As Boolean)
    'Public Event RunTestGroup(ByVal TestGroup As String, ByVal Barcode As String, ByVal TestPhase As String)
    'Public Event PreTest(ByVal TestPhase As String, Barcode As String, Cancel As Boolean)
    'Public Event PostTest()
    'Public Event AbortTest()
    'Public Event ExitInterface(Cancel As Integer)
    'Public Event Calibrate(ByVal CalName As String)
    'Public Event Help(ByVal HelpName As String)
    'Public Event Troubleshoot(ByVal TSName As String)
    'Public Event PowerOff()
    Function InitInstrument() As Boolean
    Sub RunTest(ByVal Barcode As String, ByVal TestPhase As String, ByRef Cancel As Boolean)
    'template

    Sub PreTest(TestPhase As String)
    'template

    Sub PostTest()
    'template

    Sub AbortTest()
    'template

    Sub MenuClick(ByVal MenuName As String, ByRef ItemName As String)
    'template

    Sub ExitInterface(ByRef Cancel As Short)
    'template

    Sub RunTestGroup(ByVal TestGroup As String, ByVal Barcode As String, ByVal TestPhase As String)
    'template
    Function CheckSN(ByRef CA As CableAssembly, isSecondSn As Boolean) As Boolean
    Sub MenuPhaseStationClick(PhaseStation As Object)
    Function MenuModeClick(Mode As Object) As Boolean
    Function LoadSpec() As Boolean
    Function LoadPhaseStatus(SN As String, PN As String) As Boolean
    Sub LoadPimTestData(SN As String, PN As String, Phase As String)
    Function ReadScan() As String
    Sub ReadPlcAsync()
    Sub WritePlcAsync(values As Dictionary(Of String, Object))
    Function ConnectOpcUaAsync() As Task
    Sub ClosePimDevice()
End Interface