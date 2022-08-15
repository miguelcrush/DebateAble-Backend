using DebateAble.Common;
using Microsoft.AspNetCore.Mvc;

namespace DebateAble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseDebateableController : ControllerBase
    {
        protected IActionResult HandleTypedResult(TypedResult typedResult)
        {
            if (!typedResult.WasSuccessful)
            {
                switch (typedResult.Summary)
                {
                    case TypedResultSummaryEnum.InvalidRequest:
                        return StatusCode((int)StatusCodes.Status400BadRequest, typedResult.Message);
                    case TypedResultSummaryEnum.ItemNotFound:
                        return StatusCode((int)StatusCodes.Status404NotFound);
                    default:
                        return StatusCode((int)StatusCodes.Status500InternalServerError);
                }
            }

            return StatusCode(StatusCodes.Status200OK);
        }

        protected IActionResult HandleTypedResult<TypedResultType>(TypedResult<TypedResultType> typedResult)
        {
            if (!typedResult.WasSuccessful)
            {
                switch (typedResult.Summary)
                {
                    case TypedResultSummaryEnum.InvalidRequest:
                        return StatusCode((int)StatusCodes.Status400BadRequest, typedResult.Message);
                    case TypedResultSummaryEnum.ItemNotFound:
                        return StatusCode((int)StatusCodes.Status404NotFound);
                    default:
                        return StatusCode((int)StatusCodes.Status500InternalServerError);
                }
            }

            return StatusCode(StatusCodes.Status200OK, typedResult.Payload);
        }
    }
}
