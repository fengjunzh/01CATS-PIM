Imports System.IO
Imports System.Xml
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
        Public DevSN As String
    End Class
	Public Class RetPara
		Public Enable As Boolean
		Public Address As String
	End Class
    Public Class VibrationPara
        Public Enable As Boolean
        Public Address As String
        Public COMPORT As String 'add by tony for 8222
    End Class
    '======================= add by tony
    Public Class ChamberDoor
        Public Enable As Boolean
    End Class

    Public Class ChamberID
        Public Enable As Boolean
    End Class

    Public Class ChamberVibration
        Public Enable As Boolean
    End Class
    '======================================
    Public Class ProcessCheckPara
		Public Enable As Boolean
	End Class
	'Public Class TestUpdatePara
	'	Public Enable As Boolean
	'End Class
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

            If checkNode(m_filename, "Vibration", "COMPORT") <> True Then Throw New Exception("Don't find [Vibration]-> [COMPORT] configuration.")

            dt = m_dataset.Tables("Vibration")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Vibration] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [Vibration] configuration.")

            With vibConfig

                .Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)
                .Address = dataRow(0).Item("Address")
                .COMPORT = dataRow(0).Item("COMPORT")

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
                .Item("COMPORT") = value.COMPORT

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

				.Tx1Loss = CSng(GetCellValue(dataRow(0).Item("Tx1Loss")))
				.Tx2Loss = CSng(GetCellValue(dataRow(0).Item("Tx2Loss")))
				.RxLoss = CSng(GetCellValue(dataRow(0).Item("RxLoss")))

                If dt.Columns.Contains("CableSerNum") Then
                    .CableSerNum = GetCellValue(dataRow(0).Item("CableSerNum"))
                Else
                    .CableSerNum = ""
                End If

                If dt.Columns.Contains("DevSN") Then
                    .DevSN = GetCellValue(dataRow(0).Item("DevSN"))
                Else
                    .DevSN = ""
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
            If resp.Columns.Contains("DevSN") = False Then resp.Columns.Add("DevSN") ' add by tony 12/24 2018


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

	Public Function GetProcessCheck() As ProcessCheckPara
		Try
			Dim resp As New ProcessCheckPara
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			If m_dataset.Tables.Contains("ProcessCheck") = False Then
				dt = m_dataset.Tables.Add("ProcessCheck")
				dt.Columns.Add("Enable")
				dt.Rows.Add("False")
				m_dataset.WriteXml(m_filename)
				resp.Enable = False
				Return resp
			End If

			dt = m_dataset.Tables("ProcessCheck")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [ProcessCheck] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [ProcessCheck] configuration.")

			resp.Enable = dataRow(0).Item("Enable").ToString.ToUpper

			Return resp

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetProcessCheck()::" & ex.Message)
		End Try
	End Function
	Public Sub SaveProcessCheck(value As ProcessCheckPara)
		Try
			Dim dt As New System.Data.DataTable
			Dim dataRow() As DataRow

			If m_dataset.Tables.Contains("Processcheck") = False Then
				dt = m_dataset.Tables.Add("Processcheck")
				dt.Columns.Add("Enable")
				dt.Rows.Add("False")
				m_dataset.WriteXml(m_filename)
			End If

			dt = m_dataset.Tables("Processcheck")
			dataRow = dt.Select()
			If dataRow.Count = 0 Then Throw New Exception("can not find [Processcheck] configuration.")
			If dataRow.Count > 1 Then Throw New Exception("find multi [Processcheck] configuration.")

			dataRow(0).Item("Enable") = value.Enable

			m_dataset.WriteXml(m_filename)

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::SaveProcessCheck()::" & ex.Message)
		End Try
	End Sub
    'Public Function GetTestupdate() As TestUpdatePara
    '	Try
    '		Dim resp As New TestUpdatePara
    '		Dim dt As New System.Data.DataTable
    '		Dim dataRow() As DataRow

    '		If m_dataset.Tables.Contains("Testupdate") = False Then
    '			dt = m_dataset.Tables.Add("Testupdate")
    '			dt.Columns.Add("Enable")
    '			dt.Rows.Add("False")
    '			m_dataset.WriteXml(m_filename)
    '			resp.Enable = False
    '			Return resp
    '		End If

    '		dt = m_dataset.Tables("Testupdate")
    '		dataRow = dt.Select()
    '		If dataRow.Count = 0 Then Throw New Exception("can not find [Testupdate] configuration.")
    '		If dataRow.Count > 1 Then Throw New Exception("find multi [Testupdate] configuration.")

    '		resp.Enable = dataRow(0).Item("Enable").ToString.ToUpper

    '		Return resp

    '	Catch ex As Exception
    '		Throw New Exception("CATSPimConfig()::GetTestupdate()::" & ex.Message)
    '	End Try
    'End Function
    'Public Sub SaveTestupdate(value As TestUpdatePara)
    '	Try
    '		Dim dt As New System.Data.DataTable
    '		Dim dataRow() As DataRow

    '		If m_dataset.Tables.Contains("Testupdate") = False Then
    '			dt = m_dataset.Tables.Add("Testupdate")
    '			dt.Columns.Add("Enable")
    '			dt.Rows.Add("False")
    '			m_dataset.WriteXml(m_filename)
    '		End If

    '		dt = m_dataset.Tables("Testupdate")
    '		dataRow = dt.Select()
    '		If dataRow.Count = 0 Then Throw New Exception("can not find [Testupdate] configuration.")
    '		If dataRow.Count > 1 Then Throw New Exception("find multi [Testupdate] configuration.")

    '		dataRow(0).Item("Enable") = value.Enable

    '		m_dataset.WriteXml(m_filename)

    '	Catch ex As Exception
    '		Throw New Exception("CATSPimConfig()::SaveTestupdate()::" & ex.Message)
    '	End Try
    'End Sub

    '============================add by tony
    Public Function GetDoorCheck() As ChamberDoor
        Try
            Dim resp As New ChamberDoor
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("DoorCheck") = False Then
                dt = m_dataset.Tables.Add("DoorCheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
                resp.Enable = False
                Return resp
            End If

            dt = m_dataset.Tables("DoorCheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [DoorCheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [DoorCheck] configuration.")

            resp.Enable = dataRow(0).Item("Enable").ToString.ToUpper

            Return resp

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::GetDoorCheck()::" & ex.Message)
        End Try
    End Function
    Public Function SaveDoorCheck(value As ChamberDoor) As Boolean
        Try
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("Doorcheck") = False Then
                dt = m_dataset.Tables.Add("Doorcheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
            End If

            dt = m_dataset.Tables("Doorcheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [Doorcheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [Doorcheck] configuration.")

            dataRow(0).Item("Enable") = value.Enable

            m_dataset.WriteXml(m_filename)
            Return True

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::SaveDoorCheck()::" & ex.Message)
            Return False
        End Try
    End Function

    Public Function GetChamberIDCheck() As ChamberID
        Try
            Dim resp As New ChamberID
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("ChamberIDCheck") = False Then
                dt = m_dataset.Tables.Add("ChamberIDCheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
                resp.Enable = False
                Return resp
            End If

            dt = m_dataset.Tables("ChamberIDCheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [ChamberIDCheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [ChamberIDCheck] configuration.")

            resp.Enable = dataRow(0).Item("Enable").ToString.ToUpper

            Return resp

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::GetChamberIDCheck()::" & ex.Message)
        End Try
    End Function
    Public Function SaveChamberIDCheck(value As ChamberID) As Boolean
        Try
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("ChamberIDcheck") = False Then
                dt = m_dataset.Tables.Add("ChamberIDcheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
            End If

            dt = m_dataset.Tables("ChamberIDcheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [ChamberIDcheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [ChamberIDcheck] configuration.")

            dataRow(0).Item("Enable") = value.Enable

            m_dataset.WriteXml(m_filename)
            Return True

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::SaveChamberIDCheck()::" & ex.Message)
            Return False
        End Try
    End Function


    Public Function GetChamberVibrationCheck() As ChamberVibration
        Try
            Dim resp As New ChamberVibration
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("ChamberVibrationCheck") = False Then
                dt = m_dataset.Tables.Add("ChamberVibrationCheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
                resp.Enable = False
                Return resp
            End If

            dt = m_dataset.Tables("ChamberVibrationCheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [ChamberVibrationCheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [ChamberVibrationCheck] configuration.")

            resp.Enable = dataRow(0).Item("Enable").ToString.ToUpper

            Return resp

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::GetChamberVibrationCheck()::" & ex.Message)
        End Try
    End Function
    Public Function SaveChamberVibrationCheck(value As ChamberVibration) As Boolean
        Try
            Dim dt As New System.Data.DataTable
            Dim dataRow() As DataRow

            If m_dataset.Tables.Contains("ChamberVibrationCheck") = False Then
                dt = m_dataset.Tables.Add("ChamberVibrationCheck")
                dt.Columns.Add("Enable")
                dt.Rows.Add("False")
                m_dataset.WriteXml(m_filename)
            End If

            dt = m_dataset.Tables("ChamberVibrationCheck")
            dataRow = dt.Select()
            If dataRow.Count = 0 Then Throw New Exception("can not find [ChamberVibrationCheck] configuration.")
            If dataRow.Count > 1 Then Throw New Exception("find multi [ChamberVibrationCheck] configuration.")

            dataRow(0).Item("Enable") = value.Enable

            m_dataset.WriteXml(m_filename)
            Return True

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::SaveChamberVibrationCheck()::" & ex.Message)
            Return False
        End Try
    End Function


    'add by tony to check COMPORT is exist or not for 8222
    Private Function checkNode(ByVal filename As String, ByVal nodename1 As String, ByVal nodename2 As String) As Boolean

        Dim xmlDoc As New XmlDocument
        Dim xmlNodes As XmlNodeList
        Dim xmlNode As XmlNode
        Dim strNodeXml As String = ""
        Dim strNodeText As String = ""

        Try
            'xmlDoc.Load("C:\CATS\test_system\CATSPimTestConfig.xml")
            xmlDoc.Load(filename)

            xmlNodes = xmlDoc.DocumentElement.ChildNodes
            For i = 0 To xmlNodes.Count - 1
                If xmlNodes(i).Name = nodename1 Then
                    If xmlNodes(i).LastChild.Name.ToUpper <> nodename2 Then
                        Dim newAuthor As XmlElement = xmlDoc.CreateElement(nodename2)
                        xmlNodes(i).AppendChild(newAuthor)
                    End If
                    Exit For
                End If
            Next

            '保存XML
            'Dim tr As New XmlTextWriter("C:\0XTX\00Practice\XML_Operate\XML_Test.xml", Nothing)
            Dim tr As New XmlTextWriter(filename, Nothing)
            tr.Formatting = Formatting.Indented
            xmlDoc.WriteContentTo(tr)
            tr.Close()

            m_dataset.Tables.Clear()
            m_dataset.ReadXml(filename)
            Return True

        Catch ex As Exception
            Throw New Exception("CATSPimConfig()::checkNode()::" & ex.Message)
            Return False
        End Try

    End Function


End Class
