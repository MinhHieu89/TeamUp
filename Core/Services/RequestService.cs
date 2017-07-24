using System.Threading.Tasks;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(string userId, int teamId, RequestType requestType)
        {
            var newRequest = new Request(userId, teamId, requestType);
            await _unitOfWork.Requests.Add(newRequest);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Accept(Request request)
        {

            if (request.Type == RequestType.FromMember)
            {
                var pendingRequests = await _unitOfWork.Requests.GetRequestsFromUser(request.UserId, RequestStatus.Pending);
                foreach (var req in pendingRequests)
                    req.SetStatus(RequestStatus.Canceled);
            }

            request.SetStatus(RequestStatus.Approved);

            var user = await _unitOfWork.Users.GetUserById(request.UserId, loadRelated: false);
            user.JoinTeam(request.TeamId);

            await _unitOfWork.CompleteAsync();
        }

        public async Task Reject(Request request)
        {
            request.SetStatus(RequestStatus.Rejected);
            await _unitOfWork.CompleteAsync();
        }

        public async Task Cancel(Request request)
        {
            request.SetStatus(RequestStatus.Canceled);
            await _unitOfWork.CompleteAsync();
        }
    }
}