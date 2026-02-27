Option Strict Off
Option Explicit On
Imports System.Collections.Generic
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

    Sub RunTest(ByVal Barcode As String, ByVal TestPhase As String, ByRef Cancel As Boolean)
    'template

    Sub PreTest(ByVal TestPhase As String, ByRef Barcode As String, ByRef Cancel As Boolean)
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
    Function CheckSN(bkCable As Cable, lastTestLength As Decimal) As Boolean
    Function CheckSN(ByVal SN As String) As Cable
    Sub MenuPhaseStationClick(PhaseStation As Object)
    Function MenuModeClick(Mode As Object) As Boolean
    Function LoadSpec() As Boolean
    Function LoadPhaseStatus(SN As String, PN As String) As Boolean
    Function LoadGroupStatus() As Boolean
    Function LoadMeasDetail() As Boolean
    Function LoadPimPlot() As Boolean
    Function LoadPhaseExtendCable(SN As String, PN As String) As frmNewCable.MeasPhaseExtendCable
    Function GetDbCableInput(SN As String) As frmNewCable.CableInput
    Function LoadAllCableType() As List(Of String)
    Function LoadTestConnector(CableType As String) As List(Of String)
End Interface