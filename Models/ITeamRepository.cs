using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamUp.Models
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeam(int id, bool loadRelated = true);
        Task AddAsync(Team team);
        void Delete(Team team);
        bool CheckTeamNameExists(string name);
    }
}
