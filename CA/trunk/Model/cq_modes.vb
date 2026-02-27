Imports System
Public Class cq_modes
    Public product_mode_id As Integer
    Public product_main_id As Integer
    Public mode_id As Integer
    Public order_idx As Short
    Public validity As Boolean
    Public validation_date As DateTime
    Public validation As Boolean
    Public function_id As Integer
    Public mode As String

    Public Overrides Function ToString() As String
        Return mode
    End Function

    Public Function CompareTo(value As cq_modes) As Boolean
        Try

            'If product_main_id <> value.product_main_id Then Return False
            If mode <> value.mode Then Return False
            If validity <> value.validity Then Return False
            If validation <> value.validation Then Return False

            Return True

        Catch ex As Exception
            Throw New Exception("cq_modes.Compare()::" & ex.Message)
        End Try

    End Function

End Class
