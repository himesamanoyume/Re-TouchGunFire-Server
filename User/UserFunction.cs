using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SocketProtocol;
using Newtonsoft.Json;
using SocketServer.Teammate;
using Google.Protobuf.Collections;
using SocketServer.Items;

namespace SocketServer.User
{
    internal class UserFunction
    {
        public bool Reigster(MainPack mainPack, MySqlConnection mySqlConnection, ItemController itemController)
        {
            string playerName = mainPack.RegisterPack.PlayerName;
            string account = mainPack.RegisterPack.Account;
            string password = mainPack.RegisterPack.Password;

            try
            {
                string sql = "insert into hime.user_info ( account, password, player_name, equipment_packs ,gun_packs) values ('" + account + "', '" + password + "','" + playerName + "','"+ itemController.InitEquipmentInfo()+"','"+itemController.InitPlayerGunInfo()+"')";
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

        public MainPack Login(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            string account = mainPack.LoginPack.Account;
            string password = mainPack.LoginPack.Password;
            try
            {
                string sql = "select * from hime.user_info where account='" + account + "' and password='" + password + "'";
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                
                if (mySqlDataReader.HasRows)
                {
                    mySqlDataReader.Read();
                    mainPack.Uid = mySqlDataReader.GetInt32(0);
                    
                }
                else
                {
                    mainPack.ReturnCode = ReturnCode.Incorrect;
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

        public MainPack InitPlayerInfo(MainPack mainPack, MySqlConnection mySqlConnection, ItemController itemController)
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
                string equipmentPacksStr = mySqlDataReader.GetString(8);
                if (equipmentPacksStr.Equals("") || equipmentPacksStr.Equals(null))
                {

                }
                else
                {
                    List<EquipmentPack> tempList = JsonConvert.DeserializeObject<List<EquipmentPack>>(equipmentPacksStr);
                    foreach (EquipmentPack item in tempList)
                    {
                        playerInfoPack.EquipmentPacks.Add(item);
                        itemController.UpdatePlayerEquipmentInfo(item);
                    }
                }
                


                //end
                //查武器
                string gunPacksStr = mySqlDataReader.GetString(9);
                if (gunPacksStr.Equals("") || gunPacksStr.Equals(null))
                {

                }
                else
                {
                    List<GunPack> tempList2 = JsonConvert.DeserializeObject<List<GunPack>>(gunPacksStr);
                    
                    //检查是否缺少新的武器json块
                    //if (tempList2.Count < itemController.gunInfos.Count)
                    //{
                        
                    //    int lastGunInfoUid = tempList2.Last().GunId;
                    //    int gunInfosLastUid = itemController.gunInfos.Last().Key;
                    //    for (int i = lastGunInfoUid + 1; i <= gunInfosLastUid; i++)
                    //    {

                    //    }
                    //}
                    foreach (GunPack item in tempList2)
                    {
                        playerInfoPack.GunPacks.Add(item);
                        itemController.UpdatePlayerGunInfo(item);
                    }
                }
                //end
                mySqlDataReader.Close();
                mainPack.PlayerInfoPack = playerInfoPack;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
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

        public MainPack GetPlayerBaseInfo(MainPack mainPack, MySqlConnection mySqlConnection)
        {
            try
            {
                string sql = "select * from hime.user_info where uid = " + mainPack.PlayerInfoPack.Uid;
                MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                mainPack.PlayerInfoPack.Uid = mySqlDataReader.GetInt32(0);
                mainPack.PlayerInfoPack.PlayerName = mySqlDataReader.GetString(3);
                mainPack.PlayerInfoPack.Level = mySqlDataReader.GetInt32(4);
                mySqlDataReader.Close();
                return mainPack;

            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }
    }
}
