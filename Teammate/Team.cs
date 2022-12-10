using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocketProtocol;

namespace SocketServer
{
     class Team
    {
        Server server;
        Client masterClient = null;
        public UdpServer udpServer;
        List<Client> teammates = new List<Client>();

        public Team(Client masterClient, UdpServer udpServer, Server server)
        {
            this.udpServer = udpServer;
            teammates.Add(masterClient);
            this.masterClient = masterClient;
            this.server = server;
        }

        public Client GetTeamMasterClient
        {
            get { return masterClient; }
            set { masterClient = value; }
        }

        public List<Client> Teammates
        {
            get { return teammates; }
            set { teammates = value; }
        }

        /// <summary>
        /// 广播消息 默认TCP转发
        /// </summary>
        /// <param name="client"></param>
        /// <param name="mainPack"></param>
        /// <param name="isUDP"></param>
        public void Broadcast(Client client, MainPack mainPack, bool isUDP = false)
        {
            foreach (Client c in teammates)
            {
                if (c.Equals(client))
                {
                    continue;
                }
                mainPack.Uid = c.PlayerInfo.Uid;
                mainPack.ReturnCode = ReturnCode.Success;
                if (isUDP)
                {
                    c.UdpSend(mainPack);
                }
                else
                {
                    c.TcpSend(mainPack);
                } 
            }
        }

       

        //public void JoinTeam(Client client)
        //{
        //    client.isInTheTeam = true;
        //    client.team = this;
        //    //...
        //}
    }
}
