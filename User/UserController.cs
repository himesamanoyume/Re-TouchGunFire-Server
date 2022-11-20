using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer.User
{
    internal class UserController : BaseController
    {
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public MainPack Register(Client client, MainPack mainPack)
        {
            if (client.Register(mainPack))
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack Login(Client client, MainPack mainPack)
        {
            mainPack = client.Login(mainPack);
            if (mainPack.ReturnCode == ReturnCode.Incorrect)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Incorrect.ToString());
            }
            else if(mainPack == null)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ActionCode = ActionCode.Login;
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
                //=========temp========
                client.buff += client.PlayerInfo.TempGunBuff1;
                client.buff += client.PlayerInfo.TempGunBuff2;
                client.ChangePlayerInfo();
                //end
            }

            return mainPack;
        }

        public MainPack InitPlayerInfo(Client client, MainPack mainPack)
        {
            if (client.InitPlayerInfo(mainPack) != null)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack GetPlayerBaseInfo(Client client, MainPack mainPack)
        {
            if (client.GetPlayerBaseInfo(mainPack) != null)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }
    }
}
