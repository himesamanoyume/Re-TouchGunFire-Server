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
        UserData userData;
        public bool isTeammate = false;
        public Teammate teammate;
        Server server;
        MySqlConnection connection;
        string connectStr = "database=hime; data source=princessdreamland.design; user=lzp; password=lzp19990510; pooling=false;charset=utf8;port=3306";

        public UserData GetUserData
        {
            get { return userData; }
        }

        public PlayerInfo GetPlayerInfo { get; set; }

        public Client(Socket clientSocket, Server server)
        {
            userData = new UserData();
            message = new Message();
            connection = new MySqlConnection(connectStr);
            connection.Open();

            this.server = server;
            this.tcpSocket = clientSocket;
            
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
            return GetUserData.Reigster(mainPack, connection);
        }

        public MainPack Login(MainPack mainPack)
        {
            return GetUserData.Login(mainPack, connection);
        }

        public MainPack InitPlayerInfo(MainPack mainPack)
        {
            return GetUserData.InitPlayerInfo(mainPack, connection);
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
