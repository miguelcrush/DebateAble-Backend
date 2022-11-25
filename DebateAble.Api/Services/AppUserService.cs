using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
	public interface IAppUserService
	{
		Task<TypedResult<GetAppUserDTO>> GetUserByEmail(string email);
        Task<TypedResult<GetAppUserDTO>> AddOrUpdateUser(PostAppUserDTO user);
        Task<TypedResult<GetAppUserDTO>> GetUserById(Guid id);
    }

    public class AppUserService : IAppUserService
    {
        private readonly DebateAbleDbContext _dbContext;
        private readonly IMapper _mapper;

        public AppUserService(
            DebateAbleDbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;   
        }

        public async Task<TypedResult<GetAppUserDTO>> GetUserByEmail(string email) 
        {
            if (string.IsNullOrEmpty(email))
            {
                return new TypedResult<GetAppUserDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(email)} required");
            }

            var result = await _dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.Email == email);

            if(result == null)
            {
                return new TypedResult<GetAppUserDTO>(TypedResultSummaryEnum.ItemNotFound, $"User {email} not found");
            }

            return new TypedResult<GetAppUserDTO>(_mapper.Map<GetAppUserDTO>(result));
        }

        public async Task<TypedResult<GetAppUserDTO>> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new TypedResult<GetAppUserDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(id)} required");
            }

            var result = await _dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.Id == id);

            if (result == null)
            {
                return new TypedResult<GetAppUserDTO>(TypedResultSummaryEnum.ItemNotFound, $"User {id} not found");
            }

            return new TypedResult<GetAppUserDTO>(_mapper.Map<GetAppUserDTO>(result));
        }

        public async Task<TypedResult<GetAppUserDTO>> AddOrUpdateUser(PostAppUserDTO user)
        {
            if(user == null)
            {
                return new TypedResult<GetAppUserDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(user)} required");
            }

            var dbUser = await _dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.Email == user.Email);

            if(dbUser == null)
            {
                dbUser = _mapper.Map<AppUser>(user);
                _dbContext.AppUsers.Add(dbUser);
            }

            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Invited = user.Invited;

            await _dbContext.SaveChangesAsync();

            return new TypedResult<GetAppUserDTO>(_mapper.Map<GetAppUserDTO>(dbUser));
        }
    }
}
