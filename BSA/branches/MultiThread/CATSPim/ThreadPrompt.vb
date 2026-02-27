Public Class ThreadPrompt
	Public Class PromptMessageModel
		Public PhaseName As String
		Public PortFrom As String
		Public PortTo As String
		Public Message As String
	End Class
	Public m_MTPromptQueue As New List(Of PromptMessageModel)
	Public m_MTRetryPromptQueue As New List(Of PromptMessageModel)

	Public Sub AddPromptItem(item As PromptMessageModel)
		Try
			SyncLock (m_MTPromptQueue)
				m_MTPromptQueue.Add(item)
			End SyncLock
		Catch ex As Exception
			Throw New Exception("AddPromptItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub AddRetryPromptItem(item As PromptMessageModel)
		Try
			SyncLock (m_MTRetryPromptQueue)
				m_MTRetryPromptQueue.Add(item)
			End SyncLock
		Catch ex As Exception
			Throw New Exception("AddRetryPromptItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub RemovePromptItem(item As PromptMessageModel)
		Try
			SyncLock (m_MTPromptQueue)

				Dim resp As New PromptMessageModel

				If m_MTPromptQueue.Contains(item) Then
					m_MTPromptQueue.Remove(item)
				Else
					Throw New Exception("Not found Prompt port_from =" & item.PortFrom & ", port_to=" & item.PortTo & ", message=" & item.Message)
				End If

			End SyncLock
		Catch ex As Exception
			Throw New Exception("RemovePromptItem()::" & ex.Message)
		End Try
	End Sub

	Public Sub RemoveRetryPromptItem(item As PromptMessageModel)
		Try
			SyncLock (m_MTRetryPromptQueue)

				Dim resp As New PromptMessageModel

				If m_MTRetryPromptQueue.Contains(item) Then
					m_MTRetryPromptQueue.Remove(item)
				Else
					Throw New Exception("Not found Prompt port_from =" & item.PortFrom & ", port_to=" & item.PortTo & ", message=" & item.Message)
				End If

			End SyncLock
		Catch ex As Exception
			Throw New Exception("RemoveRetryPromptItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub ClearPromptItem()
		Try
			SyncLock (m_MTPromptQueue)

				m_MTPromptQueue.Clear()

			End SyncLock
		Catch ex As Exception
			Throw New Exception("ClearPromptItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub ClearRetryPromptItem()
		Try
			SyncLock (m_MTRetryPromptQueue)

				m_MTRetryPromptQueue.Clear()

			End SyncLock
		Catch ex As Exception
			Throw New Exception("ClearRetryPromptItem()::" & ex.Message)
		End Try
	End Sub
	Public Sub ShowPromptMessage()
		Try
			SyncLock (m_MTPromptQueue)
				Dim item As PromptMessageModel

				Dim msg As String = "Please connect " & vbCrLf
				For Each item In m_MTPromptQueue
					msg += item.PhaseName & " output cable to " & item.PortTo & vbCrLf
				Next

				If MsgBox(msg, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
					TestModules.SetSemporeWhenEndTest()
				End If
				ClearPromptItem()
			End SyncLock
		Catch ex As Exception
			Throw New Exception("ShowPromptMessage()::" & ex.Message)
		End Try
	End Sub
	Public Sub ShowRetryPromptMessage()
		Try
			SyncLock (m_MTRetryPromptQueue)
				Dim item As PromptMessageModel

				Dim msg As String = "Please connect " & vbCrLf
				For Each item In m_MTRetryPromptQueue
					msg += item.PhaseName & " output cable to " & item.PortTo & vbCrLf
				Next

				If MsgBox(msg, MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
					TestModules.SetSemporeWhenTestExit()
				End If
				ClearRetryPromptItem()
			End SyncLock
		Catch ex As Exception
			Throw New Exception("ShowRetryPromptMessage()::" & ex.Message)
		End Try
	End Sub
End Class
