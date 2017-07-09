using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamUp.Core.Models;

namespace TeamUp.Core.ViewModels
{
    public class TeamEditViewModel
    {
        public int Id { get; set; }
        public UserViewModel Captain { get; set; }

        [Required(ErrorMessage = "Hãy nhập tên đội bóng.")]
        [Display(Name = "Tên đội")]
        [Remote("ValidateTeamName", "Team", AdditionalFields = "InitialName")]
        public string Name { get; set; }

        public string InitialName { get; set; }

        public string Logo { get; set; }

        [Required(ErrorMessage = "Hãy chọn tên thành phố.")]
        [Display(Name = "Thành phố")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Hãy chọn tên quận.")]
        [Display(Name = "Quận")]
        public int DistrictId { get; set; }

        public List<SelectListItem> Cities { get; private set; }
        public List<SelectListItem> Districts { get; private set; }

        public ICollection<UserViewModel> Members { get; private set; }

        public TeamEditViewModel()
        {
            Members = new Collection<UserViewModel>();
            Cities = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
        }

        public void PopulateLocationList(IEnumerable<City> cities, IEnumerable<District> districts)
        {
            foreach (var city in cities)
                Cities.Add(new SelectListItem { Value = city.Id.ToString(), Text = city.Name });

            foreach (var district in districts)
                Districts.Add(new SelectListItem { Value = district.Id.ToString(), Text = district.Name });
        }
    }
}