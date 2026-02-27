Public Interface IUSB_VIB_CONTROLLER
    Function Open() As Boolean
    Function Open(ByVal COMPort As String) As Boolean
    Function StartVIB() As Boolean
	Sub StopVIB()

	Function OpenRelay(num As Short) As Boolean

    Function CloseRelay(num As Short) As Boolean

    Function ReadInput(ByVal InputNo As Integer) As Boolean
    Function ReadInputForChamberID() As Integer
    Sub Close()
End Interface
