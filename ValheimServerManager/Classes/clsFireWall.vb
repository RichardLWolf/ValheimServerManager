Imports NetFwTypeLib

Public Class clsFireWall
    Public Enum ProtocolType
        Tcp = 6
        Udp = 17
        Any = 256
    End Enum

    Public Shared Function CheckAddPortRule(ByVal FwRuleTitle As String, ByVal Ports As String, ByVal Protcol As ProtocolType, ByVal Profile2Types As NET_FW_PROFILE_TYPE2_) As Boolean
        Try
            Dim Tpolicy2Class As Type = Type.GetTypeFromProgID("HNetCfg.FwPolicy2")
            Dim policy2Class As INetFwPolicy2 = CType(Activator.CreateInstance(Tpolicy2Class), INetFwPolicy2)
            For Each itm As INetFwRule In policy2Class.Rules
                Try
                    If (itm.Name.ToLower = FwRuleTitle.ToLower) Then
                        itm.Profiles = CType(Profile2Types, Integer)
                        itm.Protocol = CType(Protcol, Integer)
                        itm.LocalPorts = Ports
                        Return True
                    End If

                Catch ex As Exception
                    modMain.goLogger.LogException("clsFirewall.CheckAddPortRule-Inner", ex)
                End Try
            Next
            Dim fwRule As INetFwRule = CType(Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule")), INetFwRule)
            fwRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW
            fwRule.Name = FwRuleTitle
            fwRule.Profiles = CType(Profile2Types, Integer)
            fwRule.Protocol = CType(Protcol, Integer)
            fwRule.LocalPorts = Ports
            fwRule.Enabled = True
            fwRule.InterfaceTypes = "All"
            'Acceptable values for this property are "RemoteAccess", "Wireless", "Lan", and "All". 
            policy2Class.Rules.Add(fwRule)
            Return True
        Catch ex As Exception
            modMain.goLogger.LogException("clsFirewall.CheckAddPortRule-Outer", ex)
        End Try

        Return False
    End Function
End Class
