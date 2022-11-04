using System;
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
        Client masterClient;
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
            for (int i = 0; i < teammates.Count; i++)
            {
                if (teammates[i].GetPlayerInfo.UID == client.GetPlayerInfo.UID)
                {
                    continue;
                }
                udpServer.UdpSend(mainPack, teammates[i].endPoint);
            }
        }

        public void TcpBroadcast(Server server, Client client, MainPack mainPack)
        {

        }

        public void LeaveTeam(Client client)
        {
            MainPack mainPack = new MainPack();
            if (client == teammates[0])
            {
                mainPack.ActionCode = ActionCode.LevelTeam;
                mainPack.Uid = client.GetPlayerInfo.UID;
                TcpBroadcast(server, client, mainPack);
                teammates.Remove(teammates[0]);
                if (teammates.Count == 0)
                {
                    BreakTeam();
                }
                else
                {
                    masterClient = teammates[0];
                }
                //该方法未完善
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
    }
}
