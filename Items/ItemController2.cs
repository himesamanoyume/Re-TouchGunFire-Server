using MySql.Data.MySqlClient;

using Newtonsoft.Json;

using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public partial class ItemController
    {
        //用于分类存储玩家武器和装备信息的字典
        public Dictionary<int, GunInfo> gunInfos = new Dictionary<int, GunInfo>();
        public Dictionary<int, EquipmentInfo> equipmentInfos = new Dictionary<int, EquipmentInfo>();
        //end
        //用于注册用户登录时的初始化用列表
        List<GunInfo> gunInfoList = new List<GunInfo>();
        List<EquipmentInfo> equipmentInfoList = new List<EquipmentInfo>();
        //end
        Dictionary<int, Func<ItemInfo>> initItemInfoDict = new Dictionary<int, Func<ItemInfo>>();

        

        public ItemController(PlayerInfo playerInfo)
        {
            #region initGun
            initItemInfoDict.Add((int)EGunUid.AK47, () =>
            {
                //MainGunInfo<EGunUid> AK47 = new MainGunInfo<EGunUid>(playerInfo);
                GunInfo AK47 = new GunInfo(playerInfo);
                AK47.ItemId = (int)EGunUid.AK47;
                AK47.GunName = EGunName.AK47.ToString();
                AK47.ItemType = EGunType.AR.ToString();
                AK47.CoreProp = EGunCoreProp.自动步枪伤害加成.ToString();
                AK47.CorePropType = EGunCoreProp.自动步枪伤害加成;
                AK47.CorePropValue = 0.03f;
                AK47.BaseDMG = 100f;
                AK47.FiringRate = 600f;
                AK47.Magazine = 30;
                AK47.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(AK47);
                AK47.Price = 1000;
                AK47.Block = false;
                AK47.Use = true;
                return AK47;
            });

            initItemInfoDict.Add((int)EGunUid.SL7, () =>
            {
                GunInfo SL7 = new GunInfo(playerInfo);
                SL7.ItemId = (int)EGunUid.SL7;
                SL7.GunName = EGunName.SL7.ToString();
                SL7.ItemType = EGunType.DMR.ToString();
                SL7.CoreProp = EGunCoreProp.射手步枪伤害加成.ToString();
                SL7.CorePropType = EGunCoreProp.射手步枪伤害加成;
                SL7.CorePropValue = 0.03f;
                SL7.BaseDMG = 280f;
                SL7.FiringRate = 50f;
                SL7.Magazine = 5;
                SL7.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(SL7);
                SL7.Price = 8050;
                return SL7;
            });

            initItemInfoDict.Add((int)EGunUid.MP5, () =>
            {
                GunInfo MP5 = new GunInfo(playerInfo);
                MP5.ItemId = (int)EGunUid.MP5;
                MP5.GunName = EGunName.MP5.ToString();
                MP5.ItemType = EGunType.SMG.ToString();
                MP5.CoreProp = EGunCoreProp.微型冲锋枪伤害加成.ToString();
                MP5.CorePropType = EGunCoreProp.微型冲锋枪伤害加成;
                MP5.CorePropValue = 0.03f;
                MP5.BaseDMG = 70f;
                MP5.FiringRate = 800f;
                MP5.Magazine = 30;
                MP5.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(MP5);
                MP5.Price = 4500;
                return MP5;
            });

            initItemInfoDict.Add((int)EGunUid.M700, () =>
            {
                GunInfo M700 = new GunInfo(playerInfo);
                M700.ItemId = (int)EGunUid.M700;
                M700.GunName = EGunName.M700.ToString();
                M700.ItemType = EGunType.SG.ToString();
                M700.CoreProp = EGunCoreProp.狙击步枪伤害加成.ToString();
                M700.CorePropType = EGunCoreProp.狙击步枪伤害加成;
                M700.CorePropValue = 0.03f;
                M700.BaseDMG = 300f;
                M700.FiringRate = 30f;
                M700.Magazine = 7;
                M700.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M700);
                M700.Price = 10500;
                return M700;
            });

            initItemInfoDict.Add((int)EGunUid.M60, () =>
            {
                GunInfo M60 = new GunInfo(playerInfo);
                M60.ItemId = (int)EGunUid.M60;
                M60.GunName = EGunName.M60.ToString();
                M60.ItemType = EGunType.MG.ToString();
                M60.CoreProp = EGunCoreProp.轻机枪伤害加成.ToString();
                M60.CorePropType = EGunCoreProp.轻机枪伤害加成;
                M60.CorePropValue = 0.03f;
                M60.BaseDMG = 130f;
                M60.FiringRate = 500f;
                M60.Magazine = 100;
                M60.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M60);
                M60.Price = 3200;
                return M60;
            });

            initItemInfoDict.Add((int)EGunUid.M1911, () =>
            {
                GunInfo M1911 = new GunInfo(playerInfo);
                M1911.ItemId = (int)EGunUid.M1911;
                M1911.GunName = EGunName.M1911.ToString();
                M1911.ItemType = EGunType.HG.ToString();
                M1911.CoreProp = EGunCoreProp.手枪伤害加成.ToString();
                M1911.CorePropType = EGunCoreProp.手枪伤害加成;
                M1911.CorePropValue = 0.03f;
                M1911.BaseDMG = 90f;
                M1911.FiringRate = 100f;
                M1911.Magazine = 7;
                M1911.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M1911);
                M1911.Price = 500;
                M1911.Block = false;
                M1911.Use = true;
                return M1911;
            });

            initItemInfoDict.Add((int)EGunUid.M4A1, () =>
            {
                GunInfo M4A1 = new GunInfo(playerInfo);
                M4A1.ItemId = (int)EGunUid.M4A1;
                M4A1.GunName = EGunName.M4A1.ToString();
                M4A1.ItemType = EGunType.AR.ToString();
                M4A1.CoreProp = EGunCoreProp.自动步枪伤害加成.ToString();
                M4A1.CorePropType = EGunCoreProp.自动步枪伤害加成;
                M4A1.CorePropValue = 0.03f;
                M4A1.BaseDMG = 85f;
                M4A1.FiringRate = 780f;
                M4A1.Magazine = 30;
                M4A1.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M4A1);
                M4A1.Price = 7700;
                M4A1.Block = true;
                M4A1.Use = false;
                return M4A1;
            });
            #endregion
            #region initEquipment
            initItemInfoDict.Add((int)EEquipmentUid.DefaultArmor, () =>
            {
                EquipmentInfo DefaultArmor = new EquipmentInfo(playerInfo);
                DefaultArmor.ItemType = EEquipmentType.Armor.ToString();
                DefaultArmor.ItemId = (int)EEquipmentUid.DefaultArmor;
                DefaultArmor.EquipmentName = EEquipmentName.默认护甲.ToString();
                DefaultArmor.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultArmor.Use = true;
                DefaultArmor.Block = false;
                return DefaultArmor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHead, () =>
            {
                EquipmentInfo DefaultHead = new EquipmentInfo(playerInfo);
                DefaultHead.ItemId = (int)EEquipmentUid.DefaultHead;
                DefaultHead.ItemType = EEquipmentType.Head.ToString();
                DefaultHead.EquipmentName = EEquipmentName.默认头盔.ToString();
                DefaultHead.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultHead.Use = true;
                DefaultHead.Block = false;
                return DefaultHead;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHand, () =>
            {
                EquipmentInfo DefaultHand = new EquipmentInfo(playerInfo);
                DefaultHand.ItemId = (int)EEquipmentUid.DefaultHand;
                DefaultHand.ItemType = EEquipmentType.Hand.ToString();
                DefaultHand.EquipmentName = EEquipmentName.默认手套.ToString();
                DefaultHand.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultHand.Use = true;
                DefaultHand.Block = false;
                return DefaultHand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultKnee, () =>
            {
                EquipmentInfo DefaultKnee = new EquipmentInfo(playerInfo);
                DefaultKnee.ItemId = (int)EEquipmentUid.DefaultKnee;
                DefaultKnee.ItemType = EEquipmentType.Knee.ToString();
                DefaultKnee.EquipmentName = EEquipmentName.默认护膝.ToString();
                DefaultKnee.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultKnee.Use = true;
                DefaultKnee.Block = false;
                return DefaultKnee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultLeg, () =>
            {
                EquipmentInfo DefaultLeg = new EquipmentInfo(playerInfo);
                DefaultLeg.ItemId = (int)EEquipmentUid.DefaultLeg;
                DefaultLeg.ItemType = EEquipmentType.Leg.ToString();
                DefaultLeg.EquipmentName = EEquipmentName.默认护腿.ToString();
                DefaultLeg.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultLeg.Use = true;
                DefaultLeg.Block = false;
                return DefaultLeg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultBoots, () =>
            {
                EquipmentInfo DefaultBoots = new EquipmentInfo(playerInfo);
                DefaultBoots.ItemId = (int)EEquipmentUid.DefaultBoots;
                DefaultBoots.ItemType = EEquipmentType.Boots.ToString();
                DefaultBoots.EquipmentName = EEquipmentName.默认鞋.ToString();
                DefaultBoots.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultBoots.Use = true;
                DefaultBoots.Block = false;
                return DefaultBoots;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Armor, () =>
            {
                EquipmentInfo Test1Armor = new EquipmentInfo(playerInfo);
                Test1Armor.ItemId = (int)EEquipmentUid.Test1Armor;
                Test1Armor.ItemType = EEquipmentType.Armor.ToString();
                Test1Armor.EquipmentName = EEquipmentName.测试1护甲.ToString();
                Test1Armor.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Armor.Price = 3300;
                return Test1Armor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Head, () =>
            {
                EquipmentInfo Test1Head = new EquipmentInfo(playerInfo);
                Test1Head.ItemId = (int)EEquipmentUid.Test1Head;
                Test1Head.ItemType = EEquipmentType.Head.ToString();
                Test1Head.EquipmentName = EEquipmentName.测试1头盔.ToString();
                Test1Head.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Head.Price = 3300;
                return Test1Head;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Hand, () =>
            {
                EquipmentInfo Test1Hand = new EquipmentInfo(playerInfo);
                Test1Hand.ItemId = (int)EEquipmentUid.Test1Hand;
                Test1Hand.ItemType = EEquipmentType.Hand.ToString();
                Test1Hand.EquipmentName = EEquipmentName.测试1手套.ToString();
                Test1Hand.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Hand.Price = 3300;
                return Test1Hand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Leg, () =>
            {
                EquipmentInfo Test1Leg = new EquipmentInfo(playerInfo);
                Test1Leg.ItemId = (int)EEquipmentUid.Test1Leg;
                Test1Leg.ItemType = EEquipmentType.Hand.ToString();
                Test1Leg.EquipmentName = EEquipmentName.测试1护腿.ToString();
                Test1Leg.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Leg.Price = 3300;
                return Test1Leg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Knee, () =>
            {
                EquipmentInfo Test1Knee = new EquipmentInfo(playerInfo);
                Test1Knee.ItemId = (int)EEquipmentUid.Test1Knee;
                Test1Knee.ItemType = EEquipmentType.Knee.ToString();
                Test1Knee.EquipmentName = EEquipmentName.测试1护膝.ToString();
                Test1Knee.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Knee.Price = 3300;
                return Test1Knee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Boots, () =>
            {
                EquipmentInfo Test1Boots = new EquipmentInfo(playerInfo);
                Test1Boots.ItemId = (int)EEquipmentUid.Test1Boots;
                Test1Boots.ItemType = EEquipmentType.Boots.ToString();
                Test1Boots.EquipmentName = EEquipmentName.测试1鞋.ToString();
                Test1Boots.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Boots.Price = 3300;
                return Test1Boots;
            });
            #endregion
            #region addGun
            GunInfo AK47 = (GunInfo)initItemInfoDict[(int)EGunUid.AK47]();
            gunInfos.Add((int)AK47.ItemId, AK47);
            gunInfoList.Add(AK47);

            GunInfo SL7 = (GunInfo)initItemInfoDict[(int)EGunUid.SL7]();
            gunInfos.Add((int)SL7.ItemId, SL7);
            gunInfoList.Add(SL7);

            GunInfo MP5 = (GunInfo)initItemInfoDict[(int)EGunUid.MP5]();
            gunInfos.Add((int)MP5.ItemId, MP5);
            gunInfoList.Add(MP5);

            GunInfo M700 = (GunInfo)initItemInfoDict[(int)EGunUid.M700]();
            gunInfos.Add((int)M700.ItemId, M700);
            gunInfoList.Add(M700);

            GunInfo M60 = (GunInfo)initItemInfoDict[(int)EGunUid.M60]();
            gunInfos.Add((int)M60.ItemId, M60);
            gunInfoList.Add(M60);

            GunInfo M1911 = (GunInfo)initItemInfoDict[(int)EGunUid.M1911]();
            gunInfos.Add(M1911.ItemId, M1911);
            gunInfoList.Add(M1911);

            GunInfo M4A1 = (GunInfo)initItemInfoDict[(int)EGunUid.M4A1]();
            gunInfos.Add(M4A1.ItemId, M4A1);
            gunInfoList.Add(M4A1);
            #endregion
            #region addEquipment
            EquipmentInfo DefaultArmor = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultArmor]();
            equipmentInfos.Add(DefaultArmor.ItemId, DefaultArmor);
            equipmentInfoList.Add(DefaultArmor);

            EquipmentInfo DefaultHead = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultHead]();
            equipmentInfos.Add(DefaultHead.ItemId, DefaultHead);
            equipmentInfoList.Add(DefaultHead);

            EquipmentInfo DefaultHand = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultHand]();
            equipmentInfos.Add(DefaultHand.ItemId, DefaultHand);
            equipmentInfoList.Add(DefaultHand);

            EquipmentInfo DefaultKnee = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultKnee]();
            equipmentInfos.Add(DefaultKnee.ItemId, DefaultKnee);
            equipmentInfoList.Add(DefaultKnee);

            EquipmentInfo DefaultLeg = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultLeg]();
            equipmentInfos.Add(DefaultLeg.ItemId, DefaultLeg);
            equipmentInfoList.Add(DefaultLeg);

            EquipmentInfo DefaultBoots = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.DefaultBoots]();
            equipmentInfos.Add(DefaultBoots.ItemId, DefaultBoots);
            equipmentInfoList.Add(DefaultBoots);

            EquipmentInfo Test1Armor = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Armor]();
            equipmentInfos.Add(Test1Armor.ItemId, Test1Armor);
            equipmentInfoList.Add(Test1Armor);

            EquipmentInfo Test1Head = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Head]();
            equipmentInfos.Add(Test1Head.ItemId, Test1Head);
            equipmentInfoList.Add(Test1Head);

            EquipmentInfo Test1Hand = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Hand]();
            equipmentInfos.Add(Test1Hand.ItemId, Test1Hand);
            equipmentInfoList.Add(Test1Hand);

            EquipmentInfo Test1Knee = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Knee]();
            equipmentInfos.Add(Test1Knee.ItemId, Test1Knee);
            equipmentInfoList.Add(Test1Knee);

            EquipmentInfo Test1Leg = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Leg]();
            equipmentInfos.Add(Test1Leg.ItemId, Test1Leg);
            equipmentInfoList.Add(Test1Leg);

            EquipmentInfo Test1Boots = (EquipmentInfo)initItemInfoDict[(int)EEquipmentUid.Test1Boots]();
            equipmentInfos.Add(Test1Boots.ItemId, Test1Boots);
            equipmentInfoList.Add(Test1Boots);
            #endregion

            foreach (EquipmentInfo item in equipmentInfoList)
            {
                itemsDict.Add(item.ItemId, item);
            }
            foreach (GunInfo item in gunInfoList)
            {
                itemsDict.Add(item.ItemId, item);
            }
        }

        public string InitPlayerGunInfo()
        {
            //----------To Json
            string json = JsonConvert.SerializeObject(gunInfoList);
            Console.WriteLine(json);
            return json;
            //end
        }

        public string InitEquipmentInfo()
        {
            string json = JsonConvert.SerializeObject(equipmentInfoList);
            Console.WriteLine(json);
            return json;
        }

        public void UpdatePlayerGunInfo(GunPack gunPack, bool isFirst = false)
        {
            if (gunInfos.TryGetValue(gunPack.ItemId,out GunInfo gunInfo))
            {
                gunPack.BaseDMG = gunInfo.BaseDMG;
                gunPack.FiringRate = gunInfo.FiringRate;
                gunPack.CurrentFiringRatePerSecond = gunInfo.CurrentFiringRatePerSecond;
                gunPack.Magazine = gunInfo.Magazine;
                gunPack.Price = gunInfo.Price;
                gunInfo.CoreProp = gunPack.CoreProp;
                gunInfo.CorePropValue = gunPack.CorePropValue;
                gunInfo.SubProp1 = gunPack.SubProp1;
                gunInfo.SubProp1Value = gunPack.SubProp1Value;
                gunInfo.SubProp2 = gunPack.SubProp2;
                gunInfo.SubProp2Value = gunPack.SubProp2Value;
                gunInfo.SubProp3 = gunPack.SubProp3;
                gunInfo.SubProp3Value = gunPack.SubProp3Value;
                gunInfo.Use = gunPack.Use;
                gunInfo.Block = gunPack.Block;
                if (gunInfo.Use)
                {
                    SetItemEquip(gunInfo.ItemId, gunInfo.ItemType, isFirst);
                }
            }
        }

        public void UpdatePlayerEquipmentInfo(EquipmentPack equipmentPack, bool isFirst =false)
        {
            if (equipmentInfos.TryGetValue(equipmentPack.ItemId, out EquipmentInfo equipmentInfo))
            {
                equipmentInfo.SubProp1 = equipmentPack.SubProp1;
                equipmentInfo.SubProp1Value = equipmentPack.SubProp1Value;
                equipmentInfo.SubProp2 = equipmentPack.SubProp2;
                equipmentInfo.SubProp2Value = equipmentPack.SubProp2Value;
                equipmentInfo.SubProp3 = equipmentPack.SubProp3;
                equipmentInfo.SubProp3Value = equipmentPack.SubProp3Value;
                equipmentInfo.Use = equipmentPack.Use;
                equipmentInfo.Block = equipmentPack.Block;
                equipmentPack.Price = equipmentInfo.Price;
                if (equipmentInfo.Use)
                {
                    SetItemEquip(equipmentInfo.ItemId, equipmentInfo.ItemType, isFirst); 
                }
            }
        }

        float GetCurrentFiringRatePerSecond(GunInfo gunInfo)
        {
            return 1/(gunInfo.FiringRate / 60f);
        }


    }
}
