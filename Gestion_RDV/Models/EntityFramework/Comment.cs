using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_comment_cmt")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("cmt_id")]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("cmt_userid")]
        public int UserId { get; set; }

        [ForeignKey("Post"), Column("cmtpostid")]
        public int PostId { get; set; }

        [ForeignKey("Review"), Column("cmt_reviewid")]
        public int ReviewId { get; set; }

        [Required, Column("cmt_text")]
        public string Text { get; set; }

        [Column("cmt_date")]
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
