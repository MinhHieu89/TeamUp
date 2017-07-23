using System.Threading.Tasks;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public class TeamRequestService : ITeamRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(string userId, int teamId, RequestType requestType)
        {
            var newRequest = new TeamRequest(userId, teamId, requestType);
            await _unitOfWork.TeamRequests.Add(newRequest);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Accept(TeamRequest request)
        {

            if (request.RequestType == RequestType.FromMember)
            {
                var pendingRequests = await _unitOfWork.TeamRequests.GetRequestsFromUser(request.UserId, RequestStatus.Pending);
                foreach (var req in pendingRequests)
                    req.SetStatus(RequestStatus.Canceled);
            }

            request.SetStatus(RequestStatus.Approved);

            var user = await _unitOfWork.Users.GetUserById(request.UserId, loadRelated: false);
            user.JoinTeam(request.TeamId);

            await _unitOfWork.CompleteAsync();
        }

        public async Task Reject(TeamRequest request)
        {
            request.SetStatus(RequestStatus.Rejected);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Cancel(TeamRequest request)
        {
            request.SetStatus(RequestStatus.Canceled);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<TeamRequest> GetPendingRequest(string userId, int teamId)
        {
            return await _unitOfWork.TeamRequests.GetPendingRequest(userId, teamId);
        }

        public async Task<TeamRequest> GetRequestById(int id)
        {
            return await _unitOfWork.TeamRequests.GetRequestById(id);
        }
    }
}