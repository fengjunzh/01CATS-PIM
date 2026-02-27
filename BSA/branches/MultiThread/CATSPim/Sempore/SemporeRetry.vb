Imports System.Threading
Public Class SemporeRetry
	Private m_SignalCount As Integer
	Private m_SignalValue As Integer
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
	Public Function SignalCountIncrementOne() As Integer
		Try
			m_SignalCount = Interlocked.Increment(m_SignalCount)
			Return m_SignalCount

		Catch ex As Exception
			Throw New Exception("SemporeRetry.SignalCountIncrementOne()::" & ex.Message)
		End Try

	End Function
	Public Function SignalCountDecrementOne() As Integer
		Try
			m_SignalCount = Interlocked.Decrement(m_SignalCount)
			Return m_SignalCount

		Catch ex As Exception
			Throw New Exception("SemporeRetry.SignalCountDecrementOne()::" & ex.Message)
		End Try
	End Function
	Public Function IncrementOne() As Integer
		Try
			m_SignalValue = Interlocked.Increment(m_SignalValue)
			Return m_SignalValue

		Catch ex As Exception
			Throw New Exception("SemporeRetry.IncrementOne()::" & ex.Message)
		End Try

	End Function
	Public Function DecrementOne() As Integer
		Try
			m_SignalValue = Interlocked.Decrement(m_SignalValue)
			Return m_SignalValue

		Catch ex As Exception
			Throw New Exception("SemporeRetry.DecrementOne()::" & ex.Message)
		End Try
	End Function
	Public Function PromptRetryMessage() As Boolean
		Try

			Return IIf(m_SignalValue = m_SignalCount, True, False)

		Catch ex As Exception
			Throw New Exception("SemporeRetry.IsPromptRetryMessage()::" & ex.Message)
		End Try
	End Function
End Class
