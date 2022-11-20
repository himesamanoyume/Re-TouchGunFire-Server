using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using SocketServer.Friend;
using SocketServer.User;
using SocketServer.Utils;
using SocketServer.Teammate;
using SocketServer.Gaming;

namespace SocketServer
{
    internal class Client
    {
        Socket tcpSocket;
        Socket udpSocket;
        public EndPoint endPoint;
        UdpServer udpServer;
        Message message;
        UserFunction userFunction;
        FriendFunction friendFunction;
        TeamFunction teamFunction;
        GameFunction gameFunction;
        public bool IsInTheTeam = false;
        public Team team = null;
        Server server;
        MySqlConnection connection;
        
        string connectStr = "database=hime; data source=47.106.183.112; user=lzp; password=lzp19990510; pooling=false;charset=utf8;port=3306";

        public UserFunction GetUserFunction
        {
            get { return userFunction; }
        }

        public FriendFunction GetFriendFunction
        {
            get { return friendFunction; }
        }

        public TeamFunction GetTeamFunction
        {
            get { return teamFunction; }
        }

        public GameFunction GetGameFunction
        {
            get { return gameFunction; }
        }

        public PlayerInfo PlayerInfo;
        public delegate void Buff(PlayerInfo playerInfo);
        public Buff buff;
        public void ChangePlayerInfo()
        {
            PlayerInfo defaultPlayerInfo = new PlayerInfo();
            buff?.Invoke(defaultPlayerInfo);
            PlayerInfo = defaultPlayerInfo;
        }
        //public int clientPlayerUid;

        public Client(Socket clientSocket, Server server, UdpServer udpServer)
        {
            userFunction = new UserFunction();
            friendFunction = new FriendFunction();
            teamFunction = new TeamFunction();
            gameFunction = new GameFunction();
            message = new Message();
            connection = new MySqlConnection(connectStr);
            connection.Open();
            PlayerInfo = new PlayerInfo();

            this.udpServer = udpServer;
            this.server = server;
            tcpSocket = clientSocket;

            StartReceive();
        }

        void StartReceive()
        {
            tcpSocket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
        }

        void ReceiveCallback(IAsyncResult iar)
        {
            try
            {
                if (tcpSocket == null || tcpSocket.Connected == false) return;
                int length = tcpSocket.EndReceive(iar);
                if (length == 0)
                {
                    Close();
                    return;
                }
                message.ReadBuffer(length, HandleRequest);
                StartReceive();
            }
            catch
            {
                Close();
            }
        }

        public bool Register(MainPack mainPack)
        {
            return GetUserFunction.Reigster(mainPack, connection);
        }

        public MainPack Login(MainPack mainPack)
        {
            MainPack mainPack1 = GetUserFunction.Login(mainPack, connection);
            PlayerInfo.Uid = mainPack1.Uid;
            server.AddClientToDict(this);
            return mainPack1;
        }

        public MainPack InitPlayerInfo(MainPack mainPack)
        {
            mainPack = GetUserFunction.InitPlayerInfo(mainPack, connection);
            PlayerInfo.Uid = mainPack.PlayerInfoPack.Uid;
            PlayerInfo.PlayerName = mainPack.PlayerInfoPack.PlayerName;
            PlayerInfo.Level = mainPack.PlayerInfoPack.Level;
            PlayerInfo.CurrentExp = mainPack.PlayerInfoPack.CurrentExp;
            PlayerInfo.Coin = mainPack.PlayerInfoPack.Coin;
            PlayerInfo.Diamond = mainPack.PlayerInfoPack.Diamond;
            //查装备

            //end
            //查武器

            //end
            return mainPack;
        }

        public MainPack UpdatePlayerInfo(MainPack mainPack, Client client)
        {
            return GetGameFunction.UpdatePlayerInfo(mainPack, this);
        }

        public bool Regeneration(MainPack mainPack)
        {
            return GetGameFunction.Regeneration(mainPack, this);
        }

        public int SendRequestFriend(MainPack mainPack)
        {
            return GetFriendFunction.SendRequestFriend(mainPack, connection);
        }

        public MainPack SearchFriend(MainPack mainPack)
        {
            return GetFriendFunction.SearchFriend(mainPack, connection);
        }

        public MainPack GetFriends(MainPack mainPack)
        {
            return GetFriendFunction.GetFriends(mainPack, connection);
        }

        public MainPack GetFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.GetFriendRequest(mainPack, connection);
        }

        public int AcceptFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.AcceptFriendRequest(mainPack, connection);
        }

        public MainPack GetPlayerBaseInfo(MainPack mainPack)
        {
            mainPack = GetUserFunction.GetPlayerBaseInfo(mainPack, connection);
            Client client = server.GetClientFromDictByUid(mainPack.PlayerInfoPack.Uid);
            if (client == null)
            {
                mainPack.PlayerInfoPack.IsOnline = false;
                mainPack.PlayerInfoPack.IsTeam = false;
            }
            else
            {
                if (client.IsInTheTeam)
                {
                    if (client.team == team)
                    {
                        if (client.team.GetTeamMasterClient.PlayerInfo.Uid == mainPack.PlayerInfoPack.Uid)
                        {
                            mainPack.PlayerInfoPack.IsTeamMaster = true;
                        }
                        else
                        {
                            mainPack.PlayerInfoPack.IsTeamMaster = false;
                        }
                        mainPack.PlayerInfoPack.IsSameTeam = true;
                    }
                    else
                    {
                        mainPack.PlayerInfoPack.IsSameTeam = false;
                    }
                    mainPack.PlayerInfoPack.IsTeam = true;
                }
                else
                {
                    mainPack.PlayerInfoPack.IsTeam = false;
                }
                mainPack.PlayerInfoPack.IsOnline = true;
            }
            return mainPack;
        }

        public int RefuseFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.RefuseFriendRequest(mainPack, connection);
        }

        public bool DeleteFriend(MainPack mainPack)
        {
            return GetFriendFunction.DeleteFriend(mainPack, connection);
        }

        public int InviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.InviteTeam(mainPack, server, server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid).team);
        }

        public int InvitedTeam(MainPack mainPack)//被邀请者
        {
            return GetTeamFunction.InvitedTeam(mainPack, server, this, team);
        }

        public bool AcceptInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptInviteTeam(mainPack, server);
        }

        public bool AcceptedInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptedInviteTeam(mainPack, this, udpServer, server);
        }

        public bool RefuseInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefuseInviteTeam(mainPack, this, server);
        }

        public bool RefusedInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefusedInviteTeam(mainPack, this);
        }

        public MainPack GetTeammates(MainPack mainPack)
        {
            return GetTeamFunction.GetTeammates(mainPack, this);
        }

        public MainPack LeaveTeam(MainPack mainPack)
        {
            return GetTeamFunction.LeaveTeam(mainPack, this);
        }

        public int JoinTeamRequest(MainPack mainPack)
        {
            return GetTeamFunction.JoinTeamRequest(mainPack, server, this);
        }

        public int PlayerJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.PlayerJoinTeam(mainPack, server, this);
        }

        public bool AcceptJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptJoinTeam(mainPack, server, this);
        }

        public bool AcceptedJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptedJoinTeam(mainPack, this, server);
        }

        public bool RefuseJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefuseJoinTeam(mainPack, this, server);
        }

        public bool RefusedJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefusedJoinTeam(mainPack, this);
        }

        public bool KickPlayer(MainPack mainPack)
        {
            return GetTeamFunction.KickPlayer(mainPack, this, server);
        }

        public void TcpSend(MainPack mainPack)
        {
            if (tcpSocket == null || tcpSocket.Connected == false) return;
            try
            {
                Debug.Log(new StackFrame(true), "TCP发送至UID:"+ mainPack.Uid +",ActionCode:"+ mainPack.ActionCode);
                tcpSocket.Send(Message.TcpPackData(mainPack));
            }
            catch(Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
            }
        }

        public void UdpSend(MainPack mainPack)
        {
            if (endPoint == null) return;
            try
            {
                //Debug.Log(new StackFrame(true), "UDP发送:"+ mainPack.Uid + ",ActionCode:" + mainPack.ActionCode);
                udpServer.UdpSend(mainPack, endPoint);
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
            }
        }

        void HandleRequest(MainPack mainPack)
        {
            server.HandleRequest(mainPack, this);
        }

        void Close()
        {
            server.RemoveClient(this);
            Debug.Log(new StackFrame(true), "断开连接");
            if (IsInTheTeam && team != null)
            {
                MainPack mainPack = new MainPack();
                mainPack.Uid = PlayerInfo.Uid;
                mainPack.ActionCode = ActionCode.LeaveTeam;
                GetTeamFunction.LeaveTeam(mainPack, this);
            }
            tcpSocket.Close();
            connection.Close();
        }
    }
}
