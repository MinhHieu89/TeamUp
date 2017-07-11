using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class JoinRequestRepository : IJoinRequestRepository
    {
        private readonly TeamUpDbContext _context;

        public JoinRequestRepository(TeamUpDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<JoinRequest>> GetUserPendingRequestsAsync(string userId)
        {
            return await _context.JoinRequests
                .Where(r => r.UserId == userId && r.Status == RequestStatus.Pending)
                .OrderBy(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<JoinRequest>> GetTeamRequestsAsync(int teamId)
        {
            return await _context.JoinRequests
                .Where(r => r.TeamId == teamId && r.Status == RequestStatus.Pending)
                .Include(r => r.User)
                .Include(r => r.Team)
                .OrderBy(r => r.CreatedTime)
                .ToListAsync();
        }

        public async Task<JoinRequest> GetPendingRequestAsync(string userId, int teamId)
        {
            return await _context.JoinRequests.SingleOrDefaultAsync(r => r.UserId == userId && r.TeamId == teamId && r.Status == RequestStatus.Pending);
        }

        public async Task AddAsync(JoinRequest request)
        {
            await _context.JoinRequests.AddAsync(request);
        }

        public void Remove(JoinRequest request)
        {
            _context.JoinRequests.Remove(request);
        }
    }
}
