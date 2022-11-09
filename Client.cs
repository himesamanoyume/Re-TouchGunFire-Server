using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using MySql.Data.MySqlClient;

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
        public bool isTeammate = false;
        public Teammate teammate;
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

        public int clientPlayerUid;

        public Client(Socket clientSocket, Server server)
        {
            userFunction = new UserFunction();
            friendFunction = new FriendFunction();
            message = new Message();
            connection = new MySqlConnection(connectStr);
            connection.Open();

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
            if (server.ClientByUID(_mainPack.PlayerInfoPack.Uid) == null)
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
            Console.WriteLine("断开连接");
            if (isTeammate && teammate != null)
            {
                teammate.LeaveTeam(this);
            }
            tcpSocket.Close();
            connection.Close();
        }
    }
}
