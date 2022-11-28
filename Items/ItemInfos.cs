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
        //真正用于存储玩家武器装备信息的字典
        public Dictionary<int, GunInfo> gunInfos = new Dictionary<int, GunInfo>();
        public Dictionary<int, EquipmentInfo> equipmentInfos = new Dictionary<int, EquipmentInfo>();
        //end
        //用于注册用户登录时的初始化用列表
        List<GunInfo> gunInfoList = new List<GunInfo>();
        List<EquipmentInfo> equipmentInfoList = new List<EquipmentInfo>();
        //end
        Dictionary<int, Func<ItemInfo>> initItemInfoDict = new Dictionary<int, Func<ItemInfo>>();

        public ItemController()
        {
            #region initGun
            initItemInfoDict.Add((int)EGunUid.AK47, () =>
            {
                MainGunInfo AK47 = new MainGunInfo(this);
                AK47.GunId = EGunUid.AK47;
                AK47.GunName = EGunName.AK47.ToString();
                AK47.GunType = EGunType.AR.ToString();
                AK47.BaseDMG = 100f;
                AK47.FiringRate = 600f;
                AK47.Magazine = 30;
                AK47.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(AK47);
                AK47.Block = false;
                AK47.Use = true;
                return AK47;
            });

            initItemInfoDict.Add((int)EGunUid.SL7, () =>
            {
                MainGunInfo SL7 = new MainGunInfo(this);
                SL7.GunId = EGunUid.SL7;
                SL7.GunName = EGunName.SL7.ToString();
                SL7.GunType = EGunType.DMR.ToString();
                SL7.BaseDMG = 280f;
                SL7.FiringRate = 50f;
                SL7.Magazine = 5;
                SL7.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(SL7);
                SL7.Price = 8050;
                return SL7;
            });

            initItemInfoDict.Add((int)EGunUid.MP5, () =>
            {
                MainGunInfo MP5 = new MainGunInfo(this);
                MP5.GunId = EGunUid.MP5;
                MP5.GunName = EGunName.MP5.ToString();
                MP5.GunType = EGunType.SMG.ToString();
                MP5.BaseDMG = 70f;
                MP5.FiringRate = 800f;
                MP5.Magazine = 30;
                MP5.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(MP5);
                MP5.Price = 4500;
                return MP5;
            });

            initItemInfoDict.Add((int)EGunUid.M700, () =>
            {
                MainGunInfo M700 = new MainGunInfo(this);
                M700.GunId = EGunUid.M700;
                M700.GunName = EGunName.M700.ToString();
                M700.GunType = EGunType.SG.ToString();
                M700.BaseDMG = 300f;
                M700.FiringRate = 30f;
                M700.Magazine = 7;
                M700.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M700);
                M700.Price = 10500;
                return M700;
            });

            initItemInfoDict.Add((int)EGunUid.M60, () =>
            {
                MainGunInfo M60 = new MainGunInfo(this);
                M60.GunId = EGunUid.M60;
                M60.GunName = EGunName.M60.ToString();
                M60.GunType = EGunType.MG.ToString();
                M60.BaseDMG = 130f;
                M60.FiringRate = 500f;
                M60.Magazine = 100;
                M60.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M60);
                M60.Price = 3200;
                return M60;
            });

            initItemInfoDict.Add((int)EGunUid.M1911, () =>
            {
                HandGunInfo M1911 = new HandGunInfo(this);
                M1911.GunId = EGunUid.M1911;
                M1911.GunName = EGunName.M1911.ToString();
                M1911.GunType = EGunType.HG.ToString();
                M1911.BaseDMG = 90f;
                M1911.FiringRate = 100f;
                M1911.Magazine = 7;
                M1911.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M1911);
                M1911.Block = false;
                M1911.Use = true;
                return M1911;
            });

            initItemInfoDict.Add((int)EGunUid.M4A1, () =>
            {
                MainGunInfo M4A1 = new MainGunInfo(this);
                M4A1.GunId = EGunUid.M4A1;
                M4A1.GunName = EGunName.M4A1.ToString();
                M4A1.GunType = EGunType.AR.ToString();
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
                ArmorInfo DefaultArmor = new ArmorInfo(this);
                DefaultArmor.EquipmentType = EEquipmentType.Armor.ToString();
                DefaultArmor.EquipmentId = EEquipmentUid.DefaultArmor;
                DefaultArmor.EquipmentName = EEquipmentName.默认护甲.ToString();
                DefaultArmor.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultArmor.Use = true;
                DefaultArmor.Block = false;
                return DefaultArmor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHead, () =>
            {
                HeadInfo DefaultHead = new HeadInfo(this);
                DefaultHead.EquipmentId = EEquipmentUid.DefaultHead;
                DefaultHead.EquipmentType = EEquipmentType.Head.ToString();
                DefaultHead.EquipmentName = EEquipmentName.默认头盔.ToString();
                DefaultHead.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultHead.Use = true;
                DefaultHead.Block = false;
                return DefaultHead;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultHand, () =>
            {
                HandInfo DefaultHand = new HandInfo(this);
                DefaultHand.EquipmentId = EEquipmentUid.DefaultHand;
                DefaultHand.EquipmentType = EEquipmentType.Hand.ToString();
                DefaultHand.EquipmentName = EEquipmentName.默认手套.ToString();
                DefaultHand.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultHand.Use = true;
                DefaultHand.Block = false;
                return DefaultHand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultKnee, () =>
            {
                KneeInfo DefaultKnee = new KneeInfo(this);
                DefaultKnee.EquipmentId = EEquipmentUid.DefaultKnee;
                DefaultKnee.EquipmentType = EEquipmentType.Knee.ToString();
                DefaultKnee.EquipmentName = EEquipmentName.默认护膝.ToString();
                DefaultKnee.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultKnee.Use = true;
                DefaultKnee.Block = false;
                return DefaultKnee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultLeg, () =>
            {
                LegInfo DefaultLeg = new LegInfo(this);
                DefaultLeg.EquipmentId = EEquipmentUid.DefaultLeg;
                DefaultLeg.EquipmentType = EEquipmentType.Leg.ToString();
                DefaultLeg.EquipmentName = EEquipmentName.默认护腿.ToString();
                DefaultLeg.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultLeg.Use = true;
                DefaultLeg.Block = false;
                return DefaultLeg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.DefaultBoots, () =>
            {
                BootsInfo DefaultBoots = new BootsInfo(this);
                DefaultBoots.EquipmentId = EEquipmentUid.DefaultBoots;
                DefaultBoots.EquipmentType = EEquipmentType.Boots.ToString();
                DefaultBoots.EquipmentName = EEquipmentName.默认鞋.ToString();
                DefaultBoots.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultBoots.Use = true;
                DefaultBoots.Block = false;
                return DefaultBoots;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Armor, () =>
            {
                ArmorInfo Test1Armor = new ArmorInfo(this);
                Test1Armor.EquipmentId = EEquipmentUid.Test1Armor;
                Test1Armor.EquipmentType = EEquipmentType.Armor.ToString();
                Test1Armor.EquipmentName = EEquipmentName.测试1护甲.ToString();
                Test1Armor.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Armor.Price = 3300;
                return Test1Armor;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Head, () =>
            {
                HeadInfo Test1Head = new HeadInfo(this);
                Test1Head.EquipmentId = EEquipmentUid.Test1Head;
                Test1Head.EquipmentType = EEquipmentType.Head.ToString();
                Test1Head.EquipmentName = EEquipmentName.测试1头盔.ToString();
                Test1Head.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Head.Price = 3300;
                return Test1Head;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Hand, () =>
            {
                HandInfo Test1Hand = new HandInfo(this);
                Test1Hand.EquipmentId = EEquipmentUid.Test1Hand;
                Test1Hand.EquipmentType = EEquipmentType.Hand.ToString();
                Test1Hand.EquipmentName = EEquipmentName.测试1手套.ToString();
                Test1Hand.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Hand.Price = 3300;
                return Test1Hand;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Leg, () =>
            {
                LegInfo Test1Leg = new LegInfo(this);
                Test1Leg.EquipmentId = EEquipmentUid.Test1Leg;
                Test1Leg.EquipmentType = EEquipmentType.Hand.ToString();
                Test1Leg.EquipmentName = EEquipmentName.测试1护腿.ToString();
                Test1Leg.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Leg.Price = 3300;
                return Test1Leg;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Knee, () =>
            {
                KneeInfo Test1Knee = new KneeInfo(this);
                Test1Knee.EquipmentId = EEquipmentUid.Test1Knee;
                Test1Knee.EquipmentType = EEquipmentType.Knee.ToString();
                Test1Knee.EquipmentName = EEquipmentName.测试1护膝.ToString();
                Test1Knee.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Knee.Price = 3300;
                return Test1Knee;
            });

            initItemInfoDict.Add((int)EEquipmentUid.Test1Boots, () =>
            {
                BootsInfo Test1Boots = new BootsInfo(this);
                Test1Boots.EquipmentId = EEquipmentUid.Test1Boots;
                Test1Boots.EquipmentType = EEquipmentType.Boots.ToString();
                Test1Boots.EquipmentName = EEquipmentName.测试1鞋.ToString();
                Test1Boots.EquipmentSuit = EEquipmentSuit.测试1套装.ToString();
                Test1Boots.Price = 3300;
                return Test1Boots;
            });
            #endregion
            #region addGun
            MainGunInfo AK47 = (MainGunInfo)initItemInfoDict[(int)EGunUid.AK47]();
            gunInfos.Add((int)AK47.GunId, AK47);
            gunInfoList.Add(AK47);

            MainGunInfo SL7 = (MainGunInfo)initItemInfoDict[(int)EGunUid.SL7]();
            gunInfos.Add((int)SL7.GunId, SL7);
            gunInfoList.Add(SL7);

            MainGunInfo MP5 = (MainGunInfo)initItemInfoDict[(int)EGunUid.MP5]();
            gunInfos.Add((int)MP5.GunId, MP5);
            gunInfoList.Add(MP5);

            MainGunInfo M700 = (MainGunInfo)initItemInfoDict[(int)EGunUid.M700]();
            gunInfos.Add((int)M700.GunId, M700);
            gunInfoList.Add(M700);

            MainGunInfo M60 = (MainGunInfo)initItemInfoDict[(int)EGunUid.M60]();
            gunInfos.Add((int)M60.GunId, M60);
            gunInfoList.Add(M60);

            HandGunInfo M1911 = (HandGunInfo)initItemInfoDict[(int)EGunUid.M1911]();
            gunInfos.Add((int)M1911.GunId, M1911);
            gunInfoList.Add(M1911);

            MainGunInfo M4A1 = (MainGunInfo)initItemInfoDict[(int)EGunUid.M4A1]();
            gunInfos.Add((int)M4A1.GunId, M4A1);
            gunInfoList.Add(M4A1);
            #endregion
            #region addEquipment
            ArmorInfo DefaultArmor = (ArmorInfo)initItemInfoDict[(int)EEquipmentUid.DefaultArmor]();
            equipmentInfos.Add((int)DefaultArmor.EquipmentId, DefaultArmor);
            equipmentInfoList.Add(DefaultArmor);

            HeadInfo DefaultHead = (HeadInfo)initItemInfoDict[(int)EEquipmentUid.DefaultHead]();
            equipmentInfos.Add((int)DefaultHead.EquipmentId, DefaultHead);
            equipmentInfoList.Add(DefaultHead);

            HandInfo DefaultHand = (HandInfo)initItemInfoDict[(int)EEquipmentUid.DefaultHand]();
            equipmentInfos.Add((int)DefaultHand.EquipmentId, DefaultHand);
            equipmentInfoList.Add(DefaultHand);

            KneeInfo DefaultKnee = (KneeInfo)initItemInfoDict[(int)EEquipmentUid.DefaultKnee]();
            equipmentInfos.Add((int)DefaultKnee.EquipmentId, DefaultKnee);
            equipmentInfoList.Add(DefaultKnee);

            LegInfo DefaultLeg = (LegInfo)initItemInfoDict[(int)EEquipmentUid.DefaultLeg]();
            equipmentInfos.Add((int)DefaultLeg.EquipmentId, DefaultLeg);
            equipmentInfoList.Add(DefaultLeg);

            BootsInfo DefaultBoots = (BootsInfo)initItemInfoDict[(int)EEquipmentUid.DefaultBoots]();
            equipmentInfos.Add((int)DefaultBoots.EquipmentId, DefaultBoots);
            equipmentInfoList.Add(DefaultBoots);

            ArmorInfo Test1Armor = (ArmorInfo)initItemInfoDict[(int)EEquipmentUid.Test1Armor]();
            equipmentInfos.Add((int)Test1Armor.EquipmentId, Test1Armor);
            equipmentInfoList.Add(Test1Armor);

            HeadInfo Test1Head = (HeadInfo)initItemInfoDict[(int)EEquipmentUid.Test1Head]();
            equipmentInfos.Add((int)Test1Head.EquipmentId, Test1Head);
            equipmentInfoList.Add(Test1Head);

            HandInfo Test1Hand = (HandInfo)initItemInfoDict[(int)EEquipmentUid.Test1Hand]();
            equipmentInfos.Add((int)Test1Hand.EquipmentId, Test1Hand);
            equipmentInfoList.Add(Test1Hand);

            KneeInfo Test1Knee = (KneeInfo)initItemInfoDict[(int)EEquipmentUid.Test1Knee]();
            equipmentInfos.Add((int)Test1Knee.EquipmentId, Test1Knee);
            equipmentInfoList.Add(Test1Knee);

            LegInfo Test1Leg = (LegInfo)initItemInfoDict[(int)EEquipmentUid.Test1Leg]();
            equipmentInfos.Add((int)Test1Leg.EquipmentId, Test1Leg);
            equipmentInfoList.Add(Test1Leg);

            BootsInfo Test1Boots = (BootsInfo)initItemInfoDict[(int)EEquipmentUid.Test1Boots]();
            equipmentInfos.Add((int)Test1Boots.EquipmentId, Test1Boots);
            equipmentInfoList.Add(Test1Boots);
            #endregion
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

        public void UpdatePlayerGunInfo(GunPack gunPack)
        {
            if (gunInfos.TryGetValue(gunPack.GunId,out GunInfo gunInfo))
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
            }
        }

        public void UpdatePlayerEquipmentInfo(EquipmentPack equipmentPack)
        {
            if (equipmentInfos.TryGetValue(equipmentPack.EquipmentId, out EquipmentInfo equipmentInfo))
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
            }
        }

        float GetCurrentFiringRatePerSecond(GunInfo gunInfo)
        {
            return 1/(gunInfo.FiringRate / 60f);
        }


    }
}
