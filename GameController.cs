﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer
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

        /// <summary>
        /// 没有的为UDP
        /// </summary>
        /// <param name="client"></param>
        /// <param name="mainPack"></param>
        /// <returns></returns>
        public MainPack UpdatePlayerInfo(Client client, MainPack mainPack)
        {
            return null;
        }
    }
}
