using AutoMapper;
using DebateAble.DataTransfer;
using DebateAble.Models;

namespace DebateAble.Api
{
    public class AutomapperMapping : Profile
    {
        public AutomapperMapping()
        {
            CreateMap<AppUserDTO, AppUser>().ReverseMap();
            CreateMap<Debate, GetDebateDTO>()
                .ForMember(dest => dest.StartedByEmailAddress, opt => opt.MapFrom(src => src.CreatedBy.Email))
                .ReverseMap();
            CreateMap<Debate, PostDebateDTO>().ReverseMap();
            CreateMap<ParticipantType, ParticipantTypeDTO>().ReverseMap();
            CreateMap<Invitation, InvitationDTO>().ReverseMap();
        }
    }
}
