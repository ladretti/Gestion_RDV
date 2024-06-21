using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class LikeReviewDTO
    {
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public bool IsLiked { get; set; }
    }
}
