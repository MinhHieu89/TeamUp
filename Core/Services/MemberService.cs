using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberService(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task LeaveCurrentTeam(ApplicationUser user)
        {
            var members = user.Team.Members;
            user.LeaveCurrentTeam();
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
        
    }
}