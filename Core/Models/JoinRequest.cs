using System;
using TeamUp.Core.Enums;

namespace TeamUp.Core.Models
{
    public class JoinRequest
    {
        public string UserId { get; private set; }
        public ApplicationUser User { get; set; }

        public int TeamId { get; private set; }
        public Team Team { get; set; }

        public RequestStatus Status { get; private set; }
        public DateTime CreatedTime { get; private set; }

        private JoinRequest()
        {
            CreatedTime = DateTime.Now;
            Status = RequestStatus.Pending;
        }

        public JoinRequest(string userId, int teamId) : this()
        {
            UserId = userId;
            TeamId = teamId;
        }

        public void SetStatus(RequestStatus status)
        {
            Status = status;
        }
    }
}
