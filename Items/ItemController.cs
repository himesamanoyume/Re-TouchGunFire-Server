using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public partial class ItemController
    {
        private MainGunInfo mainGunInfo;
        private HandGunInfo handGunInfo;
        private ArmorInfo armorInfo;
        private HeadInfo headInfo;
        private HandInfo handInfo;
        private KneeInfo kneeInfo;
        private LegInfo legInfo;
        private BootsInfo bootsInfo;

        private Dictionary<int, ItemInfo> itemsDict = new Dictionary<int, ItemInfo>();

        public void AddItemInfo(int uid, ItemInfo itemInfo)
        {
            itemsDict.Add(uid, itemInfo);
        }

        public void RemoveItemInfo(int uid)
        {
            itemsDict.Remove(uid);
        }

        //对字典物品转json,再转进数据库

        //end

        //数据库转json再转成类

        //end

        public void SetMainGun(EGunUid eGunUid)
        {
            if (itemsDict.TryGetValue((int)eGunUid, out ItemInfo itemInfo))
            {
                if (mainGunInfo != null)
                {
                    mainGunInfo.OnRemove();
                }
                mainGunInfo = (MainGunInfo)itemInfo;
                mainGunInfo.OnEquip();
            }
        }

        public void SetHandGun(EGunUid eGunUid)
        {
            if (itemsDict.TryGetValue((int)eGunUid, out ItemInfo itemInfo))
            {
                if (handGunInfo != null)
                {
                    handGunInfo.OnRemove();
                }
                handGunInfo = (HandGunInfo)itemInfo;
                handGunInfo.OnEquip();
            }
        }
    }
}
