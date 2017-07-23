using System;
using System.ComponentModel.DataAnnotations;
using TeamUp.Core.Enums;

namespace TeamUp.Core.Models
{
    public class TeamRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; private set; }
        public ApplicationUser User { get; set; }

        public int TeamId { get; private set; }
        public Team Team { get; set; }

        public RequestStatus Status { get; private set; }

        [Required]
        public RequestType RequestType { get; private set; }

        public DateTime CreatedTime { get; private set; }
        public DateTime UpdatedTime { get; private set; }

        private TeamRequest()
        {
            CreatedTime = DateTime.Now;
            UpdatedTime = CreatedTime;
            Status = RequestStatus.Pending;
        }

        public TeamRequest(string userId, int teamId, RequestType requestType) : this()
        {
            UserId = userId;
            TeamId = teamId;
            RequestType = requestType;
        }

        public void SetStatus(RequestStatus status)
        {
            Status = status;
            UpdatedTime = DateTime.Now;
        }
    }
}
