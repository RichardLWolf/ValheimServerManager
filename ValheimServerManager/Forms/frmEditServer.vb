Public Class frmEditServer
    Public Event EditComplete(ByVal SaveChanges As Boolean, ByRef oServ As clsServer)


    Private foServ As clsServer
    Private foOrigServ As clsServer

    Public Sub ReadyForm(ByVal oServ As clsServer, ByVal bEditing As Boolean)
        foServ = oServ

        foOrigServ = New clsServer(oServ.BackupPath, oServ.FolderPath, oServ.ServerName, oServ.Port, oServ.WorldName _
                                   , oServ.Password, oServ.PresetValue, oServ.ModCombatValue, oServ.ModDeathValue _
                                   , oServ.ModResources, oServ.ModRaids, oServ.ModPortals, oServ.SaveDir, oServ.UpdateServer _
                                   , oServ.RestartServer, oServ.PublicFlag, oServ.BackupServer, oServ.RestartHour, oServ.RestartMin)
        foOrigServ.CreationGUID = oServ.CreationGUID

        txtServerName.Text = foServ.ServerName
        txtWorldName.Text = foServ.WorldName
        txtPassword.Text = foServ.Password
        txtPort.Text = foServ.Port
        txtInstallPath.Text = foServ.FolderPath
        txtDataPath.Text = foServ.SaveDir
        chkPublic.Checked = foServ.PublicFlag
        chkRestart.Checked = foServ.RestartServer
        chkUpdate.Checked = foServ.UpdateServer
        chkBackup.Checked = foServ.BackupServer

        cboPreset.Items.Clear()
        cboPreset.Items.Add("<None>")
        cboPreset.Items.Add("normal")
        cboPreset.Items.Add("casual")
        cboPreset.Items.Add("easy")
        cboPreset.Items.Add("hard")
        cboPreset.Items.Add("hardcore")
        cboPreset.Items.Add("immersive")
        cboPreset.Items.Add("hammer")

        cboModCombat.Items.Clear()
        cboModCombat.Items.Add("<Default>")
        cboModCombat.Items.Add("veryeasy")
        cboModCombat.Items.Add("easy")
        cboModCombat.Items.Add("hard")
        cboModCombat.Items.Add("veryhard")

        cboModDeath.Items.Clear()
        cboModDeath.Items.Add("<Default>")
        cboModDeath.Items.Add("casual")
        cboModDeath.Items.Add("veryeasy")
        cboModDeath.Items.Add("easy")
        cboModDeath.Items.Add("hard")
        cboModDeath.Items.Add("hardcore")

        cboModResources.Items.Clear()
        cboModResources.Items.Add("<Default>")
        cboModResources.Items.Add("muchless")
        cboModResources.Items.Add("less")
        cboModResources.Items.Add("more")
        cboModResources.Items.Add("muchmore")
        cboModResources.Items.Add("most")

        cboModRaids.Items.Clear()
        cboModRaids.Items.Add("<Default>")
        cboModRaids.Items.Add("none")
        cboModRaids.Items.Add("muchless")
        cboModRaids.Items.Add("less")
        cboModRaids.Items.Add("more")
        cboModRaids.Items.Add("muchmore")
        cboModRaids.Items.Add("most")

        cboModPortals.Items.Clear()
        cboModPortals.Items.Add("<Default>")
        cboModPortals.Items.Add("casual")
        cboModPortals.Items.Add("hard")
        cboModPortals.Items.Add("veryhard")

        cboPreset.SelectedIndex = 0
        If foServ.PresetValue <> "" Then
            cboPreset.SelectedIndex = FindObjectIndex(cboPreset, foServ.PresetValue)
        End If

        cboModCombat.SelectedIndex = 0
        If foServ.ModCombatValue <> "" Then
            cboModCombat.SelectedIndex = FindObjectIndex(cboModCombat, foServ.ModCombatValue)
        End If

        cboModDeath.SelectedIndex = 0
        If foServ.ModDeathValue <> "" Then
            cboModDeath.SelectedIndex = FindObjectIndex(cboModDeath, foServ.ModDeathValue)
        End If

        cboModResources.SelectedIndex = 0
        If foServ.ModResources <> "" Then
            cboModResources.SelectedIndex = FindObjectIndex(cboModResources, foServ.ModResources)
        End If

        cboModRaids.SelectedIndex = 0
        If foServ.ModRaids <> "" Then
            cboModRaids.SelectedIndex = FindObjectIndex(cboModRaids, foServ.ModRaids)
        End If

        cboModPortals.SelectedIndex = 0
        If foServ.ModPortals <> "" Then
            cboModPortals.SelectedIndex = FindObjectIndex(cboModPortals, foServ.ModPortals)
        End If

        dteRestartTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, foServ.RestartHour, foServ.RestartMin, 0, 0)
        Call chkRestart_CheckedChanged(Nothing, Nothing)
        Call txtPort_TextChanged(Nothing, Nothing)
        Call txtServerName_TextChanged(Nothing, Nothing)
        If bEditing Then
            txtInstallPath.Enabled = False
            txtDataPath.Enabled = False
            btnInstBrowse.Enabled = False
            btnSaveBrowse.Enabled = False
            btnSave.Text = "SA&VE"
        Else
            txtInstallPath.Enabled = True
            txtDataPath.Enabled = True
            btnInstBrowse.Enabled = True
            btnSaveBrowse.Enabled = True
            btnSave.Text = "INS&TALL"
        End If
    End Sub

    Public ReadOnly Property OriginalServerValues As clsServer
        Get
            Return foOrigServ
        End Get
    End Property


    Private Sub frmEditServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.vsm
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RaiseEvent EditComplete(False, foServ)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim psServer As String = txtServerName.Text.Trim
        Dim psFolder As String = txtInstallPath.Text.Trim
        Dim piPort As Integer

        If txtPassword.Text.Trim.Length < 6 Or txtServerName.Text.Trim.IndexOf(txtPassword.Text.Trim, StringComparison.InvariantCultureIgnoreCase) > -1 Then
            MsgBox("Password must be at least six digits and may not contain server name.", giModalExclOK, "Invalid Password")
            Exit Sub
        End If

        Integer.TryParse(txtPort.Text.Trim, piPort)

        foServ.ServerName = psServer
        foServ.FolderPath = psFolder
        foServ.Port = piPort
        foServ.WorldName = txtWorldName.Text.Trim
        foServ.Password = txtPassword.Text.Trim
        foServ.SaveDir = txtDataPath.Text.Trim
        foServ.PublicFlag = chkPublic.Checked
        foServ.RestartServer = chkRestart.Checked
        foServ.BackupServer = chkBackup.Checked
        foServ.UpdateServer = chkUpdate.Checked
        foServ.RestartHour = dteRestartTime.Value.Hour
        foServ.RestartMin = dteRestartTime.Value.Minute
        ' Modifier settings
        If cboPreset.SelectedIndex < 1 Then
            foServ.PresetValue = ""
        Else
            foServ.PresetValue = cboPreset.Items(cboPreset.SelectedIndex).ToString
        End If

        If cboModCombat.SelectedIndex < 1 Then
            foServ.ModCombatValue = ""
        Else
            foServ.ModCombatValue = cboModCombat.Items(cboModCombat.SelectedIndex).ToString
        End If

        If cboModDeath.SelectedIndex < 1 Then
            foServ.ModDeathValue = ""
        Else
            foServ.ModDeathValue = cboModDeath.Items(cboModDeath.SelectedIndex).ToString
        End If

        If cboModResources.SelectedIndex < 1 Then
            foServ.ModResources = ""
        Else
            foServ.ModResources = cboModResources.Items(cboModResources.SelectedIndex).ToString
        End If

        If cboModRaids.SelectedIndex < 1 Then
            foServ.ModRaids = ""
        Else
            foServ.ModRaids = cboModRaids.Items(cboModRaids.SelectedIndex).ToString
        End If

        If cboModPortals.SelectedIndex < 1 Then
            foServ.ModPortals = ""
        Else
            foServ.ModPortals = cboModPortals.Items(cboModPortals.SelectedIndex).ToString
        End If


        RaiseEvent EditComplete(True, foServ)
        Me.Close()
    End Sub

    Private Sub btnShowHide_Click(sender As Object, e As EventArgs) Handles btnShowHide.Click
        If txtPassword.PasswordChar = "●" Then
            txtPassword.PasswordChar = ""
        Else
            txtPassword.PasswordChar = "●"
        End If
    End Sub

    Private Sub chkRestart_CheckedChanged(sender As Object, e As EventArgs) Handles chkRestart.CheckedChanged
        dteRestartTime.Enabled = chkRestart.Checked
        chkBackup.Enabled = chkRestart.Checked
        chkUpdate.Enabled = chkRestart.Checked
    End Sub

    Private Sub btnInstBrowse_Click(sender As Object, e As EventArgs) Handles btnInstBrowse.Click
        Using poDlg As New FolderBrowserDialog
            Dim psPath As String = txtInstallPath.Text.Trim
            poDlg.SelectedPath = psPath
            poDlg.Description = "Select the folder to install the Valheim server files."
            poDlg.ShowNewFolderButton = True
            If poDlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                txtInstallPath.Text = poDlg.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnSaveBrowse_Click(sender As Object, e As EventArgs) Handles btnSaveBrowse.Click
        Using poDlg As New FolderBrowserDialog
            Dim psPath As String = txtDataPath.Text.Trim
            poDlg.SelectedPath = psPath
            poDlg.Description = "Select the folder to store the Valheim world data files (note: program will add 'worlds' to this path automatically)."
            poDlg.ShowNewFolderButton = True
            If poDlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                txtDataPath.Text = poDlg.SelectedPath
            End If
        End Using
    End Sub

    Private Sub txtPort_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        Dim piPort As Integer
        Integer.TryParse(txtPort.Text.Trim, piPort)
        If piPort = 0 Then
            lblPortRange.Text = "invalid port"
        Else
            lblPortRange.Text = String.Format("{0:#0} - {1:#0} (query {2:#0})", piPort, piPort + 2, piPort + 1)
        End If
    End Sub

    Private Sub txtServerName_TextChanged(sender As Object, e As EventArgs) Handles txtServerName.TextChanged
        Me.Text = "Server - " & txtServerName.Text.Trim
    End Sub


    Public Function FindObjectIndex(ByVal oCombo As ComboBox, ByVal oValue As Object) As Integer
        Dim piIndex As Integer

        FindObjectIndex = -1 'default not found 

        For piIndex = 0 To oCombo.Items.Count - 1
            Try
                If oCombo.Items(piIndex).Equals(oValue) Then
                    FindObjectIndex = piIndex
                    Exit For
                End If
            Catch
                Exit For
            End Try
        Next

    End Function

End Class