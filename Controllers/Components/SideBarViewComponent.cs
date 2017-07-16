using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Controllers.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SideBarViewComponent(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!_signInManager.IsSignedIn(HttpContext.User)) return View();

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _unitOfWork.Users.GetUserById(userId);
            var viewModel = _mapper.Map<ApplicationUser, MemberInfoViewModel>(user);
            return View("SideBar", viewModel);
        }
    }
}
