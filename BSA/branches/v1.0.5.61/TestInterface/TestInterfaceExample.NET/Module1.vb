Option Strict Off
Option Explicit On
Module Module1
	
	'this module contains some global variables
    Public GUI As New RFPATestInterface.clsOperatorInterface
    Public MyTestPlan As New clsTestPlan
    Public AbortFlag As Boolean
    Friend RFPATestInterfaceclsGlobals_definst As New RFPATestInterface.clsGlobals


    Public Sub MyDelay(ByVal msec As Integer)
        If AbortFlag Then
            Call GUI.RecordPassFail("Operator_Abort", False)
            Call Err.Raise(vbObjectError + 1, , "Test aborted by operator")
            AbortFlag = False
        End If
        Call RFPATestInterfaceclsGlobals_definst.Delay(msec)
    End Sub
End Module