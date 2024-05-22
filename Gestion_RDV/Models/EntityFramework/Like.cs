using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("Likes")]
    public class Like
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("UserId")]
        public int UserId { get; set; }

        [ForeignKey("Post"), Column("PostId")]
        public int PostId { get; set; }

        // Navigation properties
        public virtual Profile User { get; set; }
        public virtual Post Post { get; set; }
    }
}
