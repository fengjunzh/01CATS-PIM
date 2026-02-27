Imports System.IO
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

			Dim o As New Object

			SyncLock (o)

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

			End SyncLock

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
	Public Function GetInstrumentConfig(ByVal bandName As String) As InstrPara
		Try
			Dim o As New Object

			SyncLock (o)

				Dim dt As New System.Data.DataTable
				Dim instrConfig As New InstrPara
				Dim dataRow() As DataRow
				Dim dataString As String


				dt = m_dataset.Tables("Instruments")
				dataRow = dt.Select("BandName='" & bandName & "'")
				If dataRow.Count = 0 Then Throw New Exception("can not find [" & bandName & "] configuration.")
				If dataRow.Count > 1 Then Throw New Exception("find multi [" & bandName & "]")

				With instrConfig

					.Enable = IIf(dataRow(0).Item("Enable").ToString.ToUpper = "TRUE", True, False)
					.Vendor = dataRow(0).Item("Vendor")
					.Model = dataRow(0).Item("Model")
					.BandIdx = dataRow(0).Item("BandIdx")
					.Address = dataRow(0).Item("Address")
					.BandName = dataRow(0).Item("BandName")

					dataString = dataRow(0).Item("Tx1Loss")
					.Tx1Loss = IIf(String.IsNullOrEmpty(dataString), 0, CSng(dataString))

					dataString = dataRow(0).Item("Tx2Loss")
					.Tx2Loss = IIf(String.IsNullOrEmpty(dataString), 0, CSng(dataString))

					dataString = dataRow(0).Item("RxLoss")
					.RxLoss = IIf(String.IsNullOrEmpty(dataString), 0, CSng(dataString))

				End With

				Return instrConfig

			End SyncLock

		Catch ex As Exception
			Throw New Exception("CATSPimConfig()::GetInstrumentConfig(" & bandName & ")::" & ex.Message)
		End Try
	End Function
	Public Function GetInstruments() As DataTable
    Try

      Return m_dataset.Tables("Instruments")

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
End Class
