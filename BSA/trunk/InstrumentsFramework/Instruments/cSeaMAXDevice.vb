Public Class cSeaMAXDevice00000000000000000000

    Private m_objSeaMAX As Sealevel.SeaMAX
    Private m_intDeviceCount As Integer
    Private m_intDeviceID As Integer
    Private m_strModel As Integer
    Private m_strName As String
    Private m_abytSL As Byte()
    ' add by tony start======================================================================
    Private m_InputPin1 As Boolean = False
    Private m_InputPin2 As Boolean = False
    Private m_InputPin3 As Boolean = False
    Private m_InputPin4 As Boolean = False
    Public ReadOnly Property InputPin1 As Boolean
        Get
            Return m_InputPin1
        End Get
    End Property

    Public ReadOnly Property InputPin2 As Boolean
        Get
            Return m_InputPin2
        End Get
    End Property

    Public ReadOnly Property InputPin3 As Boolean
        Get
            Return m_InputPin3
        End Get
    End Property
    Public ReadOnly Property InputPin4 As Boolean
        Get
            Return m_InputPin4
        End Get
    End Property
    'add by tony end=========================================================

    Public ReadOnly Property ReadBytes() As Byte()
        Get
            Return m_abytSL
        End Get
    End Property

    Public ReadOnly Property DeviceCount As Integer
        Get
            Return m_intDeviceCount
        End Get
    End Property

    Public ReadOnly Property DeviceID As Integer
        Get
            Return m_intDeviceID
        End Get
    End Property

    Public ReadOnly Property Model As Integer
        Get
            Return m_strModel
        End Get
    End Property

    Public ReadOnly Property Name As String
        Get
            Return m_strName
        End Get
    End Property

    Public Sub New()

        Dim ret As Integer

        Try
            m_objSeaMAX = New Sealevel.SeaMAX

            ret = m_objSeaMAX.SDL_Initialize()
            If ret < 0 Then Throw New Exception '("Don't find SeaDACDevice device")

            m_intDeviceCount = m_objSeaMAX.SDL_SearchForDevices()
            If m_intDeviceCount < 0 Then Throw New Exception '("Don't find SeaDACDevice device")

            ret = m_objSeaMAX.SDL_FirstDevice()
            ret = m_objSeaMAX.SDL_GetDeviceID(m_intDeviceID)  'expect only one device connected, at index 0

            m_objSeaMAX.SDL_GetModel(m_strModel)
            ret = m_objSeaMAX.SDL_GetName(m_strName)

            m_objSeaMAX.SM_Open(m_strName)

        Catch ex As Exception

            MsgBox("An error occurred while initializing SeaDAC Lite.  Please check the connections and try again.", MsgBoxStyle.Critical And MsgBoxStyle.Exclamation, "SeaDAC Lite Error")
            m_objSeaMAX.SDL_Cleanup()
            m_objSeaMAX.SM_Close()
            Throw New Exception
        End Try

    End Sub

    Public Function ReadDigitalInputs() As Integer

        m_InputPin1 = False 'add tony
        m_InputPin2 = False 'add tony
        m_InputPin3 = False 'add tony
        m_InputPin4 = False 'add tony

        Dim ret As Integer = 0
        'Dim SL As Byte() = New Byte(3) {0, 0, 0, 0}

        m_abytSL = New Byte(3) {0, 0, 0, 0}

        ret = m_objSeaMAX.SM_ReadDigitalInputs(0, 4, m_abytSL)

        m_InputPin1 = CBool(m_abytSL(0) And &H1)  'add tony
        m_InputPin2 = CBool(m_abytSL(0) And &H2)  'add tony
        m_InputPin3 = CBool(m_abytSL(0) And &H4)  'add tony
        m_InputPin4 = CBool(m_abytSL(0) And &H8)  'add tony

        Return ret

    End Function

    Public Sub StartFixture()

        Dim ret As Integer

        Dim SLR As Byte() = New Byte() {1}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(1000)
        SLR(0) = 16
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(500)

    End Sub

    Public Sub StopFixture()

        Dim ret As Integer

        Dim SLR As Byte() = New Byte() {2}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(1000)
        SLR(0) = 16
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(500)

    End Sub

    Public Sub OpenClamp()

        Dim ret As Integer

        Dim SLR As Byte() = New Byte() {4}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(1000)
        SLR(0) = 16
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(500)

    End Sub

    Public Sub CloseClamp()

        Dim ret As Integer

        Dim SLR As Byte() = New Byte() {8}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(1000)
        SLR(0) = 16
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(0, 4, SLR)
        Threading.Thread.Sleep(500)

    End Sub

    Public Sub Close()

        m_objSeaMAX.SDL_Cleanup()
        m_objSeaMAX.SM_Close()

    End Sub

End Class

