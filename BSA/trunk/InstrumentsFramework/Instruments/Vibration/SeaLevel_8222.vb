
Imports AndrewIntegratedProducts.InstrumentsFramework
Public Class SeaLevel_8222
    Inherits Instrument
    Implements IUSB_VIB_CONTROLLER

    Private m_objSeaMAX As Sealevel.SeaMAX


    Public Overrides Sub Close() Implements IUSB_VIB_CONTROLLER.Close
        m_objSeaMAX.SDL_Cleanup()
        m_objSeaMAX.SM_Close()
    End Sub

    Public Overrides Function Open() As Boolean Implements IUSB_VIB_CONTROLLER.Open
        Throw New NotImplementedException()
    End Function

    Public Overloads Function Open(ByVal COMPort As String) As Boolean Implements IUSB_VIB_CONTROLLER.Open


        Try
            m_objSeaMAX = New Sealevel.SeaMAX

            'Open the connection to the device
            Dim errno As Integer = m_objSeaMAX.SM_Open(COMPort)
            If errno < 0 Then
                MsgBox("Open Returned Error Code: " & errno)
                Return False
                'Else
                '    MsgBox("SeaMAX Device Has Opened Successfully")
            End If

            'Select the device. 247 is the default slave ID, for 8222(SeaDAC Modeule) only this value 247
            m_objSeaMAX.SM_SelectDevice(247)
            Return True

        Catch ex As Exception
            MsgBox("An error occurred while initializing SeaDAC Lite.  Please check the connections and try again.", MsgBoxStyle.Critical And MsgBoxStyle.Exclamation, "SeaDAC Lite Error")
            m_objSeaMAX.SDL_Cleanup()
            m_objSeaMAX.SM_Close()
            Return False
        End Try

    End Function

    '8222 single control===============================================================================
    Public Function RelayON(ByVal relayNo As Integer) As Boolean

        Dim ret As Integer
        Dim SLR As Byte() = New Byte() {1}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(relayNo - 1, 1, SLR)
        If ret < 0 Then
            MsgBox("RelayON->" & relayNo & " Write Digital Outputs Returned Error Code: " & ret)
            Return False
        Else
            Return True
        End If
    End Function

    Public Function RelayOFF(ByVal relayNo As Integer) As Boolean

        Dim ret As Integer
        Dim SLR As Byte() = New Byte() {0}
        ret = m_objSeaMAX.SM_WriteDigitalOutputs(relayNo - 1, 1, SLR)
        If ret < 0 Then
            MsgBox("RelayOFF->" & relayNo & " Write Digital Outputs Returned Error Code: " & ret)
            Return False
        Else
            Return True
        End If

    End Function
    '  ===============================================================================

    Public Function StartVIB() As Boolean Implements IUSB_VIB_CONTROLLER.StartVIB
        Try
            Return RelayON(8)
        Catch ex As Exception
            Throw New Exception("StartVIB()::" & ex.Message)
        End Try
    End Function

    Public Sub StopVIB() Implements IUSB_VIB_CONTROLLER.StopVIB
        Try
            RelayOFF(8)
        Catch ex As Exception
            Throw New Exception("StopVIB()::" & ex.Message)
        End Try
    End Sub

    Public Function OpenRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.OpenRelay
        Try
            Return RelayOFF(num)
        Catch ex As Exception
            Throw New Exception("OpenRelay()::" & ex.Message)
        End Try
    End Function
    Public Function CloseRelay(num As Short) As Boolean Implements IUSB_VIB_CONTROLLER.CloseRelay
        Try
            Return RelayON(num)
        Catch ex As Exception
            Throw New Exception("CloseRelay()::" & ex.Message)
        End Try
    End Function

    'Read input  8222可以使用16个input信息，这里先使用4个（1，2，3，4）====================================================================================================================
    'Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IUSB_VIB_CONTROLLER.ReadInput
    '    Try
    '        Dim Inputdata As Boolean = False

    '        Dim ret As Integer = 0
    '        Dim m_abytSL As Byte() = New Byte(0) {0}
    '        ret = m_objSeaMAX.SM_ReadDigitalInputs(0, 4, m_abytSL)
    '        If ret < 0 Then
    '            MsgBox("Read input Returned Error Code: " & ret)
    '            Return False
    '        End If

    '        Select Case InputNo
    '            Case 1
    '                Inputdata = CBool(m_abytSL(0) And &H1)
    '            Case 2
    '                Inputdata = CBool(m_abytSL(0) And &H2)
    '            Case 3
    '                Inputdata = CBool(m_abytSL(0) And &H4)
    '            Case 4
    '                Inputdata = CBool(m_abytSL(0) And &H8)
    '            Case Else
    '                MsgBox("error, please check InputNo set")
    '                Inputdata = False
    '        End Select

    '        Return Inputdata

    '    Catch ex As Exception
    '        Throw New Exception("ReadInput()::" & ex.Message)
    '        Return False
    '    End Try
    'End Function

    Public Function ReadInput(ByVal InputNo As Integer) As Boolean Implements IUSB_VIB_CONTROLLER.ReadInput ' read single chanel
        Try
            Dim ret As Integer = 0
            Dim m_abytSL As Byte() = New Byte(0) {0}
            ret = m_objSeaMAX.SM_ReadDigitalInputs(InputNo - 1, 1, m_abytSL)
            Return CBool(CInt(m_abytSL(0)))
        Catch ex As Exception
            Throw New Exception("ReadInput()::" & ex.Message)
        End Try
    End Function

    Public Function ReadInputForChamberID() As Integer Implements IUSB_VIB_CONTROLLER.ReadInputForChamberID 'add 20190226  验证OK
        Try
            Dim ret As Integer = 0
            Dim m_abytSL As Byte() = New Byte(0) {0}
            ret = m_objSeaMAX.SM_ReadDigitalInputs(1, 7, m_abytSL)
            Return CInt(m_abytSL(0))
        Catch ex As Exception
            Throw New Exception("ReadInputForChamberID()::" & ex.Message)
        End Try
    End Function

End Class

