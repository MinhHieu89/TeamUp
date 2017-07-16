namespace TeamUp.Core.Models
{
    public class UserPosition
    {
        public string UserId { get; set; }
        public byte PositionId { get; set; }

        public ApplicationUser User { get; set; }
        public Position Position { get; set; }
    }
}
