using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_j_like_review_lke")]
    public class LikeReview
    {
        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("rvw_id")]
        public int ReviewId { get; set; }

        // Navigation property
        [InverseProperty("LikesReview"), ForeignKey("UserId")]
        public User User { get; set; }
        [InverseProperty("LikesReview"), ForeignKey("ReviewId")]
        public Review Review { get; set; }

    }
}
