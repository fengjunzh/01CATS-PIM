'Version 1.01: 13 Sep 2006
'Added Public method DefineProductsMenuAry to the OperatorInterface class which takes a string array as the only
' argument. This is to suplement the DefineProductsMenu method which
'requires a ParamArray

Option Strict Off
Option Explicit On
Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Timers
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Microsoft.VisualBasic
Imports Opc.Ua
Imports Opc.Ua.Client
Imports VB = Microsoft.VisualBasic
<System.Runtime.InteropServices.ProgId("clsOperatorInterface_NET.OperatorInterface")> Public Class OperatorInterface

    Dim frmMain As frmMain
    Public Event RunTest(ByVal Barcode As String, ByVal TestPhase As String, ByVal Cancel As Boolean)
    Public Event RunTestGroup(ByVal TestGroup As String, ByVal Barcode As String, ByVal TestPhase As String)
    Public Event PreTest(ByVal TestPhase As String, ByVal Barcode As String, ByVal Cancel As Boolean)
    Public Event PostTest()
    Public Event AbortTest()
    Public Event ExitInterface(ByVal Cancel As Integer)
    Public Event Calibrate(ByVal CalName As String)
    Public Event Help(ByVal HelpName As String)
    Public Event MenuClick(ByVal MenuName As String, ByRef ItemName As String)
    Public Event PowerOff()
    Private _SW_Name As String
    Private _SelectedTestPhase As String

    'Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long)
    'Private Const SW_SHOWNORMAL = 1
    'lres = ShowWindow(Form1.hwnd, SW_SHOWNORMAL)

    Public Sub New()
        MyBase.New()
        frmMain = New frmMain
        Common.GUI = Me
        pResults = New clsTestResults
        pCheck = New clsProcessCheck
        inDiagnosticMode = False
    End Sub
    Friend Sub REvntRunTest(ByVal Barcode As String, ByVal TestPhase As String, ByVal Cancel As Boolean)
        RaiseEvent RunTest(Barcode, TestPhase, Cancel)
    End Sub

    Friend Sub REvntRunTestGroup(ByVal TestGroup As String, ByVal Barcode As String, ByVal TestPhase As String)
        RaiseEvent RunTestGroup(TestGroup, Barcode, TestPhase)
    End Sub

    Friend Sub REvntPreTest(ByVal TestPhase As String, ByVal Barcode As String, ByVal Cancel As Boolean)
        RaiseEvent PreTest(TestPhase, Barcode, Cancel)
    End Sub
    Friend Sub REvntAbortTest()
        RaiseEvent AbortTest()
    End Sub

    Friend Sub REvntPostTest()
        RaiseEvent PostTest()
    End Sub

    Friend Sub REvntExitInterface(ByVal Cancel As Integer)
        RaiseEvent ExitInterface(Cancel)
    End Sub

    Friend Sub REvntMenuClick(ByVal MenuName As String, ByRef ItemName As String)
        RaiseEvent MenuClick(MenuName, ItemName)
    End Sub

    Private Sub Class_Terminate_Renamed()
        frmMain.Close()
    End Sub

    Protected Overrides Sub Finalize()
        Class_Terminate_Renamed()
        MyBase.Finalize()
    End Sub
    Public Property SAPFailSafeModeOnOff() As Boolean
        Get
            Return Common.SAPFailSafeModeOnOff
        End Get
        Set(ByVal value As Boolean)
            Common.SAPFailSafeModeOnOff = value
            frmMain.tsslSAPFailSafeModeOn.Text = IIf(value, "SAPFailSafeMode: ON", "SAPFailSafeMode: OFF")
            frmMain.tsslSAPFailSafeModeOn.ForeColor = IIf(value, Color.Red, Color.Black)
        End Set
    End Property
    Public WriteOnly Property Counter(tolerantCount As Integer) As Integer
        Set(ByVal value As Integer)
            frmMain.tsslAdapterCounter.Text = String.Format("Counter: {0}", value)
            frmMain.tsslAdapterCounter.ForeColor = IIf(value > tolerantCount - 10, Color.Red, Color.Black)
            Application.DoEvents()
        End Set
    End Property

    Property ProgressBarValue() As Integer
        Set(value As Integer)
            If frmMain IsNot Nothing Then frmMain.tsslProgressBar.Value = value
            Application.DoEvents()
        End Set
        Get
            Return frmMain.tsslProgressBar.Value
        End Get
    End Property
    Public Property ProgressMaxValue() As Integer
        Set(ByVal value As Integer)
            frmMain.tsslProgressBar.Maximum = value
        End Set
        Get
            Return frmMain.tsslProgressBar.Maximum
        End Get
    End Property
    Public WriteOnly Property Status() As String
        Set(value As String)
            If frmMain IsNot Nothing Then frmMain.tsslStatus.Text = value
            Application.DoEvents()
        End Set
    End Property
    Public Sub InitProgressBar()
        frmMain.tsslProgressBar.Minimum = 0
        frmMain.tsslProgressBar.Value = 0
        frmMain.tsslProgressBar.Maximum = 60
    End Sub
    Public Property labModeMsg() As String
        Get
            Return Common.labModeMsg
        End Get

        Set(ByVal value As String)
            Common.labModeMsg = value
            frmMain.labLabMode.Text = Common.labModeMsg
            If Len(Common.labModeMsg) > 0 Then
                frmMain.labLabMode.Visible = True
            Else
                frmMain.labLabMode.Visible = False
            End If
            If Common.inLabMode Then
                frmMain.labLabMode.ForeColor = Color.Red
            Else
                frmMain.labLabMode.ForeColor = Color.Black
            End If
        End Set
    End Property
    Public WriteOnly Property Database() As String
        Set(ByVal value As String)
            Common.gDatabase = value
            frmMain.tsslDatabase.Text = String.Format("Database: {0}", value)
        End Set
    End Property
    Public WriteOnly Property InstrVirtualMode() As String
        Set(ByVal value As String)
            Common.gInstrMode = value
            frmMain.tsslInstrVirtualMode.Text = String.Format("InstrMode: {0}", value)
        End Set
    End Property

    Public Property labMode() As Boolean
        Get
            Return Common.inLabMode
        End Get

        Set(ByVal value As Boolean)
            If Not Common.inLabMode And value Then
                frmMain.Text = frmMain.Text & " - SOFTWARE IN LAB MODE!"
            End If
            If Common.inLabMode And Not value Then
                frmMain.Text = Mid(frmMain.Text, 1, Len(frmMain.Text) - 24)
            End If
            Common.inLabMode = value
            If Common.inLabMode Then
                frmMain.shpLabMode.Visible = True
            Else
                frmMain.shpLabMode.Visible = False
            End If
        End Set
    End Property

    ReadOnly Property mainTop() As Integer
        Get
            Return frmMain.Top
        End Get
    End Property

    ReadOnly Property mainLeft() As Integer
        Get
            Return frmMain.Left
        End Get
    End Property

    ReadOnly Property mainHeight() As Integer
        Get
            Return frmMain.Height
        End Get
    End Property

    ReadOnly Property mainWidth() As Integer
        Get
            Return frmMain.Width
        End Get
    End Property

    Public ReadOnly Property MainFormHwnd() As Int32
        Get
            MainFormHwnd = CType(frmMain.Handle, Int32)
        End Get
    End Property

    Public Property repeatEnabled() As Boolean
        Get
            ' The Get property procedure is called when the value
            ' of a property is retrieved.
            Return Common.enableRepeat
        End Get
        Set(ByVal value As Boolean)
            ' The Set property procedure is called when the value 
            ' of a property is modified.  The value to be assigned
            ' is passed in the argument to Set.
            Common.enableRepeat = value
        End Set
    End Property

    'Should be called before ShowInterface
    Public Sub AssignTestPlan(ByRef TestObject As ITestPlan)
        Common.TestObj = TestObject
    End Sub

    Public Sub ShowInterface()
        frmMain.Show()
        Call frmMain.ClearResults()
    End Sub

    Private Sub UpdateCaption(ByVal swName As String)
        'frmMain.Text = Common.UUT_Type & " Test Software (version " & Common.SW_Version & ")" & IIf(Common.inLabMode, " - SOFTWARE IN LAB MODE!", "")
        frmMain.Text = String.Format("Andrew Heliax Test Software - {0} (Ver. {1} )", swName, Common.SW_Version)
    End Sub

    Public ReadOnly Property Handle() As Object
        Get
            Handle = frmMain
        End Get
    End Property
    Public ReadOnly Property StartTestTime() As String
        Get
            Return pResults.StartTestTime
        End Get
    End Property
    Public Property FixtureID() As String
        Get
            FixtureID = frmMain.TxtFixtureID.Text
        End Get
        Set(ByVal Value As String)
            frmMain.TxtFixtureID.Text = Value
            pResults.Fixture_ID = Value
        End Set
    End Property
    Public WriteOnly Property Plant() As String
        Set(ByVal Value As String)
            frmMain.tsslPlant.Text = "Factory: " & Value
            gPlant = Value
        End Set
    End Property
    Public WriteOnly Property MiiStatus() As Boolean
        Set(ByVal Value As Boolean)
            frmMain.tsslMiiStatus.Text = String.Format("MII: {0}", IIf(Value, "ON", "OFF"))
            frmMain.tsslMiiStatus.ForeColor = IIf(Value, SystemColors.WindowText, Color.Red)
            gMiiStatus = Value
        End Set
    End Property
    Public Property Pretest_PIM() As Boolean
        Set(ByVal Value As Boolean)
            gPretest = Value
        End Set
        Get
            Return gPretest
        End Get
    End Property
    Public Property Automation() As Boolean
        Set(ByVal Value As Boolean)
            frmMain.tsslAuto.Text = String.Format("Automation: {0}/{1}", IIf(Value, "ON", "OFF"), IIf(gPretest, "Pretest", "Finaltest"))
            Common.gAutomation = Value
        End Set
        Get
            Return Common.gAutomation
        End Get
    End Property
    Public Property OperatorID() As String
        Get
            OperatorID = frmMain.TxtTesterID.Text
        End Get
        Set(ByVal Value As String)
            frmMain.TxtTesterID.Text = Value
            pResults.Operator_ID = Value
        End Set
    End Property

    'Should be called before ShowInterface
    'Public Property UUT_Type() As String
    '  Get
    '    UUT_Type = Common.UUT_Type
    '  End Get
    '  Set(ByVal Value As String)
    '    Dim frmInvalid As frmInvalid
    '    Dim rType As clsGlobals.AssemblyType
    '    rType.progs = ""
    '    rType.types = ""

    '    Dim myDir As String = System.Reflection.Assembly.GetExecutingAssembly.CodeBase
    '    Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
    '    Dim br As New System.IO.BinaryReader(fs)
    '    rType.lenT = br.ReadInt32
    '    rType.lenP = br.ReadInt32
    '    rType.types = br.ReadString
    '    rType.progs = br.ReadString
    '    br.Close()
    '    fs.Close()

    '    rType.types = ConvertData(rType.types)
    '    rType.progs = ConvertData(rType.progs)

    '    If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
    '      MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
    '    Else
    '      If InStr(1, rType.types, "," & Value & ",", CompareMethod.Text) < 1 Then
    '        frmInvalid = New frmInvalid
    '        frmInvalid.Label2.Text = "Assembly_Type of """ & Value & """"
    '        frmInvalid.Label7.Text = "An Assembly_Type of ""Invalid_" & Value & """"
    '        frmInvalid.ShowDialog()
    '        Common.UUT_Type = "Invalid_" & Value
    '        pCheck.AssemblyType = "Invalid_" & Value
    '        '            MsgBox NewValue & " is not a valid Assembly_Type", vbOKOnly, "Invalid Assembly_Type"
    '        '            End
    '      Else
    '        Common.UUT_Type = Value
    '        'ProcessCheck.AssemblyType = newvalue
    '        pCheck.AssemblyType = Value
    '      End If
    '    End If
    '    pCheck.AssemblyType = Value
    '    Call UpdateCaption()
    '  End Set
    'End Property
    Public Property UUT_Type() As String
        Get
            UUT_Type = Common.UUT_Type
        End Get
        Set(ByVal Value As String)
            'Dim frmInvalid As frmInvalid
            'Dim rType As clsGlobals.AssemblyType
            'rType.progs = ""
            'rType.types = ""

            'Dim myDir As String = System.Reflection.Assembly.GetExecutingAssembly.CodeBase
            'Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
            'Dim br As New System.IO.BinaryReader(fs)
            'rType.lenT = br.ReadInt32
            'rType.lenP = br.ReadInt32
            'rType.types = br.ReadString
            'rType.progs = br.ReadString
            'br.Close()
            'fs.Close()

            'rType.types = ConvertData(rType.types)
            'rType.progs = ConvertData(rType.progs)

            'If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
            '    MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
            'Else
            '    If InStr(1, rType.types, "," & Value & ",", CompareMethod.Text) < 1 Then
            '        frmInvalid = New frmInvalid
            '        frmInvalid.Label2.Text = "Assembly_Type of """ & Value & """"
            '        frmInvalid.Label7.Text = "An Assembly_Type of ""Invalid_" & Value & """"
            '        frmInvalid.ShowDialog()
            '        Common.UUT_Type = "Invalid_" & Value
            '        pCheck.AssemblyType = "Invalid_" & Value
            '        '            MsgBox NewValue & " is not a valid Assembly_Type", vbOKOnly, "Invalid Assembly_Type"
            '        '            End
            '    Else
            '        Common.UUT_Type = Value
            '        'ProcessCheck.AssemblyType = newvalue
            '        pCheck.AssemblyType = Value
            '    End If
            'End If
            Common.UUT_Type = Value

            pCheck.AssemblyType = Value
            'Call UpdateCaption()
        End Set
    End Property
    ReadOnly Property Factory() As String
        Get
            Return Common.Location
        End Get
    End Property

    'Should be called before ShowInterface

    Public Property INI_File() As String
        Get
            INI_File = Common.INI_File
        End Get
        Set(ByVal Value As String)
            Common.INI_File = Value
            'ProcessCheck.PC_IniFile = newvalue
            pCheck.PC_IniFile = Value
        End Set
    End Property

    'Should be called before ShowInterface

    Public Property LimitsFile() As String
        Get
            LimitsFile = Common.LimitsFile
        End Get
        Set(ByVal Value As String)
            Common.LimitsFile = Value
        End Set
    End Property

    'Should be called before ShowInterface

    Public Property SW_Version() As String
        Get
            SW_Version = Common.SW_Version
        End Get
        Set(ByVal Value As String)
            Common.SW_Version = Value
            Call UpdateCaption(_SW_Name)
        End Set
    End Property
    WriteOnly Property SW_Name() As String
        Set(value As String)
            _SW_Name = value
        End Set
    End Property

    'Should be called before ShowInterface

    Public Property MTR_Version() As String
        Get
            MTR_Version = Common.MTR_Version
        End Get
        Set(ByVal Value As String)
            Common.MTR_Version = Value
        End Set
    End Property

    'Should be called before ShowInterface

    Public Property BarcodeMatch() As String
        Get
            BarcodeMatch = Common.BarcodeMatch
        End Get
        Set(ByVal Value As String)
            Common.BarcodeMatch = Value
        End Set
    End Property


    Public Property EstimatedDuration() As Integer
        Get
            EstimatedDuration = Common.EstimatedDuration
        End Get
        Set(ByVal Value As Integer)
            Common.EstimatedDuration = Value
        End Set
    End Property

    Public ReadOnly Property TestResults() As clsTestResults
        Get
            TestResults = pResults
        End Get
    End Property
    WriteOnly Property ServerName() As String
        Set(value As String)
            If frmMain IsNot Nothing Then frmMain.tsslSAPFailSafeModeOn.Text = "Server: " & value
        End Set
    End Property
    Public ReadOnly Property LogMessage() As String
        Get
            Return frmMain.TxtStatus.Text
        End Get
    End Property
    WriteOnly Property ModeName() As String
        Set(value As String)
            'If frmMain IsNot Nothing Then frmMain.tsslModeName.Text = "Mode: " & value
            If frmMain IsNot Nothing Then frmMain.TSDP_Mode.Text = value
        End Set
    End Property
    Public Property Dynamic() As Boolean
        Get
            Return Common.gDynamic
        End Get
        Set(ByVal Value As Boolean)
            Common.gDynamic = Value
            If frmMain IsNot Nothing Then frmMain.tsslDynamicStatic.Text = IIf(Value, "Dynamic", "Static")
        End Set
    End Property
    Public Property RetryCountMax() As SByte
        Get
            Return Common.gRetryCountMax
        End Get
        Set(ByVal Value As SByte)
            Common.gRetryCountMax = Value
            If frmMain IsNot Nothing Then frmMain.tsslRetryCount.Text = String.Format("Retry Count: {0}/{1}", RetryCountRest, Value)
        End Set
    End Property
    Public Property RetryCountRest() As SByte
        Get
            Return Common.gRetryCountRest
        End Get
        Set(ByVal Value As SByte)
            Common.gRetryCountRest = Value
            If frmMain IsNot Nothing Then frmMain.tsslRetryCount.Text = String.Format("Retry Count: {0}/{1}", Value, RetryCountMax)
        End Set
    End Property
    Public Property TestCountMax() As SByte
        Get
            Return Common.gTestCountMax
        End Get
        Set(ByVal Value As SByte)
            Common.gTestCountMax = Value
            If frmMain IsNot Nothing Then frmMain.tsslTestCount.Text = String.Format("Test Count: {0}/{1}", TestCountRest, Value)
        End Set
    End Property
    Public Property TestCountRest() As SByte
        Get
            Return Common.gTestCountRest
        End Get
        Set(ByVal Value As SByte)
            Common.gTestCountRest = Value
            If frmMain IsNot Nothing Then frmMain.tsslTestCount.Text = String.Format("Test Count: {0}/{1}", Value, TestCountMax)
        End Set
    End Property
    Public Property TestDoubleLength() As Boolean
        Get
            Return Common.TestDoubleLength
        End Get
        Set(ByVal Value As Boolean)
            Common.TestDoubleLength = Value
        End Set
    End Property

    ReadOnly Property SelectedTestPhase As String
        Get
            Return frmMain.dgvTestPhase.CurrentRow.Cells(0).Value.ToString
        End Get
    End Property
    Public Sub SendBeatHeart()
        If TestObj IsNot Nothing Then frmMain.SendBeatHeart()
    End Sub

    'Can be called anytime during test
    Public Sub AddStatusMsgSync(ByVal Msg As String, Optional ByVal NewLine As Boolean = True)
        Try
            With frmMain.TxtStatus
                .AppendText(String.Format("[{0:HH:mm:ss.fff}] {1}", Now, Msg))
                If NewLine Then .AppendText(NL)
                .SelectionStart = Len(.Text)
            End With
        Catch
            frmMain.TxtStatus.Text = frmMain.TxtStatus.Text & vbCrLf & "I was called, but error out!"
        End Try
    End Sub
    Public Sub AddStatusMsg(ByVal Msg As String, Optional ByVal NewLine As Boolean = True)
        If frmMain.InvokeRequired Then
            frmMain.BeginInvoke(New Action(Of String, Boolean)(AddressOf AddStatusMsgSync), Msg, NewLine)
        Else
            AddStatusMsgSync(Msg, NewLine)
        End If
        Call Globals_Renamed.MyDoEvents()
    End Sub
    Public Property PimFixtureReady() As Boolean
        Set(value As Boolean)
            gPimFixtureReady = value
        End Set
        Get
            Return gPimFixtureReady
        End Get
    End Property
#Region "OPC UA Monitored Item Handlers"
    Public Sub StationStatusChanged(monitoredItem As MonitoredItem, e As MonitoredItemNotificationEventArgs)
        If frmMain.InvokeRequired Then
            frmMain.Invoke(New Action(Of MonitoredItem, MonitoredItemNotificationEventArgs)(AddressOf StationStatusChanged), monitoredItem, e)
            Return
        End If

        Try
            Dim singleNotification = TryCast(e.NotificationValue, MonitoredItemNotification)
            If singleNotification IsNot Nothing Then
                Dim dv As DataValue = singleNotification.Value
                Dim nodeIdText = If(monitoredItem.StartNodeId?.ToString(), "(null)")
                If Not StatusCode.IsGood(dv.StatusCode) Then
                    frmMain.AppendTextToLog($"[{nodeIdText}] read failed: {dv.StatusCode}")
                    Return
                End If
                gTrigger_PIM = dv.Value
                If gTrigger_PIM Then
                    Dim msg As String = $"Received Trigger PIM, Start to request roatate fixture"
                    GUI.AddStatusMsg(msg)
                    WritePlcValues(numTestPhases.RequestSn)
                End If
            End If
        Catch ex As Exception
            WritePlcValues(numTestPhases.Err)
            frmMain.AppendTextToLog($"An error occurred while processing the notification : {ex.Message}")
        End Try
    End Sub

    Public Sub StationBarcodeChanged(monitoredItem As MonitoredItem, e As MonitoredItemNotificationEventArgs)
        If frmMain.InvokeRequired Then
            frmMain.Invoke(New Action(Of MonitoredItem, MonitoredItemNotificationEventArgs)(AddressOf StationBarcodeChanged), monitoredItem, e)
            Return
        End If

        Try
            Dim singleNotification As MonitoredItemNotification = TryCast(e.NotificationValue, MonitoredItemNotification)
            If singleNotification IsNot Nothing Then
                Dim dv As DataValue = singleNotification.Value
                Dim nodeIdText = If(monitoredItem.StartNodeId?.ToString(), "(null)")

                If Not StatusCode.IsGood(dv.StatusCode) Then
                    frmMain.AppendTextToLog($"[{nodeIdText}] read failed: {dv.StatusCode}")
                    Return
                End If

                gStationBarcode = CType(dv.Value, String)

                If gStationBarcode.Length = 0 Then Return


                Dim msg As String = $"Received Barcode {gStationBarcode} From PLC"

                If gTrigger_PIM Then frmMain.Invoke(New MethodInvoker(Sub()
                                                                          frmMain.txtSN1.Text = $"{gStationBarcode}"
                                                                      End Sub))
            End If
        Catch ex As Exception
            WritePlcValues(numTestPhases.Err)
            frmMain.AppendTextToLog($"An error occurred while processing the notification : {ex.Message}")
        End Try
    End Sub

    Private _seenFirst As New ConcurrentDictionary(Of UInteger, Boolean)()
    ' Store last-change timestamps per monitored item
    Private LastChange As New ConcurrentDictionary(Of UInteger, DateTime)
    ' Timer to check inactivity
    Public WithEvents CheckTimer As New Timers.Timer(1000) ' check every second

    Public Sub PimFixtureReadyStatusChanged(monitoredItem As MonitoredItem, e As MonitoredItemNotificationEventArgs)
        If frmMain.InvokeRequired Then
            frmMain.BeginInvoke(New Action(Of MonitoredItem, MonitoredItemNotificationEventArgs)(AddressOf PimFixtureReadyStatusChanged), monitoredItem, e)
            Return
        End If

        Try
            ' Use ClientHandle to uniquely identify the item
            Dim handle As UInteger = monitoredItem.ClientHandle

            LastChange(handle) = DateTime.UtcNow

            ' Skip the first notification for this item
            Dim alreadySeen As Boolean = _seenFirst.GetOrAdd(handle, Function(o) False)
            If Not alreadySeen Then
                ' Mark as seen and return (ignore first value)
                _seenFirst(handle) = True
                Return
            End If

            Dim singleNotification As MonitoredItemNotification = TryCast(e.NotificationValue, MonitoredItemNotification)
            If singleNotification IsNot Nothing Then
                Dim dv As DataValue = singleNotification.Value
                Dim nodeIdText = If(monitoredItem.StartNodeId?.ToString(), "(null)")

                If Not StatusCode.IsGood(dv.StatusCode) Then
                    frmMain.AppendTextToLog($"[{nodeIdText}] read failed: {dv.StatusCode}")
                    Return
                End If

                GUI.PimFixtureReady = CType(dv.Value, Boolean)

                If GUI.PimFixtureReady And gStationBarcode.Length > 0 Then frmMain.RunAllTest()

            End If
        Catch ex As Exception
            WritePlcValues(numTestPhases.Err)
            frmMain.AppendTextToLog($"An error occurred while processing the notification : {ex.Message}")
        End Try
    End Sub
    Public Sub HeartBeatIntChanged(monitoredItem As MonitoredItem, e As MonitoredItemNotificationEventArgs)
        If frmMain.InvokeRequired Then
            frmMain.Invoke(New Action(Of MonitoredItem, MonitoredItemNotificationEventArgs)(AddressOf HeartBeatIntChanged), monitoredItem, e)
            Return
        End If

        Try
            Dim singleNotification As MonitoredItemNotification = TryCast(e.NotificationValue, MonitoredItemNotification)
            If singleNotification IsNot Nothing Then
                Dim dv As DataValue = singleNotification.Value
                Dim nodeIdText = If(monitoredItem.StartNodeId?.ToString(), "(null)")

                If Not StatusCode.IsGood(dv.StatusCode) Then
                    frmMain.AppendTextToLog($"[{nodeIdText}] read failed: {dv.StatusCode}")
                    Return
                End If

                gHeartBeatInt = CType(dv.Value, Short)

            End If
        Catch ex As Exception
            WritePlcValues(numTestPhases.Err)
            frmMain.AppendTextToLog($"An error occurred while processing the notification : {ex.Message}")
        End Try
    End Sub
    Public Sub MonitorPLCHeartBeat()
        Try
            ' Create monitor with a provider that reads the shared variabl
            Dim monitor As New PulseMonitor(Function() gHeartBeatInt) With {
                .PollIntervalMs = 2000,         ' every 2 seconds
                .StaleTimeoutMs = 5000,         ' no update in 5s => Stale
                .UnchangedThreshold = 1,        ' alert immediately when unchanged for one check
                .ValidMin = 0,                 ' optional BPM-like bounds
                .ValidMax = 1
            }

            ' Wire up events
            AddHandler monitor.PulseChanged, Sub(oldV, newV, atUtc)
                                                 'Console.WriteLine($"[{atUtc:o}] Changed: {oldV} -> {newV}")
                                                 If oldV <> newV Then GUI.Status = "PLC Connected"
                                             End Sub

            AddHandler monitor.PulseUnchanged, Async Sub(v, repeats, atUtc)
                                                   'Console.WriteLine($"[{atUtc:o}] Unchanged ({v}), repeats={repeats}")
                                                   If repeats > 0 Then
                                                       GUI.Status = "PLC Disonnected"
                                                       Dim msg As String = "PLC connection lost, trying to reconnect..."
                                                       GUI.AddStatusMsg(String.Format(msg))
                                                       Await TestObj.ConnectOpcUaAsync()
                                                   End If

                                               End Sub

            'AddHandler monitor.Stale, Sub(lastUtc, ms)
            '							  Console.WriteLine($"Stale: no update since {If(lastUtc = DateTime.MinValue, "never", lastUtc.ToString("o"))}, elapsed={ms} ms")
            '						  End Sub


            'AddHandler monitor.RangeAlert, Sub(v, minV, maxV, atUtc)
            '								   Console.WriteLine($"[{atUtc:o}] Out of range: {v} (min={minV}, max={maxV})")
            '							   End Sub

            AddHandler monitor.ErrorOccurred, Sub(ex)
                                                  'Console.WriteLine($"ERROR: {ex.Message}")
                                                  Throw New Exception("Heart Beat Error")
                                              End Sub

            monitor.Start()


        Catch ex As Exception
            Throw New Exception($"MonitorPLCHeartBeat()::{vbCrLf}{ex.Message}")
        End Try
    End Sub
    Public Sub WritePlcValues(testPhases As numTestPhases, Optional res As Short = 0, Optional hearBeat As Short = 0)
        Try
            ' Determine station name based on global pretest flag
            Dim pimStation As String = If(gPretest, "1", "2")
            Dim msg As String = ""
            ' Initialize dictionary
            Dim PIM_Values = New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

            Select Case testPhases
                Case 1
                    ' Right after receive trig_PIM then set trig_PIM = 0 and busy = 1 to tell PLC to send SN and prepare to test
                    'PIM_Values.Add($"trig_PIM{pimStation}", CType(0, Short))
                    PIM_Values.Add($"busy_PIM{pimStation}", CType(1, Short))
                    PIM_Values.Add($"done_PIM{pimStation}", CType(0, Short))
                    msg = $"busy_PIM{pimStation} = 1 and done_PIM{pimStation} = 0, Request SN from PLC"
                Case 2
                    ' After init PIM instrument then set ready = 1 to tell PLC I am prepared/init done
                    Dim rotateInt As Integer = If(Common.gDynamic, 1, 2)
                    PIM_Values.Add($"instr_ready_PIM{pimStation}", CType(1, Short))
                    msg = $"instr_ready_PIM{pimStation} = 1 - Instrument is ready"
                Case 3
                    ' After received PIM rotation fixture ready singal then set fixture_ready_PIM = 1 to tell PLC we are ready and let's start to test
                    Dim rotateInt As Integer = If(Common.gDynamic, 1, 2)
                    'Dim rotateInt As Integer = 1
                    PIM_Values.Add($"ready_PIM{pimStation}", CType(1, Short))
                    msg = $"ready_PIM{pimStation} = 1 - I am ready please rotate fixture"
                Case 4
                    ' After test: done = 1, result = result_int
                    PIM_Values.Add($"done_PIM{pimStation}", CType(1, Short))
                    PIM_Values.Add($"result_PIM{pimStation}", res)
                    msg = $"done_PIM{pimStation} = 1 - I am done, result_PIM{pimStation} = {res}"
                Case 5
                    ' Send Heart Beat signal to let PLC know we are alive during long test
                    PIM_Values.Add($"online_PIM{pimStation}", CType(hearBeat, Short))
                Case 6
                    ' When error happened reset and send NG
                    PIM_Values.Add($"trig_PIM{pimStation}", CType(0, Short))
                    PIM_Values.Add($"busy_PIM{pimStation}", CType(0, Short))
                    PIM_Values.Add($"ready_PIM{pimStation}", CType(0, Short))
                    PIM_Values.Add($"done_PIM{pimStation}", CType(0, Short))
                    PIM_Values.Add($"result_PIM{pimStation}", 3)
                    msg = $"Error happend, reset all parameters to 0 and send NG = 3"
                Case 7 'Finally reset trig_PIM to 0
                    PIM_Values.Add($"trig_PIM{pimStation}", CType(0, Short))
                    msg = $"Finally reset trig_PIM{pimStation} = 0"
            End Select

            TestObj.WritePlcAsync(PIM_Values)
            If testPhases <> 5 Then AddStatusMsg(msg)
        Catch ex As Exception
            Throw New Exception($"AssignPlcWriteValues()::{vbCrLf}{ex.Message}")
        End Try
    End Sub
#End Region

    Private Sub CheckTimer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles CheckTimer.Elapsed
        For Each kvp In LastChange
            Dim handle = kvp.Key
            Dim lastTime = kvp.Value

            Dim idle = DateTime.UtcNow - lastTime

            ' Display the "no change" timespan
            Console.WriteLine($"Item {handle}: no change for {idle.TotalSeconds:F1}s")

            If idle > TimeSpan.FromSeconds(60) Then
                AddStatusMsg($"Item PIMFixtureReadyInt has not changed for more than 60 seconds!")
                Call TestObj.ClosePimDevice()
                GUI.SetTextSafe(frmMain.txtSN1, "")
                CheckTimer.Stop() ' Stop the timer to prevent repeated alerts until we handle the issue
                ' You can trigger actions here, e.g. alert the user or attempt to reconnect
            End If

            ' You can also trigger actions if idle exceeds a threshold
            ' If idle > TimeSpan.FromSeconds(30) Then ...
        Next
    End Sub

    Public Sub SetTextSafe(tb As System.Windows.Forms.TextBox, text As String)
        If tb.InvokeRequired Then
            tb.Invoke(New Action(Of System.Windows.Forms.TextBox, String)(AddressOf SetTextSafe), tb, text)
        Else
            tb.Text = text
        End If
    End Sub
    Public Sub ClearStatusMsg()
        frmMain.TxtStatus.Text = ""
    End Sub

    Public Sub DefineTestGroupsAry(ByVal TestStep As String, ByVal TestGroups() As String)
        Dim name As String
        Dim x As New Collection
        If Common.TestGroups.Contains(TestStep) Then Common.TestGroups.Remove(TestStep)
        Call Common.TestGroups.Add(x, TestStep)
        For Each name In TestGroups
            If x.Contains(name) Then
                x.Remove(name)
            End If
            'Try
            '    x.Remove(name)
            'Catch ex As Exception

            'End Try
            x.Add(name)
        Next name
        With frmMain.dgvTestPhase
            Call frmMain.UpdateGroupsGrid(TestStep)
        End With
    End Sub

    Public Sub DefineTestGroups(ByVal TestStep As String, ByVal ParamArray TestGroups() As String)
        Dim name As String
        Dim Groups() As String
        ReDim Groups(TestGroups.Length - 1)
        Dim i As Integer = 0

        For Each name In TestGroups
            Groups(i) = name
            i += 1
        Next

        DefineTestGroupsAry(TestStep, Groups)

        'Dim x As New Collection
        'Call Common.TestGroups.Add(x, TestStep)
        'For Each name In TestGroups
        '    x.Add(name)
        'Next name
        'With frmMain.LstTestStep
        '    Call frmMain.UpdateGroupsGrid(frmMain.LstTestStep.SelectedItem.ToString())
        'End With
    End Sub

    'Public Sub DefineTestStepsAry(ByVal TestSteps() As String)
    '    Dim name As String
    '    Dim progID As String
    '    Dim frmInvalid As frmInvalid

    '    Dim rType As clsGlobals.AssemblyType
    '    rType.progs = ""
    '    rType.types = ""

    '    Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
    '    Dim br As New System.IO.BinaryReader(fs)
    '    rType.lenT = br.ReadInt32
    '    rType.lenP = br.ReadInt32
    '    rType.types = br.ReadString
    '    rType.progs = br.ReadString
    '    br.Close()
    '    fs.Close()

    '    rType.types = ConvertData(rType.types)
    '    rType.progs = ConvertData(rType.progs)

    '    With frmMain.LstTestStep
    '        Call .Items.Clear()
    '        For Each name In TestSteps
    '            progID = "InvalidProgramID"
    '            If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
    '                MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
    '            Else
    '                If InStr(1, rType.progs, "," & name & ",", CompareMethod.Text) < 1 Then
    '                    frmInvalid = New frmInvalid
    '                    frmInvalid.Label2.Text = "Program_ID of """ & name & """"
    '                    frmInvalid.Label7.Text = "A Program_ID of ""Invalid_" & name & """"
    '                    frmInvalid.ShowDialog()
    '                    progID = "Invalid_" & name
    '                Else
    '                    progID = name
    '                End If
    '            End If
    '            Call .Items.Add(progID)
    '        Next name
    '        .SelectedIndex = 0
    '    End With
    'End Sub

    Public Sub DefineTestStepsAry(ByVal TestSteps() As String)
        Dim name As String
        Dim progID As String
        'Dim frmInvalid As frmInvalid

        Dim rType As clsGlobals.AssemblyType
        rType.progs = ""
        rType.types = ""

        'Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
        'Dim br As New System.IO.BinaryReader(fs)
        'rType.lenT = br.ReadInt32
        'rType.lenP = br.ReadInt32
        'rType.types = br.ReadString
        'rType.progs = br.ReadString
        'br.Close()
        'fs.Close()

        'rType.types = ConvertData(rType.types)
        'rType.progs = ConvertData(rType.progs)

        With frmMain.dgvTestPhase
            Call .Rows.Clear()
            For Each name In TestSteps
                'progID = "InvalidProgramID"
                'If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
                '  MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
                'Else
                '  If InStr(1, rType.progs, "," & name & ",", CompareMethod.Text) < 1 Then
                '    frmInvalid = New frmInvalid
                '    frmInvalid.Label2.Text = "Program_ID of """ & name & """"
                '    frmInvalid.Label7.Text = "A Program_ID of ""Invalid_" & name & """"
                '    frmInvalid.ShowDialog()
                '    progID = "Invalid_" & name
                '  Else
                '    progID = name
                '  End If
                'End If
                progID = name
                Call .Rows.Add()
                .Rows(.Rows.Count - 1).Cells(0).Value = progID
            Next name
            If .Rows.Count > 0 Then .Rows(0).Selected = True
            '.SelectedIndex = 0
        End With
    End Sub

    Public Sub DefineTestSteps(ByVal ParamArray TestSteps() As String)
        Dim name As String
        Dim Steps() As String
        ReDim Steps(TestSteps.Length - 1)
        Dim i As Integer = 0

        For Each name In TestSteps
            Steps(i) = name
            i += 1
        Next

        DefineTestStepsAry(Steps)

        'Dim name As String
        'Dim progID As String
        'Dim frmInvalid As frmInvalid

        'Dim rType As clsGlobals.AssemblyType
        'rType.progs = ""
        'rType.types = ""

        'Dim fs As New System.IO.FileStream(Application.StartupPath & "\RFPAAssemblyTypes.NET.dat", System.IO.FileMode.Open, System.IO.FileAccess.Read)
        'Dim br As New System.IO.BinaryReader(fs)
        'rType.lenT = br.ReadInt32
        'rType.lenP = br.ReadInt32
        'rType.types = br.ReadString
        'rType.progs = br.ReadString
        'br.Close()
        'fs.Close()

        'rType.types = ConvertData(rType.types)
        'rType.progs = ConvertData(rType.progs)

        'With frmMain.LstTestStep
        '    Call .Items.Clear()
        '    For Each name In TestSteps
        '        progID = "InvalidProgramID"
        '        If rType.lenT <> Len(rType.types) Or rType.lenP <> Len(rType.progs) Then
        '            MsgBox("RFPAAssemblyTypes.NET.dat is corrupt", MsgBoxStyle.OkOnly, "Corrupt RFPAAssemblyTypes.NET.dat")
        '        Else
        '            If InStr(1, rType.progs, "," & name & ",", CompareMethod.Text) < 1 Then
        '                frmInvalid = New frmInvalid
        '                frmInvalid.Label2.Text = "Program_ID of """ & name & """"
        '                frmInvalid.Label7.Text = "A Program_ID of ""Invalid_" & name & """"
        '                frmInvalid.ShowDialog()
        '                progID = "Invalid_" & name
        '            Else
        '                progID = name
        '            End If
        '        End If
        '        Call .Items.Add(progID)
        '    Next name
        '    .SelectedIndex = 0
        'End With
    End Sub

    Public Sub DefineCalMenu(ByVal ParamArray CalNames() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator

        frmMain.mnuCalibrateMain.DropDown.Items.Add(newSeperator)
        For Each name In CalNames
            newItem = New ToolStripMenuItem
            newItem.Text = name
            AddHandler newItem.Click, AddressOf frmMain.mnuCalibrateCustom_Click
            frmMain.mnuCalibrateMain.DropDown.Items.Add(newItem)
        Next
        frmMain.mnuCalibrateMain.Visible = True
    End Sub

    Public Sub DefineTroubleshootMenu(ByVal ParamArray Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator

        'frmMain.mnuTroubleshootMain.DropDown.Items.Add(newSeperator)
        For Each name In Names
            newItem = New ToolStripMenuItem
            newItem.Text = name
            AddHandler newItem.Click, AddressOf frmMain.mnuTroubleshootCustom_Click
            frmMain.mnuTroubleshootMain.DropDown.Items.Add(newItem)
            'frmMain.mnuTroubleshootMain.DropDown.Items.Add(newSeperator)
        Next
        frmMain.mnuTroubleshootMain.Visible = True
    End Sub

    Public Sub DefineSAPMenu(ByVal ParamArray Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator

        'frmMain.mnuSAPMain.DropDown.Items.Add(newSeperator)
        For Each name In Names
            newItem = New ToolStripMenuItem
            newItem.Text = name
            AddHandler newItem.Click, AddressOf frmMain.mnuSAPCustom_Click
            frmMain.mnuSAPMain.DropDown.Items.Add(newItem)
        Next
        frmMain.mnuSAPMain.Visible = True
    End Sub

    Public Sub DefineProductsMenuAry(ByVal Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator
        Dim first As Boolean = True

        frmMain.mnuProducts.DropDown.Items.Add(newSeperator)
        For Each name In Names
            name = name.Trim()
            newItem = New ToolStripMenuItem
            newItem.Text = name
            If first Then
                newItem.Checked = True
                frmMain.Text = name & " Test Software (version " & Common.SW_Version & ")" & IIf(Common.inLabMode, " - SOFTWARE IN LAB MODE!", "")
                first = False
            End If
            AddHandler newItem.Click, AddressOf frmMain.mnuProductsCustom_Click
            frmMain.mnuProducts.DropDown.Items.Add(newItem)
        Next
        frmMain.mnuProducts.Visible = True
    End Sub

    Public Sub DefineProductsMenu(ByVal ParamArray Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator
        Dim first As Boolean = True

        frmMain.mnuProducts.DropDown.Items.Add(newSeperator)
        For Each name In Names
            newItem = New ToolStripMenuItem
            newItem.Text = name
            If first Then
                newItem.Checked = True
                frmMain.Text = name & " Test Software (version " & Common.SW_Version & ")" & IIf(Common.inLabMode, " - SOFTWARE IN LAB MODE!", "")
                first = False
            End If
            AddHandler newItem.Click, AddressOf frmMain.mnuProductsCustom_Click
            frmMain.mnuProducts.DropDown.Items.Add(newItem)
        Next
        frmMain.mnuProducts.Visible = True
    End Sub

    Public Sub DefineToolsMenu(ByVal ParamArray Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator

        'frmMain.mnuToolsMain.DropDown.Items.Add(newSeperator)
        For Each name In Names
            newItem = New ToolStripMenuItem
            newItem.Text = name
            AddHandler newItem.Click, AddressOf frmMain.mnuToolsCustom_Click
            frmMain.mnuToolsMain.DropDown.Items.Add(newItem)
        Next
        frmMain.mnuToolsMain.Visible = True
    End Sub

    Public Sub DefineHelpMenu(ByVal ParamArray Names() As String)
        Dim name As String
        Dim newItem As ToolStripMenuItem
        Dim newSeperator As New ToolStripSeparator

        'frmMain.MnuHelpMain.DropDown.Items.Add(newSeperator)
        For Each name In Names
            newItem = New ToolStripMenuItem
            newItem.Text = name
            frmMain.MnuHelpMain.DropDown.Items.Add(newItem)
        Next
        frmMain.MnuHelpMain.Visible = True
    End Sub
    'Public Sub DefinePhaseStationMenu(PhaseStationList As IEnumerable(Of Object), DefaultPhaseStation As Object)
    '	Try
    '		Dim tsObject As ToolStrip = frmMain.tsPhaseStation
    '		tsObject.Items.Clear()

    '		If PhaseStationList Is Nothing Then
    '			frmMain.tsPhaseStation.Visible = False
    '			Return
    '		End If

    '		Dim tsLable As ToolStripLabel
    '		Dim tsMenuBtn As ToolStripButton
    '		Dim tsSplitBtn As New ToolStripSplitButton
    '		Dim tsMenuItem As ToolStripMenuItem

    '		Dim newFont As New Font("Segoe UI", 9, FontStyle.Bold)
    '		Dim i As Integer

    '		tsLable = New ToolStripLabel

    '		If DefaultPhaseStation IsNot Nothing Then
    '			tsLable.ToolTipText = "Phase station"
    '			tsLable.Text = DefaultPhaseStation.ToString
    '		End If

    '		tsLable.Font = newFont
    '		tsLable.Image = frmMain.imgPhaseStation.Images(1)
    '		tsObject.Items.Add(tsLable)

    '		i = 0
    '		For Each tsItem As Object In PhaseStationList
    '			If i < 5 Then
    '				tsObject.Items.Add(New ToolStripSeparator)
    '				tsMenuBtn = New ToolStripButton
    '				tsMenuBtn.Text = tsItem.ToString
    '				tsMenuBtn.Tag = tsItem
    '				tsMenuBtn.Image = frmMain.imgPhaseStation.Images(0)
    '				AddHandler tsMenuBtn.Click, AddressOf frmMain.mnuPhaseStationToolStripButton_Click
    '				tsObject.Items.Add(tsMenuBtn)
    '			Else
    '				If i = 5 Then
    '					tsObject.Items.Add(New ToolStripSeparator)
    '					tsSplitBtn.Text = "..."
    '					tsSplitBtn.Image = frmMain.imgPhaseStation.Images(0)
    '					tsObject.Items.Add(tsSplitBtn)
    '				End If

    '				tsMenuItem = New ToolStripMenuItem
    '				tsMenuItem.Text = tsItem.ToString
    '				tsMenuItem.Tag = tsItem
    '				tsMenuItem.Image = frmMain.imgPhaseStation.Images(0)
    '				AddHandler tsMenuItem.Click, AddressOf frmMain.mnuPhaseStationToolStripItem_Click
    '				tsSplitBtn.DropDown.Items.Add(tsMenuItem)
    '			End If

    '			i += 1
    '		Next

    '		frmMain.tsPhaseStation.Visible = True

    '	Catch ex As Exception
    '		Throw New Exception("DefinePhaseStationMenu()" & vbCrLf & ex.Message)
    '	End Try
    'End Sub
    Public Sub DefinePhaseStationMenu(PhaseStationList As IEnumerable(Of Object), DefaultPhaseStation As Object)
        Try

            frmMain.TSDP_PhaseStation.DropDownItems.Clear()

            If PhaseStationList Is Nothing Then
                frmMain.TSDP_PhaseStation.Text = "N/A"
                Return
            End If
            'If PhaseStationList Is Nothing Then
            '	frmMain.tsTestOptions.Visible = False
            '	Return
            'End If

            Dim tsMenuItem As ToolStripMenuItem
            'Dim newFont As New Font("Segoe UI", 9, FontStyle.Regular)
            Dim newFont As New Font("Century Gothic", 12, FontStyle.Regular)

            If DefaultPhaseStation IsNot Nothing Then
                frmMain.TSDP_PhaseStation.Text = DefaultPhaseStation.ToString
                'frmMain.TSL_PhaseStation.Text = DefaultPhaseStation.ToString
                'frmMain.TSL_PhaseStation.Font = newFont
                'frmMain.TSL_PhaseStation.Image = frmMain.imgPhaseStation.Images(1)
            End If

            'frmMain.TSDP_PhaseStation.Image = frmMain.imgPhaseStation.Images(0)
            For Each tsItem As Object In PhaseStationList
                tsMenuItem = New ToolStripMenuItem
                tsMenuItem.Text = tsItem.ToString
                tsMenuItem.Tag = tsItem
                tsMenuItem.Font = newFont
                'tsMenuItem.Image = frmMain.imgPhaseStation.Images(0)
                tsMenuItem.ImageScaling = ToolStripItemImageScaling.None
                AddHandler tsMenuItem.Click, AddressOf frmMain.mnuPhaseStationToolStripItem_Click
                frmMain.TSDP_PhaseStation.DropDown.Items.Add(tsMenuItem)
            Next

            'frmMain.tsPhaseStation.Visible = True

        Catch ex As Exception
            Throw New Exception("DefinePhaseStationMenu()" & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Sub DefineModeMenu(ModeList As IEnumerable(Of Object))
        Try
            frmMain.TSDP_Mode.DropDownItems.Clear()

            'If ModeList Is Nothing Then
            '	frmMain.tsTestOptions.Visible = False
            '	Return
            'End If

            Dim tsMenuItem As ToolStripMenuItem
            Dim newFont As New Font("Century Gothic", 12, FontStyle.Regular)


            'frmMain.TSDP_PhaseStation.Image = frmMain.imgPhaseStation.Images(0)
            For Each tsItem As Object In ModeList
                tsMenuItem = New ToolStripMenuItem
                tsMenuItem.Text = tsItem.ToString
                tsMenuItem.Font = newFont
                tsMenuItem.Tag = tsItem
                'tsMenuItem.Image = frmMain.imgPhaseStation.Images(0)
                tsMenuItem.ImageScaling = ToolStripItemImageScaling.None
                AddHandler tsMenuItem.Click, AddressOf frmMain.mnuModeToolStripItem_Click
                frmMain.TSDP_Mode.DropDown.Items.Add(tsMenuItem)
            Next

            'frmMain.tsTestOptions.Visible = True

        Catch ex As Exception
            Throw New Exception("DefinePhaseStationMenu()" & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Function RecordResult(ByVal Testname As String, ByVal measured As Double, ByVal Low As Double, ByVal High As Double) As Boolean
        Dim pass As Boolean
        pass = pResults.RecordNumeric(Testname, measured, Low, High)
        Call frmMain.DisplayNumericResult(Testname, Low, measured, High, pass)
        Call Globals_Renamed.MyDoEvents() 'to check abort flag
        Return pass
    End Function

    Public Sub RecordStringResult(ByVal Testname As String, ByVal measured As String, Optional ByVal MatchString As String = "*")
        Dim pass As Boolean
        pass = pResults.RecordString(Testname, measured, MatchString)
        Call frmMain.DisplayStringResult(Testname, "NA", measured, "NA", pass)
        Call Globals_Renamed.MyDoEvents() 'to check abort flag
    End Sub

    Public Sub RecordPassFail(ByVal Testname As String, ByVal IsPass As Boolean)
        Call pResults.RecordPassFail(Testname, IsPass)
        Call frmMain.DisplayStringResult(Testname, "True", IsPass.ToString, "True", IsPass)
        Call Globals_Renamed.MyDoEvents() 'to check abort flag
    End Sub


    Public Sub StartTestGroup(ByVal GroupName As String, Optional ByVal ClearFirst As Boolean = True)
        Call StopTestGroup() 'In case user forgot to call
        Call pResults.StartTestGroup(GroupName)
        Call frmMain.SetGroupStatus(GroupName, 1)
        If frmMain.dgvTestResult.Rows.Count > 0 Then Call frmMain.DisplayBlankRow()
    End Sub
    Public Sub DisplayBlankRow()
        If frmMain.dgvTestResult.Rows.Count > 0 Then Call frmMain.DisplayBlankRow()
    End Sub
    Public Function StopTestGroup() As Boolean
        Dim stat As Short
        Dim tmp As String

        tmp = pResults.TestGroup()
        If tmp = "" Then Return False

        If pResults.GetGroupFailures(tmp) > 0 Then
            stat = 3
        Else
            If pResults.GetGroupPasses(tmp) > 0 Then
                stat = 2
            Else
                stat = 0
            End If
        End If

        Call frmMain.SetGroupStatus(tmp, stat)
        Call pResults.StopTestGroup()

        If stat = 2 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Public Sub SetBarcode(ByVal NewValue As String)
    '  frmMain.TxtBarcode = NewValue
    'End Sub
    '
    'Public Property Get CelesticaBarcode() As String
    '  CelesticaBarcode = Common.CelesticaBarcode
    'End Property
    '
    Private Function GetTestName(ByVal menustr As String) As String
        Dim i As Short
        For i = 1 To Len(menustr)
            If Not Mid(menustr, i, 1) Like "[a-zA-Z0-9_]" Then
                Mid(menustr, i, 1) = "_"
            End If
        Next
        GetTestName = UCase(menustr)
    End Function
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
    Public Sub SetGroupStatus(ByVal TestGroup As String, ByVal Status As Short)
        Call frmMain.SetGroupStatus(TestGroup, Status)
    End Sub
    Public Sub SetStepStatus(ByVal TestStep As String, ByVal Status As Short)
        Call frmMain.SetStepStatus(TestStep, Status)
    End Sub
End Class
