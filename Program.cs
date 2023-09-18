using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using System.Diagnostics;
using SocketServer.Utils;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(4567);
            Test(5500f, 1f);
            if (Console.ReadLine() == "stop")
            {
                Debug.Log(new StackFrame(true), "服务器关闭中");
                server = null;
                GC.Collect();
            }
    
            void Test(float currentMoney, float week)
            {
                float realPayMoney = 0f;
                float realPayTimes = 0f;
                float targetMoney = 10000f;
                float needMoney = 550f;
            
                realPayTimes = (float)Math.Floor(currentMoney / needMoney);
                realPayMoney = realPayTimes * needMoney;
                currentMoney = (currentMoney - realPayMoney) + realPayMoney * 1.15f;
                Console.WriteLine(week + "Week, Get " + (realPayMoney * 0.15f) + ", now you have "+currentMoney);
                

                if (currentMoney >= targetMoney)
                {
                    Console.WriteLine("need " +week+" week to get 1w, now current money: " + currentMoney);
                }
                else
                {
                    week++;
                    Test(currentMoney, week);
                    return;
                }
            }
        }


        

    }

    
}