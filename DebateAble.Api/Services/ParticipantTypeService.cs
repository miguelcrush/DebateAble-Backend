using AutoMapper;
using DebateAble.Common;
using DebateAble.DataTransfer;
using DebateAble.Models;
using Microsoft.EntityFrameworkCore;

namespace DebateAble.Api.Services
{
    public interface IParticipantTypeService
    {
        Task<TypedResult<List<ParticipantTypeDTO>>> GetParticipantTypes();
    }

    public class ParticipantTypeService : IParticipantTypeService
    {
        private readonly DebateAbleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ParticipantTypeService(
            DebateAbleDbContext dbContext,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TypedResult<List<ParticipantTypeDTO>>> GetParticipantTypes()
        {
            var result = await _dbContext.ParticipantTypes
                .ToListAsync();

            return new TypedResult<List<ParticipantTypeDTO>>(result.Select(r => _mapper.Map<ParticipantTypeDTO>(r)).ToList());
        }
    }
}
