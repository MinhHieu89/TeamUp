using System.Threading.Tasks;
using TeamUp.Core;
using TeamUp.Core.Repositories;
using TeamUp.Persistence.Repositories;

namespace TeamUp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeamUpDbContext _context;

        public IPhotoRepository Photos { get; private set; }
        public IPositionRepository Positions { get; private set; }
        public IUserRepository Users { get; private set; }
        public ITeamRepository Teams { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public ITeamRequestRepository TeamRequests { get; private set; }

        public UnitOfWork(TeamUpDbContext context)
        {
            Photos = new PhotoRepository(context);
            Positions = new PositionRepository(context);
            Users = new UserRepository(context);
            Teams = new TeamRepository(context);
            Locations = new LocationRepository(context);
            TeamRequests = new TeamRequestRepository(context);
            _context = context;
        }

        
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
