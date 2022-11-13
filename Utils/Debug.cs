using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    internal static class Debug
    {
        /// <summary>
        /// 展示Debug信息
        /// </summary>
        /// <param name="n"></param>
        public static void Log(StackFrame sf ,string n)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[{0}:", sf.GetFileName().Split('\\')[sf.GetFileName().Split('\\').Length - 1]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0}", sf.GetMethod().Name);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(":Line.{0}]\n", sf.GetFileLineNumber());
            Console.Write("[{0}] ", DateTime.Now.ToLongTimeString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(n);
        }
    }
}
