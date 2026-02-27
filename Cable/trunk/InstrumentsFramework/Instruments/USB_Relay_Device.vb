Imports System.Runtime.InteropServices

Class usb_relay_device
    Public DeviceList() As usb_relay_device_List
    Private _Handle As UInt32 = 0
    Private RelayStatus As UInt32 = 0
    Private CurrentDevice As usb_relay_device_info
    Enum usb_relay_device_type
        USB_RELAY_DEVICE_ONE_CHANNEL = 1
        USB_RELAY_DEVICE_TWO_CHANNEL = 2
        USB_RELAY_DEVICE_FOUR_CHANNEL = 4
        USB_RELAY_DEVICE_EIGHT_CHANNEL = 8
    End Enum
    Public Structure usb_relay_device_info
        Public serial_number As String
        Public device_path As Byte
        Public device_type As usb_relay_device_type
        Public next_device As IntPtr
    End Structure
    Public Structure usb_relay_device_List
        Public device_Ptr As IntPtr
        Public device_info As usb_relay_device_info
    End Structure
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_init() As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_enumerate() As IntPtr
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Sub usb_relay_device_free_enumerate(ByVal DevicePtr As IntPtr)
    End Sub
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_open(ByVal devicePtr As IntPtr) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_open_with_serial_number(ByVal serial_number As Char(), ByVal len As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Sub usb_relay_device_close(ByVal hHandle As UInt32)
    End Sub
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_open_one_relay_channel(ByVal hHandle As UInt32, ByVal index As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_close_one_relay_channel(ByVal hHandle As UInt32, ByVal index As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_open_all_relay_channel(ByVal hHandle As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_close_all_relay_channel(ByVal hHandle As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_exit() As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_get_status(ByVal hHandle As UInt32, ByRef status As UInt32) As UInt32
    End Function
    <DllImport("usb_relay_device.dll", CallingConvention:=CallingConvention.Cdecl)> _
    Public Shared Function usb_relay_device_set_serial(ByVal hHandle As UInt32, ByVal serial_number As String) As UInt32
    End Function

    Public Sub New()
        'Dim Status As Uint32 = usb_relay_init()
        DeviceList = Nothing
    End Sub

    'Protected Overrides Sub Finalize()
    '    Dim status As UInt32 = usb_relay_exit()
    '    MyBase.Finalize()
    'End Sub

    Public Function Open_By_SN(ByVal SerialNumber As String) As Boolean
        Dim Status As UInt32 = usb_relay_init()
        _Handle = usb_relay_device_open_with_serial_number(SerialNumber, SerialNumber.Length)
        If _Handle <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function Open_By_Device(ByVal Device As usb_relay_device_List) As Boolean
        Dim Status As UInt32 = usb_relay_init()
        _Handle = usb_relay_device_open(Device.device_Ptr)

        If _Handle <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ListDevice() As Boolean
        Dim Status As UInt32 = usb_relay_init()
        If DeviceList IsNot Nothing Then
            usb_relay_device_free_enumerate(DeviceList(0).device_Ptr)
        End If
        Dim tmpPtr As IntPtr = usb_relay_device_enumerate()
        Dim FirstDevice As IntPtr = tmpPtr
        Dim tmpDevice As usb_relay_device_info
        Dim nDevice As Integer = 0
        Do
            If tmpPtr = 0 Then Exit Do
            tmpDevice = CType(Marshal.PtrToStructure(tmpPtr, GetType(usb_relay_device_info)), usb_relay_device_info)

            tmpPtr = tmpDevice.next_device
            nDevice += 1
        Loop


        ReDim DeviceList(nDevice - 1)
        tmpPtr = FirstDevice

        For i As Integer = 0 To nDevice - 1
            tmpDevice = CType(Marshal.PtrToStructure(tmpPtr, GetType(usb_relay_device_info)), usb_relay_device_info)

            DeviceList(i).device_Ptr = tmpPtr
            DeviceList(i).device_info = tmpDevice

            tmpPtr = tmpDevice.next_device
        Next

        If DeviceList.Length <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Close()
        usb_relay_device_close(_Handle)
        Dim status As UInt32 = usb_relay_exit()
        FlushMemory()
    End Sub

    Public Function USB_Relay_OpenClose_One_Channel(ByVal Open As Boolean, Optional ByVal channel As Integer = 1) As Boolean
        Dim Status As UInt32
        If Open Then
            Status = usb_relay_device_open_one_relay_channel(_Handle, channel)
        Else
            Status = usb_relay_device_close_one_relay_channel(_Handle, channel)
        End If
        If Status = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function USB_Relay_OpenClose_All_Channel(ByVal Open As Boolean) As Boolean
        Dim Status As UInt32
        If Open Then
            Status = usb_relay_device_open_all_relay_channel(_Handle)
        Else
            Status = usb_relay_device_close_all_relay_channel(_Handle)
        End If
        If Status = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function USB_Relay_Status() As UInt32
        Dim Relay_Status As UInt32
        usb_relay_device_get_status(_Handle, Relay_Status)
        Return Relay_Status
    End Function

    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
    Public Sub FlushMemory()
        GC.Collect()
        GC.WaitForPendingFinalizers()

        If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
        End If
    End Sub
End Class
