Public Class frmViewLog

    Private foSorter As ListviewSorter

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmViewLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.vsm
        Me.Text = "App Log - " & goLogger.LogFilename
        With lvwLog
            .View = View.Details
            .MultiSelect = False
            .FullRowSelect = True
            .GridLines = False
            .Items.Clear()
            .SmallImageList = imlSmall
            .Columns.Clear()
            .Columns.Add("DATE", "Date", 200)
            .Columns.Add("TEXT", "Message", lvwLog.Width - 230)
            foSorter = New ListviewSorter(0, SortOrder.Ascending, False)
            .ListViewItemSorter = foSorter
        End With
        Call SetBackgroundImage("Loading log, please wait...")
        Dim poLoadThread As System.Threading.Thread = New System.Threading.Thread(AddressOf LoadLogLines)
        poLoadThread.IsBackground = True
        poLoadThread.Start(poLoadThread)
    End Sub

    Private Sub lvwLog_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwLog.ColumnClick
        If e.Column = foSorter.SortColumn Then
            ' Reverse the current sort direction for this column.
            If foSorter.Order = Windows.Forms.SortOrder.Ascending Then
                foSorter.Order = Windows.Forms.SortOrder.Descending
            Else
                foSorter.Order = Windows.Forms.SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            foSorter.SortColumn = e.Column
            foSorter.Order = Windows.Forms.SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        lvwLog.Sort()
    End Sub







    Private Sub LoadLogLines(ByVal oLoadThread As System.Threading.Thread)

        Try
            Dim poList As New List(Of ListViewItem)
            Dim psIcon As String
            Dim piCount As Integer = 0
            If System.IO.File.Exists(goLogger.LogFilename) Then
                Dim poLog As List(Of clsLogger.LoggedEntry) = goLogger.LoadLogFormatted()
                For Each poLogEntry As clsLogger.LoggedEntry In poLog
                    Select Case poLogEntry.LogType
                        Case clsLogger.LoggingLevel.Error
                            psIcon = "error"
                        Case clsLogger.LoggingLevel.Info
                            psIcon = "info"
                        Case clsLogger.LoggingLevel.Warning
                            psIcon = "warning"
                        Case Else
                            psIcon = "info"
                    End Select
                    Dim poLvw As New ListViewItem(String.Format("{0:MMM-dd-yyyy HH:mm:ss}", poLogEntry.LogDate), psIcon)
                    poLvw.SubItems.Add(poLogEntry.LogHtmlMessage.Replace("<br />", vbCrLf))
                    poLvw.ForeColor = Color.Black
                    If piCount Mod 6 < 3 Then
                        poLvw.BackColor = Color.WhiteSmoke
                    Else
                        poLvw.BackColor = Color.White
                    End If
                    piCount = piCount + 1
                    poList.Add(poLvw)
                Next
            End If

            If poList.Count = 0 Then
                Call SetBackgroundImage("No log data to display.")
            Else
                AddBulkToListview(poList)
            End If

        Catch ex As Exception
            Call SetBackgroundImage("Failed to load log data.")
        Finally
            oLoadThread = Nothing
        End Try
    End Sub



    Private Delegate Sub Del_SetBackgroundImage(ByVal Message As String)
    Private Sub SetBackgroundImage(ByVal Message As String)
        If lvwLog.InvokeRequired Then
            lvwLog.Invoke(New Del_SetBackgroundImage(AddressOf SetBackgroundImage), New Object() {Message})
        Else
            lvwLog.SuspendLayout()
            If Message = "" Then
                lvwLog.BackgroundImage = Nothing
                lvwLog.GridLines = True
            Else
                lvwLog.Items.Clear()
                lvwLog.GridLines = False
                Call modMain.ListviewNoDataImage(lvwLog, Message)
            End If
            lvwLog.ResumeLayout(True)
        End If
    End Sub

    Private Delegate Sub Del_AddBulkToListview(ByVal oItems As List(Of ListViewItem))
    Private Sub AddBulkToListview(ByVal oItems As List(Of ListViewItem))
        If lvwLog.InvokeRequired Then
            Dim poCB As New Del_AddBulkToListview(AddressOf AddBulkToListview)
            Dim poParm(0) As Object
            poParm(0) = oItems
            lvwLog.Invoke(poCB, poParm)
        Else
            Call SetBackgroundImage("")
            lvwLog.SuspendLayout()
            lvwLog.Items.AddRange(oItems.ToArray())
            lvwLog.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)
            lvwLog.Sort()
            lvwLog.ResumeLayout(True)
        End If
    End Sub






End Class