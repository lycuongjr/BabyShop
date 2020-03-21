using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class OrderDetailDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }
    }
}
