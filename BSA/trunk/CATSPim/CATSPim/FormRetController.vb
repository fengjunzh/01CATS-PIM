Public Class FormRetController
	Private _AisgCtrl As AisgDevice
	Private Delegate Function DelScanDevice() As List(Of DataModels.RetDevice)
	Private Delegate Function DelSetTilt(tilt As Single)
	Private _PortStatus As Boolean
	Private Function GetPortNames() As List(Of String)
		Try
			Dim resp As New List(Of String)

			resp.Add("USBdATC200-LITE-USB")

			For Each pn In IO.Ports.SerialPort.GetPortNames
				resp.Add(pn.ToString)
			Next

			Return resp

		Catch ex As Exception
            Throw New Exception("GetPortNames()::" & ex.Message)
        End Try
	End Function
	Private Sub LoadPortNames()
		Try
			cmbPortName.Items.Clear()

			For Each pn In GetPortNames()
				cmbPortName.Items.Add(pn)
			Next
		Catch ex As Exception

		End Try
	End Sub

	Public Function ScanAntennaRet() As List(Of DataModels.RetDevice)
		Try
			Dim resp As New List(Of DataModels.RetDevice)
			Dim tmpRet As DataModels.RetDevice
			Dim tmpDev As AisgDevice.stDeviceInfo
			Dim addrCount As Int16
			Dim dev As an3gppxLib.DeviceData

			addrCount = _AisgCtrl.ScanDevice()
			If addrCount = 0 Then Return Nothing

			For addrI = 1 To addrCount

				tmpDev = _AisgCtrl.GetDeviceInfo(addrI)


				If tmpDev.DevType = AisgDevice.DeviceType.SingleRet Then
					tmpRet = New DataModels.RetDevice

					Try

						dev = _AisgCtrl.GetSingleDeviceInfo(addrI)
						tmpRet.RetSn = tmpDev.SerialNumber
						tmpRet.AntennaSn = dev.AntennaSerialNumber 'tmpDev.SerialNumber
						tmpRet.AntModel = dev.AntennaModelNumber
						tmpRet.FwVersion = tmpDev.Firmware
						tmpRet.HwVersion = tmpDev.Hardware
						tmpRet.Type = tmpDev.DevType
						'tmpRet.CalAddrId = addrI
						'tmpRet.CalSubAddrId = 1
						tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, 1)
						tmpRet.Tilt.GetCurrentTilt()

					Catch ex As Exception
						tmpRet.ErrorInfo = ex.Message
					End Try

					resp.Add(tmpRet)

				ElseIf tmpDev.DevType = AisgDevice.DeviceType.MultiRet Then

					For subAddrI = 1 To tmpDev.RetSubNumber

						tmpRet = New DataModels.RetDevice

						Try
							dev = _AisgCtrl.GetMultiDeviceInfo(addrI, subAddrI)
							tmpRet.RetSn = tmpDev.SerialNumber 'dev.AntennaSerialNumber
							tmpRet.AntennaSn = dev.AntennaSerialNumber
							tmpRet.AntModel = dev.AntennaModelNumber
							tmpRet.FwVersion = tmpDev.Firmware
							tmpRet.HwVersion = tmpDev.Hardware
							tmpRet.Type = tmpDev.DevType
							'tmpRet.CalAddrId = addrI
							'tmpRet.CalSubAddrId = subAddrI
							tmpRet.Tilt = New AisgDevice.Antenna(tmpDev.DevType, addrI, subAddrI)
							tmpRet.Tilt.GetCurrentTilt()

						Catch ex As Exception
							tmpRet.ErrorInfo = ex.Message
						End Try

						resp.Add(tmpRet)

					Next

				End If
			Next

			Return resp

		Catch ex As Exception
			Throw New Exception("ScanAntennaRet()::" & ex.Message)
		End Try

	End Function
	Private Sub FormRetController_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Try
			LoadPortNames()
		Catch ex As Exception

		End Try
	End Sub

	Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
		Try

			btnOpen.Enabled = False
			btnClose.Enabled = True

			Dim portName As String = cmbPortName.Text.Trim
			If portName = "" Then MsgBox("Please select port for RET device.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly) : Return

			_AisgCtrl = New AisgDevice(portName)
			If _AisgCtrl.OpenDevice = False Then MsgBox("Open RET device error.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

			_PortStatus = True

		Catch ex As Exception
			btnOpen.Enabled = True
			btnClose.Enabled = False
			MsgBox("Open RET device." & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub

	Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
		Try
			btnOpen.Enabled = True
			btnClose.Enabled = False
			_AisgCtrl.CloseDevice()
			_PortStatus = False
		Catch ex As Exception
			btnOpen.Enabled = False
			btnClose.Enabled = True
		End Try
	End Sub
	Private Sub LoadRETDevices(devRetArray As List(Of DataModels.RetDevice))
		Try
			dgvRET.Rows.Clear()
			Dim iRow As Integer
			Dim tmpMsg As String = "OK"

			For Each dev In devRetArray
				Try
					iRow = dgvRET.Rows.Add
					dgvRET.Rows(iRow).Tag = dev

					With dgvRET.Rows(iRow)

						If dev.ErrorInfo.Trim = "" Then
							tmpMsg = "OK"
							.Cells(7).Style.BackColor = Color.White
						Else
							tmpMsg = dev.ErrorInfo.Substring(dev.ErrorInfo.LastIndexOf("::") + 2)
							.Cells(7).Style.BackColor = Color.Red
						End If

						.Cells(7).Value = tmpMsg

						.Cells(0).Value = dev.RetSn
						.Cells(1).Value = dev.AntennaSn
						.Cells(2).Value = dev.AntModel
						.Cells(3).Value = IIf(dev.Type = AisgDevice.DeviceType.SingleRet, "S-RET", "M-RET")
						.Cells(4).Value = dev.Tilt.GetMinTilt
						.Cells(5).Value = dev.Tilt.GetMaxTilt
						.Cells(6).Value = dev.Tilt.GetCurrentTilt

					End With
				Catch ex As Exception

				End Try

			Next
		Catch ex As Exception

		End Try
	End Sub
	Private Sub btnScan_Click(sender As Object, e As EventArgs) Handles btnScan.Click
		Try
			btnScan.Enabled = False
			'Dim delResult As IAsyncResult
			'Dim delRetScan As New DelScanDevice(AddressOf ScanAntennaRet)
			'Dim devRetArray As List(Of DataModels.RetDevice)
			'tstlRunText.Text = "Scanning device ..."
			'tspbProcess.Style = ProgressBarStyle.Marquee
			'Application.DoEvents()
			'delResult = delRetScan.BeginInvoke(Nothing, Nothing)
			'Do While Not (delResult.AsyncWaitHandle.WaitOne(10))
			'	Application.DoEvents()
			'Loop
			'devRetArray = delRetScan.EndInvoke(delResult)
			Dim devRetArray As List(Of DataModels.RetDevice) = ScanAntennaRet()
			LoadRETDevices(devRetArray)
			'tstlRunText.Text = "Scan device done."
			'tspbProcess.Style = ProgressBarStyle.Blocks
		Catch ex As Exception
			MsgBox("Scan RET device ..." & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		Finally
			btnScan.Enabled = True
		End Try
	End Sub

	Private Sub btnSetTilt_Click(sender As Object, e As EventArgs) Handles btnSetTilt.Click
		Try

			If dgvRET.SelectedRows.Count <= 0 Then MsgBox("Please select one RET in below table.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) : Return

			Dim selRow As DataGridViewRow = dgvRET.SelectedRows(0)
			Dim selRetDev As DataModels.RetDevice = selRow.Tag

			Dim inText As String = InputBox("Input tilt:", "Set Tilt", 0)

			If IsNumeric(inText) = False Then MsgBox("Please input numeric!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) : Return

			'Dim delSetTilt As New DelSetTilt(AddressOf selRetDev.Tilt.SetTilt)
			'Dim delResult As IAsyncResult

			'tstlRunText.Text = "Set tilt ..."
			'tspbProcess.Style = ProgressBarStyle.Marquee
			'Application.DoEvents()
			'delResult = delSetTilt.BeginInvoke(CSng(inText), Nothing, Nothing)
			'Do While Not (delResult.AsyncWaitHandle.WaitOne(50))
			'	My.Application.DoEvents()
			'Loop
			'delSetTilt.EndInvoke(delResult)
			selRetDev.Tilt.SetTilt(CSng(inText))
			selRow.Cells(6).Value = selRetDev.Tilt.GetCurrentTilt
			'tstlRunText.Text = "Set tilt done."
			'tspbProcess.Style = ProgressBarStyle.Blocks

			MsgBox("Success set tilt.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)

		Catch ex As Exception

		End Try
	End Sub

	Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
		Try
			If _PortStatus = True Then _AisgCtrl.CloseDevice() : _PortStatus = False
			Me.Close()
		Catch ex As Exception
			MsgBox("Exit RET debug tool()" & vbCrLf & " at " & ex.Message)
		End Try

	End Sub

	Private Sub btnCalibration_Click(sender As Object, e As EventArgs) Handles btnCalibration.Click
		Try
			If dgvRET.SelectedRows.Count <= 0 Then MsgBox("Please select one RET in below table.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly) : Return

			Dim selRow As DataGridViewRow = dgvRET.SelectedRows(0)
			Dim selRetDev As DataModels.RetDevice = selRow.Tag

			selRetDev.Tilt.Calibrate()
			btnScan.PerformClick()
			MsgBox("Success calibrate", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)

		Catch ex As Exception
			MsgBox("RET Calibrate()" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End Try
	End Sub
End Class