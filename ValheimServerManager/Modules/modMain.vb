Module modMain
    Public goLogger As clsLogger
    Public goConfig As clsConfigInfo

    ' program constants
    Public Const APP_NAME As String = "Valheim Server Manager"
    Public Const APP_BASE_FOLDER As String = "VSM"
    Public Const APP_BACKUP_FOLDER As String = "Backups"
    Public Const SETTINGS_FILE_NAME As String = "settings.xml"

    Public gsConfigFile As String = ""
    Public gsBasePath As String = ""

    ' constants for combined messagebox styles
    Public Const giModalCritOK = MsgBoxStyle.OkOnly + MsgBoxStyle.Critical + MsgBoxStyle.SystemModal
    Public Const giModalExclOK = MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation + MsgBoxStyle.SystemModal
    Public Const giModalInfoOK = MsgBoxStyle.OkOnly + MsgBoxStyle.Information + MsgBoxStyle.SystemModal
    Public Const giModalExclYesNo = MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.SystemModal



    Public Sub Initialize()
        goLogger = New clsLogger(clsLogger.LoggingLevel.Info, RootFolderPath(), True, True, "error_log.txt")
        goLogger.PurgeLog()
        gsConfigFile = System.IO.Path.Combine(RootFolderPath(), SETTINGS_FILE_NAME)
        goConfig = New clsConfigInfo(gsConfigFile)
        goConfig.LoadConfigFile()
        gsBasePath = Application.StartupPath()
    End Sub



    ''' <summary>
    ''' Base public Documents folder path for the app root, e.g. "C:\Users\Public\Documents\APP_BASE_FOLDER\"
    ''' </summary>
    ''' <returns></returns>
    Public Function RootFolderPath() As String
        Dim psPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)
        psPath = System.IO.Path.Combine(psPath, APP_BASE_FOLDER)
        If System.IO.Directory.Exists(psPath) = False Then
            System.IO.Directory.CreateDirectory(psPath)
        End If
        Return psPath
    End Function

    ''' Public Documents folder path for the app backups, e.g. "C:\Users\Public\Documents\APP_BASE_FOLDER\APP_BACKUP_FOLDER"
    Public Function BackupFolderPath() As String
        Dim psPath As String = System.IO.Path.Combine(RootFolderPath(), APP_BACKUP_FOLDER)
        If System.IO.Directory.Exists(psPath) = False Then
            System.IO.Directory.CreateDirectory(psPath)
        End If
        Return psPath
    End Function


    Public Function RandomPassword(ByVal Length As Integer) As String
        Dim psPossible As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$0123456789abcdefghijklmnopqrstuvwxyz"
        Dim piIndex As Integer
        Dim poSB As New System.Text.StringBuilder
        Dim poRnd As New Random()

        For piIndex = 1 To Length
            poSB.Append(psPossible.Substring(poRnd.Next(0, psPossible.Length - 1), 1))
        Next
        Return poSB.ToString
    End Function


    Public Sub ListviewNoDataImage(ByVal oListview As ListView, Optional ByVal NoDataMessage As String = "No Items Found")
        Dim poBM As Bitmap
        Dim psTemp As String

        psTemp = NoDataMessage
        Using poG As System.Drawing.Graphics = oListview.CreateGraphics
            poBM = New Bitmap(CInt(poG.MeasureString(psTemp, oListview.Font).Width) + 10, CInt(poG.MeasureString(psTemp, oListview.Font).Width) + 10)
            Using poG2 As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(poBM)
                Using poBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(oListview.BackColor)
                    poG2.FillRectangle(poBrush, 0, 0, poBM.Width, poBM.Height)
                End Using
                Using poBrush As System.Drawing.SolidBrush = New System.Drawing.SolidBrush(oListview.ForeColor)
                    poG2.DrawString(psTemp, oListview.Font, poBrush, 5, 5)
                    oListview.BackgroundImageTiled = False
                    oListview.BackgroundImage = poBM
                    poG.Dispose()
                    poG2.Dispose()
                    poBrush.Dispose()
                End Using
            End Using
        End Using
    End Sub

    Public Function GetIPAddresses() As List(Of System.Net.IPAddress)
        Dim myIPAddresses As New List(Of System.Net.IPAddress)

        Try
            For Each netInterface As System.Net.NetworkInformation.NetworkInterface In System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
                If netInterface.NetworkInterfaceType <> System.Net.NetworkInformation.NetworkInterfaceType.Loopback AndAlso
                    netInterface.NetworkInterfaceType <> System.Net.NetworkInformation.NetworkInterfaceType.Tunnel AndAlso
                    netInterface.OperationalStatus = System.Net.NetworkInformation.OperationalStatus.Up Then

                    For Each address As System.Net.NetworkInformation.IPAddressInformation In netInterface.GetIPProperties.UnicastAddresses
                        myIPAddresses.Add(address.Address)
                    Next
                End If
            Next
            Return myIPAddresses
        Catch
            Return New List(Of System.Net.IPAddress)
        End Try
    End Function

    Public Function FindPrivateIP(ByVal Addresses As List(Of System.Net.IPAddress)) As System.Net.IPAddress
        Dim piIndex As Integer
        Dim psIP As String
        Dim piValue As Integer

        ' http://www.faqs.org/rfcs/rfc1918.html defines "private" ip block as follows
        '     10.0.0.0        -   10.255.255.255  (10/8 prefix)
        '     172.16.0.0      -   172.31.255.255  (172.16/12 prefix)
        '     192.168.0.0     -   192.168.255.255 (192.168/16 prefix)
        '
        If Addresses IsNot Nothing Then
            For piIndex = 0 To Addresses.Count - 1
                psIP = Addresses(piIndex).ToString.Trim
                If psIP.StartsWith("10.") Then
                    Return Addresses(piIndex)
                Else
                    If psIP.StartsWith("172.") Then
                        If Integer.TryParse(psIP.Substring(4, 2), piValue) Then
                            If piValue >= 16 And piValue <= 31 Then
                                Return Addresses(piIndex)
                            End If
                        Else
                            If psIP.StartsWith("192.168.") Then
                                Return Addresses(piIndex)
                            End If
                        End If
                    End If
                End If
            Next
            ' if we get here then no local IP detected, just return the 1st one
            Return Addresses(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function GetExternalIp() As String
        Try
            Dim ExternalIP As String
            ExternalIP = (New System.Net.WebClient()).DownloadString("http://checkip.dyndns.org/")
            ExternalIP = (New System.Text.RegularExpressions.Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(ExternalIP)(0).ToString()
            Return ExternalIP
        Catch
            Return String.Empty
        End Try
    End Function


    Public Function FullDisplayElapsed(ByVal TotalSeconds As Long) As String

        Dim piDays As Integer
        Dim piHours As Integer
        Dim piMins As Integer
        Dim piSeconds As Long
        Dim piCurr As Long
        Dim psTemp As String

        piCurr = TotalSeconds
        piDays = piCurr \ 86400 ' (60 seconds * 60 mins in an hour times 24 hours in a day)
        piCurr = piCurr - (piDays * 86400)
        piHours = piCurr \ 3600 ' 3600 seconds in an hour
        piCurr = piCurr - (piHours * 3600)
        piMins = piCurr \ 60    ' 60 seconds in a minute
        piCurr = piCurr - (piMins * 60)
        piSeconds = piCurr

        psTemp = ""
        If piDays > 0 Then
            If piHours > 0 Or piMins > 0 Then
                psTemp = psTemp & IIf(piDays = 1, "1 day, ", Format(piDays, "#,##0") & " days, ")
            Else
                psTemp = psTemp & IIf(piDays = 1, "1 day", Format(piDays, "#,##0") & " days")
            End If
        End If
        If piHours > 0 Then
            If piMins > 0 Or piSeconds > 0 Then
                If piSeconds > 0 Then
                    psTemp = psTemp & Format(piHours, "#0") & IIf(piHours = 1, " hour, ", " hours, ")
                Else
                    psTemp = psTemp & Format(piHours, "#0") & IIf(piHours = 1, " hour and ", " hours and ")
                End If
            Else
                psTemp = psTemp & Format(piHours, "#0") & IIf(piHours = 1, " hour", " hours")
            End If
        End If
        If piMins > 0 Then
            If piSeconds > 0 Then
                psTemp = psTemp & Format(piMins, "#0") & IIf(piMins = 1, " minute and ", " minutes and ")
            Else
                psTemp = psTemp & Format(piMins, "#0") & IIf(piMins = 1, " minute", " minutes")
            End If
        End If
        If piSeconds > 0 Then
            psTemp = psTemp & Format(piSeconds, "#0") & IIf(piSeconds = 1, " second", " seconds")
        Else
            If psTemp = "" Then
                psTemp = "<1 Second"
            End If
        End If
        Return psTemp
    End Function



#Region " Safe Functions "
    Public Function SafeStr(ByVal Anything As Object, Optional ByVal bTrimIt As Boolean = True, Optional ByVal bUCase As Boolean = False, Optional ByVal bProperCase As Boolean = False) As String
        Dim psNew As String = ""

        Try
            If Anything Is System.DBNull.Value Then
                Return psNew
            Else
                If Anything Is Nothing Then
                    Return psNew
                Else
                    psNew = Anything.ToString
                End If
            End If
        Catch ex As System.Exception
            psNew = ""
        Finally
            If bTrimIt Then
                psNew = psNew.Trim
            End If
            If bUCase Then
                psNew = StrConv(psNew, VbStrConv.Uppercase)
            Else
                If bProperCase Then
                    psNew = StrConv(psNew, VbStrConv.ProperCase)
                End If
            End If
        End Try

        Return psNew
    End Function

    Public Function SafeByte(ByVal Anything As Object) As Byte
        Dim pyNewByte As Byte = 0

        Try
            If Anything Is System.DBNull.Value Then
                Return pyNewByte
            Else
                If Anything Is Nothing Then
                    Return pyNewByte
                Else
                    pyNewByte = Anything
                End If
            End If
        Catch ex As System.Exception
            Try
                pyNewByte = CByte(Anything)
            Catch ex2 As System.Exception
                Try
                    pyNewByte = Val(Anything)
                Catch ex3 As System.Exception
                    pyNewByte = 0
                End Try
            End Try
        End Try

        Return pyNewByte
    End Function

    Public Function SafeInt(ByVal Anything As Object) As Integer
        Dim piNewInt As Integer = 0

        Try
            If Anything Is System.DBNull.Value Then
                Return 0
            Else
                If Anything Is Nothing Then
                    Return 0
                Else
                    If Not Integer.TryParse(Anything, piNewInt) Then
                        Return CInt(Anything)
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                piNewInt = CInt(Anything)
            Catch ex2 As System.Exception
                Try
                    piNewInt = Val(Anything)
                Catch ex3 As System.Exception
                    piNewInt = 0
                End Try
            End Try
        End Try

        Return piNewInt
    End Function

    Public Function SafeShort(ByVal Anything As Object) As Short
        Dim phNewShort As Short = 0S

        Try
            If Anything Is System.DBNull.Value Then
                Return phNewShort
            Else
                If Not Short.TryParse(Anything, phNewShort) Then
                    Return 0
                End If
            End If
        Catch ex As System.Exception
            Try
                phNewShort = CShort(Anything)
            Catch ex2 As System.Exception
                Try
                    phNewShort = Val(Anything)
                Catch ex3 As System.Exception
                    phNewShort = 0S
                End Try
            End Try
        End Try

        Return phNewShort
    End Function

    Public Function SafeLong(ByVal Anything As Object) As Long
        Dim plNewLong As Long = 0

        Try
            If Anything Is System.DBNull.Value Then
                Return plNewLong
            Else
                If Anything Is Nothing Then
                    Return plNewLong
                Else
                    If Not Long.TryParse(Anything, plNewLong) Then
                        Return 0
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                plNewLong = CLng(Anything)
            Catch ex2 As System.Exception
                Try
                    plNewLong = Val(Anything)
                Catch ex3 As System.Exception
                    plNewLong = 0
                End Try
            End Try
        End Try

        Return plNewLong
    End Function

    Public Function SafeDollar(ByVal Anything As Object) As Decimal
        Dim pcNewDecimal As Decimal = 0D
        Dim psFormat As String

        Try
            If Anything Is System.DBNull.Value Then
                Return pcNewDecimal
            Else
                If Anything Is Nothing Then
                    Return pcNewDecimal
                Else
                    If Not Decimal.TryParse(Anything, pcNewDecimal) Then
                        Return 0D
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                pcNewDecimal = CDec(Anything)
            Catch ex2 As System.Exception
                Try
                    pcNewDecimal = Val(Anything)
                Catch ex3 As System.Exception
                    pcNewDecimal = 0D
                End Try
            End Try
        End Try
        psFormat = Format(pcNewDecimal, "#0.00")

        Return CDec(psFormat)
    End Function

    Public Function SafeDec(ByVal Anything As Object) As Decimal
        Dim pcNewDecimal As Decimal = 0D

        Try
            If Anything Is System.DBNull.Value Then
                Return pcNewDecimal
            Else
                If Anything Is Nothing Then
                    Return pcNewDecimal
                Else
                    If Not Decimal.TryParse(Anything, pcNewDecimal) Then
                        Return 0D
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                pcNewDecimal = CDec(Anything)
            Catch ex2 As System.Exception
                Try
                    pcNewDecimal = Val(Anything)
                Catch ex3 As System.Exception
                    pcNewDecimal = 0D
                End Try
            End Try
        End Try

        Return pcNewDecimal
    End Function

    Public Function SafeSng(ByVal Anything As Object) As Single
        Dim pfNewSng As Single = 0.0F

        Try
            If Anything Is System.DBNull.Value Then
                Return 0.0F
            Else
                If Anything Is Nothing Then
                    Return 0.0F
                Else
                    If Not Single.TryParse(Anything, pfNewSng) Then
                        Return 0.0F
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                pfNewSng = CSng(Anything)
            Catch ex2 As System.Exception
                Try
                    pfNewSng = Val(Anything)
                Catch ex3 As System.Exception
                    pfNewSng = 0.0F
                End Try
            End Try
        End Try

        Return pfNewSng
    End Function


    Public Function SafeDate(ByVal Anything As Object) As Date
        Dim ptDate As Date

        Try
            If Anything Is System.DBNull.Value Then
                Return Nothing
            Else
                If Anything Is Nothing Then
                    Return Nothing
                Else
                    If Not Date.TryParse(Anything, ptDate) Then
                        Return Nothing
                    End If
                End If
            End If
        Catch ex As System.Exception
            Try
                ptDate = CDate(Anything)
            Catch ex2 As System.Exception
                ptDate = Nothing
            End Try
        End Try

        Return ptDate
    End Function

    Public Function SafeSQLDate(ByVal Anything As Object) As Date
        Try
            If Anything Is Nothing Then
                Return Nothing
            Else
                If Anything Is System.DBNull.Value Then
                    Return Nothing
                Else
                    Dim ptDate As Date
                    If Not Date.TryParse(Anything, ptDate) Then
                        Return Nothing
                    Else
                        If ptDate < System.Data.SqlTypes.SqlDateTime.MinValue.Value Then
                            ptDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value
                        Else
                            If ptDate > System.Data.SqlTypes.SqlDateTime.MaxValue.Value Then
                                ptDate = System.Data.SqlTypes.SqlDateTime.MaxValue.Value
                            End If
                        End If
                        Return ptDate
                    End If
                End If
            End If
        Catch ex As System.Exception
            Return Nothing
        End Try
    End Function

    Public Function SafeBool(ByVal Anything As Object) As Boolean
        Try
            If Anything Is System.DBNull.Value Then
                Return False
            Else
                If Anything Is Nothing Then
                    Return False
                Else
                    SafeBool = Anything
                End If
            End If
        Catch ex As System.Exception
            SafeBool = False
        End Try
    End Function
#End Region




End Module
