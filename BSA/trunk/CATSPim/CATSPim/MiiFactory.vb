Imports MIIBridge
Public Class MiiFactory
	Private Shared Function VerifyStation(testStation As String, miiStation As String) As Boolean
		Try
			If testStation.Trim.ToUpper = "PRETEST" Then
                'If miiStation.ToUpper.Trim = "SPPIM1" Then Return True
                If miiStation.ToUpper.Trim = "PPIM01" Then Return True
            ElseIf testStation.Trim.ToUpper = "FINALTEST" Then
				If miiStation.ToUpper.Trim = "STSP01" Then Return True
			End If

			Return False


		Catch ex As Exception
			Return False
		End Try

	End Function
	Public Shared Function CheckMiiRouting(station As CATS.Model.phase_station_main, serialNumber As String) As Boolean
		Try
			If pAppCfg.GetProcessCheck.Enable = False Then Return True

			Dim miiMain As New MIIBridge.Controller
			Dim currStation As String

			miiMain.EnableDebugLog = True
            currStation = miiMain.GetWorkStation(serialNumber) '  Factory，

            pGui.AddStatusMsg("Start to check MII routing ...", False)
			If VerifyStation(station.phase_station, currStation) Then
				pGui.AddStatusMsg(", OK")
				Return True
			Else
				pGui.AddStatusMsg(", Fail", True)
				pGui.AddStatusMsg("SN = " & serialNumber, False)
				pGui.AddStatusMsg(",MII station =" & currStation, False)
				pGui.AddStatusMsg(",Test station = " & station.phase_station, True)

				MsgBox("This Product Is Not In Correct Station !" & vbCrLf &
						"- Serial Number = " & serialNumber & vbCrLf &
						"- MII Station = " & currStation & vbCrLf &
						"- Test Station =" & station.phase_station, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
				Return False
			End If

		Catch ex As Exception
			Throw New Exception("CheckMiiRouting()::" & ex.Message)
		End Try

	End Function
	Public Shared Sub UpdateMiiRouting()
		Try

            'If pAppCfg.GetTestupdate.Enable = False Then Return
            If pAppCfg.GetProcessCheck.Enable = False Then Return

            ''===========================================================================
            'pGui.AddStatusMsg("Prepare to update MII station , waiting ... ", True)
            'Dim i As Integer = 1
            'Do Until Not File.Exists(DataSavedAsDAT) '判断文件上传后再CheckMII                   
            '    Threading.Thread.Sleep(1000)
            '    My.Application.DoEvents()
            '    i = i + 1
            '    If i > 5 Then
            '        If MsgBox("Upload Testdata to CATS database is overtime, please check network or ""C:\CATS\test_application\ResultTransferTool\ResultTransferGUI.exe is running or not"" and then try again for MII checking", MsgBoxStyle.Question + vbYesNo, "Prepare MII check process") = MsgBoxResult.Yes Then
            '            i = 1
            '        Else
            '            Exit Do
            '        End If
            '    End If
            'Loop
            ''===========================================================================


            Dim miiMain As New MIIBridge.Controller
			Dim reqs As New MIIBridge.Request
			Dim resp As MIIBridge.Response

			miiMain.EnableDebugLog = True
			reqs.Factory = pFactory
			reqs.TestUser = Environment.UserName.ToString.ToUpper
			reqs.SerialNumber = pRTP.barcode
			reqs.DbConnectionString = pDbConnString
			reqs.TestMode = pRTP.product_mode
			reqs.ProductName = pRTP.M_product_main.product_name
			reqs.PhaseStationId = pRTP.M_phase_station_main.id

			Dim ms As New MIIBridge.MeasPhase

			ms.Phase = pRTP.phase
			ms.PhaseStatus = pRTP.meas_status
			ms.StartDateTime = pRTP.meas_start_time
			ms.TotalTime = pRTP.meas_time

			reqs.CurrentMeasPhases = ms

			Try
				resp = miiMain.UpdateStatus(reqs)
				If resp.Status = Status.Error Or resp.Status = Status.Failure Then
					pGui.AddStatusMsg("MII update station , response - " & resp.ErrorMessage, True)
					pGui.AddStatusMsg("MII update station , retry ... ", True)
					MyDelay(500)
					resp = miiMain.UpdateStatus(reqs)
					If resp.Status = Status.Error Or resp.Status = Status.Failure Then
						pGui.AddStatusMsg("MII update station error - " & resp.ErrorMessage, True)
						MsgBox("MII update station error - " & resp.ErrorMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
					Else
						pGui.AddStatusMsg("MII update station , response - " & resp.ErrorMessage, True)
					End If
				Else
					pGui.AddStatusMsg("MII update station , response - " & resp.ErrorMessage, True)
				End If

			Catch ex As Exception
				pGui.AddStatusMsg("MII update station error - " & ex.Message, True)
				MsgBox("MII update station error - " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			End Try


		Catch ex As Exception
			Throw New Exception("UpdateMiiRouting()::" & ex.Message)
		End Try


	End Sub
End Class
