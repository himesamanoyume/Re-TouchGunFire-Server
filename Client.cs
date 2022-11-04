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
        Socket clientSocket;
        Socket udpClient;
        public EndPoint endPoint;
        UDPServer udpServer;
        Message message;
        UserData userData;
        Server server;
        MySqlConnection connection;
        string connectStr = "database=(数据库名称); data source=(IP);user=();password=();pooling=false;charset=utf8;port=3306";

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
            //m_connection.Open();

            this.server = server;
            this.clientSocket = clientSocket;
            
            StartReceive();
        }

        void StartReceive()
        {
            clientSocket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
        }

        void ReceiveCallback(IAsyncResult iar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
                int length = clientSocket.EndReceive(iar);
                if (length == 0) return;

                message.ReadBuffer(length, HandleRequest);
                StartReceive();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool Register(MainPack mainPack)
        {
            return GetUserData.Reigster(mainPack, connection);
        }

        public bool Login(MainPack mainPack)
        {
            return GetUserData.Login(mainPack, connection);
        }

        public void TcpSend(MainPack mainPack)
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            try
            {
                clientSocket.Send(Message.TcpPackData(mainPack));
            }
            catch
            {

            }
        }

        public void UdpSend(MainPack mainPack)
        {
            if (IEP)
            {

            }
        }

        void HandleRequest(MainPack mainPack)
        {
            server.HandleRequest(mainPack, this);
        }

        void Close()
        {
            server.RemoveClient(this);
            clientSocket.Close();
            connection.Close();
        }
    }
}
