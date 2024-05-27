using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("Comments")]
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("UserId")]
        public int UserId { get; set; }

        [ForeignKey("Post"), Column("PostId")]
        public int PostId { get; set; }

        [Required, Column("Text")]
        public string Text { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Profile User { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
