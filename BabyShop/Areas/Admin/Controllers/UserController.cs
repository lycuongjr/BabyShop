using Model.Dao;
using Model.EF;
using BabyShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
           
        }

        public ActionResult TableList(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString ,page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public ActionResult UserProfile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)

            {
                var dao = new UserDao();
                var encryptorMD5 = Encryptor.MD5Hash(user.Password);
                user.Password = encryptorMD5;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    setAlert("Update Them thanh cong", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Them khong thanh cong ");
                }
            
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)

            {
                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                    {
                    var encryptorMD5 = Encryptor.MD5Hash(user.Password);
                    user.Password = encryptorMD5;
                }           
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Update khong thanh cong ");
                }

            }
            return View("Index");
        }
        public ActionResult EditUser(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user); 


        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
         {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}