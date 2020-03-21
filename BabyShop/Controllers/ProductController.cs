using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]

        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();

            return PartialView(model);
        }

        public ActionResult Category(int cateId, int page = 1, int pageSize = 6)
        {
            var category = new CategoryDao().viewDetail(cateId);
            ViewBag.Category = category;
            ViewBag.SlideValue = new SlideDao().ListAll();
            int totalRecord = 0;
            ViewBag.RelatedProduct = new ProductDao().ListRelated(cateId, ref totalRecord, page, pageSize);
            var model = new ProductDao().ListByCategoryId(cateId, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 10;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        [OutputCache(CacheProfile = "Cache1HourDetail")]
        public ActionResult Detail(int id, int page = 1, int pageSize = 6)
        {
            var product = new ProductDao().viewDetail(id);
            var totalPage = 0;
            ViewBag.CategoryProduct = new ProductCategoryDao().viewDetail(product.CategoryID.Value);
            ViewBag.RelatedProduct = new ProductDao().ListRelated(id, ref totalPage, page, pageSize);

            return View(product);
        }

        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 12)
        {

            ViewBag.SlideValue = new SlideDao().ListAll();
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 10;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}