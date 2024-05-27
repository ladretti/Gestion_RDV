using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_telephone")]
    public class Telephone
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Column("mobile")]
        public ICollection<string> Mobile { get; set; } = new List<string>();

        [Column("fix")]
        public ICollection<string> Fix { get; set; } = new List<string>();

        public virtual Profile Profile { get; set; }

    }
}
