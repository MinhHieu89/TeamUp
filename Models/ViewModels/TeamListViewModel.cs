using System.Collections.Generic;
using TeamUp.Controllers;

namespace TeamUp.Models.ViewModels
{
    public class TeamListViewModel
    {
        public IEnumerable<TeamDetailViewModel> Teams { get; set; }
        public UserViewModel User { get; set; }
    }
}