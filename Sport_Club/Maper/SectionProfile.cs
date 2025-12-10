using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Maper
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<SectionCreateDto, Section>();
            CreateMap<SectionUpdateDto, Section>();
            CreateMap<Section, SectionGetDto>()
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager != null ? src.Manager.FullName : null));
        }
    }
}
