<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.tspMenu = New System.Windows.Forms.ToolStrip()
        Me.btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnStart = New System.Windows.Forms.ToolStripButton()
        Me.btnStop = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnBackup = New System.Windows.Forms.ToolStripButton()
        Me.btnRestore = New System.Windows.Forms.ToolStripButton()
        Me.btnUpdate = New System.Windows.Forms.ToolStripButton()
        Me.btnAppFolder = New System.Windows.Forms.ToolStripButton()
        Me.lvwServers = New System.Windows.Forms.ListView()
        Me.sstStatus = New System.Windows.Forms.StatusStrip()
        Me.tslStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.imlListview = New System.Windows.Forms.ImageList(Me.components)
        Me.btnViewServerFolder = New System.Windows.Forms.ToolStripButton()
        Me.tspMenu.SuspendLayout()
        Me.sstStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'tspMenu
        '
        Me.tspMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tspMenu.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tspMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAdd, Me.btnEdit, Me.btnDelete, Me.ToolStripSeparator1, Me.btnStart, Me.btnStop, Me.btnViewServerFolder, Me.ToolStripSeparator2, Me.btnBackup, Me.btnRestore, Me.btnUpdate, Me.btnAppFolder})
        Me.tspMenu.Location = New System.Drawing.Point(0, 0)
        Me.tspMenu.Name = "tspMenu"
        Me.tspMenu.Padding = New System.Windows.Forms.Padding(5, 0, 2, 0)
        Me.tspMenu.Size = New System.Drawing.Size(812, 39)
        Me.tspMenu.TabIndex = 0
        Me.tspMenu.Text = "ToolStrip1"
        '
        'btnAdd
        '
        Me.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAdd.Image = Global.ValheimServerManager.My.Resources.Resources.add2
        Me.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(36, 36)
        Me.btnAdd.Text = "Add Server Entry"
        '
        'btnEdit
        '
        Me.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnEdit.Image = Global.ValheimServerManager.My.Resources.Resources.pencil2
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(36, 36)
        Me.btnEdit.Text = "Edit Entry"
        '
        'btnDelete
        '
        Me.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnDelete.Image = Global.ValheimServerManager.My.Resources.Resources.delete2
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(36, 36)
        Me.btnDelete.Text = "Delete Entry"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 39)
        '
        'btnStart
        '
        Me.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnStart.Image = Global.ValheimServerManager.My.Resources.Resources.media_play_green
        Me.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(36, 36)
        Me.btnStart.Text = "Start Server"
        '
        'btnStop
        '
        Me.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnStop.Image = Global.ValheimServerManager.My.Resources.Resources.media_stop_red
        Me.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(36, 36)
        Me.btnStop.Text = "Stop Server"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 39)
        '
        'btnBackup
        '
        Me.btnBackup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnBackup.Image = Global.ValheimServerManager.My.Resources.Resources.data_out
        Me.btnBackup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(36, 36)
        Me.btnBackup.Text = "Create Backup"
        '
        'btnRestore
        '
        Me.btnRestore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnRestore.Image = Global.ValheimServerManager.My.Resources.Resources.data_down
        Me.btnRestore.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(36, 36)
        Me.btnRestore.Text = "Restore From Backup"
        '
        'btnUpdate
        '
        Me.btnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnUpdate.Image = Global.ValheimServerManager.My.Resources.Resources.server_to_client
        Me.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(36, 36)
        Me.btnUpdate.Text = "Update Server Files"
        '
        'btnAppFolder
        '
        Me.btnAppFolder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnAppFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAppFolder.Image = Global.ValheimServerManager.My.Resources.Resources.folder_view
        Me.btnAppFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAppFolder.Name = "btnAppFolder"
        Me.btnAppFolder.Size = New System.Drawing.Size(36, 36)
        Me.btnAppFolder.Text = "View app folder"
        '
        'lvwServers
        '
        Me.lvwServers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwServers.HideSelection = False
        Me.lvwServers.Location = New System.Drawing.Point(0, 39)
        Me.lvwServers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lvwServers.Name = "lvwServers"
        Me.lvwServers.Size = New System.Drawing.Size(812, 220)
        Me.lvwServers.TabIndex = 1
        Me.lvwServers.UseCompatibleStateImageBehavior = False
        '
        'sstStatus
        '
        Me.sstStatus.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sstStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslStatus})
        Me.sstStatus.Location = New System.Drawing.Point(0, 259)
        Me.sstStatus.Name = "sstStatus"
        Me.sstStatus.Padding = New System.Windows.Forms.Padding(2, 0, 21, 0)
        Me.sstStatus.Size = New System.Drawing.Size(812, 26)
        Me.sstStatus.TabIndex = 2
        Me.sstStatus.Text = "Initializing"
        '
        'tslStatus
        '
        Me.tslStatus.Margin = New System.Windows.Forms.Padding(5, 3, 0, 2)
        Me.tslStatus.Name = "tslStatus"
        Me.tslStatus.Size = New System.Drawing.Size(784, 21)
        Me.tslStatus.Spring = True
        Me.tslStatus.Text = "Initializing..."
        Me.tslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'imlListview
        '
        Me.imlListview.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imlListview.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlListview.TransparentColor = System.Drawing.Color.Transparent
        '
        'btnViewServerFolder
        '
        Me.btnViewServerFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnViewServerFolder.Image = Global.ValheimServerManager.My.Resources.Resources.data_view
        Me.btnViewServerFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewServerFolder.Name = "btnViewServerFolder"
        Me.btnViewServerFolder.Size = New System.Drawing.Size(36, 36)
        Me.btnViewServerFolder.Text = "View server folder"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 285)
        Me.Controls.Add(Me.lvwServers)
        Me.Controls.Add(Me.sstStatus)
        Me.Controls.Add(Me.tspMenu)
        Me.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VSM"
        Me.tspMenu.ResumeLayout(False)
        Me.tspMenu.PerformLayout()
        Me.sstStatus.ResumeLayout(False)
        Me.sstStatus.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tspMenu As ToolStrip
    Friend WithEvents btnAdd As ToolStripButton
    Friend WithEvents lvwServers As ListView
    Friend WithEvents sstStatus As StatusStrip
    Friend WithEvents tslStatus As ToolStripStatusLabel
    Friend WithEvents imlListview As ImageList
    Friend WithEvents btnEdit As ToolStripButton
    Friend WithEvents btnDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnStart As ToolStripButton
    Friend WithEvents btnStop As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnBackup As ToolStripButton
    Friend WithEvents btnRestore As ToolStripButton
    Friend WithEvents btnUpdate As ToolStripButton
    Friend WithEvents btnAppFolder As ToolStripButton
    Friend WithEvents btnViewServerFolder As ToolStripButton
End Class
