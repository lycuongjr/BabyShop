using BabyShop.Models;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BabyShop.Controllers
{
    public class CartController : Controller
    {
        public const string CartSession = "CartSession";

        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public JsonResult Update(string cartModel)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var cart = Session[CartSession];
            var product = new ProductDao().viewDetail(productId);
            ViewBag.Product = product;
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {

                        if (item.Product.ID == productId)

                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    // tao moi doi tuong
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                // tao moi doi tuong
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                // gan vao session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });


        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });

        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]

        public ActionResult Payment(string shipName, string phone, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipName = shipName;
            order.ShipMobile = phone;
            order.ShipEmail = email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);


                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/client/template/new-order.html"));
                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", phone);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
      
            }
            catch (Exception ex)
            {

                return Redirect("/loi-thanh-toan");
            }


            return Redirect("/hoan-thanh");
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}