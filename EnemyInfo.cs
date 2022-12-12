using SocketProtocol;

using SocketServer.Teammate;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class EnemyInfo
    {
        EFloor floor;
        EFloorPos pos;
        string enemyName;
        float currentHealth;
        float maxHealth;
        float currentArmor;
        float maxArmor;

        public EFloor Floor { get => floor; set => floor = value; }
        public EFloorPos Pos { get => pos; set => pos = value; }
        public string EnemyName { get => enemyName; set => enemyName = value; }
        public float CurrentHealth { 
            get => currentHealth;
            set
            {
                if (value>=MaxHealth)
                {
                    currentHealth = maxHealth;
                }
                else if(value <= 0)
                {
                    currentHealth = 0;
                }
                else
                {
                    currentHealth = value;
                }
            }
        }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float CurrentArmor { 
            get => currentArmor;
            set
            {
                if (value >= MaxArmor)
                {
                    currentArmor = maxArmor;
                }
                else if (value <= 0)
                {
                    currentArmor = 0;
                }
                else
                {
                    currentArmor = value;
                }
            }
        }
        public float MaxArmor { get => maxArmor; set => maxArmor = value; }

        public EnemyInfo(EFloor floor, EFloorPos pos, float maxHealth, float maxArmor, string name = "佣兵")
        {
            this.floor = floor;
            this.pos = pos;
            this.maxHealth = maxHealth;
            this.maxArmor = maxArmor;
            currentHealth = this.maxHealth;
            currentArmor = this.maxArmor;
            enemyName = name;
        }

        public bool HitToken(float dmg)
        {
            if (dmg>= CurrentArmor)
            {
                dmg -= CurrentArmor;
                CurrentArmor = 0;
                CurrentHealth -= dmg;
            }
            else if(dmg <= CurrentArmor)
            {
                CurrentArmor -= dmg;
            }else
            {
                CurrentHealth -= dmg;
            }
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public enum EFloor
    {
        Null,
        Floor1,
        Floor2,
        Floor3
    }

    public enum EFloorPos
    {
        Null,
        Pos1_1,
        Pos1_2,
        Pos1_3,
        Pos1_4,
        Pos1_5,
        Pos2_1,
        Pos2_2,
        Pos2_3,
        Pos2_4,
        Pos2_5,
        Pos3_1,
        Pos3_2,
        Pos3_3,
        Pos3_4,
        Pos3_5
    }
}
