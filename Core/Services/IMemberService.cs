using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public interface IMemberService
    {
        Task LeaveTeam(string userId);
        Task<ApplicationUser> GetUserById(string id);
    }
}
