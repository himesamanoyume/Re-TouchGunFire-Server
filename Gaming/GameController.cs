using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;
using SocketServer.Utils;

namespace SocketServer.Gaming
{
    internal class GameController : BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Gaming;
        }

        //public MainPack StartAttack(Client client, MainPack mainPack)
        //{
        //    if (client.StartAttack(mainPack))
        //    {
        //        Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
        //        mainPack.ReturnCode = ReturnCode.Success;
        //    }
        //    else
        //    {
        //        Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
        //        mainPack.ReturnCode = ReturnCode.Fail;
        //    }
        //    return mainPack;
        //}

        public MainPack UpdatePlayerInfo(Client client, MainPack mainPack)
        {
            if (client.UpdatePlayerInfo(mainPack) != null)
            {
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack Regeneration(Client client, MainPack mainPack)
        {
            if (client.Regeneration(mainPack))
            {
                //Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
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
