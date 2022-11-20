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

        /// <summary>
        /// 有Server参数说明为TCP
        /// </summary>
        /// <param name="server"></param>
        /// <param name="client"></param>
        /// <param name="mainPack"></param>
        /// <returns></returns>
        public MainPack StartAttack(Server server, Client client, MainPack mainPack)
        {
            return null;
        }

        public MainPack UpdatePlayerInfo(Client client, MainPack mainPack)
        {
            if (client.UpdatePlayerInfo(mainPack, client) != null)
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
