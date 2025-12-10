//using AutoMapper;
//using Sport_Club.DTOs;
//using Sport_Club.Models;
//using static System.Collections.Specialized.BitVector32;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Sport_Club.Maper
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            // خريطة خاصة لـ Trainer مع تعيينات يدوية
//            CreateMap<TrainerDto, Trainer>()
//                .ForMember(dest => dest.ExperienceYears,
//                          opt => opt.MapFrom(src => src.ExperienceYears))
//                .ForMember(dest => dest.Gender,
//                          opt => opt.MapFrom(src => src.Gender))
//                .ForMember(dest => dest.Shift,
//                          opt => opt.MapFrom(src => src.Shift))
//                .ForMember(dest => dest.SectionId,
//                           opt => opt.MapFrom(src => src.SectionId))
//                // إذا كنت تستخدم User للـ FullName
//                .ForMember(dest => dest.User,
//                          opt => opt.Ignore());

//            //CreateMap<Trainer, TrainerReadDto>()
//            //    .ForMember(dest => dest.FullName,
//            //              opt => opt.MapFrom(src => src.User.FullName))  // من User
//            //    .ForMember(dest => dest.Phone,
//            //              opt => opt.MapFrom(src => src.User.PhoneNumber)); // من User

//            CreateMap<Member, MemberDto>().ReverseMap();
//            CreateMap<System.Collections.Specialized.BitVector32.Section, SectionDto>().ReverseMap();
//            CreateMap<Subscription, SubscriptionDto>().ReverseMap();
//        }





//    }
//}

////{
////    "fullName": "asds",
////  "gender": "Male",
////  "shift": "Morning",
////  "specialty": "MMM",
////  "experienceYears": 20,
////  "phone": "01256546",
////  "sectionId": 1
////}
