using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketProtocol;
using System.Diagnostics;
using SocketServer.Utils;

namespace SocketServer
{
    internal class UdpServer
    {
        Socket udpServer;
        IPEndPoint bindEP;
        EndPoint remoteEP;

        Server server;
        ControllerManager controllerManager;

        byte[] buffer = new byte[1024];
        Thread receiveThread;

        public UdpServer(int port, Server server, ControllerManager controllerManager)
        {
            this.server = server;
            this.controllerManager = controllerManager;
            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            bindEP = new IPEndPoint(IPAddress.Any, port);
            remoteEP = (EndPoint)bindEP;
            udpServer.Bind(bindEP);
            receiveThread = new Thread(RecevieMsg);
            receiveThread.Start();
            Debug.Log(new StackFrame(true), "UDP服务器已启动");
        }

        ~UdpServer(){
            if (receiveThread != null)
            {
                receiveThread.Abort();
                receiveThread = null;
            }
        }

        public void RecevieMsg()
        {
            while (true)
            {
                int len = udpServer.ReceiveFrom(buffer, ref remoteEP);
                MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 0, len);
                HandleRequest(pack, remoteEP);
            }
        }

        public void HandleRequest(MainPack mainPack, EndPoint endPoint)
        {
            Client client = server.GetClientFromListByUid(mainPack.Uid);
            if (client.endPoint == null)
            {
                client.endPoint = endPoint;
            }
            controllerManager.HandleRequest(mainPack, client, true);
        }

        public void UdpSend(MainPack mainPack, EndPoint endPoint)
        {
            byte[] buffer = Message.UdpPackData(mainPack);
            udpServer.SendTo(buffer, buffer.Length, SocketFlags.None, endPoint);
        }
    }
}
