using System.Collections.Generic;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface ILocationRepository
    {
        IEnumerable<City> GetCities();
        IEnumerable<District> GetDistricts(int cityId);
    }
}
