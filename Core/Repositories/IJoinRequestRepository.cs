using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface IJoinRequestRepository
    {
        Task<IEnumerable<JoinRequest>> GetUserPendingRequestsAsync(string userId);
        Task<IEnumerable<JoinRequest>> GetTeamRequestsAsync(int teamId);
        Task<JoinRequest> GetPendingRequestAsync(string userId, int teamId);
        Task AddAsync(JoinRequest request);
        void Remove(JoinRequest request);
    }
}
