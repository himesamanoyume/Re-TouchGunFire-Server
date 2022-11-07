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

            return mainPack;
        }

        public MainPack GetFriends(Client client, MainPack mainPack)
        {

            return mainPack;
        }

        public MainPack SearchFriend(Client client, MainPack mainPack)
        {

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
    }
}
