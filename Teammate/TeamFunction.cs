using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;

using SocketProtocol;

namespace SocketServer.Teammate
{
    internal class TeamFunction
    {
        public bool AttackInvite(MainPack mainPack, Team team, Client client)
        {
            if (mainPack.TeammatePack.SenderUid == mainPack.Uid && mainPack.TeammatePack.State == 0)
            {
                try
                {
                    //if (team != null)
                    //{
                    //    foreach (Client c in team.Teammates)
                    //    {
                            //MainPack mainPack1 = new MainPack();
                            //mainPack1.ActionCode = ActionCode.AttackInvited;
                            //mainPack1.AttackAreaPack = mainPack.AttackAreaPack;
                            //TeammatePack teammatePack = new TeammatePack();
                            //teammatePack.SenderUid = mainPack.TeammatePack.SenderUid;
                            //teammatePack.TargetUid = c.PlayerInfo.Uid;
                            //mainPack1.TeammatePack = teammatePack;
                    //        c.AttackInvited(mainPack1);
                    //    }
                    //    return true;
                    //}
                    //else
                    //{
                    //    //单人出击
                    //    MainPack mainPack2 = new MainPack();
                    //    mainPack2.RequestCode = RequestCode.Gaming;
                    //    mainPack2.ActionCode = ActionCode.StartAttack;
                    //    mainPack2.Uid = client.PlayerInfo.Uid;
                    //    mainPack2.AttackAreaPack = mainPack.AttackAreaPack;
                    //    client.StartAttack(mainPack2);
                    //    return true;
                    //}

                    MainPack mainPack2 = new MainPack();
                    mainPack2.RequestCode = RequestCode.Gaming;
                    mainPack2.ActionCode = ActionCode.StartAttack;
                    mainPack2.Uid = client.PlayerInfo.Uid;
                    mainPack2.AttackAreaPack = mainPack.AttackAreaPack;
                    client.StartAttack(mainPack2);
                    return true;
                }
                catch (Exception e)
                {
                    Debug.Log(new StackFrame(true), e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int InviteTeam(MainPack mainPack, Server server, Team team)
        {
            if (mainPack.TeammatePack.SenderUid == mainPack.Uid && team == null && mainPack.TeammatePack.State == 0)
            {
                if (server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid) == null)
                {
                    return 2;//不在线
                }
                Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                MainPack mainPack1 = new MainPack();
                mainPack1.ActionCode = ActionCode.InvitedTeam;
                mainPack1.Uid = mainPack.TeammatePack.TargetUid;
                TeammatePack teammatePack = new TeammatePack();
                teammatePack.SenderUid = mainPack.TeammatePack.SenderUid;
                teammatePack.TargetUid = mainPack.TeammatePack.TargetUid;
                teammatePack.SenderName = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid).PlayerInfo.PlayerName;
                teammatePack.TargetName = target.PlayerInfo.PlayerName;
                mainPack1.TeammatePack = teammatePack;
                target.InvitedTeam(mainPack1);

                return 1;//成功
            }
            else
            {
                return 0;//失败
            }
        }

        public int AttackInvited(MainPack mainPack, Server server, Client client, Team team)
        {
            if (mainPack.TeammatePack.SenderUid == team.GetTeamMasterClient.PlayerInfo.Uid && mainPack.TeammatePack.State == 0)
            {
                if (team != null)
                {
                    //mainPack.TeammatePack.State = 2;
                    //mainPack.Uid = mainPack.TeammatePack.SenderUid;
                    //Client teamMaster = team.GetTeamMasterClient;
                    //mainPack.ActionCode = ActionCode.RefusedAttackInvite;
                    //teamMaster.RefusedAttack(mainPack);
                    return 2;
                }
                else
                {
                    try
                    {
                        MainPack mainPack1 = new MainPack();
                        mainPack1.Uid = mainPack.TeammatePack.TargetUid;
                        mainPack1.ActionCode = ActionCode.AttackInvited;
                        mainPack1.ReturnCode = ReturnCode.Success;
                        mainPack1.TeammatePack = mainPack.TeammatePack;
                        client.TcpSend(mainPack1);
                        return 1;//成功接收消息
                    }
                    catch (Exception e)
                    {
                        Debug.Log(new StackFrame(true), e.Message);
                        mainPack.ReturnCode = ReturnCode.Fail;
                        client.TcpSend(mainPack);
                        return 0;
                    }
                }
            }
            else
            {
                Debug.Log(new StackFrame(true), "失败");
                return 0;
            }
        }

        public int InvitedTeam(MainPack mainPack, Server server, Client client, Team team)
        {
            if (mainPack.TeammatePack.TargetUid == mainPack.Uid && mainPack.TeammatePack.State == 0)
            {
                if (team != null)
                {
                    mainPack.TeammatePack.State = 2;
                    mainPack.Uid = mainPack.TeammatePack.SenderUid;
                    Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid);
                    mainPack.ActionCode = ActionCode.RefusedInviteTeam;
                    sender.RefusedInviteTeam(mainPack);
                    return 2;//已有队伍
                }
                else
                {
                    try
                    {
                        MainPack mainPack1 = new MainPack();
                        mainPack1.Uid = mainPack.TeammatePack.TargetUid;
                        mainPack1.ActionCode = ActionCode.InvitedTeam;
                        mainPack1.ReturnCode = ReturnCode.Success;
                        mainPack1.TeammatePack = mainPack.TeammatePack;
                        client.TcpSend(mainPack1);
                        return 1;//成功接收消息
                    }
                    catch (Exception e)
                    {
                        Debug.Log(new StackFrame(true), e.Message);
                        mainPack.ReturnCode = ReturnCode.Fail;
                        client.TcpSend(mainPack);
                        return 0;
                    }
                }
            }
            else
            {
                Debug.Log(new StackFrame(true), "失败");
                return 0;//失败
            }
        }

        public int JoinTeamRequest(MainPack mainPack, Server server, Client  client)
        {
            try
            {
                if (mainPack.TeammatePack.JoinTeamPlayerUid == mainPack.Uid && client.IsInTheTeam == false && mainPack.TeammatePack.State == 0)
                {
                    if (server.GetClientFromDictByUid(mainPack.TeammatePack.TeamMemberUid) == null)
                    {
                        return 2;//不在线
                    }
                    Client teamMaster = server.GetClientFromDictByUid(mainPack.TeammatePack.TeamMemberUid);
                    MainPack mainPack1 = new MainPack();
                    mainPack1.ActionCode = ActionCode.PlayerJoinTeam;
                    mainPack1.Uid = mainPack.TeammatePack.TeamMemberUid;
                    TeammatePack teammatePack = new TeammatePack();
                    teammatePack.JoinTeamPlayerUid = mainPack.TeammatePack.JoinTeamPlayerUid;
                    teammatePack.TeamMemberUid = mainPack.TeammatePack.TeamMemberUid;
                    teammatePack.SenderName = client.PlayerInfo.PlayerName;
                    teammatePack.TargetName = teamMaster.PlayerInfo.PlayerName;
                    mainPack1.TeammatePack = teammatePack;
                    teamMaster.PlayerJoinTeam(mainPack1);
                    return 1;//成功
                }
                else
                {
                    return 3;//不正常情况
                }
                
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;//失败
            }
        }

        public int PlayerJoinTeam(MainPack mainPack, Server server, Client client)
        {
            try
            {
                if (mainPack.TeammatePack.TeamMemberUid == client.PlayerInfo.Uid)
                {
                    if (server.GetClientFromDictByUid(mainPack.TeammatePack.JoinTeamPlayerUid) == null)
                    {
                        return 2;//不在线
                    }
                    //发送给队长
                    try
                    {
                        mainPack.ReturnCode = ReturnCode.Success;
                        client.TcpSend(mainPack);
                    }
                    catch (Exception e)
                    {
                        
                        mainPack.ReturnCode = ReturnCode.Fail;
                        client.TcpSend(mainPack);
                        Debug.Log(new StackFrame(true), e.Message);
                        return 0;
                    }
                    return 1;
                }
                else
                {
                    return 3;
                }
                
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        public bool AcceptJoinTeam(MainPack mainPack, Server server, Client client)
        {
            try
            {
                if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.TeamMemberUid == mainPack.Uid)
                {
                    Client joinTarget = server.GetClientFromDictByUid(mainPack.TeammatePack.JoinTeamPlayerUid);
                    if (joinTarget != null && client.PlayerInfo.Uid == mainPack.TeammatePack.TeamMemberUid)
                    {
                        MainPack mainPack1 = new MainPack();
                        mainPack1.ActionCode = ActionCode.AcceptedJoinTeam;
                        mainPack1.Uid = mainPack.TeammatePack.TeamMemberUid;
                        mainPack.TeammatePack.SenderName = joinTarget.PlayerInfo.PlayerName;
                        mainPack.TeammatePack.TargetName = client.PlayerInfo.PlayerName;
                        mainPack1.TeammatePack = mainPack.TeammatePack;
                        
                        joinTarget.AcceptedJoinTeam(mainPack1);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }

        public bool AcceptedJoinTeam(MainPack mainPack, Client client, Server server)
        {
            try
            {
                if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.JoinTeamPlayerUid == client.PlayerInfo.Uid && client.team == null)
                {
                    Client teamMaster = server.GetClientFromDictByUid(mainPack.TeammatePack.TeamMemberUid);
                    teamMaster.team.Teammates.Add(client);
                    client.team = teamMaster.team;
                    client.IsInTheTeam = true;
                    client.EnemiesManager = null;
                    mainPack.ReturnCode = ReturnCode.Success;
                    client.TcpSend(mainPack);
                    return true;
                }
                else
                {
                    mainPack.ReturnCode = ReturnCode.Fail;
                    client.TcpSend(mainPack);
                    return false;
                }
            }
            catch (Exception e)
            {
                mainPack.ReturnCode = ReturnCode.Fail;
                client.TcpSend(mainPack);
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }

        public bool RefuseJoinTeam(MainPack mainPack, Client client, Server server)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.TeamMemberUid)
            {
                Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.JoinTeamPlayerUid);
                MainPack mainPack1 = new MainPack();
                mainPack1.ActionCode = ActionCode.RefusedJoinTeam;
                mainPack1.Uid = mainPack.TeammatePack.JoinTeamPlayerUid;
                mainPack.TeammatePack.SenderName = sender.PlayerInfo.PlayerName;
                mainPack.TeammatePack.TargetName = client.PlayerInfo.PlayerName;
                mainPack1.TeammatePack = mainPack.TeammatePack;
                sender.RefusedJoinTeam(mainPack1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefusedJoinTeam(MainPack mainPack, Client client)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.JoinTeamPlayerUid && client.PlayerInfo.Uid == mainPack.Uid)
            {
                mainPack.ReturnCode = ReturnCode.Success;
                client.TcpSend(mainPack);
                return true;
            }
            else
            {
                Debug.Log(new StackFrame(true), "失败");
                return false;
            }
        }

        public bool AcceptInviteTeam(MainPack mainPack, Server server)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.TargetUid == mainPack.Uid)
            {
                Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid);
                MainPack mainPack1 = new MainPack();
                mainPack1.ActionCode = ActionCode.AcceptedInviteTeam;
                mainPack1.Uid = mainPack.TeammatePack.SenderUid;
                mainPack1.TeammatePack = mainPack.TeammatePack;
                sender.AcceptedInviteTeam(mainPack1);
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool AcceptAttackInvite(MainPack mainPack, Client client)
        //{
        //    if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.TargetUid == mainPack.Uid)
        //    {
        //        Client teamMaster = client.team.GetTeamMasterClient;
        //        MainPack mainPack1 = new MainPack();
        //        mainPack1.ActionCode = ActionCode.AcceptedAttackInvite;
        //        mainPack1.Uid = mainPack.TeammatePack.SenderUid;
        //        mainPack1.TeammatePack = mainPack.TeammatePack;
        //        client.isReadyAttack = true;
        //        teamMaster.AcceptedAttackInvite(mainPack1);
        //        teamMaster.CheckTeammateAttackReady();
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool AcceptedInviteTeam(MainPack mainPack, Client client, UdpServer udpServer, Server server)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.SenderUid == mainPack.Uid)
            {
                if (client.team == null)
                {
                    client.team = new Team(client, udpServer, server);
                    Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                    client.team.Teammates.Add(target);
                    target.team = client.team;
                    client.IsInTheTeam = true;
                    target.IsInTheTeam = true;
                    target.EnemiesManager = null;
                    mainPack.ReturnCode = ReturnCode.Success;
                    client.TcpSend(mainPack);
                }
                else
                {
                    Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                    client.team.Teammates.Add(target);
                    target.team = client.team;
                    target.IsInTheTeam = true;
                    target.EnemiesManager = null;
                    mainPack.ReturnCode = ReturnCode.Success;
                    client.TcpSend(mainPack);
                }
                return true;
            }
            else
            {
                mainPack.ReturnCode = ReturnCode.Fail;
                client.TcpSend(mainPack);
                return false;
            }
        }

        public bool AcceptedAttackInvite(MainPack mainPack, Client client)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.SenderUid == mainPack.Uid)
            {
                mainPack.ReturnCode = ReturnCode.Success;
                client.TcpSend(mainPack);
                return true;
            }
            else
            {
                mainPack.ReturnCode = ReturnCode.Fail;
                client.TcpSend(mainPack);
                return false;
            }
        }

        public bool RefuseInviteTeam(MainPack mainPack, Client client, Server server)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.TargetUid)
            {
                client.team = null;
                Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid);
                MainPack mainPack1 = new MainPack();
                mainPack1.ActionCode = ActionCode.RefusedInviteTeam;
                mainPack1.Uid = mainPack.TeammatePack.SenderUid;
                mainPack1.TeammatePack = mainPack.TeammatePack;
                sender.RefusedInviteTeam(mainPack1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefuseAttack(MainPack mainPack, Client client)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.TargetUid)
            {
                Client teamMaster = client.team.GetTeamMasterClient;
                MainPack mainPack1 = new MainPack();
                mainPack1.ActionCode = ActionCode.RefusedInviteTeam;
                mainPack1.Uid = mainPack.TeammatePack.SenderUid;
                mainPack1.TeammatePack = mainPack.TeammatePack;
                teamMaster.RefusedAttack(mainPack1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefusedInviteTeam(MainPack mainPack, Client client)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.SenderUid)
            {
                mainPack.ReturnCode = ReturnCode.Success;
                client.TcpSend(mainPack);
                return true;
            }
            else
            {
                Debug.Log(new StackFrame(true), "失败");
                return false;
            }
        }

        public bool RefusedAttack(MainPack mainPack, Client client)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.SenderUid)
            {
                mainPack.ReturnCode = ReturnCode.Success;
                client.TcpSend(mainPack);
                return true;
            }
            else
            {
                Debug.Log(new StackFrame(true), "失败");
                return false;
            }
        }

        public MainPack GetTeammates(MainPack mainPack, Client client)
        {
            try
            {
                if (client.team == null)
                {
                    mainPack.ReturnCode = ReturnCode.NotFound;
                    return mainPack;
                }
                foreach (Client item in client.team.Teammates)
                {
                    if (mainPack.Uid == item.PlayerInfo.Uid)
                    {
                        continue;
                    }
                    FriendsPack friendsPack = new FriendsPack();
                    friendsPack.Player1Uid = item.PlayerInfo.Uid;
                    friendsPack.Player2Uid = mainPack.Uid;
                    mainPack.FriendsPack.Add(friendsPack);
                }
                //PlayerInfoPack充当队长uid信息的包
                TeammatePack teammatePack = new TeammatePack();
                teammatePack.TeamMemberUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;
                mainPack.TeammatePack = teammatePack;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public MainPack LeaveTeam(MainPack mainPack, Client client)//退队
        {
            try
            {
                TeammatePack teammatePack = new TeammatePack();
                teammatePack.LeaveTeamPlayerUid = client.PlayerInfo.Uid;//告知退队的人uid
                teammatePack.TeammateCount = client.team.Teammates.Count - 1;
                mainPack.TeammatePack = teammatePack;

                TeammatePack teammatePack1 = new TeammatePack();
                teammatePack1.LeaveTeamPlayerUid = client.PlayerInfo.Uid;
                teammatePack1.TeammateCount = client.team.Teammates.Count - 1;
                MainPack mainPack1 = new MainPack();
                mainPack1.Uid = mainPack.Uid;
                mainPack1.ActionCode = ActionCode.TeammateLeaveTeam;
                mainPack1.TeammatePack = teammatePack1;  

                if (client.Equals(client.team.Teammates[0]))
                {
                    client.team.GetTeamMasterClient = client.team.Teammates[1];

                    mainPack1.TeammatePack.TeamMemberUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;
                    client.team.Broadcast(client, mainPack1);

                    client.team.Teammates.Remove(client.team.Teammates[0]);
                }
                else
                {
                    mainPack1.TeammatePack.TeamMemberUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;

                    client.team.Broadcast(client, mainPack1);

                    client.team.Teammates.Remove(client);
                }

                client.IsInTheTeam = false;
                client.team = null;
                client.EnemiesManager = new EnemiesManager();
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }

        }

        public bool KickPlayer(MainPack mainPack, Client client, Server server)
        {
            if (mainPack.TeammatePack.SenderUid == client.PlayerInfo.Uid && mainPack.Uid == client.PlayerInfo.Uid && mainPack.Uid == client.team.GetTeamMasterClient.PlayerInfo.Uid)
            {
                Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                if (target != null)
                {
                    MainPack mainPack1 = new MainPack();
                    mainPack1.ActionCode = ActionCode.LeaveTeam;
                    mainPack1.RequestCode = RequestCode.Team;
                    mainPack1.Uid = mainPack.TeammatePack.TargetUid;
                    mainPack1.ReturnCode = ReturnCode.Success;
                    target.LeaveTeam(mainPack1);
                    target.TcpSend(mainPack1);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public MainPack BreakTeam(MainPack mainPack, Client client)
        {
            try
            {
                client.IsInTheTeam = false;
                client.team = null;
                client.EnemiesManager = new EnemiesManager();
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public bool TeamMasterAttackNotify(MainPack mainPack, Client client)
        {
            try
            {
                if (client.IsInTheTeam && mainPack.TeammatePack.TeamMasterUid == client.PlayerInfo.Uid && mainPack.TeammatePack.TeamMasterUid == client.team.GetTeamMasterClient.PlayerInfo.Uid)
                {
                    client.team.Broadcast(client, mainPack);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }
    }
}
