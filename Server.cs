using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using MySql.Data;
using System.Collections.Generic;
using System.Diagnostics;
using SocketServer.Utils;

namespace SocketServer
{
    internal class Server
    {
        Socket serverSocket;
        UdpServer udpServer;
        Thread thread;
        List<Client> clientList = new List<Client>();
        ControllerManager controllerManager;
        Dictionary<int, Client> clientDict = new Dictionary<int, Client>();
        

        public Server(int port)
        {
            controllerManager = new ControllerManager(this);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            serverSocket.Listen(0);
            StartAccept();
            Debug.Log(new StackFrame(true), "初始化结束");

            udpServer = new UdpServer(6678, this, controllerManager);
        }

        ~Server()
        {
            Debug.Log(new StackFrame(true), "Server关闭");
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
            clientList.Add(new Client(client, this, udpServer));
            Debug.Log(new StackFrame(true), "新的连接, 当前客户端人数: " + clientList.Count);
            StartAccept();
        }

        public void HandleRequest(MainPack pack, Client client)
        {
            controllerManager.HandleRequest(pack, client);
        }

        public void RemoveClient(Client client)
        {
            clientList.Remove(client);
            if (clientDict.TryGetValue(client.PlayerInfo.Uid, out Client clientAtDict))
            {
                clientDict.Remove(clientAtDict.PlayerInfo.Uid);
            }
        }

        public bool SetEndPoint(EndPoint endPoint, int uid)
        {
            foreach (Client client in clientList)
            {
                if (client.PlayerInfo.Uid == uid)
                {
                    client.endPoint = endPoint;
                    return true;
                }
            }
            return false;
        }

        public Client GetClientFromListByUid(int uid)
        {
            foreach (Client client in clientList)
            {
                if (client.PlayerInfo.Uid == uid)
                {
                    return client;
                }
            }
            return null;
        }

        public Client GetClientFromDictByUid(int uid)
        {
            if (clientDict.TryGetValue(uid, out Client client) && client.PlayerInfo.Uid == uid)
            {
                return client;
            }
            else
            {
                return null;
            }
        }

        public void AddClientToDict(Client c)
        {
            if (c.PlayerInfo.Uid != 0)
            {
                clientDict.Add(c.PlayerInfo.Uid, c);
            }
        }
    }
}
