using AutoMapper;
using Gestion_RDV.Models.DTO;
using Gestion_RDV.Models.EntityFramework;

namespace Gestion_RDV.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Availability, AvailabilityDTO>();
            CreateMap<User, UserLoginDTO>();
            //Notification
            CreateMap<Notification, NotificationDetailsDTO>();
            CreateMap<Office, NotificationDetailsOfficeDTO>();
            CreateMap<User, NotificationDetailsUserDTO>();
            CreateMap<RendezVous, NotificationDetailsRendezVousDTO>();

            //Office
            CreateMap<Office, OfficeDTO>();
            CreateMap<Office, OfficeDetailDTO>();
            CreateMap<User, OfficeUserDTO>();

            //Post
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.ChildPosts, opt => opt.MapFrom(src => src.ChildPosts.Take(2)))
                .ForMember(dest => dest.TotalReplies, opt => opt.MapFrom(src => src.ChildPosts.Count));
            CreateMap<Post, PostDetailDTO>();




        }
    }
}
