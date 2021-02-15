Public Class frmEditServer
    Public Event EditComplete(ByVal SaveChanges As Boolean, ByRef oServ As clsServer)


    Private foServ As clsServer


    Public Sub ReadyForm(oServ As clsServer)
        foServ = oServ

        txtServerName.Text = foServ.ServerName
        txtWorldName.Text = foServ.WorldName
        txtPassword.Text = foServ.Password
        txtPort.Text = foServ.Port
        txtInstallPath.Text = foServ.FolderPath
        txtDataPath.Text = foServ.SaveDir
        chkRestart.Checked = foServ.RestartServer
        chkUpdate.Checked = foServ.UpdateServer
        chkBackup.Checked = foServ.BackupServer
        dteRestartTime.Value = New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, foServ.RestartHour, foServ.RestartMin, 0, 0)
        Call chkRestart_CheckedChanged(Nothing, Nothing)
        Call txtPort_TextChanged(Nothing, Nothing)
        Call txtServerName_TextChanged(Nothing, Nothing)
    End Sub


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

        Integer.TryParse(txtPort.Text.Trim, piPort)

        foServ.ServerName = psServer
        foServ.FolderPath = psFolder
        foServ.Port = piPort
        foServ.WorldName = txtWorldName.Text.Trim
        foServ.Password = txtPassword.Text.Trim
        foServ.SaveDir = txtDataPath.Text.Trim
        foServ.RestartServer = chkRestart.Checked
        foServ.BackupServer = chkBackup.Checked
        foServ.UpdateServer = chkUpdate.Checked
        foServ.RestartHour = dteRestartTime.Value.Hour
        foServ.RestartMin = dteRestartTime.Value.Minute
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
            poDlg.Description = "Select the folder to store the Valheim world data files."
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
        Me.Text = "Server - " & txtServerName.Name.Trim
    End Sub



End Class