Public Class SAPCheck
    Public Class Request
        Public Class CheckSerialNumber
            Private _material As String
            Private _orderId As String 'maybe null
            Private _plant As String
            Private _serialNumber As String
            Public Property Material As String
                Get
                    Return _material
                End Get
                Set(value As String)
                    _material = value
                End Set
            End Property

            Public Property OrderId As String
                Get
                    Return _orderId
                End Get
                Set(value As String)
                    _orderId = value
                End Set
            End Property

            Public Property Plant As String
                Get
                    Return _plant
                End Get
                Set(value As String)
                    _plant = value
                End Set
            End Property

            Public Property SerialNumber As String
                Get
                    Return _serialNumber
                End Get
                Set(value As String)
                    _serialNumber = value
                End Set
            End Property
        End Class
    End Class


    Public Interface ICheckSerialNumber

        Function VerifyValidInSystem(req As Request.CheckSerialNumber) As Response.CheckSerialNumber


    End Interface

    Public Class CheckSerialNumber
        Implements ICheckSerialNumber
        'http://stscat_user:STsW3rd!@cdcb2bappd1:5182/CATSTS?RFC=Z_BAPI_CATS_SERIAL_CHK&MATERIAL=123&PLANT=215&SERIALNO=123546789&ORDERID=12399
        'dev 'Private _httpString As String = "http://stscat_user:STsW3rd!@cdcb2bappd1:5182/CATSTS?RFC=Z_BAPI_CATS_SERIAL_CHK&" 'MATERIAL=123&PLANT=215&SERIALNO=123546789&ORDERID=12399"
        'test 'Private _httpString As String = "http://stscat_user:STsW3rd!@inttestb2bintegration.commscope.com:5182/CATSTS?RFC=Z_BAPI_CATS_SERIAL_CHK&" 'MATERIAL=123&PLANT=215&SERIALNO=123546789&ORDERID=12399"
        Private _httpString As String = "http://stscat_user:STsW3rd!@intb2bintegration.commscope.com:5182/CATSTS?RFC=Z_BAPI_CATS_SERIAL_CHK&" 'MATERIAL=123&PLANT=215&SERIALNO=123546789&ORDERID=12399"
        Private Function buildRequestString(req As Request.CheckSerialNumber) As String
            Try
                Dim rsp As String

                rsp = _httpString
                rsp += "MATERIAL=" & req.Material & "&"
                rsp += "PLANT=" & req.Plant & "&"
                rsp += "SERIALNO=" & req.SerialNumber
                'rsp += "ORDERID=" & req.OrderId

                If req.OrderId IsNot Nothing AndAlso req.OrderId <> "" Then
                    rsp += "&ORDERID=" & req.OrderId
                End If

                Return rsp

            Catch ex As Exception
                Throw New Exception("buildRequestString() - " & ex.Message)
            End Try
        End Function
        Private Function getElementValue(parentElement As XElement, elementName As String) As String
            Try
                Dim elem As XElement

                Try
                    elem = parentElement.Element(elementName)
                Catch ex As Exception
                    Return Nothing
                End Try

                Return elem.Value

            Catch ex As Exception
                Throw New Exception("getElementValue() - " & ex.Message)
            End Try

        End Function
        Private Function parseResponseString(req As String) As Response.CheckSerialNumber
            Try
                '--------------successful
                '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?> 
                '<getCATS_SERIAL_CHKReply>
                '  <MATERIAL>5NPX1006F</MATERIAL> 
                '  <ORDERID>6002351597</ORDERID> 
                '  <PLANT>CN10</PLANT> 
                '  <SERIALNO>1AD1006A030470</SERIALNO> 
                '  <RETURN>
                '    <item>
                '      <MESSAGE>Request has been successful</MESSAGE> 
                '    </item>
                '  </RETURN>
                '</getCATS_SERIAL_CHKReply>

                '---------------error
                '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?> 
                '<getCATS_SERIAL_CHKReply>
                '  <MATERIAL>5NPX1006F</MATERIAL> 
                '  <ORDERID>6002351597</ORDERID> 
                '  <PLANT>CN10</PLANT> 
                '  <SERIALNO>AAA</SERIALNO> 
                '  <RETURN>
                '    <item>
                '      <TYPE>E</TYPE> 
                '      <ID>ZR</ID> 
                '      <NUMBER>999</NUMBER> 
                '      <MESSAGE>Scanned SN: AAA missing for MID:  5NPX1006F</MESSAGE> 
                '      <LOG_MSG_NO>000000</LOG_MSG_NO> 
                '      <MESSAGE_V1>Scanned SN:</MESSAGE_V1> 
                '      <MESSAGE_V2>AAA</MESSAGE_V2> 
                '      <MESSAGE_V3>missing For MID:</MESSAGE_V3> 
                '      <MESSAGE_V4>5NPX1006F</MESSAGE_V4> 
                '      <ROW>0</ROW> 
                '      <SYSTEM>CS3005</SYSTEM> 
                '    </item>
                '  </RETURN>
                '</getCATS_SERIAL_CHKReply>
                Dim rsp As New Response.CheckSerialNumber
                Dim xmlDoc As New XDocument
                Dim xmlE As XElement

                xmlDoc = XDocument.Parse(req)

                xmlE = xmlDoc.Element("getCATS_SERIAL_CHKReply")

                rsp.RequestField.Material = getElementValue(xmlE, "MATERIAL")
                rsp.RequestField.OrderId = getElementValue(xmlE, "ORDERID")
                rsp.RequestField.Plant = getElementValue(xmlE, "PLANT")
                rsp.RequestField.SerialNumber = getElementValue(xmlE, "SERIALNO")

                Dim item As New Response.CheckSerialNumber.CheckSerialNumberItem

                xmlE = xmlE.Element("RETURN").Element("item")

                item.Type = getElementValue(xmlE, "TYPE")
                item.Message = getElementValue(xmlE, "MESSAGE")
                rsp.Items.Add(item)

                Return rsp


            Catch ex As Exception
                Throw New Exception("parseResponseString() - " & ex.Message)
            End Try
        End Function
        Public Function VerifyValidInSystem(request As Request.CheckSerialNumber) As Response.CheckSerialNumber Implements ICheckSerialNumber.VerifyValidInSystem
            Try
                Dim cnn As New Net.WebClient
                Dim req, rsp As String

                req = buildRequestString(request)
                cnn.Credentials = New System.Net.NetworkCredential("stscat_user", "STsW3rd!") '用户名，密码

                rsp = cnn.DownloadString(req)

                Return parseResponseString(rsp)

            Catch ex As Exception

                Throw New Exception("VerifyValidInSystem() - " & ex.Message)

            End Try

        End Function
    End Class


    Public Class Response

        Public Class CheckSerialNumber
            Public Class CheckSerialNumberItem
                Private _type As String
                'Public _id As String
                'Public _number As String
                Private _message As String

                Public Property Message As String
                    Get
                        Return _message
                    End Get
                    Set(value As String)
                        _message = value
                    End Set
                End Property

                Public Property Type As String
                    Get
                        Return _type
                    End Get
                    Set(value As String)
                        _type = value
                    End Set
                End Property
                'Public _log_msg_no As String
                'Public _row As String
                'Public _system As String
            End Class


            Private _requestField As New Request.CheckSerialNumber
            Private _items As New List(Of CheckSerialNumberItem)

            Public Property RequestField As Request.CheckSerialNumber
                Get
                    Return _requestField
                End Get
                Set(value As Request.CheckSerialNumber)
                    _requestField = value
                End Set
            End Property

            Public ReadOnly Property Items As List(Of CheckSerialNumberItem)
                Get
                    Return _items
                End Get
            End Property
        End Class
    End Class


End Class
