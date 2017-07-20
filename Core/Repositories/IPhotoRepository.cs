using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface IPhotoRepository
    {
        Task AddAsync(Photo photo);
    }
}
