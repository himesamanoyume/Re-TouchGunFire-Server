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
        List<Client> clientSockets = new List<Client>();
        ControllerManager controllerManager;

        public Server(int port)
        {
            controllerManager = new ControllerManager(this);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(0);
            Console.WriteLine("初始化结束");
            StartAccept();

            //Thread threadListen = new Thread(StartListen);
            //threadListen.IsBackground = true;
            //threadListen.Start();
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
    }
}
