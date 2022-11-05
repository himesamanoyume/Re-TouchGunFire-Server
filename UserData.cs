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
            string playerName = mainPack.RegisterPack.PlayerName;
            string account = mainPack.RegisterPack.Account;
            string password = mainPack.RegisterPack.Password;

            try
            {
                string sql = "INSERT INTO `hime`.`user_info` ( `account`, `password`, `player_name`) VALUES ('" + account + "', '" + password + "','" + playerName + "')";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return false;

        }

        public bool Login(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            string account = mainPack.LoginPack.Account;
            string password = mainPack.LoginPack.Password;
            
            try
            {
                string sql = "SELECT * FROM hime.user_info where account='" + account + "' and password='" + password + "'";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return false;
        }
    }
}
