using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocketProtocol;

namespace SocketServer
{
    internal class Team
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

        public List<Client> GetTeammates
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
                mainPack.Uid = c.clientPlayerUid;
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

        public void LeaveTeam(Client client)
        {
            MainPack mainPack = new MainPack();
            mainPack.ActionCode = ActionCode.LeaveTeam;
            mainPack.Uid = client.clientPlayerUid;
            client.isInTheTeam = false;
            client.team = null;
            Broadcast(client, mainPack);
            if (client.Equals(teammates[0]))
            {
                teammates.Remove(teammates[0]);
                if (teammates.Count == 0)
                    BreakTeam();
                else
                    masterClient = teammates[0];
            }
            else
            {
                teammates.Remove(client);
            }
        }

        public void BreakTeam(Client client = null)
        {
            if (client != null)
            {
                MainPack mainPack = new MainPack();
                mainPack.ActionCode = ActionCode.BreakTeam;
                mainPack.Uid = client.clientPlayerUid;
            }
            
        }

        public void JoinTeam(Client client)
        {
            client.isInTheTeam = true;
            client.team = this;
            //...
        }

        public void UpdateTeam()
        {

        }

        public void InviteTeam()
        {

        }
    }
}
