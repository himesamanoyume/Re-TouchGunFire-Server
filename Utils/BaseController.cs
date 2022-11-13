using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using Google.Protobuf;

namespace SocketServer
{
    abstract  class BaseController
    {
        protected RequestCode requestCode = RequestCode.RequestNone;
        public RequestCode GetRequestCode
        {
            get { return requestCode; }
        }
    }
}
