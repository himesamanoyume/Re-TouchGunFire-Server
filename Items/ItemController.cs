using MySqlX.XDevAPI.Common;

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
                    SetRandomSubProp(itemInfo.SubProp1Type, itemInfo.SubProp1Value, itemInfo.SubProp1);
                }
                if (itemInfo.SubProp2Type != ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo.SubProp2Type, itemInfo.SubProp2Value, itemInfo.SubProp2);
                }
                if (itemInfo.SubProp3Type != ESubProp.Null)
                {
                    SetRandomSubProp(itemInfo.SubProp3Type, itemInfo.SubProp3Value, itemInfo.SubProp3);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        void SetRandomSubProp(ESubProp subPropType, float subPropValue, string subProp)
        {
            Random random = new Random();
            subPropType = (ESubProp)random.Next((int)ESubProp.health, (int)ESubProp.hgDmgBonus);
            int per = random.Next(1, 100);
            subPropValue = per / 1000f;
            subProp = subPropType.ToString();
        }

        public bool RefreshCoreProp(int uid)
        {
            if (itemsDict.TryGetValue(uid, out ItemInfo itemInfo))
            {
                Random random = new Random();
                int per = random.Next(1, 150);
                (itemInfo as GunInfo).CorePropValue = per / 1000f;
                return true;
            }
            else
            {
                return false;
            }
        }

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
