Imports System.Net
Imports System.Xml
Imports System.IO
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class KaelusBPIMDevice
  Inherits Instrument
    Implements IIMDDevice

    Private Token As String
    Private Legacy As Boolean = False
    Private Structure _AnalyzerInfo
        Dim Name As String
        Dim Legacy As Boolean
        Dim BandIndex As Integer
    End Structure
    Public Structure _CalInfo
        Dim version As Integer
        Dim caldate As String
        Dim sn As String
    End Structure
    '<Sample Count="31" Total="47" Finished="FALSE" F1="1930.000000" F2="1990.000000" FStep="1.00" SweepLeg="F2Down">
    '<TX1 Frequency="1930.000000" Power_dBm="0.00" Power_Watts="0.00" Squelched="FALSE" Unleveled="FALSE" CheckTx="FALSE" ToneOn="FALSE" TempC="17.0" />
    '<TX2 Frequency="1960.000000" Power_dBm="0.00" Power_Watts="0.00" Squelched="FALSE" Unleveled="FALSE" CheckTx="FALSE" ToneOn="FALSE" TempC="17.0" />
    '<IM Frequency="1900.000000" Power_dBm="-143.56" Power_dBc="-999.00" PeakPower_dBm="-140.75" PeakPower_dBc="-999.00" IM_Order="3"  />
    '</Sample>

    Public Structure _Sample
        Public Structure _TX
            Dim Frequency As Double
            Dim Power_dBm As Double
            Dim Power_Watts As Double
            Dim Squelched As Boolean
            Dim Unleveled As Boolean
            Dim CheckTx As Boolean
            Dim ToneOn As Boolean
            Dim TempC As Double
        End Structure
        Public Structure _IM
            Dim Frequency As Double
            Dim Power_dBm As Double
            Dim Power_dBc As Double
            Dim PeakPower_dBm As Double
            Dim PeakPower_dBc As Double
            Dim IM_Order As Integer
        End Structure

        Dim Count As Integer
        Dim Total As Integer
        Dim Finished As Boolean
        Dim F1 As Double
        Dim F2 As Double
        Dim FStep As Double
        Dim SweepLeg As String
        Dim TX1 As _TX
        Dim TX2 As _TX
        Dim IM As _IM
    End Structure
    Public Structure _TestSetDef
        Public Structure _General
            Public Structure _Options
                Dim E As Boolean
                Dim K As Boolean
                Dim H As Boolean
                Dim W As Boolean
                Dim F As Boolean
            End Structure
            Dim ID As Integer
            Dim Model As String
            Dim Dual_Port As Boolean
            Dim Options_String As String
            Dim Options As _Options
            Dim Cal_File As String
        End Structure
        Public Structure _Receiver
            Dim Fstart_MHz As Double
            Dim Fstop_MHz As Double
            Dim Fstep_MHz As Double
            Dim IF_MHz As Double
        End Structure
        Public Structure _RX_Band
            Dim Fstart_MHz As Double
            Dim Fstop_MHz As Double
        End Structure
        Public Structure _TX
            Dim Fstart_MHz As Double
            Dim Fstop_MHz As Double
            Dim Pmax_dBm As Double
            Dim Pstep_dB As Double
        End Structure
        Dim General As _General
        Dim Receiver As _Receiver
        Dim RX_Band As _RX_Band
        Dim TX_1 As _TX
        Dim TX_2 As _TX
        Dim Low_side As Boolean
    End Structure
    Public Structure _PIMTrace
        Public Structure _Point
            Dim X As Double
            Dim Y As Double
        End Structure
        Dim F2Down As List(Of _Point)
        Dim F1Up As List(Of _Point)
        Dim MinX As Double
        Dim MaxX As Double
        Dim MinY As Double
        Dim MaxY As Double
    End Structure
    Private TestSetDef As _TestSetDef
    Private AnalyzerDic As Dictionary(Of String, _AnalyzerInfo)
    Private pTX1 As Boolean
    Private pTX2 As Boolean
    Private fTX1 As Double
    Private fTX2 As Double
    Private powTX1 As Double
    Private powTX2 As Double

    Private Function GetToken() As String
        Dim res As String = ""
        Dim cmd As String = String.Format("http://{0}/api/connect/<Legacy?>", Me.Address)
        GenerateEventSentMessage(cmd)
        Dim response As String = SendRev(cmd)
        GenerateEventSentMessage(response)
        Dim xDoc As New XmlDocument
        xDoc.LoadXml(response)

        For Each nd As XmlNode In xDoc.FirstChild.FirstChild.ChildNodes
            If nd.Name = "Token" Then
                res = nd.InnerText
                Exit For
            End If
        Next
        Return res
    End Function

    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
        Try
            'Dim response As String = SendRev(String.Format("http://{0}/api/connect/<Legacy?>", Me.Address)) 'client.DownloadString(String.Format("http://{0}/api/connect/<Legacy?>", Me.Address))
            'GenerateEventSentMessage(response)
            'Dim xDoc As New XmlDocument
            'xDoc.LoadXml(response)

            'Token = ""
            'For Each nd As XmlNode In xDoc.FirstChild.FirstChild.ChildNodes
            '  If nd.Name = "Token" Then
            '    Token = nd.InnerText
            '    Exit For
            '  End If
            'Next
            Token = GetToken()
            If Token = "" Then
                Return False
            Else
                AnalyzerDic = New Dictionary(Of String, _AnalyzerInfo)
                GetAnalyzers()
                TestSetDef = GetTestSetDef()
                _ModelNumber = TestSetDef.General.Model
                _SerialNumber = GetCalInfo.sn
                pTX1 = False
                pTX2 = False
                SetModeStandard()
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        Return ""
    End Function
    Public Property FreqBand As Integer Implements IIMDDevice.FreqBand

        Get
            Return 0
        End Get
        Set(value As Integer)

        End Set
    End Property
    Public Property ImdOrder() As Integer Implements IIMDDevice.ImdOrder

        Get
            Return GetImOrder(0)
        End Get
        Set(value As Integer)
            SetImOrder(value)
        End Set
    End Property
    'Public ReadOnly Property Firmware As Double Implements IIMD_INSTRUMENT.Firmware
    '  Get

    '  End Get
    'End Property
    'Public ReadOnly Property InstrumentType As IIMD_INSTRUMENT.enumInstrType Implements IIMD_INSTRUMENT.InstrumentType
    '  Get

    '  End Get
    'End Property
    'ReadOnly Property ReadIMDDBM() As Double Implements IIMDDevice.ReadIMDDBM
    '  Get
    '    Return GetImPower(0)
    '  End Get
    'End Property
    Public Overloads Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                SetTxOn(OnOff, pTX2)
            Case IIMDDevice.enumRFPORTS.PORTB
                SetTxOn(pTX1, OnOff)
        End Select
    End Sub
    Public Overloads Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort
        SetTxOn(OnOff1, OnOff2)
    End Sub

    Public Overloads Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
        SetTxOn(OnOff, OnOff)
    End Sub
    Public Sub SetFrequency(PORT As IIMDDevice.enumRFPORTS, FreqMHz As Double) Implements IIMDDevice.SetFrequency
        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                SetTxFreqs(FreqMHz, fTX2)
            Case IIMDDevice.enumRFPORTS.PORTB
                SetTxFreqs(fTX1, FreqMHz)
        End Select
    End Sub
    Public Sub SetFrequency(FreqMHz1 As Double, FreqMHz2 As Double) Implements IIMDDevice.SetFrequency
        SetTxFreqs(FreqMHz1, FreqMHz2)
    End Sub

    Public Sub SetRFPortPowerDBM(PORT As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
        'If PowerDBM > 43 Then Throw New Exception("the power is over limit!")
        Select Case PORT
            Case IIMDDevice.enumRFPORTS.PORTA
                SetTxPower(PowerDBM, powTX2)
            Case IIMDDevice.enumRFPORTS.PORTB
                SetTxPower(powTX1, PowerDBM)
        End Select
    End Sub
    Public Sub SetRFPortPowerDBM(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
        'If PowerDBM1 > 43 Or PowerDBM2 > 43 Then Throw New Exception("the power is over limit!")
        SetTxPower(PowerDBM1, PowerDBM2)
    End Sub
    Public Sub SetTestMode(Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode
        Try
            If Mode = IIMDDevice.enumTESTMODE.REFMODE Then
                SetImMode(0)
            ElseIf Mode = IIMDDevice.enumTESTMODE.TRSMODE Then
                SetImMode(1)
            End If

        Catch ex As Exception
            Throw New Exception("SetTestMode()" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    'ReadOnly Property Analyzers() As String()
    '  Get
    '    Dim res(AnalyzerDic.Keys.Count - 1) As String
    '    For i As Integer = 0 To res.Length - 1
    '      res(i) = AnalyzerDic.Keys(i)
    '    Next
    '    Return res
    '  End Get
    'End Property
    ReadOnly Property TX1Freq() As Double()
        Get
            Dim res() As Double = {TestSetDef.TX_1.Fstart_MHz, TestSetDef.TX_1.Fstop_MHz}
            Return res
        End Get
    End Property
    ReadOnly Property TX2Freq() As Double()
        Get
            Dim res() As Double = {TestSetDef.TX_2.Fstart_MHz, TestSetDef.TX_2.Fstop_MHz}
            Return res
        End Get
    End Property
    ReadOnly Property CalDate() As String
        Get
            Return GetCalInfo.caldate
        End Get
    End Property

    Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
        Get
            Return GetImPower(0)
        End Get
    End Property

    Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
        Get
            Return GetImPowerDbc(0)
        End Get
    End Property

    Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Function SendCommand(ByVal cmd As String) As String
        Try
            Dim fullcmd As String
            fullcmd = String.Format("http://{0}/api/{1}/{2}/{3}", Address, Token, Legacy, cmd)
            GenerateEventSentMessage(fullcmd)
            Dim response As String
            response = SendRev(fullcmd)
            If response = "" Then
                Threading.Thread.Sleep(3000)
                Token = GetToken()
                fullcmd = String.Format("http://{0}/api/{1}/{2}/{3}", Address, Token, Legacy, cmd)
                response = SendRev(fullcmd)
                GenerateEventSentMessage(fullcmd)
            End If
            GenerateEventSentMessage(response)
            Return response
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Function SendRev(ByVal cmd As String) As String
        For i As Integer = 0 To 2
            Try
                Dim client As New WebClient()
                For j As Integer = 1 To 3
                    Dim res As String = client.DownloadString(cmd)
                    If res.Length > 0 Then Return res
                    Threading.Thread.Sleep(1000 * i)
                    GenerateEventSentMessage("Sleep")
                Next
            Catch ex As System.Net.WebException
                GenerateEventSentMessage("WebException")
                Exit For
            End Try
        Next
        Return ""
        'Dim request As HttpWebRequest = HttpWebRequest.Create(cmd)
        'request.Timeout = 10000
        'Dim readStream As New StreamReader(request.GetResponse.GetResponseStream())
        'Return readStream.ReadToEnd
    End Function
    Private Sub GetAnalyzers()
        Dim cmd As String = "GetAnalyzers"
        Dim response As String = SendCommand(cmd)
        Dim xDoc As New XmlDocument
        xDoc.LoadXml(response)

        For Each nd As XmlNode In xDoc.FirstChild.FirstChild.ChildNodes
            If nd.Attributes("Action").Value = cmd Then
                For Each ndv As XmlNode In nd.ChildNodes
                    Select Case ndv.Name
                        Case "String"
                            If ndv.ChildNodes.Count = 0 Then Continue For
                            Dim analyzerinfo As _AnalyzerInfo
                            With ndv.FirstChild
                                analyzerinfo.Name = .Attributes("Name").Value
                                analyzerinfo.Legacy = CBool(.Attributes("Legacy").Value)
                                analyzerinfo.BandIndex = CInt(.Attributes("BandIndex").Value)
                            End With
                            If AnalyzerDic.ContainsKey(analyzerinfo.Name) = False Then
                                AnalyzerDic.Add(analyzerinfo.Name, analyzerinfo)
                            End If
                    End Select
                Next
            Else
                Continue For
            End If
        Next
    End Sub
    Private Function WriteCommandNoResponse(ByVal cmd As String) As Boolean
        Dim response As String = SendCommand(cmd)
        Dim xDoc As New XmlDocument
        xDoc.LoadXml(response)

        Dim response_cmd As String = cmd.Split("?"c)(0)

        For Each nd As XmlNode In xDoc.FirstChild.FirstChild.ChildNodes
            If nd.Attributes("Action").Value = response_cmd Then
                Return True
            Else
                Continue For
            End If
        Next
        Return False
    End Function
    Private Function WriteCommandWithResponse(ByVal cmd As String) As XmlNode
        Dim response As String = SendCommand(cmd)
        Dim xDoc As New XmlDocument
        xDoc.LoadXml(response)

        Dim response_cmd As String = cmd.Split("?"c)(0)

        For Each nd As XmlNode In xDoc.FirstChild.FirstChild.ChildNodes
            If nd.Attributes("Action").Value = response_cmd Then
                Return nd
            Else
                Continue For
            End If
        Next
        Return Nothing
    End Function

    Public Function ResetPeaks() As Boolean
        Return WriteCommandNoResponse("ResetPeaks")
    End Function
    Public Function SetExit() As Boolean
        Return WriteCommandNoResponse("SetExit")
    End Function
    Public Function SetInit() As Boolean
        Return WriteCommandNoResponse("SetInit")
    End Function
    Private Function SetMeasBand(ByVal index As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetMeasBand?Integer={0}", index))
    End Function
    'Private Function SetMeasBand(ByVal model As String) As Boolean
    '  If AnalyzerDic Is Nothing Or AnalyzerDic.Count = 0 Or AnalyzerDic.Keys.Contains(model) = False Then Return False
    '  If SetMeasBand(AnalyzerDic(model).BandIndex) Then
    '    TestSetDef = GetTestSetDef()
    '    _ModelNumber = TestSetDef.General.Model
    '    Return True
    '  Else
    '    Return False
    '  End If
    'End Function
    Public Function SetModeStandard() As Boolean
        Return WriteCommandNoResponse("SetModeStandard")
    End Function
    Public Function SetModeSweepTx(ByVal fstep As Double, ByVal f1 As Double, ByVal f2 As Double, ByVal fullsweep As Boolean) As Boolean
        Return WriteCommandNoResponse(String.Format("SetModeSweepTx?Double={0}&Double={1}&Double={2}&Boolean={3}", fstep, f1, f2, fullsweep))
    End Function
    Public Function SetPreset() As Boolean
        Return WriteCommandNoResponse("SetPreset")
    End Function

    Public Function SetTimeout(ByVal s As Integer) As Boolean
        Dim nd As XmlNode = WriteCommandWithResponse(String.Format("SetTimeout?Integer={0}", s))

        Dim res As Integer = -1
        If nd Is Nothing Then Return False
        For Each ndv As XmlNode In nd.ChildNodes
            If ndv.Name = "Integer" Then
                res = CInt(ndv.InnerText)
            End If
        Next
        If res = s Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SetTrigger(ByVal forcemeasure As Boolean, ByVal hwtimeout As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetTrigger?Boolean={0}&Integer={1}", forcemeasure, hwtimeout))
    End Function
    Public Function SetTrigger() As Boolean
        Return WriteCommandNoResponse(String.Format("SetTrigger"))
    End Function

    Public Function GetCalInfo() As _CalInfo
        Dim nd As XmlNode = WriteCommandWithResponse("GetCalInfo")
        If nd Is Nothing Then Return Nothing
        Dim calinfo As _CalInfo = Nothing

        Dim strindex As Integer = 0
        For Each ndv As XmlNode In nd.ChildNodes
            Select Case ndv.Name
                Case "Integer"
                    calinfo.version = CInt(ndv.InnerText)
                Case "String"
                    Select Case strindex
                        Case 0
                            calinfo.caldate = ndv.InnerText
                        Case 1
                            calinfo.sn = ndv.InnerText.Replace(vbCrLf, "")
                    End Select
                    strindex += 1
            End Select
        Next
        Return calinfo
    End Function
    Public Function GetError() As Boolean
        Return WriteCommandNoResponse("GetError")
    End Function
    Private Function XmlNode2Dbls(ByVal nd As XmlNode) As Double()
        If nd Is Nothing Then Return Nothing
        Dim count As Integer = nd.ChildNodes.Count
        Dim res(count - 1) As Double
        For i As Integer = 0 To count - 1
            res(i) = CDbl(nd.ChildNodes(i).InnerText)
        Next
        Return res
    End Function
    Private Function XmlNode2bools(ByVal nd As XmlNode) As Boolean()
        If nd Is Nothing Then Return Nothing
        Dim count As Integer = nd.ChildNodes.Count
        Dim res(count - 1) As Boolean
        For i As Integer = 0 To count - 1
            res(i) = CBool(nd.ChildNodes(i).InnerText)
        Next
        Return res
    End Function
    Public Function GetImFreqs() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImFreqs")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetImOrder() As Integer()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImOrder")
        If nd Is Nothing Then Return Nothing

        Dim orderstr As String = ""
        For Each ndv As XmlNode In nd.ChildNodes
            Select Case ndv.Name
                Case "Integer"
                    orderstr = ndv.InnerText
            End Select
        Next

        If orderstr = "" Then Return Nothing
        Dim tmpstr() As String = orderstr.Split(","c)
        Dim res(tmpstr.Length - 1) As Integer
        For i As Integer = 0 To tmpstr.Length - 1
            res(i) = CInt(tmpstr(i))
        Next
        Return res
    End Function
    Public Function GetImPower() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImPower")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetImPowerDbc() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImPowerDbc")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetImPeakPower() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImPeakPower")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetImPeakPowerDbc() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetImPeakPowerDbc")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetImStatus() As Boolean
        Dim nd As XmlNode = WriteCommandWithResponse("GetImStatus")
        If nd Is Nothing Then Return Nothing

        For Each ndv As XmlNode In nd.ChildNodes
            Select Case ndv.Name
                Case "Boolean"
                    Return CBool(ndv.InnerText)
            End Select
        Next
        Return Nothing
    End Function
    Public Function GetSamples() As _Sample()
        Dim nd As XmlNode = WriteCommandWithResponse("GetSamples")
        If nd Is Nothing Then Return Nothing
        Dim res(nd.ChildNodes.Count - 1) As _Sample

        Dim i As Integer = 0
        For Each ndv As XmlNode In nd.ChildNodes
            Select Case ndv.Name
                Case "Sample"
                    res(i).Count = CInt(ndv.Attributes("Count").Value)
                    res(i).Total = CInt(ndv.Attributes("Total").Value)
                    res(i).Finished = CBool(ndv.Attributes("Finished").Value)
                    res(i).F1 = CDbl(ndv.Attributes("F1").Value)
                    res(i).F2 = CDbl(ndv.Attributes("F2").Value)
                    res(i).FStep = CDbl(ndv.Attributes("FStep").Value)
                    res(i).SweepLeg = ndv.Attributes("SweepLeg").Value

                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "TX1"
                                With res(i).TX1
                                    .Frequency = CDbl(ndd.Attributes("Frequency").Value)
                                    .Power_dBm = CDbl(ndd.Attributes("Power_dBm").Value)
                                    .Power_Watts = CDbl(ndd.Attributes("Power_Watts").Value)
                                    .Squelched = CBool(ndd.Attributes("Squelched").Value)
                                    .Unleveled = CBool(ndd.Attributes("Unleveled").Value)
                                    .CheckTx = CBool(ndd.Attributes("CheckTx").Value)
                                    .ToneOn = CBool(ndd.Attributes("ToneOn").Value)
                                    .TempC = CDbl(ndd.Attributes("TempC").Value)
                                End With
                            Case "TX2"
                                With res(i).TX2
                                    .Frequency = CDbl(ndd.Attributes("Frequency").Value)
                                    .Power_dBm = CDbl(ndd.Attributes("Power_dBm").Value)
                                    .Power_Watts = CDbl(ndd.Attributes("Power_Watts").Value)
                                    .Squelched = CBool(ndd.Attributes("Squelched").Value)
                                    .Unleveled = CBool(ndd.Attributes("Unleveled").Value)
                                    .CheckTx = CBool(ndd.Attributes("CheckTx").Value)
                                    .ToneOn = CBool(ndd.Attributes("ToneOn").Value)
                                    .TempC = CDbl(ndd.Attributes("TempC").Value)
                                End With
                            Case "IM"
                                With res(i).IM
                                    .Frequency = CDbl(ndd.Attributes("Frequency").Value)
                                    .Power_dBm = CDbl(ndd.Attributes("Power_dBm").Value)
                                    .Power_dBc = CDbl(ndd.Attributes("Power_dBc").Value)
                                    .PeakPower_dBm = CDbl(ndd.Attributes("PeakPower_dBm").Value)
                                    .PeakPower_dBc = CDbl(ndd.Attributes("PeakPower_dBc").Value)
                                    .IM_Order = CInt(ndd.Attributes("IM_Order").Value)
                                End With
                        End Select
                    Next
            End Select
            i += 1
        Next
        Return res
    End Function
    Public Function GetSamples_Single() As _Sample()
        Dim freqs() As Double = GetTxRange()
        Dim fstep As Double = GetSweepStep(0)

        Dim Count As Integer = 0
        For f As Double = freqs(0) To freqs(1) Step fstep
            Count += 1
        Next
        For f As Double = freqs(3) To freqs(2) Step -fstep
            Count += 1
        Next

        Dim res(Count - 1) As _Sample
        Dim i As Integer = 0
        For f As Double = freqs(0) To freqs(1) Step fstep
            SetTxFreqs(f, freqs(3))
            Dim freq As Double = GetImFreqs(0)
            Dim im As Double = GetImPower(0)
            res(i).Count = i + 1
            res(i).Total = Count
            res(i).Finished = False
            res(i).F1 = f
            res(i).F2 = freqs(3)
            res(i).FStep = fstep
            res(i).SweepLeg = "F1Up"

            With res(i).IM
                .Frequency = freq
                .Power_dBm = im
            End With
            i += 1
        Next

        For f As Double = freqs(3) To freqs(2) Step -fstep
            SetTxFreqs(freqs(0), f)
            Dim freq As Double = GetImFreqs(0)
            Dim im As Double = GetImPower(0)
            res(i).Count = i + 1
            res(i).Total = Count
            res(i).Finished = False
            res(i).F1 = freqs(0)
            res(i).F2 = f
            res(i).FStep = fstep
            res(i).SweepLeg = "F2Down"

            With res(i).IM
                .Frequency = freq
                .Power_dBm = im
            End With
            i += 1
        Next
        Return res
    End Function
    Public Function GetPIMTrace(ByVal IsAuto As Boolean) As _PIMTrace
        Dim res As _PIMTrace
        res.F1Up = New List(Of _PIMTrace._Point)
        res.F2Down = New List(Of _PIMTrace._Point)

        Dim s() As _Sample
        If IsAuto Then
            s = GetSamples()
        Else
            s = GetSamples_Single()
        End If

        For i As Integer = 0 To s.Length - 1
            Dim pt As _PIMTrace._Point
            pt.X = s(i).IM.Frequency
            pt.Y = s(i).IM.Power_dBm
            Select Case s(i).SweepLeg
                Case "F1Up"
                    res.F1Up.Add(pt)
                Case "F2Down"
                    res.F2Down.Add(pt)
            End Select

            If i = 0 Then
                res.MinX = pt.X
                res.MaxX = pt.X
                res.MinY = pt.Y
                res.MaxY = pt.Y
            Else
                res.MinX = Math.Min(pt.X, res.MinX)
                res.MaxX = Math.Max(pt.X, res.MaxX)
                res.MinY = Math.Min(pt.Y, res.MinY)
                res.MaxY = Math.Max(pt.Y, res.MaxY)
            End If
        Next
        Return res
    End Function
    Public Function GetSweepStep() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetSweepStep")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetTestSetDef() As _TestSetDef
        Dim nd As XmlNode = WriteCommandWithResponse("GetTestSetDef")
        If nd Is Nothing Then Return Nothing
        Dim res As _TestSetDef = Nothing

        For Each ndv As XmlNode In nd.FirstChild.FirstChild
            Select Case ndv.Name
                Case "General"
                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "ID"
                                res.General.ID = CInt(ndd.InnerText)
                            Case "Model"
                                res.General.Model = ndd.InnerText
                            Case "Dual_Port"
                                res.General.Dual_Port = CBool(ndd.InnerText)
                            Case "Options_String"
                                res.General.Options_String = ndd.InnerText
                            Case "Options"
                                For Each nddd As XmlNode In ndd.ChildNodes
                                    Select Case nddd.Name
                                        Case "E"
                                            res.General.Options.E = Convert.ToBoolean(nddd.InnerText)
                                        Case "K"
                                            res.General.Options.K = Convert.ToBoolean(nddd.InnerText)
                                        Case "H"
                                            res.General.Options.H = Convert.ToBoolean(nddd.InnerText)
                                        Case "W"
                                            res.General.Options.W = Convert.ToBoolean(nddd.InnerText)
                                        Case "F"
                                            res.General.Options.F = Convert.ToBoolean(nddd.InnerText)
                                    End Select
                                Next
                            Case "Cal_File"
                                res.General.Cal_File = ndd.InnerText
                        End Select
                    Next
                Case "Receiver"
                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "Fstart_MHz"
                                res.Receiver.Fstart_MHz = CDbl(ndd.InnerText)
                            Case "Fstop_MHz"
                                res.Receiver.Fstop_MHz = CDbl(ndd.InnerText)
                            Case "Fstep_MHz"
                                res.Receiver.Fstep_MHz = CDbl(ndd.InnerText)
                            Case "IF_MHz"
                                res.Receiver.IF_MHz = CDbl(ndd.InnerText)
                        End Select
                    Next
                Case "RX_Band"
                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "Fstart_MHz"
                                res.RX_Band.Fstart_MHz = CDbl(ndd.InnerText)
                            Case "Fstop_MHz"
                                res.RX_Band.Fstop_MHz = CDbl(ndd.InnerText)
                        End Select
                    Next
                Case "TX_1"
                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "Fstart_MHz"
                                res.TX_1.Fstart_MHz = CDbl(ndd.InnerText)
                            Case "Fstop_MHz"
                                res.TX_1.Fstop_MHz = CDbl(ndd.InnerText)
                            Case "Pmax_dBm"
                                res.TX_1.Pmax_dBm = CDbl(ndd.InnerText)
                            Case "Pstep_dB"
                                res.TX_1.Pstep_dB = CDbl(ndd.InnerText)
                        End Select
                    Next
                Case "TX_2"
                    For Each ndd As XmlNode In ndv.ChildNodes
                        Select Case ndd.Name
                            Case "Fstart_MHz"
                                res.TX_2.Fstart_MHz = CDbl(ndd.InnerText)
                            Case "Fstop_MHz"
                                res.TX_2.Fstop_MHz = CDbl(ndd.InnerText)
                            Case "Pmax_dBm"
                                res.TX_2.Pmax_dBm = CDbl(ndd.InnerText)
                            Case "Pstep_dB"
                                res.TX_2.Pstep_dB = CDbl(ndd.InnerText)
                        End Select
                    Next
                Case "Low-side"
                    res.Low_side = Convert.ToBoolean(ndv.InnerText)
            End Select
        Next
        Return res
    End Function
    Public Function GetTxFreqs() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetTxFreqs")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetTxOn() As Boolean()
        Dim nd As XmlNode = WriteCommandWithResponse("GetTxOn")
        Return XmlNode2bools(nd)
    End Function
    Public Function GetTxPower() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetTxPower")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetTxStatus() As Boolean()
        Dim nd As XmlNode = WriteCommandWithResponse("GetTxStatus")
        Return XmlNode2bools(nd)
    End Function
    Public Function GetTxRange() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetTxRange")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function GetRxRange() As Double()
        Dim nd As XmlNode = WriteCommandWithResponse("GetRxRange")
        Return XmlNode2Dbls(nd)
    End Function
    Public Function SetAlc(ByVal alc As Boolean) As Boolean
        Return WriteCommandNoResponse(String.Format("SetAlc?Boolean={0}", alc))
    End Function
    Public Function SetDutPort(ByVal port As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetAlc?Integer={0}", port))
    End Function
    Public Function SetImAvg(ByVal avglevel As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetImAvg?Integer={0}", avglevel))
    End Function
    Public Function SetImMode(ByVal mode As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetImMode?Integer={0}", mode))
    End Function
    Public Function SetImOrder(ByVal orde As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetImOrder?Integer={0}", orde))
    End Function
    Public Function SetPxx(ByVal xx As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetPxx?Integer={0}", xx))
    End Function
    Public Function SetSettlingTime(ByVal ms As Integer) As Boolean
        Return WriteCommandNoResponse(String.Format("SetSettlingTime?Integer={0}", ms))
    End Function
    Public Function SetSingleIm(ByVal one As Boolean) As Boolean
        Return WriteCommandNoResponse(String.Format("SetSingleIm?Boolean={0}", one))
    End Function
    Public Function SetSweepStep(ByVal mhz As Double) As Boolean
        Return WriteCommandNoResponse(String.Format("SetSweepStep?Double={0}", mhz))
    End Function
    Public Function SetTxFreqs(ByVal f1 As Double, ByVal f2 As Double) As Boolean
        fTX1 = f1
        fTX2 = f2
        Return WriteCommandNoResponse(String.Format("SetTxFreqs?Double={0}&Double={1}", f1, f2))
    End Function
    Public Function SetTxOn(ByVal f1 As Boolean, ByVal f2 As Boolean) As Boolean
        pTX1 = f1
        pTX2 = f2
        Return WriteCommandNoResponse(String.Format("SetTxOn?Boolean={0}&Boolean={1}", f1, f2))
    End Function
    Public Function SetTxPower(ByVal f1 As Double, ByVal f2 As Double) As Boolean
        powTX1 = f1
        powTX2 = f2
        Return WriteCommandNoResponse(String.Format("SetTxPower?Double={0}&Double={1}", f1, f2))
    End Function
    Public Function SetVaHold(ByVal hold As Boolean) As Boolean
        Return WriteCommandNoResponse(String.Format("SetVaHold?Boolean={0}", hold))
    End Function
    Public Overrides Sub Close() Implements IIMDDevice.Close
        SetTxOn(False, False)
        If AnalyzerDic IsNot Nothing Then
            AnalyzerDic.Clear()
            AnalyzerDic = Nothing
        End If
    End Sub

    Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm
        Throw New NotImplementedException()
    End Sub

    Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc
        Throw New NotImplementedException()
    End Sub

    Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

    End Sub

    Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String, Optional fixF As Single = 0, Optional stepF As Single = 0, Optional duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd

    End Sub

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp

    End Sub

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function
End Class
