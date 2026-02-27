Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Public Class Encryptor

  Private ReadOnly _key As Byte() = {96, 48, 19, 101, 176, 59, 127, 215}
  Private ReadOnly _desProvider As DESCryptoServiceProvider

  Public Sub New()

    _desProvider = New DESCryptoServiceProvider

    _desProvider.Key = _key
    _desProvider.IV = _key


  End Sub
  ''' <summary>
  ''' Encrypt input contents And write to file
  ''' </summary>
  ''' <param name="inputContents">Contents which Is needed to encrypt</param>
  ''' <param name="outputFilePath">Full path. Suggestion name Is *.dat</param>
  Public Sub EncryptStringToFile(inputContents As String, outputFilePath As String)

    Try
      Dim fileStream = New FileStream(outputFilePath, FileMode.Create, FileAccess.Write)

      Dim descencrypt = _desProvider.CreateEncryptor()
      Dim cryptostream = New CryptoStream(fileStream, descencrypt, CryptoStreamMode.Write)

      Dim Input() = Encoding.UTF8.GetBytes(inputContents)

      cryptostream.Write(Input, 0, Input.Length)

      cryptostream.Close()

      fileStream.Close()


    Catch ex As Exception
      Throw New Exception("Encryptor.EncryptToFile()::" & ex.Message)
    End Try


  End Sub
  Public Sub EncryptFile(inputFilepath As String, outputFilepath As String)
    Try
      Dim objSR As New StreamReader(inputFilepath, FileMode.Open)
      Dim strFile As String

      strFile = objSR.ReadToEnd()
      objSR.Close()

      EncryptStringToFile(strFile, outputFilepath)

    Catch ex As Exception
      Throw New Exception("Encryptor.EncryptFile()::" & ex.Message)
    End Try
  End Sub
  ''' <summary>
  ''' Decrypt from file
  ''' </summary>
  ''' <param name="filePath">File path which Is intended to decrypt</param>
  ''' <returns>Result string</returns>
  Public Function DecryptFromFile(filePath As String) As String
    Try

      Dim fileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)

      Dim descencrypt = _desProvider.CreateDecryptor()
      Dim cryptostream = New CryptoStream(fileStream, descencrypt, CryptoStreamMode.Read)

      Dim reader = New StreamReader(cryptostream)
      Dim result = reader.ReadToEnd

      reader.Close()
      fileStream.Close()


      Return result

    Catch ex As Exception
      Throw New Exception("Encryptor.DecryptFromFile()::" & ex.Message)
    End Try

  End Function
  Public Function DecryptFile(srcFilepath As String, desFilepath As String) As Boolean
    Try

      Dim result = DecryptFromFile(srcFilepath)
      Dim fw = New StreamWriter(desFilepath)

      fw.Write(result)
      fw.Close()


      Return True

    Catch ex As Exception
      Throw New Exception("Encryptor.DecryptFile()::" & ex.Message)
    End Try

  End Function

End Class
