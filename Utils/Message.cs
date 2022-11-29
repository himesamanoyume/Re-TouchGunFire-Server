using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;
using static SocketServer.Client;

namespace SocketServer.Utils
{
    internal class Message
    {
        private byte[] buffer = new byte[1024];
        private int startIndex;

        public byte[] Buffer
        {
            get { return buffer; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }

        public int Remsize
        {
            get { return buffer.Length - startIndex; }
        }

        public int InitTotalDataSize()
        {
            totalDataSize = BitConverter.ToInt32(buffer, 0);
            return totalDataSize;
        }

        public int TotalDataSize
        {
            get { return totalDataSize; }
        }
        int totalDataSize;

        public void ReadBuffer(int bufferSize, Action<MainPack> handleRequest)
        {
            startIndex += bufferSize;

            while (true)
            {
                if (startIndex <= 4) return;
                int dataSize = BitConverter.ToInt32(buffer, 0);
                if (startIndex >= dataSize + 4)
                {
                    MainPack mainPack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, dataSize);
                    handleRequest(mainPack);
                    //Array.Copy(m_buffer, count + 4, m_buffer, 0, m_startIndex - count - 4);
                    startIndex -= dataSize + 4;
                }
                else
                {
                    break;
                }
            }
        }

        public void ReadBigBuffer(byte[] bigBuffer, Action<MainPack> handleResponse)
        {

            MainPack mainPack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(bigBuffer, 4, TotalDataSize);
            handleResponse(mainPack);
        }

        public static byte[] TcpPackData(MainPack mainPack)
        {
            
            byte[] data = mainPack.ToByteArray(); //包体
            byte[] head = BitConverter.GetBytes(data.Length);//包头
            return head.Concat(data).ToArray();
        }

        public static byte[] UdpPackData(MainPack mainPack)
        {
            return mainPack.ToByteArray();
        }
    }
}
