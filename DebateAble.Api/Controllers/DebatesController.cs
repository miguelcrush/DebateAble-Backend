using DebateAble.Api.Services;
using DebateAble.Common;
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
		public async Task<IActionResult> GetList([FromQuery] DebateIncludes includes = DebateIncludes.None)
        {
			var result = await _debateService.GetList(includes);
			return base.HandleTypedResult(result);
        }

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDebate(Guid id, [FromQuery] DebateIncludes includes = DebateIncludes.None )
        {
			var result = await _debateService.GetDebate(id, includes);
			return base.HandleTypedResult(result);
        }

		[HttpPost()]
		public async Task<IActionResult> Post(PostDebateDTO dto, [FromQuery] DebateIncludes includes = DebateIncludes.None)
        {
			var result = await _debateService.PostDebate(dto, includes);
			return base.HandleTypedResult(result);
        }
	}
}
