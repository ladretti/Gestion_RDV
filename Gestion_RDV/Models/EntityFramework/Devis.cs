namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_devis_dvs")]
    public class Devis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("dvs_id")]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("dvs_professionelid")]
        public int ProfessionelId { get; set; }


        [ForeignKey("Profile"), Column("dvs_patientid")]
        public int PatientId { get; set; }

        [ForeignKey("RendezVous"), Column("dvs_appointmentid")]
        public int AppointmentId { get; set; }

        [Column("dvs_prix_avant_tva")]
        public decimal PrixAvantTva { get; set; }

        [Column("dvs_tva")]
        public decimal Tva { get; set; }

        [Column("dvs_prix_final")]
        public decimal PrixFinal { get; set; }

    }

}
