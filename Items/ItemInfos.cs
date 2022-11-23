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
        List<GunInfo> gunInfos = new List<GunInfo>();
        List<EquipmentInfo> equipmentInfos = new List<EquipmentInfo>();

        
        public string InitPlayerGunInfo()
        {
            MainGunInfo AK47 = new MainGunInfo(this);
            AK47.Uid = EGunUid.AK47;
            AK47.GunName = EGunName.AK47.ToString();
            AK47.GunType = EGunType.AR.ToString();
            AK47.BaseDMG = 100f;
            AK47.FiringRate = 600f;
            AK47.Magazine = 30;
            AK47.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(AK47);
            AK47.Block = false;
            AK47.Use = true;
            gunInfos.Add(AK47);
            //AddItemInfo((int)AK47.Uid, AK47);

            MainGunInfo SL7 = new MainGunInfo(this);
            SL7.Uid = EGunUid.SL7;
            SL7.GunName = EGunName.SL7.ToString();
            SL7.GunType = EGunType.DMR.ToString();
            SL7.BaseDMG = 280f;
            SL7.FiringRate = 50f;
            SL7.Magazine = 5;
            SL7.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(SL7);
            gunInfos.Add(SL7);
            //AddItemInfo((int)SL7.Uid, SL7);

            MainGunInfo MP5 = new MainGunInfo(this);
            MP5.Uid = EGunUid.MP5;
            MP5.GunName = EGunName.MP5.ToString();
            MP5.GunType = EGunType.SMG.ToString();
            MP5.BaseDMG = 70f;
            MP5.FiringRate = 800f;
            MP5.Magazine = 30;
            MP5.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(MP5);
            gunInfos.Add(MP5);
            //AddItemInfo((int)MP5.Uid, MP5);

            MainGunInfo M700 = new MainGunInfo(this);
            M700.Uid = EGunUid.M700;
            M700.GunName = EGunName.M700.ToString();
            M700.GunType = EGunType.SG.ToString();
            M700.BaseDMG = 300f;
            M700.FiringRate = 30f;
            M700.Magazine = 7;
            M700.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M700);
            gunInfos.Add(M700);
            //AddItemInfo((int)M700.Uid, M700);

            MainGunInfo M60 = new MainGunInfo(this);
            M60.Uid = EGunUid.M60;
            M60.GunName = EGunName.M60.ToString();
            M60.GunType = EGunType.MG.ToString();
            M60.BaseDMG = 130f;
            M60.FiringRate = 500f;
            M60.Magazine = 100;
            M60.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M60);
            gunInfos.Add(M60);
            //AddItemInfo((int)M60.Uid, M60);

            HandGunInfo M1911 = new HandGunInfo(this);
            M1911.Uid = EGunUid.M1911;
            M1911.GunName = EGunName.M1911.ToString();
            M1911.GunType = EGunType.HG.ToString();
            M1911.BaseDMG = 90f;
            M1911.FiringRate = 100f;
            M1911.Magazine = 7;
            M1911.CurrentFiringRatePerSecond = GetCurrentFiringRatePerSecond(M1911);
            M1911.Block = false;
            M1911.Use = true;
            gunInfos.Add(M1911);
            //AddItemInfo((int)M1911.Uid, M1911);

            //----------To Json
            return JsonConvert.SerializeObject(gunInfos);
            //Console.WriteLine(json);
            //end

        }

        public string InitEquipmentInfo()
        {
            ArmorInfo DefaultArmor = new ArmorInfo(this);
            DefaultArmor.Uid = EEquipmentUid.DefaultArmor;
            DefaultArmor.EquipmentName = EEquipmentName.默认护甲.ToString();
            DefaultArmor.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultArmor.Use = true;
            DefaultArmor.Block = false;
            equipmentInfos.Add(DefaultArmor);

            HeadInfo DefaultHead = new HeadInfo(this);
            DefaultHead.Uid = EEquipmentUid.DefaultHead;
            DefaultHead.EquipmentName = EEquipmentName.默认头盔.ToString();
            DefaultHead.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultHead.Use = true;
            DefaultHead.Block = false;
            equipmentInfos.Add(DefaultHead);

            HandInfo DefaultHand = new HandInfo(this);
            DefaultHand.Uid = EEquipmentUid.DefaultHead;
            DefaultHand.EquipmentName = EEquipmentName.默认手套.ToString();
            DefaultHand.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultHand.Use = true;
            DefaultHand.Block = false;
            equipmentInfos.Add(DefaultHand);

            KneeInfo DefaultKnee = new KneeInfo(this);
            DefaultKnee.Uid = EEquipmentUid.DefaultKnee;
            DefaultKnee.EquipmentName = EEquipmentName.默认护膝.ToString();
            DefaultKnee.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultKnee.Use = true;
            DefaultKnee.Block = false;
            equipmentInfos.Add(DefaultKnee);

            LegInfo DefaultLeg = new LegInfo(this);
            DefaultLeg.Uid = EEquipmentUid.DefaultLeg;
            DefaultLeg.EquipmentName = EEquipmentName.默认护腿.ToString();
            DefaultLeg.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultLeg.Use = true;
            DefaultLeg.Block = false;
            equipmentInfos.Add(DefaultLeg);

            BootsInfo DefaultBoots = new BootsInfo(this);
            DefaultBoots.Uid = EEquipmentUid.DefaultBoots;
            DefaultBoots.EquipmentName = EEquipmentName.默认鞋.ToString();
            DefaultBoots.EquipmentSuit = EEquipmentSuit.无套装效果.ToString();
            DefaultBoots.Use = true;
            DefaultBoots.Block = false;
            equipmentInfos.Add(DefaultBoots);


            return JsonConvert.SerializeObject(equipmentInfos);
        }

        float GetCurrentFiringRatePerSecond(GunInfo gunInfo)
        {
            return 1/(gunInfo.FiringRate / 60f);
        }
    }
}
