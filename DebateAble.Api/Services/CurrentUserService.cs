using DebateAble.Common;
using DebateAble.DataTransfer;
using System.Security.Claims;

namespace DebateAble.Api.Services
{
    public interface ICurrentUserService
    {
        Task<Guid> GetCurrentUserId();
        Task<TypedResult<GetAppUserDTO>> GetCurrentUser();

        Task<string> GetCurrentUserEmail();
    }
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IAppUserService _appUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IAppUserService appUserService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _appUserService = appUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor.HttpContext));
            }

            var user = _httpContextAccessor.HttpContext.User;
            if(user == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor.HttpContext.User));
            }

            var emailClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(emailClaim))
            {
                throw new ArgumentNullException("Email claim cannot be empty.");
            }

            var getDbUser = await _appUserService.GetUserByEmail(emailClaim);
            if (!getDbUser.WasSuccessful)
            {
                throw getDbUser.Exception;
            }
            if(getDbUser == null)
            {
                throw new InvalidOperationException($"User {emailClaim} was not found.");
            }

            return getDbUser.Payload.Id;
        }

        public async Task<TypedResult<GetAppUserDTO>> GetCurrentUser()
        {
            var currentUserId = await GetCurrentUserId();
            return await _appUserService.GetUserById(currentUserId);
        }

        public async Task<string> GetCurrentUserEmail()
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Payload.Email;
        }
    }
}
