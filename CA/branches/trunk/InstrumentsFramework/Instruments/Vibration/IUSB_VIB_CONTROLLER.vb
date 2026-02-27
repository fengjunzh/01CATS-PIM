Public Interface IUSB_VIB_CONTROLLER
	Function Open() As Boolean
    Sub StartFixture()
    Sub StopFixture()
    Sub OpenClamp()
    Sub CloseClamp()
    Sub Close()
    Function WatchFixtureMoving() As Boolean
    Function FixtureReady2Go() As Boolean
    Function FixtureRunComplete() As Boolean
End Interface
