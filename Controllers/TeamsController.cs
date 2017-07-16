using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Models;
using TeamUp.Core.ViewModels;

namespace TeamUp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamsController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var teams = await _unitOfWork.Teams.GetTeams();

            var viewModel = new TeamListViewModel
            {
                Teams = _mapper.Map<IEnumerable<Team>, IEnumerable<TeamInfoViewModel>>(teams),
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            if (user.TeamId.HasValue)
                return BadRequest("You've had a team.");

            var viewModel = new SaveTeamViewModel();
            viewModel.PopulateLocationList(_unitOfWork.Locations.GetCities());

            return View("TeamForm", viewModel);
        }

        public IActionResult ValidateTeamName(string name, string initialName)
        {
            if (name == initialName)
                return Json(true);

            return Json(_unitOfWork.Teams.CheckTeamNameExists(name) ? $"Tên {name} đã có người khác chọn." : "true");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(SaveTeamViewModel saveTeam)
        {
            if (!ModelState.IsValid)
                return View("TeamForm");

            var user = await GetCurrentUserAsync();
            var team = new Team(user);
            _mapper.Map(saveTeam, team);

            await _unitOfWork.Teams.AddAsync(team);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var team = await _unitOfWork.Teams.GetTeam(id);
            var viewModel = _mapper.Map<Team, TeamInfoViewModel>(team);
            var requests = await _unitOfWork.JoinRequests.GetTeamRequestsAsync(id);
            viewModel.JoinRequests = _mapper.Map<IEnumerable<JoinRequest>, IEnumerable<JoinRequestViewModel>>(requests);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _unitOfWork.Teams.GetTeam(id);
            if (team == null)
                return NotFound();

            var viewModel = new TeamEditViewModel();
            viewModel.PopulateLocationList(
                _unitOfWork.Locations.GetCities(),
                _unitOfWork.Locations.GetDistricts(team.District.CityId));


            _mapper.Map(team, viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamEditViewModel viewModel)
        {
            var team = await _unitOfWork.Teams.GetTeam(viewModel.Id);
            if (team == null)
                return NotFound();

            _mapper.Map(viewModel, team);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Info", new { id = team.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var team = await _unitOfWork.Teams.GetTeam(id, loadRelated: false);

            if (team == null)
                return NotFound();

            _unitOfWork.Teams.Delete(team);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}