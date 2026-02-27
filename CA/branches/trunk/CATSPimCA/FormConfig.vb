Imports System.IO.Ports
Imports System.Threading.Tasks
Imports System.Xml.Serialization
Imports C1.Win.C1FlexGrid
Public Class FormConfig
    Private SettingFileName As String
    Public Sub New(ByVal fileName As String, Optional ByVal PimTestConfig As CPimTestConfig = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SettingFileName = fileName
    End Sub
    Private Sub InitGridInstruments()
        Try
            'set up grid
            fgPimDevices.Clear()
            fgPimDevices.Cols.Fixed = 1
            fgPimDevices.Cols.Count = 13
            fgPimDevices.Rows.Fixed = 1
            fgPimDevices.Rows.Count = 1
            fgPimDevices.Cols(1).Caption = "Enable"
            fgPimDevices.Cols(2).Caption = "Freq Band"
            fgPimDevices.Cols(3).Caption = "Model"
            fgPimDevices.Cols(4).Caption = "Vendor"
            fgPimDevices.Cols(5).Caption = "Address"
            fgPimDevices.Cols(6).Caption = "Band Index"
            fgPimDevices.Cols(7).Caption = "TxL Loss"
            fgPimDevices.Cols(8).Caption = "TxR Loss"
            fgPimDevices.Cols(9).Caption = "Date Time"
            fgPimDevices.Cols(10).Name = "Init"
            fgPimDevices.Cols(10).Caption = "Initialize"
            fgPimDevices.Cols(11).Caption = "Serial Number"
            fgPimDevices.Cols(12).Caption = "Idn"

            Dim cs As CellStyle = fgPimDevices.Styles.Add("Enable")
            cs.DataType = Type.GetType("System.Boolean")
            fgPimDevices.Cols(1).Style = cs

            cs = fgPimDevices.Styles.Add("Button")
            cs.BackColor = SystemColors.Control
            cs.Font = New Font("Arial", 11, FontStyle.Underline Or FontStyle.Bold)
            fgPimDevices.Cols(10).Style = fgPimDevices.Styles("Button")

            For i As Integer = 7 To 12
                fgPimDevices.Cols(i).AllowEditing = False
            Next
            fgPimDevices.Cols(11).AllowEditing = True

            fgPimDevices.Cols(9).Format = "yyyy-MM-dd HH:mm:ss"

            Dim fband As New CATS.BLL.cfg_imd_freq_bandManager
            Dim fbandML As List(Of CATS.Model.cfg_imd_freq_band)
            fbandML = fband.SelectAll()
            Dim fbandMLlStr As String = String.Join("|"， fbandML)
            fgPimDevices.Cols(2).ComboList = fbandMLlStr

            Dim pmBll As New CATS.BLL.phase_mainManager
            Dim pmML As List(Of CATS.Model.phase_main)
            pmML = pmBll.SelectAll()
            Dim query = From pm In pmML
                        Where pm.phase.Contains("PIM")
            Dim pmMLStr As String = String.Join("|"， query.ToList)

            fgPimDevices.Cols(3).ComboList = pmMLStr

            Dim ivBll As New CATS.BLL.instr_vendorManager
            Dim ivmL As List(Of CATS.Model.instr_vendor)
            ivmL = ivBll.SelectAll
            Dim ivmLStr As String = String.Join("|"， ivmL)
            fgPimDevices.Cols(4).ComboList = ivmLStr

            Dim AddressStr As String = ""
            For Each com As String In System.IO.Ports.SerialPort.GetPortNames
                AddressStr += "|" & com
            Next
            fgPimDevices.Cols(5).ComboList = AddressStr

            FormatGrid(fgPimDevices, 9, 9)

            fgPimDevices.Cols(2).Width = 100
            fgPimDevices.Cols(3).Width = 100
            fgPimDevices.Cols(4).Width = 100
            fgPimDevices.Cols(5).Width = 120
        Catch ex As Exception
            Throw New Exception("InitGridInstruments()::" & vbCrLf & " at " & ex.Message)
        End Try
    End Sub
    Private Sub fgPimDevices_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles fgPimDevices.OwnerDrawCell
        Try
            If e.Row >= fgPimDevices.Rows.Fixed And e.Col = fgPimDevices.Cols.Fixed - 1 Then
                Dim rowNumber As Integer = e.Row - fgPimDevices.Rows.Fixed + 1
                e.Text = rowNumber.ToString()
            End If
        Catch ex As Exception
            MsgBox("fgPimDevices_OwnerDrawCell()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
    End Sub
    Private Sub LoadVibrationAdress()
        Try
            cbVibAddress.Items.Clear()

            For Each t In System.Enum.GetValues(GetType(VibCtrl.VibCtrlBoard))
                cbVibAddress.Items.Add(t.ToString)
            Next

        Catch ex As Exception
            Throw New Exception("PimConfig.LoadVibrationAdress()::" & ex.Message)
        End Try
    End Sub
    Private Function GetPimDeviceList() As List(Of CInstrument)
        Try
            Dim resp As New List(Of CInstrument)
            Dim item As CInstrument
            For Each row As Row In fgPimDevices.Rows
                item = New CInstrument
                item.Enable = CBool(row(1))
                item.FreqBand = row(2)
                item.Model = row(3)
                item.Vendor = row(4)
                item.Address = row(5)
                item.BandIdx = row(6)
                resp.Add(item)
            Next

            Return resp

        Catch ex As Exception
            Throw New Exception("GetPimDeviceList() - " & ex.Message)
        End Try
    End Function
    Private Sub FormConfig_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ClosePimDevice()
            If pRTP.product_mode = "PROD" Then Me.ckVibEnable.Enabled = False Else Me.ckVibEnable.Enabled = True
            LoadVibrationAdress()
            InitGridInstruments()
            If gPimTestConfig Is Nothing Then Exit Sub
            Dim row As Row
            With gPimTestConfig
                '.Vibration.Enable = False
                Me.ckVibEnable.Checked = .Vibration.Enable
                Me.cbVibAddress.Text = .Vibration.Address
                Me.ckMiiOnLine.Checked = .MII.Enable
                Me.ckSnReader.Checked = .SnReader
                Me.ckAuto.Checked = .Automation
                Me.ckPretest.Checked = .Pretest
                For Each instr As CInstrument In .InstrumentList
                    row = Me.fgPimDevices.Rows.Add
                    row(1) = instr.Enable
                    row(2) = instr.FreqBand
                    row(3) = instr.Model
                    row(4) = instr.Vendor
                    row(5) = instr.Address
                    row(6) = instr.BandIdx
                    row(7) = instr.TxLLoss
                    row(8) = instr.TxRLoss
                    row(9) = instr.DateTime
                    row(10) = "Not OK"
                    row(11) = instr.SerialNumber
                    row(12) = instr.Idn
                Next
            End With
            FormatGrid(fgPimDevices, 9, 9)
        Catch ex As Exception
            Throw New Exception("FormConfig_Load()::" & ex.Message)
        End Try
    End Sub

    Private Async Sub btnSaveExit_Click(sender As Object, e As EventArgs) Handles btnSaveExit.Click
        Try
            If gPimTestConfig IsNot Nothing Then gPimTestConfig = Nothing
            gPimTestConfig = New CPimTestConfig
            gSelectedInstrumentList = New List(Of CInstrument)
            With gPimTestConfig
                .Vibration.Enable = Me.ckVibEnable.Checked
                .Vibration.Address = Me.cbVibAddress.Text
                .MII.Enable = ckMiiOnLine.Checked
                .SnReader = ckSnReader.Checked
                .Automation = ckAuto.Checked
                .Pretest = ckPretest.Checked
                Dim instrument As CInstrument
                For Each row As Row In Me.fgPimDevices.Rows
                    If row.Index = 0 Then Continue For
                    If row(2) Is Nothing Or row(3) Is Nothing Then Continue For
                    instrument = New CInstrument
                    With instrument
                        .Enable = row(1)
                        .FreqBand = row(2)
                        .Model = row(3)
                        .Vendor = row(4)
                        .Address = row(5)
                        .BandIdx = row(6)
                        .TxLLoss = row(7)
                        .TxRLoss = row(8)
                        .DateTime = row(9)
                        If .Enable = True And String.IsNullOrEmpty(row(11)) Then
                            MsgBox("Please init the PIM analzyer to get SN automatically or manually input the SN", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                            fgPimDevices.Row = row.Index
                            fgPimDevices.Col = 11
                            Return
                        End If
                        .SerialNumber = row(11)
                        .Idn = row(12)
                    End With
                    .InstrumentList.Add(instrument)
                    If instrument.Enable = True Then gSelectedInstrumentList.Add(instrument)
                Next
                If gSelectedInstrumentList.Count = 0 Then
                    MsgBox("Please pick at least one PIM device", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PIM Config")
                    Return
                End If
                If gSelectedInstrumentList.Count > 1 Then
                    If Not ((gSelectedInstrumentList(0).Model.Contains("PIM700L") OrElse gSelectedInstrumentList(0).Model.Contains("PIM700U")) AndAlso
                        (gSelectedInstrumentList(1).Model.Contains("PIM700L") OrElse gSelectedInstrumentList(1).Model.Contains("PIM700U"))) Then
                        MsgBox("Please just pick one PIM device except PIM700LU", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PIM Config")
                        Return
                    End If
                End If
                .Save(SettingFileName)
            End With

            With gPimTestConfig
                GUI.Pretest_PIM = .Pretest
                GUI.Automation = .Automation
                Close()
                If .Automation Then
                    Await ConnectOpcUaAsync()
                    GUI.ModeName = "PROD_ATUO"
                    pRTP.product_mode = "PROD_ATUO"
                ElseIf gOpcUaClient IsNot Nothing Then
                    Await gOpcUaClient.DisconnectAsync()
                    AddStatusMsg("PLC Disconnect Success!")
                    GUI.Status = "PLC Disconnected"
                    GUI.ModeName = "PROD"
                    pRTP.product_mode = "PROD"
                End If
            End With
        Catch ex As Exception
            MsgBox("btnSaveExit_Click()::" & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Save Config")
        End Try
    End Sub
    Private Async Function ConnectOpcUaAsync() As Task
        Try
            If gPimTestConfig.Automation = False Then Return
            ' 如果已有连接，先断开
            If gOpcUaClient IsNot Nothing Then
                Await gOpcUaClient.DisconnectAsync()
            End If

            gOpcUaClient = New OpcUaUtility.OpcUaClient(GUI, "192.168.20.32", "4840")

            Await gOpcUaClient.ConnectAsync()

            If gOpcUaClient.IsConnected Then
                AddStatusMsg("PLC Connect Success!")
                GUI.Status = "PLC Connected"
            Else
                AddStatusMsg("PLC Connect Fail!")
            End If
        Catch ex As Exception
            Throw New Exception("ConnectOpcUaAsync()::" & vbCrLf & ex.Message)
        End Try
    End Function
    Private Sub fgPimDevices_Click(sender As Object, e As EventArgs) Handles fgPimDevices.Click
        Try
            If fgPimDevices.Row < 1 Then Return
            Dim row As Row
            Dim col As Column
            col = fgPimDevices.Cols(fgPimDevices.Col)
            If col.Caption = "Initialize" Then
                row = fgPimDevices.Rows(fgPimDevices.Row)
                If row(1) = False Then Return
                If row(2) Is Nothing Or row(3) Is Nothing Or row(4) Is Nothing Or row(5) Is Nothing Or row(6) Is Nothing Then
                    MsgBox("Please fill in the Freq Band, Model, Vendor, Address and Band Index first", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "PIM Config")
                    Return
                End If
                Dim instr As New CInstrument
                With instr
                    .Enable = row(1)
                    .FreqBand = row(2)
                    .Model = row(3)
                    .Vendor = row(4)
                    .Address = row(5)
                    .BandIdx = row(6)
                End With
                Dim eqList As List(Of DataModels.Instrument) = InitPimDevice(instr)
                If eqList Is Nothing Then Throw New Exception("Init Fail")
                If eqList(0).Idn.Length > 0 Then row(10) = "OK" : row(12) = eqList(0).Idn
                If eqList(0).SerialNumber.Length = 0 Then MsgBox("Serial number reading is empty, please manually input instrument serial number") : fgPimDevices.Col = 11 Else row(11) = eqList(0).SerialNumber
                FormatGrid(fgPimDevices, 9, 9)
            End If
        Catch ex As Exception
            MsgBox("fgPimDevices_Click()::" & vbCrLf & " at " & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Init PIM Analyzer Error")
        Finally
            ClosePimDevice()
        End Try
    End Sub

    Private Sub fgPimDevices_BeforeAddRow(sender As Object, e As RowColEventArgs) Handles fgPimDevices.BeforeAddRow
        Dim r As Row
        r = fgPimDevices.Rows(e.Row)
        If r(0) = True Then r(7) = "Not OK"
    End Sub

    Private Sub btnPowerCal_Click(sender As Object, e As EventArgs) Handles btnPowerCal.Click
        Dim frmPowerCal As New FormPowerCal
        Dim pimSN As String = Nothing
        Dim selectedRowIdx As Integer
        For i As Integer = fgPimDevices.Rows.Fixed To fgPimDevices.Rows.Count - 1
            If fgPimDevices(i, 1) = False Then Continue For
            frmPowerCal.PimConfigStr += String.Format("Model: {0}", fgPimDevices(i, 3))
            frmPowerCal.PimBandName = fgPimDevices(i, 2)
            frmPowerCal.PimModel = fgPimDevices(i, 3)
            frmPowerCal.PimSN = fgPimDevices(i, 11)
            pimSN = fgPimDevices(i, 11)
            frmPowerCal.VendorName = fgPimDevices(i, 4)
            selectedRowIdx = i
            Exit For
        Next
        If pimSN Is Nothing Or pimSN.Length = 0 Then MsgBox("Please init the PIM device and config the SN firstly", MsgBoxStyle.Information) : fgPimDevices.Row = selectedRowIdx : fgPimDevices.Col = 11 : Return
        If frmPowerCal.ShowDialog = DialogResult.OK Then
            For i As Integer = fgPimDevices.Rows.Fixed To fgPimDevices.Rows.Count - 1
                If fgPimDevices(i, 1) = False Then Continue For
                fgPimDevices(i, 7) = 43 - frmPowerCal.TxLPower
                fgPimDevices(i, 8) = 43 - frmPowerCal.TxRPower
                fgPimDevices(i, 9) = frmPowerCal.EndCalTime
            Next
            FormatGrid(fgPimDevices, 9, 9)
        End If
    End Sub

    Private Sub ckPretest_CheckedChanged(sender As Object, e As EventArgs) Handles ckPretest.CheckedChanged
        If ckPretest.Checked Then ckPretest.Text = "Pretest" Else ckPretest.Text = "Finaltest"
    End Sub
End Class
Public Class CPimTestConfig
    Public TestMode As New CTestMode
    Public Vibration As New CVibration
    Public MII As New CMII
    Public SnReader As Boolean
    Public InstrumentList As New List(Of CInstrument)
    Public Automation As Boolean
    Public Pretest As Boolean

    Public Sub Save(ByVal filename As String)
        Try
            Dim XSerz As New XmlSerializer(Me.GetType)
            Dim StrmWt As New System.IO.StreamWriter(filename)
            XSerz.Serialize(StrmWt, Me)
            StrmWt.Close()
        Catch ex As Exception
            Throw New Exception("CPimTestConfig.Save() - " & ex.Message)
        End Try
    End Sub
    Public Shared Function CreateInstance(ByVal filename As String) As CPimTestConfig
        Try
            If My.Computer.FileSystem.FileExists(filename) = False Then
                Return Nothing
            Else
                Dim PimTestConfig As CPimTestConfig = Nothing
                Using StrmRd As New System.IO.StreamReader(filename)
                    Dim XSerz As New XmlSerializer(GetType(CPimTestConfig))
                    PimTestConfig = CType(XSerz.Deserialize(StrmRd), CPimTestConfig)
                    StrmRd.Close()
                End Using
                Return PimTestConfig
            End If
        Catch ex As Exception
            Throw New Exception(String.Format("Read file '{0}' fail!", filename) & vbNewLine & ex.Message)
        End Try
    End Function
End Class
Public Class CAutoConnect
    Public Enable As Boolean
End Class
Public Class CMII
    Public Enable As Boolean
End Class
Public Class CTestMode
    Public ModeName As String
End Class
Public Class CVibration
    Public Enable As Boolean
    Public Address As String
End Class
Public Class CInstrument
    Public Enable As Boolean
    Public FreqBand As String
    Public Model As String
    Public Vendor As String
    Public Address As String
    Public BandIdx As Integer
    Public TxLLoss As Decimal
    Public TxRLoss As Decimal
    Public SerialNumber As String
    Public Idn As String
    Public DateTime As DateTime
End Class