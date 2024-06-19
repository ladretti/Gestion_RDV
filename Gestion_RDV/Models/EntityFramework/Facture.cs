namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_facture_fct")]
    public class Facture
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("fct_id")]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("fct_professionelid")]
        public int ProfessionelId { get; set; }


        [ForeignKey("Profile"), Column("fct_patientid")]
        public int PatientId { get; set; }

        [Column("fct_prix_avant_tva")]
        public decimal PrixAvantTva { get; set; }

        [Column("fct_tva")]
        public decimal Tva { get; set; }

        [Column("fct_prix_final")]
        public decimal PrixFinal { get; set; }

        //ForeinKey
        [Column("rdv_id")]
        public int RendezVousId { get; set; }
        //Inverse Property
        [InverseProperty("Facture"), ForeignKey("RendezVousId")]
        public RendezVous RendezVous { get; set; }

    }

}
