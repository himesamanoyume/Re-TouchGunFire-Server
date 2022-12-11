using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Gaming
{
    internal class GameFunction
    {
        //只更新玩家信息
        public MainPack UpdatePlayerInfo(MainPack mainPack, Client client)
        {
            try
            {
                UpdatePlayerInfoPack updatePlayerInfoPack = new UpdatePlayerInfoPack();
                updatePlayerInfoPack.Uid = client.PlayerInfo.Uid;
                updatePlayerInfoPack.PlayerName = client.PlayerInfo.PlayerName;
                updatePlayerInfoPack.Level = client.PlayerInfo.Level;
                updatePlayerInfoPack.MaxHealth = client.PlayerInfo.MaxHealth;
                updatePlayerInfoPack.CurrentHealth = client.PlayerInfo.CurrentHealth;
                updatePlayerInfoPack.MaxArmor = client.PlayerInfo.MaxArmor;
                updatePlayerInfoPack.CurrentArmor = client.PlayerInfo.CurrentArmor;
                updatePlayerInfoPack.MaxExp = client.PlayerInfo.MaxExp;
                updatePlayerInfoPack.CurrentExp = client.PlayerInfo.CurrentExp;
                updatePlayerInfoPack.Diamond = client.PlayerInfo.Diamond;
                updatePlayerInfoPack.Coin = client.PlayerInfo.Coin;
                updatePlayerInfoPack.BaseDmgRateBonus = client.PlayerInfo.BaseDmgBonus;
                updatePlayerInfoPack.CritDmgRateBonus = client.PlayerInfo.CritDmgRateBonus;
                updatePlayerInfoPack.CritDmgBonus = client.PlayerInfo.CritDmgBonus;
                updatePlayerInfoPack.HeadshotDmgBonus = client.PlayerInfo.HeadshotDmgRateBonus;
                updatePlayerInfoPack.PRateBonus = client.PlayerInfo.PRateBonus;
                updatePlayerInfoPack.AbeBonus = client.PlayerInfo.AbeBonus;
                updatePlayerInfoPack.ArDmgBonus = client.PlayerInfo.ArDmgBonus;
                updatePlayerInfoPack.DmrDmgBonus = client.PlayerInfo.DmrDmgBonus;
                updatePlayerInfoPack.SmgDmgBonus = client.PlayerInfo.SmgDmgBonus;
                updatePlayerInfoPack.SgDmgBonus = client.PlayerInfo.SgDmgBonus;
                updatePlayerInfoPack.MgDmgBonus = client.PlayerInfo.MgDmgBonus;
                updatePlayerInfoPack.SrDmgBonus = client.PlayerInfo.SrDmgBonus;
                updatePlayerInfoPack.HgDmgBonus = client.PlayerInfo.HgDmgBonus;
                if (client.team!=null)
                {
                    updatePlayerInfoPack.TeamMasterUid = client.team.GetTeamMasterClient.PlayerInfo.Uid;
                }
                
                mainPack.UpdatePlayerInfoPack.Add(updatePlayerInfoPack);

                if (client.IsInTheTeam)
                {
                    foreach (var c in client.team.Teammates)
                    {
                        if (c.PlayerInfo.Uid == client.PlayerInfo.Uid)
                        {
                            continue;
                        }
                        UpdatePlayerInfoPack updatePlayerInfoPack1 = new UpdatePlayerInfoPack();
                        updatePlayerInfoPack1.Uid = c.PlayerInfo.Uid;
                        updatePlayerInfoPack1.PlayerName = c.PlayerInfo.PlayerName;
                        updatePlayerInfoPack1.Level = c.PlayerInfo.Level;
                        updatePlayerInfoPack1.MaxHealth = c.PlayerInfo.MaxHealth;
                        updatePlayerInfoPack1.CurrentHealth = c.PlayerInfo.CurrentHealth;
                        updatePlayerInfoPack1.MaxArmor = c.PlayerInfo.MaxArmor;
                        updatePlayerInfoPack1.CurrentArmor = c.PlayerInfo.CurrentArmor;
                        mainPack.UpdatePlayerInfoPack.Add(updatePlayerInfoPack1);
                    }
                }
                return mainPack;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return null;
            }
        }

        public bool Regeneration(MainPack mainPack, Client client)
        {
            try
            {
                if (mainPack.Uid == client.PlayerInfo.Uid)
                {
                    if (client.PlayerInfo.CurrentHealth < client.PlayerInfo.MaxHealth && client.PlayerInfo.CurrentHealth > 0)
                    {
                        float hp = (client.PlayerInfo.MaxHealth / 20) / 10;
                        client.PlayerInfo.CurrentHealth += hp;
                    }
                    if (client.PlayerInfo.CurrentArmor < client.PlayerInfo.MaxArmor && client.PlayerInfo.CurrentArmor > 0)
                    {
                        float armor = (client.PlayerInfo.MaxArmor / 40) / 10;
                        client.PlayerInfo.CurrentArmor += armor;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }

        public bool StartAttack(MainPack mainPack, Client client)
        {
            try
            {

                if (client.IsInTheTeam)
                {
                    client.team.Broadcast(client, mainPack);
                }
                mainPack.ReturnCode = ReturnCode.Success;
                client.EnemiesManager.InitAttackArea(mainPack.AttackAreaPack.AreaNumber);
                client.TcpSend(mainPack);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                mainPack.ReturnCode = ReturnCode.Fail;
                client.TcpSend(mainPack);
                return false;
            }
        }

        public bool AttackLeave(MainPack mainPack, Client client)
        {
            try
            {
                if (client.IsInTheTeam)
                {
                    MainPack mainPack1 = new MainPack();
                    mainPack1.Uid = mainPack.Uid;
                    mainPack1.ActionCode = ActionCode.LeaveTeam;
                    mainPack1.RequestCode = RequestCode.Team;
                    client.LeaveTeam(mainPack1);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }

        public bool UpdateAttackingInfo(MainPack mainPack, Client client)
        {
            try
            {
                if (mainPack.Uid == client.PlayerInfo.Uid)
                {
                    AttackAreaPack attackAreaPack = new AttackAreaPack();
                    mainPack.AttackAreaPack = attackAreaPack;
                    if (client.EnemiesManager.attackArea.currentWave == -1)
                    {
                        mainPack.AttackAreaPack.Wave = 0;
                    }
                    else
                    {
                        mainPack.AttackAreaPack.Wave = client.EnemiesManager.attackArea.currentWave;
                    }
                    mainPack.AttackAreaPack.AreaNumber = client.EnemiesManager.attackArea.areaNumber;

                    foreach (EnemyInfo item in client.EnemiesManager.attackArea.currentWaveEnemiesDict.Values)
                    {
                        EnemyPack enemyPack = new EnemyPack();
                        enemyPack.EnemyName = item.EnemyName;
                        enemyPack.Floor = (int)item.Floor;
                        enemyPack.Pos = (int)item.Pos;
                        enemyPack.MaxHealth = item.MaxHealth;
                        enemyPack.MaxArmor = item.MaxArmor;
                        enemyPack.CurrentHealth = item.CurrentHealth;
                        enemyPack.CurrentArmor = item.CurrentArmor;
                        mainPack.AttackAreaPack.EnemyPack.Add(enemyPack);
                    }
                    if (client.IsInTheTeam)
                    {
                        MainPack mainPack1 = new MainPack();
                        mainPack1.ActionCode = mainPack.ActionCode;
                        mainPack1.RequestCode = mainPack.RequestCode;
                        mainPack1.AttackAreaPack = mainPack.AttackAreaPack;
                        client.team.Broadcast(client, mainPack1, true);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                

            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }

        public bool HitReg(MainPack mainPack, Client client)
        {
            try
            {
                mainPack.HitRegPack.Damage = client.CalcDamage((EFloor)mainPack.HitRegPack.Floor, (EFloorPos)mainPack.HitRegPack.Pos, mainPack.HitRegPack.IsMainGun, mainPack.HitRegPack.IsStrike);
                if (client.IsInTheTeam)
                {
                    client.team.Broadcast(client, mainPack, true);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
                return false;
            }
        }
    }
}
