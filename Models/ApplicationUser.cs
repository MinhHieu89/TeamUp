using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamUp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public int? TeamId { get; set; }
        public Team Team { get; set; }

    }
}
