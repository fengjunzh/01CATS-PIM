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
	Public Function CheckOnLine() As Boolean
		Try
			Return CheckOnLine(1)
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
	Public Function GetPartNumber(addr As String) As String
		Try
			Return pAisgDevice.GetInfo(addr).Split(";")(0)
		Catch ex As Exception
			Throw New Exception("AisgDevice::GetPartNumber()" & ex.Message)
		End Try
	End Function
	Public Class Antenna
		Private _RetType As DeviceType
		Private _Addr As Byte
		Private _SubAddr As Byte
		Private _MinTilt As Decimal
		Private _MaxTilt As Decimal
		'Private _CurrentTilt As Decimal
		'Private _AntennaModel As String
		Private _RetPN As String
		Public Sub New(retType As DeviceType, addr As Byte, subAddr As Byte)
			Try
				_RetType = retType
				_Addr = addr
				_SubAddr = subAddr

				If _RetType = DeviceType.SingleRet Then
					_MinTilt = pAisgDevice.GetMinTilt(_Addr)
					_MaxTilt = pAisgDevice.GetMaxTilt(_Addr)
					_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
				ElseIf _RetType = DeviceType.MultiRet Then
					_MinTilt = pAisgDevice.MultiRet_GetMinTilt(_Addr, _SubAddr)
					_MaxTilt = pAisgDevice.MultiRet_GetMaxTilt(_Addr, _SubAddr)
					_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
				End If




				'If _RetType = DeviceType.SingleRet Then
				'	_CurrentTilt = pAisgDevice.GetTilt(_Addr)
				'	_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
				'ElseIf _RetType = DeviceType.MultiRet Then
				'	_CurrentTilt = pAisgDevice.MultiRet_GetTilt(_Addr, _SubAddr)
				'	_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
				'Else
				'	Throw New Exception("Antenna type is not match with Single/Multi")
				'End If
			Catch ex As Exception
				Throw New Exception("AisgDevice::New()::" & ex.Message)
			End Try
		End Sub
		'Public Sub InitializeTilt()
		'	Try
		'		_MinTilt = Me.GetMinTilt
		'		_MaxTilt = Me.GetMaxTilt

		'		If _RetType = DeviceType.SingleRet Then
		'			_CurrentTilt = pAisgDevice.GetTilt(_Addr)
		'			_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
		'		ElseIf _RetType = DeviceType.MultiRet Then
		'			_CurrentTilt = pAisgDevice.MultiRet_GetTilt(_Addr, _SubAddr)
		'			_RetPN = pAisgDevice.GetInfo(_Addr).Split(";")(0)
		'		Else
		'			Throw New Exception("Antenna type is not match with Single/Multi")
		'		End If

		'	Catch ex As Exception
		'		Throw New Exception("AisgDevice::InitializeTilt()::" & ex.Message)
		'	End Try
		'End Sub
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
		Public Sub SetTilt(degree As Single)
			Try
				Dim minTilt, maxTilt
				minTilt = GetMinTilt()
				maxTilt = GetMaxTilt()

				If degree < minTilt Or degree > maxTilt Then
					Throw New FailedException("target degree =" & degree & " is out of range " & minTilt & "->" & maxTilt)
				End If

				StartThreadSetTilt(degree)

				If Math.Abs(GetCurrentTilt() - degree) > 0.3 Then
					Throw New FailedException("target degree can not set to " & degree)
				End If
			Catch ex As Exception
				Throw New FailedException("SetTilt()::" & ex.Message)
			End Try
		End Sub
		Private Delegate Sub DelSetTiltSub(degree As Decimal)
		Private Sub StartThreadSetTilt(degree As Decimal)
			Try
				Dim threadNew As New DelSetTiltSub(AddressOf SetTiltSub)
				Dim threadResult As IAsyncResult


				threadResult = threadNew.BeginInvoke(degree, Nothing, Nothing)
				Do While threadResult.AsyncWaitHandle.WaitOne(50) = False
					My.Application.DoEvents()
				Loop

				threadNew.EndInvoke(threadResult)

			Catch ex As Exception
				Throw New Exception("AisgDevice::StartThreadSetTilt()::" & ex.Message)
			End Try
		End Sub
		'Private Sub StartThreadSetTilt(degree As Decimal)
		'	Try
		'		Dim threadNew As New Threading.Thread(AddressOf SetTiltSub)

		'		threadNew.IsBackground = True

		'		threadNew.Start(degree)

		'		Do While threadNew.IsAlive = True
		'			My.Application.DoEvents()
		'		Loop

		'	Catch ex As Exception
		'		Throw New Exception("AisgDevice::NewThreadSetTiltSub()::" & ex.Message)
		'	End Try
		'End Sub
		Private Sub SetTiltSub(degree As Decimal)
			Try
				If _RetType = DeviceType.SingleRet Then
					If pAisgDevice.SetTilt(_Addr, degree) = False Then Throw New FailedException("response is false")
				ElseIf _RetType = DeviceType.MultiRet Then
					If pAisgDevice.MultiRet_SetTilt(_Addr, _SubAddr, degree) = False Then Throw New FailedException("response is false")
				End If
				My.Application.DoEvents()
			Catch ex As Exception
				Throw New FailedException("SetTiltSub()::" & ex.Message)
			End Try
		End Sub
		Public Function GetCurrentTilt() As Decimal
			Try

				If _RetType = DeviceType.SingleRet Then
					Return pAisgDevice.GetTilt(_Addr)
				ElseIf _RetType = DeviceType.MultiRet Then
					Return pAisgDevice.MultiRet_GetTilt(_Addr, _SubAddr)
				Else
					Throw New Exception("Antenna type is not match with Single/Multi")
				End If

			Catch ex As Exception
				Throw New FailedException("GetCurrentTilt()::" & ex.Message)
			End Try
		End Function
		Public ReadOnly Property RetPartNumber() As String
			Get
				'	Try
				'		If _RetType = DeviceType.SingleRet Then
				'			Return pAisgDevice.GetInfo(_Addr).Split(";")(0)
				'		ElseIf _RetType = DeviceType.MultiRet Then
				'			Return pAisgDevice.GetInfo(_Addr).Split(";")(0)
				'		Else
				'			Throw New Exception("Antenna type is not match with Single/Multi")
				'		End If
				'	Catch ex As Exception
				'		Throw New FailedException("RetPartNumber()::" & ex.Message)
				'	End Try
				Return _RetPN
			End Get

		End Property
		Public Function GetMaxTilt() As Decimal
			Try
				'If _RetType = DeviceType.SingleRet Then
				'	Return pAisgDevice.GetMaxTilt(_Addr)
				'ElseIf _RetType = DeviceType.MultiRet Then
				'	Return pAisgDevice.MultiRet_GetMaxTilt(_Addr, _SubAddr)
				'End If

				'Return -1
				Return _MaxTilt
			Catch ex As Exception
				Throw New FailedException("GetMaxTilt()::" & ex.Message)
			End Try
		End Function
		Public Function GetMinTilt() As Decimal
			Try
				'If _RetType = DeviceType.SingleRet Then
				'	Return pAisgDevice.GetMinTilt(_Addr)
				'ElseIf _RetType = DeviceType.MultiRet Then
				'	Return pAisgDevice.MultiRet_GetMinTilt(_Addr, _SubAddr)
				'End If

				Return _MinTilt

			Catch ex As Exception
				Throw New FailedException("GetMinTilt()::" & ex.Message)
			End Try
		End Function
		Public Sub Calibrate()
			Try
				If _RetType = DeviceType.SingleRet Then
					pAisgDevice.Command(_Addr, &H31, Nothing, 180000)
				ElseIf _RetType = DeviceType.MultiRet Then
					pAisgDevice.MultiRet_Command(_Addr, &H80, _SubAddr, Nothing, 180000)
				End If

			Catch ex As Exception
				Throw New Exception("Calibrate()::" & ex.Message)
			End Try
		End Sub
	End Class

End Class
