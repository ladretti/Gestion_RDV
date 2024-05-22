using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_address")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Column("gouvernerat"), StringLength(100)]
        public string Gouvernerat { get; set; }

        [Column("ville"), StringLength(100)]
        public string Ville { get; set; }

        [Column("cite"), StringLength(100)]
        public string Cite { get; set; }
    }
}
