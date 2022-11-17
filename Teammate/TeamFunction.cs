using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocketProtocol;

namespace SocketServer.Teammate
{
    internal class TeamFunction
    {
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
                mainPack1.TeammatePack = teammatePack;
                target.InvitedTeam(mainPack1);

                return 1;//成功
            }
            else
            {
                return 0;//失败
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
                        mainPack.Uid = mainPack.TeammatePack.TargetUid;
                        mainPack.ActionCode = ActionCode.InvitedTeam;
                        mainPack.ReturnCode = ReturnCode.Success;
                        client.TcpSend(mainPack);
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
                    if (server.GetClientFromDictByUid(mainPack.TeammatePack.TeamMasterUid) == null)
                    {
                        return 2;//不在线
                    }
                    Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                    MainPack mainPack1 = new MainPack();
                    mainPack1.ActionCode = ActionCode.PlayerJoinTeam;
                    mainPack1.Uid = mainPack.TeammatePack.TargetUid;
                    TeammatePack teammatePack = new TeammatePack();
                    teammatePack.JoinTeamPlayerUid = mainPack.TeammatePack.JoinTeamPlayerUid;
                    teammatePack.TeamMasterUid = mainPack.TeammatePack.TeamMasterUid;
                    mainPack1.TeammatePack = teammatePack;
                    target.PlayerJoinTeam(mainPack1);
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
                if (mainPack.TeammatePack.TeamMasterUid == client.PlayerInfo.Uid)
                {
                    if (server.GetClientFromDictByUid(mainPack.TeammatePack.JoinTeamPlayerUid) == null)
                    {
                        return 2;//不在线
                    }
                    //发送给队长

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
                if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.TeamMasterUid == mainPack.Uid)
                {
                    Client joinTarget = server.GetClientFromDictByUid(mainPack.TeammatePack.JoinTeamPlayerUid);
                    if (joinTarget != null && client.PlayerInfo.Uid == mainPack.TeammatePack.TeamMasterUid)
                    {
                        MainPack mainPack1 = new MainPack();
                        mainPack1.ActionCode = ActionCode.AcceptedJoinTeam;
                        mainPack1.Uid = mainPack.TeammatePack.TeamMasterUid;
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
                    Client teamMaster = server.GetClientFromDictByUid(mainPack.TeammatePack.TeamMasterUid);
                    teamMaster.team.Teammates.Add(client);
                    client.team = teamMaster.team;
                    client.IsInTheTeam = true;
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
                    mainPack.ReturnCode = ReturnCode.Success;
                    client.TcpSend(mainPack);
                }
                else
                {
                    Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                    client.team.Teammates.Add(target);
                    target.team = client.team;
                    client.IsInTheTeam = true;
                    target.IsInTheTeam = true;
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
                teammatePack.TeamMasterUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;
                mainPack.TeammatePack = teammatePack;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public MainPack LeaveTeam(MainPack mainPack, Client client)//自身退队
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

                    mainPack1.TeammatePack.TeamMasterUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;
                    client.team.Broadcast(client, mainPack1);

                    client.team.Teammates.Remove(client.team.Teammates[0]);
                }
                else
                {
                    mainPack1.TeammatePack.TeamMasterUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;

                    client.team.Broadcast(client, mainPack1);

                    client.team.Teammates.Remove(client);
                }

                client.IsInTheTeam = false;
                client.team = null;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }

        }

        public MainPack BreakTeam(Client client, MainPack mainPack)
        {
            try
            {
                client.IsInTheTeam = false;
                client.team = null;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }
    }
}
