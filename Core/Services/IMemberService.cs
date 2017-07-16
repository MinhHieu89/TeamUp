using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public interface IMemberService
    {
        Task LeaveCurrentTeam(ApplicationUser user);
    }
}
