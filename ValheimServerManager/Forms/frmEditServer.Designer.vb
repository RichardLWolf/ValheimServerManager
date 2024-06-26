﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWorldName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInstallPath = New System.Windows.Forms.TextBox()
        Me.btnInstBrowse = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.lblPortRange = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnSaveBrowse = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDataPath = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkRestart = New System.Windows.Forms.CheckBox()
        Me.dteRestartTime = New System.Windows.Forms.DateTimePicker()
        Me.chkUpdate = New System.Windows.Forms.CheckBox()
        Me.chkBackup = New System.Windows.Forms.CheckBox()
        Me.btnShowHide = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkPublic = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboPreset = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboModPortals = New System.Windows.Forms.ComboBox()
        Me.cboModRaids = New System.Windows.Forms.ComboBox()
        Me.cboModResources = New System.Windows.Forms.ComboBox()
        Me.cboModDeath = New System.Windows.Forms.ComboBox()
        Me.cboModCombat = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(15, 29)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(221, 25)
        Me.txtServerName.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtServerName, "The name the server will show in server list")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server &Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(239, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&World Name"
        '
        'txtWorldName
        '
        Me.txtWorldName.Location = New System.Drawing.Point(242, 29)
        Me.txtWorldName.Name = "txtWorldName"
        Me.txtWorldName.Size = New System.Drawing.Size(170, 25)
        Me.txtWorldName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtWorldName, "Local world file name on disk." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You can switch between saved games with this sett" &
        "ing.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 153)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "&Installation Folder"
        '
        'txtInstallPath
        '
        Me.txtInstallPath.Location = New System.Drawing.Point(15, 173)
        Me.txtInstallPath.Name = "txtInstallPath"
        Me.txtInstallPath.Size = New System.Drawing.Size(371, 25)
        Me.txtInstallPath.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtInstallPath, "Folder on your local disk to install the program")
        '
        'btnInstBrowse
        '
        Me.btnInstBrowse.Image = Global.ValheimServerManager.My.Resources.Resources.folder_into
        Me.btnInstBrowse.Location = New System.Drawing.Point(392, 169)
        Me.btnInstBrowse.Name = "btnInstBrowse"
        Me.btnInstBrowse.Size = New System.Drawing.Size(30, 30)
        Me.btnInstBrowse.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.btnInstBrowse, "Browse for folder")
        Me.btnInstBrowse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "P&ort"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(15, 125)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(108, 25)
        Me.txtPort.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtPort, "Game port (query port is this value PLUS ONE).")
        '
        'lblPortRange
        '
        Me.lblPortRange.AutoSize = True
        Me.lblPortRange.Location = New System.Drawing.Point(129, 128)
        Me.lblPortRange.Name = "lblPortRange"
        Me.lblPortRange.Size = New System.Drawing.Size(74, 17)
        Me.lblPortRange.TabIndex = 10
        Me.lblPortRange.Text = "invaild port"
        Me.lblPortRange.UseMnemonic = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 17)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "&Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(15, 77)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(221, 25)
        Me.txtPassword.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Login password (at least 6 with a special character)")
        '
        'btnSaveBrowse
        '
        Me.btnSaveBrowse.Image = Global.ValheimServerManager.My.Resources.Resources.folder_into
        Me.btnSaveBrowse.Location = New System.Drawing.Point(392, 221)
        Me.btnSaveBrowse.Name = "btnSaveBrowse"
        Me.btnSaveBrowse.Size = New System.Drawing.Size(30, 30)
        Me.btnSaveBrowse.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.btnSaveBrowse, "Browse for folder")
        Me.btnSaveBrowse.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 17)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "&Save Data Folder"
        '
        'txtDataPath
        '
        Me.txtDataPath.Location = New System.Drawing.Point(15, 221)
        Me.txtDataPath.Name = "txtDataPath"
        Me.txtDataPath.Size = New System.Drawing.Size(371, 25)
        Me.txtDataPath.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtDataPath, "Folder on your local disk to store the world game files." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(NOTE:  The program wil" &
        "l add ""worlds"" to this path" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "automatically, do not put it in your path).")
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(578, 309)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 32)
        Me.btnSave.TabIndex = 24
        Me.btnSave.Text = "INS&TALL"
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save settings and install server")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(682, 309)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 32)
        Me.btnCancel.TabIndex = 25
        Me.btnCancel.Text = "&CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkRestart
        '
        Me.chkRestart.AutoSize = True
        Me.chkRestart.Location = New System.Drawing.Point(15, 263)
        Me.chkRestart.Name = "chkRestart"
        Me.chkRestart.Size = New System.Drawing.Size(115, 21)
        Me.chkRestart.TabIndex = 17
        Me.chkRestart.Text = "&Restart Daily at"
        Me.ToolTip1.SetToolTip(Me.chkRestart, "Check to have the server restart once per day at this time")
        Me.chkRestart.UseVisualStyleBackColor = True
        '
        'dteRestartTime
        '
        Me.dteRestartTime.CustomFormat = "hh:mm tt"
        Me.dteRestartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dteRestartTime.Location = New System.Drawing.Point(136, 259)
        Me.dteRestartTime.Name = "dteRestartTime"
        Me.dteRestartTime.Size = New System.Drawing.Size(108, 25)
        Me.dteRestartTime.TabIndex = 18
        '
        'chkUpdate
        '
        Me.chkUpdate.AutoSize = True
        Me.chkUpdate.Location = New System.Drawing.Point(15, 290)
        Me.chkUpdate.Name = "chkUpdate"
        Me.chkUpdate.Size = New System.Drawing.Size(134, 21)
        Me.chkUpdate.TabIndex = 19
        Me.chkUpdate.Text = "&Update game files"
        Me.ToolTip1.SetToolTip(Me.chkUpdate, "Check to force game file update every restart")
        Me.chkUpdate.UseVisualStyleBackColor = True
        '
        'chkBackup
        '
        Me.chkBackup.AutoSize = True
        Me.chkBackup.Location = New System.Drawing.Point(15, 316)
        Me.chkBackup.Name = "chkBackup"
        Me.chkBackup.Size = New System.Drawing.Size(137, 21)
        Me.chkBackup.TabIndex = 20
        Me.chkBackup.Text = "&Backup Game Data"
        Me.ToolTip1.SetToolTip(Me.chkBackup, "Check to backup the world files while the server is down.")
        Me.chkBackup.UseVisualStyleBackColor = True
        '
        'btnShowHide
        '
        Me.btnShowHide.Image = Global.ValheimServerManager.My.Resources.Resources.eye
        Me.btnShowHide.Location = New System.Drawing.Point(242, 73)
        Me.btnShowHide.Name = "btnShowHide"
        Me.btnShowHide.Size = New System.Drawing.Size(30, 30)
        Me.btnShowHide.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.btnShowHide, "Show/hide password")
        Me.btnShowHide.UseVisualStyleBackColor = True
        '
        'chkPublic
        '
        Me.chkPublic.AutoSize = True
        Me.chkPublic.Location = New System.Drawing.Point(298, 79)
        Me.chkPublic.Name = "chkPublic"
        Me.chkPublic.Size = New System.Drawing.Size(102, 21)
        Me.chkPublic.TabIndex = 7
        Me.chkPublic.Text = "&Pu&blic Server"
        Me.chkPublic.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(445, 9)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 17)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Pr&eset"
        '
        'cboPreset
        '
        Me.cboPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPreset.FormattingEnabled = True
        Me.cboPreset.Location = New System.Drawing.Point(448, 29)
        Me.cboPreset.Name = "cboPreset"
        Me.cboPreset.Size = New System.Drawing.Size(334, 25)
        Me.cboPreset.TabIndex = 22
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cboModPortals)
        Me.GroupBox1.Controls.Add(Me.cboModRaids)
        Me.GroupBox1.Controls.Add(Me.cboModResources)
        Me.GroupBox1.Controls.Add(Me.cboModDeath)
        Me.GroupBox1.Controls.Add(Me.cboModCombat)
        Me.GroupBox1.Location = New System.Drawing.Point(448, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(334, 181)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "&Modifiers"
        '
        'cboModPortals
        '
        Me.cboModPortals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModPortals.FormattingEnabled = True
        Me.cboModPortals.Location = New System.Drawing.Point(105, 145)
        Me.cboModPortals.Name = "cboModPortals"
        Me.cboModPortals.Size = New System.Drawing.Size(213, 25)
        Me.cboModPortals.TabIndex = 9
        '
        'cboModRaids
        '
        Me.cboModRaids.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModRaids.FormattingEnabled = True
        Me.cboModRaids.Location = New System.Drawing.Point(105, 114)
        Me.cboModRaids.Name = "cboModRaids"
        Me.cboModRaids.Size = New System.Drawing.Size(213, 25)
        Me.cboModRaids.TabIndex = 7
        '
        'cboModResources
        '
        Me.cboModResources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModResources.FormattingEnabled = True
        Me.cboModResources.Location = New System.Drawing.Point(105, 83)
        Me.cboModResources.Name = "cboModResources"
        Me.cboModResources.Size = New System.Drawing.Size(213, 25)
        Me.cboModResources.TabIndex = 5
        '
        'cboModDeath
        '
        Me.cboModDeath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModDeath.FormattingEnabled = True
        Me.cboModDeath.Location = New System.Drawing.Point(105, 53)
        Me.cboModDeath.Name = "cboModDeath"
        Me.cboModDeath.Size = New System.Drawing.Size(213, 25)
        Me.cboModDeath.TabIndex = 3
        '
        'cboModCombat
        '
        Me.cboModCombat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboModCombat.FormattingEnabled = True
        Me.cboModCombat.Location = New System.Drawing.Point(105, 22)
        Me.cboModCombat.Name = "cboModCombat"
        Me.cboModCombat.Size = New System.Drawing.Size(213, 25)
        Me.cboModCombat.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(45, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 17)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Combat"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 17)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Death Penalty"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(31, 86)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 17)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Resources"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(59, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 17)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Raids"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(51, 148)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 17)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Portals"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmEditServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 353)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cboPreset)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.chkPublic)
        Me.Controls.Add(Me.btnShowHide)
        Me.Controls.Add(Me.chkBackup)
        Me.Controls.Add(Me.chkUpdate)
        Me.Controls.Add(Me.dteRestartTime)
        Me.Controls.Add(Me.chkRestart)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnSaveBrowse)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDataPath)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblPortRange)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.btnInstBrowse)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtInstallPath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtWorldName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtServerName)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.Name = "frmEditServer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Server"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtServerName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtWorldName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtInstallPath As TextBox
    Friend WithEvents btnInstBrowse As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents lblPortRange As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnSaveBrowse As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents txtDataPath As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents chkRestart As CheckBox
    Friend WithEvents dteRestartTime As DateTimePicker
    Friend WithEvents chkUpdate As CheckBox
    Friend WithEvents chkBackup As CheckBox
    Friend WithEvents btnShowHide As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents chkPublic As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboPreset As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cboModCombat As ComboBox
    Friend WithEvents cboModResources As ComboBox
    Friend WithEvents cboModDeath As ComboBox
    Friend WithEvents cboModPortals As ComboBox
    Friend WithEvents cboModRaids As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
End Class
