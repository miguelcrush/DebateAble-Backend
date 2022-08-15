using DebateAble.Api.Services;
using DebateAble.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DebateAble.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DebatesController : BaseDebateableController
	{
		private readonly IDebateService _debateService;

		public DebatesController(
			IDebateService debateService
            )
        {
			_debateService = debateService;
        }

		[HttpGet("list")]
		public async Task<IActionResult> GetList()
        {
			var result = await _debateService.GetList();
			return base.HandleTypedResult(result);
        }

		[HttpPost()]
		public async Task<IActionResult> Post(DebateDTO debate)
        {
			var result = await _debateService.PostDebate(debate);
			return base.HandleTypedResult(result);
        }
	}
}
