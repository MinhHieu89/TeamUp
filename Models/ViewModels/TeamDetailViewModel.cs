using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TeamUp.Controllers;

namespace TeamUp.Models.ViewModels
{
    public class TeamDetailViewModel
    {
        public int Id { get; set; }
        public UserViewModel Captain { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<UserViewModel> Members { get; set; }

        public TeamDetailViewModel()
        {
            Members = new Collection<UserViewModel>();
        }

    }
}