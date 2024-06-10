using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_like_lke")]
    public class Like
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("lke_id")]
        public int Id { get; set; }

        [ForeignKey("Post"), Column("lke_postid")]
        public int PostId { get; set; }



        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }

        // Navigation property
        [InverseProperty("Likes"), ForeignKey("UserId")]
        public User User { get; set; }

    }
}
