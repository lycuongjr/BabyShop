using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public List<Menuu> GetListMenu(long groupId)
        {
            return db.Menuus.Where(x => x.TypeID == groupId).ToList();
        }
       
    }
}
