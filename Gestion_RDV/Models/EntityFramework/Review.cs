using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_review")]
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("User"), Column("user_id")]
        public int UserId { get; set; }
        public virtual Profile User { get; set; }

        [Required, Column("description"), StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("type"), StringLength(50)]
        public string Type { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
