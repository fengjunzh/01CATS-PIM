Public Class AisgDevice
  Public Enum DeviceType
    SingleRet = 0
    MultiRet = 1
  End Enum

  Private Shared pAisgDevice As an3gppxLib.shell
  Private _AddrDev As String
  Private _RetCount As Byte = 10
  Public Structure stDeviceInfo
    'Dim Model As String
    'Dim AntennaModel As String
    Dim Firmware As String
    Dim Hardware As String
    Dim SerialNumber As String
    Dim DevType As DeviceType
    Dim RetSubNumber As Byte
  End Structure
  Public Sub New(devAddress As String)
    _AddrDev = devAddress
    pAisgDevice = New an3gppxLib.shell
  End Sub
  Public Property DefaultRetCount() As Byte
    Set(value As Byte)
      _RetCount = value
    End Set
    Get
      Return _RetCount
    End Get
  End Property
  Public Function OpenDevice() As Boolean
    Try

      Return pAisgDevice.OpenPort(_AddrDev)

    Catch ex As Exception
      Throw New Exception("AisgDevice::OpenDevice()" & ex.Message)
    End Try
  End Function
  Public Function ScanDevice() As Int16
    Try
      Dim i As Int16

      pAisgDevice.ScanDevice(_RetCount, 1)

      For i = _RetCount To 1 Step -1
        If pAisgDevice.IsConnected(i) = True Then Exit For
      Next

      Return i

    Catch ex As Exception
      Throw New Exception("AisgDevice::ScanDevice()" & ex.Message)
    End Try
  End Function
  Public ReadOnly Property GetDeviceInfo(addrRet As Byte) As stDeviceInfo
    Get
      Try
        Dim ret As String
        Dim szRet() As String
        Dim tmp As stDeviceInfo
        'Dim dev As an3gppxLib.DeviceData

        If pAisgDevice.IsConnected(addrRet) = False Then Throw New Exception(" ret address < " & addrRet & " > not connected")

        ret = pAisgDevice.GetInfo(addrRet)
        szRet = Split(ret, ";")
        'tmp.Model = szRet(0)
        tmp.Hardware = szRet(2)
        tmp.Firmware = szRet(3)

        ret = pAisgDevice.GetDeviceType(addrRet)
        If ret.Trim.ToUpper = "Single-Ret".ToUpper Then
          tmp.DevType = DeviceType.SingleRet

          'dev = pAisgDevice.SingleRet_DeviceData(addrRet)

          tmp.RetSubNumber = 1
        ElseIf ret.Trim.ToUpper = "Multi-Ret".ToUpper Then
          tmp.DevType = DeviceType.MultiRet
          tmp.RetSubNumber = pAisgDevice.MultiRet_GetNumberOfAntenna(addrRet)
        End If

        tmp.SerialNumber = pAisgDevice.GetSerialNumber(addrRet)

        Return tmp

      Catch ex As Exception
        Throw New Exception("AisgDevice::GetDeviceInfo()" & ex.Message)
      End Try
    End Get
  End Property
  Public ReadOnly Property GetSingleDeviceInfo(addrRet As Byte) As an3gppxLib.DeviceData
    Get
      Dim dev As an3gppxLib.DeviceData

      dev = pAisgDevice.SingleRet_DeviceData(addrRet)

      Return dev

    End Get
  End Property
  Public ReadOnly Property GetMultiDeviceInfo(addrRet As Byte, subAddr As Byte) As an3gppxLib.DeviceData
    Get
      Dim dev As an3gppxLib.DeviceData

      dev = pAisgDevice.MultiRet_DeviceData(addrRet, subAddr)

      Return dev

    End Get
  End Property
  Public Function CheckOnLine(addr As Short) As Boolean
    Try
      Return pAisgDevice.IsConnected(addr)
    Catch ex As Exception
      Return False
    End Try

  End Function
  Public Function CloseDevice() As Boolean
    Try
      pAisgDevice.ClosePort()
      Return True
    Catch ex As Exception
      Throw New Exception("AisgDevice::CloseDevice()" & ex.Message)
    End Try
  End Function
  Public Class Antenna
    Private _RetType As DeviceType
    Private _Addr As Byte
    Private _SubAddr As Byte
    Private _MinTilt As Single
    Private _MaxTilt As Single
    Private _CurrentTilt As Single
    'Private _AntennaModel As String
    Private _RetPN As String
    Public Sub New(retType As DeviceType, addr As Byte, subAddr As Byte)
      Try
        _RetType = retType
        _Addr = addr
        _SubAddr = subAddr

        _MinTilt = Me.GetMinTilt
        _MaxTilt = Me.GetMaxTilt

        If _RetType = DeviceType.SingleRet Then
          _CurrentTilt = pAisgDevice.GetTilt(_Addr)
          _RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
        ElseIf _RetType = DeviceType.MultiRet Then
          _CurrentTilt = pAisgDevice.MultiRet_GetTilt(_Addr, _SubAddr)
          _RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
        Else
          Throw New Exception("Antenna type is not match with Single/Multi")
        End If
      Catch ex As Exception
        Throw New Exception("AisgDevice::New()" & ex.Message)
      End Try
    End Sub
    Public ReadOnly Property Addr() As Byte
      Get
        Return _Addr
      End Get
    End Property
    Public ReadOnly Property SubAddr() As Byte
      Get
        Return _SubAddr
      End Get
    End Property
    Public Function SetTilt(degree As Single) As Boolean
      Try
        If degree < Me._MinTilt Or degree > Me._MaxTilt Then
          Throw New Exception("degree <" & degree & "> is out of range " & Me._MinTilt & " -> " & Me._MaxTilt)
        End If

        If _RetType = DeviceType.SingleRet Then
          If _CurrentTilt <> degree Then
            If pAisgDevice.SetTilt(_Addr, degree) = True Then
              _CurrentTilt = degree
              Return True
            Else
              Return False
            End If
          Else
            Return True
          End If
        ElseIf _RetType = DeviceType.MultiRet Then
          If _CurrentTilt <> degree Then
            If pAisgDevice.MultiRet_SetTilt(_Addr, _SubAddr, degree) = True Then
              _CurrentTilt = degree
              Return True
            Else
              Return False
            End If
          Else
            Return True
          End If
        End If

        Return False

      Catch ex As Exception
        Throw New Exception("AisgDevice::SetTilt()" & ex.Message)
      End Try
    End Function
    Public Function GetCurrentTilt() As Single
      Try

        Return _CurrentTilt

      Catch ex As Exception
        Throw New Exception("AisgDevice::GetCurrentTilt()" & ex.Message)
      End Try
    End Function
    Public ReadOnly Property MaxTilt() As Single
      Get
        Return _MaxTilt
      End Get
    End Property
    Public ReadOnly Property MinTilt() As Single
      Get
        Return _MinTilt
      End Get
    End Property
    Public ReadOnly Property RetPartNumber() As String
      Get
        Return _RetPN
      End Get
    End Property
    Private Function GetMaxTilt() As Single
      Try
        If _RetType = DeviceType.SingleRet Then
          Return pAisgDevice.GetMaxTilt(_Addr)
        ElseIf _RetType = DeviceType.MultiRet Then
          Return pAisgDevice.MultiRet_GetMaxTilt(_Addr, _SubAddr)
        End If

        Return -1

      Catch ex As Exception
        Throw New Exception("AisgDevice::GetMaxTilt()::" & ex.Message)
      End Try
    End Function
    Private Function GetMinTilt() As Single
      Try
        If _RetType = DeviceType.SingleRet Then
          Return pAisgDevice.GetMinTilt(_Addr)
        ElseIf _RetType = DeviceType.MultiRet Then
          Return pAisgDevice.MultiRet_GetMinTilt(_Addr, _SubAddr)
        End If

        Return -1

      Catch ex As Exception
        Throw New Exception("AisgDevice::GetMinTilt()::" & ex.Message)
      End Try
    End Function
  End Class
End Class
