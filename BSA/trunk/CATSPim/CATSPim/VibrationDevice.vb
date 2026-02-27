Imports CATSPIM

Public Interface IVibrationDevice
    'Property DevAddress As String
    Function OpenDev(ByVal COMPORT As String) As Boolean
    Function CloseDev() As Boolean
	Function StartVib() As Boolean
    Function StopVib() As Boolean
    Function OpenRelay(num As Short)
    Function CloseRelay(num As Short)
    Function ReadInput(ByVal InputNo As Integer) As Boolean
    Function ReadInputForChamberID() As Integer

End Interface
Public Class VibCtrl
	Implements IVibrationDevice
	Private m_address As String
	'Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX86
	Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.IUSB_VIB_CONTROLLER
    Private m_blnVib As Boolean = False
    Private Sel8222 As Boolean = False
    Public Enum VibJsbBoard
		JSB31
        JSB336
        SEALEVEL8222
    End Enum
    Public Sub New(JsbBoard As VibJsbBoard)

        Sel8222 = False

        Try

            If JsbBoard = VibJsbBoard.JSB31 Then
                Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_Jsb31x86
                m_dev = dev
            ElseIf JsbBoard = VibJsbBoard.JSB336 Then
                Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibController_Jsb336x86v1
                m_dev = dev
            ElseIf JsbBoard = VibJsbBoard.SeaLevel8222 Then
                Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.SeaLevel_8222
                m_dev = dev
                Sel8222 = True
            End If

        Catch ex As Exception
            Throw New Exception("VibCtrlx86.New()::" & ex.Message)
        End Try
    End Sub
    Public Function StartVib() As Boolean Implements IVibrationDevice.StartVib
		Try
            m_blnVib = m_dev.StartVIB
            '   StartKnockingThread()  这是敲击测试，目前ASZ和印度不用了，现先取消，将来需要只要激活这行就可以， 20191011.

            '振动确认,GOA
            If pAppCfg.GetChamberVibrationCheck.Enable Then If Check_PIMChamberVibration() = False Then Throw New Exception("  Chamber Vibration Error ,Please check!") ' ' add by tony for monitor door  

            Return m_blnVib

        Catch ex As Exception
			Throw New Exception("VibCtrlx86.StartVib()::" & ex.Message)
		End Try
	End Function

	Public Function StopVib() As Boolean Implements IVibrationDevice.StopVib
		Try
            m_blnVib = False
            Threading.Thread.Sleep(500)
            m_dev.StopVIB()
            Threading.Thread.Sleep(1000)
            Return True
		Catch ex As Exception
			Throw New Exception("VibCtrlx86.StopVib()::" & ex.Message)
		End Try
	End Function
	Private Delegate Sub delKnocking()
	Private Sub StartKnockingThread()
		Try
			Dim delK As New delKnocking(AddressOf SequenceKnocking)
			delK.BeginInvoke(Nothing, Nothing)

		Catch ex As Exception
			Throw New Exception("VibCtrlx86.StartKnockingThread()::" & ex.Message)
		End Try
	End Sub
	Private Sub KnockOnce(bitNum As Integer)
		Try
            If m_blnVib = False Then Return

            If m_blnVib Then m_dev.CloseRelay(bitNum)
            If m_blnVib Then Threading.Thread.Sleep(300)
            If m_blnVib Then Threading.Thread.Sleep(330)
            If m_blnVib Then Threading.Thread.Sleep(350)
            If m_blnVib Then m_dev.OpenRelay(bitNum)
            'Threading.Thread.Sleep(500)

        Catch ex As Exception
			Throw New Exception("VibCtrlx86.KnockOnce()::" & ex.Message)
		End Try
	End Sub
	Private Sub SequenceKnocking()
		Try

			Do While m_blnVib

                If m_blnVib Then KnockOnce(1)
                If m_blnVib Then KnockOnce(2)
                If m_blnVib Then KnockOnce(3)
                If m_blnVib Then KnockOnce(4)

            Loop

		Catch ex As Exception
			Throw New Exception("VibCtrlx86.SequenceKnocking()::" & ex.Message)
		End Try
	End Sub
    Private Function CloseDev() As Boolean Implements IVibrationDevice.CloseDev
        Try

            m_dev.Close()
            Return True

        Catch ex As Exception
            Throw New Exception("VibCtrl.CloseDev()::" & ex.Message)
            Return False
        End Try
    End Function

    Private Function OpenDev(ByVal COMPORT As String) As Boolean Implements IVibrationDevice.OpenDev
        Try
            If Sel8222 = True Then
                Return m_dev.Open(COMPORT) ' COM3
            Else
                Return m_dev.Open()
            End If

        Catch ex As Exception
            Throw New Exception("VibCtrl.OpenDev()::" & ex.Message)
            Return False
        End Try
    End Function

    Public Function OpenRelay(num As Short) As Object Implements IVibrationDevice.OpenRelay

		Try
			m_dev.OpenRelay(num)
			Return True
		Catch ex As Exception
			Throw New Exception("VibCtrl.OpenRelay()::" & ex.Message)
		End Try
	End Function

    Public Function CloseRelay(num As Short) As Object Implements IVibrationDevice.CloseRelay

        Try
            m_dev.CloseRelay(num)
            Return True
        Catch ex As Exception
            Throw New Exception("VibCtrl.CloseRelay()::" & ex.Message)
        End Try
    End Function


    'Read Input add by tony============================================================================
    Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IVibrationDevice.ReadInput '目前没有用，没有导入门监控功能

        Try
            Return m_dev.ReadInput(InputNo)
        Catch ex As Exception
            Throw New Exception("VibCtrl.ReadInput()::InputNo" & ex.Message)
        End Try
    End Function

    'add 20190228 tony
    Public Function ReadInputForChamberID() As Integer Implements IVibrationDevice.ReadInputForChamberID

        Try
            Return m_dev.ReadInputForChamberID()
        Catch ex As Exception
            Throw New Exception("VibCtrl.ReadInput()::" & ex.Message)
        End Try
    End Function



End Class
