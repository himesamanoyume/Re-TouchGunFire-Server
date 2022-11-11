using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocketProtocol;

namespace SocketServer
{
    internal class FriendFunction
    {
        public MainPack GetFriendRequest(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            try
            {
                string sql = "select * from hime.user_friends where player2_uid =" + mainPack.Uid + " and is_friend = 0";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
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
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public MainPack GetFriends(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            try
            {
                string sql = "select * from hime.user_friends where player1_uid = " + mainPack.Uid + " and is_friend = 1";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                //player1_uid永远是好友 player2_uid永远是自己
                foreach (var item in mySqlDataReader)
                {
                    mySqlDataReader.Read();
                    FriendsPack friendsPack = new FriendsPack();
                    friendsPack.Player2Uid = mySqlDataReader.GetInt32(1);
                    friendsPack.Player1Uid = mySqlDataReader.GetInt32(2);
                    mainPack.FriendsPack.Add(friendsPack);
                }

                sql = "select * from hime.user_friends where player2_uid = " + mainPack.Uid + " and is_friend = 1";
                cmd = new MySqlCommand(sql, mySqlConnection);
                mySqlDataReader.Close();
                mySqlDataReader = cmd.ExecuteReader();
                foreach (var item in mySqlDataReader)
                {
                    mySqlDataReader.Read();
                    FriendsPack friendsPack = new FriendsPack();
                    friendsPack.Player1Uid = mySqlDataReader.GetInt32(1);
                    friendsPack.Player2Uid = mySqlDataReader.GetInt32(2);
                    mainPack.FriendsPack.Add(friendsPack);
                }
                mySqlDataReader.Close();
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
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
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public int SendRequestFriend(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int senderUid = mainPack.Uid;
            int targetUid = mainPack.SendRequestFriendPack.TargetPlayerUid;
            try
            {
                string sql = "select * from hime.user_friends where player1_uid =" + senderUid + " and player2_uid =" + targetUid + " or player1_uid = " + targetUid + " and player2_uid = " + senderUid;
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    mySqlDataReader.Close();
                    return 2;
                }
                mySqlDataReader.Close();
                sql = "insert into user_friends (player1_uid, player2_uid) values (" + senderUid + ", " + targetUid + ")";
                cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        public int AcceptFriendRequest(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int senderUid = mainPack.PlayerInfoPack.Uid;
            int accepterUid = mainPack.Uid;
            try
            {
                string sql = "select * from hime.user_friends where player1_uid =" + senderUid + " and player2_uid = " + accepterUid + " and is_friend = 0";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    mySqlDataReader.Close();
                    sql = "update hime.user_friends set is_friend = 1 where player1_uid = " + senderUid + " and player2_uid = " + accepterUid;
                    cmd = new MySqlCommand(sql, mySqlConnection);
                    cmd.ExecuteNonQuery();
                    return 1;

                }
                else
                {
                    mySqlDataReader.Close();
                    return 2;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        public int RefuseFriendRequest(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int senderUid = mainPack.PlayerInfoPack.Uid;
            int accepterUid = mainPack.Uid;
            try
            {
                string sql = "select * from hime.user_friends where player1_uid =" + senderUid + " and player2_uid = " + accepterUid + " and is_friend = 0";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    mySqlDataReader.Close();
                    sql = "delete from hime.user_friends where player1_uid = " + senderUid + " and player2_uid = " + accepterUid;
                    cmd = new MySqlCommand(sql, mySqlConnection);
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                else
                {
                    mySqlDataReader.Close();
                    return 2;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        public bool DeleteFriend(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            int senderUid = mainPack.Uid;
            int targetUid = mainPack.PlayerInfoPack.Uid;
            try
            {
                string sql = "delete from hime.user_friends where player1_uid = " + senderUid + " and player2_uid = " + targetUid + " and is_friend = 1 or player1_uid = "+ targetUid +" and player2_uid = " + senderUid + " and is_friend = 1";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }
    }
}
