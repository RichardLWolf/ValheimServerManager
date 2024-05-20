Public Class clsConfigInfo


    Private moServerData As DataTable

    Private msLastError As String = ""
    Private msFileName As String = ""


    Public Property ServerData As DataTable
        Get
            Return moServerData
        End Get
        Set(value As DataTable)
            moServerData = value
        End Set
    End Property

    Public ReadOnly Property LastErrorMessage As String
        Get
            Return msLastError
        End Get
    End Property

    Public ReadOnly Property ServerCount As Integer
        Get
            Return moServerData.Rows.Count
        End Get
    End Property



    Public Sub New(ByVal ConfigFileName As String)
        msFileName = ConfigFileName
        moServerData = New DataTable("ServerList")
        moServerData.Columns.Add("GUID", GetType(String))
        moServerData.Columns.Add("ServerFolder", GetType(String))
        moServerData.Columns.Add("ServerName", GetType(String))
        moServerData.Columns.Add("ServerPort", GetType(Integer))
        moServerData.Columns.Add("ServerWorld", GetType(String))
        moServerData.Columns.Add("ServerPassword", GetType(String))
        moServerData.Columns.Add("ServerSaveDir", GetType(String))
        moServerData.Columns.Add("UpdateOnRestart", GetType(Boolean))
        moServerData.Columns.Add("Restart", GetType(Boolean))
        moServerData.Columns.Add("RestartHour", GetType(Integer))
        moServerData.Columns.Add("RestartMin", GetType(Integer))
        moServerData.Columns.Add("Backup", GetType(Boolean))
        moServerData.Columns.Add("Public", GetType(Boolean))
        ' Version 1.2 new columns (modifier settings)
        moServerData.Columns.Add("Preset", GetType(String))
        moServerData.Columns.Add("ModCombat", GetType(String))
        moServerData.Columns.Add("ModDeathPenalty", GetType(String))
        moServerData.Columns.Add("ModResources", GetType(String))
        moServerData.Columns.Add("ModRaids", GetType(String))
        moServerData.Columns.Add("ModPortals", GetType(String))
        Me.Clear()
    End Sub





    Public Sub Clear()
        msLastError = ""
        moServerData.Rows.Clear()
    End Sub


    Public Function ConfigFound() As Boolean
        If System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(msFileName)) = False Then
            Return False
        Else
            Return System.IO.File.Exists(msFileName)
        End If
    End Function


    Public Function SaveConfigFile() As Boolean
        Dim pbResult As Boolean = False

        Try
            If System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(msFileName)) = False Then
                ' create it
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(msFileName))
            End If

            If System.IO.File.Exists(msFileName) Then
                Try
                    System.IO.File.Delete(msFileName)
                Catch ex2 As Exception
                    ' don't care
                End Try
            End If

            If moServerData.Rows.Count > 0 Then
                moServerData.AcceptChanges()
                moServerData.WriteXml(msFileName)
                pbResult = True
            End If
        Catch ex As Exception
            msLastError = "Error saving configuration file: [" & msFileName & "]."
            goLogger.LogException("clsConfigInfo.SaveConfigFile", ex)
        End Try

        Return pbResult
    End Function


    Public Function LoadConfigFile() As Boolean
        Dim pbSuccess As Boolean = False
        Dim psCurrElem As String = ""

        Me.Clear()

        Try
            If System.IO.File.Exists(msFileName) Then
                moServerData.ReadXml(msFileName)
                pbSuccess = True
            Else
                msLastError = "Configuration file was not found: [" & msFileName & "]."
            End If

        Catch ex As Exception
            msLastError = ex.Message
            goLogger.LogException("clsConfigInfo.LoadConfigFile", ex)
            pbSuccess = False
        End Try

        Return pbSuccess
    End Function




End Class
