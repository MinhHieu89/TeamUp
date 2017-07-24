using System.Threading.Tasks;
using TeamUp.Core.Repositories;

namespace TeamUp.Core
{
    public interface IUnitOfWork
    {
        IPhotoRepository Photos { get; }
        IPositionRepository Positions { get; }
        IUserRepository Users { get; }
        ITeamRepository Teams { get; }
        ILocationRepository Locations { get; }
        IRequestRepository Requests { get; }
        Task CompleteAsync();
    }
}
