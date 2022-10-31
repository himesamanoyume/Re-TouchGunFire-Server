using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocketProtocol;

namespace SocketServer
{
    internal class UserData
    {

        public bool Reigster(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            string userName = mainPack.RegisterPack.UserName;
            string password = mainPack.RegisterPack.Password;

            //string sql = "INSERT INTO `databaseName`. `userdata` (`uid`,`username`, `password`) VALUES (`@uid`, `@username`, `@password`)";
            //MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
            //try
            //{
            //    cmd.ExecuteNonQuery();
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    return false;
            //}

            return true;

        }

        public bool Login(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            string userName = mainPack.LoginPack.UserName;
            string password = mainPack.LoginPack.Password;
            return true;
        }
    }
}
