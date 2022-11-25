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

        string equipmentSuit;
        string equipmentName;
        string subProp1 = ESubProp.Null.ToString();
        float subProp1Value = 0;
        string subProp2 = ESubProp.Null.ToString();
        float subProp2Value = 0;
        string subProp3 = ESubProp.Null.ToString();
        float subProp3Value = 0;
        EEquipmentTalent talent1 = EEquipmentTalent.Null;
        EEquipmentTalent talent2 = EEquipmentTalent.Null;
        EEquipmentUid equipmentId;
        bool use = false;
        bool block = true;
        float price = 0;

        public string EquipmentSuit { get => equipmentSuit; set => equipmentSuit = value; }
        public string EquipmentName { get => equipmentName; set => equipmentName = value; }
        public string SubProp1 { get => subProp1; set => subProp1 = value; }
        public float SubProp1Value { get => subProp1Value; set => subProp1Value = value; }
        public string SubProp2 { get => subProp2; set => subProp2 = value; }
        public float SubProp2Value { get => subProp2Value; set => subProp2Value = value; }
        public string SubProp3 { get => subProp3; set => subProp3 = value; }
        public float SubProp3Value { get => subProp3Value; set => subProp3Value = value; }
        public EEquipmentTalent Talent1 { get => talent1; set => talent1 = value; }
        public EEquipmentTalent Talent2 { get => talent2; set => talent2 = value; }
        public EEquipmentUid EquipmentId { get => equipmentId; set => equipmentId = value; }
        public bool Use { get => use; set => use = value; }
        public bool Block { get => block; set => block = value; }
        public float Price { get => price; set => price = value; }
    }

    public abstract class GunInfo : ItemInfo
    {
        public GunInfo(ItemController itemController) : base(itemController)
        {
            this.itemController = itemController;
        }

        string gunName;
        string gunType;
        float baseDMG = 0;
        float firingRate = 0;
        float currentFiringRatePerSecond = 0;
        int magazine = 0;
        string coreProp = EGunCoreProp.Null.ToString();
        float corePropValue = 0;
        string subProp1 = ESubProp.Null.ToString();
        float subProp1Value = 0;
        string subProp2 = ESubProp.Null.ToString();
        float subProp2Value = 0;
        string subProp3 = ESubProp.Null.ToString();
        float subProp3Value = 0;
        EGunUid gunId;
        bool use = false;
        bool block = true;
        float price = 0;

        public string GunName { get => gunName; set => gunName = value; }
        public string GunType { get => gunType; set => gunType = value; }
        public float BaseDMG { get => baseDMG; set => baseDMG = value; }
        public float FiringRate { get => firingRate; set => firingRate = value; }
        public float CurrentFiringRatePerSecond { get => currentFiringRatePerSecond; set => currentFiringRatePerSecond = value; }
        public int Magazine { get => magazine; set => magazine = value; }
        public string CoreProp { get => coreProp; set => coreProp = value; }
        public float CorePropValue { get => corePropValue; set => corePropValue = value; }
        public string SubProp1 { get => subProp1; set => subProp1 = value; }
        public float SubProp1Value { get => subProp1Value; set => subProp1Value = value; }
        public string SubProp2 { get => subProp2; set => subProp2 = value; }
        public float SubProp2Value { get => subProp2Value; set => subProp2Value = value; }
        public string SubProp3 { get => subProp3; set => subProp3 = value; }
        public float SubProp3Value { get => subProp3Value; set => subProp3Value = value; }
        public EGunUid GunId { get => gunId; set => gunId = value; }
        public bool Use { get => use; set => use = value; }
        public bool Block { get => block; set => block = value; }
        public float Price { get => price; set => price = value; }
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
