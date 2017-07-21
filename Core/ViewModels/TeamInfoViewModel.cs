using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TeamUp.Core.ViewModels
{
    public class TeamInfoViewModel
    {
        public int Id { get; set; }
        public UserViewModel Captain { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<JoinRequestViewModel> JoinRequests { get; set; }

        public ICollection<MemberInfoViewModel> Members { get; set; }

        public TeamInfoViewModel()
        {
            Members = new Collection<MemberInfoViewModel>();
        }

    }
}