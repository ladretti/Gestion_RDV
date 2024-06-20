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
            CreateMap<Office, OfficeDTO>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.RendezVous.Average(r => (r.Review != null) ? r.Review.Note : 0)));
            CreateMap<Office, OfficeDetailDTO>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.RendezVous.Average(r => (r.Review != null) ? r.Review.Note : 0)))
            .ForMember(dest => dest.NbSub, opt => opt.MapFrom(src => src.Subscriptions.Count));
            CreateMap<User, OfficeUserDTO>();
            CreateMap<Address, AddressDTO>();          

            //Post
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.ChildPosts, opt => opt.MapFrom(src => src.ChildPosts.Take(2)))
                .ForMember(dest => dest.TotalReplies, opt => opt.MapFrom(src => src.ChildPosts.Count));
            CreateMap<Post, PostDetailDTO>();

            //Review
            CreateMap<Review, ReviewDTO>();
            CreateMap<RendezVous, ReviewRendezVousDTO>();


            //Conversation
            CreateMap<Conversation, ConversationUserDTO>();
            CreateMap<User, ConversationDTO>();

            //Message
            CreateMap<Message, MessageDTO>();

        }
    }
}
