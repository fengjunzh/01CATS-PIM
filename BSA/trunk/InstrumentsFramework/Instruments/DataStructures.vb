'Imports System.Xml.Serialization
'Imports System.IO
'Imports System.Collections.Generic
'Imports System.Security.Cryptography
'Imports System.Text
'Imports System.Windows.Forms
'Public Class Statistics
'  Public PeakToPeak As Double
'  Public Average As Double
'  Public Stdev As Double
'  Sub New()
'    PeakToPeak = 0
'    Average = 0
'    Stdev = 0
'  End Sub
'End Class

'Public Class FrequencyPoint
'  Public Value As TracePoint
'  Public Frequency As Double
'  Public FormatAmplitude As INetworkAnalyzer.NAMeasFormat
'  Public Sub New()
'    Value = New TracePoint
'    Value.Real = 0
'    Value.Imag = 0
'    Frequency = 0
'    FormatAmplitude = INetworkAnalyzer.NAMeasFormat.NALOGM
'  End Sub
'End Class

'Public Class Trace
'  Public Name As String
'  Public NumPoints As Integer
'  Public StartFreq As Double
'  Public StopFreq As Double
'  Public PointsEquallySpaced As Boolean
'  <XmlIgnore()> Public DataFormat As INetworkAnalyzer.NAMeasFormat
'  <XmlIgnore()> Public ListofTracePoints As New List(Of TracePoint)

'  Public Property TracePoints() As TracePoint()
'    Get
'      Dim tmpPointsArray(ListofTracePoints.Count - 1) As TracePoint
'      'THESE CREATE NEW INSTANCES FOR EACH ELEMENT OF THE ARRAY!
'      ListofTracePoints.CopyTo(tmpPointsArray)
'      Return tmpPointsArray
'    End Get
'    Set(ByVal value As TracePoint())
'      ListofTracePoints.Clear()
'      If value IsNot Nothing Then
'        For Each tmpItem As TracePoint In value
'          ListofTracePoints.Add(tmpItem)
'        Next
'      End If
'    End Set
'  End Property
'  Public Sub New()
'    DataFormat = INetworkAnalyzer.NAMeasFormat.NALOGM
'    StartFreq = 0
'    StopFreq = 0
'    NumPoints = 0
'    PointsEquallySpaced = False
'  End Sub
'  Public Property DataType() As String
'    Get
'      Dim tmpstring As String
'      tmpstring = ""
'      Select Case DataFormat
'        Case INetworkAnalyzer.NAMeasFormat.NALOGM
'          tmpstring = "LogMag"
'        Case INetworkAnalyzer.NAMeasFormat.NADELAY
'          tmpstring = "Delay"
'        Case INetworkAnalyzer.NAMeasFormat.NAPHASE
'          tmpstring = "Phase"
'        Case INetworkAnalyzer.NAMeasFormat.NAPOLAR
'          tmpstring = "Complex"
'      End Select
'      Return tmpstring
'    End Get
'    Set(ByVal value As String)
'      Select Case value
'        Case "LogMag"
'          DataFormat = INetworkAnalyzer.NAMeasFormat.NALOGM
'        Case "Delay"
'          DataFormat = INetworkAnalyzer.NAMeasFormat.NADELAY
'        Case "Phase"
'          DataFormat = INetworkAnalyzer.NAMeasFormat.NAPHASE
'        Case "Complex"
'          DataFormat = INetworkAnalyzer.NAMeasFormat.NAPOLAR
'      End Select
'    End Set
'  End Property
'  Public Sub SaveTraceToFile(ByVal strFileName As String)
'    'write to txt or xml file
'    Try
'      If strFileName.Contains(".txt") = True Then
'        Dim writer As StreamWriter
'        writer = File.CreateText(strFileName)
'        'text file
'        'Setup header
'        With Me
'          writer.WriteLine("Name: " & .Name)
'          writer.WriteLine("Start frequency: " & .StartFreq)
'          writer.WriteLine("Stop frequency: " & .StopFreq)
'          writer.WriteLine("NumPoints: " & .NumPoints)
'          Select Case .DataFormat
'            Case INetworkAnalyzer.NAMeasFormat.NALOGM
'              writer.WriteLine("Data Format: LogMag")
'              writer.WriteLine("Data:")
'              For Each item As TracePoint In .TracePoints
'                writer.WriteLine(item.FreqMHz & vbTab & item.Amplitude)
'              Next
'            Case INetworkAnalyzer.NAMeasFormat.NADELAY
'              writer.WriteLine("Data Format: Delay")
'              writer.WriteLine("Data:")
'              For Each item As TracePoint In .TracePoints
'                writer.WriteLine(item.FreqMHz & vbTab & item.Real)
'              Next
'            Case INetworkAnalyzer.NAMeasFormat.NAPHASE
'              writer.WriteLine("Data Format: Phase")
'              writer.WriteLine("Data:")
'              For Each item As TracePoint In .TracePoints
'                writer.WriteLine(item.FreqMHz & vbTab & item.Phase)
'              Next
'            Case INetworkAnalyzer.NAMeasFormat.NAPOLAR
'              writer.WriteLine("Data Format: Complex")
'              writer.WriteLine("Data:")
'              For Each item As TracePoint In .TracePoints
'                writer.WriteLine(item.FreqMHz & vbTab & item.Real & vbTab & item.Imag)
'              Next
'          End Select
'        End With
'        writer.Close()
'      ElseIf strFileName.Contains(".xml") = True Then
'        'xml
'        Dim writer As FileStream
'        Dim Serializer As System.Xml.Serialization.XmlSerializer
'        Dim tempFileName As String
'        tempFileName = strFileName & ".tmp"
'        Dim tempFileInfo As New FileInfo(tempFileName)
'        If tempFileInfo.Exists = True Then tempFileInfo.Delete()
'        writer = New FileStream(tempFileName, FileMode.Create)
'        Serializer = New XmlSerializer(Me.GetType)
'        Serializer.Serialize(writer, Me)
'        writer.Close()
'        tempFileInfo.CopyTo(strFileName, True)
'        tempFileInfo.Delete()
'      Else
'        Throw New Exception(String.Format("File name '{0}' is an invalid name to save trace data (valid extensions are.txt or .xml)", strFileName))
'      End If
'    Catch ex As Exception
'      Throw New Exception(ex.Message)
'    End Try
'  End Sub
'  Public Function GetTracePointAmplitude(ByVal Freq As Double) As Double

'    Try
'      If Freq < StartFreq Or Freq > StopFreq Then
'        Throw New Exception("Frequency " & Freq & " MHz" & " in path " & _
'        Me.Name & " not present")
'      Else
'        If PointsEquallySpaced Then
'          Return ListofTracePoints.Item(CType((Freq - StartFreq) / CType((StopFreq - StartFreq) / (NumPoints - 1), Double), Integer)).Amplitude
'        Else
'          Dim MaxPosition As Double = NumPoints - 1
'          Dim MinPosition As Double = 0
'          Dim Interval As Double
'          Dim FreqIndex_Inf As Integer, FreqIndex_Sup As Integer
'          Do
'            Interval = (MaxPosition - MinPosition) / 2
'            FreqIndex_Inf = CInt(Math.Floor(Interval + MinPosition))
'            FreqIndex_Sup = CInt(Math.Ceiling(Interval + MinPosition))
'            If FreqIndex_Sup > NumPoints - 1 Then
'              FreqIndex_Sup = NumPoints - 1
'            End If
'            If Freq >= ListofTracePoints.Item(FreqIndex_Inf).FreqMHz And _
'            Freq <= ListofTracePoints.Item(FreqIndex_Sup).FreqMHz Then
'              If Freq = ListofTracePoints.Item(FreqIndex_Inf).FreqMHz Then
'                Return ListofTracePoints.Item(FreqIndex_Inf).Amplitude
'              ElseIf Freq = ListofTracePoints.Item(FreqIndex_Sup).FreqMHz Then
'                Return ListofTracePoints.Item(FreqIndex_Sup).Amplitude
'              Else
'                Return ListofTracePoints.Item(FreqIndex_Inf).Amplitude + _
'                (Freq - ListofTracePoints.Item(FreqIndex_Inf).FreqMHz) * _
'                (ListofTracePoints.Item(FreqIndex_Sup).Amplitude - ListofTracePoints.Item(FreqIndex_Inf).Amplitude) / (ListofTracePoints.Item(FreqIndex_Sup).FreqMHz - ListofTracePoints.Item(FreqIndex_Inf).FreqMHz)
'              End If
'            ElseIf Freq < ListofTracePoints.Item(FreqIndex_Inf).FreqMHz Then
'              MaxPosition = FreqIndex_Inf
'            ElseIf Freq > ListofTracePoints.Item(FreqIndex_Sup).FreqMHz Then
'              MinPosition = FreqIndex_Sup
'            End If
'          Loop While Interval >= 1
'          Return Nothing
'        End If
'      End If
'    Catch ex As Exception
'      Throw New Exception(ex.Message)
'    End Try
'  End Function

'  Public Sub GetStats(ByVal FreqStart As Double, ByVal FreqStop As Double, ByRef Ymean As Double, ByRef Ymax As Double, ByRef Ymin As Double)
'    Dim FirstTime As Boolean = True
'    Dim ysum As Double = 0
'    Dim count As Integer = 0

'    For Each pt As TracePoint In ListofTracePoints
'      If pt.FreqMHz >= FreqStart And pt.FreqMHz <= FreqStop Then
'        Dim tmpVal As Double
'        Select Case Me.DataType
'          Case "LogMag"
'            tmpVal = pt.Amplitude
'          Case "Phase"
'            tmpVal = pt.Phase
'          Case Else
'            tmpVal = 0
'        End Select

'        If FirstTime Then
'          FirstTime = False
'          ysum = tmpVal
'          Ymin = tmpVal
'          Ymax = tmpVal
'        Else
'          ysum += tmpVal
'          If tmpVal > Ymax Then Ymax = tmpVal
'          If tmpVal < Ymin Then Ymin = tmpVal
'        End If

'        count += 1
'      End If
'    Next
'    If FirstTime = False Then
'      Ymean = ysum / count
'    End If
'  End Sub

'End Class

'Public Class TracePoint
'  Public Real As Double
'  Public Imag As Double
'  Public Amplitude As Double
'  Public Phase As Double
'  Public FreqMHz As Double
'  Sub New()
'    Real = 0
'    Imag = 0
'    Amplitude = 0
'    Phase = 0
'    FreqMHz = 0
'  End Sub
'End Class

'Public Class CalibrationData
'  Public CalDateFixture As Date
'  Public CalDatePowerSensorB As Date
'  Public CalDatePowerSensorA As Date
'  <XmlIgnore()> Private pTraceList As New Dictionary(Of String, Trace)
'  Public Sub AddPathData(ByVal TraceItem As Trace)
'    pTraceList.Add(TraceItem.Name, TraceItem)
'  End Sub
'  Public Sub ClearPathData()
'    Try
'      If pTraceList IsNot Nothing Then
'        If pTraceList.Count > 0 Then
'          pTraceList.Clear()
'        End If
'      End If
'    Catch ex As Exception
'      Throw New Exception(ex.Message)
'    End Try
'  End Sub
'  Public Function GetPathData(ByVal PathName As String) As Trace
'    Return pTraceList.Item(PathName)
'  End Function
'  Public Property Paths() As Trace()
'    Get
'      Try
'        Dim tmpTraceArray(pTraceList.Count - 1) As Trace
'        Dim tmpTrace As Trace
'        Dim i As Integer = 0
'        If pTraceList.Count > 0 Then
'          For Each item As KeyValuePair(Of String, Trace) In pTraceList
'            'It is necessary to create a new instance of Trace
'            'for each item
'            tmpTrace = New Trace
'            With tmpTrace
'              .DataType = item.Value.DataType
'              .Name = item.Value.Name
'              .NumPoints = item.Value.NumPoints
'              .StartFreq = item.Value.StartFreq
'              .StopFreq = item.Value.StopFreq
'              .PointsEquallySpaced = Convert.ToBoolean(item.Value.PointsEquallySpaced)
'              .TracePoints = item.Value.TracePoints
'            End With
'            tmpTraceArray(i) = tmpTrace
'            i += 1
'          Next
'        End If
'        'pTraceList.CopyTo(tmpTraceArray)
'        Return tmpTraceArray
'      Catch ex As Exception
'        Throw New Exception(ex.Message)
'      End Try
'    End Get
'    Set(ByVal value As Trace())
'      pTraceList.Clear()
'      If value IsNot Nothing Then
'        For Each tmpItem As Trace In value
'          pTraceList.Add(tmpItem.Name, tmpItem)
'        Next
'      End If
'    End Set
'  End Property

'  Public Sub SaveToXML(ByVal FileName As String)
'    Try
'      'xml storaging
'      Dim writer As FileStream
'      Dim Serializer As System.Xml.Serialization.XmlSerializer
'      Dim tempFileName As String
'      Dim CalDataDeltaCheckFailedFlagFile As String = My.Application.Info.DirectoryPath & "\CalCheckFailed"

'      tempFileName = FileName & ".tmp"
'      Dim tempFileInfo As New FileInfo(tempFileName)
'      If tempFileInfo.Exists Then tempFileInfo.Delete()
'      writer = New FileStream(tempFileName, FileMode.Create)
'      Serializer = New XmlSerializer(Me.GetType)
'      Serializer.Serialize(writer, Me)
'      writer.Close()
'      tempFileInfo.CopyTo(FileName, True)
'      tempFileInfo.Delete()

'      Dim InfoFile As System.IO.FileInfo
'      InfoFile = New System.IO.FileInfo(CalDataDeltaCheckFailedFlagFile)
'      If Not InfoFile.Exists Then
'        Dim CalFileInfo As New FileInfo(FileName)
'        Dim path As String = CalFileInfo.Directory.FullName
'        Dim md5file As String = path & "\Calibration.md5"
'        SaveCalMD5File(FileName, md5file)
'      Else
'        MessageBox.Show("Calibration Delta Check failed, need to re-do calibration!" & vbNewLine & _
'                        "md5 file cannot be saved this time.", "Warning", MessageBoxButtons.OK)
'      End If


'    Catch ex As Exception
'      Throw New Exception(ex.Message)
'    End Try
'  End Sub

'  Private Sub SaveCalMD5File(ByRef fname As String, ByVal md5file As String)
'    Try
'      Dim CalFileReader As New System.IO.StreamReader(fname, System.Text.Encoding.UTF8)
'      Dim CalFileString As String = CalFileReader.ReadToEnd
'      CalFileReader.Close()
'      Dim hash As String = getMd5Hash(CalFileString)

'      Dim InfoFile As System.IO.FileInfo
'      InfoFile = New System.IO.FileInfo(md5file)
'      If InfoFile.Exists Then InfoFile.Delete()
'      Dim myStream As New System.IO.FileStream(md5file, FileMode.Create)
'      myStream.Close()
'      Dim myWriter As System.IO.StreamWriter = New System.IO.StreamWriter(md5file, False, System.Text.Encoding.UTF8)
'      myWriter.Write(hash)
'      myWriter.Flush()
'      myWriter.Close()

'    Catch ex As Exception
'      Throw New Exception(ex.Message)
'    End Try
'  End Sub
'  Function getMd5Hash(ByVal input As String) As String

'    Dim md5Hasher As New MD5CryptoServiceProvider()
'    Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))
'    Dim sBuilder As New StringBuilder()

'    Dim i As Integer
'    For i = 0 To data.Length - 1
'      sBuilder.Append(data(i).ToString("x2"))
'    Next i

'    Return sBuilder.ToString()
'  End Function
'End Class

