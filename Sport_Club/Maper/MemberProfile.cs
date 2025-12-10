using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Maper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberCreateDto, Member>();
            CreateMap<MemberUpdateDto, Member>();
            CreateMap<Member, MemberGetDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null));
        }
    }
}
