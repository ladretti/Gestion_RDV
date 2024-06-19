using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_j_like_post_lke")]
    public class LikePost
    {
        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("pst_id")]
        public int PostId { get; set; }

        // Navigation property
        [InverseProperty("LikesPosts"), ForeignKey("UserId")]
        public User? User { get; set; }
        [InverseProperty("LikesPosts"), ForeignKey("PostId")]
        public Post? Post { get; set; }

    }
}
