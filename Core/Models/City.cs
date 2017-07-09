using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TeamUp.Core.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Thành Phố")]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }

        public City()
        {
            Districts = new Collection<District>();
        }
    }
}
