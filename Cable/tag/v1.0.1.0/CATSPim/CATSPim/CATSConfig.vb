Imports System.Xml.Serialization
Public Class CATSFrameProperty
  Private m_CreateTime As String
  <XmlElement("CreateTime")>
  Public Property CreateTime As String
    Get
      Return m_CreateTime
    End Get
    Set(value As String)
      m_CreateTime = value
    End Set
  End Property
End Class
Public Class CATSFrameFactory
  Private m_Location As String
  <XmlElement("Location")>
  Public Property Location As String
    Get
      Return m_Location
    End Get
    Set(value As String)
      m_Location = value
    End Set
  End Property
  Public Sub New()

  End Sub
End Class
Public Class CATSFrameDataBase
  Private m_connString As String
  <XmlElement("ConnString")>
  Public Property ConnString As String
    Get
      Return m_connString
    End Get
    Set(value As String)
      m_connString = value
    End Set
  End Property
End Class
<XmlRoot("CATS")>
Public Class CATSConfig
  <XmlIgnore()>
  Private m_filePath As String

  <XmlElement("Property")>
  Public CatsProperty As CATSFrameProperty

  <XmlElement("Factory")>
  Public Factory As CATSFrameFactory

  <XmlElement("DataBase")>
  Public DataBase As CATSFrameDataBase

  'Public Sub New(filePath As String)
  '  m_filePath = filePath
  '  m_factoryM = New Factory
  '  m_dataBaseM = New DataBase
  'End Sub


End Class
