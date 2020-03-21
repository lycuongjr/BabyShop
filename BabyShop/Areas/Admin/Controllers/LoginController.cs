using Model.Dao;
using BabyShop.Areas.Models;
using BabyShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabyShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
    
        public ActionResult Login(LoginModel loginModel)
        {
            //check validate
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(loginModel.UserName, Encryptor.MD5Hash(loginModel.Password));

                if (result == 1)
                {
                    var user = dao.GetById(loginModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "User", new { area = "Admin" }); //Action, controller
                }
                else if(result == 0)
                {
                    ModelState.AddModelError("", "Khong ton tai");

                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tai khoan dang bi khoa");

                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mat khau khong dung");

                }
              

            }

            return View("Login");   // index
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}