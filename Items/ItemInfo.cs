using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public abstract class ItemInfo
    {
        public ItemInfo(PlayerInfo playerInfo)
        {
            this.playerInfo = playerInfo;
        }
        protected PlayerInfo playerInfo;

        public virtual void OnEquip(bool isFirst = false)
        {
            if ((Block == false && Use == false) || (Block == false && Use == true && isFirst == true))
            {
                Use = true;
                EquipFunc();
            }
        }

        public virtual void UpdateItemPropFunc()
        {
            RemoveFunc();
            EquipFunc();
        }

        protected virtual void EquipFunc()
        {
            if (playerInfo.EquipSubPropFuncs.TryGetValue(SubProp1Type, out Action<float> action1))
            {
                action1?.Invoke(SubProp1Value);
            }
            if (playerInfo.EquipSubPropFuncs.TryGetValue(SubProp2Type, out Action<float> action2))
            {
                action2?.Invoke(SubProp2Value);
            }
            if (playerInfo.EquipSubPropFuncs.TryGetValue(SubProp3Type, out Action<float> action3))
            {
                action3?.Invoke(SubProp3Value);
            }
        }

        protected virtual void RemoveFunc()
        {
            if (playerInfo.RemoveSubPropFuncs.TryGetValue(SubProp1Type, out Action<float> action1))
            {
                action1?.Invoke(SubProp1Value);
            }
            if (playerInfo.RemoveSubPropFuncs.TryGetValue(SubProp2Type, out Action<float> action2))
            {
                action2?.Invoke(SubProp2Value);
            }
            if (playerInfo.RemoveSubPropFuncs.TryGetValue(SubProp3Type, out Action<float> action3))
            {
                action3?.Invoke(SubProp3Value);
            }
        }

        public virtual void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
                RemoveFunc();
            }
        }
        long price;
        public long Price { 
            get => price;
            set
            {
                price = value;
                diamondPrice = price / 10000f;
                unlockItemSubPropPrice = price / 4f;
                refreshItemPropPrice = price / 10f;
            }
        }
        float diamondPrice;
        public float DiamondPrice { get => diamondPrice; set => diamondPrice = value; }
        float unlockItemSubPropPrice;
        public float UnlockItemSubPropPrice { get => unlockItemSubPropPrice; set => unlockItemSubPropPrice = value; }
        public float RefreshItemPropPrice { get => refreshItemPropPrice; set => refreshItemPropPrice = value; }
        float refreshItemPropPrice;
        public bool Use { get => use; set => use = value; }
        public bool Block { get => block; set => block = value; }
        bool use = false;
        bool block = true;
        string subProp1 = ESubProp.Null.ToString();
        float subProp1Value = 0;
        ESubProp subProp1Type = ESubProp.Null;
        string subProp2 = ESubProp.Null.ToString();
        float subProp2Value = 0;
        ESubProp subProp2Type = ESubProp.Null;
        string subProp3 = ESubProp.Null.ToString();
        float subProp3Value = 0;
        ESubProp subProp3Type = ESubProp.Null;
        public string SubProp1 { get => subProp1; set => subProp1 = value; }
        public float SubProp1Value { get => subProp1Value; set => subProp1Value = value; }
        public string SubProp2 { get => subProp2; set => subProp2 = value; }
        public float SubProp2Value { get => subProp2Value; set => subProp2Value = value; }
        public string SubProp3 { get => subProp3; set => subProp3 = value; }
        public float SubProp3Value { get => subProp3Value; set => subProp3Value = value; }
        public ESubProp SubProp1Type { get => subProp1Type; set => subProp1Type = value; }
        public ESubProp SubProp2Type { get => subProp2Type; set => subProp2Type = value; }
        public ESubProp SubProp3Type { get => subProp3Type; set => subProp3Type = value; }
        string itemType;
        int itemId;
        public string ItemType { get => itemType; set => itemType = value; }
        public int ItemId { get => itemId; set => itemId = value; }
        public float HealthBonus { get => healthBonus; set => healthBonus = value; }
        public float BaseDmgBonus { get => baseDmgBonus; set => baseDmgBonus = value; }
        public float CDmgRateBonus { get => cDmgRateBonus; set => cDmgRateBonus = value; }
        public float CDmgBonus { get => cDmgBonus; set => cDmgBonus = value; }
        public float HeadshotDmgBonus { get => headshotDmgBonus; set => headshotDmgBonus = value; }
        public float PRateBonus { get => pRateBonus; set => pRateBonus = value; }
        public float AbeBonus { get => abeBonus; set => abeBonus = value; }
        public float ArmorBonus { get => armorBonus; set => armorBonus = value; }
        public float ItemHealth { get => itemHealth; set => itemHealth = value; }
        public float ItemArmor { get => itemArmor; set => itemArmor = value; }

        float healthBonus = 0;
        float baseDmgBonus = 0;
        float cDmgRateBonus = 0;
        float cDmgBonus = 0;
        float headshotDmgBonus = 0;
        float pRateBonus = 0;
        float abeBonus = 0;
        float armorBonus = 0;
        float itemHealth = 0;
        float itemArmor = 0;
    }  

    public sealed class EquipmentInfo : ItemInfo
    {

        public EquipmentInfo(PlayerInfo playerInfo, EEquipmentUid eEquipmentUid, EEquipmentType eEquipmentType, EEquipmentName eEquipmentName, EEquipmentSuit eEquipmentSuit, long price = 0, bool block = true, bool use = false) : base(playerInfo)
        {
            this.playerInfo = playerInfo;
            ItemId = (int)eEquipmentUid;
            ItemType = eEquipmentType.ToString();
            EquipmentName = eEquipmentName.ToString();
            EquipmentSuit = eEquipmentSuit.ToString();
            Price = price;
            Block = block;
            Use = use;
        }

        string equipmentSuit;
        string equipmentName;

        EEquipmentTalent talent1 = EEquipmentTalent.Null;
        EEquipmentTalent talent2 = EEquipmentTalent.Null;

        public string EquipmentSuit { get => equipmentSuit; set => equipmentSuit = value; }
        
        public EEquipmentTalent Talent1 { get => talent1; set => talent1 = value; }
        public EEquipmentTalent Talent2 { get => talent2; set => talent2 = value; }
        
        public string EquipmentName { get => equipmentName; set => equipmentName = value; }
    }

    public sealed class GunInfo : ItemInfo
    {
        public GunInfo(PlayerInfo playerInfo, EGunUid eGunUid, EGunName eGunName, EGunType eGunType, EGunCoreProp eGunCoreProp, float baseDmg, float firingRate, int magazine, float currentFiringRatePerSecond, float reloadingTime, long price, bool block = true, bool use = false ) : base(playerInfo)
        {
            this.playerInfo = playerInfo;
            ItemId = (int)eGunUid;
            GunName = eGunName.ToString();
            ItemType = eGunType.ToString();
            CoreProp = eGunCoreProp.ToString();
            CorePropType = eGunCoreProp;
            CorePropValue = 0.003f;
            BaseDmg = baseDmg;
            FiringRate = firingRate;
            Magazine = magazine;
            CurrentFiringRatePerSecond = currentFiringRatePerSecond;
            ReloadingTime = reloadingTime;
            Price = price;
            Block = block;
            Use = use;
        }

        string gunName;
        float baseDmg = 0;
        float firingRate = 0;
        float currentFiringRatePerSecond = 0;
        int magazine = 0;
        float reloadingTime = 0;
        string coreProp = EGunCoreProp.Null.ToString();
        float corePropValue = 0;
        EGunCoreProp corePropType;

        protected override void EquipFunc()
        {
            base.EquipFunc();
            if (playerInfo.EquipGunCorePropFuncs.TryGetValue(CorePropType, out Action<float> action))
            {
                action(CorePropValue);
            }
        }

        protected override void RemoveFunc()
        {
            base.RemoveFunc();
            if (playerInfo.RemoveGunCorePropFuncs.TryGetValue(CorePropType, out Action<float> action))
            {
                action(CorePropValue);
            }
        }

        public string GunName { get => gunName; set => gunName = value; }
        public float BaseDmg { get => baseDmg; set => baseDmg = value; }
        public float FiringRate { get => firingRate; set => firingRate = value; }
        public float CurrentFiringRatePerSecond { get => currentFiringRatePerSecond; set => currentFiringRatePerSecond = value; }
        public int Magazine { get => magazine; set => magazine = value; }
        public string CoreProp { get => coreProp; set => coreProp = value; }
        public float CorePropValue { get => corePropValue; set => corePropValue = value; }
        public EGunCoreProp CorePropType { get => corePropType; set => corePropType = value; }
        public float ReloadingTime { get => reloadingTime; set => reloadingTime = value; }
    }

}
