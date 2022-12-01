﻿using MySqlX.XDevAPI.Common;

using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public partial class ItemController
    {
        //private MainGunInfo mainGunInfo = null;
        //private HandGunInfo handGunInfo = null;
        //private ArmorInfo armorInfo = null;
        //private HeadInfo headInfo = null;
        //private HandInfo handInfo = null;
        //private KneeInfo kneeInfo = null;
        //private LegInfo legInfo = null;
        //private BootsInfo bootsInfo = null;
        private GunInfo mainGunInfo = null;
        private GunInfo handGunInfo = null;
        private EquipmentInfo armorInfo = null;
        private EquipmentInfo headInfo = null;
        private EquipmentInfo handInfo = null;
        private EquipmentInfo kneeInfo = null;
        private EquipmentInfo legInfo = null;
        private EquipmentInfo bootsInfo = null;


        //用于不分类存储玩家武器和装备信息的字典
        public Dictionary<int, ItemInfo> itemsDict = new Dictionary<int, ItemInfo>();
        //end
        public void AddItemInfo(int uid, ItemInfo itemInfo)
        {
            itemsDict.Add(uid, itemInfo);
        }

        public void RemoveItemInfo(int uid)
        {
            itemsDict.Remove(uid);
        }

        public void InitAllProp(int uid)
        {

        }

        public void SetItemEquip(int itemId, string itemType, bool isFirst = false)
        {
            if (itemsDict.TryGetValue(itemId, out ItemInfo itemInfo))
            {
                //if (mainGunInfo != null)
                //{
                //    mainGunInfo.OnRemove();
                //}
                //mainGunInfo = (MainGunInfo)itemInfo;
                //mainGunInfo.OnEquip(isFirst);
                switch (itemType)
                {
                    case "Armor":
                        if (armorInfo != null)
                        {
                            armorInfo.OnRemove();
                        }
                        armorInfo = (EquipmentInfo)itemInfo;
                        armorInfo.OnEquip(isFirst);
                        break;
                    case "Head":
                        if (headInfo != null)
                        {
                            headInfo.OnRemove();
                        }
                        headInfo = (EquipmentInfo)itemInfo;
                        headInfo.OnEquip(isFirst);
                        break;
                    case "Hand":
                        if (handInfo != null)
                        {
                            handInfo.OnRemove();
                        }
                        handInfo = (EquipmentInfo)itemInfo;
                        handInfo.OnEquip(isFirst);
                        break;
                    case "Leg":
                        if (legInfo != null)
                        {
                            legInfo.OnRemove();
                        }
                        legInfo = (EquipmentInfo)itemInfo;
                        legInfo.OnEquip(isFirst);
                        break;
                    case "Knee":
                        if (kneeInfo != null)
                        {
                            kneeInfo.OnRemove();
                        }
                        kneeInfo = (EquipmentInfo)itemInfo;
                        kneeInfo.OnEquip(isFirst);
                        break;
                    case "Boots":
                        if (bootsInfo != null)
                        {
                            bootsInfo.OnRemove();
                        }
                        bootsInfo = (EquipmentInfo)itemInfo;
                        bootsInfo.OnEquip(isFirst);
                        break;
                    case "HG":
                        if (handGunInfo != null)
                        {
                            handGunInfo.OnRemove();
                        }
                        handGunInfo = (GunInfo)itemInfo;
                        handGunInfo.OnEquip(isFirst);
                        break;
                    default:
                        if (mainGunInfo != null)
                        {
                            mainGunInfo.OnRemove();
                        }
                        mainGunInfo = (GunInfo)itemInfo;
                        mainGunInfo.OnEquip(isFirst);
                        break;
                }
            }
        }

        //public void SetMainGun(EGunUid eGunUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eGunUid, out ItemInfo itemInfo))
        //    {
        //        if (mainGunInfo != null)
        //        {
        //            mainGunInfo.OnRemove();
        //        }
        //        mainGunInfo = (MainGunInfo)itemInfo;
        //        mainGunInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetHandGun(EGunUid eGunUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eGunUid, out ItemInfo itemInfo))
        //    {
        //        if (handGunInfo != null)
        //        {
        //            handGunInfo.OnRemove();
        //        }
        //        handGunInfo = (HandGunInfo)itemInfo;
        //        handGunInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetArmor(EEquipmentUid eEquipmentUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (armorInfo != null)
        //        {
        //            armorInfo.OnRemove();
        //        }
        //        armorInfo = (ArmorInfo)itemInfo;
        //        armorInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetHead(EEquipmentUid eEquipmentUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (headInfo != null)
        //        {
        //            headInfo.OnRemove();
        //        }
        //        headInfo = (HeadInfo)itemInfo;
        //        headInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetHand(EEquipmentUid eEquipmentUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (handInfo != null)
        //        {
        //            handInfo.OnRemove();
        //        }
        //        handInfo = (HandInfo)itemInfo;
        //        handInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetLeg(EEquipmentUid eEquipmentUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (legInfo != null)
        //        {
        //            legInfo.OnRemove();
        //        }
        //        legInfo = (LegInfo)itemInfo;
        //        legInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetKnee(EEquipmentUid eEquipmentUid , bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (kneeInfo != null)
        //        {
        //            kneeInfo.OnRemove();
        //        }
        //        kneeInfo = (KneeInfo)itemInfo;
        //        kneeInfo.OnEquip(isFirst);
        //    }
        //}

        //public void SetBoots(EEquipmentUid eEquipmentUid, bool isFirst = false)
        //{
        //    if (itemsDict.TryGetValue((int)eEquipmentUid, out ItemInfo itemInfo))
        //    {
        //        if (bootsInfo != null)
        //        {
        //            bootsInfo.OnRemove();
        //        }
        //        bootsInfo = (BootsInfo)itemInfo;
        //        bootsInfo.OnEquip(isFirst);
        //    }
        //}
    }
}
