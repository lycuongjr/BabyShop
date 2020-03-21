using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [StringLength(20,MinimumLength = 6, ErrorMessage ="Độ dài mật khẩu ít nhất 6 kí tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không trùng nhau.")]
        [Required(ErrorMessage = "Yêu cầu xác nhận mật khẩu")]
        public string ConfimPassword { get; set; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Yêu cầu nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string Phone { get; set; }
        [Display(Name="Tỉnh/Thành")]
        public string ProvinceID { get; set; }
        [Display(Name = "Quận/Huyện")]
        public string DistrictID { get; set; }
        [Display(Name = "Xã/Phường")]
        public string CommuneID { get; set; }
    }
}