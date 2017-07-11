using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace TeamUp.Core.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string CaptainId { get; set; }
        public ApplicationUser Captain { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Logo { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public DateTime CreatedDate { get; private set; }

        public ICollection<ApplicationUser> Members { get; }

        public ICollection<JoinRequest> JoinRequests { get; }

        private Team()
        {
            Members = new Collection<ApplicationUser>();
            JoinRequests = new Collection<JoinRequest>();
        }

        public Team(ApplicationUser user) : this()
        {
            Logo = "/images/logos/default-logo.jpg";
            CreatedDate = DateTime.Now;
            Captain = user;
            AddMember(user);
        }

        public void AddMember(ApplicationUser user)
        {
            Members.Add(user);
        }
    }
}
