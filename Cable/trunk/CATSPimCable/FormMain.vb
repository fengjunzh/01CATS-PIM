Imports System.DirectoryServices.ActiveDirectory
Imports System.Xml.Serialization
Imports System.Text
Imports Microsoft.VisualBasic
Imports Sunisoft.IrisSkin
Imports System.Text.RegularExpressions

Public Class FormMain
    ''' <summary>
    ''' 解密参数path路径下的加密文件CATSConfig.xml内容并返回CATSConfig
    ''' CATSConfig文件保存数据库连接字符串和工厂，模式等信息
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private Function GetCATSConfigure(path As String) As CATSConfig
        Try
            Dim resp As New CATSConfig

            Dim tmpPath As String = New IO.FileInfo(path).DirectoryName & "\tmp.tmp"
            '解密path路径文件（将文件内容解密到tmp.tmp中）
            Dim ept As New Encryptor
            ept.DecryptFile(path, tmpPath)

            'ept.EncryptFile("C:\CATS\test_system\CATS.xmls", "C:\CATS\test_system\tmp.tmp")

            Dim xs As New XmlSerializer(GetType(CATSConfig))
            Dim sr As New System.IO.StreamReader(tmpPath)

            Try
                'xml反序列化生成实例化的CATSConfig
                resp = xs.Deserialize(sr)
            Catch ex As Exception
                Return Nothing
                'Throw New Exception("Deseriallize fail.")
            Finally
                sr.Close()
                If IO.File.Exists(tmpPath) Then IO.File.Delete(tmpPath)
            End Try

            Return resp

        Catch ex As Exception
            Return Nothing
            'Throw New Exception("GetCATSConfigure()::" & ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' 如果pCatsCfgFile(c:\cats\test_system\)路径下的CATS.xmls文件不存在，则复制服务器路径svrPath下
    ''' 的CATS.xmls文件到测试系统目录下(c:\cats\test_system\)，比较两个文件的创建时间，如果服务器的文件
    ''' 创建时间较晚，则更新（复制并覆盖）测试系统目录下的文件
    ''' </summary>
    Private Sub CopyCATSConfigure()
        Try
            Dim svrPath As String = Application.StartupPath & "\CATS.xmls"
            'Dim svrPath As String = "C:\CATS\Test_Application\Heliax\CATSPim\CATS.xmls"
            'sysPath = c:\cats\test_system\CATS.xmls
            'pCatsCfgFile = c:\cats\test_system\CATS.xmls
            Dim sysPath As String = pCatsCfgFile

            If IO.File.Exists(pCatsCfgFile) = False Then
                IO.File.Copy(svrPath, pCatsCfgFile)
            Else
                Dim svrCfg As CATSConfig
                Dim sysCfg As CATSConfig
                If IO.File.Exists(svrPath) = True Then
                    svrCfg = GetCATSConfigure(svrPath)
                    sysCfg = GetCATSConfigure(sysPath)
                    If sysCfg Is Nothing Then
                        IO.File.Copy(svrPath, pCatsCfgFile, True)
                    Else
                        If DateTime.Compare(CDate(svrCfg.Property_.CreateTime), CDate(sysCfg.Property_.CreateTime)) > 0 Then
                            IO.File.Copy(svrPath, pCatsCfgFile, True)
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            Throw New Exception("CopyCATSConfigure()::" & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 读取配置文件CATSPimTestConfig.xml和CATS.xmls
    ''' </summary>
    Private Sub InitializeConfigure()
        Try
            '判断文件夹c:\cats\test_system是否存在，若不存在则创建，然后从程序启动目录下（bin\debug或release)
            '复制CatsPimConfig.xml文件到系统目录下
            'pTestSystemPath = c:\cats\test_system\
            'pAppCfgFileName = CATSPimTestConfig.xml
            'pAppCfgFilePath = c:\cats\test_system\CATSPimTestConfig.xml
            If System.IO.File.Exists(pAppCfgFilePath) = False Then
                If System.IO.Directory.Exists(pTestSystemPath) = False Then System.IO.Directory.CreateDirectory(pTestSystemPath)
                FileCopy(Application.StartupPath & "\" & pAppCfgFileName, pAppCfgFilePath)
            End If

            If System.IO.File.Exists(pSAPFailSafeModeCfgFile) = False Then
                FileCopy(Application.StartupPath & "\" & pSAPFailSafeModeCfgFileName, pSAPFailSafeModeCfgFile)
            End If

            '实例化LocalConfig并赋值给pAppCfg，将CATSPimTestConfig文件的内容读取到pAppCfg的私有m_dataset内
            '包括TestMode、Instruments、RET、Vibration等信息

            pAppCfg = New CATSPimConfig.LocalConfig(pAppCfgFilePath)
            'pTestResultPath = c:\cats\test_result\
            If System.IO.Directory.Exists(pTestResultPath) = False Then System.IO.Directory.CreateDirectory(pTestResultPath)
            '复制CATS.xmls到测试系统目录c:\cats\test_system(如果不存在)

            'CopyCATSConfigure()
            Dim dbConnFile As String = "CATS_Cable.xmls"
            If My.Computer.FileSystem.FileExists(String.Format("{0}\{1}", pTestSystemPath, dbConnFile)) Then
                pCatsCfgFile = pTestSystemPath & dbConnFile
            End If

            Dim sysConfig As CATSConfig = GetCATSConfigure(pCatsCfgFile)
            '获取连接字符串
            pDbConnString = sysConfig.DataBase.ConnString
            '获取工厂名
            pFactory = sysConfig.Factory.Location

            Select Case pFactory
                Case "ASZ"
                    pPlantCode = "CN10"
                Case "Reynosa"
                    pPlantCode = "USR1"
                Case "GOA"
                    pPlantCode = "IN13"
            End Select

            GUI.Plant = pFactory

            Dim serverName As String = String.Empty
            Dim database As String = String.Empty
            Dim reg As Regex = New Regex("Data Source=(\S+)")
            Dim match As Match = reg.Match(pDbConnString.Split(";")(0))
            If match.Success Then serverName = match.Groups(1).Value.ToUpper
            reg = New Regex("Initial Catalog=(\S+)")
            match = reg.Match(pDbConnString.Split(";")(1))
            If match.Success Then database = match.Groups(1).Value.ToUpper
            GUI.DB = String.Format("{0}_{1}", serverName, database)

            Dim CatsConn As New CATS.BLL.CATSManager
            '连接服务器
            CatsConn.ActivateCATS(sysConfig.DataBase.ConnString)

        Catch ex As Exception
            Throw New Exception("InitializeConfigure()::" & ex.Message)
        End Try

    End Sub
    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

#If INSTR_NORMAL_TEST = 0 Then
        MsgBox("The program is running in debug and virtual test mode , can not apply to testing.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
#End If
        Try

            Me.ShowInTaskbar = False
            Me.Visible = False

            'SetFormSkin()

            'Dim domainName As String = Domain.GetComputerDomain.Name.ToLower.Trim

            'If domainName.Contains("commscope.com") = False Then
            '    MsgBox("The test system is not in correct network!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            '    End
            'End If

            Try
                InitializeConfigure()

            Catch ex As Exception
                MsgBox("FormLoad()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                End
            End Try

            '实例化Run Time Parameter
            pRTP = New DataModels.RTimePara
            '初始化用户界面(Graphical User Interface)，转到TestPlan的InitializeGUI()方法
            pTestPlan.InitializeGUI()

        Catch ex As Exception
            MsgBox("CATSPim.StartUp()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
            'End
        End Try

    End Sub

    Private Sub SetFormSkin()
        Try
            Dim skinFile As String = "MSN.ssk"
            Dim skin As New SkinEngine With
{
                .SkinFile = Application.StartupPath & "\" & skinFile,
                .Active = True
            }
        Catch ex As Exception
            Throw New Exception("FormStartup.SetFormSkin()::" & ex.Message)
        End Try
    End Sub
End Class