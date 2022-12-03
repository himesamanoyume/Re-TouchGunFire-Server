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
                GunInfo AK47 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.AK47,
                    eGunName: EGunName.AK47,
                    eGunType: EGunType.AR,
                    eGunCoreProp:EGunCoreProp.自动步枪伤害加成,
                    baseDmg:100f,
                    firingRate:600f,
                    magazine:30,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(600f),
                    price:1000,
                    block:false,
                    use:true
                 );
                return AK47;
            });

            initItemInfoDict.Add((int)EGunUid.SL7, () =>
            {
                GunInfo SL7 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.SL7,
                    eGunName: EGunName.SL7,
                    eGunType: EGunType.DMR,
                    eGunCoreProp: EGunCoreProp.射手步枪伤害加成,
                    baseDmg: 280f,
                    firingRate: 50f,
                    magazine: 5,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(50f),
                    price: 8050
                 );
                return SL7;
            });

            initItemInfoDict.Add((int)EGunUid.MP5, () =>
            {
                GunInfo MP5 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.MP5,
                    eGunName: EGunName.MP5,
                    eGunType: EGunType.SMG,
                    eGunCoreProp: EGunCoreProp.微型冲锋枪伤害加成,
                    baseDmg: 70f,
                    firingRate: 800f,
                    magazine: 30,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(800f),
                    price: 4500
                 );
                return MP5;
            });

            initItemInfoDict.Add((int)EGunUid.M700, () =>
            {
                GunInfo M700 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.M700,
                    eGunName: EGunName.M700,
                    eGunType: EGunType.SG,
                    eGunCoreProp: EGunCoreProp.狙击步枪伤害加成,
                    baseDmg: 300f,
                    firingRate: 30f,
                    magazine: 7,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(30f),
                    price: 10500
                 );
                return M700;
            });

            initItemInfoDict.Add((int)EGunUid.M60, () =>
            {
                GunInfo M60 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.M60,
                    eGunName: EGunName.M60,
                    eGunType: EGunType.MG,
                    eGunCoreProp: EGunCoreProp.轻机枪伤害加成,
                    baseDmg: 130f,
                    firingRate: 500f,
                    magazine: 100,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(500f),
                    price: 3200
                 );
                return M60;
            });

            initItemInfoDict.Add((int)EGunUid.M1911, () =>
            {
                GunInfo M1911 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.M1911,
                    eGunName: EGunName.M1911,
                    eGunType: EGunType.HG,
                    eGunCoreProp: EGunCoreProp.手枪伤害加成,
                    baseDmg: 90f,
                    firingRate: 100f,
                    magazine: 7,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(100f),
                    price: 500,
                    block:false,
                    use:true
                 );
                return M1911;
            });

            initItemInfoDict.Add((int)EGunUid.M4A1, () =>
            {
                GunInfo M4A1 = new GunInfo(
                    playerInfo,
                    eGunUid: EGunUid.M4A1,
                    eGunName: EGunName.M4A1,
                    eGunType: EGunType.AR,
                    eGunCoreProp: EGunCoreProp.自动步枪伤害加成,
                    baseDmg: 85f,
                    firingRate: 780f,
                    magazine: 30,
                    currentFiringRatePerSecond: GetCurrentFiringRatePerSecond(780f),
                    price: 7700
                 );
                return M4A1;
            });
            #endregion
            #region initEquipment
            initItemInfoDict.Add((int)EEquipmentUid.DefaultArmor, () =>
            {
                EquipmentInfo DefaultArmor = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultArmor,
                    eEquipmentType: EEquipmentType.Armor,
                    eEquipmentName: EEquipmentName.默认护甲,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultArmor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHead, () =>
            {
                EquipmentInfo DefaultHead = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultHead,
                    eEquipmentType: EEquipmentType.Head,
                    eEquipmentName: EEquipmentName.默认头盔,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultHead;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHand, () =>
            {
                EquipmentInfo DefaultHand = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultHand,
                    eEquipmentType: EEquipmentType.Hand,
                    eEquipmentName: EEquipmentName.默认手套,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultHand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultKnee, () =>
            {
                EquipmentInfo DefaultKnee = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultKnee,
                    eEquipmentType: EEquipmentType.Knee,
                    eEquipmentName: EEquipmentName.默认护膝,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultKnee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultLeg, () =>
            {
                EquipmentInfo DefaultLeg = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultLeg,
                    eEquipmentType: EEquipmentType.Leg,
                    eEquipmentName: EEquipmentName.默认护腿,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultLeg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultBoots, () =>
            {
                EquipmentInfo DefaultBoots = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.DefaultBoots,
                    eEquipmentType: EEquipmentType.Boots,
                    eEquipmentName: EEquipmentName.默认鞋,
                    eEquipmentSuit: EEquipmentSuit.无套装效果,
                    block: false,
                    use: true
                );
                return DefaultBoots;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Armor, () =>
            {
                EquipmentInfo Test1Armor = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Armor,
                    eEquipmentType: EEquipmentType.Armor,
                    eEquipmentName: EEquipmentName.测试1护甲,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
                return Test1Armor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Head, () =>
            {
                EquipmentInfo Test1Head = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Head,
                    eEquipmentType: EEquipmentType.Head,
                    eEquipmentName: EEquipmentName.测试1头盔,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
                return Test1Head;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Hand, () =>
            {
                EquipmentInfo Test1Hand = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Hand,
                    eEquipmentType: EEquipmentType.Hand,
                    eEquipmentName: EEquipmentName.测试1手套,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
                return Test1Hand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Leg, () =>
            {
                EquipmentInfo Test1Leg = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Leg,
                    eEquipmentType: EEquipmentType.Leg,
                    eEquipmentName: EEquipmentName.测试1护腿,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
                return Test1Leg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Knee, () =>
            {
                EquipmentInfo Test1Knee = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Knee,
                    eEquipmentType: EEquipmentType.Knee,
                    eEquipmentName: EEquipmentName.测试1护膝,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
                return Test1Knee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Boots, () =>
            {
                EquipmentInfo Test1Boots = new EquipmentInfo(
                    playerInfo,
                    eEquipmentUid: EEquipmentUid.Test1Boots,
                    eEquipmentType: EEquipmentType.Boots,
                    eEquipmentName: EEquipmentName.测试1鞋,
                    eEquipmentSuit: EEquipmentSuit.测试1套装,
                    price: 3300
                );
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

        float GetCurrentFiringRatePerSecond(float firingRate)
        {
            return 1 / (firingRate / 60f);
        }


    }
}
