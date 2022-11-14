using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;
using SocketServer.Utils;

namespace SocketServer.Friend
{
    internal class FriendController : BaseController
    {
        public FriendController()
        {
            requestCode = RequestCode.Friend;
        }

        public MainPack GetFriendRequest(Client client, MainPack mainPack)
        {
            if (client.GetFriendRequest(mainPack) != null)
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

        public MainPack GetFriends(Client client, MainPack mainPack)
        {
            if (client.GetFriends(mainPack) != null)
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

        public MainPack SearchFriend(Client client, MainPack mainPack)
        {
            if (client.SearchFriend(mainPack) != null)
            {
                if (mainPack.ReturnCode == ReturnCode.RepeatedRequest)
                {
                    Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                }
                else
                {
                    Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                    mainPack.ReturnCode = ReturnCode.Success;
                }
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack SendRequestFriend(Client client, MainPack mainPack)
        {
            int code = client.SendRequestFriend(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 2)
            {
                Debug.Log(new StackFrame(true), ReturnCode.RepeatedRequest.ToString());
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack AcceptFriendRequest(Client client, MainPack mainPack)
        {
            int code = client.AcceptFriendRequest(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 2)
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack RefuseFriendRequest(Client client, MainPack mainPack)
        {
            int code = client.RefuseFriendRequest(mainPack);
            if (code == 1)
            {
                Debug.Log(new StackFrame(true), ReturnCode.Success.ToString());
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (code == 2)
            {
                Debug.Log(new StackFrame(true), ReturnCode.NotFound.ToString());
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), ReturnCode.Fail.ToString());
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack DeleteFriend(Client client, MainPack mainPack)
        {
            if (client.DeleteFriend(mainPack))
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
