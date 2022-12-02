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
                string equipmentPacksStr = mySqlDataReader.GetString(8);
                string gunPacksStr = mySqlDataReader.GetString(9);
                //查装备
                mySqlDataReader.Close();
                if (equipmentPacksStr.Equals("") || equipmentPacksStr.Equals(null))
                {

                }
                else
                {
                    List<EquipmentPack> tempList = JsonConvert.DeserializeObject<List<EquipmentPack>>(equipmentPacksStr);

                    //检查是否缺少新的武器json块
                    if (tempList.Count < itemController.equipmentInfos.Count)
                    {

                        int lastEquipmentInfoUid = tempList.Last().ItemId;
                        int equipmentInfosLastUid = itemController.equipmentInfos.Last().Key;
                        for (int i = lastEquipmentInfoUid + 1; i <= equipmentInfosLastUid; i++)
                        {
                            string tempJson = JsonConvert.SerializeObject(itemController.equipmentInfos[i]);
                            EquipmentPack equipmentPack = JsonConvert.DeserializeObject<EquipmentPack>(tempJson);
                            tempList.Add(equipmentPack);
                        }
                    }

                    foreach (EquipmentPack item in tempList)
                    {
                        playerInfoPack.EquipmentPacks.Add(item);
                        itemController.UpdatePlayerEquipmentInfo(item, true);
                    }

                    string updateEquipmentJson = JsonConvert.SerializeObject(tempList);
                    sql = "update hime.user_info set equipment_packs = '" + updateEquipmentJson + "' where uid = " + mainPack.Uid;
                    cmd = new MySqlCommand(sql, mySqlConnection);
                    cmd.ExecuteNonQuery();
                    //end
                }
                //end
                //查武器

                if (gunPacksStr.Equals("") || gunPacksStr.Equals(null))
                {

                }
                else
                {
                    List<GunPack> tempList2 = JsonConvert.DeserializeObject<List<GunPack>>(gunPacksStr);

                    //检查是否缺少新的武器json块
                    if (tempList2.Count < itemController.gunInfos.Count)
                    {

                        int lastGunInfoUid = tempList2.Last().ItemId;
                        int gunInfosLastUid = itemController.gunInfos.Last().Key;
                        for (int i = lastGunInfoUid + 1; i <= gunInfosLastUid; i++)
                        {
                            string tempJson = JsonConvert.SerializeObject(itemController.gunInfos[i]);
                            GunPack gunPack = JsonConvert.DeserializeObject<GunPack>(tempJson);
                            tempList2.Add(gunPack);
                        }
                    }
                    foreach (GunPack item in tempList2)
                    {
                        playerInfoPack.GunPacks.Add(item);
                        itemController.UpdatePlayerGunInfo(item, true);
                    }
                    string updateGunJson = JsonConvert.SerializeObject(tempList2);
                    sql = "update hime.user_info set gun_packs = '" + updateGunJson + "' where uid = " + mainPack.Uid;
                    cmd = new MySqlCommand(sql, mySqlConnection);
                    cmd.ExecuteNonQuery();
                    //end
                }
                //end
                //mySqlDataReader.Close();
                mainPack.PlayerInfoPack = playerInfoPack;
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public int EquipItem(MainPack mainPack, Client client)
        {
            try
            {
                if (mainPack.Uid == client.PlayerInfo.Uid && mainPack.EquipItemPack.Uid == client.PlayerInfo.Uid)
                {
                    if (client.ItemController.itemsDict.TryGetValue(mainPack.EquipItemPack.ItemId, out ItemInfo itemInfo))
                    {
                        client.ItemController.SetItemEquip(itemInfo.ItemId, itemInfo.ItemType);
                        client.GetItemInfo();
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        public bool GetItemInfo(Client client)
        {
            try
            {
                MainPack mainPack = new MainPack();
                mainPack.Uid = client.PlayerInfo.Uid;
                mainPack.ActionCode = ActionCode.GetItemInfo;
                PlayerInfoPack playerInfoPack = new PlayerInfoPack();

                List<GunInfo> tempGunInfos = new List<GunInfo>();
                foreach (GunInfo item in client.ItemController.gunInfos.Values)
                {
                    tempGunInfos.Add(item);
                }
                List<EquipmentInfo> tempEquipmentInfos = new List<EquipmentInfo>();
                foreach (EquipmentInfo item in client.ItemController.equipmentInfos.Values)
                {
                    tempEquipmentInfos.Add(item);
                }
                string tempGunJson = JsonConvert.SerializeObject(tempGunInfos);
                string tempEquipmentJson = JsonConvert.SerializeObject(tempEquipmentInfos);
                List<GunPack> tempGunPacks = JsonConvert.DeserializeObject<List<GunPack>>(tempGunJson);
                List<EquipmentPack> tempEquipmentPacks = JsonConvert.DeserializeObject<List<EquipmentPack>>(tempEquipmentJson);
                foreach (GunPack item in tempGunPacks)
                {
                    playerInfoPack.GunPacks.Add(item);
                }
                foreach (EquipmentPack item in tempEquipmentPacks)
                {
                    playerInfoPack.EquipmentPacks.Add(item);
                }
                mainPack.PlayerInfoPack = playerInfoPack;
                mainPack.ReturnCode = ReturnCode.Success;
                client.TcpSend(mainPack);
                return true;
            }
            catch (Exception e)
            {

                Debug.Log(new StackFrame(true), e.Message);
                MainPack mainPack = new MainPack();
                mainPack.Uid = client.PlayerInfo.Uid;
                mainPack.ActionCode = ActionCode.GetItemInfo;
                mainPack.ReturnCode = ReturnCode.Fail;
                client.TcpSend(mainPack);
                return false;
            }
        }

        public int Shopping(MainPack mainPack, Client client, MySqlConnection mySqlConnection)
        {
            try
            {
                if (mainPack.Uid == mainPack.ShoppingPack.Uid)
                {
                    if (client.ItemController.gunInfos.TryGetValue(mainPack.ShoppingPack.ItemId, out GunInfo gunInfo))
                    {
                        int code = ShoppingFunc(mainPack, gunInfo, client);
                        if (code == 1)
                        {
                            ShoppingSuccess(client, mySqlConnection);
                        }
                        return code;
                    }
                    else if (client.ItemController.equipmentInfos.TryGetValue(mainPack.ShoppingPack.ItemId, out EquipmentInfo equipmentInfo))
                    {
                        int code = ShoppingFunc(mainPack, equipmentInfo, client);
                        if (code == 1)
                        {
                            ShoppingSuccess(client, mySqlConnection);
                        }
                        return code;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return 0;
            }
        }

        int ShoppingFunc(MainPack mainPack, ItemInfo itemInfo, Client client)
        {
            if (itemInfo.Block)
            {
                if (mainPack.ShoppingPack.IsDiamond == true)
                {
                    if (itemInfo.DiamondPrice <= client.PlayerInfo.Diamond && itemInfo.DiamondPrice == mainPack.ShoppingPack.DiamondPrice)
                    {
                        client.PlayerInfo.Diamond -= itemInfo.DiamondPrice;
                        itemInfo.Block = false;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (itemInfo.Price <= client.PlayerInfo.Coin && itemInfo.Price == mainPack.ShoppingPack.Price)
                    {
                        client.PlayerInfo.Coin -= itemInfo.Price;
                        itemInfo.Block = false;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return 2;//repeated
            }
        }

        void ShoppingSuccess(Client client, MySqlConnection mySqlConnection)
        {
            string sql = "update hime.user_info set diamond = " + client.PlayerInfo.Diamond + " , coin = " + client.PlayerInfo.Coin + " where uid = " + client.PlayerInfo.Uid;
            MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
            cmd.ExecuteNonQuery();
            List<GunInfo> tempGunInfos = new List<GunInfo>();
            foreach (GunInfo item in client.ItemController.gunInfos.Values)
            {
                tempGunInfos.Add(item);
            }
            List<EquipmentInfo> tempEquipmentInfos = new List<EquipmentInfo>();
            foreach (EquipmentInfo item in client.ItemController.equipmentInfos.Values)
            {
                tempEquipmentInfos.Add(item);
            }
            string tempGunJson = JsonConvert.SerializeObject(tempGunInfos);
            string tempEquipmentJson = JsonConvert.SerializeObject(tempEquipmentInfos);
            sql = "update hime.user_info set gun_packs = '" + tempGunJson + "' , equipment_packs = '" + tempEquipmentJson + "' where uid = " + client.PlayerInfo.Uid;
            cmd = new MySqlCommand(sql, mySqlConnection);
            cmd.ExecuteNonQuery();
            client.GetItemInfo();
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
