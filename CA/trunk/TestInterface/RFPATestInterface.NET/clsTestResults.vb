Option Strict Off
Option Explicit On
Imports System.IO
Imports Microsoft.VisualBasic
Imports System
<System.Runtime.InteropServices.ProgId("clsTestResults_NET.clsTestResults")> Public Class clsTestResults

    'Here's the intended operation for this module
    ' 1) At startup, initialize the public variables at the top of this module
    ' 2) Call "StartTest" at the beginning of a new test passing in the unit
    '    serial number and the process step
    ' 3) For each of the test groups within an overall test, call StartTestGroup
    '    at the beginning of the test group and StopTestGroup when finished.  This
    '    module will automatically log the amount of time spent in the test group
    '    and the overall status for the test group.
    ' 4) Call RecordNumeric, RecordPassFail, or RecordString to log individual
    '    measurement results during the test.
    ' 5) Call "StopTest" at end of test.  This automatically save results to DCF file

    'Other stuff
    ' - Call GetTestStatus to determine the status of all tests
    ' - Call SetLimitsFile to use a ".csv" limits file

    'The following variables would usually be initialized at start of program
    Public Test_SW_Rev As String 'e.g., "1.2.1"
    Public Test_System_ID As String 'e.g., "ATP-2"
    Public DCF_File As String 'e.g., "c:\testdata.dcf"
    Public Production_Site_ID As String 'e.g., "CEL-Tor"
    Public Fixture_ID As String 'e.g., "50018-7-1","1","2"
    Public Operator_ID As String 'e.g., "1234"
    Public TestLimits_Rev As String
    Public Gen4 As String
    Public Gen5 As String

    Public Firmware_Rev As String 'this can be assigned anytime during test

    'The following variables are set by calling "StartTest", "StopTest" & "StartTestGroup
    Private pAssembly_Type As String 'e.g., "KLAM","TAZ"
    Public Assembly_Rev As String
    Private pProgram_ID As String
    Private pProcess_Step_ID As String 'e.g., "FTEST1", "PostBurnin"
    Private pSerial_Number As String
    Private pSerial_Number_CM As String
    Private pStartTestTime As DateTime
    Private pStopTestTime As DateTime
    Private pStartGroupTime As DateTime
    Private pTestGroup As String

    'This variable stores all the test results recorded with "RecordMeasurement"
    Private TestData As New Collection
    Private LimitsData As New Collection

    Const DCF_Rev As String = "1"


    '*****************************************************************************
    'The following properties can be used for read-only access to some of the private
    'variables.
    '*****************************************************************************
    'Public Property Assembly_Type() As String
    '  Get
    '    Assembly_Type = pAssembly_Type
    '  End Get
    '  Set(ByVal Value As String)
    '    Dim frmInvalid As frmInvalid
    '    Dim rType As clsGlobals.AssemblyType
    '    rType.progs = ""
    '    rType.types = ""

    '    Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
    '    Dim br As New System.IO.BinaryReader(fs)
    '    If Value.IndexOf("Invalid_") < 0 Then
    '      rType.lenT = br.ReadInt32
    '      rType.lenP = br.ReadInt32
    '      rType.types = br.ReadString
    '      rType.progs = br.ReadString
    '      br.Close()
    '      fs.Close()

    '      rType.types = ConvertData(rType.types)
    '      rType.progs = ConvertData(rType.progs)

    '      pAssembly_Type = "InvalidAssemblyType"
    '      If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
    '        MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
    '      Else
    '        If InStr(1, rType.types, "," & Value & ",", CompareMethod.Text) < 1 Then
    '          frmInvalid = New frmInvalid
    '          frmInvalid.Label2.Text = "Assembly_Type of """ & Value & """"
    '          frmInvalid.Label7.Text = "A Assembly_Type of ""Invalid_" & Value & """"
    '          frmInvalid.ShowDialog()
    '          pAssembly_Type = "Invalid_" & Value
    '        Else
    '          pAssembly_Type = Value
    '        End If
    '      End If
    '    Else
    '      pAssembly_Type = Value
    '    End If
    '  End Set
    'End Property

    Public Property Assembly_Type() As String
        Get
            Assembly_Type = pAssembly_Type
        End Get
        Set(ByVal Value As String)
            'Dim frmInvalid As frmInvalid
            'Dim rType As clsGlobals.AssemblyType
            'rType.progs = ""
            'rType.types = ""

            'Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
            'Dim br As New System.IO.BinaryReader(fs)
            'If Value.IndexOf("Invalid_") < 0 Then
            '  rType.lenT = br.ReadInt32
            '  rType.lenP = br.ReadInt32
            '  rType.types = br.ReadString
            '  rType.progs = br.ReadString
            '  br.Close()
            '  fs.Close()

            '  rType.types = ConvertData(rType.types)
            '  rType.progs = ConvertData(rType.progs)

            '  pAssembly_Type = "InvalidAssemblyType"
            '  If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
            '    MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
            '  Else
            '    If InStr(1, rType.types, "," & Value & ",", CompareMethod.Text) < 1 Then
            '      frmInvalid = New frmInvalid
            '      frmInvalid.Label2.Text = "Assembly_Type of """ & Value & """"
            '      frmInvalid.Label7.Text = "A Assembly_Type of ""Invalid_" & Value & """"
            '      frmInvalid.ShowDialog()
            '      pAssembly_Type = "Invalid_" & Value
            '    Else
            '      pAssembly_Type = Value
            '    End If
            '  End If
            'Else
            pAssembly_Type = Value
            'End If
        End Set
    End Property
    'Public Property Program_ID() As String
    '	Get
    '		Program_ID = pProgram_ID
    '	End Get
    '	Set(ByVal Value As String)
    '           Dim frmInvalid As frmInvalid
    '           Dim rType As clsGlobals.AssemblyType
    '           rType.progs = ""
    '           rType.types = ""

    '           Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
    '           Dim br As New System.IO.BinaryReader(fs)
    '           If Value.IndexOf("Invalid_") < 0 Then
    '               rType.lenT = br.ReadInt32
    '               rType.lenP = br.ReadInt32
    '               rType.types = br.ReadString
    '               rType.progs = br.ReadString
    '               br.Close()
    '               fs.Close()

    '               rType.types = ConvertData(rType.types)
    '               rType.progs = ConvertData(rType.progs)

    '               pProgram_ID = "InvalidProgramID"
    '               If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
    '                   MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
    '               Else
    '                   If InStr(1, rType.progs, "," & Value & ",", CompareMethod.Text) < 1 Then
    '                       frmInvalid = New frmInvalid
    '                       frmInvalid.Label2.Text = "Program_ID of """ & Value & """"
    '                       frmInvalid.Label7.Text = "A Program_ID of ""Invalid_" & Value & """"
    '                       frmInvalid.ShowDialog()
    '                       pProgram_ID = "Invalid_" & Value
    '                   Else
    '                       pProgram_ID = Value
    '                   End If
    '               End If
    '           Else
    '               pProgram_ID = Value
    '           End If
    '       End Set
    'End Property
    Public Property Program_ID() As String
        Get
            Program_ID = pProgram_ID
        End Get
        Set(ByVal Value As String)
            'Dim frmInvalid As frmInvalid
            Dim rType As clsGlobals.AssemblyType
            rType.progs = ""
            rType.types = ""

            'Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
            'Dim br As New System.IO.BinaryReader(fs)
            'If Value.IndexOf("Invalid_") < 0 Then
            '  rType.lenT = br.ReadInt32
            '  rType.lenP = br.ReadInt32
            '  rType.types = br.ReadString
            '  rType.progs = br.ReadString
            '  br.Close()
            '  fs.Close()

            '  rType.types = ConvertData(rType.types)
            '  rType.progs = ConvertData(rType.progs)

            '  pProgram_ID = "InvalidProgramID"
            '  If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
            '    MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
            '  Else
            '    If InStr(1, rType.progs, "," & Value & ",", CompareMethod.Text) < 1 Then
            '      frmInvalid = New frmInvalid
            '      frmInvalid.Label2.Text = "Program_ID of """ & Value & """"
            '      frmInvalid.Label7.Text = "A Program_ID of ""Invalid_" & Value & """"
            '      frmInvalid.ShowDialog()
            '      pProgram_ID = "Invalid_" & Value
            '    Else
            '      pProgram_ID = Value
            '    End If
            '  End If
            'Else
            pProgram_ID = Value
            'End If
        End Set
    End Property
    Public ReadOnly Property TestGroup() As String
        Get
            TestGroup = pTestGroup
        End Get
    End Property


    Public Property Serial_Number() As String
        Get
            Serial_Number = pSerial_Number
        End Get
        Set(ByVal Value As String)
            'check for invalid charactors
            Dim i As Integer
            Dim oneChar As String
            Dim checkedSerial As String = ""
            Dim frmBadSerial As New frmBadSerial

            For i = 1 To Len(Value)
                oneChar = Mid(Value, i, 1)
                If InStr(1, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.", oneChar, CompareMethod.Binary) > 0 Then
                    checkedSerial = checkedSerial & oneChar
                End If
            Next i
            If StrComp(Value, checkedSerial, CompareMethod.Binary) <> 0 Then
                frmBadSerial.Label2.Text = Value
                frmBadSerial.Label7.Text = "A serial number of """ & checkedSerial & """"

                frmBadSerial.Show()
            End If
            pSerial_Number = checkedSerial
        End Set
    End Property


    Public Property Serial_Number_CM() As String
        Get
            Serial_Number_CM = pSerial_Number_CM
        End Get
        Set(ByVal Value As String)
            pSerial_Number_CM = Value
        End Set
    End Property

    Public ReadOnly Property Process_Step_ID() As String
        Get
            Process_Step_ID = pProcess_Step_ID
        End Get
    End Property

    Public ReadOnly Property StartTestTime() As DateTime
        Get
            StartTestTime = pStartTestTime
        End Get
    End Property

    Public ReadOnly Property StopTestTime() As DateTime
        Get
            StopTestTime = pStopTestTime
        End Get
    End Property
    Private Function ConvertData(ByRef sData As String) As String

        'Schmidt - msnews.microsoft.vb.general.discussion
        Dim cnt As Integer
        Dim response As String = ""
        Rnd(-4)
        For cnt = 1 To Len(sData)
            response = response & Chr(Asc(Mid(sData, cnt)) Xor Rnd() * 99)
        Next
        ConvertData = response

    End Function


    '*****************************************************************************
    'The following functions should be called at the start/stop of an overall test and
    'at the start/stop of smaller "Test Groups"
    '*****************************************************************************

    Public Sub StartTest(ByVal SN As String, ByVal ProcStep As String, Optional ByVal SN_CM As String = "")
        Call ClearAllData()
        pStartTestTime = Now
        pSerial_Number = SN
        pSerial_Number_CM = SN_CM
        pProcess_Step_ID = ProcStep
    End Sub

    Public Sub StopTest()
        pStopTestTime = Now
        'Call Save_DCF
    End Sub

    Public Sub StartTestGroup(ByVal GroupName As String)
        Call ClearGroupData(GroupName)
        pStartGroupTime = Now
        pTestGroup = GroupName
    End Sub

    Public Sub StopTestGroup()
        Dim Duration As Double
        Dim Status As String

        If pTestGroup = "" Then Exit Sub

        Dim testDuration As TimeSpan

        testDuration = Now.Subtract(pStartGroupTime)
        Duration = testDuration.TotalSeconds
        Call RecordNumeric("TIME_" & pTestGroup, Duration, 0, 100000)

        Status = GetGroupStatus(pTestGroup)
        Call RecordPassFail("STATUS_" & pTestGroup, (Status = "PASS"))

        pTestGroup = ""
    End Sub


    '*****************************************************************************
    'The following functions should be called to log individual measurement results
    'during the course of the test.
    '*****************************************************************************

    Public Function RecordNumeric(ByVal Testname As String, ByVal Measurement As Double, Optional ByRef ll As Double = 0.0#, Optional ByRef ul As Double = 0.0#, Optional ByRef Gen5 As String = "") As Boolean

        Dim rec As clsMeasurement
        Dim lim As clsMeasurement
        Dim MeasurementTime As Integer
        Dim testDuration As TimeSpan

        testDuration = Now.Subtract(pStartTestTime)
        MeasurementTime = CType(testDuration.TotalSeconds, Integer)

        On Error Resume Next
        rec = TestData.Item(UCase(Testname))
        If Err.Number <> 0 Then
            Call Err.Clear()
            rec = New clsMeasurement
            Call TestData.Add(rec, UCase(Testname))
        End If

        rec.name = Testname
        rec.NumericType = True
        rec.TestGroup = pTestGroup
        rec.MeasuredValue = Measurement
        rec.MeasurementTime = CStr(MeasurementTime)
        rec.Gen5 = Gen5

        'Look-up test limits in LimitsData collection
        On Error Resume Next
        lim = LimitsData.Item(UCase(rec.name))
        If Err.Number = 0 Then
            rec.LowerLimit = lim.LowerLimit
            rec.UpperLimit = lim.UpperLimit
            rec.Units = lim.Units
            ll = rec.LowerLimit
            ul = rec.UpperLimit
        Else
            rec.LowerLimit = ll
            rec.UpperLimit = ul
        End If
        On Error GoTo 0

        'Check against the limits
        rec.Passed = (Measurement >= rec.LowerLimit And Measurement <= rec.UpperLimit)

        RecordNumeric = rec.Passed
    End Function

    Public Function RecordString(ByVal Testname As String, ByVal Measurement As String, Optional ByVal MatchStr As String = "*") As Boolean

        Dim rec As clsMeasurement
        Dim FixedMeas As String

        On Error Resume Next
        rec = TestData.Item(UCase(Testname))
        If Err.Number <> 0 Then
            Call Err.Clear()
            rec = New clsMeasurement
            Call TestData.Add(rec, UCase(Testname))
        End If

        rec.name = Testname
        rec.NumericType = False
        rec.TestGroup = pTestGroup
        FixedMeas = Replace(Measurement, Chr(13), " ", 1, -1, CompareMethod.Binary)
        FixedMeas = Replace(FixedMeas, Chr(10), " ", 1, -1, CompareMethod.Binary)
        FixedMeas = Replace(FixedMeas, "|", " ", 1, -1, CompareMethod.Binary)
        rec.MeasuredString = FixedMeas

        'Check against the limits
        rec.Passed = (Measurement Like MatchStr)
        RecordString = rec.Passed
    End Function

    Public Sub RecordPassFail(ByVal Testname As String, ByVal IsPass As Boolean)
        Dim rec As clsMeasurement

        On Error Resume Next
        rec = TestData.Item(UCase(Testname))
        If Err.Number <> 0 Then
            Call Err.Clear()
            rec = New clsMeasurement
            Call TestData.Add(rec, UCase(Testname))
        End If

        rec.name = Testname
        rec.NumericType = True
        rec.TestGroup = pTestGroup
        rec.LowerLimit = 0
        rec.UpperLimit = 0
        rec.MeasuredValue = IIf(IsPass, 0, -1)
        rec.Passed = IsPass
    End Sub


    '*****************************************************************************
    'The following functions can be used to clear the measurement list, but this
    'is usually not necessary because the "StartTest" procedures take care of this.
    '*****************************************************************************

    Public Sub ClearAllData()
        Call DeleteMeasurements("*")
        If Not inDiagnosticMode Then pSerial_Number = ""
        pTestGroup = ""
    End Sub

    Public Sub ClearGroupData(ByVal GroupName As String)
        Call DeleteMeasurements(GroupName)
        '  pTestGroup = ""
    End Sub

    Private Sub DeleteMeasurements(ByVal GroupName As String)
        Dim i As Short
        Dim rec As clsMeasurement
        For i = TestData.Count() To 1 Step -1
            rec = TestData.Item(i)
            If GroupName = "*" Or GroupName = rec.TestGroup Then
                Call TestData.Remove(i)
            End If
        Next
    End Sub


    '*****************************************************************************
    'The following functions can be used to determine the test status of individual
    'test groups or the entire test.
    '*****************************************************************************

    Public Function GetGroupFailures(ByVal GroupName As String) As Short
        Dim NumPass, NumFail As Short
        Call CountPassFail(GroupName, NumPass, NumFail)
        GetGroupFailures = NumFail
    End Function

    Public Function GetGroupPasses(ByVal GroupName As String) As Short
        Dim NumPass, NumFail As Short
        Call CountPassFail(GroupName, NumPass, NumFail)
        GetGroupPasses = NumPass
    End Function

    Public Function GetFailures() As Short
        GetFailures = GetGroupFailures("*")
    End Function

    Public Function GetPasses() As Short
        GetPasses = GetGroupPasses("*")
    End Function

    Public Sub CountPassFail(ByVal GroupName As String, ByRef NumPass As Short, ByRef NumFail As Short)
        Dim rec As clsMeasurement
        NumPass = 0
        NumFail = 0
        For Each rec In TestData
            If GroupName = "*" Or GroupName = rec.TestGroup Then
                If rec.Passed Then
                    NumPass = NumPass + 1
                Else
                    NumFail = NumFail + 1
                End If
            End If
        Next rec
    End Sub

    Public Function GetGroupStatus(ByVal GroupName As String) As String
        Dim NumPass, NumFail As Short

        Call CountPassFail(GroupName, NumPass, NumFail)

        If NumFail > 0 Then
            GetGroupStatus = "FAIL"
        Else
            If NumPass > 0 Then
                GetGroupStatus = "PASS"
            Else
                GetGroupStatus = "UNTESTED"
            End If
        End If
    End Function

    Public Function GetTestStatus() As String
        GetTestStatus = GetGroupStatus("*")
    End Function


    '*****************************************************************************
    'The following functions can be used to save or print the test data.  Note that
    'the Save_DCF is automatically called from StopTest.
    '*****************************************************************************

    Public Sub Save_DCF()
        Dim fh As Short
        Dim tmp As String
        Dim rec As clsMeasurement

        If DCF_File = "" Then Exit Sub
        If TestData.Count() = 0 Then Exit Sub

        fh = FreeFile()
        FileOpen(fh, DCF_File, OpenMode.Append)

        tmp = FormatEventStart()
        PrintLine(fh, tmp)

        For Each rec In TestData
            tmp = FormatProductMeasure(rec)
            PrintLine(fh, tmp)
        Next rec

        tmp = FormatEventStop()
        PrintLine(fh, tmp)

        FileClose(fh)
    End Sub

    Private Function FormatEventStart() As String
        Dim tmp As String

        If Process_Step_ID Like "FTEST#" Then
            Program_ID = Right(Process_Step_ID, 1) & "E"
        Else
            Program_ID = Process_Step_ID
        End If

        tmp = "EVENTSTART|" '   0    Event Start
        tmp = tmp & DCF_Rev & "|" '   1    dcf_rev
        tmp = tmp & pStartTestTime.ToString("yyyyMMddHHmmss") & "00|" '   2    event_date_time
        tmp = tmp & "TEST|" '   3    event_type
        tmp = tmp & Production_Site_ID & "|" '   4    prod_site_id
        tmp = tmp & "|" '   5    prod_line_id
        tmp = tmp & pProcess_Step_ID & "|" '   6    process_step_id
        tmp = tmp & Test_System_ID & "|" '   7    controller_id
        tmp = tmp & "1|" '   8    machine_id
        tmp = tmp & "|" '   9    machine_type
        tmp = tmp & "|" '   10   machine_mode:data_source
        tmp = tmp & "|" '   11   machine_step_status
        tmp = tmp & Program_ID & "|" '   12   program_id
        tmp = tmp & TestLimits_Rev & "|" '   13   program_rev
        tmp = tmp & Fixture_ID & "|" '   14   resource_id
        tmp = tmp & "|" '   15   customer_id
        tmp = tmp & "|" '   16   order_id
        tmp = tmp & pAssembly_Type & "|" '   17   assembly_type
        tmp = tmp & Assembly_Rev & "|" '   18   assembly_rev
        tmp = tmp & pSerial_Number & "|" '   19   asmbly_serial_number
        tmp = tmp & "|" '   20   assembly_option
        tmp = tmp & "|" '   21   verification_asmbly
        tmp = tmp & "|" '   22   lot_id
        tmp = tmp & "|" '   23   batch_id
        tmp = tmp & "YES|" '   24   sampled
        tmp = tmp & "|" '   25   sampling_rate
        tmp = tmp & "|" '   26   sampling_method
        tmp = tmp & Operator_ID & "|" '   27   operator_id
        tmp = tmp & "|" '   28   operator_type

        FormatEventStart = tmp
    End Function

    Private Function FormatProductMeasure(ByRef rec As clsMeasurement) As String
        Dim tmp, Status As String
        Status = IIf(rec.Passed, "PASS", "FAIL")

        tmp = "PRODUCTMEASURE|" '  0    Measurement Record
        tmp = tmp & DCF_Rev & "|" '  1    dcf_rev
        tmp = tmp & rec.name & "|" '  2    test_designator
        tmp = tmp & "|" '  3    subtest_designator
        tmp = tmp & Status & "|" '  4    test_status
        tmp = tmp & "|" '  5    event_duration
        tmp = tmp & rec.MeasuredString & "|" '  6    non_numeric
        tmp = tmp & rec.MeasuredValue.ToString("0.000") & "|" '  7    numeric
        tmp = tmp & rec.Units & "|" '  8    units
        tmp = tmp & "||||||||||||" '  9-20 not used
        tmp = tmp & rec.LowerLimit & "|" ' 21    gen1
        tmp = tmp & rec.UpperLimit & "|" ' 22    gen2
        tmp = tmp & rec.TestGroup & "|" ' 23    gen3
        tmp = tmp & rec.MeasurementTime & "|" ' 24    gen4
        tmp = tmp & rec.Gen5 & "|" ' 25    gen5

        FormatProductMeasure = tmp
    End Function

    Private Function FormatEventStop() As String
        Dim tmp, Status As String
        Dim Duration As Double
        Status = GetTestStatus() 'PASS or FAIL
        Dim testDuration As TimeSpan

        testDuration = pStopTestTime.Subtract(pStartTestTime)
        Duration = CType(testDuration.TotalSeconds, Integer)

        tmp = "EVENTSTOP|" '   0    Event Stop
        tmp = tmp & DCF_Rev & "|" '   1    dcf_rev
        tmp = tmp & Status & "|" '   2    event_status
        tmp = tmp & Duration & "|" '   3    event_duration
        tmp = tmp & "|" '   4    validity
        tmp = tmp & Test_SW_Rev & "|" '   5    gen1
        tmp = tmp & Firmware_Rev & "|" '   6    gen2
        tmp = tmp & pSerial_Number_CM & "|" '   7    gen3
        tmp = tmp & Gen4 & "|" '   8    gen4
        tmp = tmp & Gen5 & "|" '   9    gen5

        FormatEventStop = tmp
    End Function

    Public Sub PrintData(Optional ByRef fname As String = "c:\functest.txt")
        Dim Test_Duration, pf, Test_Result As String
        Dim rec As clsMeasurement
        Dim value As String
        Dim PrintFile As Short
        Dim testDuration As TimeSpan

        testDuration = pStopTestTime.Subtract(pStartTestTime)
        Test_Duration = testDuration.TotalSeconds.ToString("0")
        Test_Result = GetTestStatus()

        PrintFile = FreeFile()
        FileOpen(PrintFile, fname, OpenMode.Output)

        PrintLine(PrintFile, TAB(7), "UUT Type/Rev:", TAB(20), pAssembly_Type & "/" & Assembly_Rev, TAB(45), "Test Step:", TAB(57), pProcess_Step_ID)
        PrintLine(PrintFile, TAB(7), "Ser Number:", TAB(20), pSerial_Number, TAB(45), "Status:", TAB(57), Test_Result)
        PrintLine(PrintFile, TAB(7), "Date/Time:", TAB(20), pStartTestTime.ToShortDateString, TAB(45), "Duration:", TAB(57), Test_Duration)
        PrintLine(PrintFile, TAB(7), "Testset ID:", TAB(20), Test_System_ID, TAB(45), "Fixture:", TAB(57), Fixture_ID)
        PrintLine(PrintFile, TAB(7), "Operator:", TAB(20), Operator_ID, TAB(45), "Test S/W:", TAB(57), Test_SW_Rev)
        PrintLine(PrintFile, "")
        PrintLine(PrintFile, TAB(2), "TEST NAME", TAB(36), "LL", TAB(46), "MEAS", TAB(56), "UL", TAB(66), "STATUS", TAB(72), "TIME")

        For Each rec In TestData
            'If rec.TestRun Then
            If rec.NumericType Then
                value = rec.MeasuredValue.ToString("0.000")
            Else
                value = rec.MeasuredString
            End If

            If rec.Passed Then
                pf = "OK"
            Else
                pf = "FAIL"
            End If
            PrintLine(PrintFile, TAB(2), rec.name, TAB(36), rec.LowerLimit, TAB(46), value, TAB(56), rec.UpperLimit, TAB(66), pf, TAB(72), rec.MeasurementTime)
            'End If
        Next rec
        FileClose(PrintFile)

        Call Shell("notepad /p " & fname, AppWinStyle.MinimizedNoFocus)
    End Sub

    Public Sub PrintData_2(Optional ByRef fname As String = "c:\functest.txt", Optional ByVal PrintFailGroupsOnly As Boolean = False)
        Dim ul, Testname, data, ll, meas As String
        Dim Test_Duration, pf, rec_type, Test_Result As String
        Dim i As Short
        Dim rec As clsMeasurement
        Dim value As String

        Dim PrintFile, MaxTestNum As Short
        Dim TestSegName As String = ""
        Dim TempRec As Short
        Dim j, Last As Short
        Dim y As Object
        Dim xF As New Collection
        Dim xT As New Collection
        Dim x As Collection
        Dim xx As String
        Dim testDuration As TimeSpan

        testDuration = pStopTestTime.Subtract(pStartTestTime)
        Test_Duration = testDuration.TotalSeconds.ToString("0")
        Test_Result = GetTestStatus()

        'The following loop creates a list (collection) of all the unique
        '"TestGroup" values. "xT" represents all groups, "xF" represents Fail groups
        On Error Resume Next
        For Each rec In TestData
            If rec.Passed = False Then Call xF.Add(rec.TestGroup, rec.TestGroup)
            Call xT.Add(rec.TestGroup, rec.TestGroup)
        Next rec
        On Error GoTo 0

        'The following loop creates a comma-delimited string of all the
        '"TestGroup" values for tests that fail
        For Each xx In xF
            If TestSegName <> "" Then TestSegName = TestSegName & ","
            TestSegName = TestSegName & xx
        Next xx

        PrintFile = FreeFile()

        fname = "c:\Andrew Test Report"
        FileOpen(PrintFile, fname, OpenMode.Output)

        'Print #PrintFile, Tab(20); "ANDREW CORPORATION PROPRIETARY"
        PrintLine(PrintFile, "")
        PrintLine(PrintFile, TAB(1), "UUT Type/Rev:", TAB(12), pAssembly_Type & "/" & Assembly_Rev, TAB(40), "Software Rev:", TAB(56), Test_SW_Rev)
        PrintLine(PrintFile, TAB(1), "CM Barcode:", TAB(14), pSerial_Number_CM, TAB(40), "Andrew Barcode:", TAB(58), pSerial_Number)
        PrintLine(PrintFile, TAB(1), "Test Step:", TAB(13), Process_Step_ID, TAB(40), "Slot No.:", TAB(52), "N/A")
        PrintLine(PrintFile, TAB(1), "Date:", TAB(9), pStopTestTime.ToString("dddd, MMM d yyyy"), TAB(40), "Testset ID:", TAB(53), Test_System_ID)
        PrintLine(PrintFile, TAB(1), "Time:", TAB(9), pStopTestTime.ToString("hh:mm:ss"), TAB(40), "Operator ID:", TAB(55), Operator_ID)
        Print(PrintFile, TAB(1), "Fails:", TAB(9), TestSegName)
        PrintLine(PrintFile, TAB(1), "___________________________________________________________________________")
        PrintLine(PrintFile, "")
        PrintLine(PrintFile, TAB(2), "TEST NAME", TAB(36), "LL", TAB(46), "MEAS", TAB(56), "UL", TAB(66), "STATUS", TAB(72), "TIME")
        PrintLine(PrintFile, "")

        'The variable "x" is a reference variable (notice that it is not declared
        'as 'new').  This variable is set to refer to one of the two collections
        '(xT or xF) created above depending on if we should print all data or
        'just failing group data
        If PrintFailGroupsOnly Then
            x = xF
        Else
            x = xT
        End If

        'In the following nested loops, the outer loop iterates over each
        'unique "TestGroup" value while the inner loop iterates over all
        'measurements checking each measurement's "TestGroup" field to see
        'if it matches the outer loops current value.
        For Each xx In x
            For Each rec In TestData
                If rec.TestGroup = xx Then
                    If rec.NumericType Then
                        value = rec.MeasuredValue.ToString("0.000")
                    Else
                        value = rec.MeasuredString
                    End If

                    If rec.Passed Then
                        pf = "OK"
                    Else
                        pf = "FAIL"
                    End If
                    'If we are printing failures , make sure we only print from the last failed test
                    'If Main.ChkPrintFail.value = vbChecked And rec.TestGroup = MaxTestNum Then
                    PrintLine(PrintFile, TAB(2), rec.name, TAB(36), rec.LowerLimit, TAB(46), value, TAB(56), rec.UpperLimit, TAB(66), pf, TAB(72), rec.MeasurementTime)
                End If
            Next rec
            'Print a blank line after each test group
            PrintLine(PrintFile, "")
        Next xx

        FileClose(PrintFile)

        Call Shell("notepad /p " & fname, AppWinStyle.MinimizedNoFocus)
    End Sub

    Public Sub Save_CSV(ByVal fname As String)
        Dim ul, Testname, data, ll, meas As String
        Dim Test_Duration, pf, rec_type, Test_Result As String
        Dim i As Short
        Dim rec As clsMeasurement
        Dim value As String

        Dim PrintFile, MaxTestNum As Short
        Dim TestSegName As String = ""
        Dim TempRec As Short
        Dim j, Last As Short
        Dim y As Object
        Dim xF As New Collection
        Dim xT As New Collection
        Dim x As Collection
        Dim xx As String
        Dim testDuration As TimeSpan

        testDuration = pStopTestTime.Subtract(pStartTestTime)
        Test_Duration = testDuration.TotalSeconds.ToString("0")
        Test_Result = GetTestStatus()

        'The following loop creates a list (collection) of all the unique
        '"TestGroup" values. "xT" represents all groups, "xF" represents Fail groups
        On Error Resume Next
        For Each rec In TestData
            If rec.Passed = False Then Call xF.Add(rec.TestGroup, rec.TestGroup)
            Call xT.Add(rec.TestGroup, rec.TestGroup)
        Next rec
        On Error GoTo 0

        'The following loop creates a comma-delimited string of all the
        '"TestGroup" values for tests that fail
        For Each xx In xF
            If TestSegName <> "" Then TestSegName = TestSegName & ","
            TestSegName = TestSegName & xx
        Next xx

        PrintFile = FreeFile()

        FileOpen(PrintFile, fname, OpenMode.Output)

        WriteLine(PrintFile, "AssemblyType/Rev", pAssembly_Type & "/" & Assembly_Rev)
        WriteLine(PrintFile, "SoftwareRef", Test_SW_Rev)
        WriteLine(PrintFile, "Date", Now)
        WriteLine(PrintFile, "Status", Test_Result)
        WriteLine(PrintFile, "SerialNumber", pSerial_Number)
        WriteLine(PrintFile, "ProcessStep", Process_Step_ID)
        WriteLine(PrintFile, "Testset ID", Test_System_ID)
        WriteLine(PrintFile, "")
        WriteLine(PrintFile, "Measurement Name, LL, Value, UL, Status, MeasurementTime, Gen5")

        'The variable "x" is a reference variable (notice that it is not declared
        'as 'new').  This variable is set to refer to one of the two collections
        '(xT or xF) created above depending on if we should print all data or
        'just failing group data
        '   If PrintFailGroupsOnly Then
        '     Set x = xF
        '   Else
        x = xT
        '   End If

        'In the following nested loops, the outer loop iterates over each
        'unique "TestGroup" value while the inner loop iterates over all
        'measurements checking each measurement's "TestGroup" field to see
        'if it matches the outer loops current value.
        For Each xx In x
            For Each rec In TestData
                If rec.TestGroup = xx Then
                    If rec.NumericType Then
                        value = rec.MeasuredValue.ToString("0.000")
                    Else
                        value = rec.MeasuredString
                    End If

                    If rec.Passed Then
                        pf = "OK"
                    Else
                        pf = "FAIL"
                    End If
                    'If we are printing failures , make sure we only print from the last failed test
                    'If Main.ChkPrintFail.value = vbChecked And rec.TestGroup = MaxTestNum Then
                    WriteLine(PrintFile, rec.name, rec.LowerLimit, value, rec.UpperLimit, pf, rec.MeasurementTime, rec.Gen5)
                End If
            Next rec
            'Print a blank line after each test group
            WriteLine(PrintFile, "")
        Next xx

        FileClose(PrintFile)
    End Sub



    '*****************************************************************************
    'The following functions deal with test limits files.  Only the SetLimitsFile is
    'public.  Call it with empty string for no limits file.
    '*****************************************************************************

    Public Sub SetLimitsFile(ByVal filename As String)
        Call DeleteLimits()
        If filename <> "" Then
            Call ReadLimitsFile(filename)
        End If
    End Sub

    Private Sub ReadLimitsFile(ByVal filename As String)
        Dim fh As Short
        Dim tmps As String
        Dim x() As String
        Dim y As clsMeasurement

        On Error GoTo ErrorHandler
        fh = FreeFile()
        FileOpen(fh, filename, OpenMode.Input)
        Do Until EOF(fh)
            tmps = LineInput(fh)
            x = Split(tmps, ",")
            If UBound(x) >= 3 Then
                y = New clsMeasurement
                y.name = UCase(x(0))
                y.LowerLimit = x(1)
                y.UpperLimit = x(2)
                y.Units = x(3)
                Call LimitsData.Add(y, y.name)
            End If
        Loop

        FileClose(fh)
        Exit Sub
ErrorHandler:
        FileClose(fh)
        Call Err.Raise(Err.Number, "ReadLimitsFile", "Error reading limits file: " & filename & vbNewLine & Err.Description)
    End Sub

    Private Sub DeleteLimits()
        Dim i As Short
        For i = LimitsData.Count() To 1 Step -1
            Call LimitsData.Remove(i)
        Next
    End Sub

    Public Sub New()
        pStartTestTime = Now()
    End Sub
End Class