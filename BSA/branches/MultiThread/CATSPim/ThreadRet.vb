Imports System.Threading
Public Class ThreadRet
	Private m_RetCtrl As AisgDevice
	Private m_RetList As Dictionary(Of String, DataModels.RetDevice)
	Public Enum RetTypeEnum
		RET = 0
		VET = 1
		NA = 2
	End Enum
	Public Class RetTiltModel
		Public PhaseName As String
		Public RetIdx As Integer
		Public RetTilt As String
	End Class
	Private m_RetMTQueue As New List(Of RetTiltModel)
	Public Sub AddRetItem(item As RetTiltModel)
		Try
			SyncLock (m_RetMTQueue)
				m_RetMTQueue.Add(item)
			End SyncLock
		Catch ex As Exception
			Throw New Exception("AddRetItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub RemoveRetItem(item As RetTiltModel)
		Try
			SyncLock (m_RetMTQueue)

				Dim resp As New RetTiltModel

				If m_RetMTQueue.Contains(item) Then
					m_RetMTQueue.Remove(item)
				Else
					Throw New Exception("Not found item PhaseName=" & item.PhaseName & ", Idx=" & item.RetIdx & ",Tilt=" & item.RetTilt)
				End If

			End SyncLock
		Catch ex As Exception
			Throw New Exception("RemoveRetItem()::" & ex.Message)
		End Try
	End Sub
	Public ReadOnly Property RetList As Dictionary(Of String, DataModels.RetDevice)
		Get
			Return m_RetList
		End Get
	End Property
	Public Function GetProductRetType() As RetTypeEnum
		Try
			If pRTGlobal.M_product_main.dwtilt_enable = True Then
				If (pRTGlobal.M_product_main.dwtilt_type = 2 Or pRTGlobal.M_product_main.dwtilt_type = 3) Then
					Return RetTypeEnum.RET
				Else
					Return RetTypeEnum.VET
				End If
			Else
				Return RetTypeEnum.NA
			End If

		Catch ex As Exception
			Throw New Exception("GetProductRetType()::" & ex.Message)
		End Try
	End Function
	Public Function IsProductAccuRet() As Boolean
		Try
			Return (pRTGlobal.M_product_main.dwtilt_type = 3)
		Catch ex As Exception
			Throw New Exception("IsProductAccuRet()::" & ex.Message)
		End Try

	End Function
	Public Sub OpenRetDevice()
		Try
			Dim retPara As CATSPimConfig.LocalConfig.RetPara

			If GetProductRetType() = RetTypeEnum.RET Then
				retPara = pAppCfg.GetRetConfig()
				m_RetCtrl = New AisgDevice(retPara.Address)
				If m_RetCtrl.OpenDevice = False Then Throw New Exception("Open Ret device")
			End If

		Catch ex As Exception
			Throw New Exception("OpenRetDevice()::" & ex.Message)
		End Try

	End Sub
	Public Function CloseRetDevice() As Boolean
		Try
			If GetProductRetType() = RetTypeEnum.RET Then
				Return m_RetCtrl.CloseDevice()
			End If
			Return True
		Catch ex As Exception
			Throw New Exception("CloseRetDevice()::" & ex.Message)
		End Try

	End Function
	Public Sub ScanAntennaRet(RetPN As Short, RetMaps As List(Of CATS.Model.product_ret_map))
		Try
			Dim resp As New Dictionary(Of String, DataModels.RetDevice)
			Dim tmpRet As DataModels.RetDevice
			Dim tmpDev As AisgDevice.stDeviceInfo
			Dim addrCount As Int16
			Dim addrI As Int16
			Dim subAddrI As Int16
			Dim dev As an3gppxLib.DeviceData

			addrCount = m_RetCtrl.ScanDevice()
			If addrCount = 0 Then resp = Nothing

			For addrI = 1 To addrCount

				tmpDev = m_RetCtrl.GetDeviceInfo(addrI)

				If tmpDev.DevType = AisgDevice.DeviceType.SingleRet Then
					dev = m_RetCtrl.GetSingleDeviceInfo(addrI)
					tmpRet = New DataModels.RetDevice
					tmpRet.RetSn = tmpDev.SerialNumber
					tmpRet.AntennaSn = dev.AntennaSerialNumber 'tmpDev.SerialNumber
					tmpRet.AntModel = dev.AntennaModelNumber
					'tmpRet.RetModel = 
					tmpRet.FwVersion = tmpDev.Firmware
					tmpRet.HwVersion = tmpDev.Hardware
					tmpRet.Type = tmpDev.DevType

					tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, 1)

					pGui.AddStatusMsg(String.Format("RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
		addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)

					If RetPN = 3 Or tmpRet.Tilt.RetPartNumber.ToString.ToUpper.Contains("ACCURET") Then 'AccuRET
						If RetMaps Is Nothing Then Throw New Exception("The AccuRET have no map in DB.")
						If RetMaps.Count = 0 Then Throw New Exception("The AccuRET have no map in DB.")

						Dim band_id As String = tmpRet.AntModel.ToString.Substring(tmpRet.AntModel.ToString.LastIndexOf("-") + 1)

						Dim ret_idx As String

						ret_idx = RetMaps.Find(Function(o) o.band_id = band_id).ret_idx

						resp.Add(tmpRet.RetSn & "." & ret_idx, tmpRet)

					Else
						resp.Add(tmpRet.RetSn & "." & Right(tmpRet.RetSn, 1), tmpRet)
					End If

				ElseIf tmpDev.DevType = AisgDevice.DeviceType.MultiRet Then

					For subAddrI = 1 To tmpDev.RetSubNumber

						tmpRet = New DataModels.RetDevice

						dev = m_RetCtrl.GetMultiDeviceInfo(addrI, subAddrI)
						tmpRet.RetSn = tmpDev.SerialNumber 'dev.AntennaSerialNumber
						tmpRet.AntennaSn = dev.AntennaSerialNumber
						tmpRet.AntModel = dev.AntennaModelNumber
						tmpRet.FwVersion = tmpDev.Firmware
						tmpRet.HwVersion = tmpDev.Hardware
						tmpRet.Type = tmpDev.DevType

						tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, subAddrI)
						resp.Add(tmpRet.RetSn & "." & subAddrI, tmpRet)

						pGui.AddStatusMsg(String.Format(" RetSn[{1}], AntSn[{2}], Model[{3}], Fw[{4}], Hw[{5}], Type[{6}]",
					addrI, tmpRet.RetSn, tmpRet.AntennaSn, tmpRet.AntModel, tmpRet.FwVersion, tmpRet.HwVersion, tmpRet.Type), True)

					Next
				End If
			Next

			m_RetList = resp

		Catch ex As Exception
			Throw New Exception("ScanAntennaRet()::" & ex.Message)
		End Try

	End Sub
	Public Sub CheckRets()
		Try

			'check count
			pGui.AddStatusMsg("Check Ret number ... , ", False)
			If m_RetList.Count <> pRTGlobal.M_product_main.dwtilt_num Then
				pGui.AddStatusMsg("Fail")
				Throw New Exception("Rets number do not match to Db")
			Else
				pGui.AddStatusMsg("OK", True)
			End If

			'check model
			pGui.AddStatusMsg("Check Ret model ... ,", False)
			For Each r In m_RetList
				pGui.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, r.Value.AntModel), False)
				'If r.Value.AntModel.Trim.ToUpper = pRTP.M_product_main.dwtilt_pn.Trim.ToUpper Then
				If r.Value.AntModel.Trim.ToUpper.Contains(pRTGlobal.M_product_main.dwtilt_pn.Trim.ToUpper) Then
					pGui.AddStatusMsg("OK", True)
				Else
					pGui.AddStatusMsg("Fail", True)
					Throw New Exception("Rets model do not match to product")
				End If
			Next

			' check sn
			pGui.AddStatusMsg("Check Ret sn ... ,", False)
			For Each r In m_RetList
				pGui.AddStatusMsg(String.Format("Ret[{0}], SN [{1}] , ", r.Key, r.Value.AntennaSn), False)
				If r.Value.AntennaSn.Trim.ToUpper.Contains(pRTGlobal.barcode.Trim.ToUpper) Then
					pGui.AddStatusMsg("OK", True)
				Else
					pGui.AddStatusMsg("Fail", True)
					Throw New Exception("Rets sn do not match to product")
				End If
			Next

			'check multiy/single
			pGui.AddStatusMsg("Check Ret single/multi ... ,", False)
			For Each r In m_RetList
				pGui.AddStatusMsg(String.Format("Ret[{0}], type [{1}] , ", r.Key, CType(r.Value.Type, AisgDevice.DeviceType).ToString), False)
				If IIf(r.Value.Type = AisgDevice.DeviceType.SingleRet, "S", "M") = pRTGlobal.M_product_main.dwtilt_mode.Trim.ToUpper Then
					pGui.AddStatusMsg("OK", True)
				Else
					pGui.AddStatusMsg("Fail", True)
					Throw New Exception("Rets number miss match to Db")
				End If
			Next

		Catch ex As Exception
			Throw New Exception("CheckRets()::" & ex.Message)
		End Try

	End Sub
	Private Sub SetRetDownTilt(tiltIdxs As String, downTilts As String)
		Try
			Dim tpidx() As String
			Dim tpdwtilt() As String
			Dim fSet As Boolean = False

			tpidx = tiltIdxs.Split(",")
			tpdwtilt = downTilts.Split(",")
			If tpidx.Count <> tpdwtilt.Count Then
				pGui.AddStatusMsg(String.Format("RET idx {0} miss match downtilt{1}.", tiltIdxs, downTilts), True)
				Throw New Exception(String.Format("RET idx {0} miss match downtilt {1}.", tiltIdxs, downTilts))
			End If

			For Each dev In m_RetList
				For id As Short = 0 To tpidx.Count - 1
					'If dev.Value.Type = AisgDevice.DeviceType.SingleRet Then
					If Right(dev.Key.ToString, 1) = tpidx(id) Then
						Dim frm As New FormSetRet
						Try
							pGui.AddStatusMsg(String.Format("Setting RET idx={0},downtilt={1} ... , ", tpidx(id), tpdwtilt(id)), False)
							frm.ShowDownTilt = tpdwtilt(id)
							frm.Show()
							My.Application.DoEvents()
							dev.Value.Tilt.SetTilt(tpdwtilt(id))
							pGui.AddStatusMsg("Ok")
							fSet = True
						Catch ex As Exception
							Throw New Exception(ex.Message)
						Finally
							frm.Close()
						End Try
					End If
				Next
			Next

			If fSet = False Then Throw New Exception("Mismatch RET ID")

		Catch ex As Exception
			Throw New Exception("SetRetDownTilt()::" & ex.Message)
		End Try
	End Sub
	Private Sub SetVetDownTilt(tiltIdxs As String, downTilts As String)
		Try
			Dim tpidx() As String
			Dim tpdwtilt() As String
			Dim msg As String = ""

			tpidx = tiltIdxs.Split(",")
			tpdwtilt = downTilts.Split(",")
			If tpidx.Count <> tpdwtilt.Count Then
				pGui.AddStatusMsg(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts), True)
				Throw New Exception(String.Format("Tilt# {0} miss match Angle {1}.", tiltIdxs, downTilts))
			End If

			msg += "Please manually turn the Downtilt to specified angle :" & vbCrLf

			For id As Short = 0 To tpidx.Count - 1
				msg += String.Format(" Tilt# {0}, Angle ={1}", tpidx(id), tpdwtilt(id)) & vbCrLf
			Next

			MsgBox(msg, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)

			pGui.AddStatusMsg("OK")

		Catch ex As Exception
			Throw New Exception("SetVetDownTilt()::" & ex.Message)
		End Try
	End Sub
	Private Sub SetDownTilts(item As RetTiltModel)
		Try
			Dim retType As RetTypeEnum = GetProductRetType()

			If retType = RetTypeEnum.RET Then
				SetRetDownTilt(item.RetIdx, item.RetTilt)
			ElseIf retType = RetTypeEnum.VET Then 'VET
				SetVetDownTilt(item.RetIdx, item.RetTilt)
			Else
				Throw New Exception("RET is enable, but not RET or VET  or ...")
			End If

		Catch ex As Exception
			Throw New Exception("SetDownTilts()::" & ex.Message)
		End Try
	End Sub
	Public Sub SetTiltThread()
		Try

			Do While (pRTGlobal.semporeRet.IsRetThreadExit = False)

				If (pRTGlobal.semporeRet.IsPrepareSetTilt() And pRTGlobal.semporeRetry.SignalValue = 0) Then

					Dim tmpTilts As New List(Of RetTiltModel)
					tmpTilts.AddRange(m_RetMTQueue)


					For Each tilt In tmpTilts

						Try
							SetDownTilts(tilt)
						Catch ex As Exception

						Finally
							RemoveRetItem(tilt)
						End Try

					Next

					pRTGlobal.semporeRet.SignalValue = 0

				End If

				Thread.Sleep(500)

			Loop


		Catch ex As Exception
			Throw New Exception("SetTiltThread()::" & ex.Message)
		End Try
	End Sub
End Class
