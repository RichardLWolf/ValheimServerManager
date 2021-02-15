Public Class clsLogger
    Private Const LOG_FILE_NAME As String = "error_log.txt"
    Private msLogFile As String
    Private mbExternalDebugger As Boolean = False
    Private mbShowUEErrors As Boolean = False

    Public Enum LoggingLevel As Integer
        [Off] = 0
        [Error] = 1
        [Warning] = 2
        [Info] = 3
    End Enum

    Public Structure LoggedEntry
        Public LogDate As Date
        Public LogType As LoggingLevel
        Public LogHtmlMessage As String
    End Structure

    Private fiLogLevel As LoggingLevel

    Public ReadOnly Property ExternalDebugger As Boolean
        Get
            Return mbExternalDebugger
        End Get
    End Property

    Public ReadOnly Property LogFilename As String
        Get
            Return msLogFile
        End Get
    End Property


    ''' <summary>
    ''' Gets the logging form ready.
    ''' </summary>
    ''' <param name="LogLevel">Level of logging to record</param>
    ''' <param name="LogFilePath">The full path (NOT INCLUDING FILE NAME) of where to store the log file.</param>
    ''' <param name="ShowUEErrors">True if you wish unhandled exception errors to be displayed.</param>
    ''' <param name="FileName">Name of the log file, no path.  If unsupplied it will be "error_log.txt".</param>
    Public Sub New(ByVal LogLevel As LoggingLevel, ByVal LogFilePath As String, ByVal ShowUEErrors As Boolean, Optional ByVal StartUEHandler As Boolean = True, Optional ByVal FileName As String = "")
        mbShowUEErrors = ShowUEErrors
        If FileName.Trim = "" Then
            msLogFile = System.IO.Path.Combine(LogFilePath, LOG_FILE_NAME)
        Else
            msLogFile = System.IO.Path.Combine(LogFilePath, FileName)
        End If
        fiLogLevel = LogLevel
        ' purge the log file down to 100k or less
        Call LogPurge()

        If StartUEHandler Then
            Call StartUEH()
        End If

        Dim psText As String = String.Format("Log started {0:MMM-dd-yyyy hh:mm:ss tt}", DateTime.Now)
        ' send to windows log
        Call LogWrite(psText, EventLogEntryType.Information)
    End Sub

    Public Sub StopUEH()
        If Not System.Diagnostics.Debugger.IsAttached Then
            Try
                RemoveHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionEventRaised
                RemoveHandler Application.ThreadException, AddressOf ApplicationThreadEventRaised
            Catch ex As Exception
                ' don't really care at this point 
            End Try
        End If
    End Sub

    Public Sub PurgeLog()
        Call LogPurge()
    End Sub

    Public Sub Clear()
        Call ClearLog()
    End Sub

    Public Function LoadLogFormatted(Optional ByVal StartAtLineNumber As Integer = 0) As List(Of LoggedEntry)
        Return Me.ReadAllLog(StartAtLineNumber)
    End Function

    Public Function LoadLogRaw() As String
        Try
            Return System.IO.File.ReadAllText(msLogFile)
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' Entry will be written to event log and log windows based on the requested logging level.
    ''' </summary>
    ''' <param name="LogMessage"></param>
    ''' <param name="Severity"></param>
    Public Sub LogEntry(ByVal LogMessage As String, ByVal Severity As System.Diagnostics.EventLogEntryType)
        Dim pbWrite As Boolean = False
        Dim piTargetLevel As Integer
        Select Case Severity
            Case EventLogEntryType.Information
                piTargetLevel = CInt(LoggingLevel.Info)
            Case EventLogEntryType.Warning
                piTargetLevel = CInt(LoggingLevel.Warning)
            Case Else
                piTargetLevel = CInt(LoggingLevel.Error)
        End Select
        If piTargetLevel <= fiLogLevel Then
            ' send to windows log
            Call LogWrite(LogMessage, Severity)
        End If
    End Sub

    ''' <summary>
    ''' Entry will always be written to the log window and windows event log with error severity.
    ''' </summary>
    ''' <param name="LogMessage"></param>
    ''' <param name="ExceptionMessage"></param>
    Public Sub LogError(ByVal LogMessage As String, ByVal ExceptionMessage As String)
        Dim psTemp As String
        psTemp = LogMessage & ControlChars.CrLf & "Err Msg: " & ExceptionMessage
        ' send to windows log
        Call LogWrite(psTemp, EventLogEntryType.Error)
    End Sub

    Public Sub LogException(ByVal From As String, ByVal Ex As Exception)
        Try
            Dim psTemp As String
            Dim psEx As String

            psTemp = "Exception Information: " & Ex.Message

            psEx = "Sender: " & From & vbCrLf
            psEx = psEx & "Exception: " & Ex.Message
            If Ex.InnerException IsNot Nothing Then
                psEx = psEx & vbCrLf & vbCrLf & "Inner Exception: " & Ex.InnerException.Message
            End If
            ' send to windows log
            Call LogWrite(psEx, EventLogEntryType.Error)
        Catch ex2 As Exception
            ' don't care at this point 
        End Try
    End Sub


    Private Sub ClearLog()
        Try
            System.IO.File.Delete(msLogFile)
        Catch ex As Exception
        End Try
        ' send to windows log
        Call LogWrite("Log file cleared", EventLogEntryType.Information)
    End Sub


    Private Sub StartUEH()
        If Not System.Diagnostics.Debugger.IsAttached Then
            Try
                ' Unhandled exception errors
                AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionEventRaised
                ' Application errros
                AddHandler Application.ThreadException, AddressOf ApplicationThreadEventRaised
            Catch ex As Exception
                MsgBox("Error attempting to add unhandled exception method: " & ControlChars.CrLf & ex.Message & ControlChars.CrLf & ControlChars.CrLf & "Unhandled exception trapping is not enabled for this run.", giModalInfoOK, "Unhandled Exception Trapping Unavailable")
            End Try
        Else
            Debug.Print(ControlChars.CrLf & "Windows indicates that an external debugger is attached to this program's process." & ControlChars.CrLf & "The " & Application.ProductName & " application will not trap for unhandled exceptions during this run." & ControlChars.CrLf)
            mbExternalDebugger = True
            'MsgBox("Windows indicates that an external debugger is attached to this program's process." & ControlChars.CrLf & ControlChars.CrLf & "The " & modConstants.APP_NAME & " application will not trap for unhandled exceptions during this run.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly + MsgBoxStyle.SystemModal, "External Debugger Detected")
        End If
    End Sub


    Private Sub UnhandledExceptionEventRaised(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        If e.IsTerminating Then
            Dim o As Object = e.ExceptionObject
            Call LogException("Unhandled Exception Error", e.ExceptionObject)
            'Call LogWrite("Unhandled exception error occurred: " & o.ToString, EventLogEntryType.Error)
            If mbShowUEErrors Then
                Call ShowUEError(o.ToString)
            End If
        End If
    End Sub

    Private Sub ApplicationThreadEventRaised(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        'Dim psTemp As String
        'psTemp = "Unhandled Application thread error: " & ControlChars.CrLf & e.Exception.Message & ControlChars.CrLf & "-----------------------" & ControlChars.CrLf & "Stack Trace:" & ControlChars.CrLf & e.Exception.StackTrace
        Call LogException("Unhandled Application Thread Error", e.Exception)
        If mbShowUEErrors Then
            If ShowAppError(e.Exception.Message, e.Exception.StackTrace) = False Then
                Application.Exit()
            End If
        Else
            Application.Exit()
        End If
    End Sub

    Private Sub ShowUEError(ByVal ExceptionMessage As String)
        Dim psTemp As String
        psTemp = "An unrecoverable error has occurred in the application.  The details of this error have been recorded in the Windows Event Log.{0}{0}If you continue to receive this error please contact product support.{0}{0}Exception Detail: {1}"
        psTemp = String.Format(psTemp, ControlChars.Lf, ExceptionMessage)
        MsgBox(psTemp, giModalCritOK, "Unhandled Exception Error")
    End Sub

    Private Function ShowAppError(ByVal ExceptionMessage As String, ByVal StackTrace As String) As Boolean
        Dim poSB As New System.Text.StringBuilder

        poSB.AppendLine("An exepected program error has occurred: ")
        poSB.AppendLine(ExceptionMessage).Append(ControlChars.NewLine)
        poSB.Append("Continue running program?")
        If MsgBox(poSB.ToString, giModalExclYesNo, "Application Error") = MsgBoxResult.Yes Then
            Return True
        End If
        Return False
    End Function

    Private Function ReadAllLog(Optional ByVal StartAtLineNumber As Integer = 0) As List(Of LoggedEntry)
        Dim poList As New List(Of LoggedEntry)
        Dim psLines() As String = New String() {}
        Dim piIndex As Integer
        Dim psFields() As String
        Dim ptStart As Date = DateTime.Now

        Try
            If System.IO.File.Exists(msLogFile) Then
                Do While psLines.Count = 0 And Math.Abs(DateTime.Now.Subtract(ptStart).TotalSeconds) < 4
                    Try
                        psLines = System.IO.File.ReadAllLines(msLogFile)
                        Exit Do
                    Catch InUseEx As Exception
                        ' assume file in use just idle
                        System.Threading.Thread.Sleep(250)
                    End Try
                Loop
            End If
            For piIndex = 0 To psLines.Count - 1
                If piIndex >= StartAtLineNumber Then
                    Dim poNew As New LoggedEntry
                    psFields = psLines(piIndex).Split(vbTab)
                    If psFields.Count = 3 Then
                        poNew.LogDate = ParseLogDateTime(psFields(0).Trim)
                        poNew.LogHtmlMessage = psFields(2)

                        Select Case SafeStr(psFields(1))
                            Case "INF"
                                poNew.LogType = LoggingLevel.Info
                            Case "WAR"
                                poNew.LogType = LoggingLevel.Warning
                            Case "ERR"
                                poNew.LogType = LoggingLevel.Error
                            Case Else
                                poNew.LogType = LoggingLevel.Off
                        End Select
                        poList.Add(poNew)
                    End If
                End If
            Next
        Catch ex As Exception
            Debug.Print("RTE clsLogger.ReadAllLog: " & ex.Message)
        End Try

        Return poList
    End Function

    Private Function ParseLogDateTime(ByVal LogDate As String) As Date
        ' 0123456789.1234
        '        01234567
        ' 181219-14:11:30
        If LogDate.Length <> 15 Then
            Return Nothing
        Else
            Dim psDate As String = LogDate.Substring(0, 6)
            Dim psTime As String = LogDate.Substring(7, 8)
            Dim ptDate As New Date(SafeInt("20" & psDate.Substring(0, 2)), SafeInt(psDate.Substring(2, 2)), SafeInt(psDate.Substring(4, 2)) _
                                   , SafeInt(psTime.Substring(0, 2)), SafeInt(psTime.Substring(3, 2)), SafeInt(psTime.Substring(6, 2)))
            Return ptDate
        End If
    End Function


    Private Sub LogWrite(ByVal LogMessage As String, ByVal Severity As System.Diagnostics.EventLogEntryType)
        Dim poSB As New System.Text.StringBuilder

        Try
            poSB.Append(String.Format("{0:yyMMdd-HH:mm:ss}", DateTime.Now)).Append(vbTab)
            Select Case Severity
                Case EventLogEntryType.Warning
                    poSB.Append("WAR")
                Case EventLogEntryType.Information
                    poSB.Append("INF")
                Case Else
                    poSB.Append("ERR")
            End Select
            poSB.Append(vbTab)
            poSB.Append(System.Net.WebUtility.HtmlEncode(LogMessage.Replace(vbTab, "     ")).Replace(vbCrLf, "<br />"))
            poSB.Append(vbCrLf)
            Dim ptStart As Date = DateTime.Now
            Do While Math.Abs(DateTime.Now.Subtract(ptStart).TotalSeconds) < 2
                Try
                    System.IO.File.AppendAllText(msLogFile, poSB.ToString)
                    Exit Do
                Catch InUseEx As Exception
                    System.Threading.Thread.Sleep(125)
                End Try
            Loop

        Catch ex As Exception
            Debug.Print("RTE attempting to write to application log: " & ex.Message)
            Debug.Print("=>Log message: " & LogMessage)
        End Try
    End Sub

    ''' <summary>
    ''' Purges log file down to around 30k if log size is over 100k.
    ''' </summary>
    Private Sub LogPurge()
        Dim flFileSize As Long

        Try
            If System.IO.File.Exists(msLogFile) Then
                flFileSize = New System.IO.FileInfo(msLogFile).Length
                If flFileSize > 100000 Then
                    Dim psAllText As String = System.IO.File.ReadAllText(msLogFile)
                    Dim piOffset As Integer = psAllText.Length - 70000
                    Dim piStart As Integer = 0

                    For piIndex = piOffset To 0 Step -1
                        If psAllText.Substring(piIndex, 2) = vbCrLf Then
                            piStart = piIndex + 2
                            Exit For
                        End If
                    Next
                    System.IO.File.WriteAllText(msLogFile, psAllText.Substring(piStart))
                End If
            End If

        Catch ex As Exception
            Debug.Print("ERROR PURGING LOG FILE: " & ex.Message)
        End Try
    End Sub

End Class
