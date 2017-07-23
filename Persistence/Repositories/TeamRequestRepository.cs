using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class TeamRequestRepository : ITeamRequestRepository
    {
        private readonly TeamUpDbContext _context;

        public TeamRequestRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<TeamRequest> GetRequestById(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<TeamRequest> GetPendingRequest(string userId, int teamId)
        {
            return await _context.Requests
                .SingleOrDefaultAsync(r => r.UserId == userId
                                           && r.TeamId == teamId
                                           && r.Status == RequestStatus.Pending);

        }


        public async Task<IEnumerable<TeamRequest>> GetRequestsFromUser(string userId, RequestStatus? requestStatus = null)
        {
            if (!requestStatus.HasValue)
            {
                return await _context.Requests
                    .Where(r => r.UserId == userId && r.RequestType == RequestType.FromMember)
                    .OrderByDescending(r => r.CreatedTime)
                    .ToListAsync();
            }

            return await _context.Requests
                .Where(r => r.UserId == userId && r.RequestType == RequestType.FromMember && r.Status == requestStatus)
                .OrderByDescending(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeamRequest>> GetAllPendingRequestsForTeam(int teamId)
        {
            return await _context.Requests
                .Where(r => r.TeamId == teamId && r.Status == RequestStatus.Pending && r.RequestType == RequestType.FromMember)
                .Include(r => r.User)
                .Include(r => r.Team)
                .OrderBy(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task Add(TeamRequest teamRequest)
        {
            await _context.Requests.AddAsync(teamRequest);
        }

        public void Remove(TeamRequest teamRequest)
        {
            _context.Requests.Remove(teamRequest);
        }
    }
}
