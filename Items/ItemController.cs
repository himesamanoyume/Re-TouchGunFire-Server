using MySqlX.XDevAPI.Common;

using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public partial class ItemController
    {
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

        public bool RefreshAllSubProp(int uid)
        {
            if (itemsDict.TryGetValue(uid, out ItemInfo itemInfo))
            {
                if (itemInfo.SubProp1Type.Equals(ESubProp.Null) && itemInfo.SubProp2Type.Equals(ESubProp.Null) && itemInfo.SubProp3Type.Equals(ESubProp.Null))
                {
                    return false;
                }
                if(itemInfo.SubProp1Type != ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo, 1);
                    
                }
                if (itemInfo.SubProp2Type != ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo, 2);
                    
                }
                if (itemInfo.SubProp3Type != ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo, 3);

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        void SetRandomSubProp(ItemInfo itemInfo, int code)
        {
            if (code == 1 || code == 2 || code == 3)
            {
                itemInfo.OnRemove();
                switch (code)
                {
                    case 1:
                        Random random = new Random();
                        itemInfo.SubProp1Type = (ESubProp)random.Next((int)ESubProp.生命值百分比加成, (int)ESubProp.手枪伤害加成);
                        int per = random.Next(1, 100);
                        itemInfo.SubProp1Value = per / 1000f;
                        itemInfo.SubProp1 = itemInfo.SubProp1Type.ToString();
                        break;
                    case 2:
                        Random random1 = new Random();
                        itemInfo.SubProp2Type = (ESubProp)random1.Next((int)ESubProp.生命值百分比加成, (int)ESubProp.手枪伤害加成);
                        int per1 = random1.Next(1, 100);
                        itemInfo.SubProp2Value = per1 / 1000f;
                        itemInfo.SubProp2 = itemInfo.SubProp2Type.ToString();
                        break;
                    case 3:
                        Random random2 = new Random();
                        itemInfo.SubProp3Type = (ESubProp)random2.Next((int)ESubProp.生命值百分比加成, (int)ESubProp.手枪伤害加成);
                        int per2 = random2.Next(1, 100);
                        itemInfo.SubProp3Value = per2 / 1000f;
                        itemInfo.SubProp3 = itemInfo.SubProp3Type.ToString();
                        break;
                }
                itemInfo.OnEquip();
            }
            else
            {
                Debug.Log(new StackFrame(true), "错误的Code");
            }
        }

        public bool RefreshCoreProp(int itemId)
        {
            if (itemsDict.TryGetValue(itemId, out ItemInfo itemInfo))
            {
                itemInfo.OnRemove();
                Random random = new Random();
                int per = random.Next(1, 150);
                (itemInfo as GunInfo).CorePropValue = per / 1000f;
                itemInfo.OnEquip();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int UnlockItemSubProp(int itemId)
        {
            if (itemsDict.TryGetValue(itemId, out ItemInfo itemInfo))
            {
                if (itemInfo.SubProp1Type == ESubProp.Null && itemInfo.SubProp2Type == ESubProp.Null && itemInfo.SubProp3Type == ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo, 1);
                    return 1;//成功
                }else if (itemInfo.SubProp1Type != ESubProp.Null && itemInfo.SubProp2Type == ESubProp.Null && itemInfo.SubProp3Type == ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo ,2);
                    return 1;
                }
                else if (itemInfo.SubProp1Type != ESubProp.Null && itemInfo.SubProp2Type != ESubProp.Null && itemInfo.SubProp3Type == ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo ,3);
                    return 1;
                }
                else if(itemInfo.SubProp1Type != ESubProp.Null && itemInfo.SubProp2Type != ESubProp.Null && itemInfo.SubProp3Type != ESubProp.Null)
                {
                    return 2;//重复
                }
                return 3;//不正常
            }
            else
            {
                return 4;//未找到
            }
        }

        //public void UpdateItemProp()
        //{
        //    armorInfo.UpdateItemPropFunc();
        //    headInfo.UpdateItemPropFunc();
        //    handInfo.UpdateItemPropFunc();
        //    legInfo.UpdateItemPropFunc();
        //    kneeInfo.UpdateItemPropFunc();
        //    bootsInfo.UpdateItemPropFunc();
        //    mainGunInfo.UpdateItemPropFunc();
        //    handGunInfo.UpdateItemPropFunc();
        //}

        public void SetItemEquip(int itemId, string itemType, bool isFirst = false)
        {
            if (itemsDict.TryGetValue(itemId, out ItemInfo itemInfo))
            {
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
    }
}
