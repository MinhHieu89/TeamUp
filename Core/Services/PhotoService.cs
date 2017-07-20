using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TeamUp.Core.Models;

namespace TeamUp.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoStorage _photoStorage;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoStorage photoStorage)
        {
            _unitOfWork = unitOfWork;
            _photoStorage = photoStorage;
        }

        public async Task<Photo> UploadPhoto(ApplicationUser user, IFormFile file, string uploadsFolderPath)
        {
            var fileName = await _photoStorage.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo { FileName = fileName };
            user.Avatar = photo;
            await _unitOfWork.CompleteAsync();
            return photo;
        }
    }
}