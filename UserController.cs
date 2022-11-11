using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                Debug.Log(new StackFrame(true), "Success");
                //Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack Login(Client client, MainPack mainPack)
        {
            if (client.Login(mainPack) != null)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }

            return mainPack;
        }

        public MainPack InitPlayerInfo(Client client, MainPack mainPack)
        {
            if (client.InitPlayerInfo(mainPack) != null)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack GetPlayerBaseInfo(Client client, MainPack mainPack)
        {
            if (client.GetPlayerBaseInfo(mainPack) != null)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }
    }
}
