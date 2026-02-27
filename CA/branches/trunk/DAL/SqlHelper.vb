Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.Common
Imports System.Collections.Generic
Public Module SqlHelper
	'Private _connString As String '= "Data Source=asz-42jc23x;Initial Catalog=CATS;User ID=sa;Password=Asc4tore"
	'Private _conn As New SqlConnection(_connString)
	Private _conn As SqlConnection
	'Public Sub ActivateConnection()
	'	Try
	'		_connString = My.Settings.connString.ToString.Trim
	'		If _connString = "" Then Throw New Exception("Not found connString.")
	'		_conn = New SqlConnection(_connString)

	'	Catch ex As Exception
	'		Throw New Exception("ActiveConnection()::" & ex.Message)
	'	End Try
	'End Sub
	Public Sub ActivateConnection(connString As String)
		Try
			_conn = New SqlConnection(connString)
		Catch ex As Exception
			Throw New Exception("ActiveConnection(string)::" & ex.Message)
		End Try
	End Sub
	Private Function buildQueryCommand(ByVal connection As SqlConnection, ByVal storedProcName As String, ByVal parameters() As IDataParameter) As SqlCommand
		Dim command As SqlCommand = New SqlCommand(storedProcName, connection)
		command.CommandType = CommandType.StoredProcedure
		Dim parameter As SqlParameter
		Try
			For Each parameter In parameters
				If Not parameter Is Nothing Then
					If (parameter.Direction = ParameterDirection.InputOutput Or parameter.Direction = ParameterDirection.Input) And IsNothing(parameter.Value) Then
						parameter.Value = DBNull.Value
					End If
					command.Parameters.Add(parameter)
				End If
			Next
		Catch ex As Exception
			Throw New Exception("BuildQueryCommand()::" & ex.Message)
		End Try

		Return command
	End Function
	Private Function buildIntCommand(ByVal connection As SqlConnection, ByVal storedProcName As String, ByVal parameters() As IDataParameter) As SqlCommand
		Try
			Dim command As SqlCommand = buildQueryCommand(connection, storedProcName, parameters)
			command.Parameters.Add(New SqlParameter("ReturnValue",
				SqlDbType.Int, 4, ParameterDirection.ReturnValue,
				False, 0, 0, String.Empty, DataRowVersion.Default, Nothing))
			Return command
		Catch ex As Exception
			Throw New Exception("BuildIntCommand()::" & ex.Message)
		End Try
	End Function
	Private Sub openConn()
		Try
			If _conn.State <> ConnectionState.Open Then _conn.Open()
		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("openConn()::" & ex.Message)
		End Try
	End Sub
	Private Sub closeConn()
		Try
			If _conn.State = ConnectionState.Open Then _conn.Close()
		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("closeConn()::" & ex.Message)
		End Try
	End Sub
	Public Function execNonQuery(ByVal sql As String) As Integer
		Dim cmd As SqlCommand
		Dim rows As Integer

		Try
			cmd = New SqlCommand(sql, _conn)
			openConn()
			rows = cmd.ExecuteNonQuery()
			cmd.Dispose()
			closeConn()

		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("ExecNonQuery()::" & ex.Message)
		End Try

		Return rows

	End Function
	Public Function execNonQuery(ByVal sql As String, ByVal timeOut As Integer) As Integer
		Dim cmd As SqlCommand
		Dim rows As Integer

		Try
			cmd = New SqlCommand(sql, _conn)
			openConn()
			cmd.CommandTimeout = timeOut
			rows = cmd.ExecuteNonQuery()
			cmd.Dispose()
			closeConn()

		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("ExecNonQuery()::" & ex.Message)
		End Try

		Return rows
	End Function
	Public Function execQuery(ByVal sql As String) As DataSet
		Dim da As SqlDataAdapter
		Dim ds As DataSet = New DataSet

		Try
			da = New SqlDataAdapter(sql, _conn)
			da.Fill(ds, "dt")

		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("ExecQuery()::" & ex.Message)
		End Try

		Return ds

	End Function
	Public Function execQuery(ByVal sql As String, ByVal timeOut As Integer) As DataSet
		Dim da As SqlDataAdapter
		Dim ds As DataSet = New DataSet

		Try
			da = New SqlDataAdapter(sql, _conn)
			da.SelectCommand.CommandTimeout = timeOut
			da.Fill(ds, "dt")
		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("ExecQuery()::" & ex.Message)
		End Try

		Return ds

	End Function
	Public Function runProcedureWithAffectedRow(ByVal storedProcName As String, ByVal parameters() As IDataParameter) As Integer
		Dim rowsAffected As Integer
		Dim cmd As SqlCommand

		Try
			openConn()
			cmd = buildQueryCommand(_conn, storedProcName, parameters)
			rowsAffected = cmd.ExecuteNonQuery()
			closeConn()
		Catch ex As System.Data.SqlClient.SqlException
			closeConn()
			Throw New Exception("RunProcedure()::" & ex.Message)
		End Try

		Return rowsAffected

	End Function
	'Public Function runProcedureWithReader(ByVal storedProcName As String, ByVal parameters() As IDataParameter) As SqlDataReader
	'  Dim reader As SqlDataReader
	'  Dim cmd As SqlCommand

	'  Try
	'    cmd = buildQueryCommand(_conn, storedProcName, parameters)
	'    openConn()
	'    reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
	'    'closeConn()
	'  Catch ex As System.Data.SqlClient.SqlException
	'    'closeConn()
	'    Throw New Exception("RunProcedure()::" & ex.Message)
	'  End Try

	'  Return reader

	'End Function
	Public Function runProcedureWithDataset(ByVal storedProcName As String, ByVal parameters() As IDataParameter, ByVal tableName As String) As DataSet
		Dim ds As DataSet = New DataSet()
		Dim da As SqlDataAdapter = New SqlDataAdapter()

		Try

			da.SelectCommand = buildQueryCommand(_conn, storedProcName, parameters)
			da.Fill(ds, tableName)

		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("RunProcedure()::" & ex.Message)
		End Try

		Return ds

	End Function
	Public Function runProcedureWithDataset(ByVal storedProcName As String, ByVal parameters() As IDataParameter, ByVal tableName As String, ByVal timeOut As Integer) As DataSet
		Dim ds As DataSet = New DataSet()
		Dim da As SqlDataAdapter = New SqlDataAdapter()

		Try

			da.SelectCommand = buildQueryCommand(_conn, storedProcName, parameters)
			da.SelectCommand.CommandTimeout = timeOut
			da.Fill(ds, tableName)

		Catch ex As System.Data.SqlClient.SqlException
			Throw New Exception("RunProcedure()::" & ex.Message)
		End Try
		Return ds
	End Function
	Public Function runProcedureWithReturnValue(ByVal storedProcName As String, ByVal parameters() As IDataParameter, ByRef rowsAffected As Integer) As Integer
		Dim rtn As Integer

		Try

			Dim cmd As SqlCommand = buildIntCommand(_conn, storedProcName, parameters)
			openConn()
			rowsAffected = cmd.ExecuteNonQuery()
			rtn = CType(cmd.Parameters("ReturnValue").Value, Integer)
			closeConn()
		Catch ex As Exception
			closeConn()
			Throw New Exception("RunProcedure()::" & ex.Message)
		End Try

		Return rtn
	End Function

End Module



