using System.Collections.Generic;

namespace TeamUp.Core.ViewModels
{
    public class MemberInfoViewModel
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public KeyValuePairViewModel Team { get; set; }
        public IEnumerable<string> Positions { get; set; }
        public string StrongFoot { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Phone { get; set; }
        public IEnumerable<JoinRequestViewModel> JoinRequests { get; set; }

        public string GetLocation()
        {
            return $"{District}, {City}";
        }
    }
}
