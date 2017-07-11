using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamUp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public void JoinTeam(int teamId)
        {
            TeamId = teamId;
        }
    }
}
