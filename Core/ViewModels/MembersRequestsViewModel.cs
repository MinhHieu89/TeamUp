using System.Collections.Generic;

namespace TeamUp.Core.ViewModels
{
    public class MembersRequestsViewModel
    {
        public IEnumerable<JoinRequestViewModel> Requests { get; set; }
        public IEnumerable<JoinRequestViewModel> Invitations { get; set; }
    }
}