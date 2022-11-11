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
        UDPServer udpServer;
        Message message;
        UserFunction userFunction;
        FriendFunction friendFunction;
        TeamFunction teamFunction;
        public bool isTeammate = false;
        public Teammate teammate = null;
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

        public Client(Socket clientSocket, Server server, UDPServer udpServer)
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
            if (mainPack.TeammatePack.SenderUid == mainPack.Uid && teammate == null && mainPack.TeammatePack.State == 0)
            {
                if (server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid) == null)
                {
                    return 2;//不在线
                }
                Client target = server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid);
                MainPack mainPack1 = new MainPack();
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

        public int InvitedTeam(MainPack mainPack)//被邀请者
        {
            if (mainPack.TeammatePack.TargetUid == mainPack.Uid && mainPack.TeammatePack.State == 0)
            {
                if (teammate!=null)
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
                        Debug.Log(new StackFrame(true), "即将发送InvitedTeam包");
                        mainPack.Uid = mainPack.TeammatePack.TargetUid;
                        mainPack.ActionCode = ActionCode.InvitedTeam;
                        mainPack.ReturnCode = ReturnCode.Success;
                        TcpSend(mainPack);
                        return 1;//成功接收消息
                    }
                    catch (Exception e)
                    {
                        Debug.Log(new StackFrame(true), e.Message);
                        mainPack.ReturnCode = ReturnCode.Fail;
                        TcpSend(mainPack);
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

        public bool AcceptInviteTeam(MainPack mainPack)
        {
            if (mainPack.TeammatePack.State == 0 && mainPack.TeammatePack.TargetUid == mainPack.Uid)
            {
                Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid);
                mainPack.TeammatePack.State = 1;
                mainPack.Uid = mainPack.TeammatePack.SenderUid;
                //JoinTeam(mainPack, sender.teammate);

                mainPack.ActionCode = ActionCode.AcceptedInviteTeam;
                sender.TcpSend(mainPack);
                //sender.AcceptedInviteTeam(mainPack);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AcceptedInviteTeam(MainPack mainPack)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.TeammatePack.SenderUid == mainPack.Uid)
            {
                teammate = new Teammate(this, udpServer, server);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefuseInviteTeam(MainPack mainPack)
        {
            if (mainPack.TeammatePack.State == 0 && mainPack.Uid == mainPack.TeammatePack.TargetUid)
            {
                teammate = null;
                Client sender = server.GetClientFromDictByUid(mainPack.TeammatePack.SenderUid);
                mainPack.Uid = mainPack.TeammatePack.SenderUid;
                mainPack.TeammatePack.State = 2;
                sender.RefusedInviteTeam(mainPack);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefusedInviteTeam(MainPack mainPack)
        {
            if (mainPack.TeammatePack.State == 2 && mainPack.Uid == mainPack.TeammatePack.SenderUid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void JoinTeam(MainPack mainPack)
        {

        }

        public void JoinTeam(MainPack mainPack, Teammate teammate)
        {
            if (mainPack.TeammatePack.State == 1 && mainPack.Uid == mainPack.TeammatePack.TargetUid)
            {
                this.teammate = teammate;
                mainPack.Uid = mainPack.TeammatePack.TargetUid;
                this.teammate.Broadcast(this, mainPack);
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
            if (isTeammate && teammate != null)
            {
                teammate.LeaveTeam(this);
            }
            tcpSocket.Close();
            connection.Close();
        }
    }
}
