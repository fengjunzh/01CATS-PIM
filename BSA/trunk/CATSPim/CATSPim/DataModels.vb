Public Class DataModels
	Public Class AlgorithmLimit
		'Public Shared Lambda As Single = 200
		'Public Shared ImdLL As Single = -175
		Public Lambda_CalcCompensation As Single '140
		Public Lambda_CalcLimit As Single  '-158
		Public Lambda_MaxLimit As Single '400
		Public Lambda_PFLimit As Single '200
		Public TwoTone_DiscardLimit As Single '-175
		Public Ctrn_AvgMaxLimit As Single '140
		Public Ctrn_CalcCoefficient As Single '0.3246
		Public Ctrn_CalcMaxLimit As Single '400
		Public TwoTone_FilterCoefficient As Single '6

	End Class
	Public Class PowerLoss
		Public TxL As Single = 0
		Public TxR As Single = 0
		Public Rx As Single = 0
	End Class
	Public Class FrequencyGroup
		Public Sweep As New Dictionary(Of Short, CATS.Model.cfg_imd_sfbox)
		Public Fixed As New Dictionary(Of Short, CATS.Model.cfg_imd_ffbox)
		Public Custom As New Dictionary(Of Short, CATS.Model.cfg_imd_cfbox)
	End Class
	Public Class RTimePara
		Public barcode As String
		Public product_mode_id As Integer
		Public product_mode As String

		'Public MiiOnline As Boolean
		Public M_product_main As CATS.Model.product_main
		'Public product_main_id As Integer
		'Public production As String

		Public phase_main_id As Integer
		Public phase As String

		Public spec_main_id As Integer
		Public group_main_id As Integer
		'Public factory As String = "ASZ"

		Public meas_start_time As DateTime
		Public meas_stop_time As DateTime
		Public conn_time As Integer
		Public meas_time As Integer
		Public total_time As Integer
		Public setup_time As Integer
		Public meas_status As String

		Public MeasWatch As New Stopwatch
		Public ConnWatch As New Stopwatch

		Public AlgoParas As New AlgorithmLimit

		Public RetMaps As New List(Of CATS.Model.product_ret_map)

		Public instr_vendor_id As Integer

		Public M_phase_station_main As New CATS.Model.phase_station_main

		'Public cable_ser_no As String
		'Public cable_consume_count As Integer


		'Public test_groups_items As New Dictionary(Of Short, DataModels.TestGroupPara)
	End Class

	'Public Class TestGroupPara
	'  Public Group_name As String
	'  Public group_main_id As Integer
	'  Public group_id As Integer
	'  Public item_details As New List(Of CATS.Model.cq_spec_imd_details)
	'End Class
	Public Class FrequencyPoint
		Public SeriesId As Short
		Public XData As Single
		Public YData As Single
		Public TxlFreq As Single
		Public TxrFreq As Single
		Public RxFreq As Single
		Public Descr As String
	End Class
	Public Class TestTrace

		Public Sweep As New List(Of FrequencyPoint)
		Public SweepDown As New List(Of FrequencyPoint)
		Public SweepUp As New List(Of FrequencyPoint)
		Public TwoTone As New List(Of FrequencyPoint)
		Public TwoToneFilter As New List(Of FrequencyPoint)
		Public Lambda As New List(Of PointF)

		Public StsA As New Dictionary(Of String, List(Of FrequencyPoint))
		'Dim sweep_freq_down_fit As List(Of FrequencyPoint)
		'Dim sweep_freq_up_fit As List(Of FrequencyPoint)
		'Dim sweep_freq_fit As List(Of FrequencyPoint)

		'Dim time_sweep_point_fit As List(Of FrequencyPoint)
	End Class
	'Public Class TestResult
	'  Dim TestItem As String
	'  Dim meas As Single
	'  Dim ll As Single
	'  Dim ul As Single
	'End Class

	Public Class RetDevice
		Public RetSn As String
		Public AntennaSn As String
		Public RetModel As String
		Public AntModel As String
		Public HwVersion As String
		Public FwVersion As String
		'Public CalAddrId As Integer
		'Public CalSubAddrId As Integer
		Public Tilt As AisgDevice.Antenna
		Public Type As AisgDevice.DeviceType
		Public ErrorInfo As String = ""
	End Class
	Public Class Instrument
		Public Model As String
		Public SerialNumber As String
		Public Hardware As String
		Public Firmware As String
		Public Idn As String
	End Class
	Public Class AxisMaxMin
		Public Min As Single
		Public Max As Single
	End Class
	Public Enum TestMode
		Test = 0
		Debug = 1
	End Enum
	Public Enum TestStatus
		Normal = 1
		Abort = 2
		Err = 3
	End Enum

End Class
Public Class AbortedException
	Inherits ApplicationException

	Public Sub New()
		MyBase.New("TestAbored")
	End Sub
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub
End Class
Public Class FailedException
	Inherits ApplicationException
	Public Sub New()
		MyBase.New("TestFailed")
	End Sub
	Public Sub New(message As String)
		MyBase.New(message)
	End Sub
End Class
