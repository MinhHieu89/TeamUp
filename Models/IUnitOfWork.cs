using System.Threading.Tasks;

namespace TeamUp.Models
{
    public interface IUnitOfWork
    {
        ITeamRepository Teams { get; }
        ILocationRepository Locations { get; }
        Task CompleteAsync();
    }
}
