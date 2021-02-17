Public Class frmAbout

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.vsm
        Me.Text = "About Valheim Server Manager Version " & Application.ProductVersion
        txtLicense.Rtf = My.Resources.RTF_License
    End Sub

    Private Sub txtLicense_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles txtLicense.LinkClicked
        Try
            Me.Cursor = System.Windows.Forms.Cursors.AppStarting
            Process.Start(e.LinkText)
        Catch ex As Exception
            MsgBox("Can't start default browser.", giModalExclOK, "No Internet Browser")
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Arrow
        End Try

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

End Class