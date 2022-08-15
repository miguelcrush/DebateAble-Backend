using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
    public interface IDebateService
    {
        Task<TypedResult<List<GetDebateDTO>>> GetList();
        Task<TypedResult<GetDebateDTO>> PostDebate(PostDebateDTO dto);
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

        public async Task<TypedResult<List<GetDebateDTO>>> GetList()
        {
            var results = await _dbContext.Debates
                .ToListAsync();

            return new TypedResult<List<GetDebateDTO>>(results.Select(r => _mapper.Map<GetDebateDTO>(r)).ToList());
        }

        public async Task<TypedResult<GetDebateDTO>> PostDebate(PostDebateDTO dto)
        {
            if(dto == null)
            {
                return new TypedResult<GetDebateDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(dto)} required");
            }

            if(string.IsNullOrEmpty(dto.Title))
            {
                return new TypedResult<GetDebateDTO>(TypedResultSummaryEnum.InvalidRequest, $"{nameof(dto.Title)} required");
            }

            var debate = new Debate()
            {
                Description = dto.Description,
                Title = dto.Title,
                CreatedByAppUserId = await _currentUserService.GetCurrentUserId()
            };

            _dbContext.Debates.Add(debate);
            await _dbContext.SaveChangesAsync();

            if(dto.ResponseRequest!=null)
            {
                var responseRequest = _mapper.Map<ResponseRequest>(dto.ResponseRequest);
                _dbContext.ResponseRequests.Add(responseRequest);
                await _dbContext.SaveChangesAsync();
            }

            return new TypedResult<GetDebateDTO>(_mapper.Map<GetDebateDTO>(debate));
        }
    }
}
