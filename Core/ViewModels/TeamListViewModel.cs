using System.Collections.Generic;

namespace TeamUp.Core.ViewModels
{
    public class TeamListViewModel
    {
        public IEnumerable<TeamInfoViewModel> Teams { get; set; }
    }
}