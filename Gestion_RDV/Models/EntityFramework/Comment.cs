using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_comment")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("User"), Column("user_id")]
        public int UserId { get; set; }
        public virtual Profile User { get; set; }

        [Required, Column("text")]
        public string Text { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
