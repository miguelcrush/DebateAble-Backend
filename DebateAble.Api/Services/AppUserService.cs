using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
	public interface IAppUserService
	{
		Task<TypedResult<AppUserDTO>> GetUserByEmail(string email);
        Task<TypedResult<AppUserDTO>> AddOrUpdateUser(AppUserDTO user);

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

        public async Task<TypedResult<AppUserDTO>> GetUserByEmail(string email) 
        {
            if (string.IsNullOrEmpty(email))
            {
                return new TypedResult<AppUserDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(email)} required");
            }

            var result = await _dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.Email == email);

            if(result == null)
            {
                return new TypedResult<AppUserDTO>(TypedResultSummaryEnum.ItemNotFound, $"User {email} not found");
            }

            return new TypedResult<AppUserDTO>(_mapper.Map<AppUserDTO>(result));
        }

        public async Task<TypedResult<AppUserDTO>> AddOrUpdateUser(AppUserDTO user)
        {
            if(user == null)
            {
                return new TypedResult<AppUserDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(user)} required");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentNullException($"{nameof(user.Email)} required");
            }

            var dbUser = await _dbContext.AppUsers
                .FirstOrDefaultAsync(au => au.Email == user.Email);

            if(dbUser == null)
            {
                dbUser = new AppUser()
                {
                    Email = user.Email
                };

                _dbContext.AppUsers.Add(dbUser);
            }

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            await _dbContext.SaveChangesAsync();

            return new TypedResult<AppUserDTO>(_mapper.Map<AppUserDTO>(dbUser));
        }
    }
}
