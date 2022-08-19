using DebateAble.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebateAble.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppUsersController : BaseDebateableController
	{
		private readonly ICurrentUserService _currentUserService;

		public AppUsersController(
			ICurrentUserService currentUserService
            )
        {
			_currentUserService = currentUserService;
        }

		[HttpGet("me")]
		public async Task<IActionResult> GetMe()
		{
			var result = await _currentUserService.GetCurrentUser();
			return base.HandleTypedResult(result);
		}
	}
}
