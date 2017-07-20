using System.Threading.Tasks;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly TeamUpDbContext _context;

        public PhotoRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Photo photo)
        {
            await _context.AddAsync(photo);
        }
    }
}
