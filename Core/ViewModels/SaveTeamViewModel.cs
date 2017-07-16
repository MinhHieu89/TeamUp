using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamUp.Core.Models;

namespace TeamUp.Core.ViewModels
{
    public class SaveTeamViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên đội")]
        [Required(ErrorMessage = "Hãy đặt tên cho đội của bạn")]
        [Remote("ValidateTeamName", "Teams")]
        public string Name { get; set; }

        [Display(Name = "Thành phố")]
        [Required(ErrorMessage = "Hãy chọn thành phố")]
        public int CityId { get; set; }

        [Display(Name = "Quận")]
        [Required(ErrorMessage = "Hãy chọn quận")]
        public int DistrictId { get; set; }

        public List<SelectListItem> Cities { get; private set; }

        public SaveTeamViewModel()
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