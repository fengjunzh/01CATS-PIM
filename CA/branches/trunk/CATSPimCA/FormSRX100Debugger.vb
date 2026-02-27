Imports Keyence.AutoID.SDK

Public Class FormSRX100Debugger
    Dim m_reader As ReaderAccessor = New ReaderAccessor()
    Dim m_searcher As ReaderSearcher = New ReaderSearcher()
    Dim m_nicList As List(Of NicSearchResult) = New List(Of NicSearchResult)()
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        m_nicList = m_searcher.ListUpNic()
        If IsNothing(m_nicList) = False Then
            For i = 0 To m_nicList.Count - 1
                NICcomboBox.Items.Add(m_nicList(i).NicIpAddr)
            Next i
        End If
        NICcomboBox.SelectedIndex = 0
    End Sub
    Private Sub SchBtn_Click(sender As Object, e As EventArgs) Handles SchBtn.Click
        'm_searcher.IsSearching is true while searching readers.
        If m_searcher.IsSearching = False Then
            m_searcher.SelectedNicSearchResult = m_nicList(NICcomboBox.SelectedIndex)
            NICcomboBox.Enabled = False
            SchBtn.Enabled = False
            SctBtn.Enabled = False
            comboBox1.Items.Clear()
            'Start searching readers.
            m_searcher.Start(AddressOf ReaderDetectedAction)
        End If
    End Sub

    Private Sub ReaderDetectedAction(res As ReaderSearchResult)
        'Define searched actions here.Defined actions work asynchronously.
        '"SearchListUp" works when a reader was searched.
        BeginInvoke(New delegateUserControl(AddressOf SearchListUp), res.IpAddress)
    End Sub

    Private Sub SctBtn_CheckedChanged(sender As Object, e As EventArgs) Handles SctBtn.CheckedChanged
        If SctBtn.Checked Then
            If comboBox1.SelectedItem <> vbNullString Then
                'Stop liveview.
                liveviewForm1.EndReceive()
                'Set ip address of liveview.
                liveviewForm1.IpAddress = comboBox1.SelectedItem.ToString()
                'Start liveview.
                liveviewForm1.BeginReceive()

                'Set ip address of ReaderAccessor.
                m_reader.IpAddress = comboBox1.SelectedItem.ToString()
                'Connect TCP/IP.
                m_reader.Connect(AddressOf ReceivedDataAction)
                NICcomboBox.Enabled = False
                SchBtn.Enabled = False
                comboBox1.Enabled = False
                TgrBtn.Enabled = True
            End If
        Else
            NICcomboBox.Enabled = True
            SchBtn.Enabled = True
            comboBox1.Enabled = True
            TgrBtn.Enabled = False
        End If
    End Sub
    Private Sub ReceivedDataAction(data As Byte())
        'Define received data actions here.Defined actions work asynchronously.
        '"ReceivedDataWrite" works when reading data was received.
        BeginInvoke(New delegateUserControl(AddressOf ReceivedDataWrite), System.Text.Encoding.ASCII.GetString(data))
    End Sub

    Private Sub TgrBtn_Click(sender As Object, e As EventArgs) Handles TgrBtn.Click
        If comboBox1.SelectedItem <> vbNullString Then
            'ExecCommand("command")is for sending a command and getting a command response.
            ReceivedDataWrite(m_reader.ExecCommand("LON"))
        End If
    End Sub
    'delegateUserControl is for controlling UserControl from other threads.
    Private Delegate Sub delegateUserControl(str As String)
    Private Sub ReceivedDataWrite(receivedData As String)
        DataText.Text = ("[" + m_reader.IpAddress + "][" + DateTime.Now + "]" + receivedData)
    End Sub
    Private Sub SearchListUp(ip As String)
        If ip <> "" Then
            comboBox1.Items.Add(ip)
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1
            Return
        Else
            NICcomboBox.Enabled = True
            SctBtn.Enabled = True
            SchBtn.Enabled = True
        End If
    End Sub
    'Dispose before closing Form for avoiding error.
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) _
 Handles Me.FormClosing
        m_reader.Dispose()
        m_searcher.Dispose()
        liveviewForm1.Dispose()
    End Sub
End Class

