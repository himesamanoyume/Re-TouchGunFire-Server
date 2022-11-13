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
            //if ()
            //{

            //}
            return null;
        }

        public MainPack InviteTeam(Client client, MainPack mainPack)
        {
            if (client.InviteTeam(mainPack) == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if(client.InviteTeam(mainPack) == 0)
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
            
            if (client.InvitedTeam(mainPack) == 2)//已有队伍
            {
                Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else if(client.InvitedTeam(mainPack) == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }else if(client.InvitedTeam(mainPack) == 0)
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
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            else if (mainPack.ReturnCode == ReturnCode.NotFound)
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            return mainPack;
        }

        public MainPack JoinTeam(Client client, MainPack mainPack)
        {
            return null;
        }

        public MainPack UpdateTeam(Client client, MainPack mainPack)
        {
            return null;
        }
    }
}