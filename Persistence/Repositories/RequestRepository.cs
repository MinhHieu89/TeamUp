using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly TeamUpDbContext _context;

        public RequestRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task Add(Request request)
        {
            await _context.Requests.AddAsync(request);
        }

        public void Remove(Request request)
        {
            _context.Requests.Remove(request);
        }

        public async Task<Request> GetRequestById(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<Request> GetPendingRequest(string userId, int teamId)
        {
            return await _context.Requests
                .SingleOrDefaultAsync(r => r.UserId == userId
                                           && r.TeamId == teamId
                                           && r.Status == RequestStatus.Pending);

        }


        public async Task<IEnumerable<Request>> GetRequestsFromUser(string userId, RequestStatus? requestStatus = null)
        {
            if (!requestStatus.HasValue)
            {
                return await _context.Requests
                    .Where(r => r.UserId == userId && r.Type == RequestType.FromMember)
                    .OrderByDescending(r => r.CreatedTime)
                    .ToListAsync();
            }

            return await _context.Requests
                .Where(r => r.UserId == userId && r.Type == RequestType.FromMember && r.Status == requestStatus)
                .OrderByDescending(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllPendingRequestsForTeam(int teamId)
        {
            return await _context.Requests
                .Where(r => r.TeamId == teamId && r.Status == RequestStatus.Pending && r.Type == RequestType.FromMember)
                .Include(r => r.User)
                .Include(r => r.Team)
                .OrderBy(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForUser(string userId)
        {
            return await _context.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .Include(r => r.Team)
                .OrderByDescending(r => r.UpdatedTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllRequestsForTeam(int id)
        {
            return await _context.Requests
                .Where(r => r.TeamId == id)
                .Include(r => r.User)
                .Include(r => r.Team)
                .OrderByDescending(r => r.UpdatedTime)
                .ToListAsync();
        }
    }
}
