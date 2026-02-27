Public Class TestCable
	Private _CableSN As String
	Private _ConsumedNum As Integer '已使用的数量
	Private _ConnectionNum As Integer '本次测试连接次数
	Private _TolerantNum As Integer

	Public Sub New(CableSN As String)
		Try
			_CableSN = CableSN

			If _CableSN = "" Then
				_TolerantNum = 500
				MsgBox("The #SN of test cable is empty, please input it in Menu 'Tools->DeviceSetup'!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly)
				Return
			End If

			Dim tcmBll As New CATS.BLL.test_cable_mainManager
			Dim tcmM As CATS.Model.test_cable_main

			tcmM = tcmBll.SelectByCableSN(CableSN)
			If tcmM Is Nothing Then
				_TolerantNum = 500
				_ConsumedNum = 0
			Else
				_ConsumedNum = tcmM.test_count

				Dim tcmdBll As New CATS.BLL.test_cable_modelManager
				Dim tcmdM As CATS.Model.test_cable_model

				tcmdM = tcmdBll.SelectById(tcmM.cable_model_id)
				If tcmdM Is Nothing Then
					_TolerantNum = 500
				Else
					_TolerantNum = tcmdM.tolerant_count
				End If
			End If

		Catch ex As Exception
			Throw New Exception("TestCable.New()::" & ex.Message)
		End Try

	End Sub
	Public Property CableSN As String
		Get
			Return _CableSN
		End Get
		Set(value As String)
			_CableSN = value
		End Set
	End Property
	Public Property TolerantNum As Integer
		Get
			Return _TolerantNum
		End Get
		Set(value As Integer)
			_TolerantNum = value
		End Set
	End Property
	Public Function IsAvaliable() As Boolean
		Try
			If _CableSN = "" Then Return True

			If _ConsumedNum >= _TolerantNum Then Return False

			Return True

		Catch ex As Exception
			Throw New Exception("TestCable.IsAvaliable()::" & ex.Message)
		End Try
	End Function
	Public Sub AddConsumedNum(value As Integer)
		Try
			_ConsumedNum += value
			_ConnectionNum += value

		Catch ex As Exception
			Throw New Exception("TestCable.AddConsumedNum()::" & ex.Message)
		End Try
	End Sub
	Public Sub StoreConsumedNum(phase_main_id As Integer, product_serial_num As String)
		Try
			If _CableSN = "" Then Return

			Dim tcmBll As New CATS.BLL.test_cable_mainManager
			Dim tcmM As CATS.Model.test_cable_main
			Dim test_cable_main_id As Integer

			tcmM = tcmBll.SelectByCableSN(CableSN)
			If tcmM Is Nothing Then
				'add
				tcmM = New CATS.Model.test_cable_main
				With tcmM
					.cable_model_id = 1
					.cable_serial_num = CableSN
					.test_count = _ConsumedNum
					.register_date_time = Now
				End With
				test_cable_main_id = tcmBll.AddReturnId(tcmM)
			Else
				'update
				tcmM.test_count = _ConsumedNum
				test_cable_main_id = tcmM.id
				tcmBll.Update(tcmM)
			End If

			Dim tcdBll As New CATS.BLL.test_cable_detailManager
			Dim tcdM As New CATS.Model.test_cable_detail

			With tcdM
				.product_serial_num = product_serial_num
				.phase_main_id = phase_main_id
				.test_cable_main_id = test_cable_main_id
				.date_time = Now
				.test_count = _ConnectionNum
				.controller_name = My.Computer.Name
				.login_name = Environment.UserName.ToString.ToUpper
				.factory = pFactory  'GUI.Factory '"ASZ"
				tcdBll.Add(tcdM)
			End With

		Catch ex As Exception
			Throw New Exception("TestCable.StoreConsumedNum()::" & ex.Message)
		End Try
	End Sub
End Class
