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

    Dim MasterSN_T As String = ""
    Dim Logtime As String '=Now.ToString("yyyyMMddHHmmss")

    Dim CurrentCheck_3500 As Boolean = False

    Private _DeviceCheckOK As Boolean = True

    Private Enum HardwareVersion As Byte
        V1 = 1
        V1pro = 2
    End Enum
    Private _HardwareVersion As HardwareVersion = HardwareVersion.V1

    Private Class FilterProfile         'ben
        Public _listOneFilterbandName As New List(Of String) 'one filter contain multi-bands
        Public _filterSerialNumber As String
        Public _filterPartNumber As String
        Public _filterHardwareVersion As String
        Public Overrides Function ToString() As String
            Return String.Format("SN:{0},PN:{1},Band:{2}", _filterSerialNumber, _filterPartNumber, String.Join(",", _listOneFilterbandName))
        End Function
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

    Public Function CheckPowerVersion(ByVal PowerDBM1 As Double) As Boolean Implements IIMDDevice.CheckPowerVersion

        Select Case _HardwareVersion
            Case HardwareVersion.V1
                If PowerDBM1 > 43 Then Throw New Exception("Filter is not match with V1 master unit power now is -> " & PowerDBM1)
            Case HardwareVersion.V1pro
                If PowerDBM1 > 46 Then Throw New Exception("Filter is not match with V1Pro master unit power now is -> " & PowerDBM1)
        End Select

    End Function

    Public Function SwitchUpAndDown(ByVal SweepMode As Boolean) As Boolean Implements IIMDDevice.SwitchUpAndDown

    End Function

    Public Overrides Function Open() As Boolean Implements IIMDDevice.Open
        Try
            _DeviceCheckOK = True
            Dim connInfo As New PasswordConnectionInfo(Me.Address, Me.userName, Me.passWord)
            _stUnitInfor = New clsUnitInfor
            _SshClient = New SshClient(connInfo)
            If _SshClient.IsConnected = False Then
                _SshClient.Connect()
            End If
            _ShellStream = _SshClient.CreateShellStream("xterm", 80, 24, 800, 600, 1024)
            Send_And_Read("/etc/init.d/watchdog stop;/opt/andrew/bin/console")

            Logtime = Now.ToString("yyyyMMddHHmmss")

            Try
                MasterSN_T = MasterSerialNumber()
            Catch ex As Exception
            End Try

            Send_And_Read(String.Format("timestamp -s {0:yyyy MM dd HH mm ss}", Now))

            Dim ms As String = Send_And_Read(String.Format("alarm du"))
            Dim mc As MatchCollection = Regex.Matches(ms, "PA[\d]")
            If mc.Count <> 0 Then Throw New Exception("There is alarm : " & (IIf(mc.Item(0).Value = "", "PA", mc.Item(0).Value)).ToString)

            If checkAlarm("Open", True) = False Then Throw New Exception("Happen alarm and stop test!")

            'Try
            '    MasterSN_T = MasterSerialNumber()
            'Catch ex As Exception
            'End Try
            _P1 = -10
            _P2 = -10
            _HardwareVersion = ReadHardwareVersion()
            Return True
        Catch ex As Exception
            Throw New Exception("Open(): " & vbCrLf & ex.Message)
        End Try
    End Function

    Private ReadOnly Property Init_Status() As Boolean ' 
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
            Dim msPN As Match = Regex.Match(temStr, "part_number:\s+(\S+)")
            Dim msSN As Match = Regex.Match(temStr, "serial_number: (\S+)")
            Dim msBands As MatchCollection = Regex.Matches(temStr, "band_name[_\d]*: (\S+)")
            If msPN.Success = True Then
                _filterProfile._filterPartNumber = msPN.Groups(1).Value
                Dim msHw As Match = Regex.Match(msPN.Groups(1).Value, "\S+-(\d+)\S+")
                If msHw.Success = True Then
                    _filterProfile._filterHardwareVersion = msHw.Groups(1).Value
                Else
                    _filterProfile._filterHardwareVersion = "0"
                End If
            Else
                _filterProfile._filterPartNumber = ""
                _filterProfile._filterHardwareVersion = "0"
            End If
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
            CheckFilterHardwareVerion(_filterProfile)
        Next
    End Sub

    Private Sub CheckFilterHardwareVerion(_filterProfile As FilterProfile)
        Select Case _HardwareVersion
            Case HardwareVersion.V1
                If _filterProfile._filterHardwareVersion <> "1" Then
                    Throw New Exception(String.Format("Filter ({0}) is not match with V1 master unit!", _filterProfile))
                End If
            Case HardwareVersion.V1pro
                If _filterProfile._filterHardwareVersion <> "2" Then
                    Throw New Exception(String.Format("Filter ({0}) is not match with V1pro master unit!", _filterProfile))
                End If
        End Select
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
            Try
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

                If _currentBandName.ToUpper.Contains("AWS2100".ToUpper) Then
                    If _stUnitInfor._dicFilterBandProfile.Keys.Contains(_currentBandName.Split(" ")(0)) = False Then Throw New Exception(_currentBandName & " can't find in the unit .")
                    _bandlist = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName.Split(" ")(0))._listOneFilterbandName
                    _SerialNumber = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName.Split(" ")(0))._filterSerialNumber 'add by tony
                Else
                    If _stUnitInfor._dicFilterBandProfile.Keys.Contains(_currentBandName) = False Then Throw New Exception(_currentBandName & " can't find in the unit .")
                    _bandlist = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName)._listOneFilterbandName
                    _SerialNumber = _stUnitInfor._dicFilterBandProfile.Item(_currentBandName)._filterSerialNumber 'add by tony
                End If

                If CheckMasterFilterCalDate(_SerialNumber) = False Then _SerialNumber = _SerialNumber & "->Mismatch for the current Master with Filter" 'add 20210312

                'filter name define every band conbine use symbol "-"
                Dim _filterName As String = ""
                If _bandlist.Count > 1 Then
                    _filterName = _bandlist(0) & "-" & _bandlist(1)
                End If
                _ModelNumber = "ZuluPimTester " & CStr(IIf(_bandlist.Count > 1, _filterName, _bandlist(0)))
                Return _ModelNumber
            Catch ex As Exception
                Throw New Exception("FilterBandName()::" & ex.Message)
            End Try
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

        If _HardwareVersion = HardwareVersion.V1pro Then
            Dim freqs As Double() = DplxCheckFreq(_F1, _F2)
            _F1 = freqs(0)
            _F2 = freqs(1)
        End If

        resp = Send_And_Read(String.Format("pimsweep {0} {2} {1} {3}", _F1, _F2, _P1, _P2))
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
        Savelog(resstr)
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
        Savelog(resstr)
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
                'If resi < -175 Then
                '    resi = -175
                'End If
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
                'If resi < -132 Then
                '    resi = -132
                'End If
                Return Math.Round(resi, 3) '20181123
                ' Return resi
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Get
    End Property

    Private ReadOnly Property PACurrents(i As Integer) As Double()  ' add 20210813
        Get
            If i = 0 Or i = 1 Then
                _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(0.1))
                Dim tmpstr As String = Send_And_Read(String.Format("pa{0}mcu du power", i))
                Dim mas As MatchCollection = Regex.Matches(tmpstr, "current=(\S+)mA")
                Dim res(mas.Count - 1) As Double
                For j As Integer = 0 To mas.Count - 1
                    res(j) = Convert.ToDouble(mas(j).Groups(1).Value)
                Next
                Return res
            Else
                Return {0, 0}
            End If
        End Get
    End Property
    Private Function CurrentCheck(ch As Integer) As Boolean  ' add 20210813

        ' If ch <> 0 Or ch <> 1 Then Return False

        Try
            Dim resultError As Boolean = False
            Dim i As Integer = 0
            If CurrentCheck_3500 = True Then

                If ch = 0 Then
                    Dim curr0() As Double = PACurrents(ch)
                    For i = 0 To curr0.Length - 1
                        If i = 0 Then
                            If curr0(i) > 100 Then resultError = True
                        Else
                            If curr0(i) > 4200 Then resultError = True
                        End If
                    Next
                End If

                If ch = 1 Then
                    Dim curr1() As Double = PACurrents(ch)
                    For i = 0 To curr1.Length - 1
                        If i = 0 Then
                            If curr1(i) > 100 Then resultError = True
                        Else
                            If curr1(i) > 4200 Then resultError = True
                        End If
                    Next
                End If

                If resultError = True Then ' 关闭
                    Send_And_Read("pimtest req txoff")
                    'Send_And_Read(String.Format("tx{0} m {1}", 0, 0)) ' add 20190306
                    'Send_And_Read(String.Format("tx{0} m {1}", 1, 0)) ' add 20190306
                    If checkAlarm("Power Off  and After Test", True) = False Then Throw New Exception("Happen alarm and stop test!")
                    _DeviceCheckOK = False
                    Return False
                End If

            End If

            Return True

        Catch ex As Exception
            _DeviceCheckOK = False
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Sub RFPowerOnOff_OnePort(OnOff1 As Boolean, OnOff2 As Boolean) Implements IIMDDevice.RFPowerOnOff_OnePort

        Try
            If _HardwareVersion = HardwareVersion.V1pro AndAlso ((OnOff1 = True AndAlso OnOff2 = False) OrElse (OnOff1 = False AndAlso OnOff2 = True)) Then
                Dim freqs As Double() = DplxCheckFreq(_F1, _F2)
                _F1 = freqs(0)
                _F2 = freqs(1)
            End If

            If OnOff1 = True And OnOff2 = False Then
                If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
                Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, _P1, _F2, -100))
                'MsgBox(String.Format("pimtest tx {0} {1} {2} {3}", _F1, _P1, _F2, -100))
                'Send_And_Read("pimtest gainloop tx0 on")
                'Threading.Thread.Sleep(500)
                WaitTxGainLoopOn(0)
                If CurrentCheck(0) = False Then Throw New Exception("F1 power current error !")  'check current  add 20210813
            End If
            If OnOff1 = False And OnOff2 = True Then
                If _F1 = 0 Or _F2 = 0 Then Throw New Exception("You need first set the Freq !")
                Send_And_Read(String.Format("pimtest tx {0} {1} {2} {3}", _F1, -100, _F2, _P2))
                'MsgBox(String.Format("pimtest tx {0} {1} {2} {3}", _F1, -100, _F2, _P2))
                'Send_And_Read("pimtest gainloop tx1 on")
                'Threading.Thread.Sleep(500)
                WaitTxGainLoopOn(1)
                If CurrentCheck(1) = False Then Throw New Exception("F2 power current error !")
            End If
            If OnOff1 = False And OnOff2 = False Then
                Send_And_Read("pimtest req txoff")
                Send_And_Read(String.Format("tx{0} m {1}", 0, 0)) ' add 20190306
                Send_And_Read(String.Format("tx{0} m {1}", 1, 0)) ' add 20190306
                If checkAlarm("Power Off  and After Test", True) = False Then Throw New Exception("Happen alarm and stop test!")  'check current  add 20210813

                Try 'add 20210813
                    Send_And_Read("pimtest du alarm")
                Catch ex As Exception
                End Try

            End If

            'PA_OnOff(0, OnOff1) = OnOff1
            'PA_OnOff(1, OnOff2) = OnOff2

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Function SetTxGainLoopOn(ByVal i As Integer) As Double
        Dim res As Double = 0
        If i = 0 Or i = 1 Then
            Dim tmpstr As String = Send_And_Read(String.Format("pimtest gainloop tx{0} on", i))
            If tmpstr.ToLower().Contains("gainloop failed") Then
                Throw New Exception(tmpstr)
            End If
            Dim ma As Match = Regex.Match(tmpstr, "gainError=(\S+)$")
            If ma.Success Then
                res = Convert.ToDouble(ma.Groups(1).Value)
            End If
        End If
        Return res
    End Function

    Private Sub WaitTxGainLoopOn(ByVal i As Integer)
        Dim gainerr As Double
        Threading.Thread.Sleep(2000)
        For k As Integer = 0 To 2
            For r As Integer = 0 To 2
                Threading.Thread.Sleep(500)
                gainerr = SetTxGainLoopOn(i)
                If Math.Abs(gainerr) <= 0.05 Then Exit For
            Next
        Next
        Send_And_Read("pimtest gainloop v")
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
            Savelog(res)
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
        _PowerPA = CSng(Math.Round(PowerDBM1, 3))     'PowerDBM1) add 20210813
        _PowerPB = CSng(Math.Round(PowerDBM2, 3))     'PowerDBM2) add 20210813
        SetRFPower(IIMDDevice.enumRFPORTS.PORTA, PowerDBM1)
        SetRFPower(IIMDDevice.enumRFPORTS.PORTB, PowerDBM2)
        If checkAlarm("Set Power Value and Before Test", True) = False Then Throw New Exception("Happen alarm and stop test!")
        If CheckPAHealthBeforeOn() = False Then Throw New Exception("CheckPAHealthBeforeOn Fail and stop test!")
    End Sub

    Public Sub SetRFPower(Port As IIMDDevice.enumRFPORTS, PowerDBM As Double) Implements IIMDDevice.SetRFPower
        Select Case _HardwareVersion
            Case HardwareVersion.V1
                PowerDBM -= 53
            Case HardwareVersion.V1pro
                PowerDBM -= 56
            Case Else
                Throw New Exception("SetRFPower(): HardwareVersion error!")
        End Select
        Dim MaxPower As Double = -2
        If PowerDBM > MaxPower Then PowerDBM = MaxPower
        Select Case Port
            Case IIMDDevice.enumRFPORTS.PORTA
                '_P1 = PowerDBM 
                _P1 = Math.Round(PowerDBM, 2) '  add 20210813
            Case IIMDDevice.enumRFPORTS.PORTB
                ' _P2 = PowerDBM  
                _P2 = Math.Round(PowerDBM, 2) '  add 20210813
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


    '============================================================================
    'Private Function OverTimeMode(ByVal band As String) As Boolean
    '    'overtime point set as like below
    '    'Sweep down is true,Sweep up is false

    '    If band = "LTE600" Then Return False
    '    If band = "LTE700L" Then Return True
    '    If band = "LTE700U" Then Return False
    '    If band = "LTE700" Then Return True
    '    If band = "DIG-DIV" Then Return True
    '    If band = "EGSM900" Then Return True
    '    If band = "LTE1400" Then Return True
    '    If band = "3500M" Then Return False 'add by tony
    '    If band = "WCS2300" Then Return True
    '    Return True
    'End Function
    Private Function OverTimeMode(ByVal band As String) As Boolean ' updated by tony 20201124
        'overtime point set as like below
        'Sweep down is true,Sweep up is false

        Return PointInUpSweepTrace

    End Function

    Private _PointInUpSweepTrace As Boolean
    Public Property PointInUpSweepTrace() As Boolean Implements IIMDDevice.PointInUpSweepTrace
        Get
            Return _PointInUpSweepTrace
        End Get
        Set(value As Boolean)
            _PointInUpSweepTrace = value
        End Set
    End Property

    '============================================================================

    Public Sub RFPowerRampUp(startF As Single, stopF As Single, ImdBoxMode As String) Implements IIMDDevice.RFPowerRampUp
        Try
            _bySinglePointsFlag = True
            _ImdBoxMode = ImdBoxMode.Trim.ToUpper
            SetSelectPA(_ImdBoxMode)
            Send_And_Read("pimtest sweep mode 3")
            If _HardwareVersion = HardwareVersion.V1pro Then
                Dim freqs As Double() = DplxCheckFreq(startF, stopF)
                startF = freqs(0)
                stopF = freqs(1)
            End If
            Send_And_Read(String.Format("pimsweep {0} {2} {1} {3}", startF, stopF, _P1, _P2)) 'R&D ask to send the first point which is in the band 

            'If CurrentCheck(0) = False Or CurrentCheck(1) = False Then Throw New Exception(" power current error !") ' add 20210813

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

            If _HardwareVersion = HardwareVersion.V1 Then
                If _ImdBoxMode <> "OVERTIME" Then
                    'AWS2100 改， 20211206==================================
                    If _currentBandName.Contains("AWS2100") And startF0 > fixF1 Then
                        ''If fSweepUp Then
                        ''    cmd = String.Format("pimsweep {3} {3} 0 -10 {0} {1} {2} -10", fLL, fUL, fstep, ffix)
                        'cmd = String.Format("pimsweep {ffix} {ffix} 0 -10 {fLL} {fUL} {fstep} -10", fLL, fUL, fstep, ffix)
                        ''Else
                        ''    cmd = String.Format("pimsweep {0} {1} {2} -10 {3} {3} 0 -10", fLL, fUL, fstep, ffix)
                        'cmd = String.Format("pimsweep { fLL} {fUL} {fstep} -10 {ffix} {ffix} 0 -10", fLL, fUL, fstep, ffix)
                        ''End If
                        'cmd = String.Format("pimsweep {startF1} {stopF1} {stepF1} {_P1} {fixF0} {_P2}", startF1, stopF1, stepF1, fixF0, _P1, _P2)
                        cmd = String.Format("pimsweep {3} {3} 0 {4} {0} {1} {2} {5}", startF0, stopF0, stepF0, fixF1, _P1, _P2)
                    Else
                        cmd = String.Format("pimsweep {0} {1} {2} {4} {3} {5}", startF0, stopF0, stepF0, fixF1, _P1, _P2)
                    End If
                    '===============================================

                    ' cmd = String.Format("pimsweep {0} {1} {2} {4} {3} {5}", startF0, stopF0, stepF0, fixF1, _P1, _P2)
                Else
                    Send_And_Read(String.Format("pim2tone gldelay 3"))
                    'AWS2100 改， 20211206===================
                    If _currentBandName.Contains("AWS2100") And startF0 > fixF1 Then
                        cmd = String.Format("pim2tone {2} {1} {0} {3} {4} {5}", startF0, _P1, fixF1, _P2, duration_Sec, 0.1)
                    Else
                        cmd = String.Format("pim2tone {0} {1} {2} {3} {4} {5}", startF0, _P1, fixF1, _P2, duration_Sec, 0.1)
                    End If
                    '=======================================================

                    ' cmd = String.Format("pim2tone {0} {1} {2} {3} {4} {5}", startF0, _P1, fixF1, _P2, duration_Sec, 0.1)
                End If
            ElseIf _HardwareVersion = HardwareVersion.V1pro Then
                If _ImdBoxMode <> "OVERTIME" Then
                    Dim freqs As Double() = DplxCheckFreq(startF0, stopF0, stepF0, fixF1)
                    cmd = String.Format("pimsweep {0} {1} {2} {3} {4} {5} {6} {7}", freqs(0), freqs(1), freqs(2), _P1, freqs(3), freqs(4), freqs(5), _P2)
                Else
                    Dim freqs As Double() = DplxCheckFreq(startF0, fixF1)
                    cmd = String.Format("pim2tone {0} {1} {2} {3} {4} {5}", freqs(0), _P1, freqs(1), _P2, duration_Sec, 0.1)
                End If
            Else
                Throw New Exception("RFPowerRampUp(): HardwareVersion error!")
            End If

            _ShellStream.WriteLine(cmd)
            GenerateEventSentMessage(cmd & vbCrLf)
            Savelog(cmd & vbCrLf)

            ' If CurrentCheck(0) = False Or CurrentCheck(1) = False Then Throw New Exception(" power current error !") ' add 20210813

        Catch ex As Exception
            Send_And_Read("pimtest req txoff")
            Throw New Exception("RFPowerRampUp() " & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub SetSelectPA(ByVal temImdBoxMode As String)
        If _HardwareVersion <> HardwareVersion.V1 Then Exit Sub
        Select Case temImdBoxMode
            Case "OVERTIME"
                Send_And_Read(String.Format("pimsweep init sp {0}", IIf(OverTimeMode(_currentBandName), "down", "up")))
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
                CurrentCheck_3500 = False ' add 20210813
                'lna0 mcu temperature: 21'C
                If value.ToString.ToUpper = "DIG-DIV850" Then value = "DIG-DIV"
                If value.ToString.ToUpper = "1400" Then value = "1400M"
                If value.ToString.ToUpper = "600" Then value = "LTE600"
                If value.ToString.ToUpper = "WIMAX3500" Then
                    value = "3500M"
                    CurrentCheck_3500 = True ' add 20210813
                End If
                If value.ToString.ToUpper = "2300" Then value = "WCS2300" 'add by tony 20200609
                If value.ToString.ToUpper = "UMTSII2600" Then value = "E2600M" 'add by tony 20200609
                If value.ToString.ToUpper = "AMPS800W" Then value = "AMPS800" 'add by tony 20210326
                If value.ToString.ToUpper = "UMTS2100W" Then value = "UMTS2100" 'add by tony 20210326
                If value.ToString.ToUpper = "AWS2100" Then value = "AWS2100 PCS1900" 'add by tony 20211022 zulu AWS2100比较特殊，是由PCS1900PCS2100拼凑起来得，实际AWS2100得filter , 集成了PCS1900PCS2100得功能。

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
                Dim dplx As StDplxInfo = DplxInfo
                CheckDplxInfo(dplx)
            Catch ex As Exception
                Throw New Exception("FreqBandSet : " & vbCrLf & ex.Message)
            Finally
                Threading.Thread.Sleep(100)
                ''If checkAlarm("After BandSel1", False) = False Then
                ''    Threading.Thread.Sleep(1000)
                ''    If checkAlarm("After BandSel2", False) = False Then
                ''        Threading.Thread.Sleep(1000)
                '       If checkAlarm("After BandSel3", True) = False Then Throw New Exception("Happen alarm and stop test!")
                If checkAlarm("After BandSel", False) = False Then Throw New Exception("Happen alarm and stop test!") ' Fan 告警不提示
                ''    End If
                ''End If
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
            Savelog(res)
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

    Private Function ReadDTP(Accurate As String, speed As String) As String
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



    '=====================================================================================add 20210312
    Private Function CheckMasterFilterCalDate(sn As String) As Boolean ' sn=CurrentFilterSN -> _SerialNumber 
        Dim MasterSN As String = ""
        Dim FileterSN As String = ""
        Dim Caldate As Date

        'Get Master SN in Master
        _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(1))
        Dim tmpstr As String = Send_And_Read(String.Format("shell cat /opt/andrew/dsmr/SN.txt"))
        If tmpstr.Contains("No such file or directory") Then
            MsgBox("Don't read avaible SN for this master ")
            Return False
        Else
            Dim mas As MatchCollection = Regex.Matches(tmpstr, "(\S+):(\d+)")
            If mas.Count = 0 Then
                MsgBox("Don't read avaible SN for this master ")
                Return False
            End If
            MasterSN = mas(mas.Count - 1).Groups(1).Value
        End If

        'Get Master SN in Filter
        Dim resp As String = Send_And_Read(String.Format("lna0mcu eedata v all", 0))
        Dim ma As Match = Regex.Match(resp, "master_sn: (\S+)")
        If ma.Success Then
            FileterSN = ma.Groups(1).Value
        Else
            MsgBox("Don't read avaible SN for this filter ")
            Return False
        End If

        'Check match
        If MasterSN = "" Or FileterSN = "" Or MasterSN <> FileterSN Then
            MsgBox("Mismatch error! Please do calibration for this master(" & MasterSN & ") and fileter(" & FileterSN & ").")
            Return False
        End If

        'Check Calibration time
        Dim tmpstrt As String = Send_And_Read(String.Format("shell sed -n '/{0}:[0-9]\+/p' /opt/andrew/dsmr/fiters_sn.txt", sn.ToUpper))
        If tmpstrt.Contains("No such file or directory") Then
            Caldate = DateTime.MinValue
        Else
            Dim mas As MatchCollection = Regex.Matches(tmpstrt, String.Format("{0}:(\d+)", sn.ToUpper))
            If mas.Count = 0 Then Caldate = DateTime.MinValue
            Caldate = DateTime.ParseExact(mas(mas.Count - 1).Groups(1).Value, "yyyyMMddHHmmss", Nothing)
        End If

        If DateDiff("d", CDate(Format(Caldate, "yyyy/MM/dd HH:mm:ss")), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > 180 Then ' 半年校准一次
            MsgBox("Mismatch error! Please do calibration for this master and fileter")
            Return False
        Else
            Return True
        End If

    End Function

    '20210518 add====================================
    ReadOnly Property MasterSerialNumber() As String
        Get
            _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(1))
            Dim tmpstr As String = Send_And_Read(String.Format("shell cat /opt/andrew/dsmr/SN.txt"))
            If tmpstr.Contains("No such file or directory") Then
                Return ""
            Else
                Dim mas As MatchCollection = Regex.Matches(tmpstr, "(\S+):(\d+)")
                If mas.Count = 0 Then Return ""
                Return mas(mas.Count - 1).Groups(1).Value
            End If
        End Get
    End Property

    Private Sub Savelog(ErrorStr As String)

        ' If pFactoryCode.ToUpper.Trim <> "CN10".ToUpper Then Return

        Try
            If MasterSN_T.Length < 2 Then MasterSN_T = "unknow"
            'Local test PC
            Dim DataFolder As String = String.Format("{0}\test_system", My.Application.Info.DirectoryPath)
            Dim filename As String = MasterSN_T & "_" & Logtime & "_log.txt"

            Dim PathnamePlus As String = DataFolder & "\" & "ZULUCommandLog" & "\" & Now.ToString("yyyyMMdd")
            If (Directory.Exists(PathnamePlus)) Then
            Else
                Directory.CreateDirectory(PathnamePlus)
            End If

            Dim sw As StreamWriter = New StreamWriter(PathnamePlus & "\" & filename, True)
            sw.WriteLine(ErrorStr)
            sw.Close()

            'save in server
            Try
                Dim filenameServer As String = "ServerAddress.txt"
                Dim PathnameFile As String = DataFolder & "\" & "ZULUCommandLog" & "\" & filenameServer
                If Not File.Exists(PathnameFile) Then Return

                Dim sw1 As StreamReader = New StreamReader(PathnameFile)
                Dim ServerAddress As String = sw1.ReadLine()
                sw1.Close()

                If ServerAddress Is Nothing Then Return
                If ServerAddress.Length < 10 Then Return

                Dim PathnamePlus2 As String = ServerAddress & "\" & "ZULUCommandLog" & "\" & Now.ToString("yyyyMMdd")
                If (Directory.Exists(PathnamePlus2)) Then
                Else
                    Directory.CreateDirectory(PathnamePlus2)
                End If

                Dim sw2 As StreamWriter = New StreamWriter(PathnamePlus2 & "\" & filename, True)
                sw2.WriteLine(ErrorStr)
                sw2.Close()
            Catch ex As Exception

            End Try



        Catch ex As Exception

        End Try

    End Sub

    Private Sub SaveAlarmlog(ErrorStr As String)

        ' If pFactoryCode.ToUpper.Trim <> "CN10".ToUpper Then Return

        Try
            If MasterSN_T.Length < 2 Then MasterSN_T = "unknow"
            'Local test PC
            Dim DataFolder As String = String.Format("{0}\test_system", My.Application.Info.DirectoryPath)
            Dim filename As String = MasterSN_T & "_" & Logtime & "_Alarmlog.txt"

            Dim PathnamePlus As String = DataFolder & "\" & "ZULUCommandLog" & "\" & Now.ToString("yyyyMMdd")
            If (Directory.Exists(PathnamePlus)) Then
            Else
                Directory.CreateDirectory(PathnamePlus)
            End If

            Dim sw As StreamWriter = New StreamWriter(PathnamePlus & "\" & filename, True)
            If ErrorStr.Contains("->Open->") Then sw.WriteLine("")
            sw.WriteLine(ErrorStr)
            sw.Close()

            'save in server
            Try
                Dim filenameServer As String = "ServerAddress.txt"
                Dim PathnameFile As String = DataFolder & "\" & "ZULUCommandLog" & "\" & filenameServer
                If Not File.Exists(PathnameFile) Then Return

                Dim sw1 As StreamReader = New StreamReader(PathnameFile)
                Dim ServerAddress As String = sw1.ReadLine()
                sw1.Close()

                If ServerAddress Is Nothing Then Return
                If ServerAddress.Length < 10 Then Return

                Dim PathnamePlus2 As String = ServerAddress & "\" & "ZULUCommandLog" & "\" & Now.ToString("yyyyMMdd")
                If (Directory.Exists(PathnamePlus2)) Then
                Else
                    Directory.CreateDirectory(PathnamePlus2)
                End If

                Dim sw2 As StreamWriter = New StreamWriter(PathnamePlus2 & "\" & filename, True)
                sw2.WriteLine(ErrorStr)
                sw2.Close()
            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try

    End Sub

    Function getZulu_ActiveAlarms() As List(Of Integer) 'Integer'()

        Try ' add 20210813
            Send_And_Read("pimtest du alarm")
        Catch ex As Exception

        End Try


        Try

            Dim resstr As String = ""
            resstr = Send_And_Read("alarm du")

            Dim pattenNoAlarm As String = "No alarm is present"
            If resstr.Contains(pattenNoAlarm) Then Return Nothing
            ''   Return {NoAlarm}
            'Return Nothing
            'End If

            Dim patten As String = "Alarm (\d+)"
            Dim ma2 As MatchCollection = Regex.Matches(resstr, patten)

            If ma2.Count = 0 Then Return Nothing 'Return {NoAlarm}

            Dim arrAlarms As Integer
            Dim arrAlarmsList As New List(Of Integer)
            Dim j As Integer = 0
            For Each m As Match In ma2
                arrAlarms = Integer.Parse(m.Groups(1).Value)
                j = j + 1
                arrAlarmsList.Add(arrAlarms)
            Next
            Return arrAlarmsList

        Catch ex As Exception
            Throw New Exception("Happen error when check Alarm, getZulu_ActiveAlarms::!" & ex.Message)
        End Try


    End Function

    Public Enum alarmLevel
        Critical = 0
        Major
        Minor
        NoAlarm
        unknown
    End Enum
    Public Structure alarmInfo

        Dim alarmNo As Integer
        Dim alarmName As String
        Dim messageToTester As String
        Dim alarmLevel As String
    End Structure

    Private Sub initArrAlarmInfo(ByVal i As Integer, ByRef AlarmNameInfo As String, ByRef Stoptest As Boolean, whereAlarm As String, Showerrormessag As Boolean)
        'ReDim arrAlarmInfo(24)
        Dim arrAlarmInfo(24) As alarmInfo
        Try
            Select Case i

                Case 0
                    arrAlarmInfo(0).alarmNo = 0
                    arrAlarmInfo(0).alarmName = "No alarm is present"
                    arrAlarmInfo(0).messageToTester = ""
                    arrAlarmInfo(0).alarmLevel = alarmLevel.NoAlarm.ToString
                Case 1
                    arrAlarmInfo(1).alarmNo = 1
                    arrAlarmInfo(1).alarmName = "PA0 HW alarm"
                    arrAlarmInfo(1).messageToTester = "PA alarm"
                    arrAlarmInfo(1).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 2
                    arrAlarmInfo(2).alarmNo = 2
                    arrAlarmInfo(2).alarmName = "PA1 HW alarm"
                    arrAlarmInfo(2).messageToTester = "PA alarm"
                    arrAlarmInfo(2).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 3
                    arrAlarmInfo(3).alarmNo = 3
                    arrAlarmInfo(3).alarmName = "PA0 driver under current alarm"
                    arrAlarmInfo(3).messageToTester = "PA alarm"
                    arrAlarmInfo(3).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 4
                    arrAlarmInfo(4).alarmNo = 4
                    arrAlarmInfo(4).alarmName = "PA0 final under current alarm"
                    arrAlarmInfo(4).messageToTester = "PA alarm"
                    arrAlarmInfo(4).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 5
                    arrAlarmInfo(5).alarmNo = 5
                    arrAlarmInfo(5).alarmName = "PA1 driver under current alarm"
                    arrAlarmInfo(5).messageToTester = "PA alarm"
                    arrAlarmInfo(5).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 6
                    arrAlarmInfo(6).alarmNo = 6
                    arrAlarmInfo(6).alarmName = "PA1 final under current alarm"
                    arrAlarmInfo(6).messageToTester = "PA alarm"
                    arrAlarmInfo(6).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 7
                    arrAlarmInfo(7).alarmNo = 7
                    arrAlarmInfo(7).alarmName = "PA0 Over temperature alarm"
                    arrAlarmInfo(7).messageToTester = "PA alarm"
                    arrAlarmInfo(7).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 8
                    arrAlarmInfo(8).alarmNo = 8
                    arrAlarmInfo(8).alarmName = "PA1 Over temperature alarm"
                    arrAlarmInfo(8).messageToTester = "PA alarm"
                    arrAlarmInfo(8).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 9
                    arrAlarmInfo(9).alarmNo = 9
                    arrAlarmInfo(9).alarmName = "PA0 voltage low alarm"
                    arrAlarmInfo(9).messageToTester = "PA alarm"
                    arrAlarmInfo(9).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 10
                    arrAlarmInfo(10).alarmNo = 10
                    arrAlarmInfo(10).alarmName = "PA1 voltage low alarm"
                    arrAlarmInfo(10).messageToTester = "PA alarm"
                    arrAlarmInfo(10).alarmLevel = alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    Stoptest = True
                Case 11
                    arrAlarmInfo(11).alarmNo = 11
                    arrAlarmInfo(11).alarmName = "Master unit FAN alarm"
                    arrAlarmInfo(11).messageToTester = "check master fan"
                    arrAlarmInfo(11).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.Major.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    If Showerrormessag = True Then Stoptest = True  '  band set 后， fan issue 允许测
                Case 12
                    arrAlarmInfo(12).alarmNo = 12
                    arrAlarmInfo(12).alarmName = "Filter unit FAN alarm"
                    arrAlarmInfo(12).messageToTester = "check filter fan"
                    arrAlarmInfo(12).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.Major.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 13
                    arrAlarmInfo(13).alarmNo = 13
                    arrAlarmInfo(13).alarmName = "Filter SN mismatch alarm"
                    arrAlarmInfo(13).messageToTester = "reset and calibrate units"
                    arrAlarmInfo(13).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 14
                    arrAlarmInfo(14).alarmNo = 14
                    arrAlarmInfo(14).alarmName = "LNA communication fail alarm"
                    arrAlarmInfo(14).messageToTester = "check filter connection"
                    arrAlarmInfo(14).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.Critical.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 15
                    arrAlarmInfo(15).alarmNo = 15
                    arrAlarmInfo(15).alarmName = "Switch life cycle alarm"
                    arrAlarmInfo(15).messageToTester = ""
                    arrAlarmInfo(15).alarmLevel = alarmLevel.Minor.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 16
                    arrAlarmInfo(16).alarmNo = 16
                    arrAlarmInfo(16).alarmName = "TX0 gain loop failure alarm"
                    arrAlarmInfo(16).messageToTester = ""
                    arrAlarmInfo(16).alarmLevel = alarmLevel.Minor.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 17
                    arrAlarmInfo(17).alarmNo = 17
                    arrAlarmInfo(17).alarmName = "TX1 gain loop failure alarm"
                    arrAlarmInfo(17).messageToTester = ""
                    arrAlarmInfo(17).alarmLevel = alarmLevel.Minor.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 18
                    arrAlarmInfo(18).alarmNo = 18
                    arrAlarmInfo(18).alarmName = "TX0 VSWR Warn alarm"
                    arrAlarmInfo(18).messageToTester = ""
                    arrAlarmInfo(18).alarmLevel = alarmLevel.Minor.ToString '// alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 19
                    arrAlarmInfo(19).alarmNo = 19
                    arrAlarmInfo(19).alarmName = "TX0 VSWR failure alarm"
                    arrAlarmInfo(19).messageToTester = ""
                    arrAlarmInfo(19).alarmLevel = alarmLevel.Minor.ToString '//alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 20
                    arrAlarmInfo(20).alarmNo = 20
                    arrAlarmInfo(20).alarmName = "TX1 VSWR Warn alarm"
                    arrAlarmInfo(20).messageToTester = ""
                    arrAlarmInfo(20).alarmLevel = alarmLevel.Minor.ToString '// alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 21
                    arrAlarmInfo(21).alarmNo = 21
                    arrAlarmInfo(21).alarmName = "TX1 VSWR failure alarm"
                    arrAlarmInfo(21).messageToTester = ""
                    arrAlarmInfo(21).alarmLevel = alarmLevel.Minor.ToString '// alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 22
                    arrAlarmInfo(22).alarmNo = 22
                    arrAlarmInfo(22).alarmName = "Alarm 22"
                    arrAlarmInfo(22).messageToTester = ""
                    arrAlarmInfo(22).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 23
                    arrAlarmInfo(23).alarmNo = 23
                    arrAlarmInfo(23).alarmName = "Alarm 23"
                    arrAlarmInfo(23).messageToTester = ""
                    arrAlarmInfo(23).alarmLevel = alarmLevel.Minor.ToString  '// alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                Case 24
                    arrAlarmInfo(24).alarmNo = 24
                    arrAlarmInfo(24).alarmName = "Alarm 24"
                    arrAlarmInfo(24).messageToTester = ""
                    arrAlarmInfo(24).alarmLevel = alarmLevel.Minor.ToString  '//alarmLevel.unknown.ToString
                    AlarmNameInfo = arrAlarmInfo(i).alarmName
                    '  Case Else

            End Select


            If i >= 1 And i <= 24 Then
                Dim AlarmLog As String = ""
                AlarmLog = Now & "->" & whereAlarm & "->" & "Active AlarmNo =" & arrAlarmInfo(i).alarmNo
                AlarmLog = AlarmLog & "," & "Level =" & arrAlarmInfo(i).alarmLevel
                AlarmLog = AlarmLog & "," & "Alarm Name" & arrAlarmInfo(i).alarmName
                SaveAlarmlog(AlarmLog)
            End If


        Catch ex As Exception
            Throw New Exception("Happen error when check Alarm, initArrAlarmInfo::!" & ex.Message)
        End Try

    End Sub

    Private Function MasterFanProcess(i As Integer) As String ' add 20210813
        Try
            Dim tmpstr As String = Send_And_Read("pimtest fan") '，  Fan speed

            Dim res(2) As Integer
            If i = 0 Or i = 1 Then
                Dim resp As String = ""
                Dim ma As Match = Nothing
                For r As Integer = 0 To 2
                    resp = Send_And_Read(String.Format("pa{0}mcu du", i))
                    ma = Regex.Match(resp, "Temperature: bias=(\S+), in=(\S+), out=(\S+)")
                    If ma.Success Then Exit For
                Next

                'If ma IsNot Nothing AndAlso ma.Success Then
                '    res(0) = ma.Groups(1).Value
                '    res(1) = ma.Groups(2).Value
                '    res(2) = ma.Groups(3).Value
                '    tmpstr = tmpstr & vbCrLf & res(0) & vbCrLf & res(1) & vbCrLf & res(2)
                'End If

                tmpstr = tmpstr & vbCrLf & resp
            End If

            Return tmpstr

        Catch ex As Exception
            Throw New Exception("Happen error when get fan speed and temperature of PA, MasterFanProcess()::" & ex.Message)
        End Try
    End Function

    Private Function checkAlarm(whereAlarm As String, Showerrormessage As Boolean) As Boolean
        Try

            Dim AlarmList As List(Of Integer) = getZulu_ActiveAlarms()
            If AlarmList Is Nothing Then Return True

            Dim AlarmNameInfo As String = ""
            Dim AlarmNameInfoT As String = ""
            Dim Stoptest As Boolean = False

            For Each Tempalarm As Integer In AlarmList
                initArrAlarmInfo(Tempalarm, AlarmNameInfoT, Stoptest, whereAlarm, Showerrormessage)
                AlarmNameInfo = AlarmNameInfo & AlarmNameInfoT & vbLf & ";"
            Next

            'Try
            If AlarmNameInfo.Contains("Master unit FAN alarm") Then
                SaveAlarmlog(MasterFanProcess(0)) ' add 20210813
                SaveAlarmlog(MasterFanProcess(1)) ' add 20210813

            End If
            'Catch ex As Exception
            'End Try

            If Stoptest = True Then
                _DeviceCheckOK = False
                ' If Showerrormessage = True Then MsgBox("there is something wrong with the ZULU , please check error code as below :" & vbLf & AlarmNameInfo, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                MsgBox("there is something wrong with the ZULU , please check error code as below :" & vbLf & AlarmNameInfo, MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
                Return False
            End If

            Return True

        Catch ex As Exception
            _DeviceCheckOK = False
            Throw New Exception("Happen error when check Alarm, please double check!" & ex.Message)
            '   Return True
        End Try

    End Function


    '  '开功率之前检查下 PA  开始==================================================================================================================
    Private Function CheckPAHealthBeforeOn() As Boolean
        Try
            If HealthCheck() <> "" Then
                _DeviceCheckOK = False
                Return False
            End If
            Return True
        Catch ex As Exception
            _DeviceCheckOK = False
            Throw New Exception("Happen error when CheckPAHealthBeforeOn::CheckPAHealthBeforeOn()!" & ex.Message)
        End Try
    End Function

    Private ReadOnly Property PAFirmware(i As Integer) As String
        Get
            _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(0.01)) '// add 2019-08-30 at emc factory
            Dim tmpstr As String = Send_And_Read(String.Format("pa{0}mcu fw ver", i))
            Dim ma As Match = Regex.Match(tmpstr, "ver = (\S+)")
            If ma.Success Then
                Return ma.Groups(1).Value
            Else
                Return ""
            End If
        End Get
    End Property

    Public Sub SetTxOff()
        Dim resp2 As String
        resp2 = Send_And_Read("pimtest req txoff")
    End Sub

    Private ReadOnly Property PA_Status(ByVal i As Integer) As stPAstatus
        Get
            Dim res As New stPAstatus
            If i = 0 Or i = 1 Then
                Dim resp As String = Send_And_Read(String.Format("pa{0}mcu bias v", i))
                Dim ma As Match '= Regex.Match(resp, "Driver bias: d2a=(\d+), a2d=(\d+)")
                ma = Regex.Match(resp, "Alarm_\d+V=(\d+), Alarm_L=(\d+), Alarm=(\d+), PA_Status=(\d+), bias_step=(-\d+|\d+)")
                If ma.Success Then
                    ReDim res.Alarm(2)
                    res.Alarm(0) = ma.Groups(1).Value
                    res.Alarm(1) = ma.Groups(2).Value
                    res.Alarm(2) = ma.Groups(3).Value
                    res.PA_Status = ma.Groups(4).Value
                    res.bias_step = ma.Groups(5).Value
                Else
                    res.bias_step = -4
                    Return res
                End If
            End If
            Return res
        End Get
    End Property

    Private Function WaitBias(ByVal i As Integer) As Boolean
        Dim cycle As Integer = 0
        Dim pas As stPAstatus
        If i = 0 Or i = 1 Then
            Do
                Threading.Thread.Sleep(100)
                cycle += 1
                pas = PA_Status(i)
                If cycle > 150 Then Exit Do
            Loop Until (pas.bias_step <= 0)
        End If
        If pas.bias_step = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Structure stPAstatus
        'Dim Driver_D2A() As Integer
        'Dim Final_D2A() As Integer
        Dim Alarm() As Integer
        Dim PA_Status As Integer
        Dim bias_step As Integer
    End Structure

    Private Function HealthCheck() As String
        SetTxOff()
        _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(1)) '//2019-08-28 : add for 3.5G zulu test at EMC Factory
        For i As Integer = 0 To 1
            If PAFirmware(i) = "" Then Return String.Format("PA{0} no reponse!", i)
            Dim st As stPAstatus = PA_Status(i)
            If st.Alarm(0) + st.Alarm(1) + st.Alarm(2) > 0 Then
                Send_And_Read(String.Format("pa{0}mcu reset alarm", i))
                st = PA_Status(i)
                If st.Alarm(0) + st.Alarm(1) + st.Alarm(2) > 0 Then Return String.Format("PA{0} has alarm!", i)
            End If

            If st.bias_step <> 4 Then
                Send_And_Read(String.Format("pa{0}mcu bias on", i))

                st = PA_Status(i)
                If st.bias_step <> 4 Then
                    Send_And_Read(String.Format("pa{0}mcu bias off", i))
                    Send_And_Read(String.Format("pa{0}mcu bias config", i))
                    If WaitBias(i) = False Then Return String.Format("PA{0} bias config fail!", i)
                    Send_And_Read(String.Format("pa{0}mcu bias on", i))
                    st = PA_Status(i)

                    If st.bias_step <> 4 Then Return String.Format("PA{0} bias fail!", i)
                End If
            End If
        Next
        Return ""
    End Function
    '  '开功率之前检查  结束==================================================================================================================


    ''读 zulu  的 master sn
    'Property SerialNumber() As String
    '    Get
    '        _ShellStream.Expect(expectEnd, TimeSpan.FromSeconds(1))
    '        Dim tmpstr As String = Send_And_Read(String.Format("shell cat /opt/andrew/dsmr/SN.txt"))
    '        If tmpstr.Contains("No such file or directory") Then
    '            Return ""
    '        Else
    '            Dim mas As MatchCollection = Regex.Matches(tmpstr, "(\S+):(\d+)")
    '            If mas.Count = 0 Then Return ""
    '            Return mas(mas.Count - 1).Groups(1).Value
    '        End If
    '    End Get

    '    Set(MasterUnitSn As String)
    '        Send_And_Read(String.Format("shell echo '{0}:{1:yyyyMMddHHmmss}' >>/opt/andrew/dsmr/SN.txt", MasterUnitSn, Now))
    '    End Set
    'End Property


    '' 读写filter lna 的master sn
    'Property LNA_Master_SN() As String   '// add 2019-08-05
    '    Get

    '        Dim resp As String = Send_And_Read(String.Format("lna0mcu eedata v all", 0))
    '        Dim ma As Match = Regex.Match(resp, "master_sn: (\S+)")
    '        If ma.Success Then
    '            Return ma.Groups(1).Value
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(value As String)
    '        Dim str = String.Format("lna0mcu eedata master_sn {0}", value)
    '        'Send_And_Read(str, 0)
    '        Send_And_Read(str)
    '    End Set
    'End Property

    ''记录filter 的校准时间
    'Public Sub SaveFilterSN()
    '    Dim sn As String = _SerialNumber 'CurrentFilterSN
    '    If sn <> "" Then
    '        Send_And_Read(String.Format("shell echo '{0}:{1:yyyyMMddHHmmss}' >>/opt/andrew/dsmr/fiters_sn.txt", sn, Now))
    '    End If
    'End Sub


    '' 读取filter 的校准时间
    'ReadOnly Property FilterCalDate(sn As String) As DateTime
    '    Get
    '        Dim tmpstrt As String = Send_And_Read(String.Format("shell sed -n '/{0}:[0-9]\+/p' /opt/andrew/dsmr/fiters_sn.txt", sn.ToUpper))
    '        If tmpstrt.Contains("No such file or directory") Then
    '            Return DateTime.MinValue
    '        Else
    '            Dim mas As MatchCollection = Regex.Matches(tmpstrt, String.Format("{0}:(\d+)", sn.ToUpper))
    '            If mas.Count = 0 Then Return DateTime.MinValue
    '            Return DateTime.ParseExact(mas(mas.Count - 1).Groups(1).Value, "yyyyMMddHHmmss", Nothing)
    '        End If
    '    End Get
    'End Property

    Public Property DeviceCheckOK() As Boolean Implements IIMDDevice.DeviceCheckOK
        Get
            Return _DeviceCheckOK
        End Get
        Set(value As Boolean)
            _DeviceCheckOK = value
        End Set
    End Property

#Region "V1Upgrade"
    Private Function ReadHardwareVersion() As HardwareVersion
        Dim resp As String = Send_And_Read("pimtest req pn")
        'Master PN: RF204636-2A
        Dim ma As Match = Regex.Match(resp, "Master PN:\s*\S+-(\d+)\S+")
        If ma.Success Then
            If ma.Groups(1).Value = "2" Then
                Return HardwareVersion.V1pro
            Else
                Return HardwareVersion.V1
            End If
        Else
            Return HardwareVersion.V1
        End If
    End Function
    Public Enum DplxPath As Byte
        A
        B
        None
    End Enum
    Public Enum DplxMode As Byte
        AutoA
        AutoB
        ForceA
        ForceB
    End Enum
    Private WriteOnly Property DplxAutoMode() As DplxPath
        Set(value As DplxPath)
            Send_And_Read(String.Format("pimtest dplx auto {0}", value))
        End Set
    End Property
    Private WriteOnly Property DplxForceMode() As DplxPath
        Set(value As DplxPath)
            Send_And_Read(String.Format("pimtest dplx force {0}", value))
        End Set
    End Property
    Private Structure StDplxInfo
        Dim Mode As DplxMode
        Dim Path As DplxPath
        Dim A1StartFreq As Double
        Dim A1StopFreq As Double

        Dim A2StartFreq As Double
        Dim A2StopFreq As Double

        Dim B1StartFreq As Double
        Dim B1StopFreq As Double

        Dim B2StartFreq As Double
        Dim B2StopFreq As Double
    End Structure
    Private ReadOnly Property DplxInfo() As StDplxInfo
        Get
            Dim result As StDplxInfo
            Dim resp As String = Send_And_Read("pimtest dplx v")
            'Current diplexer configurations
            'Dpx mode: auto A
            'Dpx path: A
            'A1: 640.00 ~ 652.00
            'A2: 617.00 ~ 627.00
            'B1: 650.00 ~ 652.00
            'B2: 617.00 ~ 641.00

            Dim ma As Match = Regex.Match(resp, "Dpx mode:\s*(.*)")
            If ma.Success Then
                Select Case ma.Groups(1).Value
                    Case "auto A"
                        result.Mode = DplxMode.AutoA
                    Case "auto B"
                        result.Mode = DplxMode.AutoB
                    Case "force A"
                        result.Mode = DplxMode.ForceA
                    Case "force B"
                        result.Mode = DplxMode.ForceB
                End Select
            End If

            ma = Regex.Match(resp, "Dpx path:\s*(\S+)")
            If ma.Success Then
                Select Case ma.Groups(1).Value
                    Case "A"
                        result.Path = DplxPath.A
                    Case "B"
                        result.Path = DplxPath.B
                    Case "None"
                        result.Path = DplxPath.None
                End Select
            End If

            ma = Regex.Match(resp, "A1:\s*(\d+\.\d+)\s*~\s*(\d+\.\d+)")
            If ma.Success Then
                result.A1StartFreq = CDbl(ma.Groups(1).Value)
                result.A1StopFreq = CDbl(ma.Groups(2).Value)
            End If
            ma = Regex.Match(resp, "A2:\s*(\d+\.\d+)\s*~\s*(\d+\.\d+)")
            If ma.Success Then
                result.A2StartFreq = CDbl(ma.Groups(1).Value)
                result.A2StopFreq = CDbl(ma.Groups(2).Value)
            End If
            ma = Regex.Match(resp, "B1:\s*(\d+\.\d+)\s*~\s*(\d+\.\d+)")
            If ma.Success Then
                result.B1StartFreq = CDbl(ma.Groups(1).Value)
                result.B1StopFreq = CDbl(ma.Groups(2).Value)
            End If
            ma = Regex.Match(resp, "B2:\s*(\d+\.\d+)\s*~\s*(\d+\.\d+)")
            If ma.Success Then
                result.B2StartFreq = CDbl(ma.Groups(1).Value)
                result.B2StopFreq = CDbl(ma.Groups(2).Value)
            End If
            Return result
        End Get
    End Property

    Private Sub CheckDplxInfo(dplxinfo As StDplxInfo)
        'Current diplexer configurations:
        'Dpx mode: auto A
        'Dpx path: None
        'A1: -0.10 ~ 0.00
        'A2: 0.00 ~ 0.00
        'B1: 0.00 ~ -0.00
        'B2: 0.00 ~ 0.00
        If dplxinfo.A1StartFreq < 1 Then
            _HardwareVersion = HardwareVersion.V1
        End If
    End Sub

    Private Function InRange(freq As Double, fLL As Double, fUL As Double) As Boolean
        Return freq >= fLL AndAlso freq <= fUL
    End Function
    Private Function InRange(fs As Double, fp As Double, fLL As Double, fUL As Double) As Boolean
        Return InRange(fs, fLL, fUL) AndAlso InRange(fp, fLL, fUL)
    End Function
    Private Function DplxCheckFreq(f1 As Double, f2 As Double) As Double()
        Dim dplx As StDplxInfo = DplxInfo

        If InRange(f1, dplx.A1StartFreq, dplx.A1StopFreq) AndAlso InRange(f2, dplx.A2StartFreq, dplx.A2StopFreq) Then
            DplxAutoMode = DplxPath.A
            Return {f1, f2}
        ElseIf InRange(f2, dplx.A1StartFreq, dplx.A1StopFreq) AndAlso InRange(f1, dplx.A2StartFreq, dplx.A2StopFreq) Then
            DplxAutoMode = DplxPath.A
            Return {f2, f1}
        ElseIf InRange(f1, dplx.B1StartFreq, dplx.B1StopFreq) AndAlso InRange(f2, dplx.B2StartFreq, dplx.B2StopFreq) Then
            DplxAutoMode = DplxPath.B
            Return {f1, f2}
        ElseIf InRange(f2, dplx.B1StartFreq, dplx.B1StopFreq) AndAlso InRange(f1, dplx.B2StartFreq, dplx.B2StopFreq) Then
            DplxAutoMode = DplxPath.B
            Return {f2, f1}
        Else
            Throw New Exception(String.Format("DplxCheckFreq: Freq pair {0},{1} not support!", f1, f2))
        End If
    End Function

    Private Function DplxCheckFreq(fstart As Double, fstop As Double, fstep As Double, ffix As Double) As Double()
        Dim dplx As StDplxInfo = DplxInfo

        If InRange(fstart, fstop, dplx.A1StartFreq, dplx.A1StopFreq) AndAlso InRange(ffix, dplx.A2StartFreq, dplx.A2StopFreq) Then
            DplxAutoMode = DplxPath.A
            Return {fstart, fstop, fstep, ffix, ffix, 0}
        ElseIf InRange(ffix, dplx.A1StartFreq, dplx.A1StopFreq) AndAlso InRange(fstart, fstop, dplx.A2StartFreq, dplx.A2StopFreq) Then
            DplxAutoMode = DplxPath.A
            Return {ffix, ffix, 0, fstart, fstop, fstep}
        ElseIf InRange(fstart, fstop, dplx.B1StartFreq, dplx.B1StopFreq) AndAlso InRange(ffix, dplx.B2StartFreq, dplx.B2StopFreq) Then
            DplxAutoMode = DplxPath.B
            Return {fstart, fstop, fstep, ffix, ffix, 0}
        ElseIf InRange(ffix, dplx.B1StartFreq, dplx.B1StopFreq) AndAlso InRange(fstart, fstop, dplx.B2StartFreq, dplx.B2StopFreq) Then
            DplxAutoMode = DplxPath.B
            Return {ffix, ffix, 0, fstart, fstop, fstep}
        Else
            Throw New Exception(String.Format("DplxCheckFreq: Freq pair {0}-{1},{2} not support!", fstart, fstop, ffix))
        End If
    End Function

    Public Function ReadDTP() As String Implements IIMDDevice.ReadDTP
        Throw New NotImplementedException()
    End Function
#End Region
End Class


