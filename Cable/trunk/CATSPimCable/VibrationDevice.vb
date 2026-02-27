Imports CATSPIM

Public Interface IVibrationDevice
    Function Open() As Boolean
    Sub Close()
    Sub StartFixture()
    Sub StopFixture()
    Sub OpenClamp()
    Sub CloseClamp()
End Interface
Public Class VibCtrl
	Implements IVibrationDevice
    Private m_address As String
    Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.IUSB_VIB_CONTROLLER
    Public Enum VibCtrlBoard
        JSB340
        SEADACLITE_8112
    End Enum
    Public Sub New(JsbBoard As VibCtrlBoard)
        Try
            Select Case JsbBoard
                Case VibCtrlBoard.JSB340
                    Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_Jsb340x86
                    m_dev = dev
                Case VibCtrlBoard.SEADACLITE_8112
                    Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_SeaDACLite8112x86
                    m_dev = dev
            End Select
        Catch ex As Exception
            Throw New Exception("VibCtrl.New()::" & ex.Message)
        End Try
    End Sub

    Public Sub StartFixture() Implements IVibrationDevice.StartFixture
        Try
            m_dev.StartFixture()
        Catch ex As Exception
            Throw New Exception("VibCtrl.StartFixture()::" & ex.Message)
        End Try
    End Sub

    Public Sub StopFixture() Implements IVibrationDevice.StopFixture
        Try
            m_dev.StopFixture()
        Catch ex As Exception
            Throw New Exception("VibCtrl.StopFixture()::" & ex.Message)
        End Try
    End Sub

    Private Sub CloseDev() Implements IVibrationDevice.Close
        Try
            m_dev.Close()
        Catch ex As Exception
            Throw New Exception("VibCtrl.Close()::" & ex.Message)
        End Try
    End Sub

    Private Function OpenDev() As Boolean Implements IVibrationDevice.Open
        Try
            Return m_dev.Open()
        Catch ex As Exception
            Throw New Exception("VibCtrl.Open()::" & ex.Message)
        End Try
    End Function

    Public Sub OpenClamp() Implements IVibrationDevice.OpenClamp

        Try
            m_dev.OpenClamp()
        Catch ex As Exception
            Throw New Exception("VibCtrl.OpenClamp()::" & ex.Message)
        End Try
    End Sub

    Public Sub CloseRelay() Implements IVibrationDevice.CloseClamp

        Try
            m_dev.CloseClamp()
        Catch ex As Exception
            Throw New Exception("VibCtrl.CloseRelay()::" & ex.Message)
        End Try
    End Sub
End Class
