Option Strict Off
Imports System
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class VibController_Jsb340x86
    Inherits Instrument
    Implements IUSB_VIB_CONTROLLER

    Private jwControl As New jwModuleApi
    Public serialNumber As String
    Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Dim nNumberOfModules As Integer = 0
        Try
            nNumberOfModules = jwControl.NumberOfModules
            If nNumberOfModules > 0 Then
                serialNumber = jwControl.SerialNumber(1)

                ' State
                ' True  - not affect other bits 
                ' False - the other bits will set to 0,and the RelayNumber will set 1
                'jwControlSetRelayState(g_strSerial, 8, True)

                'jsb34x.RelayOn(serialNumber, 1)

            Else
                Throw New Exception("Cannot find Vibration USB controller!")
            End If
            Return True
            'Catch exp As InvalidCastException
            '    Throw New Exception("Selected resource must be a message-based session")
        Catch exp As Exception
            Throw New Exception(String.Format("Got error message during opening address {0}:" +
            Environment.NewLine + "{1}" +
            Environment.NewLine + "SCPI string response: {2}_{3}", Me.Address, exp.Message, _ModelNumber, _FWRevision))
        End Try
    End Function

    Public Sub StartFixture() Implements IUSB_VIB_CONTROLLER.StartFixture
        Try
            jwControl.RelayOn(serialNumber, 1)
            Threading.Thread.Sleep(200)
            jwControl.RelayOff(serialNumber, 1)
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_Jsb340x86.CloseClamp()::" & ex.Message)
        End Try
    End Sub

    Public Sub StopFixture() Implements IUSB_VIB_CONTROLLER.StopFixture
        Try
            jwControl.RelayOn(serialNumber, 2)
            Threading.Thread.Sleep(200)
            jwControl.RelayOff(serialNumber, 2)
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_Jsb340x86.CloseClamp()::" & ex.Message)
        End Try
    End Sub

    Public Sub OpenClamp() Implements IUSB_VIB_CONTROLLER.OpenClamp
        Try
            jwControl.RelayOn(serialNumber, 4)
            Threading.Thread.Sleep(200)
            jwControl.RelayOff(serialNumber, 4)
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_Jsb340x86.OpenClamp()::" & ex.Message)
        End Try
    End Sub

    Public Sub CloseClamp() Implements IUSB_VIB_CONTROLLER.CloseClamp
        Try
            jwControl.RelayOn(serialNumber, 3)
            Threading.Thread.Sleep(200)
            jwControl.RelayOff(serialNumber, 3)
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_Jsb340x86.CloseClamp()::" & ex.Message)
        End Try
    End Sub
End Class

Public Class jwModuleApi
#Region "NumberOfModules"
    Public Function NumberOfModules() As Byte
        Return jwControl.Info.NumberOfModules()
    End Function
#End Region

#Region "SerialNumber"
    ' returns the serial number of module based on ModuleNumber
    ' ModuleNumber starts at 1 to the NumberOfModules
    ' If string is empty could not find module or some other type issue
    Public Function SerialNumber(ByVal ModuleNumber As Byte) As String
        Return jwControl.Info.SerialNumber(ModuleNumber)
    End Function
#End Region

#Region "Versions"

#Region "ClassVersion"
    Public Function VersionClass() As String
        Return jwControl.Info.ClassLibraryVersion()
    End Function
#End Region

#Region "DriverVersion"
    Public Function VersionDriver(ByVal serialNum As String) As String
        Return jwControl.Info.DriverVersion(serialNum)
    End Function
#End Region

#Region "FirmwareVersion"
    Public Function VersionFirmware(ByVal serialNum As String) As String
        Return jwControl.Info.FirmwareVersion(serialNum)
    End Function
#End Region


#End Region
#Region "PrivateId"
    Public Function PrivateId(ByVal serialNumber As String) As UInt32
        Return jwControl.Info.PrivateId(serialNumber)
    End Function
#End Region

#Region "Name"
    Public Function Name(ByVal serialNumber As String) As String
        Return jwControl.Info.ModuleName(serialNumber)
    End Function
#End Region

#Region "FlashLed"
    Public Sub Led(ByVal serialNumber As String)
        jwControl.Info.FlashLed(serialNumber)
    End Sub
#End Region

#Region "NumberOpto"
    Public Function NumberOptoIn(ByVal serialNumber As String) As Byte
        Return jwControl.Opto.NumberOfChannels(serialNumber)
    End Function
#End Region

#Region "NumberRelay"
    Public Function NumberRelay(ByVal serialNumber As String) As Byte
        Return jwControl.Relay.NumberOfChannels(serialNumber)
    End Function
#End Region


#Region "RelayState"

    Public Function IsRelayOn(ByVal serialNumber As String, ByVal relayNum As Byte) As Boolean
        Return jwControl.Relay.IsRelayOn(serialNumber, relayNum)
    End Function

    Public Function IsRelayOff(ByVal serialNumber As String, ByVal relayNum As Byte) As Boolean
        Return jwControl.Relay.IsRelayOff(serialNumber, relayNum)
    End Function

    Public Sub SetRelayState(ByVal serialNumber As String, ByVal relayNum As Byte, ByVal state As Boolean)
        jwControl.Relay.SetRelayState(serialNumber, relayNum, state)
    End Sub

    Public Sub RelayOff(ByVal serialNumber As String, ByVal relayNum As Byte)
        jwControl.Relay.SetRelayOff(serialNumber, relayNum)
    End Sub

    Public Sub RelayOn(ByVal serialNumber As String, ByVal relayNum As Byte)
        jwControl.Relay.SetRelayOn(serialNumber, relayNum)
    End Sub

#End Region

#Region "RelayPattern"
    Public Sub SetRelayBits(ByVal serialNumber As String, ByVal pattern As UInt64)
        jwControl.Relay.SetRelayBitPattern(serialNumber, pattern)
    End Sub

    Public Function RelayBits(ByVal serialNumber As String)
        Return jwControl.Relay.RelayBitPattern(serialNumber)
    End Function
#End Region

#Region "OptoState"
    Public Function OptoState(ByVal serialNumber As String, ByVal bitNum As Byte) As Boolean
        Return jwControl.Opto.InputChannelState(serialNumber, bitNum)
    End Function
#End Region

#Region "OptoPattern"
    Public Function OptoBits(ByVal serialNumber As String) As UInt64
        Return jwControl.Opto.InputsBitPattern(serialNumber)
    End Function
#End Region

#Region "Adc"
    Public Function NumberOfAdc(ByVal SerialNumber As String) As Byte
        Return jwControl.ADC.NumberOfChannels(SerialNumber)
    End Function

    Public Function AdcRawValue(ByVal SerialNumber As String, ByVal Channel As Byte) As UInt32
        Return jwControl.ADC.AdcRawValue(SerialNumber, Channel)
    End Function

    Public Function ReadAdcVoltage(ByVal SerialNumber As String, ByVal Channel As Byte) As Double
        Return jwControl.ADC.AdcVoltage(SerialNumber, Channel)
    End Function

    Public Function AdcRangeMin(ByVal SerialNumber As String, ByVal Channel As Byte) As Double
        Return jwControl.ADC.AdcRangeMin(SerialNumber, Channel)
    End Function

    Public Function AdcRangeMax(ByVal SerialNumber As String, ByVal Channel As Byte) As Double
        Return jwControl.ADC.AdcRangeMax(SerialNumber, Channel)
    End Function


#End Region

#Region "Wdt"

#Region "NumWdt"
    ' function uses api call (class library jwControl) to get
    ' the number of wdt on given modules. return is either 
    ' a 0 or 1  Serial number is obtain from a previous call
    ' to jwControl::Info::SerialNumber(ModuleNumber)
    Public Function NumberOfWdt(ByVal SerialNumber As String) As Byte

        Return jwControl.watchdog.NumberOfChannels(SerialNumber)

    End Function
#End Region

#Region "watchOnTime"
    ' function returns the Wdt on time. This is the amount of
    ' time the relay will stay in the time-out state. If the
    ' value is zero (0) relay stays in timeout state until
    ' timer is enabled again Value is 100msec intervals.
    'Serial number is obtain from a previous call
    ' to jwControl::Info::SerialNumber(ModuleNumber)
    Public Function WdtOnTime(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogRelayOnTime(SerialNumber)

    End Function
#End Region

#Region "watchdogResetTime"
    Public Function WdtResetTime(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogTime(SerialNumber)

    End Function
#End Region

#Region "watchdogDelayTime"
    Public Function WdtDelayTime(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogDelayTime(SerialNumber)

    End Function
#End Region


#Region "watchdogTimeLeft"
    Public Function WdtTimeLeft(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogTimeLeft(SerialNumber)

    End Function
#End Region

#Region "watchdogDelayLeft"
    Public Function WdtDelayLeft(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogPwrDelayLeft(SerialNumber)

    End Function
#End Region

#Region "watchdogRelayLeft"
    Public Function WdtRelayLeft(ByVal SerialNumber As String) As UInt16

        Return jwControl.watchdog.watchdogRelayLeft(SerialNumber)

    End Function
#End Region



#Region "watchdogSave"
    Public Sub WdtSave(ByVal SerialNumber)

        jwControl.watchdog.SaveSettings(SerialNumber)

    End Sub
#End Region

#Region "watchdogEnableTimer"
    Public Sub WdtEnableTimer(ByVal SerialNumber As String)

        jwControl.watchdog.Enable(SerialNumber, True)

    End Sub

#End Region

#Region "watchdogResetTimer"
    Public Sub WdtResetTimer(ByVal SerialNumber As String)

        jwControl.watchdog.Reset(SerialNumber)

    End Sub

#End Region

#Region "WatchSetPattern"
    Public Sub watchSetPattern(ByVal SerialNumber As String, ByVal pwrOn As Byte, ByVal enable As Byte, ByVal timeOut As Byte)
        jwControl.watchdog.watchdogSetRelayPatterns(SerialNumber, pwrOn, enable, timeOut, 0)
    End Sub

#End Region

#Region "WatchPwrPattern"
    Public Function watchPwrPattern(ByVal SerialNumber As String) As Byte
        Return jwControl.watchdog.readPwrOnPattern(SerialNumber)
    End Function
#End Region


#Region "WatchEnablePattern"
    Public Function watchEnablePattern(ByVal SerialNumber As String) As Byte
        Return jwControl.watchdog.readEnablePattern(SerialNumber)

    End Function

#End Region

#Region "WatchTimeoutPattern"

    Public Function watchTimeoutPattern(ByVal SerialNumber As String) As Byte
        Return jwControl.watchdog.readTimeoutPattern(SerialNumber)
    End Function


#End Region

#Region "WatchNumberOfRelays"
    Public Function watchNumberOfRelays(ByVal SerialNumber As String) As Byte
        Return jwControl.watchdog.numberOfRelays(SerialNumber)
    End Function
#End Region

#Region "watchdogSetTimes"
    Public Sub WdtSetTimes(ByVal SerialNumber As String, ByVal ResetTime As UInt16, ByVal OnTime As UInt16, ByVal DelayTime As UInt16)

        jwControl.watchdog.watchdogSetTimes(SerialNumber, ResetTime, OnTime, DelayTime)

    End Sub
#End Region

#End Region

#Region "DIO"
    Public Sub DioSetOutByNumber(ByVal SerialNumber As String, ByVal bitNumber As Byte, ByVal state As Boolean)

        jwControl.DIO.SetBitByNumber(SerialNumber, bitNumber, state)
    End Sub

    Public Sub DioSetDirByNumber(ByVal SerialNumber As String, ByVal bitNumber As Byte, ByVal state As Boolean)

        jwControl.DIO.SetDirByNumber(SerialNumber, bitNumber, state)
    End Sub

#Region "DIONUM"

    Public Function NumberOfDio(ByVal SerialNumber As String) As Byte

        Return jwControl.DIO.NumberOfDigitalIO(SerialNumber)
    End Function

    Public Function NumberOfDigitalInputOnly(ByVal SerialNumber As String) As Byte

        Return jwControl.DIO.NumberOfDigitalInput(SerialNumber)
    End Function

    Public Function NumberOfDigitalOutputOnly(ByVal SerialNumber As String) As Byte

        Return jwControl.DIO.NumberOfDigitalOutput(SerialNumber)
    End Function

#End Region

#Region "DIODIR"
    Public Sub DioSetDirectionPattern(ByVal SerialNumber As String, ByVal Pattern As UInt64)

        jwControl.DIO.SetDirPattern(SerialNumber, Pattern)
    End Sub

    Public Function DioReadDirectionPattern(ByVal SerialNumber As String) As UInt64

        Return jwControl.DIO.GetDirectionPattern(SerialNumber)
    End Function

#End Region

    Public Sub DioSetOutputPattern(ByVal SerialNumber As String, ByVal Pattern As UInt64)

        jwControl.DIO.SetOutPattern(SerialNumber, Pattern)
    End Sub


    Public Function DioReadInputPattern(ByVal SerialNumber As String) As UInt64

        Return jwControl.DIO.GetInputPattern(SerialNumber)
    End Function

    Public Function DioReadOutputPattern(ByVal SerialNumber As String) As UInt64

        Return jwControl.DIO.GetOutputPattern(SerialNumber)
    End Function

    Public Sub DioSaveToEprom(ByVal SerialNumber As String)

        jwControl.DIO.SaveSettingsToFlash(SerialNumber)

    End Sub
#End Region
End Class


