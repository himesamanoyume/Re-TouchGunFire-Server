using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class EnemiesManager
    {
        EnemyInfo[] enemiesType1 = new EnemyInfo[] {
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_1, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_3, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_2, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_4, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_1, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_5, 2000, 500),
        };

        EnemyInfo[] enemiesType2 = new EnemyInfo[] {
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_1, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_3, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_5, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_2, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_4, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_5, 2000, 500),
        };

        EnemyInfo[] enemiesType3 = new EnemyInfo[] {
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_1, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_5, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_2, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_1, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_5, 2000, 500),
        };

        EnemyInfo[] enemiesType4 = new EnemyInfo[] {
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos1_1, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos2_2, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos3_5, 2000, 500),
        };

        EnemyInfo[] enemiesType5 = new EnemyInfo[] {
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_1, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos1_3, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos1_5, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos2_2, 2000, 500),
            new EnemyInfo(EFloor.Floor2, EFloorPos.Pos2_4, 2000, 500),
            new EnemyInfo(EFloor.Floor1, EFloorPos.Pos3_1, 2000, 500),
            new EnemyInfo(EFloor.Floor3, EFloorPos.Pos3_5, 2000, 500),
        };

        public AttackArea attackArea;
        List<EnemyInfo[]> attackArea1List;
        public EnemiesManager()
        {
            attackArea1List = new List<EnemyInfo[]>();
            attackArea1List.Add(enemiesType1);
            attackArea1List.Add(enemiesType5);
            attackArea1List.Add(enemiesType4);
            
        }

        public void InitAttackArea(int areaNumber)
        {
            switch (areaNumber)
            {
                case 1:
                    attackArea = new AttackArea(attackArea1List, 1);
                    break;
            }
            attackArea.NextWave();

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

        public void NextWave()
        {
            if (currentWaveEnemiesDict!=null)
            {
                currentWaveEnemiesDict.Clear();
            }
            currentWave++;
            foreach (EnemyInfo item in enemyWaves[currentWave])
            {
                currentWaveEnemiesDict.Add((int)item.Floor * 100 + (int)item.Pos, item);
            }
        }
    }

}
