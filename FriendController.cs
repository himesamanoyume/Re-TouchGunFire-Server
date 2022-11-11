using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;

namespace SocketServer
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

        public MainPack GetFriends(Client client, MainPack mainPack)
        {
            if (client.GetFriends(mainPack) != null)
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

        public MainPack SearchFriend(Client client, MainPack mainPack)
        {
            if (client.SearchFriend(mainPack) != null)
            {
                if (mainPack.ReturnCode == ReturnCode.RepeatedRequest)
                {
                    Debug.Log(new StackFrame(true), "Repeated");
                }
                else
                {
                    Debug.Log(new StackFrame(true), "Success");
                    mainPack.ReturnCode = ReturnCode.Success;
                }  
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack SendRequestFriend(Client client, MainPack mainPack)
        {
            if (client.SendRequestFriend(mainPack) == 1)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if(client.SendRequestFriend(mainPack) == 2)
            {
                Debug.Log(new StackFrame(true), "Repeated");
                mainPack.ReturnCode = ReturnCode.RepeatedRequest;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack AcceptFriendRequest(Client client, MainPack mainPack)
        {
            if (client.AcceptFriendRequest(mainPack) == 1)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }else if (client.AcceptFriendRequest(mainPack) == 2)
            {
                Debug.Log(new StackFrame(true), "NotFound");
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack RefuseFriendRequest(Client client, MainPack mainPack)
        {
            if (client.RefuseFriendRequest(mainPack) == 1)
            {
                Debug.Log(new StackFrame(true), "Success");
                mainPack.ReturnCode = ReturnCode.Success;
            }
            else if (client.RefuseFriendRequest(mainPack) == 2)
            {
                Debug.Log(new StackFrame(true), "NotFound");
                mainPack.ReturnCode = ReturnCode.NotFound;
            }
            else
            {
                Debug.Log(new StackFrame(true), "Failed");
                mainPack.ReturnCode = ReturnCode.Fail;
            }
            return mainPack;
        }

        public MainPack DeleteFriend(Client client, MainPack mainPack)
        {
            if (client.DeleteFriend(mainPack))
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
