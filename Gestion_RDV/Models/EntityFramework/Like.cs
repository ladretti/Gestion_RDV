using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_like_lke")]
    public class Like
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("lke_id")]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("lke_userid")]
        public int UserId { get; set; }

        [ForeignKey("Post"), Column("lke_postid")]
        public int PostId { get; set; }

        
    }
}
