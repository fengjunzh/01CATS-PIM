Imports CATSPIM

Public Interface IVibrationDevice
  'Property DevAddress As String
  Function OpenDev() As Boolean
  Function CloseDev() As Boolean
  Function StartVib() As Boolean
  Function StopVib() As Boolean

End Interface
Public Class VibCtrl
  Implements IVibrationDevice

  Private m_address As String
  'Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX86
  Private m_dev As AndrewIntegratedProducts.InstrumentsFramework.IUSB_VIB_CONTROLLER
  Public Sub New()

  End Sub
  'Public Property DevAddress As String Implements IVibrationDevice.DevAddress
  '  Get
  '    'Return m_address
  '  End Get
  '  Set(value As String)
  '    'm_dev.Address = value
  '    'm_address = value
  '  End Set
  'End Property
  Public Function StartVib() As Boolean Implements IVibrationDevice.StartVib
    Try
      Return m_dev.StartVIB

    Catch ex As Exception
      Throw New Exception("VibCtrlx86.StartVib()::" & ex.Message)
    End Try
  End Function

  Public Function StopVib() As Boolean Implements IVibrationDevice.StopVib
    Try
      m_dev.StopVIB()
      Return True
    Catch ex As Exception
      Throw New Exception("VibCtrlx86.StopVib()::" & ex.Message)
    End Try
  End Function

  Private Function CloseDev() As Boolean Implements IVibrationDevice.CloseDev
    Return True
  End Function

  Private Function OpenDev() As Boolean Implements IVibrationDevice.OpenDev
    Try

      'If Environment.Is64BitOperatingSystem Then
      '  Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX64Bit
      '  dev.Open()
      '  m_dev = dev
      '  Return True
      'Else
      Dim dev As New AndrewIntegratedProducts.InstrumentsFramework.VibrationControllerX86
      dev.Open()
      m_dev = dev
      Return True
      'End If

      Return False
      'Return m_dev.StartVIB
    Catch ex As Exception
      Throw New Exception("VibCtrl.OpenDev()::" & ex.Message)
    End Try
  End Function
End Class
