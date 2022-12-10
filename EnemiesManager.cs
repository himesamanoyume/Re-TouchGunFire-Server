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

        public AttackArea attackArea1;
        
        public EnemiesManager()
        {
            List<EnemyInfo[]> attackArea1List = new List<EnemyInfo[]>();
            attackArea1List.Add(enemiesType1);
            attackArea1List.Add(enemiesType5);
            attackArea1List.Add(enemiesType4);
            attackArea1 = new AttackArea(attackArea1List);
        }

    }

    public class AttackArea
    {
        public List<EnemyInfo[]> enemyWaves;

        public AttackArea(List<EnemyInfo[]> enemyWaves)
        {
            this.enemyWaves = enemyWaves;
        }
    }

}
