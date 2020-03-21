using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContactDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public Contact GetActive()
        {

            return db.Contacts.Single(x => x.Status == true);
        }
        public int InsertFeedBack(Feddback feddback)
        {
            db.Feddbacks.Add(feddback);
            db.SaveChanges();
            return feddback.ID;
        }
    }
}
