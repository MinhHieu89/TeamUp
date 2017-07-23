using System.Linq;
using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LeaveTeam(string userId)
        {
            var user = await GetUserById(userId);
            user.LeaveTeam();

            var members = user.Team.Members;
            if (members.Count <= 1)
            {
                _unitOfWork.Teams.Delete(user.Team);
            }
            else if (user.Team.CaptainId == user.Id)
            {
                var other = members.FirstOrDefault(m => m.Id != user.Id);
                user.Team.CaptainId = other.Id;
            }

            await _unitOfWork.CompleteAsync();
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _unitOfWork.Users.GetUserById(userId);
        }
    }
}