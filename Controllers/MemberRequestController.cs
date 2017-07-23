using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamUp.Core.Models;
using TeamUp.Core.Services;

namespace TeamUp.Controllers
{
    [Authorize]
    public class MemberRequestController : Controller
    {
        private readonly ITeamRequestService _requestService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberService _memberService;

        public MemberRequestController(
            ITeamRequestService requestService,
            UserManager<ApplicationUser> userManager,
            IMemberService memberService)
        {
            _requestService = requestService;
            _userManager = userManager;
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> Join(int teamId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.HasATeam())
                return BadRequest("Bạn phải rời khỏi đội bóng hiện tại trước khi có thể gửi yêu cầu.");

            var request = await _requestService.GetPendingRequest(user.Id, teamId);
            if (request != null)
                return BadRequest("Bạn đã gửi yêu cầu tới đội bóng này hoặc đội bóng này đã gửi lời mời gia nhập đến bạn");

            await _requestService.Create(user.Id, teamId, RequestType.FromMember);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int requestId)
        {
            var request = await _requestService.GetRequestById(requestId);
            if (request == null)
                return NotFound("Yêu cầu đã bị hủy bỏ");

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user.HasATeam())
                return BadRequest("Thành viên này đã gia nhập đội khác. Yêu cầu hết hiệu lực");

            await _requestService.Accept(request);
            return Ok(user.Name);
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int requestId)
        {
            var request = await _requestService.GetRequestById(requestId);
            if (request == null)
                return NotFound("Yêu cầu đã hết hiệu lực");

            await _requestService.Reject(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int requestId)
        {
            var request = await _requestService.GetRequestById(requestId);
            if (request == null)
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