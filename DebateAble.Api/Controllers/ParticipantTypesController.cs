using DebateAble.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DebateAble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantTypesController : BaseDebateableController
    {
        private readonly IParticipantTypeService _participantTypeService;

        public ParticipantTypesController(
            IParticipantTypeService participantTypeService
            )
        {
            _participantTypeService = participantTypeService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            var result = await _participantTypeService.GetParticipantTypes();
            return base.HandleTypedResult(result);
        }
    }
}
