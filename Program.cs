using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using System.Diagnostics;
using SocketServer.Utils;

namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(4567);
            if (Console.ReadLine() == "stop")
            {
                Debug.Log(new StackFrame(true), "服务器关闭中");
                server = null;
                GC.Collect();
                
            }
        }

    }
}