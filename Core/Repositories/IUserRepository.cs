using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Core.Models;

namespace TeamUp.Core.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserById(string id, bool loadRelated = true);
    }
}
