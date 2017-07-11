using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamUpDbContext _context;

        public TeamRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Members)
                .Include(t => t.District)
                    .ThenInclude(d => d.City)
                .ToListAsync();
        }

        public async Task<Team> GetTeam(int id, bool loadIncluded = true)
        {
            if (!loadIncluded)
                return await _context.Teams.FindAsync(id);

            return await _context.Teams
                .Include(t => t.Captain)
                .Include(t => t.Members)
                .Include(t => t.District)
                    .ThenInclude(d => d.City)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Team> GetTeamWithRequests(int id)
        {
            return await _context.Teams
                .Include(t => t.JoinRequests)
                .SingleOrDefaultAsync(t => t.Id == id);

        }

        public async Task AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
        }

        public void Delete(Team team)
        {
            _context.Teams.Remove(team);
        }

        public bool CheckTeamNameExists(string name)
        {
            return _context.Teams.Any(t => t.Name == name);
        }
    }
}
