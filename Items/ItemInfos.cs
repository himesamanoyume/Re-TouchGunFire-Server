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
            #endregion
            #region initEquipment
            initItemInfoDict.Add((int)EEquipmentUid.DefaultArmor, () =>
            {
                ArmorInfo DefaultArmor = new ArmorInfo(this);
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
                DefaultBoots.EquipmentName = EEquipmentName.默认鞋.ToString();
                DefaultBoots.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
                DefaultBoots.Use = true;
                DefaultBoots.Block = false;
                return DefaultBoots;
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
            #endregion
        }

        public string InitPlayerGunInfo()
        {
            //----------To Json
            string json = JsonConvert.SerializeObject(gunInfoList);
            //Console.WriteLine(json);
            return json;
            //end
        }

        public string InitEquipmentInfo()
        {
            string json = JsonConvert.SerializeObject(equipmentInfoList);
            //Console.WriteLine(json);
            return json;
        }

        public void UpdatePlayerGunInfo(GunPack gunPack)
        {
            if (gunInfos.TryGetValue(gunPack.GunId,out GunInfo gunInfo))
            {
                gunInfo.BaseDMG = gunPack.BaseDMG;
                gunInfo.FiringRate = gunPack.FiringRate;
                gunInfo.CurrentFiringRatePerSecond = gunPack.CurrentFiringRatePerSecond;
                gunInfo.Magazine = gunPack.Magazine;
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
            }
        }

        float GetCurrentFiringRatePerSecond(GunInfo gunInfo)
        {
            return 1/(gunInfo.FiringRate / 60f);
        }


    }
}
