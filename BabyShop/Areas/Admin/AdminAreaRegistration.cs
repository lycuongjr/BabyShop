using System.Web.Mvc;

namespace BabyShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "ProductCreate",
               "Admin/them-san-pham",
               new { action = "Create",controller= "Product", id = UrlParameter.Optional }
           );
            context.MapRoute(
               "ProductList",
               "Admin/danh-sach-san-pham",
               new { action = "ListProduct", controller = "Product", id = UrlParameter.Optional }
           );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}