﻿using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;

namespace SocketServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(4567);
            Console.Read();
        }

    }
}