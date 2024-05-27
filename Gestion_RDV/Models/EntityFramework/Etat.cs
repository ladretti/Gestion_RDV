using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("Etats")]
    public class Etat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name"), Required]
        public String Name { get; set; }
    }
}
