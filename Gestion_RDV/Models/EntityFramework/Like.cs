using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_like")]
    public class Like
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("User"), Column("user_id")]
        public int UserId { get; set; }
        public virtual Profile User { get; set; }
    }
}
