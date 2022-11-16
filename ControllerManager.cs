using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;
using System.Reflection;
using System.Diagnostics;
using SocketServer.Friend;
using SocketServer.User;
using SocketServer.Utils;
using SocketServer.Teammate;

namespace SocketServer
{
    internal class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();

        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            UserController userController = new UserController();
            controllerDict.Add(userController.GetRequestCode, userController);

            TeamController teamController = new TeamController();
            controllerDict.Add(teamController.GetRequestCode, teamController);

            FriendController friendController = new FriendController();
            controllerDict.Add(friendController.GetRequestCode, friendController);
        }

        public void HandleRequest(MainPack mainPack, Client client, bool isUDP = false)
        {
            if(controllerDict.TryGetValue(mainPack.RequestCode, out BaseController controller))
            {
                if (!isUDP)
                {
                    Debug.Log(new StackFrame(true), "接收到UID" + mainPack.Uid + "的" + mainPack.ActionCode + "消息,处理中");
                }
                string methodName = mainPack.ActionCode.ToString();
                MethodInfo method = controller.GetType().GetMethod(methodName);
                if( method == null)
                {
                    Debug.Log(new StackFrame(true), "未找到对应的处理方法,请检查MainPack是否填入ActionCode");
                    return;
                }

                object[] obj;

                if (isUDP)
                {
                    obj = new object[] { client, mainPack };
                    //method.Invoke(controller, obj);
                    object ret = method.Invoke(controller, obj);
                    if (ret != null)
                    {
                        client.UdpSend(ret as MainPack);
                    }
                }
                else
                {
                    obj = new object[] { client, mainPack };
                    object ret = method.Invoke(controller, obj);
                    if (ret != null)
                    {
                        client.TcpSend(ret as MainPack);
                    }
                }
                
            }
            else
            {
                Debug.Log(new StackFrame(true), "没有找到对应的controller");
            }
        }
    }
}
