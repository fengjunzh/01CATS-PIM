Imports Renci.SshNet
Imports Renci.SshNet.Common
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Linq
Imports AndrewIntegratedProducts.InstrumentsFramework

Public Class ZuluPIM
    'This class first set band, beacuse Zulu judge whether the band in the list or not    then read unitprofile  
    Inherits Instrument
    Implements IIMDDevice

    Private userName As String = "root"
    Private passWord As String = "ION4you1"
    Private expectEnd As String = "dSMR>"
    Private _SshClient As SshClient = Nothing
    Private _timeOutSec As Integer = 5
    Private _ShellStream As ShellStream = Nothing
    Private _P1, _P2, _F1, _F2 As Double
    'Private _PowerPA As Single
    Private _PowerPA, _PowerPB As Single '20181123
    Private _ImdBoxMode As String = "" 'tell the different overtime upseep downsweep
    Private _lastBandName As String = ""
    Private _currentBandName As String 'Current band when you testing
    Private _stUnitInfor As clsUnitInfor
    Private _bySinglePointsFlag As Boolean = False ' read one point TX Freq then read rx value ,one point by one point.

    Private Class FilterProfile         'ben
        Public _listOneFilterbandName As New List(Of String) 'one filter contain multi-bands
        Public _filterSerialNumber As String
    End Class

    Private Class clsUnitInfor       'ben
        Public _listUnitBandName As New List(Of String) 'how many bands on one master 
        Public _dicFilterBandProfile As New Dictionary(Of String, FilterProfile)
    End Class

    Public Overrides Sub Close() Implements IIMDDevice.Close
        If _ShellStream IsNot Nothing Then
            _ShellStream.Close()
            _ShellStream = Nothing
        End If

        If _SshClient IsNot Nothing Then
            _SshClient.Disconnect()
            _SshClient = Nothing
        End If
    End Sub

    Private _ImdOrder As Integer = 3
    Public Property ImdOrder As Integer Implements IIMDDevice.ImdOrder
        Get
            Return _ImdOrder
        End Get
        Set(value As Integer)
            Send_And_Read(String.Format("pimtest rx0 filtmode {0} {1}", "0x103", 10))
            Send_And_Read(String.Format("pimsweep order {0}", value))
            Send_And_Read(String.Format("pim2tone order {0}", value))
            _ImdOrder = value
        End Set
    End Property

    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
        Try
            Dim connInfo As New PasswordConnectionInfo(Me.Address, Me.userName, Me.passWord)
            _stUnitInfor = New clsUnitInfor
            _SshClient = New SshClient(connInfo)
            If _SshClient.IsConnected = False Then
                _SshClient.Connect()
            End If
            _ShellStream = _SshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024)
            Send_And_Read("/etc/init.d/watchdog stop;/opt/andrew/bin/console")
            Send_And_Read(String.Format("timestamp -s {0:yyyy MM dd HH mm ss}", Now))
            'Send_And_Read(String.Format("timestamp {0:yyyy MM dd HH mm ss}", Now))
            Dim ms As String = Send_And_Read(String.Format("alarm du"))
            Dim mc As MatchCollection = Regex.Matches(ms, "PA[\d]")
            'Dim mc As MatchCollection = Regex.Matches(ms, "(PA[\d]+ HW alarm)")
            If mc.Count <> 0 Then Throw New Exception("There is alarm : " & (IIf(mc.Item(0).Value = "", "PA", mc.Item(0).Value)).ToString)
            'If mc.Count <> 0 Then Throw New Exception("There is alarm : " & mc.Item(0).Groups(1).Value) 
            Return True
        Catch ex As Exception
            Throw New Exception("Open(): " & vbCrLf & ex.Message)
        End Try
    End Function

    ReadOnly Property Init_Status() As Boolean ' Open 之后需要再判断下是否成功， add tony 2019 0522
        Get
            Dim tmpstr As String = Send_And_Read("pimtest du init_done").ToUpper()
            If tmpstr.Contains("FALSE") Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property


    Private Sub ListBandName(ByVal mc As MatchCollection)       'ben
        For Each m As Match In mc
            _stUnitInfor._listUnitBandName.Add(m.Groups(1).Value.ToUpper)
        Next
    End Sub

    Private Sub FilterBandProfile(ByVal resp As String)        'ben
        Dim mc As MatchCollection = Regex.Matches(resp, "(\S+) @ switch port (\d)")
        For i As Integer = 0 To mc.Count - 1
            If mc(i).Groups(1).Value.ToLower.Contains("board") Then Continue For
            Dim temStr As String = ""
            If i < mc.Count - 1 Then
                temStr = resp.Substring(mc(i).Index, mc(i + 1).Index - mc(i).Index)
            Else
                temStr = resp.Substring(mc(i).Index)
            End If
            Dim _filterProfile As New FilterProfile
            Dim msSN As Match = Regex.Match(temStr, "serial_number: (\S+)")
            Dim msBands As MatchCollection = Regex.Matches(temStr, "band_name[_\d]*: (\S+)")
            If msSN.Success = True Then
                _filterProfile._filterSerialNumber = msSN.Groups(1).Value
                _SerialNumber = msSN.Groups(1).Value
            Else
                _filterProfile._filterSerialNumber = "NULLB000"
                _SerialNumber = "NULLB000"
            End If
            For Each m As Match In msBands
                _filterProfile._listOneFilterbandName.Add(m.Groups(1).Value.ToUpper)
            Next
            For j As Integer = 0 To _filterProfile._listOneFilterbandName.Count - 1 'one filter maybe contain multi-bands
                _stUnitInfor._dicFilterBandProfile.Add(_filterProfile._listOneFilterbandName.Item(j).ToString, _filterProfile)
            Next
        Next
    End Sub

    Private Sub FilterVesion()        'ben
        Dim temfw As String = Send_And_Read(String.Format("ver"))
        Dim maFw As Match = Regex.Match(temfw, "version: (\S+)")
        If maFw.Success = False Then
            _FWRevision = ""
        Else
            _FWRevision = maFw.Groups(1).Value
        End If
    End Sub

    Public ReadOnly Property FilterBandName() As String
        Get
            _timeOutSec = 30
            Dim resp As String = Send_And_Read("lna0mcu eedata v all")
            Dim mc As MatchCollection = Regex.Matches(resp, "band_name[_\d]*: (\S+)")
            If mc.Count = 0 Then
                ' Send_And_Read("lna0mcu eedata update")
                System.Threading.Thread.Sleep(800)
                resp = Send_And_Read("lna0mcu eedata v all")
                mc = Regex.Matches(resp, "band_name[_\d]*: (\S+)")
            End If
            If mc.Count = 0 Then Throw New Exception("Can't find any bands on present unit .")
            ListBandName(mc)
            FilterBandProfile(resp)
            FilterVesion()
            Dim _bandlist As New List(Of String)
            If _stUnitInfor._dicFilterBandProfile.Keys.Contains(_currentBandName) = False Then Throw New Exception(_currentBandName & " can't find in the unit .")
            _bandlist = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName)._listOneFilterbandName

            _SerialNumber = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName)._filterSerialNumber 'add by tony

            'filter name define every band conbine use symbol "-"
            Dim _filterName As String = ""
            If _bandlist.Count > 1 Then
                _filterName = _bandlist(0) & "-" & _bandlist(1)
            End If
            _ModelNumber = "ZuluPimTester " & CStr(IIf(_bandlist.Count > 1, _filterName, _bandlist(0)))
            Return _ModelNumber
        End Get
    End Property

    Private ReadOnly Property GetCurrentBandName As String        'ben
        Get
            Dim temStr As String = Send_And_Read(String.Format("bandconfig v"))
            System.Threading.Thread.Sleep(90)
            Dim ma As Match = Regex.Match(temStr, "Current band: (\S+)")
            If ma.Success = False Then
                temStr = Send_And_Read(String.Format("bandconfig v"))
                System.Threading.Thread.Sleep(90)
                ma = Regex.Match(temStr, "Current band: (\S+)")
                If ma.Success = False Then Return Nothing
            End If
            Return Trim(ma.Groups(1).Value.ToUpper)
        End Get
    End Property

    Private Function ReadSinglePointsRssi() As Double        'ben
        Dim resp As String

        resp = Send_And_Read(String.Format("pimsweep {0} -10 {1} -10", _F1, _F2))
        Dim ms As MatchCollection = Regex.Matches(resp, "rssi=\S+dBm, rssi2=(\S+)dBm")
        Dim imdvalues(ms.Count \ 2 - 1) As Double
        For i As Integer = 0 To imdvalues.Length - 1
            imdvalues(i) = CDbl(ms(i).Groups(1).Value)
        Next
        Return imdvalues.Max()
    End Function

    Private Function ReadSweepFreqRssi() As Double        'ben

        Dim patten As String = ""
        Dim res(1) As Double
        Dim resstr As String
        patten += "RX0 ch A, freq=(\S+)" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm"
        resstr = _ShellStream.Expect(New Regex(patten), TimeSpan.FromSeconds(5))
        If resstr Is Nothing Then Return Nothing
        GenerateEventSentMessage(resstr)
        Dim ma As Match = Regex.Match(resstr, patten)
        If ma.Success = False Then Return Nothing
        res(0) = CDbl(ma.Groups(1).Value)
        Dim imdval(2) As Double
        For i As Integer = 0 To imdval.Length - 1
            If Double.TryParse(ma.Groups(2 + i).Value, imdval(i)) = False Then
                imdval(i) = Double.MinValue
            End If
        Next
        res(1) = imdval(0)
        For i As Integer = 1 To imdval.Length - 1
            res(1) = Math.Max(res(1), imdval(i))
        Next
        Return res(1)
    End Function

    Private Function ReadSweepTime() As Double        'ben

        Dim patten As String = ""
        patten += "RX0 ch A, freq=(\S+)" & vbCrLf
        patten += "Reg=\S+, rssi=\S+dBm, rssi2=(\S+)dBm"
        Dim resstr As String = _ShellStream.Expect(New Regex(patten), TimeSpan.FromSeconds(1.1))
        If resstr Is Nothing Then Return Nothing
        GenerateEventSentMessage(resstr)
        Dim ma As Match = Regex.Match(resstr, patten)
        If ma.Success = False Then Return Nothing
        Dim res(1) As Double
        res(0) = CDbl(ma.Groups(1).Value)
        Dim imdval As Double
        If Double.TryParse(ma.Groups(2).Value, imdval) Then
            res(1) = imdval
        Else
            res(1) = Double.MinValue
        End If
        Return res(1)
    End Function

    Public ReadOnly Property ReadImd_dBc As Double Implements IIMDDevice.ReadImd_dBc
        Get
            Try
                Dim resi As Double
                If _bySinglePointsFlag = True Then
                    resi = ReadSinglePointsRssi() - _PowerPA
                Else
                    If _ImdBoxMode = "UPSWEEP" OrElse _ImdBoxMode = "DOWNSWEEP" Then
                        resi = ReadSweepFreqRssi() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweepfrequency : Can't read validate value .")
                    ElseIf _ImdBoxMode = "OVERTIME" Then
                        resi = ReadSweepTime() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweeptime : Can't read validate value .")
                    End If
                End If
                'antenna need set the limmter
                If resi < -175 Then
                    resi = -175
                End If
                'Return Math.Round(resi, 3)
                ' Return resi
                Return Math.Round(resi, 3) '20181127
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Public ReadOnly Property ReadImd_dBm As Double Implements IIMDDevice.ReadImd_dBm
        Get
            Try
                Dim resi As Double
                If _bySinglePointsFlag = True Then
                    resi = ReadSinglePointsRssi() - _PowerPA
                Else
                    If _ImdBoxMode = "UPSWEEP" OrElse _ImdBoxMode = "DOWNSWEEP" Then
                        resi = ReadSweepFreqRssi() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweepfrequency : Can't read validate value .")
                    ElseIf _ImdBoxMode = "OVERTIME" Then
                        resi = ReadSweepTime() - _PowerPA
                        If resi = Nothing Then Throw New Exception("Sweeptime : Can't read validate value .")
                    End If
                End If
                'antenna need set the limmter
                If resi < -132 Then
                    resi = -132
                End If
                Return Math.Round(resi, 3) '20181123
                ' Return resi
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Public Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort

        If OnOff1 = True And OnOff2 = False Then
            If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
            Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, _P1, _F2, -100))
            Send_And_Read("pimtest gainloop tx0 on")
            Threading.Thread.Sleep(500)
        End If
        If OnOff1 = False And OnOff2 = True Then
            If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
            Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, -100, _F2, _P2))
            Send_And_Read("pimtest gainloop tx1 on")
            Threading.Thread.Sleep(500)
        End If
        If OnOff1 = False And OnOff2 = False Then
            Send_And_Read("pimtest req txoff")
            Send_And_Read(String.Format("tx{0} m {1}", 0, 0)) ' add 20190306
            Send_And_Read(String.Format("tx{0} m {1}", 1, 0)) ' add 20190306

        End If

        'PA_OnOff(0, OnOff1) = OnOff1
        'PA_OnOff(1, OnOff2) = OnOff2
    End Sub

    Public Sub RFPowerOnOff_TwoPorts(OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_TwoPorts
        PA_OnOff(0) = OnOff
        PA_OnOff(1) = OnOff
    End Sub

    Private Function Send_And_Read(cmd As String) As String Implements IIMDDevice.Send_And_Read
        Try
            _ShellStream.WriteLine(cmd)
            Dim res As String = _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(_timeOutSec))
            ' GenerateEventSentMessage(res)
            Return res
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SetFrequency(freqMHz1 As Double, freqMHz2 As Double) Implements IIMDDevice.SetFrequency
        _F1 = Math.Round(freqMHz1, 1)
        _F2 = Math.Round(freqMHz2, 1)

    End Sub

    Public Sub SetFrequency(Port As IIMDDevice.enumRFPORTS, freqMHz As Double) Implements IIMDDevice.SetFrequency
        Select Case Port
            Case IIMDDevice.enumRFPORTS.PORTA
                _F1 = Math.Round(freqMHz, 1)
            Case IIMDDevice.enumRFPORTS.PORTB
                _F2 = Math.Round(freqMHz, 1)
        End Select
    End Sub

    Public Sub SetRFPower(PowerDBM1 As Double, PowerDBM2 As Double) Implements IIMDDevice.SetRFPower
        _PowerPA = CSng(PowerDBM1)
        _PowerPB = CSng(PowerDBM2)
        SetRFPower(IIMDDevice.enumRFPORTS.PORTA, PowerDBM1)
        SetRFPower(IIMDDevice.enumRFPORTS.PORTB, PowerDBM2)
    End Sub

    Public Sub SetRFPower(Port As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
        PowerDBM -= 53
        Dim MaxPower As Double = -2
        If PowerDBM > MaxPower Then PowerDBM = MaxPower
        Select Case Port
            Case IIMDDevice.enumRFPORTS.PORTA
                _P1 = PowerDBM
            Case IIMDDevice.enumRFPORTS.PORTB
                _P2 = PowerDBM
        End Select
    End Sub

    WriteOnly Property PA_OnOff(ByVal i As Integer, Optional IsBiasChange As Boolean = True) As Boolean        'ben
        Set(value As Boolean)
            If value = False AndAlso _ImdBoxMode.Contains("SWEEP") = False Then
                Send_And_Read("pimtest req txoff")
            End If
            '------------------' add 20190306
            If i = 0 Or i = 1 Then
                Send_And_Read(String.Format("tx{0} m {1}", i, IIf(value, "1", "0")))
            End If
            '--------------------
        End Set
    End Property

    Public Function GetRFPower() As Double() Implements IIMDDevice.GetRFPower
        Try '20181123
            Dim resp(1) As Double
            resp(0) = Math.Round(_PowerPA, 3)
            resp(1) = Math.Round(_PowerPB, 3)
            Return resp
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        'Try
        '    Dim resp(1) As Double
        '    resp(0) = _PowerPA
        '    resp(1) = _PowerPA
        '    Return resp
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
    End Function

    Private Function OverTimeMode(ByVal band As String) As Boolean
        'overtime point set as like below
        'Sweep down is true,Sweep up is false

        If band = "LTE600" Then Return False
        If band = "LTE700L" Then Return True
        If band = "LTE700U" Then Return False
        If band = "LTE700" Then Return True
        If band = "DIG-DIV" Then Return True
        If band = "EGSM900" Then Return True
        If band = "LTE1400" Then Return True
        If band = "3500M" Then Return False 'add by tony
        Return True
    End Function

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp
        Try
            _bySinglePointsFlag = True
            _ImdBoxMode = ImdBoxMode.Trim.ToUpper
            SetSelectPA(_ImdBoxMode)
            Send_And_Read("pimtest sweep mode 3")
            Send_And_Read(String.Format("pimsweep {0} -10 {1} -10", startF, stopF)) 'R&D ask to send the first point which is in the band 

        Catch ex As Exception
            Throw New Exception("RFPowerRampUp() " & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Sub RFPowerRampUp(startF0 As Single, fixF1 As Single, ImdBoxMode As String, Optional stopF0 As Single = 0, Optional stepF0 As Single = 0, Optional duration_Sec As Single = 30) Implements IIMDDevice.RFPowerRampUp
        Try
            Dim cmd As String
            _timeOutSec = 30
            _bySinglePointsFlag = False
            _ImdBoxMode = ImdBoxMode.Trim.ToUpper
            SetSelectPA(_ImdBoxMode)
            If startF0 = 0 Or fixF1 = 0 Then Throw New Exception("The parameters : startF0 = " & startF0 & " Or fixF1 = " & fixF1 & " is not correct ")
            If _ImdBoxMode <> "OVERTIME" Then

                cmd = String.Format("pimsweep {0} {1} {2} {4} {3} {5}", startF0, stopF0, stepF0, fixF1, _P1, _P2)
            Else
                Send_And_Read(String.Format("pim2tone gldelay 3"))
                cmd = String.Format("pim2tone {0} {1} {2} {3} {4} {5}", startF0, _P1, fixF1, _P2, duration_Sec, 0.1)
            End If
            _ShellStream.WriteLine(cmd)
            GenerateEventSentMessage(cmd & vbCrLf)
        Catch ex As Exception
            Send_And_Read("pimtest req txoff")
            Throw New Exception("RFPowerRampUp() " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub SetSelectPA(ByVal temImdBoxMode As String)

        Select Case temImdBoxMode
            Case "OVERTIME"
                Send_And_Read(String.Format("pimsweep init sp ", IIf(OverTimeMode(_currentBandName), "down", "up")))
            Case "UPSWEEP"
                Send_And_Read("pimsweep init mp up")
            Case "DOWNSWEEP"
                Send_And_Read("pimsweep init mp down")
        End Select
    End Sub

    Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
        Get
            Return _currentBandName
        End Get
        Set(value As String)
            Try
                'lna0 mcu temperature: 21'C
                If value.ToString.ToUpper = "DIG-DIV850" Then value = "DIG-DIV"
                If value.ToString.ToUpper = "1400" Then value = "1400M"
                If value.ToString.ToUpper = "600" Then value = "LTE600"
                If value.ToString.ToUpper = "WIMAX3500" Then value = "3500M"

                _currentBandName = GetCurrentBandName
                Send_And_Read("pimtest req txoff")
                If _currentBandName = value Then
                    Exit Property
                End If
                Send_And_Read("Pimtest gainloop tx0 off")
                Send_And_Read("Pimtest gainloop tx1 off")
                _timeOutSec = 30
                Send_And_Read(String.Format("bandconfig {0}", value))
                Threading.Thread.Sleep(30)
                _currentBandName = value
            Catch ex As Exception
                Throw New Exception("FreqBandSet : " & vbCrLf & ex.Message)
            Finally
                Dim respTempture As String = Send_And_Read(String.Format("lna0mcu temp"))
                Dim mcT As Match = Regex.Match(respTempture, "mcu temperature:")
                If mcT.Success = False Then Throw New Exception(vbCrLf & "There is an error on lna in Zulu ,please stop testing .")
            End Try
        End Set
    End Property

    'Public Property FreqBandSet As String Implements IIMDDevice.FreqBandSet
    '    Get
    '        Return _currentBandName
    '    End Get
    '    Set(value As String)
    '        Try
    '            If value.ToString.ToUpper = "DIG-DIV850" Then value = "DIG-DIV"
    '            If value.ToString.ToUpper = "1400" Then value = "1400M"
    '            If value.ToString.ToUpper = "600" Then value = "LTE600"
    '            _currentBandName = GetCurrentBandName
    '            Send_And_Read("pimtest req txoff")
    '            If _currentBandName = value Then
    '                Exit Property
    '            End If
    '            Send_And_Read("Pimtest gainloop tx0 off")
    '            Send_And_Read("Pimtest gainloop tx1 off")
    '            'Send_And_Read("pimtest req txoff")
    '            _timeOutSec = 30
    '            Send_And_Read(String.Format("bandconfig {0}", value))
    '            Threading.Thread.Sleep(30)
    '            _currentBandName = value
    '            '_lastBandName = value
    '        Catch ex As Exception
    '            Throw New Exception("FreqBandSet : " & vbCrLf & ex.Message)
    '        End Try
    '    End Set
    'End Property

    Public Sub ClearEnd() Implements IIMDDevice.ClearEnd
        If _bySinglePointsFlag = False Then
            Dim res As String = _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(5))
            ' GenerateEventSentMessage(res)
        End If

    End Sub

    Public Sub RFPowerOnOff_OnePort(PORT As IIMDDevice.enumRFPORTS, OnOff As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort

    End Sub

    Public Sub SetTestMode(Mode As IIMDDevice.enumTESTMODE) Implements IIMDDevice.SetTestMode

    End Sub
    Public Sub SetImdUnit_dBc() Implements IIMDDevice.SetImdUnit_dBc

    End Sub

    Public Sub SetImdUnit_dBm() Implements IIMDDevice.SetImdUnit_dBm

    End Sub
    Public ReadOnly Property ReadRxRange As IIMDDevice.stFreq Implements IIMDDevice.ReadRxRange
        Get

        End Get
    End Property

    Public ReadOnly Property ReadTxRange As IIMDDevice.stTxFreq Implements IIMDDevice.ReadTxRange
        Get

        End Get
    End Property

    Public Sub CorrectRFPower(Port As IIMDDevice.enumRFPORTS) Implements IIMDDevice.CorrectRFPower

    End Sub

    Public Sub CorrectRFPower_TwoPort() Implements IIMDDevice.CorrectRFPower_TwoPort

    End Sub

    Public Sub SetPIMFreqMHz(freqMHzIM As Double) Implements IIMDDevice.SetPIMFreqMHz
        ' Throw New NotImplementedException()
    End Sub

    Public Property FreqBand As Integer Implements IIMDDevice.FreqBand
        Get
            'Return _BandID
        End Get
        Set(value As Integer)

        End Set
    End Property


    'Below is DTF===============================================================
    Public Sub DTF_Cal() Implements IIMDDevice.DTF_Cal

        If MsgBox("Connect Cal Kits to start to InstanDtf Calibration ", MsgBoxStyle.Question + vbOKCancel, "Connect Cal Kits") = MsgBoxResult.Ok Then

            Try
                Send_And_Read(String.Format("dist2pim win_cfg 65536 8"))
                Send_And_Read(String.Format("dist2pim sweep"))

            Catch ex As Exception
                Throw New Exception("DTF_Cal()::" & ex.Message)
            End Try

            MsgBox(" Finished ! ")

        Else
            MsgBox(" InstanDtf Calibration Cancelled ")

        End If


    End Sub

    Public Function ReadDTP(Accurate As String, speed As String) As String
        Try
            Send_And_Read(String.Format("dist2pim win_cfg " & Accurate & " " & speed))
            Dim dtpStr As String = Send_And_Read(String.Format("dist2pim sweep"))
            Return dtpStr
        Catch ex As Exception
            Throw New Exception("ReadDTP()::" & ex.Message)
        End Try
    End Function


    Public Function GetDTP_Time(Accurate As String, speed As String) As String Implements IIMDDevice.GetDTP_Time
        Try
            Dim DelayT As String
            Dim rT As String
            Dim Rssi_Diff As Single

            Dim dtpStr As String = ReadDTP(Accurate, speed)
            '1
            Dim r As Regex = New Regex("delay=.?\d*.?\d*")
            Dim m As Match = r.Match(dtpStr)
            If (m.Success) Then
                'Return m.Value
                DelayT = m.Value
            Else
                Return "NA"
            End If

            '2
            Dim r2 As Regex = New Regex("r=.?\d*.?\d*")
            Dim m2 As Match = r2.Match(dtpStr)
            If (m2.Success) Then
                rT = m2.Value
            Else
                Return "NA"
            End If

            '3
            Dim r3 As Regex = New Regex("rssi=.?\d*.?\d*")
            Dim m3 As MatchCollection = r3.Matches(dtpStr)
            Dim maxT As Single
            Dim minT As Single

            If m3.Count > 0 Then
                For Each Temp As Match In m3
                    maxT = CSng(Temp.Value.Replace("rssi=", "").Trim)
                    minT = CSng(Temp.Value.Replace("rssi=", "").Trim)
                    Exit For
                Next

                For Each Temp As Match In m3
                    If maxT < CSng(Temp.Value.Replace("rssi=", "").Trim) Then maxT = CSng(Temp.Value.Replace("rssi=", "").Trim)
                    If minT > CSng(Temp.Value.Replace("rssi=", "").Trim) Then minT = CSng(Temp.Value.Replace("rssi=", "").Trim)
                Next
            Else
                Return "NA"
            End If
            Rssi_Diff = maxT - minT

            '4
            '  TXT_save(dtpStr, DelayT & " , " & rT & " , " & "rssi=" & Rssi_Diff) '  will be delete infuture


            Return DelayT & " , " & rT & " , " & "rssi=" & Rssi_Diff

        Catch ex As Exception
            Throw New Exception("GetDTP_Time()::" & ex.Message)
        End Try
    End Function


    Private Sub TXT_save(ByVal dtpStr As String, ByVal filename As String)
        Try
            ' Dim PathnamePlus As String = "C:\CATS\test_result\ZULUTestData" & "\" & Now.ToString("yyyyMMdd")
            Dim PathnamePlus As String = "\\asz-42jc23x\tempdata$\zulu" & "\" & Now.ToString("yyyyMMdd")

            Dim strDataFileName As String = filename & ".txt"

            If (Directory.Exists(PathnamePlus)) Then
            Else
                Directory.CreateDirectory(PathnamePlus)
            End If

            Dim sw As StreamWriter = New StreamWriter(PathnamePlus & "\" & strDataFileName, False)
            sw.WriteLine(dtpStr)
            sw.Close()


        Catch ex As Exception
            Throw New Exception("ZULU Txt Save error()::" & ex.Message)
        End Try

    End Sub

End Class


