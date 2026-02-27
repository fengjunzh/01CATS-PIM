Imports System.Threading
Public Class SemporeRet
	Private m_SignalCount As Integer
	Private m_SignalValue As Integer
	Private m_SignalLoop As Integer
	Public Property SignalCount As Integer
		Get
			Return m_SignalCount
		End Get
		Set(value As Integer)
			Interlocked.Exchange(m_SignalCount, value)
		End Set
	End Property
	Public Property SignalValue As Integer
		Get
			Return m_SignalValue
		End Get
		Set(value As Integer)
			Interlocked.Exchange(m_SignalValue, value)
		End Set
	End Property
	Public Property SignalLoopValue As Integer
		Get
			Return m_SignalLoop
		End Get
		Set(value As Integer)
			Interlocked.Exchange(m_SignalLoop, value)
		End Set
	End Property
	Public Function SignalCountIncrementOne() As Integer
		Try
			Interlocked.Increment(m_SignalCount)
			Return m_SignalCount

		Catch ex As Exception
			Throw New Exception("SemporeRet.SignalCountIncrementOne()::" & ex.Message)
		End Try

	End Function
	Public Function SignalCountDecrementOne() As Integer
		Try
			Interlocked.Decrement(m_SignalCount)
			Return m_SignalCount

		Catch ex As Exception
			Throw New Exception("SemporeRet.SignalCountDecrementOne()::" & ex.Message)
		End Try
	End Function
	Public Function IncrementOne() As Integer
		Try
			Interlocked.Increment(m_SignalValue)
			Return m_SignalValue

		Catch ex As Exception
			Throw New Exception("SemporeRet.IncrementOne()::" & ex.Message)
		End Try

	End Function
	Public Function DecrementOne() As Integer
		Try
			Interlocked.Decrement(m_SignalValue)
			Return m_SignalValue

		Catch ex As Exception
			Throw New Exception("SemporeRet.DecrementOne()::" & ex.Message)
		End Try
	End Function
	Public Function IsPrepareSetTilt() As Boolean
		Try

			Return Interlocked.Equals(m_SignalValue, m_SignalCount)

		Catch ex As Exception
			Throw New Exception("SemporeRet.IsPrepareSetTilt()::" & ex.Message)
		End Try

	End Function
	Public Function IsRetThreadExit() As Boolean
		Try

			Return IIf(m_SignalLoop = 1, False, True)

		Catch ex As Exception
			Throw New Exception("SemporeRet.IsRetThreadExit()::" & ex.Message)
		End Try
	End Function

End Class

