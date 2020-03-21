using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
   public class ProductDao
    {
        BabyShopDbContext db = new BabyShopDbContext();
        public List<Product> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CrratedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //var model =
            //            from a in db.Products
            //            join b in db.CategoryProducts
            //            on a.CategoryID equals b.ID
            //            where a.CategoryID == categoryID
            //            select new ProductViewModel()
            //            {

            //                CategoryMetatitle = b.MetaTitle,
            //                CategoryName = b.Name,
            //                CreatedDate = a.CrratedDate,
            //                Detail = a.Detail,
            //                ID = a.ID,
            //                Images = a.Image,
            //                Name = a.Name,
            //                PromotionPrice = a.PromotionPrice,
            //                MetaTitle = a.MetaTitle,
            //                Price = a.Price
            //            };


            return model;
        }



        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CrratedDate).Take(top).ToList();
        }

        public List<Product> ListSpecialProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CrratedDate).Take(top).ToList();
        }
        public List<Product> ListRelated(long productId, ref int totalRecord, int pageIndex, int pageSize)
        {

            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).OrderBy(x => x.CrratedDate).Skip((pageIndex - 1) * pageIndex).Take(pageSize).ToList();
        }
        public Product viewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }


        public List<Product> Search(string keyword, ref int totalRecord, int pageIndex, int pageSize)
        {
            totalRecord = db.Products.Where(x => x.Name.Contains(keyword)).Count();
            var model = db.Products.Where(x => x.Name.Contains(keyword)).OrderByDescending(x => x.CrratedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //var model =
            //            from a in db.Products
            //            join b in db.CategoryProducts
            //            on a.CategoryID equals b.ID
            //            where a.CategoryID == categoryID
            //            select new ProductViewModel()
            //            {

            //                CategoryMetatitle = b.MetaTitle,
            //                CategoryName = b.Name,
            //                CreatedDate = a.CrratedDate,
            //                Detail = a.Detail,
            //                ID = a.ID,
            //                Images = a.Image,
            //                Name = a.Name,
            //                PromotionPrice = a.PromotionPrice,
            //                MetaTitle = a.MetaTitle,
            //                Price = a.Price
            //            };


            return model;
        }
        public long Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString)); //giam dan
            }
            return model.OrderByDescending(x => x.CrratedDate).ToPagedList(page, pageSize);
        }
    }
}
