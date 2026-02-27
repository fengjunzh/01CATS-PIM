'Version 1.01: 13 Sep 2006
'Added Public method DefineProductsMenuAry to the OperatorInterface class which takes a string array as the only
' argument. This is to suplement the DefineProductsMenu method which
'requires a ParamArray

Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Collections.Generic
<System.Runtime.InteropServices.ProgId("clsOperatorInterface_NET.clsOperatorInterface")> Public Class clsOperatorInterface

	Dim frmMain As frmMain
	Dim frmSplash As frmSplash
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

	'Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long)
	'Private Const SW_SHOWNORMAL = 1
	'lres = ShowWindow(Form1.hwnd, SW_SHOWNORMAL)

	Public Sub New()
		MyBase.New()
		frmMain = New frmMain
		Common.clsOperator = Me
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
		frmMain.Text = String.Format("CommScope Antenna Test Software – {0} (Ver. {1} )", swName, Common.SW_Version)
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
			If frmMain IsNot Nothing Then frmMain.tsslServerName.Text = "Server: " & value
		End Set
	End Property
	WriteOnly Property ModeName() As String
		Set(value As String)
			'If frmMain IsNot Nothing Then frmMain.tsslModeName.Text = "Mode: " & value
			If frmMain IsNot Nothing Then frmMain.TSDP_Mode.Text = value
		End Set
	End Property

	'Can be called anytime during test
	Public Sub AddStatusMsg(ByVal Msg As String, Optional ByVal NewLine As Boolean = True)
		'Dim i As Short
		Try
			With frmMain.TxtStatus
				'If NewLine = False Then 'you must want to write over the last line
				'    'start at the end of the text (omit the very last NL) and look for the next-to-last line feed
				'    Try
				'        i = InStrRev(.Text, NL, Len(.Text) - 2, CompareMethod.Text)
				'        'trim off the last line (try to include the last NL)
				'        If i > 0 Then 'if you found a nl then trim off the last line
				'            .Text = Left(.Text, i + 1) ' this should leave the last nl so you are still on a new line
				'        End If
				'    Catch
				'    End Try
				'End If

				.AppendText(Msg)
				If NewLine Then .AppendText(NL)
				'.Text = .Text & Msg & NL
				.SelectionStart = Len(.Text)
			End With
		Catch
			frmMain.TxtStatus.Text = frmMain.TxtStatus.Text & vbCrLf & "I was called, but error out!"
		End Try
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
		With frmMain.LstTestStep
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

		With frmMain.LstTestStep
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

		frmMain.mnuTroubleshootMain.DropDown.Items.Add(newSeperator)
		For Each name In Names
			newItem = New ToolStripMenuItem
			newItem.Text = name
			AddHandler newItem.Click, AddressOf frmMain.mnuTroubleshootCustom_Click
			frmMain.mnuTroubleshootMain.DropDown.Items.Add(newItem)
		Next
		frmMain.mnuTroubleshootMain.Visible = True
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

		frmMain.mnuToolsMain.DropDown.Items.Add(newSeperator)
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

		frmMain.MnuHelpMain.DropDown.Items.Add(newSeperator)
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
        If frmMain.DataGridView1.Rows.Count > 0 Then Call frmMain.DisplayBlankRow()
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

    Public Sub ShowSplash(ByVal Duration As Single)

        frmSplash = New frmSplash

        frmSplash.Timer1.Enabled = False
        If Duration = 0 Then
            frmSplash.Timer1.Interval = 500
        Else
            frmSplash.Timer1.Interval = CInt(Duration * 1000.0#)
        End If
        frmSplash.Timer1.Enabled = True
        frmSplash.ShowDialog()
    End Sub

    Public Function WaitForSplash(ByVal Timeout As Single) As Boolean
        Dim StartTime As Single
        StartTime = VB.Timer()
        Do While Globals_Renamed.ElapsedTime(StartTime, VB.Timer()) < Timeout
            If frmSplash.Visible = False Then
                WaitForSplash = True
                Exit Function
            End If
        Loop
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
    Public Sub SetStepStatus(ByVal TestGroup As String, ByVal Status As Short)
        Call frmMain.SetStepStatus(TestGroup, Status)
    End Sub
End Class