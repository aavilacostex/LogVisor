Imports System.Globalization
Imports LogVisor.DTO
Imports LogVisor.UTIL

Public Class LogVisor : Implements IDisposable
    Private disposedValue As Boolean

    Private Shared strLogCadenaCabecera As String = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString()
    Dim strLogCadena As String = Nothing

    Shared ReadOnly objLog = New Logs()

    Public Function GetPDLogsFromSql(ByRef dsResult As DataSet) As Integer
        Dim result As Integer = -1
        dsResult = New DataSet()
        Dim dt As DataTable = New DataTable()
        Dim exMessage As String = " "
        Dim query = "SELECT * FROM dbCTPSystem.dbo.CtpSystemLog"
        Try
            Dim dsOut = New DataSet()
            Dim objDatos = New ClsRPGClientHelper()
            dt = objDatos.ExecuteQueryStoredProcedure(query, Nothing)

            If dt.Rows.Count > 0 Then
                dsResult.Tables.Add(dt)
                result = dt.Rows.Count
            End If

            Return result
        Catch ex As Exception
            exMessage = ex.ToString + ". " + ex.Message + ". " + ex.ToString
            objLog.writeLog(strLogCadenaCabecera, objLog.ErrorTypeEnum.Exception, ex.Message, ex.ToString())
            Return result
        End Try
    End Function

    Public Function GetAllPaAndPsUsers() As Data.DataSet
        Dim exMessage As String = " "
        Dim Sql As String
        Dim result As Integer = -1
        Dim dsResult As New DataSet()
        dsResult.Locale = CultureInfo.InvariantCulture
        Try
            Dim objDatos = New ClsRPGClientHelper()
            Sql = "SELECT A1.CNT03 PA, TRIM(A2.USNAME) FULLNAME, TRIM(A2.USUSER) USER  FROM qs36f.CNTRLL A1 INNER JOIN qs36f.CSUSER A2 ON CNT03 = DIGITS(USPURC)  WHERE CNT01 = '216' AND USPTY9 <> 'R' AND USPURC <> 0"
            result = objDatos.GetOdBcDataFromDatabase(Sql, dsResult)
            Return dsResult
        Catch ex As Exception
            exMessage = ex.ToString + ". " + ex.Message + ". " + ex.ToString
            objLog.writeLog(strLogCadenaCabecera, objLog.ErrorTypeEnum.Exception, ex.Message, ex.ToString())
            Return Nothing
        End Try
    End Function

#Region "IDisposable"

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override finalizer
            ' TODO: set large fields to null
            disposedValue = True
        End If
    End Sub

    ' ' TODO: override finalizer only if 'Dispose(disposing As Boolean)' has code to free unmanaged resources
    ' Protected Overrides Sub Finalize()
    '     ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
    '     Dispose(disposing:=False)
    '     MyBase.Finalize()
    ' End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
