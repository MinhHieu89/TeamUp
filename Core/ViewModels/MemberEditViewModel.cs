using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeamUp.Core.Models;

namespace TeamUp.Core.ViewModels
{
    public class MemberEditViewModel
    {
        [Required(ErrorMessage = "Hãy chọn tên thành phố.")]
        [Display(Name = "Thành phố")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Hãy chọn tên quận.")]
        [Display(Name = "Quận")]
        public int DistrictId { get; set; }

        public string Avatar { get; set; }

        [Display(Name = "Chân thuận")]
        public bool? StrongFoot { get; set; }

        [PastDate]
        [Display(Name = "Ngày sinh")]
        public string BirthDate { get; set; }

        [MaxLength(20)]
        [Display(Name = "Điện thoại")]
        [UIHint("PhoneNumber")]
        public string Phone { get; set; }

        [MaxLength(3)]
        [Display(Name = "Chiều cao (cm)")]
        public string Height { get; set; }


        public List<SelectListItem> Cities { get; private set; }
        public List<SelectListItem> Districts { get; private set; }

        [Display(Name = "Vị trí ưa thích")]
        public List<PositionViewModel> Positions { get; private set; }

        public MemberEditViewModel()
        {
            Cities = new List<SelectListItem>();
            Districts = new List<SelectListItem>();
            Positions = new List<PositionViewModel>();
        }

        public void PopulateList(
            IEnumerable<City> cities, 
            IEnumerable<District> districts,
            IEnumerable<Position> positions)
        {
            foreach (var city in cities)
                Cities.Add(new SelectListItem { Value = city.Id.ToString(), Text = city.Name });

            foreach (var district in districts)
                Districts.Add(new SelectListItem {Value = district.Id.ToString(), Text = district.Name});

            foreach (var position in positions)
            {
                if (Positions.All(p => p.Id != position.Id))
                    Positions.Add(new PositionViewModel { Id = position.Id, Name = position.Name, Selected = false});
            }
            Positions.Sort((x, y) => x.Id - y.Id);
        }

        public DateTime GetDate() => DateTime.Parse($"{BirthDate}");

    }
}