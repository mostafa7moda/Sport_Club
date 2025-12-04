using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Profiles
{
    public class TrainerProfile : Profile
    {
        public TrainerProfile()
        {
            CreateMap<Trainer, TrainerReadDto>();
            CreateMap<TrainerDto, Trainer>();
            CreateMap<TrainerUpdateDto, Trainer>();
        }
    }
}
