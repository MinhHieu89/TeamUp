using System.Collections.Generic;

namespace TeamUp.Models
{
    public interface ILocationRepository
    {
        IEnumerable<City> GetCities();
        IEnumerable<District> GetDistricts(int cityId);
    }
}
