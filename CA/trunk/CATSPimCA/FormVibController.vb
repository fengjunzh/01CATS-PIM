
Public Class FormVibController
    Dim jwModule As jwModuleAPI  ' a class that is one of many ways to communicate with class library
    Dim selectedSerialNum As String
    Private Sub ScanForModules()
        Dim NumberOfModules As Byte

        listBoxSerialNumbers.Items.Clear()

        NumberOfModules = jwModule.NumberOfModules()  'find out how many modules are attached
        textBoxNumOfModules.Text = NumberOfModules.ToString()
        ' now cycle thru all the modules and fill up serial number list box
        ' serial numbers are used to access a module in a unique manner
        ' if an empty string is passed, then the first module that the usb stack detects is used
        ' modules start with module 1
        For ModuleNumber As Byte = 1 To NumberOfModules
            listBoxSerialNumbers.Items.Add(jwModule.SerialNumber(ModuleNumber))
        Next
        If (NumberOfModules > 0) Then
            listBoxSerialNumbers.SetSelected(0, True)
        End If

    End Sub 'ScanForModules

    Private Sub UpdateModuleInfo()
        groupBoxModuleInfo.Text = "Info for s/n " + selectedSerialNum
        labelClassLibVersion.Text = jwModule.VersionClass()
        labelDriverVersion.Text = jwModule.VersionDriver(selectedSerialNum)
        labelFirwareVersion.Text = jwModule.VersionFirmware(selectedSerialNum)
        labelModuleName.Text = jwModule.Name(selectedSerialNum)
        LabelId.Text = jwModule.PrivateId(selectedSerialNum)

        EnableRelayDisplay()

    End Sub
    Private Sub EnableRelayDisplay()
        Dim numRelays As Byte

        numRelays = jwModule.NumberRelay(selectedSerialNum)
        If numRelays > 0 Then
            checkedListBoxRelays.Show()
            groupBoxRelay.Show()
            checkedListBoxRelays.Items.Clear()
            For relayNum As Byte = 1 To numRelays
                checkedListBoxRelays.Items.Add("Relay " + relayNum.ToString())
            Next
            groupBoxRelay.Text = "Number of Relays is " + numRelays.ToString()
        Else
            checkedListBoxRelays.Hide()
            groupBoxRelay.Hide()
        End If
    End Sub
    Private Sub FormVibController_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' create class for communicating with modules, as long as the class library functions are
        ' called, these calls can be done in other ways, like in-line directly
        jwModule = New jwModuleAPI
        ScanForModules()
    End Sub
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub checkedListBoxRelays_SelectedIndexChanged(sender As Object, e As EventArgs) Handles checkedListBoxRelays.SelectedIndexChanged
        Dim checkIndex As Integer
        checkIndex = checkedListBoxRelays.SelectedIndex
        Dim state As Boolean
        state = checkedListBoxRelays.GetItemChecked(checkIndex)

        jwModule.SetRelayState(selectedSerialNum, (checkIndex + 1), state)

        Dim relayBitValue As UInt64
        relayBitValue = jwModule.RelayBits(selectedSerialNum)
    End Sub

    Private Sub buttonLed_Click(sender As Object, e As EventArgs) Handles buttonLed.Click
        jwModule.Led(selectedSerialNum)
    End Sub

    Private Sub listBoxSerialNumbers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listBoxSerialNumbers.SelectedIndexChanged
        selectedSerialNum = jwModule.SerialNumber(listBoxSerialNumbers.SelectedIndex + 1)

        UpdateModuleInfo()
    End Sub

    Private Sub buttonRefresh_Click(sender As Object, e As EventArgs) Handles buttonRefresh.Click
        ScanForModules()
    End Sub
End Class