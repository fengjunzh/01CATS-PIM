Imports System
Imports System.Data
Imports System.Text
Imports System.Data.SqlClient

Partial Public Class spec_detailService
	Public Function Add(model As FACTS.Model.spec_detail) As Boolean
		Try
			DateTime.Compare(model.validation_date, Date.Parse("2016"))

			Dim rowsAffected As Integer
			Dim parameters As SqlParameter() = {
		  New SqlParameter("@group_main_id", model.group_main_id),
		  New SqlParameter("@order_idx", model.order_idx),
		  New SqlParameter("@meas_item", model.meas_item),
		  New SqlParameter("@limit_low", model.limit_low),
		  New SqlParameter("@limit_up", model.limit_up),
		  New SqlParameter("@limit_string", model.limit_string),
		  New SqlParameter("@limit_unit", model.limit_unit),
		  New SqlParameter("@cust_limit_low", model.cust_limit_low),
		  New SqlParameter("@cust_limit_up", model.cust_limit_up),
		  New SqlParameter("@cust_limit_unit", model.cust_limit_unit),
		  New SqlParameter("@cust_limit_string", model.cust_limit_string),
		  New SqlParameter("@sub_unit", model.Sub_unit),
		  New SqlParameter("@fiber", model.Fiber),
		  New SqlParameter("@wave_length", model.Wave_length),
		  New SqlParameter("@meas_required", model.meas_required),
		  New SqlParameter("@script", model.script),
		  New SqlParameter("@message", model.message),
		  New SqlParameter("@validity", model.validity),
		  New SqlParameter("@validation_date", model.validation_date),
		  New SqlParameter("@invalidation_date", model.invalidation_date),
		  New SqlParameter("@gen1", model.gen1),
		  New SqlParameter("@gen2", model.gen2),
		  New SqlParameter("@gen3", model.gen3)
		}
			rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_detail_Add", parameters)
			Return IIf(rowsAffected > 0, True, False)
		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.Add()::" & ex.Message)
		End Try
	End Function
	Public Function AddReturnId(model As FACTS.Model.spec_detail) As Integer
		Try
			Dim rowsAffected As Integer
			Dim parameters As SqlParameter() = {
		New SqlParameter("@group_main_id", model.group_main_id),
		New SqlParameter("@order_idx", model.order_idx),
		New SqlParameter("@meas_item", model.meas_item),
		New SqlParameter("@limit_low", model.limit_low),
		New SqlParameter("@limit_up", model.limit_up),
		New SqlParameter("@limit_string", model.limit_string),
		New SqlParameter("@limit_unit", model.limit_unit),
		New SqlParameter("@cust_limit_low", model.cust_limit_low),
		New SqlParameter("@cust_limit_up", model.cust_limit_up),
		New SqlParameter("@cust_limit_unit", model.cust_limit_unit),
		New SqlParameter("@cust_limit_string", model.cust_limit_string),
		New SqlParameter("@sub_unit", model.Sub_unit),
		New SqlParameter("@fiber", model.Fiber),
		New SqlParameter("@wave_length", model.Wave_length),
		New SqlParameter("@meas_required", model.meas_required),
		New SqlParameter("@script", model.script),
		New SqlParameter("@message", model.message),
		New SqlParameter("@validity", model.validity),
		New SqlParameter("@validation_date", model.validation_date),
		New SqlParameter("@invalidation_date", model.invalidation_date),
		New SqlParameter("@gen1", model.gen1),
		New SqlParameter("@gen2", model.gen2),
		New SqlParameter("@gen3", model.gen3)
	  }
			Return SqlHelper.runProcedureWithReturnValue("proc_spec_detail_Add", parameters, rowsAffected)
		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.AddReturnId()::" & ex.Message)
		End Try
	End Function
	Public Function Delete(id As Integer) As Boolean
		Try
			Dim rowsAffected As Integer
			Dim parameters As SqlParameter() = {New SqlParameter("@id", id)}

			rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_detail_DeleteRow", parameters)
			Return IIf(rowsAffected > 0, True, False)

		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.Delete()::" & ex.Message)
		End Try
	End Function
	Public Function Update(model As FACTS.Model.spec_detail) As Boolean
		Try
			Dim rowsAffected As Integer
			Dim parameters As SqlParameter() = {
		New SqlParameter("@id", model.id),
		New SqlParameter("@group_main_id", model.group_main_id),
		New SqlParameter("@order_idx", model.order_idx),
		New SqlParameter("@meas_item", model.meas_item),
		New SqlParameter("@limit_low", model.limit_low),
		New SqlParameter("@limit_up", model.limit_up),
		New SqlParameter("@limit_string", model.limit_string),
		New SqlParameter("@limit_unit", model.limit_unit),
		New SqlParameter("@cust_limit_low", model.cust_limit_low),
		New SqlParameter("@cust_limit_up", model.cust_limit_up),
		New SqlParameter("@cust_limit_unit", model.cust_limit_unit),
		New SqlParameter("@cust_limit_string", model.cust_limit_string),
		New SqlParameter("@fiber", model.Fiber),
		New SqlParameter("@wave_length", model.Wave_length),
		New SqlParameter("@meas_required", model.meas_required),
		New SqlParameter("@meas_required", model.meas_required),
		New SqlParameter("@script", model.script),
		New SqlParameter("@message", model.message),
		New SqlParameter("@validity", model.validity),
		New SqlParameter("@validation_date", model.validation_date),
		New SqlParameter("@invalidation_date", model.invalidation_date),
		New SqlParameter("@gen1", model.gen1),
		New SqlParameter("@gen2", model.gen2),
		New SqlParameter("@gen3", model.gen3)
	  }

			rowsAffected = SqlHelper.runProcedureWithAffectedRow("proc_spec_detail_Update", parameters)
			Return IIf(rowsAffected > 0, True, False)
		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.Update()::" & ex.Message)
		End Try
	End Function
	Public Function SelectAll() As List(Of FACTS.Model.spec_detail)
		Try
			Dim dt As DataTable
			Dim model As FACTS.Model.spec_detail
			Dim mlist As New List(Of FACTS.Model.spec_detail)

			Dim parameters As SqlParameter() = {}
			dt = SqlHelper.runProcedureWithDataset("proc_spec_detail_SelectAll", parameters, "dt").Tables("dt")

			If dt.Rows.Count = 0 Then Return Nothing

			For Each row As DataRow In dt.Rows
				model = New FACTS.Model.spec_detail
				model = DataRowToModel(row)
				mlist.Add(model)
			Next

			Return mlist

		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.SelectAll()::" & ex.Message)
		End Try
	End Function
	Public Function SelectById(id As Integer) As FACTS.Model.spec_detail
		Try
			Dim dt As DataTable
			Dim model As New FACTS.Model.spec_detail
			Dim parameters As SqlParameter() = {
									New SqlParameter("@id", id)}

			dt = SqlHelper.runProcedureWithDataset("proc_spec_detail_SelectRow", parameters, "dt").Tables("dt")
			If dt.Rows.Count = 0 Then Return Nothing

			model = DataRowToModel(dt.Rows(0))
			Return model

		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.SelectById()::" & ex.Message)
		End Try
	End Function
	Public Function SelectAllByGroupMainId(group_main_id As Integer) As List(Of Model.spec_detail)
		Try
			Dim dt As DataTable
			Dim resp As New List(Of Model.spec_detail)
			Dim respItem As Model.spec_detail

			dt = SelectByWhere("group_main_id =" & group_main_id).Tables("dt")

			If dt.Rows.Count = 0 Then Return Nothing

			For Each row In dt.Rows
				respItem = New Model.spec_detail
				respItem = DataRowToModel(row)
				resp.Add(respItem)
			Next

			Return resp

		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.SelectAllByGroupMainId()::" & ex.Message)
		End Try
	End Function
	Public Function SelectByWhere(whereString As String) As DataSet
		Try
			Dim ds As DataSet

			Dim parameters As SqlParameter() = {
				  New SqlParameter("@where", whereString)}

			ds = SqlHelper.runProcedureWithDataset("proc_spec_detail_SelectByWhere", parameters, "dt")

			Return ds

		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.SelectByWhere()::" & ex.Message)
		End Try
	End Function
	Public Shared Function DataRowToModel(row As DataRow) As FACTS.Model.spec_detail
		Try
			Dim model As New FACTS.Model.spec_detail
			If IsNothing(row) Then Return Nothing
			If row.Table.Columns.Contains("id") Then If Not (IsDBNull(row("id"))) Then model.id = Convert.ToInt32(row("id"))
			If row.Table.Columns.Contains("group_main_id") Then If Not (IsDBNull(row("group_main_id"))) Then model.group_main_id = Convert.ToInt32(row("group_main_id"))
			If row.Table.Columns.Contains("order_idx") Then If Not (IsDBNull(row("order_idx"))) Then model.order_idx = Convert.ToInt32(row("order_idx"))
			If row.Table.Columns.Contains("meas_item") Then If Not (IsDBNull(row("meas_item"))) Then model.meas_item = Convert.ToString(row("meas_item"))
			If row.Table.Columns.Contains("limit_low") Then If Not (IsDBNull(row("limit_low"))) Then model.limit_low = Convert.ToDecimal(row("limit_low"))
			If row.Table.Columns.Contains("limit_up") Then If Not (IsDBNull(row("limit_up"))) Then model.limit_up = Convert.ToDecimal(row("limit_up"))
			If row.Table.Columns.Contains("limit_string") Then If Not (IsDBNull(row("limit_string"))) Then model.limit_string = Convert.ToString(row("limit_string"))
			If row.Table.Columns.Contains("limit_unit") Then If Not (IsDBNull(row("limit_unit"))) Then model.limit_unit = Convert.ToString(row("limit_unit"))
			If row.Table.Columns.Contains("cust_limit_low") Then If Not (IsDBNull(row("cust_limit_low"))) Then model.cust_limit_low = Convert.ToDecimal(row("cust_limit_low"))
			If row.Table.Columns.Contains("cust_limit_up") Then If Not (IsDBNull(row("cust_limit_up"))) Then model.cust_limit_up = Convert.ToDecimal(row("cust_limit_up"))
			If row.Table.Columns.Contains("cust_limit_unit") Then If Not (IsDBNull(row("cust_limit_unit"))) Then model.cust_limit_unit = Convert.ToString(row("cust_limit_unit"))
			If row.Table.Columns.Contains("cust_limit_string") Then If Not (IsDBNull(row("cust_limit_string"))) Then model.cust_limit_string = Convert.ToString(row("cust_limit_string"))
			If row.Table.Columns.Contains("sub_unit") Then If Not (IsDBNull(row("sub_unit"))) Then model.Sub_unit = Convert.ToInt16(row("sub_unit"))
			If row.Table.Columns.Contains("fiber") Then If Not (IsDBNull(row("fiber"))) Then model.Fiber = Convert.ToInt16(row("fiber"))
			If row.Table.Columns.Contains("wave_length") Then If Not (IsDBNull(row("wave_length"))) Then model.Wave_length = Convert.ToInt16(row("wave_length"))
			If row.Table.Columns.Contains("meas_required") Then If Not (IsDBNull(row("meas_required"))) Then model.meas_required = Convert.ToBoolean(row("meas_required"))
			If row.Table.Columns.Contains("script") Then If Not (IsDBNull(row("script"))) Then model.script = Convert.ToString(row("script"))
			If row.Table.Columns.Contains("message") Then If Not (IsDBNull(row("message"))) Then model.message = Convert.ToString(row("message"))
			If row.Table.Columns.Contains("validity") Then If Not (IsDBNull(row("validity"))) Then model.validity = Convert.ToBoolean(row("validity"))
			If row.Table.Columns.Contains("validation_date") Then If Not (IsDBNull(row("validation_date"))) Then model.validation_date = Convert.ToDateTime(row("validation_date"))
			If row.Table.Columns.Contains("invalidation_date") Then If Not (IsDBNull(row("invalidation_date"))) Then model.invalidation_date = Convert.ToDateTime(row("invalidation_date"))
			If row.Table.Columns.Contains("gen1") Then If Not (IsDBNull(row("gen1"))) Then model.gen1 = Convert.ToString(row("gen1"))
			If row.Table.Columns.Contains("gen2") Then If Not (IsDBNull(row("gen2"))) Then model.gen2 = Convert.ToString(row("gen2"))
			If row.Table.Columns.Contains("gen3") Then If Not (IsDBNull(row("gen3"))) Then model.gen3 = Convert.ToString(row("gen3"))
			Return model
		Catch ex As Exception
			Throw New Exception("spec_detail.DAL.DataRowToModel()::" & ex.Message)
		End Try
	End Function
End Class
