using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public interface IRequestService
    {
        Task Create(string userId, int teamId, RequestType requestType);
        Task Accept(Request request);
        Task Reject(Request request);
        Task Cancel(Request request);
    }
}