Public Interface IUSB_VIB_CONTROLLER
	Function Open() As Boolean
    Sub StartFixture()
    Sub StopFixture()
    Sub OpenClamp()
    Sub CloseClamp()
    Sub Close()
End Interface
