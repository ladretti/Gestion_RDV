using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_adress_adr")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("adr_id")]
        public int Id { get; set; }

        [Column("adr_adresse")]
        public string Adresse { get; set; }

        [Column("adr_ville")]
        public string Ville { get; set; }

        [Column("adr_codepostal")]
        public int CodePostal { get; set; }


    }
}
