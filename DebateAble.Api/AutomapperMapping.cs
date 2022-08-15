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
            CreateMap<Debate, DebateDTO>()
                .ForMember(dest => dest.StartedByEmailAddress, opt => opt.MapFrom(src => src.CreatedBy.Email))
                .ReverseMap();
        }
    }
}
