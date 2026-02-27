Imports System.Windows.Forms.DataVisualization.Charting
Imports System.DirectoryServices.ActiveDirectory
Imports System.Xml.Serialization
Imports System.Text
Public Class FormMain
	Private Function GetCATSConfigure(path As String) As CATSConfig
		Try
			Dim resp As New CATSConfig

			Dim tmpPath As String = New IO.FileInfo(path).DirectoryName & "\tmp.tmp"

			Dim ept As New Encryptor
			ept.DecryptFile(path, tmpPath)

			'ept.EncryptFile("C:\CATS\test_system\CATS.xml", "C:\CATS\test_system\CATS.xm")

			Dim xs As New XmlSerializer(GetType(CATSConfig))
			Dim sr As New System.IO.StreamReader(tmpPath)

			Try
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
	Private Sub CopyCATSConfigure()
		Try
			Dim svrPath As String = Application.StartupPath & "\CATS.xmls"
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
						If DateTime.Compare(CDate(svrCfg.CatsProperty.CreateTime), CDate(sysCfg.CatsProperty.CreateTime)) > 0 Then
							IO.File.Copy(svrPath, pCatsCfgFile, True)
						End If
					End If

				End If
			End If
		Catch ex As Exception
			Throw New Exception("CopyCATSConfigure()::" & ex.Message)
		End Try
	End Sub

	Private Sub InitializeConfigure()
		Try

			If System.IO.File.Exists(pAppCfgFilePath) = False Then
				If System.IO.Directory.Exists(pTestSystemPath) = False Then System.IO.Directory.CreateDirectory(pTestSystemPath)
				FileCopy(Application.StartupPath & "\" & pAppCfgFileName, pAppCfgFilePath)
			End If

			pAppCfg = New CATSPimConfig.LocalConfig(pAppCfgFilePath)

			CopyCATSConfigure()

			Dim sysConfig As CATSConfig = GetCATSConfigure(pCatsCfgFile)
			pDbConnString = sysConfig.DataBase.ConnString
			pFactory = sysConfig.Factory.Location

			Dim CatsConn As New CATS.BLL.CATSManager
			CatsConn.ActivateCATS(sysConfig.DataBase.ConnString)

		Catch ex As Exception
			Throw New Exception("InitializeConfigure()::" & ex.Message)
		End Try

	End Sub
	Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load


		'Dim x As New Encryptor

		'x.DecryptFile("c:\atsc\IMDSN001!20161114195350!PROD!HBX6516DS!PIM1800.dat", "c:\atsc\IMDSN001!20161114195350!PROD!HBX6516DS!PIM1800.xml")

#If INSTR_NORMAL_TEST = 0 Then
		MsgBox("The program is running in debug mode , can not apply to testing.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
		End
#End If

		Try

			Me.ShowInTaskbar = False
			Me.Visible = False

			Dim domainName As String = Domain.GetComputerDomain.Name.ToLower.Trim

			If domainName.Contains("commscope.com") = False Then
				MsgBox("The test system is not in correct network!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
				End
			End If



			Try
				InitializeConfigure()

				'Dim o As Object = (New CATS.BLL.phase_mainManager).SelectById(1)

				'Debug.Print(o.ToString)


			Catch ex As Exception
				MsgBox("FormLoad()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
				End
			End Try


			pRTP = New DataModels.RTimePara

			pTestPlan.InitializeGUI()

		Catch ex As Exception
			MsgBox("CATSPim.StartUp()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
			'End
		End Try

	End Sub
End Class