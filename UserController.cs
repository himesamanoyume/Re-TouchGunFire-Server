using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer
{
    internal class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        
        public MainPack Register(Client client, MainPack mainPack)
        {
            if(client.Register(mainPack))
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

        public MainPack Login(Client client, MainPack mainPack)
        {
            if (client.Login(mainPack) != null)
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

        public MainPack InitPlayerInfo(Client client, MainPack mainPack)
        {
            if (client.InitPlayerInfo(mainPack) != null)
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
    }
}
