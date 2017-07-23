using TeamUp.Core.Enums;

namespace TeamUp.Core.ViewModels
{
    public class JoinRequestViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public RequestStatus Status { get; set; }
    }
}