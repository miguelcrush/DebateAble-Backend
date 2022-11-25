using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{

    public interface IInvitationService
    {
        /// <summary>
        /// Invites an email as the current user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<TypedResult<GetInvitationDTO>> InviteUser(string email);

        /// <summary>
        /// Invites an email as the current user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="force">Forces the invitation, even if the user already exists</param>
        /// <returns></returns>
        Task<TypedResult<GetInvitationDTO>> InviteUser(string email, bool force);

        /// <summary>
        /// Invites an email as a specified user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        Task<TypedResult<GetInvitationDTO>> InviteUser(string email, Guid appUserId);

        /// <summary>
        /// Invites an email as a specified user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="appUserId"></param>
        /// <param name="force">Forces the invitation, even if the user already exists</param>
        /// <returns></returns>
        Task<TypedResult<GetInvitationDTO>> InviteUser(string email, Guid appUserId, bool force);

        /// <summary>
        /// Sends invitations to recipients
        /// </summary>
        /// <returns></returns>
        Task<JobResult> SendInvitations();

        /// <summary>
        /// Gets all invitations
        /// </summary>
        /// <returns></returns>
        Task<TypedResult<List<GetInvitationDTO>>> GetInvitations();

    }
    public class InvitationService : IInvitationService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly DebateAbleDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;
        private readonly ISimpleEncryptionService _encryptionService;

        public InvitationService(
            ICurrentUserService currentUser,
            DebateAbleDbContext dbContext,
            IMapper mapper,
            IAppUserService appUserService,
            ISimpleEncryptionService encryptionService
            )
        {
            _currentUser = currentUser;
            _dbContext = dbContext;
            _mapper = mapper;
            _appUserService = appUserService;
            _encryptionService = encryptionService; 
        }

        public async Task<TypedResult<GetInvitationDTO>> InviteUser(string email)
        {
            return await this.InviteUser(email, false);
        }

        public async Task<TypedResult<GetInvitationDTO>> InviteUser(string email, bool force)
        {
            var currentUserId = await _currentUser.GetCurrentUserId();
            return await InviteUser(email, currentUserId);
        }

        public async Task<TypedResult<GetInvitationDTO>> InviteUser(string email, Guid appUserId)
        {
            return await InviteUser(email, appUserId, false);
        }

        public async Task<TypedResult<GetInvitationDTO>> InviteUser(string email, Guid appUserId, bool force)
        {
            //remember GDPR - don't want to store email addresses until the invitee registers

            var getUser = await _appUserService.GetUserById(appUserId);
            if (!getUser.WasSuccessful || getUser.Payload == null)
            {
                return new TypedResult<GetInvitationDTO>(TypedResultSummaryEnum.ItemNotFound, $"User {appUserId} was not found.");
            }

            //does the invitee already exist?
            var getInvitee = await _appUserService.GetUserByEmail(email);
            if (!getInvitee.WasSuccessful && getInvitee.Summary != TypedResultSummaryEnum.ItemNotFound)
            {
                return getInvitee.AsResult<GetAppUserDTO, GetInvitationDTO>();
            }

            //if they already exist and we're not forcing, return
            if(getInvitee.Payload != null && !force)
            {
                return new TypedResult<GetInvitationDTO>(new GetInvitationDTO()
                {
                    InviteeAppUserId = getInvitee.Payload.Id
                });
            }

            //otherwise, create an appuser record with a token
            var createAppUserRecord = await _appUserService.AddOrUpdateUser(new PostAppUserDTO()
            {
                Invited = true
            }) ;

            if (!createAppUserRecord.WasSuccessful)
            {
                return createAppUserRecord.AsResult<GetAppUserDTO, GetInvitationDTO>();
            }

            var token = await this.CreateInvitationToken(email);

            var invitation = new Invitation()
            {
                InvitationToken = token,
                InviteeAppUserId = createAppUserRecord.Payload.Id,
                InviterAppUserId = appUserId
            };

            try
            {
                _dbContext.Invitations.Add(invitation);
                await _dbContext.SaveChangesAsync();
                return new TypedResult<GetInvitationDTO>(_mapper.Map<GetInvitationDTO>(invitation));
            }
            catch (Exception ex)
            {
                return new TypedResult<GetInvitationDTO>($"Problem creating invitation", ex);
            }
        }

        public async Task<TypedResult<List<GetInvitationDTO>>> GetInvitations()
        {
            var queryResult = await _dbContext.Invitations.ToListAsync();
            return new TypedResult<List<GetInvitationDTO>>(queryResult.Select(i => _mapper.Map<GetInvitationDTO>(i)).ToList());
        }

        public async Task<JobResult> SendInvitations()
        {
            var result = new JobResult();

            //get all invitations
            var invitations = (await GetInvitations()).Payload;

            foreach(var invitation in invitations)
            {
                //get the email (decrypted)
                var emailAddress = await this.GetEmailFromInvitationToken(invitation.InvitationToken);
                //todo: email logic

                result.SuccessfulOperations++;
            }

            return result;
        }

        private async Task<string> CreateInvitationToken(string email)
        {
            var payload = $"Token:{email.ToLower()}";
            var token = await _encryptionService.Encrypt(email);
            return token;
        }

        private async Task<string> GetEmailFromInvitationToken(string token)
        {
            var decrypted = await _encryptionService.Decrypt(token);
            var email = decrypted.Split(":")[1];
            return email;
        }
    }
}
