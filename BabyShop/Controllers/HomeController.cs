using BabyShop.Common;
using BabyShop.Models;
using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Index()
        {
            ViewBag.SlideValue = new SlideDao().ListAll();
            ViewBag.Category = new CategoryDao().ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProduct = productDao.ListNewProduct(4);
            ViewBag.SpecialProduct = productDao.ListSpecialProduct(4);

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().GetListMenu(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
        [ChildActionOnly]

        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}