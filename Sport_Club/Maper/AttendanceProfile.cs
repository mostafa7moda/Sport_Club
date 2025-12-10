using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Maper
{
    public class AttendanceProfile : Profile
    {
        public AttendanceProfile()
        {
            CreateMap<AttendanceLogDto, Attendance>();
            CreateMap<Attendance, AttendanceGetDto>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member != null && src.Member.User != null ? src.Member.User.FullName : null))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section != null ? src.Section.Name : null));
        }
    }
}
