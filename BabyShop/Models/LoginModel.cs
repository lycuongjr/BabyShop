using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="Bạn phải nhập mật khẩu")]
        public string Password { get; set; }
    }
}