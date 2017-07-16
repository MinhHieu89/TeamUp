using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamUp.Core.Models;

namespace TeamUp.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Tên tài khoản")]
        [Remote("ValidateName", "Account")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hãy chọn tên thành phố.")]
        [Display(Name = "Thành phố")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Hãy chọn tên quận.")]
        [Display(Name = "Quận")]
        public int DistrictId { get; set; }

        public List<SelectListItem> Cities { get; private set; }

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

        public RegisterViewModel()
        {
            Cities = new List<SelectListItem>();
            
        }

        public void PopulateLocationList(IEnumerable<City> cities)
        {
            foreach (var city in cities)
                Cities.Add(new SelectListItem { Value = city.Id.ToString(), Text = city.Name });
        }
    }
}
