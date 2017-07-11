using System;
using TeamUp.Core.Enums;

namespace TeamUp.Core.ViewModels
{
    public class JoinRequestViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime CreatedTime { get; set; }
        public RequestStatus Status { get; set; }
    }
}