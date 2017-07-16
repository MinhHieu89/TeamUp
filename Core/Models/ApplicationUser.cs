using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamUp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Avatar { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<UserPosition> Positions { get; set; }
        public ICollection<JoinRequest> JoinRequests { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public Team TeamToManage { get; set; }

        public bool? StrongFoot { get; set; }
        public DateTime? BirthDate { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(3)]
        public string Height { get; set; }

        public int? Age
        {
            get
            {
                if (BirthDate == null)
                    return null;
                return (int) ((DateTime.Now - BirthDate.Value).TotalDays / 365);
            }
        }

        public ApplicationUser()
        {
            Avatar = "/images/avatars/noAvatar.jpg";
            Positions = new Collection<UserPosition>();
            JoinRequests = new Collection<JoinRequest>();
        }

        public void JoinTeam(int teamId)
        { 
            TeamId = teamId;
        }

        public void LeaveCurrentTeam()
        {
            TeamId = null;
        }
    }
}
