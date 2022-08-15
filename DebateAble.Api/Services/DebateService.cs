using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
    public interface IDebateService
    {
        Task<TypedResult<List<DebateDTO>>> GetList();
        Task<TypedResult<DebateDTO>> PostDebate(DebateDTO dto);
    }

    public class DebateService : IDebateService
    {
        private readonly DebateAbleDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public DebateService(
            DebateAbleDbContext dbContext,
            IMapper mapper,
            ICurrentUserService currentUserService
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TypedResult<List<DebateDTO>>> GetList()
        {
            var results = await _dbContext.Debates
                .ToListAsync();

            return new TypedResult<List<DebateDTO>>(results.Select(r => _mapper.Map<DebateDTO>(r)).ToList());
        }

        public async Task<TypedResult<DebateDTO>> PostDebate(DebateDTO dto)
        {
            if(dto == null)
            {
                return new TypedResult<DebateDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(dto)} required");
            }

            if(string.IsNullOrEmpty(dto.Title))
            {
                return new TypedResult<DebateDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(dto.Title)} required");
            }

            var debate = new Debate()
            {
                Description = dto.Description,
                Title = dto.Title,
                CreatedByAppUserId = await _currentUserService.GetCurrentUserId()
            };

            _dbContext.Debates.Add(debate);
            await _dbContext.SaveChangesAsync();

            return new TypedResult<DebateDTO>(_mapper.Map<DebateDTO>(debate));
        }
    }
}
