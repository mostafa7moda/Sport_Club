using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Models;

namespace Sport_Club.Maper
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<SubscriptionCreateDto, Subscription>();
            CreateMap<SubscriptionUpdateDto, Subscription>();
            CreateMap<Subscription, SubscriptionGetDto>();
        }
    }
}
