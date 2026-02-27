Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
<System.Runtime.InteropServices.ProgId("clsGlobals_NET.clsGlobals")> Public Class clsGlobals

    'Public AbortFlag As Boolean
    'Public INI_File As String
    '
    'Public UUT_Type As String
    'Public SW_Version As String
    'Public MTR_Version As String
    'Public LimitsFile As String
    'Public BarcodeMatch As String


    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer

    Public Structure AssemblyType
        Dim lenT As Int32
        Dim lenP As Int32
        Dim types As String
        Dim progs As String
    End Structure

    Private Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As String
        Dim lpDesktop As String
        Dim lpTitle As String
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwYSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As Integer
        Dim hStdInput As Integer
        Dim hStdOutput As Integer
        Dim hStdError As Integer

        Public Sub Initialize()
            cb = 0
            lpReserved = ""
            lpDesktop = ""
            lpTitle = ""
            dwX = 0
            dwY = 0
            dwXSize = 0
            dwYSize = 0
            dwXCountChars = 0
            dwYCountChars = 0
            dwFillAttribute = 0
            dwFlags = 0
            wShowWindow = 0
            cbReserved2 = 0
            lpReserved2 = 0
            hStdInput = 0
            hStdOutput = 0
            hStdError = 0
        End Sub

    End Structure

    Private Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwProcessID As Integer
        Dim dwThreadID As Integer
    End Structure

    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer

    Private Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Integer, ByVal lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As Integer, ByVal lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer

    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer

    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer

    Private Const NORMAL_PRIORITY_CLASS As Integer = &H20
    Private Const INFINITE As Short = -1

    Const UserAbort As Integer = vbObjectError + 100

    Private Structure SingleType
        Dim s As Single
    End Structure

    Private Structure Byte4Type
        <VBFixedArray(3)> Dim b() As Byte

        Public Sub Initialize()
            ReDim b(3)
        End Sub
    End Structure

    Public Function Read_INI_Field(ByVal AppName As String, ByVal KeyName As String, ByVal Default_Renamed As String, Optional ByVal filename As String = "") As String
        Dim Valid, Size As Short
        Dim ReturnString As String
        If filename = "" Then filename = INI_File

        ReturnString = Space(255)
        Size = Len(ReturnString)

    Valid = GetPrivateProfileString(AppName, KeyName, Default_Renamed, ReturnString, Size, filename)
        Read_INI_Field = Left(ReturnString, Valid)
    End Function

    Public Sub Write_INI_Field(ByVal AppName As String, ByVal KeyName As String, ByVal value As String, Optional ByVal filename As String = "")
        If filename = "" Then filename = INI_File
        Call WritePrivateProfileString(AppName, KeyName, value, filename)
    End Sub

    Public Function ReadMessage(ByVal MsgName As String, Optional ByRef MsgDefault As String = "") As String
        Dim tmp As String
        tmp = Read_INI_Field("MessageBox", MsgName, MsgDefault, INI_File)
        tmp = Replace(tmp, "\n", vbNewLine)
        ReadMessage = tmp
    End Function

    Public Function GetComputerName() As String

        GetComputerName = SystemInformation.ComputerName

    End Function


    Public Function BitSet(ByVal Word As Short, ByVal Bit As Short) As Boolean
        Dim Mask As Short

        If Bit > 14 Then
            Mask = &H8000S
        ElseIf Bit < 0 Then
            Mask = 0
        Else
            Mask = 2 ^ Bit
        End If

        BitSet = ((Word And Mask) <> 0)
    End Function

    Public Function FormatSecondsOn(ByVal secs As Integer) As String
        Dim interval As New TimeSpan(0, 0, secs)
        FormatSecondsOn = interval.ToString
    End Function

    Function DBMtoWATTS(ByRef x As Single) As Single
        DBMtoWATTS = 10 ^ ((x - 30) / 10)
    End Function

    Public Sub Delay(ByVal msec As Integer)
        Dim StartTime, WaitTime As Single
        WaitTime = CDbl(msec) / 1000.0#
        StartTime = VB.Timer()
        Do
            Call MyDoEvents()
        Loop Until ElapsedTime(StartTime, VB.Timer()) > WaitTime
    End Sub

    Public Function ElapsedTime(ByVal StartTime As Single, ByVal StopTime As Single) As Single
        Const SecsPerDay As Double = 24.0# * 60 * 60
        ElapsedTime = New_Mod(StopTime - StartTime, SecsPerDay)
    End Function

    Public Function New_Mod(ByVal value As Double, ByVal modulus As Double) As Double
        Dim N As Integer
        N = Int(value / modulus)
        New_Mod = value - N * modulus
    End Function

    Public Sub MyDoEvents()
        If AbortFlag Then
            AbortFlag = False
            Call Err.Raise(UserAbort, , "User Aborted Test")
        End If
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Function Roundoff(ByVal Number As Double, ByVal PowerOf10 As Short) As Double
        Roundoff = (10 ^ PowerOf10) * System.Math.Round(Number / (10 ^ PowerOf10))
    End Function

    Public Function GetMax(ByVal ParamArray Values() As Single) As Single
        'this function compares any number of values and returns the maximum
        Dim x, tmpMax As Single
        Dim FirstTime As Boolean

        FirstTime = True
        For Each x In Values
            If FirstTime Then
                tmpMax = x
                FirstTime = False
            Else
                If x > tmpMax Then tmpMax = x
            End If
        Next x
        GetMax = tmpMax
    End Function

    Public Function GetMin(ByVal ParamArray Values() As Single) As Single
        'this function compares any number of values and returns the minimum
        Dim x, tmpMin As Single
        Dim FirstTime As Boolean

        FirstTime = True
        For Each x In Values
            If FirstTime Then
                tmpMin = x
                FirstTime = False
            Else
                If x < tmpMin Then tmpMin = x
            End If
        Next x
        GetMin = tmpMin
    End Function

    Public Function InputBoxMatch(ByRef Prompt As String, Optional ByRef Title As String = "", Optional ByRef Default_Renamed As String = "", Optional ByRef MatchStr As String = "*") As String

        Dim tmpstr As String
        Do
            tmpstr = InputBox(Prompt, Title, Default_Renamed)
            If tmpstr = "" Then Exit Do

            If tmpstr Like MatchStr Then
                Exit Do
            End If

            If MsgBox("Input string (" & tmpstr & ") is not in the correct format", MsgBoxStyle.RetryCancel, "Invalid Bar Code") = MsgBoxResult.Cancel Then Exit Do
        Loop
        InputBoxMatch = tmpstr
    End Function

    '*** Byte/bit functions
    Public Function Bytes2Long(ByRef bytearray() As Byte, Optional ByRef Offset As Short = 0) As Integer
        Dim msw, lsw As Short
        msw = Bytes2Integer(bytearray, Offset + 0)
        lsw = Bytes2Integer(bytearray, Offset + 2)
        Bytes2Long = FormLong(msw, lsw)
    End Function

    Public Function Bytes2Integer(ByRef bytearray() As Byte, Optional ByRef Offset As Short = 0) As Short
        Bytes2Integer = FormInt(bytearray(Offset + 0), bytearray(Offset + 1))
    End Function

    Public Function Bytes2String(ByRef bytearray() As Byte) As String
        'UPGRADE_ISSUE: Constant vbUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Bytes2String = StrConv(System.Text.UnicodeEncoding.Unicode.GetString(bytearray), 64)
    End Function

    Public Function String2Bytes(ByVal s As String) As Byte()
        'UPGRADE_ISSUE: Constant vbFromUnicode was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        'UPGRADE_TODO: Code was upgraded to use System.Text.UnicodeEncoding.Unicode.GetBytes() which may not have the same behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="93DD716C-10E3-41BE-A4A8-3BA40157905B"'
        String2Bytes = System.Text.UnicodeEncoding.Unicode.GetBytes(StrConv(s, 128))
    End Function

    Public Function FormWord(ByVal msb As Byte, ByVal lsb As Byte) As Integer
        FormWord = CInt(msb) * 256 + CInt(lsb)
    End Function

    Public Function FormInt(ByVal msb As Byte, ByVal lsb As Byte) As Short
        FormInt = &H100S * CShort(msb And &H7FS) + lsb
        If (msb And &H80S) <> 0 Then FormInt = FormInt Or &H8000S
    End Function

    Public Function FormLong(ByVal msw As Integer, ByVal lsw As Integer) As Integer
        FormLong = &H10000 * CShort(msw And &H7FFF) + CShort(lsw And &HFFFF)
        If (msw And &H8000) <> 0 Then FormLong = FormLong Or &H80000000
    End Function

    Public Function LowerByte(ByVal value As Integer) As Byte
        LowerByte = value And &HFF
    End Function

    Public Function UpperByte(ByVal value As Integer) As Byte
        UpperByte = (value And &HFF00) \ 256
    End Function

    Public Sub Long2Bytes(ByVal LVal As Integer, ByRef bytearray() As Byte, Optional ByRef Offset As Short = 0)
        bytearray(Offset + 0) = (LVal And &H7F000000) \ &H1000000
        If LVal < 0 Then bytearray(Offset + 0) = bytearray(Offset + 0) Or &H80S
        bytearray(Offset + 1) = (LVal And &HFF0000) \ &H10000
        bytearray(Offset + 2) = UpperByte(LVal)
        bytearray(Offset + 3) = LowerByte(LVal)
    End Sub

    'Public Sub Single2Bytes(ByVal SVal As Single, ByRef bytearray() As Byte, Optional ByVal Offset As Short = 0)
    '       Dim Sing As SingleType
    '       Dim Bytes4 As Byte4Type
    '       Bytes4.Initialize()
    '	Dim i As Short
    '	Sing.s = SVal
    '	'UPGRADE_ISSUE: LSet cannot assign one type to another. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="899FA812-8F71-4014-BAEE-E5AF348BA5AA"'
    '       Bytes4 = LSet(Sing)
    '	For i = 0 To 3
    '		bytearray(Offset + i) = Bytes4.b(i)
    '	Next 
    'End Sub

    'Public Function Bytes2Single(ByRef bytearray() As Byte, Optional ByVal Offset As Short = 0) As Object
    '       Dim Sing As SingleType
    '       Dim Bytes4 As Byte4Type
    '       Bytes4.Initialize()
    '	Dim i As Short
    '	For i = 0 To 3
    '		Bytes4.b(i) = bytearray(Offset + i)
    '	Next 
    '	'UPGRADE_ISSUE: LSet cannot assign one type to another. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="899FA812-8F71-4014-BAEE-E5AF348BA5AA"'
    '	Sing = LSet(Bytes4)
    '	'UPGRADE_WARNING: Couldn't resolve default property of object Bytes2Single. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '	Bytes2Single = Sing.s
    'End Function

    Public Function ReadFileByteArray(ByVal filename As String, ByVal index As Integer, ByVal NumBytes As Short, ByRef OutBytes() As Byte) As Boolean
        Dim TmpBytes() As Byte
        Dim fh, i As Short

        ReDim TmpBytes(NumBytes)
        Try

            fh = FreeFile()
            FileOpen(fh, filename, OpenMode.Binary)
            FileGet(fh, TmpBytes, index + 1)
            FileClose(fh)

            For i = 0 To NumBytes - 1
                OutBytes(i) = TmpBytes(i + 1)
            Next

            ReadFileByteArray = True
        Catch ex As Exception
            FileClose(fh)
        End Try
    End Function


    '*************************************************************************
    'Conversion & Utility Functions
    '*************************************************************************

    Public Function SplitString(ByVal InpStr As String, ByRef Fields As Collection, Optional ByVal delim As Object = " ") As Short
        Dim tok As String
        Do
            tok = GetToken(InpStr, delim)
            If tok = "" Then Exit Do
            Call Fields.Add(tok)
        Loop
        SplitString = Fields.Count()
    End Function

    Public Function GetToken(ByRef InpStr As String, Optional ByVal delim As String = " ") As String
        Dim i, i2 As Short

        GetToken = ""
        'Find first character in string that is NOT a delimiter character
        For i = 1 To Len(InpStr)
            If Mid(InpStr, i, 1) <> delim Then Exit For
            If i = Len(InpStr) Then Exit Function
        Next

        i2 = InStr(i, InpStr, delim, CompareMethod.Text)
        If i2 = 0 Then i2 = Len(InpStr) + 1

        GetToken = Mid(InpStr, i, i2 - i)
        InpStr = Mid(InpStr, i2 + 1)
    End Function

    Public Function GetString(ByRef InputStr As String, ByRef LeftDelim As String, ByRef RightDelim As String) As String
        Dim i1, i2 As Short
        Dim result As String = ""
        i1 = InStr(1, InputStr, LeftDelim, CompareMethod.Text)
        If i1 > 0 Then
            i2 = InStr(i1 + Len(LeftDelim), InputStr, RightDelim, CompareMethod.Text)
            If i2 > i1 Then
                result = Mid(InputStr, i1 + Len(LeftDelim), i2 - (i1 + Len(LeftDelim)))
            End If
        End If
        GetString = result
    End Function

    Public Function ParseHexString(ByRef InputString_Renamed As String, ByRef StartByte As Short, Optional ByRef NumBytes As Short = 1) As String
        Dim StartIndex, StrLength As Short

        StartIndex = StartByte * 2 - 1
        StrLength = NumBytes * 2
        If StrLength < 0 Then StrLength = Len(InputString_Renamed) - StartIndex + 1

        ParseHexString = Mid(InputString_Renamed, StartIndex, StrLength)
    End Function

    Public Function Hex2String(ByRef InputString_Renamed As String) As String
        Dim i As Short
        Dim result As String = ""
        Dim tmp As Byte = 0
        For i = 1 To Len(InputString_Renamed) / 2
            tmp = Hex2Byte(ParseHexString(InputString_Renamed, i))
            result = Chr(tmp)
        Next
        Hex2String = result
    End Function

    Public Function Byte2Hex(ByVal MyByte As Byte) As String
        Byte2Hex = Hex(MyByte)
        If Len(Byte2Hex) = 1 Then Byte2Hex = "0" & Byte2Hex
    End Function

    Public Function Hex2Byte(ByRef MyHex As String) As Byte
        If Len(MyHex) > 2 Then Call Err.Raise(vbObjectError + 1, "Hex2Byte", "Invalid Hex conversion")
        Hex2Byte = Val("&H" & MyHex)
    End Function

    Public Function Int2Hex(ByVal MyInt As Short) As String
        Int2Hex = Hex(MyInt)
        Int2Hex = New String("0", 4 - Len(Int2Hex)) & Int2Hex 'Pad with enough zeros
    End Function

    Public Function Hex2Int(ByRef MyHex As String) As Short
        If Len(MyHex) > 4 Then Call Err.Raise(vbObjectError + 1, "Hex2Int", "Invalid Hex conversion")
        Hex2Int = Val("&H" & MyHex)
    End Function

    Public Function TrimExtra(ByRef InString As String) As String
        TrimExtra = Trim(InString) 'gets rid of trailing spaces
        TrimExtra = Left(TrimExtra, Len(TrimExtra) - 1) 'gets rid of end of string character that the dll leaves on there
    End Function

    'Public Function Bytes2Hex(bytes() As Byte, ByVal L As Integer) As String
    '  Dim i As Integer
    '  For i = 0 To L - 1
    '    Bytes2Hex = Bytes2Hex & Byte2Hex(bytes(i))
    '  Next
    'End Function

    Public Function Bytes2Hex(ByRef bytes() As Byte, Optional ByVal SpaceDelim As Boolean = False) As String
        Dim i As Short
        Dim result As String = ""
        Dim tmp As String
        tmp = IIf(SpaceDelim, " ", "")
        For i = LBound(bytes) To UBound(bytes)
            result = Byte2Hex(bytes(i)) & tmp
        Next
        Bytes2Hex = result
    End Function

    Public Function Hex2Bytes(ByVal HexString As String, Optional ByVal SpaceDelim As Boolean = False) As Byte()
        Dim N, i, L As Short
        Dim b() As Byte
        N = IIf(SpaceDelim, 3, 2)
        L = Len(HexString) / N
        ReDim b(L - 1)
        For i = 0 To L - 1
            b(i) = Hex2Byte(Mid(HexString, i * N + 1, 2))
        Next
        Hex2Bytes = b
    End Function


    '*** Process Check stuff
    Public ReadOnly Property DoProcessCheck() As Boolean
        Get
            'DoProcessCheck = ProcessCheck.PC_Check
            DoProcessCheck = pCheck.PC_Check
        End Get
    End Property

    Public Sub Get_PC_Fields(ByRef PCstatus As String, ByRef PCstring As String)
        PCstatus = Common.PC_Status
        PCstring = Common.PC_String
    End Sub

    'Public Function RunProcessCheck(ByVal UUTType As String, ByVal TestStep As String, _
    ''  Barcode As String, Operator As String, CelesticaBarcode As String, _
    ''  FMR1Barcode As String, FNT2Barcode As String, ByVal KillResponseFile As Boolean) As Boolean
    '  'This function returns the Andrew barcode or else empty string if the process
    '  'check fails
    '
    '  Dim recs() As PC_Record, PassStatus As Boolean ', ReturnedOperator As String
    '
    '  ReDim recs(1 To 1)
    '
    '  'PC Stuff
    '  recs(1).TestStep = TestStep
    '  recs(1).UUTType = UUTType
    '  recs(1).AndrewBarcode = Barcode
    '  recs(1).Slot = 1
    '
    '  If Not ProcessCheck.WriteRequestFile(PC_RequestFile, Operator, recs()) Then
    '  'If Not ProcessCheck.WriteRequestFile("c:\pcrequest1.txt", Operator, recs()) Then
    '    Call MsgBox("Process Check write request failed", vbCritical, "Error")
    '    Exit Function
    '  End If
    '
    '  On Error Resume Next
    '  'Kill "c:\PCResponse.txt"
    '  Kill PC_ResponseFile
    '  On Error GoTo 0
    '
    '  'Here's where we'll shell out (ExecCmd) to call the ProcessCheck executable'
    '  'call ExecCmd("ProcessCheck c:\pcrequest.txt c:\pcresponse.txt")
    '  'Call ExecCmd.ExecCmd("javaw cli.ProcessCheck c:\PCRequest.txt c:\PCResponse.txt")
    '  Call Globals.ExecCmd(PC_CommandLine & " " & PC_RequestFile & " " & PC_ResponseFile)
    '
    '  If ProcessCheck.ReadResponseFile(PC_ResponseFile, Operator, PassStatus, recs()) Then
    '    If PassStatus And UBound(recs()) > 0 Then
    '      Barcode = recs(1).AndrewBarcode
    '      CelesticaBarcode = recs(1).CelesticaBarcode
    '      FMR1Barcode = recs(1).FMR1Barcode
    '      FNT2Barcode = recs(1).FNT2Barcode
    '      PC_Status = recs(1).Status
    '      PC_String = recs(1).ErrorMsg
    '
    '      'Eliminate file so we don't reuse for next product.
    '      On Error Resume Next
    '      If KillResponseFile Then Kill "c:\PCResponse.txt"
    '      On Error GoTo 0
    '      RunProcessCheck = True
    '    End If
    '    If recs(1).Status = False Then
    '      Call MsgBox("Process Check Error Message: " & vbNewLine & recs(1).ErrorMsg, vbCritical, "Error")
    '    End If
    '  End If
    'End Function
    '
    Public Sub Get_Testset_INI_Info()
        'Testset_ID = GetComputerName()
        Testset_ID = ReadTestsetINI("General", "Testset_ID", GetComputerName())
        If Testset_ID = "" Then
            Testset_ID = InputBox("Could not find Testset_ID field in '.INI' file.  Please enter TestSet ID")
        End If
        Location = ReadTestsetINI("General", "Location", " ")

        '  'Process Check
        '  PC_Check = (UCase(ReadTestsetINI("ProcessCheck", "Enable", "FALSE")) = "TRUE")
        '  PC_CommandLine = ReadTestsetINI("ProcessCheck", "Program", "")
        '  If PC_CommandLine = "" Then PC_Check = False
        '  PC_RequestFile = ReadTestsetINI("ProcessCheck", "RequestFile", "c:\PCRequest.txt")
        '  PC_ResponseFile = ReadTestsetINI("ProcessCheck", "ResponseFile", "c:\PCResponse.txt")
        '  Kill_Response_File = (UCase(ReadTestsetINI("ProcessCheck", "KillResponseFile", "TRUE")) = "TRUE")
        '
        '  'Data Collection
        '  XferData = (UCase(ReadTestsetINI("DataCollection", "Enable", "FALSE")) = "TRUE")
        '  XferProg = ReadTestsetINI("DataCollection", "Program", "")
        '  If XferProg = "" Then XferData = False
        '  DataFile = ReadTestsetINI("DataCollection", "DataFile", "c:\testdata.dcf")

        'Printing Options
        PassingUnits = (UCase(ReadTestsetINI("Print", "PassingUnits", "FALSE")) = "TRUE")
        FailingUnits = (UCase(ReadTestsetINI("Print", "FailingUnits", "FALSE")) = "TRUE")
        FailingGroups = (UCase(ReadTestsetINI("Print", "FailingGroups", "FALSE")) = "TRUE")
    End Sub


    'Public Sub ExecuteCommand(ByVal cmd As String, Optional ByVal msecTimeout As Long = -1)
    '  Call ExecCmd.ExecCmd(cmd, msecTimeout)
    'End Sub
    '
    Private Function ReadTestsetINI(ByVal App As String, ByVal Key As String, ByVal Def As String) As String
        Dim tmp As String
        Const TestsetINI As String = "c:\testset.ini"

        tmp = Read_INI_Field(App, Key, Def, INI_File)
        If tmp = "" Then
            tmp = Read_INI_Field(App, Key, "", TestsetINI)
        End If
        ReadTestsetINI = tmp
    End Function

    Private Sub Class_Initialize_Renamed()
        Common.Globals_Renamed = Me
        BarcodeMatch = "*"
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Public Function ExecCmd(ByVal cmdline As String, Optional ByRef msecTimeout As Integer = -1) As Integer
        Dim proc As PROCESS_INFORMATION
        Dim start As New STARTUPINFO
        start.Initialize()
        Dim ret As Integer

        ' Initialize the STARTUPINFO structure:
        start.cb = Len(start)
        start.dwFlags = AppWinStyle.MinimizedNoFocus

        ' Start the shelled application:
        ret = CreateProcessA(vbNullString, cmdline, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, vbNullString, start, proc)

        ' Wait for the shelled application to finish:
        ret = WaitForSingleObject(proc.hProcess, msecTimeout)
        Call GetExitCodeProcess(proc.hProcess, ret)
        Call CloseHandle(proc.hThread)
        Call CloseHandle(proc.hProcess)
        ExecCmd = ret
    End Function

    Public Function ExecCmd2(ByVal cmdline As String, ByRef cmdoutput As String, Optional ByRef msecTimeout As Integer = -1, Optional ByVal tmpfile As String = "c:\tmpresult.txt") As Integer
        Dim tmp As String
        Dim fh As Short
        cmdoutput = ""
        ExecCmd2 = ExecCmd("cmd /c " & cmdline & " > " & tmpfile, msecTimeout)

        Try
            fh = FreeFile()
            FileOpen(fh, tmpfile, OpenMode.Input)
            Do While Not EOF(fh)
                tmp = LineInput(fh)
                cmdoutput = cmdoutput & tmp & vbNewLine
            Loop
        Catch
            FileClose(fh)
        End Try
    End Function

    Public Function ReadTextFile(ByVal fname As String) As String
        Dim fh As Short
        Dim bytes() As Byte
        Dim L As Integer

        Try
            L = FileLen(fname)
            ReDim bytes(L)
            fh = FreeFile()
            FileOpen(fh, fname, OpenMode.Binary, OpenAccess.Read)
            FileGet(fh, bytes, , True)
            FileClose(fh)
            ReadTextFile = Bytes2String(bytes)
        Catch ex As Exception
            ReadTextFile = ""
        End Try
    End Function
End Class