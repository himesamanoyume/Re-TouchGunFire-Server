using SocketProtocol;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Items
{
    public partial class ItemController
    {
        public ItemController()
        {
            MainGunInfo AK47 = new MainGunInfo(this);
            AK47.Uid = EGunUid.AK47;
            AK47.GunName = EGunName.AK47;
            AK47.GunType = EGunType.AR;
            AK47.BaseDMG = 100f;
            AK47.FiringRate = 600f;
            AK47.Magazine = 30;
            AK47.MagazineCount = 16;
            AddItemInfo((int)AK47.Uid, AK47);

            
        } 

        
    }
}
