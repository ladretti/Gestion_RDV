using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("Addresses")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Gouvernerat")]
        public string Gouvernerat { get; set; }

        [Column("Ville")]
        public string Ville { get; set; }

        [Column("Cite")]
        public string Cite { get; set; }

        // Navigation property
        public virtual Profile Profile { get; set; }
    }
}
