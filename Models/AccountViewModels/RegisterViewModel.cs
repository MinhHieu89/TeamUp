using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TeamUp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Tên tài khoản")]
        [Remote("ValidateName", "Account")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("ValidateEmail", "Account")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu phải chứa tối thiểu {2} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}
