using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(ApplicationUser user, IFormFile file, string uploadsFolderPath);
    }
}
