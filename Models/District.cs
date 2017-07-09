using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TeamUp.Models
{
    public class District
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quận")]
        [MaxLength(100)]
        public string Name { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public District()
        {
            Users = new Collection<ApplicationUser>();
        }
    }
}
