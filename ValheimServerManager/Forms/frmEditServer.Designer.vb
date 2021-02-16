<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "&Installation Folder"
        '
        'txtInstallPath
        '
        Me.txtInstallPath.Location = New System.Drawing.Point(15, 173)
        Me.txtInstallPath.Name = "txtInstallPath"
        Me.txtInstallPath.Size = New System.Drawing.Size(371, 25)
        Me.txtInstallPath.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtInstallPath, "Folder on your local disk to install the program")
        '
        'btnInstBrowse
        '
        Me.btnInstBrowse.Image = Global.ValheimServerManager.My.Resources.Resources.folder_into
        Me.btnInstBrowse.Location = New System.Drawing.Point(392, 169)
        Me.btnInstBrowse.Name = "btnInstBrowse"
        Me.btnInstBrowse.Size = New System.Drawing.Size(30, 30)
        Me.btnInstBrowse.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.btnInstBrowse, "Browse for folder")
        Me.btnInstBrowse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "P&ort"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(15, 125)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(108, 25)
        Me.txtPort.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtPort, "Game port (query port is this value PLUS ONE).")
        '
        'lblPortRange
        '
        Me.lblPortRange.AutoSize = True
        Me.lblPortRange.Location = New System.Drawing.Point(129, 128)
        Me.lblPortRange.Name = "lblPortRange"
        Me.lblPortRange.Size = New System.Drawing.Size(74, 17)
        Me.lblPortRange.TabIndex = 9
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
        Me.btnSaveBrowse.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.btnSaveBrowse, "Browse for folder")
        Me.btnSaveBrowse.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 201)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 17)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "&Save Data Folder"
        '
        'txtDataPath
        '
        Me.txtDataPath.Location = New System.Drawing.Point(15, 221)
        Me.txtDataPath.Name = "txtDataPath"
        Me.txtDataPath.Size = New System.Drawing.Size(371, 25)
        Me.txtDataPath.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtDataPath, "Folder on your local disk to store the world game files." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(NOTE:  The program wil" &
        "l add ""worlds"" to this path" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "automatically, do not put it in your path).")
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(208, 337)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 32)
        Me.btnSave.TabIndex = 20
        Me.btnSave.Text = "INS&TALL"
        Me.ToolTip1.SetToolTip(Me.btnSave, "Save settings and install server")
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(312, 337)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 32)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "&CANCEL"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkRestart
        '
        Me.chkRestart.AutoSize = True
        Me.chkRestart.Location = New System.Drawing.Point(15, 263)
        Me.chkRestart.Name = "chkRestart"
        Me.chkRestart.Size = New System.Drawing.Size(115, 21)
        Me.chkRestart.TabIndex = 16
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
        Me.dteRestartTime.TabIndex = 17
        '
        'chkUpdate
        '
        Me.chkUpdate.AutoSize = True
        Me.chkUpdate.Location = New System.Drawing.Point(250, 263)
        Me.chkUpdate.Name = "chkUpdate"
        Me.chkUpdate.Size = New System.Drawing.Size(134, 21)
        Me.chkUpdate.TabIndex = 18
        Me.chkUpdate.Text = "&Update game files"
        Me.ToolTip1.SetToolTip(Me.chkUpdate, "Check to force game file update every restart")
        Me.chkUpdate.UseVisualStyleBackColor = True
        '
        'chkBackup
        '
        Me.chkBackup.AutoSize = True
        Me.chkBackup.Location = New System.Drawing.Point(250, 290)
        Me.chkBackup.Name = "chkBackup"
        Me.chkBackup.Size = New System.Drawing.Size(137, 21)
        Me.chkBackup.TabIndex = 19
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
        'frmEditServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(426, 381)
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
        Me.Text = "frmEditServer"
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
End Class
