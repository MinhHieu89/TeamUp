using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core;
using TeamUp.Core.Enums;
using TeamUp.Core.Models;
using TeamUp.Core.Services;

namespace TeamUp.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberService _memberService;
        private readonly IUnitOfWork _unitOfWork;

        public RequestController(
            IRequestService requestService,
            UserManager<ApplicationUser> userManager,
            IMemberService memberService,
            IUnitOfWork unitOfWork)
        {
            _requestService = requestService;
            _userManager = userManager;
            _memberService = memberService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Join(int teamId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.HasATeam())
                return BadRequest("Bạn phải rời khỏi đội bóng hiện tại trước khi có thể gửi yêu cầu.");

            var request = await _unitOfWork.Requests.GetPendingRequest(user.Id, teamId);
            if (request != null)
                return BadRequest("Bạn đã gửi yêu cầu tới đội bóng này hoặc đội bóng này đã gửi lời mời gia nhập đến bạn");

            await _requestService.Create(user.Id, teamId, RequestType.FromMember);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Invite(string userId)
        {
            var requestSender = await _userManager.GetUserAsync(HttpContext.User);
            if (!requestSender.HasATeam())
                return BadRequest("Bạn phải là thành viên một đội bóng để có thể gửi yêu cầu");

            var teamId = requestSender.TeamId.Value;

            var request = await _unitOfWork.Requests.GetPendingRequest(userId, teamId);
            if (request != null)
                return BadRequest("Đội bóng đã gửi lời mời tới người này hoặc người này đã gửi yêu cầu gia nhập đến đội bóng");

            await _requestService.Create(userId, teamId, RequestType.FromTeam);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int requestId)
        {
            var request = await _unitOfWork.Requests.GetRequestById(requestId);
            if (request == null || request.Status != RequestStatus.Pending)
                return NotFound("Yêu cầu đã bị hủy bỏ");
            
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user.HasATeam() && request.Type == RequestType.FromMember)
                return BadRequest("Thành viên này đã gia nhập đội khác. Yêu cầu hết hiệu lực");

            await _requestService.Accept(request);
            return Ok(user.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int requestId)
        {
            var request = await _unitOfWork.Requests.GetRequestById(requestId);
            if (request == null || request.Status != RequestStatus.Pending)
                return NotFound("Yêu cầu đã hết hiệu lực");

            await _requestService.Reject(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int requestId)
        {
            var request = await _unitOfWork.Requests.GetRequestById(requestId);
            if (request == null || request.Status != RequestStatus.Pending)
                return NotFound("Yêu cầu đã hết hiệu lực");

            await _requestService.Cancel(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Leave()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            await _memberService.LeaveTeam(userId);
            return Ok();
        }
    }
}