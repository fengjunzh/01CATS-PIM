Imports AndrewIntegratedProducts.InstrumentsFramework
Imports Sealevel
Public Class VibController_SeaDACLite8112x86
    Inherits Instrument
    Implements IUSB_VIB_CONTROLLER

    Private SeaMAX_DeviceHandler As New SeaMAX()

    Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close
        Try
            SeaMAX_DeviceHandler.SDL_Cleanup()
            SeaMAX_DeviceHandler.SM_Close()
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.Close()::" & ex.Message)
        End Try
    End Sub

    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Try
            'initialize the SDL portion of the API
            SeaMAX_DeviceHandler.SDL_Initialize()
            'search for SDL devices
            Dim deviceCount As Integer = SeaMAX_DeviceHandler.SDL_SearchForDevices()

            If deviceCount < 0 Then
                Throw New Exception("Error " & deviceCount.ToString() & " while searching for SDL Devices.")
            End If

            'select the first device
            Dim errno As Integer = SeaMAX_DeviceHandler.SDL_FirstDevice()
            If errno < 0 Then
                Throw New Exception("Error selecting first device.")
            End If

            errno = SeaMAX_DeviceHandler.SDL_GetName(Name)
            If errno < 0 Then
                Throw New Exception("Error acquiring device name.")
            End If

            SeaMAX_DeviceHandler.SM_Open(Name)

            If SeaMAX_DeviceHandler.IsSeaMAXOpen Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.Open()::" & ex.Message)
        End Try
    End Function


    Private Sub IUSB_VIB_CONTROLLER_StartFixture() Implements IUSB_VIB_CONTROLLER.StartFixture
        Try
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {1}
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error starting fixture.")
            End If
            Threading.Thread.Sleep(200)
            SLR(0) = 16
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error starting fixture.")
            End If
            Threading.Thread.Sleep(200)


        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.StartFixture()::" & ex.Message)
        End Try
    End Sub

    Private Sub IUSB_VIB_CONTROLLER_StopFixture() Implements IUSB_VIB_CONTROLLER.StopFixture
        Try
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {2}
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error stopping fixture.")
            End If
            Threading.Thread.Sleep(200)
            SLR(0) = 16
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error stopping fixture.")
            End If
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.StopFixture()::" & ex.Message)
        End Try
    End Sub

    Private Sub IUSB_VIB_CONTROLLER_OpenClamp() Implements IUSB_VIB_CONTROLLER.OpenClamp
        Try
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {4}
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error opening clamp.")
            End If
            Threading.Thread.Sleep(200)
            SLR(0) = 16
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error opening clamp.")
            End If
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.OpenClamp()::" & ex.Message)
        End Try
    End Sub

    Private Sub IUSB_VIB_CONTROLLER_CloseClamp() Implements IUSB_VIB_CONTROLLER.CloseClamp
        Try
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {8}
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error closing clamp.")
            End If
            Threading.Thread.Sleep(200)
            SLR(0) = 16
            errno = SeaMAX_DeviceHandler.SM_WriteDigitalOutputs(0, 4, SLR)
            If errno < 0 Then
                Throw New Exception("Error closing clamp.")
            End If
            Threading.Thread.Sleep(200)
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.CloseClamp()::" & ex.Message)
        End Try
    End Sub

    Public Function WatchFixtureMoving() As Boolean Implements IUSB_VIB_CONTROLLER.WatchFixtureMoving
        Try
            Return True
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {0}
            Dim i As Integer
            Do
                errno = SeaMAX_DeviceHandler.SM_ReadDigitalInputs(0, 4, SLR)
                If errno < 0 Then
                    Throw New Exception("Error watching fixture moving.")
                End If
                If SLR(0) = 1 Then Return True
                i += 1
                Threading.Thread.Sleep(500)
            Loop Until (i > 10)
            Return False
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.WatchFixtureMoving()::" & ex.Message)
        End Try
    End Function

    Public Function FixtureReady2Go() As Boolean Implements IUSB_VIB_CONTROLLER.FixtureReady2Go
        Try
            'Return True
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {0}
            Dim i As Integer
            Do
                errno = SeaMAX_DeviceHandler.SM_ReadDigitalInputs(0, 4, SLR)
                If errno < 0 Then
                    Throw New Exception("Error fixture ready to go.")
                End If
                If SLR(0) = 2 Then Return True
                i += 1
                Threading.Thread.Sleep(500)
            Loop Until (i > 10)
            Return False
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.FixtureReady2Go()::" & ex.Message)
        End Try
    End Function

    Public Function FixtureRunComplete() As Boolean Implements IUSB_VIB_CONTROLLER.FixtureRunComplete
        Try
            'Return True
            Dim errno As Integer

            Dim SLR As Byte() = New Byte() {0}
            Dim i As Integer
            Do
                errno = SeaMAX_DeviceHandler.SM_ReadDigitalInputs(0, 4, SLR)
                If errno < 0 Then
                    Throw New Exception("Error fixture run complete.")
                End If
                If SLR(0) = 2 Then Return True
                i += 1
                Threading.Thread.Sleep(500)
            Loop Until (i > 10)
            Return False
        Catch ex As Exception
            Throw New Exception("VibController_SeaDACLite8112x86.FixtureRunComplete()::" & ex.Message)
        End Try
    End Function
End Class
