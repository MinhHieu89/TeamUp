using System.Collections.Generic;
using System.Linq;
using TeamUp.Core.Models;
using TeamUp.Core.Repositories;

namespace TeamUp.Persistence.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly TeamUpDbContext _context;

        public LocationRepository(TeamUpDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(c => c.Name).ToList();
        }

        public IEnumerable<District> GetDistricts(int cityId)
        {
            return _context.Districts.Where(d => d.CityId == cityId).OrderBy(d => d.Name).ToList();
        }
    }
}
