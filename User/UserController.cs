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
                //client.buff += client.PlayerInfo.TempGunBuff1;
                //client.buff += client.PlayerInfo.TempGunBuff2;
                //client.InitDefaultPlayerInfo();
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

        public MainPack RefreshItemSubProp(Client client, MainPack mainPack)
        {
            if (client.RefreshItemSubProp(mainPack))
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

        public MainPack RefreshGunCoreProp(Client client, MainPack mainPack)
        {
            if (client.RefreshGunCoreProp(mainPack))
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

        public MainPack UnlockItemSubProp(Client client, MainPack mainPack)
        {
            int code = client.UnlockItemSubProp(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }else if (code == 2)
            {
                Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }else if (code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }else if(code == 4)
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Incorrect.ToString());
                mainPack.ReturnCode = ReturnCode.Incorrect;
            }
            return mainPack;
        }

        public MainPack EquipItem(Client client, MainPack mainPack)
        {
            int code = client.EquipItem(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if(code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            return mainPack;
        }

        public MainPack Shopping(Client client, MainPack mainPack)
        {
            int code = client.Shopping(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 2)
            {
                Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }else if(code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public void GetItemInfo(Client client)
        {
            if (client.GetItemInfo())
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
            }
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
