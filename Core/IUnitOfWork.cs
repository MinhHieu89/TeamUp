using System.Threading.Tasks;
using TeamUp.Core.Repositories;

namespace TeamUp.Core
{
    public interface IUnitOfWork
    {
        ITeamRepository Teams { get; }
        ILocationRepository Locations { get; }
        Task CompleteAsync();
    }
}
