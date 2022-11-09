using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer
{
    internal class FriendController : BaseController
    {
        public FriendController()
        {
            requestCode = RequestCode.Friend;
        }

        public MainPack GetFriendRequest(Client client, MainPack mainPack)
        {
            if (client.GetFriendRequest(mainPack) != null)
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

        public MainPack GetFriends(Client client, MainPack mainPack)
        {
            if (client.GetFriends(mainPack) != null)
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

        public MainPack SearchFriend(Client client, MainPack mainPack)
        {
            if (client.SearchFriend(mainPack) != null)
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

        public MainPack SendRequestFriend(Client client, MainPack mainPack)
        {
            if (client.SendRequestFriend(mainPack) == 1)
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if(client.SendRequestFriend(mainPack) == 2)
            {
                Console.WriteLine("Repeated");
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack AcceptFriendRequest(Client client, MainPack mainPack)
        {
            if (client.AcceptFriendRequest(mainPack) == 1)
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }else if (client.AcceptFriendRequest(mainPack) == 2)
            {
                Console.WriteLine("NotFound");
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack RefuseFriendRequest(Client client, MainPack mainPack)
        {
            if (client.RefuseFriendRequest(mainPack) == 1)
            {
                Console.WriteLine("Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (client.RefuseFriendRequest(mainPack) == 2)
            {
                Console.WriteLine("NotFound");
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Console.WriteLine("Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }
    }
}
