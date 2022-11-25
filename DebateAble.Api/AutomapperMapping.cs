using AutoMapper;
using DebateAble.DataTransfer;
using DebateAble.Models;

namespace DebateAble.Api
{
    public class AutomapperMapping : Profile
    {
        public AutomapperMapping()
        {
            CreateMap<GetAppUserDTO, AppUser>().ReverseMap();
            CreateMap<PostAppUserDTO, AppUser>().ReverseMap();

            CreateMap<Debate, GetDebateDTO>()
                .ForMember(dest => dest.StartedByEmailAddress, opt => opt.MapFrom(src => src.CreatedBy.Email))
                .ReverseMap();
            CreateMap<Debate, PostDebateDTO>().ReverseMap();

            CreateMap<ParticipantType, ParticipantTypeDTO>().ReverseMap();
            CreateMap<GetDebateParticipantDTO, DebateParticipant>().ReverseMap();
            CreateMap<PostDebateParticipantDTO, DebateParticipant>().ReverseMap();

            CreateMap<Invitation, GetInvitationDTO>().ReverseMap();
        }
    }
}
