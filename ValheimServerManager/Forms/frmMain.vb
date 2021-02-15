Imports ValheimServerManager

Public Class frmMain
    Private Enum MonitorState
        [Loading] = 0
        [Running] = 1
        [Stopping] = 2
    End Enum

    Private WithEvents foTimer As Timer


    Private moCheckThread As System.Threading.Thread
    Private moLoggedErr As New List(Of Integer)

    Private foInstallThread As System.Threading.Thread

    Private fiMonitorState As MonitorState = MonitorState.Loading




    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If foInstallThread IsNot Nothing Then
            MsgBox("Please wait for the current install to complete.", giModalExclOK, "Install In Progress")
            Exit Sub
        End If

        Dim piCount As Integer = lvwServers.Items.Count
        Dim poFrm As New frmEditServer
        Dim psFolder As String = ""
        Dim psSaveDir As String = System.IO.Path.Combine(psFolder, "savedata")
        Dim psServer As String

        If piCount = 0 Then
            psServer = "My New Server"
            psFolder = "C:\Valheim Server"
        Else
            psFolder = String.Format("C:\Valheim Server {0:#0}", piCount + 1)
            psServer = String.Format("My New Server {0:#0}", piCount + 1)
        End If

        Dim poNewServ As New clsServer(modMain.BackupFolderPath, psFolder, psServer, 2456, "My New World", modMain.RandomPassword(10), psSaveDir, False, True, True, 3, 0)
        poFrm.ReadyForm(poNewServ)
        AddHandler poFrm.EditComplete, AddressOf ServerEdit_EditComplete
        poFrm.Show(Me)
    End Sub

    Private Sub btnAppFolder_Click(sender As Object, e As EventArgs) Handles btnAppFolder.Click
        Try
            Process.Start("explorer.exe", "/select," & Chr(34) & modMain.RootFolderPath & Chr(34))

        Catch ex As Exception
            MsgBox("Unable to open app folder: " & modMain.RootFolderPath, giModalExclOK, "Error starting explorer.")
        End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click

    End Sub

    Private Sub btnViewServerFolder_Click(sender As Object, e As EventArgs) Handles btnViewServerFolder.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                Try
                    Process.Start("explorer.exe", "/select," & Chr(34) & poTag.FolderPath & Chr(34))
                Catch ex As Exception
                    MsgBox("Unable to open server folder: " & poTag.FolderPath, giModalExclOK, "Error starting explorer.")
                End Try
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

    End Sub


    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.vsm
        Me.Text = "VSM v" & Application.ProductVersion
        fiMonitorState = MonitorState.Loading

        modMain.Initialize()

        foTimer = New Timer
        foTimer.Interval = 1000
        foTimer.Enabled = True
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        goLogger.StopUEH()
        fiMonitorState = MonitorState.Stopping
        foTimer.Enabled = False
        If moCheckThread IsNot Nothing Then
            Try
                If moCheckThread.IsAlive Then
                    moCheckThread.Abort()
                End If
            Catch ex As Exception
                ' don't care 
            Finally
                moCheckThread = Nothing
            End Try
        End If
        If foInstallThread IsNot Nothing Then
            Try
                If foInstallThread.IsAlive Then
                    foInstallThread.Abort()
                End If
            Catch ex As Exception
                ' don't care
            Finally
                foInstallThread = Nothing
            End Try
        End If
    End Sub

    Private Sub foTimer_Tick(sender As Object, e As EventArgs) Handles foTimer.Tick
        Select Case fiMonitorState
            Case MonitorState.Stopping
                Exit Sub

            Case MonitorState.Running

            Case MonitorState.Loading
                foTimer.Enabled = False
                Call LoadGui()
        End Select
    End Sub

    Private Sub lvwServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwServers.SelectedIndexChanged
        Call SetMenuOptions()
    End Sub

    Private Sub ServerEdit_EditComplete(SaveChanges As Boolean, ByRef oServ As clsServer)
        If SaveChanges Then
            ' new or not 
            For Each poLvItem As ListViewItem In lvwServers.Items
                If oServ.Equals(poLvItem.Tag) Then
                    ' update this one 

                    Exit Sub
                End If
            Next
            foInstallThread = New System.Threading.Thread(AddressOf InstallServer)
            foInstallThread.IsBackground = True
            foInstallThread.Start(oServ)
        End If
    End Sub




    Private Sub InstallServer(ByVal oServer As clsServer)
        Dim pbContinue As Boolean = True

        Call UpdateStatus("Installing server to " & oServer.FolderPath)

        Try
            ' validate install directory
            If Not System.IO.Directory.Exists(oServer.FolderPath) Then
                Try
                    System.IO.Directory.CreateDirectory(oServer.FolderPath)
                Catch ex As Exception
                    MsgBox("Failed to create directory: " & oServer.FolderPath, giModalCritOK, "Cannot install server.")
                    pbContinue = False
                End Try
            End If
            ' validate save directory
            If pbContinue And Not System.IO.Directory.Exists(oServer.SaveDir) Then
                Try
                    System.IO.Directory.CreateDirectory(oServer.SaveDir)
                Catch ex As Exception
                    MsgBox("Failed to create save directory: " & oServer.SaveDir, giModalCritOK, "Cannot install server.")
                    pbContinue = False
                End Try
            End If

            ' make sure steamcmd is there
            If pbContinue Then
                If oServer.HasSteamCmd = False Then
                    Call UpdateStatus("Downloading SteamCMD.exe to " & oServer.FolderPath)
                    pbContinue = oServer.DownloadSteam
                End If
            End If
            ' start install process
            If pbContinue Then
                Dim processStartInfo As System.Diagnostics.ProcessStartInfo
                Dim psArgLine As String

                psArgLine = "+login anonymous +force_install_dir """ & oServer.FolderPath & """ +app_update 896660 validate +exit"

                processStartInfo = New System.Diagnostics.ProcessStartInfo()
                processStartInfo.FileName = System.IO.Path.Combine(oServer.FolderPath, "steamcmd.exe")
                processStartInfo.Arguments = psArgLine

                processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                processStartInfo.UseShellExecute = True
                processStartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(oServer.FolderPath)

                Call UpdateStatus("Downloading game files from Steam, please wait...")

                Using poInstProcess As System.Diagnostics.Process = System.Diagnostics.Process.Start(processStartInfo)
                    poInstProcess.WaitForExit(900000) ' 15 mintues
                    If poInstProcess.ExitCode <> 0 And poInstProcess.ExitCode <> 7 Then
                        MsgBox("Warning: Install exit code was " & poInstProcess.ExitCode & " not 0 or 7.", giModalExclOK, "Install May Have Failed")
                    End If
                End Using
            End If
            ' save server to config and listview
            If pbContinue Then
                Dim poDR As DataRow = goConfig.ServerData.NewRow
                poDR.Item("ServerFolder") = oServer.FolderPath
                poDR.Item("ServerName") = oServer.ServerName
                poDR.Item("ServerPort") = oServer.Port
                poDR.Item("ServerWorld") = oServer.WorldName
                poDR.Item("ServerPassword") = oServer.Password
                poDR.Item("ServerSaveDir") = oServer.SaveDir
                poDR.Item("UpdateOnRestart") = oServer.UpdateServer
                poDR.Item("Restart") = oServer.RestartServer
                poDR.Item("RestartHour") = oServer.RestartHour
                poDR.Item("RestartMin") = oServer.RestartMin
                poDR.Item("Backup") = oServer.BackupServer
                goConfig.ServerData.Rows.Add(poDR)
                pbContinue = goConfig.SaveConfigFile()
            End If
            If pbContinue Then
                Call AddToListView(oServer)
            End If

        Catch ex As Exception
            goLogger.LogException("frmMain.InstallServer", ex)
            MsgBox("Warning: Install failed, see error log for more information.")

        Finally
            Call UpdateStatus("Ready")
            foInstallThread = Nothing
        End Try
    End Sub

    Private Sub ServerFound(ByVal oServer As clsServer)


    End Sub



    Private Sub SetMenuOptions()
        btnAdd.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnStart.Enabled = False
        btnStop.Enabled = False
        btnBackup.Enabled = False
        btnRestore.Enabled = False
        btnUpdate.Enabled = False

        If lvwServers.SelectedItems.Count > 0 Then
            Dim poServ As clsServer = lvwServers.SelectedItems(0).Tag
            If poServ IsNot Nothing Then
                If poServ.CurrentState = clsServer.StateVals.Running Then
                    btnStop.Enabled = True
                Else
                    btnEdit.Enabled = True
                    btnDelete.Enabled = True
                    btnStart.Enabled = True
                    If poServ.HasBackups Then
                        btnRestore.Enabled = True
                    End If
                    If poServ.HasGameFiles Then
                        btnBackup.Enabled = True
                    End If
                    btnUpdate.Enabled = True
                End If
            End If
        End If
    End Sub


    Private Sub LoadGui()
        tslStatus.Text = "Initializing..."

        With imlListview
            .ImageSize = New Size(24, 24)
            .Images.Add("clear", My.Resources.bullet_ball_glass_grey)
            .Images.Add("green", My.Resources.bullet_ball_glass_green)
            .Images.Add("blue", My.Resources.bullet_ball_glass_blue)
            .Images.Add("red", My.Resources.bullet_ball_glass_red)
            .Images.Add("yellow", My.Resources.bullet_ball_glass_yellow)
        End With

        With lvwServers
            .View = View.Details
            .MultiSelect = False
            .FullRowSelect = True
            .GridLines = True
            .Items.Clear()
            .SmallImageList = imlListview
            .Columns.Clear()
            .Columns.Add("STATE", "State", 50)
            .Columns.Add("PID", "PID", 80)
            .Columns.Add("VER", "Version", 100)
            .Columns.Add("NAME", "Name", 300)
            .Columns.Add("FOLDER", "Folder", 250)
            .SmallImageList = imlListview
        End With

        Call SetMenuOptions()


        UpdateStatus("Loading server list...")
        ' thread the load 
        Dim poLoadThread As New System.Threading.Thread(AddressOf LoadServerList)
        poLoadThread.IsBackground = True
        poLoadThread.Start()
    End Sub

    Private Sub LoadServerList()


        If goConfig.ServerCount = 0 Then
            Call SetNoData()
        Else
            ' load server list from config to server classes
            For Each poDR As DataRow In goConfig.ServerData.Rows
                Dim psFolder As String = SafeStr(poDR.Item("ServerFolder"))
                Dim psName As String = SafeStr(poDR.Item("ServerName"))
                Dim piPort As Integer = SafeInt(poDR.Item("ServerPort"))
                Dim psWorld As String = SafeStr(poDR.Item("ServerWorld"))
                Dim psPass As String = SafeStr(poDR.Item("ServerPassword"))
                Dim psSaveDir As String = SafeStr(poDR.Item("ServerSaveDir"))
                Dim pbUdpate As Boolean = SafeBool(poDR.Item("UpdateOnRestart"))
                Dim pbRestart As Boolean = SafeBool(poDR.Item("Restart"))
                Dim piHour As Integer = SafeInt(poDR.Item("RestartHour"))
                Dim piMin As Integer = SafeInt(poDR.Item("RestartHour"))
                Dim pbBackup As Boolean = poDR.Item("Backup")
                Dim poServer As New clsServer(modMain.BackupFolderPath, psName, psFolder, piPort, psWorld, psPass, psSaveDir, pbUdpate, pbRestart, pbBackup, piHour, piMin)
                AddToListView(poServer)
            Next
        End If

        UpdateStatus("Server list loaded.")
    End Sub





    Private Sub MontiorServers()
        Try
            'For Each poServ As clsServer In foServerList
            '    poServ.CheckStatus()
            'Next

        Catch ThreadEx As System.Threading.ThreadAbortException
            ' don't care, just exit 

        Catch ex As Exception
            Dim poWin32 As System.ComponentModel.Win32Exception = TryCast(ex, System.ComponentModel.Win32Exception)
            If poWin32 IsNot Nothing Then
                If Not moLoggedErr.Contains(poWin32.ErrorCode) Then
                    goLogger.LogException("frmMain.MonitorServers", ex)
                    moLoggedErr.Add(poWin32.ErrorCode)
                End If
            End If
        Finally
            moCheckThread = Nothing
        End Try
    End Sub


    Private Delegate Sub Del_UpdateStatus(ByVal Message As String)
    Private Sub UpdateStatus(ByVal Message As String)
        If sstStatus.InvokeRequired Then
            Dim poCB As New Del_UpdateStatus(AddressOf UpdateStatus)
            Dim poParm(0) As Object
            poParm(0) = Message
            sstStatus.Invoke(poCB, poParm)
        Else
            tslStatus.Text = Message
            Application.DoEvents()
        End If
    End Sub

    Private Delegate Sub Del_SetNoData()
    Private Sub SetNoData()
        If lvwServers.InvokeRequired Then
            lvwServers.Invoke(New Del_SetNoData(AddressOf SetNoData))
        Else
            lvwServers.SuspendLayout()
            lvwServers.Items.Clear()
            lvwServers.GridLines = False
            modMain.ListviewNoDataImage(lvwServers, "No game servers added.")
            lvwServers.ResumeLayout(True)
        End If
    End Sub

    Private Delegate Sub Del_AddToListView(oServer As clsServer)
    Private Sub AddToListView(oServer As clsServer)
        If lvwServers.InvokeRequired Then
            lvwServers.Invoke(New Del_AddToListView(AddressOf AddToListView), New Object() {oServer})
        Else
            AddHandler oServer.ServerFound, AddressOf ServerFound
            lvwServers.SuspendLayout()
            Dim poLvw As New ListViewItem
            poLvw.Tag = oServer
            poLvw.Text = ""
            poLvw.ImageKey = "clear"
            poLvw.SubItems.Add(oServer.PID)
            poLvw.SubItems.Add(oServer.ExeVersion)
            poLvw.SubItems.Add(oServer.ServerName)
            poLvw.SubItems.Add(oServer.FolderPath)
            lvwServers.Items.Add(poLvw)
            lvwServers.BackgroundImage = Nothing
            lvwServers.GridLines = True
            lvwServers.ResumeLayout(True)
        End If
    End Sub



End Class






'