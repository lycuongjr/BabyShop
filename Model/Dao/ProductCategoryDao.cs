using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ProductCategoryDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public List<CategoryProduct> ListAll()
        {
            return db.CategoryProducts.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }

        public CategoryProduct viewDetail(long id)
        {
            return db.CategoryProducts.Find(id);
        }
    }
}
