using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Dtos;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;
using TeamUp.Core.Services;

namespace TeamUp.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemberService _memberService;

        public RequestController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            IMemberService memberService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> Join(JoinRequestDto dto)
        {
            var user = await GetCurrentUser();
            if (user.TeamId.HasValue)
                return BadRequest("Bạn phải rời khỏi đội bóng hiện tại trước khi có thể gửi yêu cầu.");

            var request = await _unitOfWork.JoinRequests.GetPendingRequestAsync(user.Id, dto.TeamId);
            if (request != null)
                return BadRequest("Bạn đã gửi yêu cầu tới đội bóng này.");

            var newRequest = new JoinRequest(user.Id, dto.TeamId);
            await _unitOfWork.JoinRequests.AddAsync(newRequest);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Approve(JoinRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user.TeamId.HasValue)
                return BadRequest("Bạn phải rời khỏi đội bóng hiện tại trước khi có thể gửi yêu cầu.");

            var request = await _unitOfWork.JoinRequests.GetPendingRequestAsync(dto.UserId, dto.TeamId);
            if (request == null)
                return NotFound("Yêu cầu đã bị hủy bỏ");

            var pendingRequests = await _unitOfWork.JoinRequests.GetUserPendingRequestsAsync(dto.UserId);

            foreach (var req in pendingRequests)
                req.SetStatus(RequestStatus.Canceled);

            request.SetStatus(RequestStatus.Approved);
            user.JoinTeam(dto.TeamId);
            await _unitOfWork.CompleteAsync();

            return Ok(user.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Reject(JoinRequestDto dto)
        {
            var request = await _unitOfWork.JoinRequests.GetPendingRequestAsync(dto.UserId, dto.TeamId);
            if (request == null)
                return NotFound("Request not found");

            request.SetStatus(RequestStatus.Rejected);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(JoinRequestDto dto)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var request = await _unitOfWork.JoinRequests.GetPendingRequestAsync(userId, dto.TeamId);
            if (request == null)
                return NotFound("Request not found");

            request.SetStatus(RequestStatus.Canceled);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Leave()
        {
            var user = await GetCurrentUser();
            await _memberService.LeaveCurrentTeam(user);

            return Ok();
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return await _unitOfWork.Users.GetUserById(userId);
        }
    }
}