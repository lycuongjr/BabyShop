using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        BabyShopDbContext db = new BabyShopDbContext();

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }


        public CategoryProduct viewDetail(long id)
        {
            return db.CategoryProducts.Find(id);
        }
        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
    }
}
