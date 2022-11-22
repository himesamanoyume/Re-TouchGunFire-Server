using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public abstract class ItemInfo
    {
        protected ItemController itemController;
        public ItemInfo(ItemController itemController)
        {
            this.itemController = itemController;
        }

        public abstract void OnEquip();
        public abstract void OnRemove();
    }

    public abstract class EquipmentInfo : ItemInfo
    {

        public EquipmentInfo(ItemController itemController) : base(itemController)
        {
            this.itemController = itemController;
        }

        EEquipmentSuit equipmentSuit;
        EEquipmentName equipmentName;
        ESubProp subProp1;
        float subProp1Value;
        ESubProp subProp2;
        float subProp2Value;
        ESubProp subProp3;
        float subProp3Value;
        EEquipmentTalent talent1;
        EEquipmentTalent talent2;
        EEquipmentUid uid;
        bool use;
        bool block;

        public EEquipmentSuit EquipmentSuit { get => equipmentSuit; set => equipmentSuit = value; }
        public EEquipmentName EquipmentName { get => equipmentName; set => equipmentName = value; }
        public ESubProp SubProp1 { get => subProp1; set => subProp1 = value; }
        public float SubProp1Value { get => subProp1Value; set => subProp1Value = value; }
        public ESubProp SubProp2 { get => subProp2; set => subProp2 = value; }
        public float SubProp2Value { get => subProp2Value; set => subProp2Value = value; }
        public ESubProp SubProp3 { get => subProp3; set => subProp3 = value; }
        public float SubProp3Value { get => subProp3Value; set => subProp3Value = value; }
        public EEquipmentTalent Talent1 { get => talent1; set => talent1 = value; }
        public EEquipmentTalent Talent2 { get => talent2; set => talent2 = value; }
        public EEquipmentUid Uid { get => uid; set => uid = value; }
        public bool Use { get => use; set => use = value; }
        public bool Block { get => block; set => block = value; }
    }

    public abstract class GunInfo : ItemInfo
    {
        public GunInfo(ItemController itemController) : base(itemController)
        {
            this.itemController = itemController;
        }

        EGunName gunName;
        EGunType gunType;
        float baseDMG;
        float firingRate;
        float currentFiringRatePerSecond;
        int magazine;
        int magazineCount;
        EGunCoreProp coreProp;
        float corePropValue;
        ESubProp subProp1;
        float subProp1Value;
        ESubProp subProp2;
        float subProp2Value;
        ESubProp subProp3;
        float subProp3Value;
        EGunUid uid;
        bool use;
        bool block;

        public EGunName GunName { get => gunName; set => gunName = value; }
        public EGunType GunType { get => gunType; set => gunType = value; }
        public float BaseDMG { get => baseDMG; set => baseDMG = value; }
        public float FiringRate { get => firingRate; set => firingRate = value; }
        public float CurrentFiringRatePerSecond { get => currentFiringRatePerSecond; set => currentFiringRatePerSecond = value; }
        public int Magazine { get => magazine; set => magazine = value; }
        public int MagazineCount { get => magazineCount; set => magazineCount = value; }
        public EGunCoreProp CoreProp { get => coreProp; set => coreProp = value; }
        public float CorePropValue { get => corePropValue; set => corePropValue = value; }
        public ESubProp SubProp1 { get => subProp1; set => subProp1 = value; }
        public float SubProp1Value { get => subProp1Value; set => subProp1Value = value; }
        public ESubProp SubProp2 { get => subProp2; set => subProp2 = value; }
        public float SubProp2Value { get => subProp2Value; set => subProp2Value = value; }
        public ESubProp SubProp3 { get => subProp3; set => subProp3 = value; }
        public float SubProp3Value { get => subProp3Value; set => subProp3Value = value; }
        public EGunUid Uid { get => uid; set => uid = value; }
        public bool Use { get => use; set => use = value; }
        public bool Block { get => block; set => block = value; }
    }

    public sealed class MainGunInfo : GunInfo
    {
        public MainGunInfo(ItemController itemController) : base(itemController)
        {
            this.itemController = itemController;
        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class HandGunInfo : GunInfo
    {
        public HandGunInfo(ItemController itemController) : base(itemController)
        {
            this.itemController = itemController;
        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class ArmorInfo : EquipmentInfo
    {
        public ArmorInfo(ItemController itemController) : base(itemController) { 

            this.itemController = itemController;
        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class HeadInfo : EquipmentInfo
    {
        public HeadInfo(ItemController itemController) : base(itemController) { this.itemController = itemController; }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class HandInfo : EquipmentInfo
    {
        public HandInfo(ItemController itemController) : base(itemController) { this.itemController = itemController;  }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class KneeInfo : EquipmentInfo
    {
        public KneeInfo(ItemController itemController) : base(itemController) { this.itemController = itemController;        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class LegInfo : EquipmentInfo
    {
        public LegInfo(ItemController itemController) : base(itemController) { this.itemController = itemController;        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }

    public sealed class BootsInfo : EquipmentInfo
    {
        public BootsInfo(ItemController itemController) : base(itemController) { this.itemController = itemController;        }

        public override void OnEquip()
        {
            if (Block == false && Use == false)
            {
                Use = true;
            }
        }

        public override void OnRemove()
        {
            if (Block == false && Use == true)
            {
                Use = false;
            }
        }
    }
}
