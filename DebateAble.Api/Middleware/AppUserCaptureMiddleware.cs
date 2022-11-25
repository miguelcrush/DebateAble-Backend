using DebateAble.Api.Services;
using System.Security.Claims;

namespace DebateAble.Api.Middleware
{
    /// <summary>
    /// Middleware to capture and sync an inbound user (in the absence of provider webhook)
    /// </summary>
    public class AppUserCaptureMiddleware
    {
        private readonly RequestDelegate _next;

        public AppUserCaptureMiddleware(
            RequestDelegate next
            )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAppUserService appUserService)
        {
            if (context.User == null)
            {
                return;
            }

            var emailClaimTypes = new string[]
            {
                ClaimTypes.NameIdentifier
            };

            var emailClaim = context.User.Claims.FirstOrDefault(c=> emailClaimTypes.Contains(c.Type));
            if (emailClaim == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            if (string.IsNullOrEmpty(emailClaim.Value))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var firstNameValue = context.User.FindFirstValue(ClaimTypes.GivenName);
            var lastNameValue = context.User.FindFirstValue(ClaimTypes.Surname);

            var createUserResult = await appUserService.AddOrUpdateUser(new DataTransfer.PostAppUserDTO()
            {
                Email = emailClaim.Value,
                FirstName = firstNameValue,
                LastName = lastNameValue
            });

            if (!createUserResult.WasSuccessful)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }

    public static class AppUserCaptureMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppUserCapture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppUserCaptureMiddleware>();
        }
    }
}
