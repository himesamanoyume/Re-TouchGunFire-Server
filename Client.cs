using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using MySql.Data.MySqlClient;
using System.Diagnostics;

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
        public bool isInTheTeam = false;
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

        public int clientPlayerUid;

        public Client(Socket clientSocket, Server server, UdpServer udpServer)
        {
            userFunction = new UserFunction();
            friendFunction = new FriendFunction();
            teamFunction = new TeamFunction();
            message = new Message();
            connection = new MySqlConnection(connectStr);
            connection.Open();

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
            clientPlayerUid = mainPack1.Uid;
            server.AddClientToDict(this);
            return mainPack1;
        }

        public MainPack InitPlayerInfo(MainPack mainPack)
        {
            return GetUserFunction.InitPlayerInfo(mainPack, connection);
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
            MainPack _mainPack = GetUserFunction.GetPlayerBaseInfo(mainPack, connection);
            if (server.GetClientFromDictByUid(_mainPack.PlayerInfoPack.Uid) == null)
            {
                _mainPack.PlayerInfoPack.IsOnline = false;
            }
            else
            {
                _mainPack.PlayerInfoPack.IsOnline = true;
            }
            return _mainPack;
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
            return GetTeamFunction.InviteTeam(mainPack, server, team);
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

        public void JoinTeam(MainPack mainPack)
        {

        }

        public void JoinTeam(MainPack mainPack, Team team)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.Uid == mainPack.TeammatePack.TargetUid)
            {
                this.team = team;
                mainPack.Uid = mainPack.TeammatePack.TargetUid;
                this.team.Broadcast(this, mainPack);
                //return true;
            }
            else
            {

            }
        }

        public void TcpSend(MainPack mainPack)
        {
            if (tcpSocket == null || tcpSocket.Connected == false) return;
            try
            {
                tcpSocket.Send(Message.TcpPackData(mainPack));
            }
            catch
            {

            }
        }

        public void UdpSend(MainPack mainPack)
        {
            if (endPoint == null) return;
            udpServer.UdpSend(mainPack, endPoint);
        }

        void HandleRequest(MainPack mainPack)
        {
            server.HandleRequest(mainPack, this);
        }

        void Close()
        {
            server.RemoveClient(this);
            Debug.Log(new StackFrame(true), "断开连接");
            if (isInTheTeam && team != null)
            {
                team.LeaveTeam(this);
            }
            tcpSocket.Close();
            connection.Close();
        }
    }
}
