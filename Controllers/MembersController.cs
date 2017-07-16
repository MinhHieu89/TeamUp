using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Controllers
{
    public class MembersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public MembersController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Info(string id)
        {
            var user = await _unitOfWork.Users.GetUserById(id);
            if (user == null)
                return NotFound("User not found");

            var viewModel = _mapper.Map<ApplicationUser, MemberInfoViewModel>(user);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _unitOfWork.Users.GetUsers();
            var viewModel = _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<MemberInfoViewModel>>(users);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await GetCurrentUser();
            var viewModel = _mapper.Map<ApplicationUser, MemberEditViewModel>(user);
            viewModel.PopulateList(
                _unitOfWork.Locations.GetCities(), 
                _unitOfWork.Locations.GetDistricts(user.District.CityId),
                _unitOfWork.Positions.GetPositions());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberEditViewModel viewModel)
        {
            var user = await GetCurrentUser();
            _mapper.Map(viewModel, user);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Info", new { id = user.Id });
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return await _unitOfWork.Users.GetUserById(userId);
        }
    }
}