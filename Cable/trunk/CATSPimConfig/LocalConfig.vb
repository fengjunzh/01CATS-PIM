Option Strict Off
Imports System
Imports System.IO
Imports System.Data
Imports System.Linq
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Public Class LocalConfig
	Private m_dataset As New DataSet
	Private m_filename As String
	Public Class InstrPara
		Public Enable As Boolean
		Public Vendor As String
		Public Model As String
		Public BandIdx As String
		Public Address As String
		Public BandName As String
		Public Tx1Loss As Single
		Public Tx2Loss As Single
		Public RxLoss As Single
		Public CableSerNum As String
	End Class
	Public Class RetPara
		Public Enable As Boolean
		Public Address As String
	End Class
	Public Class VibrationPara
		Public Enable As Boolean
		Public Address As String
	End Class
	Public Sub New(fileName As String)
		Try

			If File.Exists(fileName) = False Then Throw New Exception("Could not find file '" & fileName & "'")

			m_filename = fileName
			m_dataset.ReadXml(fileName)

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::" & ex.Message)
		End Try
	End Sub
	Public Function GetTestMode() As String
		Try
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("TestMode")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [TestMode] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [TestMode] configuration.")

			Return dataRow(0).Item("ModeName").ToString.ToUpper


		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetTestMode()::" & ex.Message)
		End Try

	End Function
	Public Function GetRetConfig() As RetPara
		Try
			Dim dt As New System.Data.DataTable
			Dim retConfig As New RetPara
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("Ret")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Ret] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [Ret] configuration.")

			With retConfig

				.Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)

				.Address = dataRow(0).Item("Address")

			End With

			Return retConfig

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetRetConfig()::" & ex.Message)
		End Try
	End Function
	Public Function SaveTestMode(value As String) As Boolean
		Try
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("TestMode")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [TestMode] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [TestMode] configuration.")

			With dataRow(0)

				.Item("ModeName") = value

			End With

			m_dataset.WriteXml(m_filename)

			Return True

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::SaveTestMode()::" & ex.Message)
		End Try
	End Function
	Public Function SaveRetConfig(value As RetPara) As Boolean
		Try
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("Ret")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Ret] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [Ret] configuration.")

			With dataRow(0)

				.Item("Enable") = value.Enable

				.Item("Address") = value.Address

			End With

			m_dataset.WriteXml(m_filename)

			Return True

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::SaveRetConfig()::" & ex.Message)
		End Try
	End Function
	Public Function GetVibrationConfig() As VibrationPara
		Try
			Dim dt As New System.Data.DataTable
			Dim vibConfig As New VibrationPara
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("Vibration")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Vibration] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [Vibration] configuration.")

			With vibConfig

				.Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)

				.Address = dataRow(0).Item("Address")

			End With

			Return vibConfig

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetVibrationConfig()::" & ex.Message)
		End Try
	End Function
	Public Function SaveVibrationConfig(value As VibrationPara) As Boolean
		Try
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("Vibration")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Vibration] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [Vibration] configuration.")

			With dataRow(0)

				.Item("Enable") = value.Enable

				.Item("Address") = value.Address

			End With

			m_dataset.WriteXml(m_filename)

			Return True

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::SaveVibrationConfig()::" & ex.Message)
		End Try
	End Function
	Private Function GetCellValue(value As Object) As String
        Try
            If IsDBNull(value) Then Return ""
            Return value.ToString

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::GetCellValue()::" & ex.Message)
        End Try
    End Function
	Public Function GetInstrumentConfig(ByVal bandName As String) As InstrPara
		Try
			Dim dt As New System.Data.DataTable
			Dim instrConfig As New InstrPara
			Dim dataRow() As DataRow

			dt = m_dataset.Tables("Instruments")
			dataRow = dt.Select("BandName='" & bandName & "' and Enable='True'")
			If dataRow.Count = 0 Then Throw New Exception("can not find [" & bandName & "] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [" & bandName & "]")

			With instrConfig

				.Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)
				.Vendor = GetCellValue(dataRow(0).Item("Vendor"))
				If .Vendor = "" Then Throw New Exception("Empty <Vendor> parameter")

				.Model = GetCellValue(dataRow(0).Item("Model"))
				If .Model = "" Then Throw New Exception("Empty <Model> parameter")

				.BandIdx = GetCellValue(dataRow(0).Item("BandIdx"))
				If .BandIdx = "" Then Throw New Exception("Empty <BandIdx> parameter")

				.Address = GetCellValue(dataRow(0).Item("Address"))
				If .Address = "" Then Throw New Exception("Empty <Address> parameter")

				.BandName = GetCellValue(dataRow(0).Item("BandName"))
				If .BandName = "" Then Throw New Exception("Empty <BandName> parameter")

                '.Tx1Loss = CSng(GetCellValue(dataRow(0).Item("Tx1Loss")))
                '.Tx2Loss = CSng(GetCellValue(dataRow(0).Item("Tx2Loss")))
                '.RxLoss = CSng(GetCellValue(dataRow(0).Item("RxLoss")))

                If dt.Columns.Contains("CableSerNum") Then
					.CableSerNum = GetCellValue(dataRow(0).Item("CableSerNum"))
				Else
					.CableSerNum = ""
				End If


			End With

			Return instrConfig

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetInstrumentConfig(" & bandName & ")::" & ex.Message)
		End Try
	End Function
	Public Function GetInstruments() As DataTable
		Try
			Dim resp As DataTable

			resp = m_dataset.Tables("Instruments")

			If resp.Columns.Contains("CableSerNum") = False Then resp.Columns.Add("CableSerNum")

			Return resp

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetInstruments()::" & ex.Message)
		End Try
	End Function
    Public Function SaveInstruments(dt As DataTable) As Boolean
        Try

            dt.TableName = "Instruments"
            m_dataset.Tables.Remove("Instruments")
            m_dataset.Tables.Add(dt)
            m_dataset.WriteXml(m_filename)
            Return True

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::SaveInstruments()::" & ex.Message)
        End Try
    End Function
    Public Function GetInstrumentConfig() As InstrPara
        Try
            Dim dt As New System.Data.DataTable
            Dim instrConfig As New InstrPara
            Dim dataRow() As DataRow

            dt = m_dataset.Tables("Instruments")
            dataRow = dt.Select("Enable='True'")
            If dataRow.Count = 0 Then Throw New Exception("can not find instrument.")
            If dataRow.Count > 1 Then Throw New Exception("find multi intruments")

            With instrConfig

                .Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)
                .Vendor = GetCellValue(dataRow(0).Item("Vendor"))
                If .Vendor = "" Then Throw New Exception("Empty <Vendor> parameter")

                .Model = GetCellValue(dataRow(0).Item("Model"))
                If .Model = "" Then Throw New Exception("Empty <Model> parameter")

                .BandIdx = GetCellValue(dataRow(0).Item("BandIdx"))
                If .BandIdx = "" Then Throw New Exception("Empty <BandIdx> parameter")

                .Address = GetCellValue(dataRow(0).Item("Address"))
                If .Address = "" Then Throw New Exception("Empty <Address> parameter")

                .BandName = GetCellValue(dataRow(0).Item("BandName"))
                If .BandName = "" Then Throw New Exception("Empty <BandName> parameter")

                '.Tx1Loss = CSng(GetCellValue(dataRow(0).Item("Tx1Loss")))
                '.Tx2Loss = CSng(GetCellValue(dataRow(0).Item("Tx2Loss")))
                '.RxLoss = CSng(GetCellValue(dataRow(0).Item("RxLoss")))

                If dt.Columns.Contains("CableSerNum") Then
                    .CableSerNum = GetCellValue(dataRow(0).Item("CableSerNum"))
                Else
                    .CableSerNum = ""
                End If


            End With

            Return instrConfig

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::GetInstrumentConfig()::" & ex.Message)
        End Try
    End Function
End Class
