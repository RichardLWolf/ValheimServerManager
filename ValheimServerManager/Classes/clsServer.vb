Public Class clsServer
    Public Event ServerFound(ByVal oServ As clsServer)

    Public Enum StateVals
        [Undetermined] = 0
        [Stopped] = 1
        [Running] = 2
        [Updating] = 3
    End Enum

    Private Const ServerProcessName As String = "valheim_server.exe"

    Private miCurrState As StateVals
    Private msFolderPath As String
    Private miPID As Integer
    Private msExeVer As String
    Private msServerName As String
    Private miPort As Integer
    Private msWorldName As String
    Private msPass As String
    Private msSaveDir As String
    Private mbUpdate As Boolean
    Private mbRestart As Boolean
    Private mbBackup As Boolean
    Private miRestartHour As Integer
    Private miRestartMin As Integer
    Private msBackupPath As String
    Private msWorldFolder As String

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

#End Region     '   PUBLIC PROPERTIES

    Public Sub New(ByVal sBackupPath As String, ByVal sBaseFolderPath As String, ByVal sServerName As String, ByVal iServPort As Integer, ByVal sWorldName As String, ByVal sPass As String _
                            , ByVal sSaveDir As String, ByVal bUpdateOnRestart As Boolean, ByVal bRestart As Boolean, ByVal bBackup As Boolean _
                            , ByVal iRestartHour As Integer, ByVal iRestartMin As Integer)
        miCurrState = StateVals.Undetermined
        miPID = 0

        msBackupPath = sBackupPath
        msFolderPath = sBaseFolderPath
        msServerName = sServerName
        miPort = iServPort
        msWorldName = sWorldName
        msPass = sPass
        msSaveDir = sSaveDir
        mbUpdate = bUpdateOnRestart
        mbRestart = bRestart
        mbBackup = bBackup
        miRestartHour = iRestartHour
        miRestartMin = iRestartMin
        msWorldFolder = System.IO.Path.Combine(msSaveDir, "worlds")
    End Sub

    Public Function HasBackups() As Boolean
        If System.IO.Directory.Exists(msBackupPath) Then
            If System.IO.File.Exists(System.IO.Path.Combine(msBackupPath, "*.zip")) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function HasGameFiles() As Boolean
        If System.IO.Directory.Exists(msWorldFolder) Then
            If System.IO.File.Exists(System.IO.Path.Combine(msWorldFolder, "*.db")) Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function HasSteamCmd() As Boolean
        Dim psSteamCMD As String = System.IO.Path.Combine(msFolderPath, "steamcmd.exe")
        Return System.IO.File.Exists(psSteamCMD)
    End Function




    Public Sub CheckStatus()

    End Sub

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



    Private Sub CheckForProgram()
        Try
            miPID = 0
            msExeVer = ""
            For Each poProc As Process In Process.GetProcesses()
                If poProc.ProcessName.IndexOf(ServerProcessName, 0, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                    Try
                        Dim psPath As String = poProc.Modules(0).FileName
                        Dim psVer As String = poProc.Modules(0).FileVersionInfo.ProductVersion
                        If psPath.IndexOf(msFolderPath, 0, StringComparison.InvariantCultureIgnoreCase) > -1 Then
                            miPID = poProc.Id
                            msExeVer = psVer
                            miCurrState = StateVals.Running
                            RaiseEvent ServerFound(Me)
                            Exit Sub
                        End If
                    Catch ex2 As Exception
                        goLogger.LogException("clsServer.CheckForProgram(" & msFolderPath & ")", ex2)
                    End Try
                End If
            Next
        Catch OuterEx As Exception
            goLogger.LogException("clsServer.CheckForProgram(" & msFolderPath & ")", OuterEx)
        End Try
    End Sub


End Class
