using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer
{
    internal class TeamController : BaseController
    {

        public TeamController()
        {
            requestCode = RequestCode.Team;
        }

        public MainPack LeaveTeam(Client client, MainPack mainPack)
        {
            //if ()
            //{

            //}
            return null;
        }

        public MainPack InviteTeam(Client client, MainPack mainPack)
        {
            if (client.InviteTeam(mainPack) == 1)
            {
                Console.WriteLine("InviteTeam Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if(client.InviteTeam(mainPack) == 0)
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else
            {
                Console.WriteLine("No Online");
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            return mainPack;
        }

        public MainPack InvitedTeam(Client client, MainPack mainPack)
        {
            Console.WriteLine("处理InvitedTeam");
            if (client.InvitedTeam(mainPack) == 2)//已有队伍
            {
                Console.WriteLine("Repeated");
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else if(client.InvitedTeam(mainPack) == 1)
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }else if(client.InvitedTeam(mainPack) == 0)
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack AcceptInviteTeam(Client client, MainPack mainPack)
        {
            if (client.AcceptInviteTeam(mainPack))
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack AcceptedInviteTeam(Client client, MainPack mainPack)
        {
            if (client.AcceptedInviteTeam(mainPack))
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack RefuseInviteTeam(Client client, MainPack mainPack)
        {
            if (client.RefuseInviteTeam(mainPack))
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack RefusedInviteTeam(Client client, MainPack mainPack)
        {
            if (client.RefusedInviteTeam(mainPack))
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack JoinTeam(Client client, MainPack mainPack)
        {
            return null;
        }

        public MainPack UpdateTeam(Client client, MainPack mainPack)
        {
            return null;
        }
    }
}
