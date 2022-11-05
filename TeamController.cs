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

        public MainPack LevelTeam()
        {
            return null;
        }
    }
}
