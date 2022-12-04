using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class PlayerInfo
    {
        int uid;
        public int Uid {
            get { return uid; }
            set
            {
                if (value<=0)
                {
                    uid = 0;
                }
                else
                {
                    uid = value;
                }
            }
        }
        string playerName;
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        int level;
        public int Level
        {
            get { return level; }
            set
            {
                if (value<=1)
                {
                    level = 1;
                }
                else
                {
                    level = value;
                }
            }
        }
        float currentExp;
        public float CurrentExp
        {
            get { return currentExp; }
            set
            {
                if (value >= 1000f)
                {
                    currentExp = 1000f;
                }
                else if(value<= 0)
                {
                    currentExp = 0;
                }
                else
                {
                    currentExp = value;
                }
            }
        }
        float maxExp;
        public float MaxExp
        {
            get { return maxExp; }
        }
        float currentHealth;
        float maxHealth;
        float currentAmor;
        float maxArmor;
        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                if (value>=0 && value <= maxHealth)
                {
                    currentHealth = value;
                }
                else if(value < 0)
                {
                    currentHealth = 0;
                }else if (value > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }
        public float CurrentArmor
        {
            get { return currentAmor; }
            set
            {
                if (value >= 0 && value <= maxArmor)
                {
                    currentAmor = value;
                }
                else if (value < 0)
                {
                    currentAmor = 0;
                }
                else if (value > maxArmor)
                {
                    currentAmor = maxArmor;
                }
            }
        }
        public float MaxHealth
        {
            get { return maxHealth; }
            set
            {
                if (value<= 1000f)
                {
                    maxHealth = 1000f;
                }
                else
                {
                    maxHealth = value;
                }
            }
        }
        public float MaxArmor
        {
            get { return maxArmor; }
            set
            {
                if (value<= 1000f)
                {
                    maxArmor = 1000f;
                }
                else
                {
                    maxArmor = value;
                }
            }
        }
        float baseDmgBonus;
        public float BaseDmgBonus
        {
            get { return baseDmgBonus; }
            set
            {
                baseDmgBonus = value;
            }
        }
        float critDmgRateBonus;
        public float CritDmgRateBonus 
        {
            get { return critDmgRateBonus; }
            set
            {
                critDmgRateBonus = value;
            }
        }
        float critDmgBonus;
        public float CritDmgBonus
        {
            get { return critDmgBonus; }
            set { critDmgBonus = value; }
        }
        float headshotDmgBonus;
        public float HeadshotDmgRateBonus
        {
            get { return headshotDmgBonus; }
            set
            {
                headshotDmgBonus = value;
                if (headshotDmgBonus <= 0)
                {
                    headshotDmgBonus = 0;
                }
            }
        }
        float pRateBonus;//穿透率加成
        public float PRateBonus
        {
            get { return pRateBonus; }
            set
            {
                pRateBonus = value;
                if (pRateBonus <= 0)
                {
                    pRateBonus = 0;
                }
            }
        }
        float abeBonus;//破甲效率加成
        public float AbeBonus
        {
            get { return abeBonus; }
            set
            {
                abeBonus = value;
                if (abeBonus <= 0)
                {
                    abeBonus = 0;
                }
            }
        }
        float arDmgBonus;
        public float ArDmgBonus
        {
            get { return arDmgBonus; }
            set
            {
                arDmgBonus = value;
                if (arDmgBonus<=0)
                {
                    arDmgBonus=0;
                }
            }
        }
        float dmrDmgBonus;
        public float DmrDmgBonus
        {
            get { return dmrDmgBonus; }
            set
            {
                dmrDmgBonus = value;
                if (dmrDmgBonus <= 0)
                {
                    dmrDmgBonus = 0;
                }
            }
        }
        float smgDmgBonus;
        public float SmgDmgBonus
        {
            get { return smgDmgBonus; }
            set
            {
                smgDmgBonus = value;
                if (smgDmgBonus <= 0)
                {
                    smgDmgBonus = 0;
                }
            }
        }
        float sgDmgBonus;
        public float SgDmgBonus
        {
            get { return sgDmgBonus; }
            set
            {
                sgDmgBonus = value;
                if (sgDmgBonus <= 0)
                {
                    sgDmgBonus = 0;
                }
            }
        }
        float mgDmgBonus;
        public float MgDmgBonus
        {
            get { return mgDmgBonus; }
            set
            {
                mgDmgBonus = value;
                if (mgDmgBonus <= 0)
                {
                    mgDmgBonus = 0;
                }
            }
        }
        float srDmgBonus;
        public float SrDmgBonus
        {
            get { return srDmgBonus; }
            set
            {
                srDmgBonus = value;
                if (srDmgBonus <= 0)
                {
                    srDmgBonus = 0;
                }
            }
        }
        float hgDmgBonus;
        public float HgDmgBonus
        {
            get { return hgDmgBonus; }
            set
            {
                hgDmgBonus = value;
                if (hgDmgBonus <= 0)
                {
                    hgDmgBonus = 0;
                }
            }
        }
        float diamond;
        public float Diamond
        {
            get { return diamond; }
            set { diamond = value; }
        }
        float coin;
        public float Coin
        {
            get { return coin; }
            set { coin = value; }
        }

        public PlayerInfo()
        {
            uid = 0;
            playerName = "";
            level = 0;
            currentExp = 0;
            maxExp = 1000f;
            currentHealth = 1000f;
            maxHealth = 1000f;
            currentAmor = 1000f;
            maxArmor = 1000f;
            baseDmgBonus = 0;
            critDmgRateBonus = 0.05f;
            critDmgBonus = 0.5f;
            headshotDmgBonus = 0.5f;
            pRateBonus = 0;//穿透率加成
            abeBonus = 0;//破甲效率加成
            arDmgBonus = 0;
            dmrDmgBonus = 0;
            smgDmgBonus = 0;
            sgDmgBonus = 0;
            mgDmgBonus = 0;
            srDmgBonus = 0;
            hgDmgBonus = 0;
            diamond = 0;
            coin = 0;


            EquipGunCorePropFuncs.Add(EGunCoreProp.全武器伤害加成, (value) =>
            {
                arDmgBonus += value;
                dmrDmgBonus += value;
                smgDmgBonus +=value;
                sgDmgBonus += value;
                mgDmgBonus += value;
                srDmgBonus += value;
                hgDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.全武器伤害加成, (value) =>
            {
                arDmgBonus -= value;
                dmrDmgBonus -= value;
                smgDmgBonus -= value;
                sgDmgBonus -= value;
                mgDmgBonus -= value;
                srDmgBonus -= value;
                hgDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.自动步枪伤害加成, (value) =>
            {
                arDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.自动步枪伤害加成, (value) =>
            {
                arDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.手枪伤害加成, (value) =>
            {
                hgDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.手枪伤害加成, (value) =>
            {
                hgDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.射手步枪伤害加成, (value) =>
            {
                dmrDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.射手步枪伤害加成, (value) =>
            {
                dmrDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.轻机枪伤害加成, (value) =>
            {
                mgDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.轻机枪伤害加成, (value) =>
            {
                mgDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.微型冲锋枪伤害加成, (value) =>
            {
                smgDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.微型冲锋枪伤害加成, (value) =>
            {
                smgDmgBonus -= value;
            });

            EquipGunCorePropFuncs.Add(EGunCoreProp.狙击步枪伤害加成, (value) =>
            {
                srDmgBonus += value;
            });
            RemoveGunCorePropFuncs.Add(EGunCoreProp.狙击步枪伤害加成, (value) =>
            {
                srDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.Null, null);
            RemoveSubPropFuncs.Add(ESubProp.Null, null);

            EquipSubPropFuncs.Add(ESubProp.基础伤害加成, (value) =>
            {
                baseDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.基础伤害加成, (value) =>
            {
                baseDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.生命值百分比加成, (value) =>
            {
                maxHealth += (1 + value)*maxHealth;
            });
            RemoveSubPropFuncs.Add(ESubProp.生命值百分比加成, (value) =>
            {
                maxHealth -= (1 + value) * maxHealth;
            });

            EquipSubPropFuncs.Add(ESubProp.暴击率加成, (value) =>
            {
                critDmgRateBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.暴击率加成, (value) =>
            {
                critDmgRateBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.暴击伤害加成, (value) =>
            {
                critDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.暴击伤害加成, (value) =>
            {
                critDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.爆头伤害加成, (value) =>
            {
                headshotDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.爆头伤害加成, (value) =>
            {
                headshotDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.穿透率加成, (value) =>
            {
                pRateBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.穿透率加成, (value) =>
            {
                pRateBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.破甲效率加成, (value) =>
            {
                abeBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.破甲效率加成, (value) =>
            {
                abeBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.自动步枪伤害加成, (value) =>
            {
                arDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.自动步枪伤害加成, (value) =>
            {
                arDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.精确射手步枪伤害加成, (value) =>
            {
                dmrDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.精确射手步枪伤害加成, (value) =>
            {
                dmrDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.微型冲锋枪伤害加成, (value) =>
            {
                smgDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.微型冲锋枪伤害加成, (value) =>
            {
                smgDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.霰弹枪伤害加成, (value) =>
            {
                sgDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.霰弹枪伤害加成, (value) =>
            {
                sgDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.狙击步枪伤害加成, (value) =>
            {
                srDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.狙击步枪伤害加成, (value) =>
            {
                srDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.轻机枪伤害加成, (value) =>
            {
                mgDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.轻机枪伤害加成, (value) =>
            {
                mgDmgBonus -= value;
            });

            EquipSubPropFuncs.Add(ESubProp.手枪伤害加成, (value) =>
            {
                hgDmgBonus += value;
            });
            RemoveSubPropFuncs.Add(ESubProp.手枪伤害加成, (value) =>
            {
                hgDmgBonus -= value;
            });
        }

        public void UpdatePlayerInfoToDatabase()
        {
            //改为离线后再一次性刷新
        }

        public Dictionary<EGunCoreProp, Action<float>> EquipGunCorePropFuncs = new Dictionary<EGunCoreProp, Action<float>>();
        public Dictionary<EGunCoreProp, Action<float>> RemoveGunCorePropFuncs = new Dictionary<EGunCoreProp, Action<float>>();

        public Dictionary<ESubProp, Action<float>> EquipSubPropFuncs = new Dictionary<ESubProp, Action<float>>();
        public Dictionary<ESubProp, Action<float>> RemoveSubPropFuncs = new Dictionary<ESubProp, Action<float>>();

        public Dictionary<EEquipmentTalent, Action> EquipEquipmentTalentFuncs = new Dictionary<EEquipmentTalent, Action>();
    }
}
