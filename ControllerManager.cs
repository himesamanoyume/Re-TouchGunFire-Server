using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketProtocol;
using System.Reflection;


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
        }

        public void HandleRequest(MainPack mainPack, Client client)
        {
            if(controllerDict.TryGetValue(mainPack.RequestCode, out BaseController controller))
            {
                Console.WriteLine("处理消息中");
                string methodName = mainPack.ActionCode.ToString();
                Console.WriteLine(methodName);
                MethodInfo method = controller.GetType().GetMethod(methodName);
                if( method == null)
                {
                    Console.WriteLine("未找到对应的处理方法");
                    return;
                }
                object[] obj = new object[] {server, client, mainPack };
                object ret = method.Invoke(controller, obj);
                if(ret != null)
                {
                    client.Send(ret as MainPack);
                }
            }
            else
            {
                Console.WriteLine("没有找到对应的controller");
            }
        }
    }
}
