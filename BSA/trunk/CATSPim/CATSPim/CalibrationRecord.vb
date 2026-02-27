
Imports System.IO

Public Class CalibrationRecord
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Int32, ByVal lpFileName As String) As Int32
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Int32

    Dim TimeDiff1 As Integer = 10
    Dim TimeDiff2 As Integer = 168
    Dim TimeDiff3 As Integer = 10
    Public CalEnable As Boolean = True
    Dim CalRemind As Boolean = True

    Private Sub CalibrationRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '  1481, 1008
        Me.Width = 742
        Me.Height = 530

        Cal_Record.Rows.Clear()
        'PIM700/PIM800等测试频段信息
        If Cal_phasenamelist.Count = 0 Then
            MsgBox("Please input the PN and SN to get the test spec first !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return
        End If

        '------------------文件不存在处理-----------------------------
        Dim CalPathFile As String = pTestSystemPath & "CALPim.ini"
        If File.Exists(CalPathFile) Then
        Else
            MsgBox("There is no Calibration record, and please calibrate it !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return
        End If
        '-----------------------------------------------

        For i = 0 To Cal_phasenamelist.Count - 1
            INI_Read_Cal(1, Cal_phasenamelist(i), 0)
        Next

        If Cal_Record.Rows.Count = 0 Then
            MsgBox("There is no calibrate record to find, please start to calibrate for your test !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End If

    End Sub

    ''Public Sub InitializeCal()
    ''    Dim check_result As Boolean = True
    ''    Dim CalPathFile As String = Application.StartupPath & "\CALPim.ini"
    ''    Dim tmpPath As String = Application.StartupPath & "\tmp.ini"
    ''    Dim ept As New Encryptor

    ''    If File.Exists(CalPathFile) Then '文件存在
    ''        '解密
    ''        Try
    ''            ept.DecryptFile(CalPathFile, tmpPath) '解密完成并把内容保存到tmp.tmp中,效果等于就是复制了一个文件。

    ''            If Read_INI_Field(”CalTimeSpec“, “LowPim_Load", “NA”, tmpPath) = "NA" Or Read_INI_Field(“CalTimeSpec”, ”Standard_110dBm“, “NA”, tmpPath) = "NA" Or Read_INI_Field(“CalTimeSpec”, ”CalEnable“, “NA”, tmpPath) = "NA" Then
    ''                MsgBox("Error，when Initialize Calibration record,please check !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
    ''            Else
    ''                TimeDiff1 = CInt(Read_INI_Field(”CalTimeSpec“, “LowPim_Load", “NA”, tmpPath))
    ''                TimeDiff2 = CInt(Read_INI_Field(“CalTimeSpec”, ”Standard_110dBm“, “NA”, tmpPath))
    ''                CalEnable = Read_INI_Field(“CalTimeSpec”, ”CalEnable“, “NA”, tmpPath)
    ''            End If

    ''        Catch ex As Exception
    ''            Throw New Exception("Error，when Initialize Calibration record,please check !." & ex.Message)
    ''        End Try
    ''    Else '文件不存在
    ''        MsgBox("There is no Calibration record,pleas check !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
    ''    End If

    ''    '删除临时文件
    ''    ' If File.Exists(tmpPath) Then File.Delete(tmpPath)

    ''End Sub

    Public Function check_Cal(ByVal phase As String, ByVal phase_Spec As Single) As Boolean

        Dim check_result As Boolean = True

        Cal_Record.Rows.Clear()

        If Cal_phasenamelist.Count = 0 Then
            MsgBox("Please input the PN and SN to get the test spec first !.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            Return False
        End If

        '------------------文件不存在处理-----------------------------
        Dim CalPathFile As String = pTestSystemPath & "CALPim.ini"
        If File.Exists(CalPathFile) Then

        Else
            If DateDiff("h", CDate("2018/07/30 10:02:08"), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > 0 Then
                Return False
            Else
                If CalRemind = True Then
                    CalRemind = False
                    MsgBox("There is no Calibration record, and please calibrate before 2018/07/30 10:02:08,and click OK to continue to test!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End If
                Return True
            End If

        End If '
        '-----------------------------------------------

        If INI_Read_Cal(2, phase, phase_Spec) = False Then check_result = False

        Return check_result
    End Function


    Private Function INI_Read_Cal(ByVal sel As Integer, ByVal phase As String, ByVal phase_Spec As Single)

        Dim check_result As Boolean = True
        Dim CalPathFile As String = pTestSystemPath & "CALPim.ini"
        Dim tmpPath As String = pTestSystemPath & "tmp.ini"
        Dim ept As New Encryptor

        '针对苏州工厂RD使用，功率校准为一周做一次
        If pFactory = "ASZ" And pRTP.product_mode = "RD" Then
            TimeDiff3 = 168
        Else
            TimeDiff3 = 10
        End If

        If File.Exists(CalPathFile) Then '文件存在
            '解密
            Try
                ept.DecryptFile(CalPathFile, tmpPath) '解密完成并把内容保存到tmp.tmp中,效果等于就是复制了一个文件。

                Cal_Record.Rows.Add()
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(0).Value = phase
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(1).Value = Read_INI_Field(phase, phase & "_PC_name", “NA”, tmpPath)
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(2).Value = Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath)
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(3).Value = Read_INI_Field(phase, phase & "_LowPim_Load_Spec", “NA”, tmpPath)
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(4).Value = Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath)
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(5).Value = Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath)
                Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(6).Value = Read_INI_Field(phase, phase & "_Op_ID", “NA”, tmpPath)


                ' h 时 
                'n 分钟 
                's 秒 
                '----sel=1
                If sel = 1 Then '颜色显示控制
                    If Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
                    ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff1 Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red ' _LowPim_Load校准的间隔时间，目前是4小时
                    End If

                    If Read_INI_Field(phase, phase & "_LowPim_Load_Spec", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(3).Style.BackColor = Color.Red
                    End If

                    If Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(4).Style.BackColor = Color.Red
                    ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff2 Then ' _Standard_110dBm校准的间隔时间，目前是7天
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(4).Style.BackColor = Color.Red
                    End If

                    If Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(5).Style.BackColor = Color.Red
                    ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff3 Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(5).Style.BackColor = Color.Red ' _LowPim_Load校准的间隔时间，目前是4小时
                    End If
                    '-------

                    '----sel=2
                ElseIf sel = 2 Then
                    If Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
                        check_result = False
                    Else
                        If DateDiff("h", CDate(Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff1 And DateDiff("h", CDate(Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) <= TimeDiff1 + 2 Then ' _LowPim_Load校准的间隔时间，目前是4小时
                            Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(2).Style.BackColor = Color.Red
                            MsgBox("Remind: Need to Calibrate for " & phase & " test with Low Pim_Load in next two hours,please pay attention to that and click OK to continue to test !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_LowPim_Load", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff1 + 2 Then
                            check_result = False
                        End If
                    End If

                    If Read_INI_Field(phase, phase & "_LowPim_Load_Spec", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(3).Style.BackColor = Color.Red
                        check_result = False
                    Else
                        If phase_Spec - CSng(Read_INI_Field(phase, phase & "_LowPim_Load_Spec", “NA”, tmpPath)) < 10 Then
                            Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(3).Style.BackColor = Color.Red
                            MsgBox("Error，LowPim_Load calibration spec( " & Read_INI_Field(phase, phase & "_LowPim_Load_Spec", “NA”, tmpPath) & " )should be 10 smaller than Antenna PIm test spec( “ & phase_Spec & ” )please recalibration for LOW_PIM_LOAD  ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                            check_result = False
                        End If
                    End If

                    If Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(4).Style.BackColor = Color.Red
                        check_result = False
                    Else
                        If DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff2 And DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) <= TimeDiff2 + 2 Then ' _Standard_110dBm校准的间隔时间，目前是7天
                            Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(4).Style.BackColor = Color.Red
                            MsgBox("Remind: Need to Calibrate for " & phase & " test with Standard_110dBm in next two hours,please pay attention to that and click OK to continue to test !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Standard_110dBm", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff2 + 2 Then
                            check_result = False
                        End If
                    End If

                    If Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath) = "NA" Then
                        Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(5).Style.BackColor = Color.Red
                        check_result = False
                    Else
                        If DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff3 And DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) <= TimeDiff3 + 2 Then ' _Power校准的间隔时间，目前是4小时
                            Cal_Record.Rows(Cal_Record.Rows.Count - 1).Cells(5).Style.BackColor = Color.Red
                            MsgBox("Remind: Need to Calibrate for " & phase & " test with Power Meter in next two hours,please pay attention to that and click OK to continue to test !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                        ElseIf DateDiff("h", CDate(Read_INI_Field(phase, phase & "_Power", “NA”, tmpPath)), CDate(Format(Now(), "yyyy/MM/dd HH:mm:ss"))) > TimeDiff3 + 2 Then
                            check_result = False
                        End If
                    End If
                    '-------
                End If

            Catch ex As Exception
                Throw New Exception("Read Calibration record error!" & ex.Message)
                Return False
            End Try
        Else '文件不存在
            Return False
        End If

        '删除临时文件
        If File.Exists(tmpPath) Then File.Delete(tmpPath)

        Return check_result

    End Function
    '                                    PIM700                PC                     ID                      1
    Public Function INI_Write_Cal(ByVal phase As String, ByVal info1 As String, ByVal info2 As String， ByVal info3 As Single， ByVal sel As String)

        Dim CalPathFile As String = pTestSystemPath & "CALPim.ini"
        Dim tmpPath As String = pTestSystemPath & "tmp.ini"

        Dim ept As New Encryptor

        Try
            If File.Exists(CalPathFile) Then ept.DecryptFile(CalPathFile, tmpPath) '文件存在'解密,解密完成并把内容保存到tmp.tmp中,效果等于就是复制了一个文件。

            WritePrivateProfileString(phase, phase & "_PC_name", info1, tmpPath)

            If sel = 1 Then
                WritePrivateProfileString(phase, phase & "_LowPim_Load", Format(Now(), "yyyy/MM/dd HH:mm:ss"), tmpPath)
                WritePrivateProfileString(phase, phase & "_LowPim_Load_Spec", CStr(info3), tmpPath)
            End If
            If sel = 2 Then WritePrivateProfileString(phase, phase & "_Standard_110dBm", Format(Now(), "yyyy/MM/dd HH:mm:ss"), tmpPath)
            If sel = 3 Then WritePrivateProfileString(phase, phase & "_Power", Format(Now(), "yyyy/MM/dd HH:mm:ss"), tmpPath)

            WritePrivateProfileString(phase, phase & "_Op_ID", info2, tmpPath)

            ept.EncryptFile(tmpPath, CalPathFile) ' 写完要加密保存到CATSPim.ini

            '删除临时文件
            If File.Exists(tmpPath) Then File.Delete(tmpPath)

        Catch ex As Exception
            Throw New Exception("Write(INI) Calibration record error!" & ex.Message)
            Return False
        End Try

        Return True

    End Function

    Private Function Read_INI_Field(ByVal AppName As String, ByVal KeyName As String, ByVal Default_Renamed As String, ByVal filename As String) As String
        Dim Valid, Size As Short
        Dim ReturnString As String
        ReturnString = Space(255)
        Size = Len(ReturnString)
        Valid = GetPrivateProfileString(AppName, KeyName, Default_Renamed, ReturnString, Size, filename)
        Read_INI_Field = Microsoft.VisualBasic.Left(ReturnString, Valid)
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim CalPathFile As String = pTestSystemPath & "CALPim.ini"
        Dim tmpPath As String = pTestSystemPath & "tmp.ini"

        Dim ept As New Encryptor
        ept.EncryptFile(tmpPath, CalPathFile)
    End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs)
    '    Dim ept As New Encryptor
    '    If File.Exists(”C: \CATS\test_system\CALPim.ini“) Then ept.DecryptFile(”C: \CATS\test_system\CALPim.ini“, ”C: \CATS\test_system\CALPim_Temp.ini“)

    'End Sub
End Class