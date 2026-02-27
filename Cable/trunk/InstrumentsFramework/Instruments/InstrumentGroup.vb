Imports AndrewIntegratedProducts.InstrumentsFramework
Imports System.XMl
Public Class InstrumentGroup
  Inherits Dictionary(Of String, Instrument)
  'Private Instruments As New Dictionary(Of String, Instrument)
  Private ConfigurationXMLDoc As System.Xml.XmlDocument
  'Private WithEvents pTmpInstrument As Instrument = Nothing
  Public Function GetInstrument(ByVal key_value As String) As Instrument
    Try
      If MyBase.ContainsKey(key_value) Then
        'If Instruments.ContainsKey(key_value) Then
        'Return Instruments.Item(key_value)
        Return MyBase.Item(key_value)
      Else
        Throw New Exception(String.Format("Instrument {0} doesn't exist in the list of instruments", key_value))
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Function
  'Public Sub Clear()
  '    Instruments.Clear()
  'End Sub
  Sub New(ByVal InstrumentConfigXMLFile As String)
    Try
      'Check if Config file exist and then loads in XML doc
      Dim FileInfo As System.IO.FileInfo = New System.IO.FileInfo(InstrumentConfigXMLFile)
      If FileInfo.Exists = False Then
        Throw New Exception(String.Format("Instruments Configuration file ({0}) doesn't exist!", InstrumentConfigXMLFile))
      End If
      ' Load the config file into the XML DOM.     
      ConfigurationXMLDoc = New System.Xml.XmlDocument()
      ConfigurationXMLDoc.Load(FileInfo.FullName)
      SetupInstrumentsGroup()
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
  Private Sub SetupInstrumentsGroup()
    Try
      Dim InstrumentNode As System.Xml.XmlNode
      Dim pTmpInstrument As Instrument = Nothing
      For Each InstrumentNode In ConfigurationXMLDoc.Item("Instruments")
        If InstrumentNode.Name = "add" Then
          Select Case InstrumentNode.Attributes.GetNamedItem("type").Value
            Case "ROSENBERGER_IMD_INSTRUMENT"
              pTmpInstrument = New RosenbergerDevice
            Case "KAELUS"
              pTmpInstrument = New KaelusBPIMDevice
            Case "DUMMY_IMD_INSTRUMENT"
              pTmpInstrument = New DummyDevice
            Case "HMR"
              'pTmpInstrument = New Hammering_Machine
            Case Else
              Throw New Exception(String.Format("Instrument type {0} not implemented for ID {1}!", InstrumentNode.Attributes.GetNamedItem("type").Value, InstrumentNode.Attributes.GetNamedItem("ID").Value))
          End Select
          pTmpInstrument.Name = InstrumentNode.Attributes.GetNamedItem("ID").Value
          pTmpInstrument.Address = InstrumentNode.Attributes.GetNamedItem("address").Value
          MyBase.Add(pTmpInstrument.Name, pTmpInstrument)
        End If
      Next
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
End Class
