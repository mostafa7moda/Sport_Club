using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Maper
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<TrainerCreateDto, Trainer>();
            CreateMap<TrainerUpdateDto, Trainer>();
            CreateMap<Trainer, TrainerGetDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null)) // UserName or FullName? User has UserName (Identity) and FullName (AppUser)
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : null))
                .ForMember(dest => dest.SectionName, opt => opt.MapFrom(src => src.Section != null ? src.Section.Name : null));
        }
    }
}
