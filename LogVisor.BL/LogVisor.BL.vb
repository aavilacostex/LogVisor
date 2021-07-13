Imports LogVisor.DTO
Imports LogVisor.UTIL

Public Class LogVisor : Implements IDisposable
    Private disposedValue As Boolean

    Private Shared strLogCadenaCabecera As String = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString()
    Dim strLogCadena As String = Nothing

    Shared ReadOnly objLog = New Logs()

    Public Function GetAllPaAndPsUsers() As DataSet
        Dim dsResult = New DataSet()
        Dim exMessage As String = " "
        Try
            Dim objDal = New DAL.LogVisor()
            dsResult = objDal.GetAllPaAndPsUsers()
            Return dsResult
        Catch ex As Exception
            exMessage = ex.ToString + ". " + ex.Message + ". " + ex.ToString
            Return Nothing
        End Try
    End Function

    Public Function GetPDLogsFromSql(ByRef dsResult As DataSet) As Integer
        dsResult = New DataSet()
        Dim result As Integer = -1
        Dim exMessage As String = " "
        Try
            Dim objDal = New DAL.LogVisor()
            result = objDal.GetPDLogsFromSql(dsResult)
            Return result
        Catch ex As Exception
            exMessage = ex.ToString + ". " + ex.Message + ". " + ex.ToString
            Return result
        End Try
    End Function

    Public Function GetGridParameterDin() As List(Of String)
        Dim exMessage As String = Nothing
        Try
            Dim objDatos = New ClsRPGClientHelper()
            Dim totalRow = objDatos.GetRowCount
            Dim pageSize = objDatos.GetPageSize
            Dim lstGridParameter = New List(Of String)()
            lstGridParameter.Add(totalRow)
            lstGridParameter.Add(pageSize)
            Return lstGridParameter
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
