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
            CreateMap<SocialMediaAccount, SocialDTO>();
            CreateMap<OfficePostDTO, Office>();
            CreateMap<OfficePutDTO, Office>();

            //Post
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.ChildPosts, opt => opt.MapFrom(src => src.ChildPosts.Take(2)))
                .ForMember(dest => dest.TotalReplies, opt => opt.MapFrom(src => src.ChildPosts.Count))
                .ForMember(dest => dest.NbLike, opt => opt.MapFrom(src => src.LikesPosts.Count()));
            CreateMap<Post, PostDetailDTO>()
                .ForMember(dest => dest.NbLike, opt => opt.MapFrom(src => src.LikesPosts.Count()));
            CreateMap<PostPostDTO, Post>();

            //Review
            CreateMap<Review, ReviewDTO>()
                .ForMember(dest => dest.NbLike, opt => opt.MapFrom(src => src.LikesReview.Count(l => l.IsLiked)))
                .ForMember(dest => dest.NbDislike, opt => opt.MapFrom(src => src.LikesReview.Count(l => !l.IsLiked)));
            CreateMap<RendezVous, ReviewRendezVousDTO>();
            CreateMap<Comment, CommentDTO>();


            //Conversation
            CreateMap<Conversation, Conversation_UserDTO>();
            CreateMap<User, Conversation_UserDTO>();

            //Message
            CreateMap<Message, MessageDTO>();
            CreateMap<User, Message_UserDTO>();
            CreateMap<MessagePostDTO, Message>();

            //RendezVous
            CreateMap<RendezVousPostDTO, RendezVous>();
            CreateMap<RendezVous, RendezVousDTO>();
            CreateMap<UserSignInDTO, User>();

            //User
            CreateMap<User, UserDetailDTO>();

            //Availability
            CreateMap<AvailabilityPostDTO, Availability>();
            CreateMap<AvailabilityDTO, Availability>();

            //Subscription
            CreateMap<SubscriptionPostDTO, Subscription>();
            CreateMap<Subscription, SubscriptionPostDTO>();

            //Like
            CreateMap<LikePostDTO, LikePost>();
            CreateMap<LikePost, LikePostDTO>();
            CreateMap<LikeReviewDTO, LikeReview>();
            CreateMap<LikeReview, LikeReviewDTO>();

            //Facture

            CreateMap<FacturePostDTO, Facture>();
            CreateMap<Facture, FactureDTO>();
        }
    }
}
