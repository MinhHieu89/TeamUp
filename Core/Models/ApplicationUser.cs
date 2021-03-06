﻿using System;
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

        public Photo Avatar { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<UserPosition> Positions { get; set; }
        public ICollection<Request> JoinRequests { get; set; }

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
            Positions = new Collection<UserPosition>();
            JoinRequests = new Collection<Request>();
        }

        public void JoinTeam(int teamId)
        { 
            TeamId = teamId;
        }

        public void LeaveTeam()
        {
            TeamId = null;
        }

        public bool HasATeam()
        {
            return TeamId.HasValue;
        }
    }
}
