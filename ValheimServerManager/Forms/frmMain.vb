Imports NetFwTypeLib
Imports ValheimServerManager

Public Class frmMain
    Private moLoggedErr As New List(Of Integer)

    Private foInstallThread As System.Threading.Thread




    Private Sub btnAbout_Click(sender As Object, e As EventArgs) Handles btnAbout.Click
        frmAbout.Show(Me)
    End Sub

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
        psSaveDir = System.IO.Path.Combine(psFolder, "savedata")

        Dim poNewServ As New clsServer(modMain.BackupFolderPath, psFolder, psServer, 2456, "My New World", modMain.RandomPassword(10), psSaveDir, False, True, True, 3, 0)
        poNewServ.AutoStart = False

        poFrm.ReadyForm(poNewServ, False)
        AddHandler poFrm.EditComplete, AddressOf ServerAdd_EditComplete

        poFrm.Show(Me)
    End Sub

    Private Sub btnAppFolder_Click(sender As Object, e As EventArgs) Handles btnAppFolder.Click
        Try
            Process.Start("explorer.exe", "/root," & Chr(34) & modMain.RootFolderPath & "\" & Chr(34))
        Catch ex As Exception
            MsgBox("Unable to open app folder: " & modMain.RootFolderPath, giModalExclOK, "Error starting explorer.")
        End Try
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState = clsServer.StateVals.Stopped Then
                    poTag.DoBackup()
                End If
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                Dim psTemp As String = "Are you sure you wish to remove this server entry?" & vbCrLf
                psTemp = psTemp & "Note that you will need to manually remove the program " & vbCrLf
                psTemp = psTemp & "folder yourself, this program will not delete it." & vbCrLf & vbCrLf
                psTemp = psTemp & "REMOVE SERVER ENTRY?"
                If MsgBox(psTemp, giModalExclYesNo, "Confirm Removal") = MsgBoxResult.Yes Then
                    For Each poRow As DataRow In goConfig.ServerData.Rows
                        If poRow("GUID") = poTag.CreationGUID Then
                            poRow.Delete()
                            goConfig.SaveConfigFile()
                            lvwServers.Items.Remove(lvwServers.SelectedItems(0))
                            lvwServers.Refresh()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                Dim poFrm As New frmEditServer
                poFrm.ReadyForm(poTag, True)
                AddHandler poFrm.EditComplete, AddressOf ServerEdit_EditComplete
                poFrm.Show(Me)
            End If
        End If
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState = clsServer.StateVals.Stopped Then
                    Using poDlg As New OpenFileDialog
                        poDlg.Title = "Select world backup to restore"
                        poDlg.RestoreDirectory = False
                        poDlg.InitialDirectory = poTag.BackupPath
                        poDlg.FileName = poTag.CreationGUID & "*.zip"
                        poDlg.Filter = "Zip Files|*.zip|All Files|*.*"
                        poDlg.FilterIndex = 0
                        poDlg.Multiselect = False
                        If poDlg.ShowDialog(Me) = DialogResult.OK Then
                            poTag.DoRestore(poDlg.FileName)
                        End If
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState = clsServer.StateVals.Stopped Or poTag.CurrentState = clsServer.StateVals.Undetermined Then
                    poTag.AutoStart = True
                    poTag.StartMonitor()
                End If
            End If
        End If
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                poTag.AutoStart = False
                poTag.StopMonitor()
            End If
        End If
    End Sub

    Private Sub btnViewServerFolder_Click(sender As Object, e As EventArgs) Handles btnViewServerFolder.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                Try
                    Process.Start("explorer.exe", "/root," & Chr(34) & poTag.FolderPath & "\" & Chr(34))
                Catch ex As Exception
                    MsgBox("Unable to open server folder: " & poTag.FolderPath, giModalExclOK, "Error starting explorer.")
                End Try
            End If
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState = clsServer.StateVals.Stopped Then
                    poTag.DoUpdate()
                End If
            End If
        End If
    End Sub


    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.vsm
        Me.Text = "VSM v" & Application.ProductVersion

        modMain.Initialize()
        Call LoadGui()
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        goLogger.StopUEH()
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

    Private Sub lvwServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwServers.SelectedIndexChanged
        Call SetMenuOptions()
    End Sub

    Private Sub mnuPopInfo_Click(sender As Object, e As EventArgs) Handles mnuPopInfo.Click
        If lvwServers.SelectedItems.Count > 0 Then
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState = clsServer.StateVals.Running Then
                    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                    Application.DoEvents()
                    Dim poInfo As SteamQueryNet.Models.ServerInfo = poTag.GetServerInfo
                    Dim poPlayers As List(Of SteamQueryNet.Models.Player) = poTag.GetPlayerInfo
                    Me.Cursor = System.Windows.Forms.Cursors.Arrow
                    If poInfo IsNot Nothing Then
                        Dim poSB As New System.Text.StringBuilder
                        poSB.Append("Steam ID: ").Append(poInfo.SteamID).Append(vbCrLf)
                        poSB.Append("Game ID: ").Append(poInfo.GameID).Append(vbCrLf)
                        poSB.Append("Game: ").Append(poInfo.Game).Append(vbCrLf)
                        poSB.Append("Version: ").Append(poInfo.Version).Append(vbCrLf)
                        poSB.Append("Server Type: ").Append(poInfo.ServerType).Append(vbCrLf)
                        poSB.Append("# of Players: ").Append(poPlayers.Count).Append(vbCrLf)
                        MsgBox(poSB.ToString, giModalInfoOK, "Server Info")
                    Else
                        MsgBox("Failed to get server information from Steam client.", giModalExclOK, "No Data")
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub mnuPop_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles mnuPop.Opening
        If lvwServers.SelectedItems.Count = 0 Then
            e.Cancel = True
        Else
            Dim poTag As clsServer = lvwServers.SelectedItems(0).Tag
            If poTag IsNot Nothing Then
                If poTag.CurrentState <> clsServer.StateVals.Running Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub



    Private Sub ServerAdd_EditComplete(SaveChanges As Boolean, ByRef oServ As clsServer)
        If SaveChanges Then
            foInstallThread = New System.Threading.Thread(AddressOf InstallServer)
            foInstallThread.IsBackground = True
            foInstallThread.Start(oServ)
        End If
    End Sub

    Private Sub ServerEdit_EditComplete(SaveChanges As Boolean, ByRef oServ As clsServer)
        If SaveChanges Then
            For Each poRow As DataRow In goConfig.ServerData.Rows
                If poRow("GUID") = oServ.CreationGUID Then
                    'poRow.Item("ServerFolder") = oServ.FolderPath
                    'poRow.Item("ServerSaveDir") = oServ.SaveDir
                    poRow.Item("ServerName") = oServ.ServerName
                    poRow.Item("ServerPort") = oServ.Port
                    poRow.Item("ServerWorld") = oServ.WorldName
                    poRow.Item("ServerPassword") = oServ.Password
                    poRow.Item("UpdateOnRestart") = oServ.UpdateServer
                    poRow.Item("Restart") = oServ.RestartServer
                    poRow.Item("RestartHour") = oServ.RestartHour
                    poRow.Item("RestartMin") = oServ.RestartMin
                    poRow.Item("Backup") = oServ.BackupServer
                    goConfig.SaveConfigFile()
                    UpdateListView(oServ)
                End If
            Next
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
            If pbContinue And Not System.IO.Directory.Exists(oServer.WorldDataFolder) Then
                Try
                    System.IO.Directory.CreateDirectory(oServer.WorldDataFolder)
                Catch ex As Exception
                    MsgBox("Failed to create save directory: " & oServer.WorldDataFolder, giModalCritOK, "Cannot install server.")
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
                poDR.Item("GUID") = oServer.CreationGUID
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
            ' check firewall 
            If pbContinue Then
                Dim psPorts As String = String.Format("{0:#0}-{1:#0}", oServer.Port, oServer.Port + 2)
                Dim psErr As String = ""
                Dim Profile2Types As NetFwTypeLib.NET_FW_PROFILE_TYPE2_ = (NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_DOMAIN Or NET_FW_PROFILE_TYPE2_.NET_FW_PROFILE2_PUBLIC)
                If Not clsFireWall.CheckAddPortRule("Valheim Dedicated Server (TCP " & psPorts & ")", psPorts, clsFireWall.ProtocolType.Tcp, Profile2Types) Then
                    psErr = psErr & "Failed to add TCP port range " & psPorts & " to Windows Firewall." & vbCrLf
                End If
                If Not clsFireWall.CheckAddPortRule("Valheim Dedicated Server (UDP " & psPorts & ")", psPorts, clsFireWall.ProtocolType.Udp, Profile2Types) Then
                    psErr = psErr & "Failed to add UDP port range " & psPorts & " to Windows Firewall." & vbCrLf
                End If
                If psErr <> "" Then
                    psErr = psErr & vbCrLf & "Most likely you are not running this program elevated or are logged in as a Windows Limited User account." & vbCrLf & vbCrLf
                    psErr = psErr & "You will need to verfiy the Windows Firewall settings for this server manually."
                    MsgBox(psErr, giModalInfoOK, "Check Windows Firewall")
                End If
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





    Private Sub ServerStatus(ByVal oServer As clsServer)
        Call UpdateListView(oServer)
    End Sub


    Private Sub SetMenuOptions()
        btnAdd.Enabled = True
        btnEdit.Enabled = False
        btnDelete.Enabled = False
        btnStart.Enabled = False
        btnStop.Enabled = False
        btnViewServerFolder.Enabled = False
        btnBackup.Enabled = False
        btnRestore.Enabled = False
        btnUpdate.Enabled = False

        If lvwServers.SelectedItems.Count > 0 Then
            Call UpdateStatus("Ready")
            Dim poServ As clsServer = lvwServers.SelectedItems(0).Tag
            If poServ IsNot Nothing Then
                btnViewServerFolder.Enabled = True
                If poServ.CurrentState = clsServer.StateVals.Running Then
                    btnStop.Enabled = True
                    If poServ.RestartServer Then
                        Dim pdNext As Date = poServ.NextRestart
                        If pdNext <> Nothing Then
                            Dim pdTarget As TimeSpan = pdNext.Subtract(DateTime.Now)
                            Call UpdateStatus("Server scheduled for restart in " & FullDisplayElapsed(pdTarget.TotalSeconds))
                        End If
                    End If
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

    Private Sub FindIpAdds(oThread As System.Threading.Thread)
        Dim psLocalIP As String = "n/a"
        Dim psExtIP As String = "n/a"

        Try
            Dim poIPs As List(Of System.Net.IPAddress)
            poIPs = modMain.GetIPAddresses
            Dim poLocalIP As System.Net.IPAddress = modMain.FindPrivateIP(poIPs)
            psLocalIP = poLocalIP.ToString
            psExtIP = modMain.GetExternalIp

        Catch ex As Exception
            goLogger.LogException("frmMain.FindIpAdds", ex)

        Finally
            Call UpdateIPs(psLocalIP, psExtIP)
            oThread = Nothing
        End Try
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
            .Columns.Add("STATE", "State", 110)
            .Columns.Add("PID", "PID", 50)
            .Columns.Add("VER", "Version", 110)
            .Columns.Add("PORTS", "Ports", 120)
            .Columns.Add("WORLD", "World Name", 200)
            .Columns.Add("NAME", "Name", 200)
            .Columns.Add("FOLDER", "Folder", 200)
            .SmallImageList = imlListview
        End With

        Call SetMenuOptions()

        ' get our IP addresses
        Dim poIPThread As New System.Threading.Thread(AddressOf FindIpAdds)
        poIPThread.IsBackground = True
        poIPThread.Start(poIPThread)

        ' Load list of servers
        UpdateStatus("Loading server list...")

        ' thread the load 
        Dim poLoadThread As New System.Threading.Thread(AddressOf LoadServerList)
        poLoadThread.IsBackground = True
        poLoadThread.Start()
    End Sub

    Private Sub LoadServerList()
        Dim poList As New List(Of clsServer)
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
                Dim poServer As New clsServer(modMain.BackupFolderPath, psFolder, psName, piPort, psWorld, psPass, psSaveDir, pbUdpate, pbRestart, pbBackup, piHour, piMin)
                poServer.CreationGUID = SafeStr(poDR.Item("GUID"))
                poServer.AutoStart = True
                AddToListView(poServer)
                poList.Add(poServer)
            Next
        End If
        UpdateStatus("Server list loaded, starting servers...")
        For Each poServ As clsServer In poList
            If poServ.CurrentState = clsServer.StateVals.Stopped Or poServ.CurrentState = clsServer.StateVals.Undetermined Then
                poServ.StartMonitor()
            End If
        Next
        UpdateStatus("Ready")
    End Sub



    Private Delegate Function Del_GetServerList() As List(Of clsServer)
    Private Function GetServerList() As List(Of clsServer)
        If lvwServers.InvokeRequired Then
            Return lvwServers.Invoke(New Del_GetServerList(AddressOf GetServerList))
        Else
            Dim poServs As New List(Of clsServer)
            For Each poLvwItem As ListViewItem In lvwServers.Items
                poServs.Add(poLvwItem.Tag)
            Next
            Return poServs
        End If
    End Function


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

    Private Delegate Sub Del_UpdateIPs(ByVal LocalIP As String, ByVal ExternalIP As String)
    Private Sub UpdateIPs(ByVal LocalIP As String, ByVal ExternalIP As String)
        If sstStatus.InvokeRequired Then
            sstStatus.Invoke(New Del_UpdateIPs(AddressOf UpdateIPs), New Object() {LocalIP, ExternalIP})
        Else
            tslLocalIP.Text = LocalIP
            tslExternalIP.Text = ExternalIP
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
            AddHandler oServer.ServerStatus, AddressOf ServerStatus
            lvwServers.SuspendLayout()
            Dim poLvw As New ListViewItem
            poLvw.Tag = oServer
            poLvw.Text = "Undetermined"
            poLvw.ImageKey = "clear"
            poLvw.SubItems.Add(IIf(oServer.PID = 0, "", oServer.PID))
            poLvw.SubItems.Add(oServer.ExeVersion)
            poLvw.SubItems.Add(String.Format("{0:#0}-{1:#0}", oServer.Port, oServer.Port + 2))
            poLvw.SubItems.Add(oServer.WorldName)
            poLvw.SubItems.Add(oServer.ServerName)
            poLvw.SubItems.Add(oServer.FolderPath)
            lvwServers.Items.Add(poLvw)
            lvwServers.BackgroundImage = Nothing
            lvwServers.GridLines = True
            lvwServers.ResumeLayout(True)
        End If
    End Sub

    Private Delegate Sub Del_UpdateListView(oServer As clsServer)
    Private Sub UpdateListView(oServer As clsServer)
        If lvwServers.InvokeRequired Then
            lvwServers.Invoke(New Del_UpdateListView(AddressOf UpdateListView), New Object() {oServer})
        Else
            For Each poLvItem As ListViewItem In lvwServers.Items
                Dim poTag As clsServer = poLvItem.Tag
                If poTag.CreationGUID = oServer.CreationGUID Then
                    lvwServers.SuspendLayout()
                    poLvItem.Tag = oServer
                    Select Case oServer.CurrentState
                        Case clsServer.StateVals.BackingUp
                            poLvItem.Text = "Backing up"
                            poLvItem.ImageKey = "yellow"
                        Case clsServer.StateVals.Restoring
                            poLvItem.Text = "Restoring backup"
                            poLvItem.ImageKey = "yellow"
                        Case clsServer.StateVals.Running
                            poLvItem.Text = "Running"
                            poLvItem.ImageKey = "green"
                        Case clsServer.StateVals.Starting
                            poLvItem.Text = "Starting"
                            poLvItem.ImageKey = "blue"
                        Case clsServer.StateVals.Stopped
                            poLvItem.Text = "Stopped"
                            poLvItem.ImageKey = "red"
                        Case clsServer.StateVals.Updating
                            poLvItem.Text = "Updating"
                            poLvItem.ImageKey = "yellow"
                        Case Else
                            poLvItem.Text = "Undetermined"
                            poLvItem.ImageKey = "clear"
                    End Select
                    poLvItem.SubItems(1).Text = IIf(oServer.PID = 0, "", oServer.PID)
                    poLvItem.SubItems(2).Text = oServer.ExeVersion
                    poLvItem.SubItems(3).Text = String.Format("{0:#0}-{1:#0}", oServer.Port, oServer.Port + 2)
                    poLvItem.SubItems(4).Text = oServer.WorldName
                    poLvItem.SubItems(5).Text = oServer.ServerName
                    poLvItem.SubItems(6).Text = oServer.FolderPath
                    lvwServers.BackgroundImage = Nothing
                    lvwServers.GridLines = True
                    lvwServers.ResumeLayout(True)
                    If oServer.LastBackupResult = False Then
                        Call UpdateStatus("Last backup job failed.")
                    Else
                        If oServer.LastRestoreResult = False Then
                            Call UpdateStatus("Last restore job failed.")
                        End If
                    End If
                    Call lvwServers_SelectedIndexChanged(Nothing, Nothing)
                End If
            Next
        End If
    End Sub

End Class






'