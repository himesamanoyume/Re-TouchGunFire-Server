using System;
using System.Net;
using System.Net.Sockets;
using SocketProtocol;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using SocketServer.Friend;
using SocketServer.User;
using SocketServer.Utils;
using SocketServer.Teammate;
using SocketServer.Gaming;
using SocketServer.Items;
using Newtonsoft.Json;

namespace SocketServer
{
    internal class Client
    {
        Socket tcpSocket;
        Socket udpSocket;
        public EndPoint endPoint;
        UdpServer udpServer;
        Message message;
        UserFunction userFunction;
        FriendFunction friendFunction;
        TeamFunction teamFunction;
        GameFunction gameFunction;
        public bool IsInTheTeam = false;
        public Team team = null;
        Server server;
        MySqlConnection connection;
        
        string connectStr = "database=hime; data source=47.106.183.112; user=lzp; password=lzp19990510; pooling=false;charset=utf8;port=3306";

        public UserFunction GetUserFunction
        {
            get { return userFunction; }
        }

        public FriendFunction GetFriendFunction
        {
            get { return friendFunction; }
        }

        public TeamFunction GetTeamFunction
        {
            get { return teamFunction; }
        }

        public GameFunction GetGameFunction
        {
            get { return gameFunction; }
        }

        public PlayerInfo PlayerInfo;
        public ItemController ItemController;
        public EnemiesManager EnemiesManager;
        public delegate void Buff(PlayerInfo playerInfo);
        public Buff buff;

        public void InitDefaultPlayerInfo()
        {
            PlayerInfo defaultPlayerInfo = new PlayerInfo();
            buff?.Invoke(defaultPlayerInfo);
            PlayerInfo = defaultPlayerInfo;
        }

        public void ChangePlayerInfoBuff()
        {
            buff?.Invoke(PlayerInfo);
        }

        public Client(Socket clientSocket, Server server, UdpServer udpServer)
        {
            userFunction = new UserFunction();
            friendFunction = new FriendFunction();
            teamFunction = new TeamFunction();
            gameFunction = new GameFunction();
            message = new Message();
            connection = new MySqlConnection(connectStr);
            connection.Open();
            PlayerInfo = new PlayerInfo();
            ItemController = new ItemController(PlayerInfo);
            EnemiesManager = new EnemiesManager();
            this.udpServer = udpServer;
            this.server = server;
            tcpSocket = clientSocket;
            //ItemController.InitEquipmentInfo();
            //ItemController.InitPlayerGunInfo();
            StartReceive();
        }

        void StartReceive()
        {
            tcpSocket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
        }

        public float CalcDamage(EFloor floor, EFloorPos floorPos, bool isMainGun, bool isStrike, out bool isHeadshot, out bool isCrit)
        {

            EnemyInfo enemyInfo = IsInTheTeam == true? team.GetTeamMasterClient.EnemiesManager.GetEnemy(floor, floorPos): EnemiesManager.GetEnemy(floor, floorPos);

            if (isMainGun)
            {
                GunInfo mainGun = ItemController.mainGunInfo;
                switch (mainGun.ItemType)
                {
                    case "AR":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.ArDmgBonus, isStrike, out isHeadshot, out isCrit);
                    case "DMR":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.DmrDmgBonus, isStrike, out isHeadshot, out isCrit);
                    case "SMG":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.SmgDmgBonus, isStrike, out isHeadshot, out isCrit);
                    case "SG":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.SgDmgBonus, isStrike, out isHeadshot, out isCrit);
                    case "MG":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.MgDmgBonus, isStrike, out isHeadshot, out isCrit);
                    case "SR":
                        return SubCalcDamage(mainGun, enemyInfo, PlayerInfo.SrDmgBonus, isStrike, out isHeadshot, out isCrit);
                }
                isCrit = false;
                isHeadshot = false;
                return 0;
            }
            else
            {
                GunInfo handGun = ItemController.handGunInfo;
                return SubCalcDamage(handGun, enemyInfo, PlayerInfo.HgDmgBonus, isStrike, out isHeadshot, out isCrit);
            }
        }

        void AttackEnd(MainPack mainPack)
        {
            GetGameFunction.AttackEnd(mainPack, this);
        }

        void BeatEnemy(EnemyInfo enemyInfo)
        {
            EFloor eFloor = enemyInfo.Floor;
            EFloorPos eFloorPos = enemyInfo.Pos;
            
            if (IsInTheTeam == true ? team.GetTeamMasterClient.EnemiesManager.BeatEnemy(eFloor, eFloorPos) : EnemiesManager.BeatEnemy(eFloor, eFloorPos))
            {
                MainPack mainPack = new MainPack();
                mainPack.Uid = PlayerInfo.Uid;
                mainPack.ActionCode = ActionCode.BeatEnemy;
                mainPack.RequestCode = RequestCode.Gaming;
                mainPack.ReturnCode = ReturnCode.Success;
                HitRegPack hitRegPack = new HitRegPack();
                hitRegPack.HitSenderUid = PlayerInfo.Uid;
                hitRegPack.Floor = (int)eFloor;
                hitRegPack.Pos = (int)eFloorPos;
                hitRegPack.IsDead = true;
                mainPack.HitRegPack = hitRegPack;
                GetGameFunction.BeatEnemy(mainPack, this);
                if (IsInTheTeam == true ? team.GetTeamMasterClient.EnemiesManager.CheckCurrentWave(): EnemiesManager.CheckCurrentWave())
                {
                    //战斗结束
                    MainPack mainPack1 = new MainPack();
                    mainPack1.Uid = PlayerInfo.Uid;
                    mainPack1.ActionCode = ActionCode.AttackEnd;
                    mainPack1.RequestCode = RequestCode.Gaming;
                    mainPack1.ReturnCode = ReturnCode.Success;
                    AttackAreaPack attackAreaPack = new AttackAreaPack();
                    attackAreaPack.IsEnd = true;
                    mainPack1.AttackAreaPack = attackAreaPack;
                    AttackEnd(mainPack1);
                }
            }
        }

        float CalcDmg3(float gunDmg2, EnemyInfo enemyInfo, bool isStrike, out bool isHeadshot, out bool isCrit)
        {
            //是否爆头
            Random random = new Random();
            float r1 = (float)random.NextDouble();
            if (r1 <= 0.2f && r1 >= 0)
            {
                isHeadshot = true;
                return CalcDmg4(gunDmg2 * (1 + PlayerInfo.HeadshotDmgRateBonus), enemyInfo, isStrike, out isCrit);
            }
            else
            {
                isHeadshot = false;
                return CalcDmg4(gunDmg2, enemyInfo, isStrike, out isCrit);

            }
        }

        float CalcDmg4(float gunDmg3, EnemyInfo enemyInfo, bool isStrike, out bool isCrit)
        {
            if (enemyInfo.CurrentArmor > 0)
            {
                return CalcDmg5(gunDmg3 * (1 + PlayerInfo.AbeBonus), isStrike, out isCrit);
            }
            else
            {
                return CalcDmg5(gunDmg3, isStrike, out isCrit);
            }
        }

        float CalcDmg5(float gunDmg4, bool isStrike, out bool isCrit)
        {
            if (isStrike)
            {
                return CalcFinDmg(gunDmg4 * (0.5f + PlayerInfo.PRateBonus), out isCrit);
            }
            else
            {
                return CalcFinDmg(gunDmg4, out isCrit);
            }
        }

        float CalcFinDmg(float gunDmg5, out bool isCrit)
        {
            //是否暴击
            Random random = new Random();
            float r2 = (float)random.NextDouble();
            if (r2 <= PlayerInfo.CritDmgBonus && r2 >= 0)
            {
                //最终伤害
                isCrit = true;
                return gunDmg5 * (1 + PlayerInfo.CritDmgBonus);
                
            }
            else
            {
                //最终伤害
                isCrit=false;
                return gunDmg5;
            }
        }

        float SubCalcDamage(GunInfo gunInfo, EnemyInfo enemyInfo, float dmgBonus, bool isStrike, out bool isHeadshot, out bool isCrit)
        {

            //武器基础伤害*基础伤害加成
            float gunDmg1 = gunInfo.BaseDmg * (1 + PlayerInfo.BaseDmgBonus);
            //武器基础伤害*对应武器伤害加成
            float gunDmg2 = gunDmg1 * (1 + dmgBonus);
            //是否爆头 固定50%几率
            float dmgFin = CalcDmg3(gunDmg2, enemyInfo, isStrike, out isHeadshot, out isCrit);
            if (enemyInfo.HitToken(dmgFin))
            {
                BeatEnemy(enemyInfo);
            }
            return dmgFin; 
        }

        bool isFirst = true;
        byte[] bigBuffer;
        int progress = 0;

        void ReceiveCallback(IAsyncResult iar)
        {
            try
            {
                if (tcpSocket == null || tcpSocket.Connected == false) return;
                int bufferSize = tcpSocket.EndReceive(iar);
                if (isFirst)
                {
                    message.InitTotalDataSize();
                }

                if (message.TotalDataSize <= 1020)
                {
                    if (bufferSize == 0)
                    {
                        Close();
                        return;
                    }

                    message.ReadBuffer(bufferSize, HandleRequest);
                }
                else
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        int dataSize = message.TotalDataSize;
                        bigBuffer = new byte[dataSize + 4];
                    }

                    if (bufferSize > 0)
                    {
                        Array.Copy(message.Buffer, 0, bigBuffer, progress, bufferSize);
                        progress += bufferSize;
                        if (progress >= message.TotalDataSize + 4)
                        {
                            message.ReadBigBuffer(bigBuffer, HandleRequest);
                            progress = 0;
                            isFirst = true;
                            bigBuffer = null;
                        }
                    }
                }
                StartReceive();
            }
            catch
            {
                Close();
            }
        }

        //public bool isReadyAttack = false;

        public bool Register(MainPack mainPack)
        {
            return GetUserFunction.Reigster(mainPack, connection, ItemController);
        }

        public bool HitReg(MainPack mainPack)
        {
            return GetGameFunction.HitReg(mainPack, this);
        }

        public MainPack Login(MainPack mainPack)
        {
            MainPack mainPack1 = GetUserFunction.Login(mainPack, connection);
            PlayerInfo.Uid = mainPack1.Uid;
            server.AddClientToDict(this);
            return mainPack1;
        }

        public MainPack InitPlayerInfo(MainPack mainPack)
        {
            mainPack = GetUserFunction.InitPlayerInfo(mainPack, connection, ItemController);
            PlayerInfo.Uid = mainPack.PlayerInfoPack.Uid;
            PlayerInfo.PlayerName = mainPack.PlayerInfoPack.PlayerName;
            PlayerInfo.Level = mainPack.PlayerInfoPack.Level;
            PlayerInfo.CurrentExp = mainPack.PlayerInfoPack.CurrentExp;
            PlayerInfo.Diamond = mainPack.PlayerInfoPack.Diamond;
            PlayerInfo.Coin = mainPack.PlayerInfoPack.Coin;
           
            return mainPack;
        }

        public int UnlockItemSubProp(MainPack mainPack)
        {
            return GetUserFunction.UnlockItemSubProp(mainPack, this, connection);
        }

        public bool RefreshItemSubProp(MainPack mainPack)
        {
            return GetUserFunction.RefreshItemSubProp(mainPack, this, connection);
        }

        public bool RefreshGunCoreProp(MainPack mainPack)
        {
            return GetUserFunction.RefreshGunCoreProp(mainPack, this, connection);
        }

        public int Shopping(MainPack mainPack)
        {
            return GetUserFunction.Shopping(mainPack, this, connection);
        }

        public bool StartAttack(MainPack mainPack)
        {
            return GetGameFunction.StartAttack(mainPack, this);
        }

        public bool AttackInvite(MainPack mainPack)
        {
            return GetTeamFunction.AttackInvite(mainPack, team, this);
        }

        public int AttackInvited(MainPack mainPack)
        {
            return GetTeamFunction.AttackInvited(mainPack, server, this, team);
        }

        public bool RefusedAttack(MainPack mainPack)
        {
            return GetTeamFunction.RefusedAttack(mainPack, this);
        }

        public bool RefuseAttack(MainPack mainPack)
        {
            return GetTeamFunction.RefuseAttack(mainPack, this);
        }

        public bool AcceptedAttackInvite(MainPack mainPack)
        {
            return GetTeamFunction.AcceptedAttackInvite(mainPack, this);
        }

        public bool GetItemInfo()
        {
            return GetUserFunction.GetItemInfo(this);
        }

        public int EquipItem(MainPack mainPack)
        {
            return GetUserFunction.EquipItem(mainPack, this, connection);
        }

        public MainPack UpdatePlayerInfo(MainPack mainPack)
        {
            return GetGameFunction.UpdatePlayerInfo(mainPack, this);
        }

        public bool Regeneration(MainPack mainPack)
        {
            return GetGameFunction.Regeneration(mainPack, this);
        }

        public int SendRequestFriend(MainPack mainPack)
        {
            return GetFriendFunction.SendRequestFriend(mainPack, connection);
        }

        public MainPack SearchFriend(MainPack mainPack)
        {
            return GetFriendFunction.SearchFriend(mainPack, connection);
        }

        public MainPack GetFriends(MainPack mainPack)
        {
            return GetFriendFunction.GetFriends(mainPack, connection);
        }

        public MainPack GetFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.GetFriendRequest(mainPack, connection);
        }

        public int AcceptFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.AcceptFriendRequest(mainPack, connection);
        }

        public MainPack GetPlayerBaseInfo(MainPack mainPack)
        {
            mainPack = GetUserFunction.GetPlayerBaseInfo(mainPack, connection);
            Client client = server.GetClientFromDictByUid(mainPack.PlayerInfoPack.Uid);
            if (client == null)
            {
                mainPack.PlayerInfoPack.IsOnline = false;
                mainPack.PlayerInfoPack.IsTeam = false;
            }
            else
            {
                if (client.IsInTheTeam)
                {
                    if (client.team == team)
                    {
                        if (client.team.GetTeamMasterClient.PlayerInfo.Uid == mainPack.PlayerInfoPack.Uid)
                        {
                            mainPack.PlayerInfoPack.IsTeamMaster = true;
                        }
                        else
                        {
                            mainPack.PlayerInfoPack.IsTeamMaster = false;
                        }
                        mainPack.PlayerInfoPack.IsSameTeam = true;
                    }
                    else
                    {
                        mainPack.PlayerInfoPack.IsSameTeam = false;
                    }
                    mainPack.PlayerInfoPack.IsTeam = true;
                }
                else
                {
                    mainPack.PlayerInfoPack.IsTeam = false;
                }
                mainPack.PlayerInfoPack.IsOnline = true;
            }
            return mainPack;
        }

        public bool ReadyAttack(MainPack mainPack)
        {
            return GetGameFunction.ReadyAttack(mainPack, this);
        }

        public bool CancelReadyAttack(MainPack mainPack)
        {
            return GetGameFunction.CancelReadyAttack(mainPack, this);
        }

        public bool TeamMasterAttackNotify(MainPack mainPack)
        {
            return GetTeamFunction.TeamMasterAttackNotify(mainPack, this);
        }

        public int RefuseFriendRequest(MainPack mainPack)
        {
            return GetFriendFunction.RefuseFriendRequest(mainPack, connection);
        }

        public bool DeleteFriend(MainPack mainPack)
        {
            return GetFriendFunction.DeleteFriend(mainPack, connection);
        }

        public int InviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.InviteTeam(mainPack, server, server.GetClientFromDictByUid(mainPack.TeammatePack.TargetUid).team);
        }

        public int InvitedTeam(MainPack mainPack)//被邀请者
        {
            return GetTeamFunction.InvitedTeam(mainPack, server, this, team);
        }

        public bool AcceptInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptInviteTeam(mainPack, server);
        }

        public bool AcceptedInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptedInviteTeam(mainPack, this, udpServer, server);
        }

        public bool RefuseInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefuseInviteTeam(mainPack, this, server);
        }

        public bool RefusedInviteTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefusedInviteTeam(mainPack, this);
        }

        public MainPack GetTeammates(MainPack mainPack)
        {
            return GetTeamFunction.GetTeammates(mainPack, this);
        }

        public bool UpdateAttackingInfo(MainPack mainPack)
        {
            return GetGameFunction.UpdateAttackingInfo(mainPack, this);
        }

        public MainPack LeaveTeam(MainPack mainPack)
        {
            return GetTeamFunction.LeaveTeam(mainPack, this);
        }

        public bool AttackLeave(MainPack mainPack)
        {
            return GetGameFunction.AttackLeave(mainPack, this);
        }

        public int JoinTeamRequest(MainPack mainPack)
        {
            return GetTeamFunction.JoinTeamRequest(mainPack, server, this);
        }

        public int PlayerJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.PlayerJoinTeam(mainPack, server, this);
        }

        public bool AcceptJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptJoinTeam(mainPack, server, this);
        }

        public bool AcceptedJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.AcceptedJoinTeam(mainPack, this, server);
        }

        public bool RefuseJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefuseJoinTeam(mainPack, this, server);
        }

        public bool RefusedJoinTeam(MainPack mainPack)
        {
            return GetTeamFunction.RefusedJoinTeam(mainPack, this);
        }

        public bool KickPlayer(MainPack mainPack)
        {
            return GetTeamFunction.KickPlayer(mainPack, this, server);
        }

        public void TcpSend(MainPack mainPack)
        {
            if (tcpSocket == null || tcpSocket.Connected == false) return;
            try
            {
                Debug.Log(new StackFrame(true), "TCP发送至UID:"+ mainPack.Uid +",ActionCode:"+ mainPack.ActionCode);
                byte[] temp = Message.TcpPackData(mainPack);
                tcpSocket.Send(temp);
            }
            catch(Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
            }
        }

        public void UdpSend(MainPack mainPack)
        {
            if (endPoint == null) return;
            try
            {
                //Debug.Log(new StackFrame(true), "UDP发送:"+ mainPack.Uid + ",ActionCode:" + mainPack.ActionCode);
                udpServer.UdpSend(mainPack, endPoint);
            }
            catch (Exception e)
            {
                Debug.Log(new StackFrame(true), e.Message);
            }
        }

        void HandleRequest(MainPack mainPack)
        {
            server.HandleRequest(mainPack, this);
        }

        void Close()
        {
            server.RemoveClient(this);
            Debug.Log(new StackFrame(true), "断开连接");
            if (IsInTheTeam && team != null)
            {
                MainPack mainPack = new MainPack();
                mainPack.Uid = PlayerInfo.Uid;
                mainPack.ActionCode = ActionCode.LeaveTeam;
                GetTeamFunction.LeaveTeam(mainPack, this);
            }
            tcpSocket.Close();
            connection.Close();
        }
    }
}
