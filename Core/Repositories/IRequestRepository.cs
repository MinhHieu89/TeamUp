using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> GetRequestsFromUser(string userId, RequestStatus? requestStatus = null);
        Task<IEnumerable<Request>> GetAllPendingRequestsForTeam(int teamId);
        Task<Request> GetPendingRequest(string userId, int teamId);
        Task<Request> GetRequestById(int id);
        Task Add(Request request);
        void Remove(Request request);
        Task<IEnumerable<Request>> GetAllRequestsForUser(string userId);
        Task<IEnumerable<Request>> GetAllRequestsForTeam(int id);
    }
}
