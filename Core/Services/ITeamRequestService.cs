using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public interface ITeamRequestService
    {
        Task Create(string userId, int teamId, RequestType requestType);
        Task Accept(TeamRequest request);
        Task Reject(TeamRequest request);
        Task Cancel(TeamRequest request);
        Task<TeamRequest> GetPendingRequest(string userId, int teamId);
        Task<TeamRequest> GetRequestById(int requestId);
    }
}