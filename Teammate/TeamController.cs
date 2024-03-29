﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer.Teammate
{
    internal class TeamController : BaseController
    {

        public TeamController()
        {
            requestCode = RequestCode.Team;
        }

        public MainPack LeaveTeam(Client client, MainPack mainPack)
        {
            if (client.LeaveTeam(mainPack) != null)
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

        public MainPack AttackInvite(Client client, MainPack mainPack)
        {
            if (client.AttackInvite(mainPack))
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

        public MainPack AttackInvited(Client client, MainPack mainPack)
        {
            int code = client.AttackInvited(mainPack);
            if (code == 1 || code == 2)
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

        public MainPack BreakTeam(Client client, MainPack mainPack)
        {
            mainPack = client.GetTeamFunction.BreakTeam(mainPack, client);
            if (mainPack != null)
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

        public MainPack InviteTeam(Client client, MainPack mainPack)
        {
            int code = client.InviteTeam(mainPack);
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
                Debug.Log(new StackFrame(true), "No Online");
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            return mainPack;
        }

        public MainPack InvitedTeam(Client client, MainPack mainPack)
        {
            int code = client.InvitedTeam(mainPack);
            if (code == 2)//已有队伍
            {
                Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else if(code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }else if(code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack JoinTeamRequest(Client client, MainPack mainPack)
        {
            int code = client.JoinTeamRequest(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else if (code == 2)//不在线
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else if (code == 3)//不正常
            {
                
            }
            return mainPack;
        }

        public MainPack PlayerJoinTeam(Client client, MainPack mainPack)
        {
            int code = client.JoinTeamRequest(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 0)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else if (code == 2)//不在线
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else if (code == 3)//不正常
            {

            }
            return mainPack;
        }

        public MainPack RefuseJoinTeam(Client client, MainPack mainPack)
        {
            if (client.RefuseJoinTeam(mainPack))
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

        public MainPack RefusedJoinTeam(Client client, MainPack mainPack)
        {
            if (client.RefusedJoinTeam(mainPack))
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

        public MainPack AcceptJoinTeam(Client client, MainPack mainPack)
        {
            if (client.AcceptJoinTeam(mainPack))
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

        public MainPack AcceptedJoinTeam(Client client, MainPack mainPack)
        {
            if (client.AcceptedJoinTeam(mainPack))
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

        public MainPack AcceptInviteTeam(Client client, MainPack mainPack)
        {
            if (client.AcceptInviteTeam(mainPack))
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

        public MainPack AcceptedInviteTeam(Client client, MainPack mainPack)
        {

            if (client.AcceptedInviteTeam(mainPack))
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

        public MainPack RefuseInviteTeam(Client client, MainPack mainPack)
        {
            if (client.RefuseInviteTeam(mainPack))
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

        public MainPack RefusedInviteTeam(Client client, MainPack mainPack)
        {
            if (client.RefusedInviteTeam(mainPack))
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

        public MainPack GetTeammates(Client client, MainPack mainPack)
        {
            mainPack = client.GetTeamFunction.GetTeammates(mainPack, client);
            if (mainPack == null)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
            }
            else if (mainPack.ReturnCode == ReturnCode.NotFound)
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            return mainPack;
        }

        public MainPack KickPlayer(Client client, MainPack mainPack)
        {
            if (client.KickPlayer(mainPack))
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

        public MainPack TeamMasterAttackNotify(Client client, MainPack mainPack)
        {
            if (client.TeamMasterAttackNotify(mainPack))
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
