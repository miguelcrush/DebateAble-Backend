using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebateAble.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppUsersController : ControllerBase
	{

		[HttpGet("whoami")]
		public async Task<IActionResult> WhoAmI()
		{
			return Ok("helllllo");
		}
	}
}
