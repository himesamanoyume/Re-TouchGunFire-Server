using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using MySql.Data;

namespace SocketServer
{
    internal class Server
    {
        Socket serverSocket;
        UDPServer udpServer;
        Thread thread;
        List<Client> clientSockets = new List<Client>();
        ControllerManager controllerManager;

        public Server(int port)
        {
            controllerManager = new ControllerManager(this);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(0);
            StartAccept();
            Console.WriteLine("初始化结束");

            udpServer = new UDPServer(6678, this, controllerManager);
            //Thread threadListen = new Thread(StartListen);
            //threadListen.IsBackground = true;
            //threadListen.Start();
        }

        ~Server()
        {
            Console.WriteLine("Server关闭");
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        void StartAccept()
        {
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        void AcceptCallback(IAsyncResult iar)
        {
            Socket client = serverSocket.EndAccept(iar);
            Console.WriteLine("新的连接");
            clientSockets.Add(new Client(client, this));
            StartAccept();
        }

        public void HandleRequest(MainPack pack, Client client)
        {
            controllerManager.HandleRequest(pack, client);
        }

        public void RemoveClient(Client client)
        {
            clientSockets.Remove(client);
        }

        public bool SetEndPoint(EndPoint endPoint, int uid)
        {
            foreach (Client client in clientSockets)
            {
                if (client.GetPlayerInfo.UID == uid)
                {
                    client.endPoint = endPoint;
                    return true;
                }
            }
            return false;
        }

        public Client ClientFromUID(int uid)
        {
            foreach (Client client in clientSockets)
            {
                if (client.GetPlayerInfo.UID == uid)
                {
                    return client;
                }
            }
            return null;
        }

    }
}
