using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Persistence.Repositories;

namespace TeamUp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeamUpDbContext _context;

        public ITeamRepository Teams { get; private set; }
        public ILocationRepository Locations { get; private set; }

        public UnitOfWork(TeamUpDbContext context)
        {
            Teams = new TeamRepository(context);
            Locations = new LocationRepository(context);
            _context = context;
        }

        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
