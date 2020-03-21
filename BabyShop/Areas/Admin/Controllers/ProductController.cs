using BabyShop.Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult ListProduct(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                product.CreatedBy = session.UserName;
                product.CrratedDate = DateTime.Now;
                long id = dao.Insert(product);
                if (id > 0)
                {
                    setAlert("Update Them thanh cong", "success");
                    return RedirectToAction("Create", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Them khong thanh cong ");
                }

            }
            return View("Index");
        }
           
        }
    }
