using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TeamUpDbContext _context;

        public UserRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.Team)
                .Include(u => u.District)
                    .ThenInclude(d => d.City)
                .Include(u => u.Positions)
                    .ThenInclude(up => up.Position)
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id, bool loadRelated = true)
        {
            if (!loadRelated)
                return await _context.Users.FindAsync(id);

            return await _context.Users
                .Include(u => u.Team)
                    .ThenInclude(t => t.Members)
                .Include(u => u.District)
                    .ThenInclude(d => d.City)
                .Include(u => u.Positions)
                    .ThenInclude(up => up.Position)
                .Include(u => u.JoinRequests)
                    .ThenInclude(r => r.Team)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
