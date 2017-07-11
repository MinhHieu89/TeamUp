using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Dtos;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;

namespace TeamUp.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public RequestController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Join(JoinRequestDto dto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
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
                return NotFound("Request not found");

            var pendingRequests = await _unitOfWork.JoinRequests.GetUserPendingRequestsAsync(dto.UserId);

            foreach (var req in pendingRequests)
                req.SetStatus(RequestStatus.Expired);

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
    }
}