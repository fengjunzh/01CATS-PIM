Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System

Public Class SerialInq
    Public Sub New()

    End Sub

    Public Function GetProduct(ByVal serialNumber As String) As Product
        Try
            Dim strURL As String = My.Settings.SAPReqURL & serialNumber
            Dim cableAssembly As Product
            cableAssembly = LookUpSAPData(strURL)
            If cableAssembly IsNot Nothing Then
                Return cableAssembly
            Else
                'strURL = My.Settings.SAPReqURL & serialNumber
                'cableAssembly = LookUpSAPData(strURL)
                'If cableAssembly IsNot Nothing Then
                '    Return cableAssembly
                'Else
                '    Return Nothing
                'End If
                strURL = My.MySettings.Default.SAPReqURL & (Convert.ToInt16(DateTime.Now.Year) - 1).ToString.Substring(2) & serialNumber.Remove(0, 2)
                cableAssembly = LookUpSAPData(strURL)
                If cableAssembly IsNot Nothing Then
                    Return cableAssembly
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            Throw New Exception("GetCableAssembly()::" & ex.Message)
        End Try

    End Function

    Private Function LookUpSAPData(ByVal strURL As String) As Product
        Try
            Dim cableAssembly As New Product
            Dim strXML As String

            Dim objRequest As WebRequest = WebRequest.Create(strURL)
            objRequest.Method = "GET"
            objRequest.ContentType = "application/json"
            objRequest.Headers.Add("apiAccessKey", My.MySettings.Default.APIAccessKey)
            'objRequest.AuthenticationLevel = Security.AuthenticationLevel.None
            objRequest.Timeout = 30000

            Dim objResponse As HttpWebResponse = CType(objRequest.GetResponse, HttpWebResponse)
            Dim objStream As Stream = objResponse.GetResponseStream()
            Dim objReader As StreamReader = New StreamReader(objStream)
            strXML = objReader.ReadToEnd()

            objReader.Close()
            objStream.Close()
            objResponse.Close()

            Dim objDOM As XmlDocument = New XmlDocument()
            objDOM.LoadXml(strXML)

            Dim objNav As XPathNavigator = objDOM.CreateNavigator()
            Dim objNode As XPathNavigator = objNav.SelectSingleNode("//MATNR") 'Part Number

            If objNode Is Nothing OrElse objNode.InnerXml = "" Then
                Return Nothing
            Else
                objNode = objNav.SelectSingleNode("//AUFNR") 'Order Number
                If objNode.InnerXml <> "" Then
                    cableAssembly.OrderNumber = objNode.InnerXml
                Else
                    cableAssembly.OrderNumber = ""
                End If
                objNode = objNav.SelectSingleNode("//SERNBR") 'Serial Number
                cableAssembly.SerialNumber = objNode.InnerXml
                objNode = objNav.SelectSingleNode("//MATNR") 'Part Number
                cableAssembly.PartNumber = objNode.InnerXml
                objNode = objNav.SelectSingleNode("//WERKS") 'Plant
                cableAssembly.Plant = objNode.InnerXml
                objNode = objNav.SelectSingleNode("//ERDAT") 'Order Date
                cableAssembly.OrderDate = objNode.InnerXml
                objNode = objNav.SelectSingleNode("//J_2CELNG") 'Length
                cableAssembly.Length = objNode.InnerXml
                objNode = objNav.SelectSingleNode("//MEINS") 'Unit of Measurement
                cableAssembly.UOM = objNode.InnerXml
                Return cableAssembly
            End If
        Catch ex As Exception
            Throw New Exception("CableAssembly.LookUpSAPData()::" & ex.Message)
        End Try
    End Function
End Class
