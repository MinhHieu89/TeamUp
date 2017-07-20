using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TeamUp.Core;
using TeamUp.Core.Models;
using TeamUp.Core.Services;

namespace TeamUp.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;
        private readonly IPhotoService _photoService;
        private readonly PhotoSettings _photoSettings;

        public PhotosController(
            IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment host,
            IOptionsSnapshot<PhotoSettings> options,
            IPhotoService photoService)
        {
            _photoSettings = options.Value;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _host = host;
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _unitOfWork.Users.GetUserById(userId, loadRelated: false);

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Maximum file size exceeded");

            var fileType = "." + file.ContentType.Split('/')[1];
            if (!_photoSettings.IsSupported(fileType)) return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            var photo = await _photoService.UploadPhoto(user, file, uploadsFolderPath);

            return Ok(photo);
        }

    }
}
