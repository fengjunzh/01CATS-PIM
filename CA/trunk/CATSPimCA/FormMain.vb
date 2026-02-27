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
    Private Sub CopySapFailSafeModeFile()
        Try
            Dim svrPath As String = Application.StartupPath & "\SAPFailSafeMode.xml"
            'sysPath = c:\cats\test_system\SAPFailSafeMode.xmls
            'pSAPFailSafeModeCfgFile = c:\cats\test_system\CATS.xmls
            Dim sysPath As String = pSAPFailSafeModeCfgFile

            If IO.File.Exists(svrPath) = False Then Return

            If IO.File.Exists(sysPath) = False Then
                IO.File.Copy(svrPath, sysPath)
            Else
                If IO.File.GetLastWriteTime(svrPath) > IO.File.GetLastWriteTime(sysPath) Then
                    IO.File.Copy(svrPath, sysPath, True)
                End If
            End If
        Catch ex As Exception
            Throw New Exception("CopySapFailSafeModeFile()::" & ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 读取配置文件CATSPimTestConfig.xml和CATS.xmls
    ''' </summary>
    Private Sub InitializeConfigure()
        Try
            If System.IO.Directory.Exists(pTestSystemPath) = False Then
                System.IO.Directory.CreateDirectory(pTestSystemPath)
            End If

            If System.IO.File.Exists(pSAPFailSafeModeCfgFile) = False Then
                FileCopy(Application.StartupPath & "\" & pSAPFailSafeModeCfgFileName, pSAPFailSafeModeCfgFile)
            End If

            '复制CATS.xmls到测试系统目录c:\cats\test_system(如果不存在)
            'CopyCATSConfigure()
            If My.Computer.FileSystem.FileExists(pCatsCfgFile) = False Then
                Dim connFile = "CATS.xmls"
                pCatsCfgFile = pTestSystemPath & connFile
                If My.Computer.FileSystem.FileExists(pCatsCfgFile) = False Then
                    Throw New Exception(String.Format("The file '{0}' not found!", connFile))
                End If
            End If

            Dim sysConfig As CATSConfig = GetCATSConfigure(pCatsCfgFile)
            '获取连接字符串
            pDbConnString = sysConfig.DataBase.ConnString
            '获取工厂名
            pFactory = sysConfig.Factory.Location
            Dim CatsConn As New CATS.BLL.CATSManager
            '连接服务器
            CatsConn.ActivateCATS(sysConfig.DataBase.ConnString)

            Dim serverName As String = String.Empty
            Dim database As String = String.Empty
            Dim reg As Regex = New Regex("Data Source=(\S+)")
            Dim match As Match = reg.Match(sysConfig.DataBase.ConnString.Split(";")(0))
            If match.Success Then serverName = match.Groups(1).Value.ToUpper
            reg = New Regex("Initial Catalog=(\S+)")
            match = reg.Match(sysConfig.DataBase.ConnString.Split(";")(1))
            If match.Success Then database = match.Groups(1).Value.ToUpper
            GUI.Database = String.Format("{0}_{1}", serverName, database)

            pPlantCode = GetFactoryCode(pFactory)

            gPimTestConfig = CPimTestConfig.CreateInstance(gPimCfgFileName)
            If gPimTestConfig Is Nothing Then
                Dim frmPimConfig = New FormConfig(gPimCfgFileName, gPimTestConfig)
                frmPimConfig.ShowDialog()
            End If

            gPimTestConfig = CPimTestConfig.CreateInstance(gPimCfgFileName)
            If gPimTestConfig Is Nothing Then Throw New Exception("Please setup device!")
            With gPimTestConfig.Vibration
                .Enable = False
                .Address = VibCtrl.VibCtrlBoard.SEADACLITE_8112.ToString
            End With
            gSelectedInstrumentList = gPimTestConfig.InstrumentList.Where(Function(o) o.Enable = True).ToList
            If gSelectedInstrumentList Is Nothing Then Throw New Exception("Please pick an active PIM anaylzer!")
            'pTestResultPath = c:\cats\test_result\
            If System.IO.Directory.Exists(pTestResultPath) = False Then System.IO.Directory.CreateDirectory(pTestResultPath)

            CopySapFailSafeModeFile()

            gSAPFailSafeMode = SAPFailSafeMode.CreateInstance(gSAPFailSafeModeFile)
            If gSAPFailSafeMode Is Nothing Then
                Dim frmSAPFailSafeMode As New FormSAPFailSafeMode()
                frmSAPFailSafeMode.ShowDialog()
            End If

            gSAPFailSafeMode = SAPFailSafeMode.CreateInstance(gSAPFailSafeModeFile)
            If gSAPFailSafeMode Is Nothing Then
                GUI.SAPFailSafeModeOnOff = False
            Else
                GUI.SAPFailSafeModeOnOff = gSAPFailSafeMode.SAPFailSafeModeOnOff
            End If

#If INSTR_NORMAL_TEST Then
            GUI.InstrVirtualMode = "Normal"
#Else
            GUI.InstrVirtualMode = "Virtual"
#End If

        Catch ex As Exception
            Throw New Exception("InitializeConfigure()::" & ex.Message)
        End Try

    End Sub
    Private Function GetFactoryCode(ByVal factory As String) As String
        Try
            Dim FactoryM As CATS.Model.factory
            Dim FactoryBll As New CATS.BLL.factoryManager
            FactoryM = FactoryBll.SelectByFactory(pFactory)

            If factory Is Nothing Then Return "Unknown"
            Return FactoryM.code
        Catch ex As Exception
            Throw New Exception("GetFactoryCode() - " & ex.Message)
        End Try
    End Function
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

    Private Sub FormMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If gOpcUaClient IsNot Nothing Then
            gOpcUaClient.Dispose()
        End If
        Dim frm As System.Windows.Forms.Form
        For Each frm In My.Application.OpenForms
            frm.Close()
        Next frm
    End Sub
End Class