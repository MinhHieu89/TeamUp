using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface ITeamRequestRepository
    {
        Task<IEnumerable<TeamRequest>> GetRequestsFromUser(string userId, RequestStatus? requestStatus = null);
        Task<IEnumerable<TeamRequest>> GetAllPendingRequestsForTeam(int teamId);
        Task<TeamRequest> GetPendingRequest(string userId, int teamId);
        Task<TeamRequest> GetRequestById(int id);
        Task Add(TeamRequest teamRequest);
        void Remove(TeamRequest teamRequest);
    }
}
