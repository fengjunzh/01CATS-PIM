Imports System.IO
Public Class PowerCalData

    Dim nn As New List(Of String)
    Dim mm As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pp As New List(Of CATS.Model.cq_rec_imd_calibration)
        Dim xx As New List(Of PowerCalData11)


        nn.Clear()
        mm = ""

        Dim getdata As New CATS.BLL.cq_rec_imd_calibrationManager
        Dim st As Date = DateTimePicker1.Value
        Dim sp As Date = DateTimePicker2.Value

        Try

            DataGridView1.RowsDefaultCellStyle.BackColor = Color.Bisque
            DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Beige

            mm = "serial_number" & “,” & "phase" & “,” & "controller" & “,” & "instr_sn" & “,” & "employee" & “,” & "power" & “,” & "attenuation" & “,” & "tx1_power" & “,” & "tx1_offset" & “,” & "tx2_power" & “,” & "tx2_offset" & “,” & "power_meter" & “,” & "cal_type" & “,” & "cal_time"
            nn.Add(mm)

        pp = getdata.SelectByTime(st, sp)

        If pp IsNot Nothing Then
                For Each aa In pp
                    Dim yy As New PowerCalData11
                    yy.serial_number = aa.Rec_imd_calibrationM.serial_number
                    yy.phase = aa.Phase
                    yy.controller = aa.Controller
                    yy.instr_sn = aa.Instr_sn
                    yy.employee = aa.Employee
                    yy.power = aa.Rec_imd_calibrationM.power
                    yy.attenuation = aa.Rec_imd_calibrationM.attenuation
                    yy.tx1_power = aa.Rec_imd_calibrationM.tx1_power
                    yy.tx1_offset = aa.Rec_imd_calibrationM.tx1_offset
                    yy.tx2_power = aa.Rec_imd_calibrationM.tx2_power
                    yy.tx2_offset = aa.Rec_imd_calibrationM.tx2_offset
                    yy.power_meter_idn = aa.Rec_imd_calibrationM.power_meter_idn
                    yy.cal_type = aa.Rec_imd_calibrationM.cal_type
                    yy.cal_time = aa.Rec_imd_calibrationM.cal_time
                    xx.Add(yy)

                    If yy.power_meter_idn Is Nothing Then yy.power_meter_idn = "Unknown"
                    mm = yy.serial_number & “,” & yy.phase & “,” & yy.controller & “,” & yy.instr_sn.Replace(",", " ").Replace(Chr(10), "").Replace(Chr(13), "") & “,” & yy.employee & “,” & yy.power & “,” & yy.attenuation & “,” & yy.tx1_power & “,” & yy.tx1_offset & “,” & yy.tx2_power & “,” & yy.tx2_offset & “,” & yy.power_meter_idn.Replace(",", " ").Replace(Chr(10), "").Replace(Chr(13), "") & “,” & yy.cal_type & “,” & yy.cal_time
                    nn.Add(mm)
                Next

                DataGridView1.DataSource = xx

        Else
            MsgBox("There is no record during you selected time,please check!")

        End If

        Catch ex As Exception
            'MsgBox("error,please check!")
            Throw New Exception("error,please check!::" & ex.Message)
        End Try

    End Sub


    Public Class PowerCalData11
        Public Property serial_number As String
        Public Property phase As String
        Public Property controller As String
        Public Property instr_sn As String
        Public Property employee As String
        Public Property power As Decimal
        Public Property attenuation As Decimal
        Public Property tx1_power As Decimal
        Public Property tx1_offset As Decimal
        Public Property tx2_power As Decimal
        Public Property tx2_offset As Decimal
        Public Property power_meter_idn As String
        Public Property cal_type As String
        Public Property cal_time As DateTime
    End Class

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim Path00 As String = Application.StartupPath
            Dim SaveFileDialog1 As New SaveFileDialog
            SaveFileDialog1.FileName = "PowerCalData"
            SaveFileDialog1.InitialDirectory = Path00
            SaveFileDialog1.Filter = "CSV (*.csv)|*.CSV|All Files (*.*)|*.*"

            If SaveFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Path00 = SaveFileDialog1.FileName 'FileName即为绝对路径
            Else
                Return
            End If

            If WriteToCSV(Path00) = True Then
                MsgBox("ok")
            End If

        Catch ex As Exception
            MsgBox("save fail,please check!")
        End Try
    End Sub

    Public Function WriteToCSV(ByVal filePath As String) As Boolean
        Dim fileStream As System.IO.FileStream
        Dim streamWriter As System.IO.StreamWriter
        Dim strRow As String
        Try
            If (System.IO.File.Exists(filePath)) Then
                System.IO.File.Delete(filePath)
            End If
            fileStream = New FileStream(filePath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write)
            streamWriter = New StreamWriter(fileStream, System.Text.Encoding.Default)
            For Each strRow In nn
                streamWriter.WriteLine(strRow)
            Next
            streamWriter.Close()
            fileStream.Close()
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function


End Class

