using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_address_adr")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("adr_id")]
        public int AdresseId { get; set; }

        [Column("adr_adresse")]
        public string Adresse { get; set; }

        [Column("adr_ville")]
        public string Ville { get; set; }

        [Column("adr_codepostal")]
        public int CodePostal { get; set; }

        //ForeignKey

        // Navigation property
        [InverseProperty("Adresse")]
        public virtual ICollection<User>? Users { get; }

        [InverseProperty("Adresse")]
        public virtual ICollection<Office>? Offices { get; }

    }
}
