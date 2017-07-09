using System.Collections.Generic;

namespace TeamUp.Core.ViewModels
{
    public class TeamListViewModel
    {
        public IEnumerable<TeamDetailViewModel> Teams { get; set; }
        public UserViewModel User { get; set; }
    }
}