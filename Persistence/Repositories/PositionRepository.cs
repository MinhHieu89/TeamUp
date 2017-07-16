using System.Collections.Generic;
using System.Linq;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly TeamUpDbContext _context;

        public PositionRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _context.Positions.ToList();
        }
    }
}
