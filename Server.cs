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
            clientSockets.Add(new Client(client, this, udpServer));
            Console.WriteLine("新的连接, 当前客户端人数: " + clientSockets.Count);
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
                if (client.clientPlayerUid == uid)
                {
                    client.endPoint = endPoint;
                    return true;
                }
            }
            return false;
        }

        public Client ClientByUID(int uid)
        {
            foreach (Client client in clientSockets)
            {
                if (client.clientPlayerUid == uid)
                {
                    return client;
                }
            }
            return null;
        }
    }
}
