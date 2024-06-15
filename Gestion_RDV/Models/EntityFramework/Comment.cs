using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_comment_cmt")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("cmt_id")]
        public int CommentId { get; set; }

        [Required, Column("cmt_text")]
        public string Text { get; set; }

        [Column("cmt_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("rvw_id")]
        public int ReviewId { get; set; }

        // Navigation property
        [InverseProperty("Comments"), ForeignKey("UserId")]
        public User User { get; set; }

        [InverseProperty("Comments"), ForeignKey("ReviewId")]
        public Review Review { get; set; }


    }
}
