﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SocketProtocol;

namespace SocketServer
{
    internal class Teammate
    {
        Server server;
        Client masterClient = null;
        public UDPServer udpServer;
        List<Client> teammates = new List<Client>();

        public Teammate(Client masterClient, UDPServer udpServer, Server server)
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

        public void UdpBroadcast(Client client, MainPack mainPack)
        {
            foreach (Client c in teammates)
            {
                if (c.Equals(client))
                {
                    continue;
                }
                c.UdpSend(mainPack);
            }
        }

        public void TcpBroadcast(Client client, MainPack mainPack)
        {
            foreach (Client c in teammates)
            {
                if (c.Equals(client))
                {
                    continue;
                }
                c.TcpSend(mainPack);
            }
        }

        public void LeaveTeam(Client client)
        {
            MainPack mainPack = new MainPack();
            mainPack.ActionCode = ActionCode.LevelTeam;
            mainPack.Uid = client.GetPlayerInfo.UID;
            client.isTeammate = false;
            client.teammate = null;
            TcpBroadcast(client, mainPack);
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
                mainPack.Uid = client.GetPlayerInfo.UID;
            }
            
        }

        public void JoinTeam(Client client)
        {
            client.isTeammate = true;
            client.teammate = this;
            //...
        }

        public void UpdateTeammateInfo()
        {

        }
    }
}
