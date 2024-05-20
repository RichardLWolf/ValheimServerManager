'  Thank you to the developer(s) of https://github.com/cyilcode/SteamQueryNet for simplifying my life!

Public Class clsServer
    Public Event ServerStatus(ByVal oServ As clsServer)

    Public Enum StateVals
        [Undetermined] = 0
        [Stopped] = 1
        [Starting] = 2
        [Running] = 3
        [Updating] = 4
        [BackingUp] = 5
        [Restoring] = 6
    End Enum

    Private Const ServerProcessName As String = "valheim_server"

    Private miCurrState As StateVals
    Private msFolderPath As String
    Private miPID As Integer
    Private msExeVer As String
    Private msServerName As String
    Private miPort As Integer
    Private msWorldName As String
    Private msPass As String
    Private msSaveDir As String
    Private mbPublicFlag As Boolean
    Private mbUpdate As Boolean
    Private mbRestart As Boolean
    Private mbBackup As Boolean
    Private miRestartHour As Integer
    Private miRestartMin As Integer
    Private msBackupPath As String
    Private msWorldFolder As String
    ' Configuration settings
    Private msPresetValue As String
    Private msModCombatValue As String
    Private msModDeathValue As String
    Private msModResources As String
    Private msModRaids As String
    Private msModPortals As String


    Private mbAutoStart As Boolean = False
    Private mbAbort As Boolean = False
    Private mbAlive As Boolean = False

    Private moWorkThread As System.Threading.Thread
    Private moMonitorThread As System.Threading.Thread
    Private mbChecking As Boolean = False
    Private moServerProc As System.Diagnostics.Process = Nothing
    Private mtNextRestart As Date


    Public CreationGUID As String
    Public LastRestoreResult As Boolean = True
    Public LastBackupResult As Boolean = True

#Region "  PUBLIC PROPERTIES "

    Public ReadOnly Property PID As Integer
        Get
            Return miPID
        End Get
    End Property

    Public ReadOnly Property ExeVersion As String
        Get
            Return msExeVer
        End Get
    End Property


    Public ReadOnly Property CurrentState As StateVals
        Get
            Return miCurrState
        End Get
    End Property

    Public Property ServerName As String
        Get
            Return msServerName
        End Get
        Set(Value As String)
            msServerName = Value
        End Set
    End Property

    Public Property WorldName As String
        Get
            Return msWorldName
        End Get
        Set(value As String)
            msWorldName = value.Trim
        End Set
    End Property

    Public Property Password As String
        Get
            Return msPass
        End Get
        Set(value As String)
            msPass = value.Trim
        End Set
    End Property

    Public Property Port As Integer
        Get
            Return miPort
        End Get
        Set(value As Integer)
            miPort = value
        End Set
    End Property

    Public Property SaveDir As String
        Get
            Return msSaveDir
        End Get
        Set(value As String)
            msSaveDir = value.Trim
        End Set
    End Property

    Public Property PublicFlag As Boolean
        Get
            Return mbPublicFlag
        End Get
        Set(value As Boolean)
            mbPublicFlag = value
        End Set
    End Property
    Public Property RestartServer As Boolean
        Get
            Return mbRestart
        End Get
        Set(value As Boolean)
            mbRestart = value
        End Set
    End Property

    Public Property UpdateServer As Boolean
        Get
            Return mbUpdate
        End Get
        Set(value As Boolean)
            mbUpdate = value
        End Set
    End Property

    Public Property BackupServer As Boolean
        Get
            Return mbBackup
        End Get
        Set(value As Boolean)
            mbBackup = value
        End Set
    End Property


    Public Property RestartHour As Integer
        Get
            Return miRestartHour
        End Get
        Set(value As Integer)
            miRestartHour = Math.Max(0, Math.Min(24, value))
        End Set
    End Property

    Public Property RestartMin As Integer
        Get
            Return miRestartMin
        End Get
        Set(value As Integer)
            miRestartMin = Math.Max(0, Math.Min(59, value))
        End Set
    End Property

    Public Property BackupPath As String
        Get
            Return msBackupPath
        End Get
        Set(value As String)
            msBackupPath = value
        End Set
    End Property

    Public Property FolderPath As String
        Get
            Return msFolderPath
        End Get
        Set(value As String)
            msFolderPath = value
        End Set
    End Property

    Public ReadOnly Property WorldDataFolder As String
        Get
            Return System.IO.Path.Combine(System.IO.Path.Combine(Me.FolderPath, Me.SaveDir), "worlds")
        End Get
    End Property

    Public Property AutoStart As Boolean
        Get
            Return mbAutoStart
        End Get
        Set(value As Boolean)
            mbAutoStart = value
        End Set
    End Property

    Public ReadOnly Property NextRestart As Date
        Get
            If mbRestart = False Then
                Return Nothing
            Else
                Return mtNextRestart
            End If
        End Get
    End Property



    Public Property PresetValue As String
        Get
            Return msPresetValue
        End Get
        Set(ByVal value As String)
            msPresetValue = value.Trim
        End Set
    End Property

    Public Property ModCombatValue As String
        Get
            Return msModCombatValue
        End Get
        Set(ByVal value As String)
            msModCombatValue = value.Trim
        End Set
    End Property

    Public Property ModDeathValue As String
        Get
            Return msModDeathValue
        End Get
        Set(ByVal value As String)
            msModDeathValue = value.Trim
        End Set
    End Property

    Public Property ModResources As String
        Get
            Return msModResources
        End Get
        Set(ByVal value As String)
            msModResources = value.Trim
        End Set
    End Property

    Public Property ModRaids As String
        Get
            Return msModRaids
        End Get
        Set(ByVal value As String)
            msModRaids = value.Trim
        End Set
    End Property

    Public Property ModPortals As String
        Get
            Return msModPortals
        End Get
        Set(ByVal value As String)
            msModPortals = value.Trim
        End Set
    End Property




#End Region     '   PUBLIC PROPERTIES

    Public Sub New(ByVal sBackupPath As String, ByVal sBaseFolderPath As String, ByVal sServerName As String, ByVal iServPort As Integer, ByVal sWorldName As String, ByVal sPass As String _
                            , ByVal sPresetValue As String, ByVal sModCombatValue As String, ByVal sModDeathValue As String, ByVal sModResources As String _
                            , ByVal smodRaids As String, ByVal sModPortals As String _
                            , ByVal sSaveDir As String, ByVal bUpdateOnRestart As Boolean, ByVal bPublic As Boolean, ByVal bRestart As Boolean, ByVal bBackup As Boolean _
                            , ByVal iRestartHour As Integer, ByVal iRestartMin As Integer)
        CreationGUID = Guid.NewGuid.ToString

        miCurrState = StateVals.Undetermined
        miPID = 0

        msBackupPath = sBackupPath
        msFolderPath = sBaseFolderPath
        msServerName = sServerName
        miPort = iServPort
        msWorldName = sWorldName
        msPass = sPass

        msPresetValue = sPresetValue
        msModCombatValue = sModCombatValue
        msModDeathValue = sModDeathValue
        msModResources = sModResources
        msModRaids = smodRaids
        msModPortals = sModPortals

        msSaveDir = sSaveDir
        mbPublicFlag = bPublic
        mbUpdate = bUpdateOnRestart
        mbRestart = bRestart
        mbBackup = bBackup
        miRestartHour = iRestartHour
        miRestartMin = iRestartMin
        msWorldFolder = System.IO.Path.Combine(msSaveDir, "worlds")
    End Sub

    Public Function HasBackups() As Boolean
        If System.IO.Directory.Exists(msBackupPath) Then
            For Each psFile As String In System.IO.Directory.EnumerateFiles(System.IO.Path.Combine(msBackupPath))
                If psFile.IndexOf(Me.CreationGUID, StringComparison.CurrentCultureIgnoreCase) > -1 Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Function HasGameFiles() As Boolean
        If System.IO.Directory.Exists(Me.WorldDataFolder) Then
            If System.IO.Directory.EnumerateFiles(Me.WorldDataFolder).Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function HasSteamCmd() As Boolean
        Dim psSteamCMD As String = System.IO.Path.Combine(msFolderPath, "steamcmd.exe")
        Return System.IO.File.Exists(psSteamCMD)
    End Function





    Public Function DownloadSteam() As Boolean
        Dim pbResult As Boolean = False
        Try
            Using poClient As New System.Net.WebClient
                Dim psTarget As String = System.IO.Path.Combine(msFolderPath, "steamcmd.zip")
                poClient.DownloadFile("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip", psTarget)
                ' file downloaded, unzip it
                System.IO.Compression.ZipFile.ExtractToDirectory(psTarget, msFolderPath)
                ' pause a tick for windows to release handles
                System.Threading.Thread.Sleep(250)
                ' delete the zip
                Try
                    System.IO.File.Delete(psTarget)
                Catch InnerEx As Exception
                    ' don't care
                End Try
                pbResult = True
            End Using

        Catch ex As Exception
            goLogger.LogException("clsServer.SteamCmdDownloadThread(" & msFolderPath & ")", ex)
            MsgBox("Failed to download steam client to " & msFolderPath & ", see error log for more information.", giModalExclOK, "SteamCMD Download Failed")
        End Try

        Return pbResult
    End Function

    Public Sub StartMonitor()
        moServerProc = Nothing
        moMonitorThread = New System.Threading.Thread(AddressOf MonitorServer)
        moMonitorThread.IsBackground = True
        moMonitorThread.Start()
    End Sub

    Public Sub StopMonitor()
        mbAutoStart = False
        mbAbort = True
    End Sub

    Public Sub DoBackup()
        If moWorkThread IsNot Nothing AndAlso moWorkThread.IsAlive Then
            Exit Sub
        Else
            moWorkThread = New System.Threading.Thread(AddressOf PerformBackupData)
            moWorkThread.IsBackground = True
            moWorkThread.Start()
        End If
    End Sub

    Public Sub DoRestore(ByVal ZipFileToRestore As String)
        If moWorkThread IsNot Nothing AndAlso moWorkThread.IsAlive Then
            Exit Sub
        Else
            moWorkThread = New System.Threading.Thread(AddressOf PerformDataRestore)
            moWorkThread.IsBackground = True
            moWorkThread.Start(ZipFileToRestore)
        End If
    End Sub

    Public Sub DoUpdate()
        If moWorkThread IsNot Nothing AndAlso moWorkThread.IsAlive Then
            Exit Sub
        Else
            moWorkThread = New System.Threading.Thread(AddressOf PerformProgramUpdate)
            moWorkThread.IsBackground = True
            moWorkThread.Start()
        End If
    End Sub


    Public Function GetServerInfo() As SteamQueryNet.Models.ServerInfo
        Try
            Using poQuery As New SteamQueryNet.ServerQuery(String.Format("steam://connect/{0}:{1:#0}", "127.0.0.1", Me.Port + 1))
                Dim poServInfo As SteamQueryNet.Models.ServerInfo = poQuery.GetServerInfo
                Return poServInfo
            End Using

        Catch ex As Exception
            goLogger.LogException("clsServer.GetServerInfo", ex)
        End Try

        Return Nothing
    End Function

    Public Function GetPlayerInfo() As List(Of SteamQueryNet.Models.Player)
        Dim poPlayers As New List(Of SteamQueryNet.Models.Player)
        Try
            Using poQuery As New SteamQueryNet.ServerQuery(String.Format("steam://connect/{0}:{1:#0}", "127.0.0.1", Me.Port + 1))
                poPlayers = poQuery.GetPlayers
            End Using

        Catch ex As Exception
            goLogger.LogException("clsServer.GetPlayerInfo", ex)
        End Try

        Return poPlayers
    End Function



    Private Sub MonitorServer()
        Dim ptStartTime As Date
        Dim pbRestartTime As Boolean = False

        Try
            mbAbort = False
            ' is the server running already?
            If CheckForProcess() = False Then
                If System.IO.File.Exists(BuildProcessExeName()) Then
                    Dim processStartInfo As System.Diagnostics.ProcessStartInfo
                    Dim psArgLine As String

                    miCurrState = StateVals.Starting
                    RaiseEvent ServerStatus(Me)

                    'valheim_server -nographics -batchmode -name "CompanyOfWolves" -port 7810 -world "worldcow" -password "CompanyOfWolves!" -savedir "C:\Valheim Server\savedata"
                    psArgLine = "-nographics -batchmode -name " & Chr(34) & Me.ServerName & Chr(34)
                    psArgLine = psArgLine & " -port " & Me.Port
                    psArgLine = psArgLine & " -world " & Chr(34) & Me.WorldName & Chr(34) & " -password " & Chr(34) & Me.Password & Chr(34)
                    psArgLine = psArgLine & " -savedir " & Chr(34) & Me.SaveDir & Chr(34)
                    psArgLine = psArgLine & " -public " & IIf(Me.PublicFlag, "1", "0")
                    ' mods
                    If msPresetValue <> "" Then
                        psArgLine = psArgLine & "-preset " & msPresetValue
                    End If
                    If msModCombatValue <> "" Then
                        psArgLine = psArgLine & "-modifier combat " & msModCombatValue
                    End If
                    If msModDeathValue <> "" Then
                        psArgLine = psArgLine & "-modifier deathpenalty " & msModDeathValue
                    End If
                    If msModResources <> "" Then
                        psArgLine = psArgLine & "-modifier resources " & msModResources
                    End If
                    If msModRaids <> "" Then
                        psArgLine = psArgLine & "-modifier raids " & msModRaids
                    End If
                    If msModPortals <> "" Then
                        psArgLine = psArgLine & "-modifier portals " & msModPortals
                    End If
                    ' Start the process itself
                    processStartInfo = New System.Diagnostics.ProcessStartInfo()
                    processStartInfo.FileName = Chr(34) & BuildProcessExeName() & Chr(34)
                    processStartInfo.Arguments = psArgLine
                    processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                    processStartInfo.UseShellExecute = False
                    processStartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Me.FolderPath)
                    processStartInfo.EnvironmentVariables.Add("SteamAppId", "892970")
                    moServerProc = System.Diagnostics.Process.Start(processStartInfo)
                    System.Threading.Thread.Sleep(3000)
                    If CheckForProcess() = False Then
                        goLogger.LogError("clsServer.MonitorServer", "Failed to load executable file: " & BuildProcessExeName())
                        mbAbort = True
                        mbRestart = False
                    Else
                        miCurrState = StateVals.Running
                        mbAlive = True
                        RaiseEvent ServerStatus(Me)
                    End If
                Else
                    goLogger.LogError("clsServer.MonitorServer", "Missing server executable file: " & BuildProcessExeName())
                    mbAbort = True
                    mbRestart = False
                End If
            Else
                miCurrState = StateVals.Running
                mbAlive = True
                RaiseEvent ServerStatus(Me)
            End If
            If moServerProc IsNot Nothing And Not mbAbort And mbAlive Then
                '' see if we can query the server
                'mbAlive = False
                'ptStartTime = DateTime.Now
                'Do While Not mbAbort And Not mbAlive And DateTime.Now.Subtract(ptStartTime).TotalMinutes < 2 And moServerProc IsNot Nothing
                '    System.Threading.Thread.Sleep(250)
                '    Try
                '        Using poQuery As New SteamQueryNet.ServerQuery(String.Format("steam://connect/{0}:{1:#0}", "127.0.0.1", Me.Port + 1))
                '            poQuery.ReceiveTimeout = 100
                '            poQuery.SendTimeout = 100
                '            Dim poServInfo As SteamQueryNet.Models.ServerInfo = poQuery.GetServerInfo
                '            If poServInfo IsNot Nothing Then
                '                Debug.Print("Product version is " & moServerProc.Modules(0).FileVersionInfo.ProductVersion)
                '                Debug.Print("File version is " & moServerProc.Modules(0).FileVersionInfo.FileVersion)
                '                Dim psVer As String = moServerProc.Modules(0).FileVersionInfo.ProductVersion
                '                miPID = moServerProc.Id
                '                msExeVer = psVer
                '                mbAlive = True
                '                miCurrState = StateVals.Running
                '                RaiseEvent ServerStatus(Me)
                '            Else
                '                goLogger.LogEntry("Start thread " & Me.ServerName & " no response from steam connect port " & Me.Port + 1 & ": Abort(" & mbAbort & ") Alive(" & mbAlive & ") Restart(" & pbRestartTime & ")", EventLogEntryType.Warning)
                '            End If
                '        End Using

                '    Catch ThreadEx As System.Threading.ThreadAbortException
                '        mbAbort = True
                '        Exit Do

                '    Catch QueryEx As Exception
                '        ' skip it and try again
                '    End Try
                'Loop
                'If mbAbort Then
                '    miCurrState = StateVals.Stopped
                '    RaiseEvent ServerStatus(Me)
                'End If
                ' idle here until server goes away or time for backup, etc.
                mtNextRestart = NextRestartTime()
                pbRestartTime = False
                Do While mbAlive And Not mbAbort And Not pbRestartTime And moServerProc IsNot Nothing
                    System.Threading.Thread.Sleep(1000)
                    If moServerProc IsNot Nothing AndAlso moServerProc.HasExited Then
                        mbAlive = False
                    Else
                        If mbRestart AndAlso DateTime.Now >= mtNextRestart Then
                            pbRestartTime = True
                        End If
                    End If
                Loop
                goLogger.LogEntry("Wait thread " & Me.ServerName & " ending: Abort(" & mbAbort & ") Alive(" & mbAlive & ") Restart(" & pbRestartTime & ")", EventLogEntryType.Information)
                If mbAbort Or pbRestartTime Then
                    ' attempt to stop the game process, or kill it if it is not co-operating.
                    Try
                        ' try to be nice first, give it some time to close itself out.
                        If moServerProc IsNot Nothing AndAlso moServerProc.HasExited = False Then
                            moServerProc.CloseMainWindow()
                            moServerProc.Close()
                            moServerProc.WaitForExit(30000) ' give it 30 seconds
                        End If
                        ' if the process is still running, be a little more forcefull
                        If moServerProc IsNot Nothing AndAlso moServerProc.HasExited = False Then
                            moServerProc.Kill()
                        End If

                    Catch InnerEx As Exception
                        ' don't care

                    Finally
                        moServerProc = Nothing
                    End Try
                End If
                ' restart time?
                If pbRestartTime Then
                    miCurrState = StateVals.Stopped
                    RaiseEvent ServerStatus(Me)
                    goLogger.LogEntry("Scheduled restart for " & Me.FolderPath, EventLogEntryType.Information)
                    mbAutoStart = False ' prevent backup or update block from restarting this thread
                    System.Threading.Thread.Sleep(1000) ' let windows catch up
                    If mbBackup Then
                        goLogger.LogEntry("Beginning backup for " & Me.WorldDataFolder, EventLogEntryType.Information)
                        moWorkThread = New System.Threading.Thread(AddressOf PerformBackupData)
                        moWorkThread.IsBackground = True
                        moWorkThread.Start()
                        ptStartTime = DateTime.Now
                        Do While DateTime.Now.Subtract(ptStartTime).TotalSeconds < 120 AndAlso moWorkThread IsNot Nothing AndAlso moWorkThread.IsAlive
                            System.Threading.Thread.Sleep(1000)
                        Loop
                    End If
                    If mbUpdate Then
                        goLogger.LogEntry("Beginning update of program files for " & Me.FolderPath, EventLogEntryType.Information)
                        moWorkThread = New System.Threading.Thread(AddressOf PerformProgramUpdate)
                        moWorkThread.IsBackground = True
                        moWorkThread.Start()
                        ptStartTime = DateTime.Now
                        Do While DateTime.Now.Subtract(ptStartTime).TotalMinutes < 120 AndAlso moWorkThread IsNot Nothing AndAlso moWorkThread.IsAlive
                            System.Threading.Thread.Sleep(1000)
                        Loop
                    End If
                    ' turn autostart back on so this thread gets restarted
                    mbAutoStart = True
                    mbAbort = False
                End If
            End If


        Catch ThreadEx As System.Threading.ThreadAbortException
            ' don't care

        Catch ex As Exception
            goLogger.LogException("clsServer.MonitorServer", ex)

        Finally
            moServerProc = Nothing
            miPID = 0
            msExeVer = ""
            mtNextRestart = Nothing
            miCurrState = StateVals.Stopped
            RaiseEvent ServerStatus(Me)
            ' see if restart
            If mbAutoStart And mbAbort = False Then
                goLogger.LogEntry("Monitor thread ending, auto restart is enabled (" & Me.FolderPath & ").", EventLogEntryType.Information)
                Me.StartMonitor()
            End If
            moMonitorThread = Nothing
        End Try
    End Sub

    Private Sub PerformBackupData()
        Try
            miCurrState = StateVals.BackingUp
            RaiseEvent ServerStatus(Me)
            LastBackupResult = True

            ' pause a tick for windows to finish up game files if necessary
            System.Threading.Thread.Sleep(1500)


            Dim psSourceFolder As String = Me.WorldDataFolder
            Dim psDestFile As String = System.IO.Path.Combine(msBackupPath, String.Format("{0}-{1:yyyyMMdd-HHmmss}.zip", Me.CreationGUID, DateTime.Now))

            goLogger.LogEntry("Beginning backup of [" & psSourceFolder & "] to archive [" & psDestFile & "].", EventLogEntryType.Information)
            System.IO.Compression.ZipFile.CreateFromDirectory(psSourceFolder, psDestFile)
            ' pause a tick for windows to release handles
            System.Threading.Thread.Sleep(1500)
            goLogger.LogEntry("Backup of " & Me.WorldDataFolder & " to archive " & psDestFile & " complete.", EventLogEntryType.Information)
            ' get list of files
            Dim psList As New List(Of String)
            Dim psFileNames() As String = System.IO.Directory.GetFiles(msBackupPath, Me.CreationGUID & "*.zip")
            If psFileNames.Count > 20 Then
                Dim poDict As New Dictionary(Of String, Date)
                For Each psFile As String In psFileNames
                    Dim poCreation As Date = System.IO.File.GetCreationTime(psFile)
                    poDict.Add(psFile, poCreation)
                Next
                poDict = poDict.OrderBy(Function(x) x.Value).ToDictionary(Function(x) x.Key, Function(x) x.Value)
                Do While poDict.Count > 20
                    System.IO.File.Delete(poDict.Keys(0))
                    poDict.Remove(poDict.Keys(0))
                Loop
            End If

        Catch ThreadEx As System.Threading.ThreadAbortException
            LastBackupResult = False
            ' don't care

        Catch ex As Exception
            LastBackupResult = False
            goLogger.LogException("clsServer.PerformDataBackup", ex)

        Finally
            miCurrState = StateVals.Stopped
            RaiseEvent ServerStatus(Me)
            ' see if restart
            If mbAutoStart Then
                Me.StartMonitor()
            End If
            moWorkThread = Nothing
        End Try
    End Sub



    Private Sub PerformDataRestore(ByVal sZipToRestore As String)
        Try
            miCurrState = StateVals.Restoring
            RaiseEvent ServerStatus(Me)
            LastRestoreResult = True

            Dim psDestPath As String = Me.WorldDataFolder

            ' first nuke any existing files that are in the archive
            Using archive As System.IO.Compression.ZipArchive = System.IO.Compression.ZipFile.OpenRead(sZipToRestore)
                For Each ZipEntry As System.IO.Compression.ZipArchiveEntry In archive.Entries
                    Dim psFilename As String = System.IO.Path.Combine(psDestPath, ZipEntry.FullName)
                    If System.IO.File.Exists(psFilename) Then
                        System.IO.File.Delete(psFilename)
                    End If
                Next
            End Using
            ' pause a tick for windows to release handles
            System.Threading.Thread.Sleep(250)
            ' now restore files from the archive
            System.IO.Compression.ZipFile.ExtractToDirectory(sZipToRestore, psDestPath)
            ' pause a tick for windows to release handles
            System.Threading.Thread.Sleep(250)

        Catch ThreadEx As System.Threading.ThreadAbortException
            LastRestoreResult = False
            ' don't care

        Catch ex As Exception
            LastRestoreResult = False
            goLogger.LogException("clsServer.PerformDataRestore", ex)

        Finally
            miCurrState = StateVals.Stopped
            RaiseEvent ServerStatus(Me)
            ' see if restart
            If mbAutoStart Then
                Me.StartMonitor()
            End If
            moWorkThread = Nothing
        End Try
    End Sub

    Private Sub PerformProgramUpdate()
        Try
            Dim processStartInfo As System.Diagnostics.ProcessStartInfo
            Dim psArgLine As String

            miCurrState = StateVals.Updating
            RaiseEvent ServerStatus(Me)


            psArgLine = "+login anonymous +force_install_dir """ & Me.FolderPath & """ +app_update 896660 validate +exit"

            processStartInfo = New System.Diagnostics.ProcessStartInfo()
            processStartInfo.FileName = System.IO.Path.Combine(Me.FolderPath, "steamcmd.exe")
            processStartInfo.Arguments = psArgLine

            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
            processStartInfo.UseShellExecute = True
            processStartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(Me.FolderPath)

            Using poInstProcess As System.Diagnostics.Process = System.Diagnostics.Process.Start(processStartInfo)
                poInstProcess.WaitForExit(900000) ' 15 mintues
                If poInstProcess.ExitCode <> 0 And poInstProcess.ExitCode <> 7 Then
                    goLogger.LogError("clsServer.PerformProgramUpdate(" & Me.FolderPath & ")", "Update exit code was not 0 or 7: " & poInstProcess.ExitCode)
                End If
            End Using
            goLogger.LogEntry("Program update for " & Me.FolderPath & " is complete.", EventLogEntryType.Information)


        Catch ex As Exception
            goLogger.LogException("clsServer.PerformProgramUpdate", ex)

        Finally
            miCurrState = StateVals.Stopped
            RaiseEvent ServerStatus(Me)
            moWorkThread = Nothing
        End Try

    End Sub









    Private Function NextRestartTime() As Date
        Dim ptTarget As New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Me.RestartHour, Me.RestartMin, 0)
        If ptTarget < DateTime.Now Then
            ptTarget = ptTarget.AddDays(1)
        End If
        Return ptTarget
    End Function


    Private Function BuildProcessExeName() As String
        Return System.IO.Path.Combine(System.IO.Path.Combine(Me.FolderPath, "steamapps\common\Valheim dedicated server"), ServerProcessName & ".exe")
    End Function

    Private Function CheckForProcess() As Boolean
        Dim pbResult As Boolean = False
        Dim psProcName As String
        Dim psFileName As String
        Dim psVer As String

        Try
            miPID = 0
            msExeVer = ""
            For Each poProc As Process In Process.GetProcesses()
                psProcName = poProc.ProcessName
                If psProcName.IndexOf(ServerProcessName, 0, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                    Try
                        psFileName = poProc.Modules(0).FileName
                        psVer = poProc.Modules(0).FileVersionInfo.ProductVersion
                        Debug.Print(psProcName & "- " & psFileName)
                    Catch ex As Exception
                        psFileName = ""
                        psVer = ""
                    End Try
                    If psFileName.IndexOf(BuildProcessExeName, 0, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                        miPID = poProc.Id
                        msExeVer = psVer
                        miCurrState = StateVals.Running
                        moServerProc = poProc
                        pbResult = True
                        Exit For
                    End If
                End If
            Next
        Catch OuterEx As Exception
            goLogger.LogException("clsServer.CheckForProgram(" & msFolderPath & ")", OuterEx)
        End Try

        Return pbResult
    End Function





End Class
