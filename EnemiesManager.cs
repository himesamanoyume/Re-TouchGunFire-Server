﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class EnemiesManager
    {

        public AttackArea attackArea;
        List<EnemyInfo[]> attackArea1List;

        //public enum EEnemySpawnType
        //{
        //    Mercenary,
        //    SeniorOfficer,
        //    BOSS
        //}

        /// <summary>
        /// 佣兵
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxArmor"></param>
        /// <param name="attack"></param>
        /// <param name="name"></param>
        void EnemySpawnMercenaryType(out float maxHealth, out float maxArmor, out float attack, out string name)
        {
            maxHealth = 2000;
            maxArmor = 500;
            attack = 10;
            name = "佣兵";
        }

        /// <summary>
        /// 高级军官
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxArmor"></param>
        /// <param name="attack"></param>
        /// <param name="name"></param>
        void EnemySpawnSeniorOfficerType(out float maxHealth, out float maxArmor, out float attack, out string name)
        {
            maxHealth = 5000;
            maxArmor = 2500;
            attack = 30;
            name = "高级军官";
        }

        /// <summary>
        /// BOSS
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="maxArmor"></param>
        /// <param name="attack"></param>
        /// <param name="name"></param>
        void EnemySpawnBOSSType(out float maxHealth, out float maxArmor, out float attack, out string name)
        {
            maxHealth = 8000;
            maxArmor = 7000;
            attack = 50;
            name = "BOSS";
        }

        #region InitAttackAreaList
        void InitAttackArea1List()
        {
            attackArea1List = new List<EnemyInfo[]>();
            attackArea1List.Add(InitEnemiesType4());
            attackArea1List.Add(InitEnemiesType5());
            attackArea1List.Add(InitEnemiesType4());
        }
        #endregion
        #region InitEnemiesType
        EnemyInfo[] InitEnemiesType1()
        {
            EnemyInfo[] enemiesType = new EnemyInfo[] {
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_3, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_2, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_4, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_5, EnemySpawnMercenaryType),
            };
            return enemiesType;
        }
        EnemyInfo[] InitEnemiesType2()
        {
            EnemyInfo[] enemiesType = new EnemyInfo[] {
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_3, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_5, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_2, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_4, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_5, EnemySpawnMercenaryType),
            };
            return enemiesType;
        }
        EnemyInfo[] InitEnemiesType3()
        {
            EnemyInfo[] enemiesType = new EnemyInfo[] {
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_5, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_2, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_1, EnemySpawnSeniorOfficerType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_5, EnemySpawnMercenaryType),
            };
            return enemiesType;
        }
        EnemyInfo[] InitEnemiesType4()
        {
            EnemyInfo[] enemiesType = new EnemyInfo[] {
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_1, EnemySpawnSeniorOfficerType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_2, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_5, EnemySpawnMercenaryType),
            };
            return enemiesType;
        }
        EnemyInfo[] InitEnemiesType5()
        {
            EnemyInfo[] enemiesType = new EnemyInfo[] {
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_2, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, EnemySpawnBOSSType),
                new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, EnemySpawnMercenaryType),
                new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_5, EnemySpawnMercenaryType),
            };
            return enemiesType;
        }
        #endregion

        public void InitAttackArea(int areaNumber)
        {
            switch (areaNumber)
            {
                case 1:
                    InitAttackArea1List();
                    attackArea = new AttackArea(attackArea1List, 1);
                    break;
            }
            attackArea.NextWave();
        }

        //用于检测当前波次的怪物是否全部被击杀
        public bool CheckCurrentWave()
        {
            if (attackArea.currentWaveEnemiesDict.Count == 0)
            {
                if (attackArea.NextWave())
                {
                    attackArea = null;
                    return true;
                }
            }
            return false;
        }

        public bool BeatEnemy(EFloor floor, EFloorPos floorPos)
        {
            try
            {
                if (attackArea.currentWaveEnemiesDict.TryGetValue((int)floor * 100 + (int)floorPos, out EnemyInfo enemyInfo))
                {
                    enemyInfo = null;
                    attackArea.currentWaveEnemiesDict.Remove((int)floor * 100 + (int)floorPos);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public EnemyInfo GetEnemy(EFloor floor, EFloorPos floorPos)
        {
            if (attackArea.currentWaveEnemiesDict.TryGetValue((int)floor * 100 + (int)floorPos, out EnemyInfo enemyInfo))
            {
                return enemyInfo;
            }
            else
            {
                return null;
            }
        }
    }

    public class AttackArea
    {
        public List<EnemyInfo[]> enemyWaves;
        public Dictionary<int, EnemyInfo> currentWaveEnemiesDict = new Dictionary<int, EnemyInfo>();
        public int areaNumber;
        public int currentWave = -1;

        public AttackArea(List<EnemyInfo[]> enemyWaves, int areaNumber)
        {
            this.enemyWaves = enemyWaves;
            this.areaNumber = areaNumber;
        }

        public bool NextWave()
        {
            if (currentWaveEnemiesDict!=null)
            {
                currentWaveEnemiesDict.Clear();
            }
            if (currentWave >= enemyWaves.Count - 1)
            {
                return true;
            }
            currentWave++;
            foreach (EnemyInfo item in enemyWaves[currentWave])
            {
                currentWaveEnemiesDict.Add((int)item.Floor * 100 + (int)item.Pos, item);
            }
            return false;
        }
    }

}
