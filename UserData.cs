using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
                string sql = "insert into hime.user_info ( account, password, player_name) values ('" + account + "', '" + password + "','" + playerName + "')";
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

        public MainPack Login(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            string account = mainPack.LoginPack.Account;
            string password = mainPack.LoginPack.Password;
            
            try
            {
                string sql = "select * from hime.user_info where account='" + account + "' and password='" + password + "'";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                mainPack.Uid = mySqlDataReader.GetInt32(0);
                mySqlDataReader.Close();
                return mainPack;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return  null;
        }

        public MainPack InitPlayerInfo(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            //登陆游戏时一次性查询完全部信息
            try
            {
                string sql = "select * from hime.user_info where uid = " + mainPack.Uid;
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                PlayerInfoPack playerInfoPack = new PlayerInfoPack();
                playerInfoPack.Uid = mySqlDataReader.GetInt32(0);
                playerInfoPack.PlayerName = mySqlDataReader.GetString(3);
                playerInfoPack.Level = mySqlDataReader.GetInt32(4);
                playerInfoPack.CurrentExp = mySqlDataReader.GetFloat(5);
                playerInfoPack.Diamond = mySqlDataReader.GetInt64(6);
                playerInfoPack.Coin = mySqlDataReader.GetInt64(7);
                //查装备

                //查武器

                //end
                mySqlDataReader.Close();
                mainPack.PlayerInfoPack = playerInfoPack;
                return mainPack;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }

        //只更新玩家信息
        public MainPack UpdatePlayerInfo(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        //只查询玩家信息
        public MainPack GetPlayerInfo(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        //只更新装备信息
        public MainPack UpdatePlayerEquipment(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        //只查询装备信息
        public MainPack GetPlayerEquipment(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        //只更新武器信息
        public MainPack UpdatePlayerGun(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        //只查询武器信息
        public MainPack GetPlayerGun(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            return null;
        }

        public MainPack GetFriendRequest(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            try
            {
                string sql = "select * from hime.user_friends where player2_uid =" + mainPack.Uid + " and is_friend = 0";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Console.WriteLine(mySqlDataReader.GetInt32(1) + "_" + mySqlDataReader.GetInt32(2));
                    FriendsPack friendsPack = new FriendsPack();
                    friendsPack.Player1Uid = mySqlDataReader.GetInt32(1);
                    friendsPack.Player2Uid = mySqlDataReader.GetInt32(2);
                    friendsPack.IsFriend = mySqlDataReader.GetInt32(3);
                    mainPack.FriendsPack.Add(friendsPack);
                }
                mySqlDataReader.Close();
                return mainPack;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }

        public MainPack GetFriends(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            try
            {
                string sql = "select * from hime.user_friends where player1_uid = "+ mainPack.Uid +" or player2_uid =" + mainPack.Uid + " and is_friend = 1";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                foreach (var item in mySqlDataReader)
                {
                    mySqlDataReader.Read();
                    Console.WriteLine(mySqlDataReader.GetInt32(1) + "_" + mySqlDataReader.GetInt32(2));
                    FriendsPack friendsPack = new FriendsPack();
                }

                mySqlDataReader.Close();
                return mainPack;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }

        public MainPack SearchFriend(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int targetPlayerUid = mainPack.PlayerInfoPack.Uid;
            try
            {
                string sql = "select * from hime.user_info where uid =" + targetPlayerUid;
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                PlayerInfoPack playerInfoPack = new PlayerInfoPack();
                if (targetPlayerUid != mySqlDataReader.GetInt32(0))
                {
                    return null;
                }
                playerInfoPack.PlayerName = mySqlDataReader.GetString(3);
                playerInfoPack.Level = mySqlDataReader.GetInt32(4);
                mainPack.PlayerInfoPack = playerInfoPack;
                mySqlDataReader.Close();
                return mainPack;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return null;
        }

        public int SendRequestFriend(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int senderUid = mainPack.Uid;
            int targetUid = mainPack.SendRequestFriendPack.TargetPlayerUid;
            try
            {
                string sql = "select * from hime.user_friends where player1_uid =" + senderUid + " and player2_uid =" + targetUid + " or player1_uid = " + targetUid +" and player2_uid = " + senderUid;
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    mySqlDataReader.Close();
                    return 2;
                }
                mySqlDataReader.Close();
                sql = "insert into user_friends (player1_uid, player2_uid) values (" + senderUid + ", " + targetUid +")";
                cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return 0;
        }
    }

    
}
