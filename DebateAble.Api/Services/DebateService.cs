using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
    public interface IDebateService
    {
        Task<TypedResult<List<GetDebateDTO>>> GetList(DebateIncludes includes = DebateIncludes.None);
        Task<TypedResult<GetDebateDTO>> PostDebate(PostDebateDTO dto, DebateIncludes includes = DebateIncludes.None);
        Task<TypedResult<GetDebateDTO>> GetDebate(Guid debateId, DebateIncludes includes = DebateIncludes.None);
    }

    public class DebateService : IDebateService
    {
        private readonly DebateAbleDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISluggerService _sluggerService;

        public DebateService(
            DebateAbleDbContext dbContext,
            IMapper mapper,
            ICurrentUserService currentUserService,
            ISluggerService sluggerService
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _sluggerService = sluggerService;
        }

        public async Task<TypedResult<List<GetDebateDTO>>> GetList(DebateIncludes includes = DebateIncludes.None)
        {
            var query = _dbContext.Debates
                .Where(d => 1 == 1);

            if (includes.HasFlag(DebateIncludes.Comments))
            {
                query = query.Include(d => d.Comments);
            }
            if (includes.HasFlag(DebateIncludes.Participants))
            {
                query = query.Include(d => d.Participants);
            }
            if (includes.HasFlag(DebateIncludes.Posts))
            {
                query = query.Include(d => d.Posts);
            }
            if (includes.HasFlag(DebateIncludes.ResponseRequests))
            {
                query = query.Include(d => d.ResponseRequests);
            }

            var results = await query.ToListAsync();

            return new TypedResult<List<GetDebateDTO>>(results.Select(r => _mapper.Map<GetDebateDTO>(r)).ToList());
        }

        public async Task<TypedResult<GetDebateDTO>> GetDebate(Guid debateId, DebateIncludes includes = DebateIncludes.None)
        {
            var query = _dbContext.Debates
                .Where(d => d.Id == debateId);

            if (includes.HasFlag(DebateIncludes.Comments))
            {
                query = query.Include(d => d.Comments);
            }
            if (includes.HasFlag(DebateIncludes.Participants))
            {
                query = query.Include(d => d.Participants);
            }
            if (includes.HasFlag(DebateIncludes.Posts))
            {
                query = query.Include(d => d.Posts);
            }
            if (includes.HasFlag(DebateIncludes.ResponseRequests))
            {
                query = query.Include(d => d.ResponseRequests);
            }

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
            {
                return new TypedResult<GetDebateDTO>(TypedResultSummaryEnum.ItemNotFound, $"Debate {debateId} was not found");
            }

            return new TypedResult<GetDebateDTO>(_mapper.Map<GetDebateDTO>(result));
        }

        public async Task<TypedResult<GetDebateDTO>> PostDebate(PostDebateDTO dto, DebateIncludes includes = DebateIncludes.None)
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
                CreatedByAppUserId = await _currentUserService.GetCurrentUserId(),
                Slug = _sluggerService.GetSlug(dto.Title)
            };

            _dbContext.Debates.Add(debate);
            await _dbContext.SaveChangesAsync();

            if (includes.HasFlag(DebateIncludes.Participants))
            {
                var participantsDtos = dto.Participants ?? Enumerable.Empty<PostDebateParticipantDTO>();

            }

            return new TypedResult<GetDebateDTO>(_mapper.Map<GetDebateDTO>(debate));
        }
    }
}
