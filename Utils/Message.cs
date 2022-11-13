using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;

namespace SocketServer.Utils
{
    internal class Message
    {
        private byte[] m_buffer = new byte[1024];
        private int m_startIndex;

        public byte[] Buffer
        {
            get { return m_buffer; }
        }

        public int StartIndex
        {
            get { return m_startIndex; }
        }

        public int Remsize
        {
            get { return m_buffer.Length - m_startIndex; }
        }

        public void ReadBuffer(int length, Action<MainPack> handleRequest)
        {
            m_startIndex += length;

            while (true)
            {
                if (m_startIndex <= 4) return;
                int count = BitConverter.ToInt32(m_buffer, 0);
                if (m_startIndex >= count + 4)
                {
                    MainPack mainPack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(m_buffer, 4, count);
                    handleRequest(mainPack);
                    Array.Copy(m_buffer, count + 4, m_buffer, 0, m_startIndex - count - 4);
                    m_startIndex -= count + 4;
                }
                else
                {
                    break;
                }
            }

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
