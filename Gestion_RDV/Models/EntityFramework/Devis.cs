namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_devis")]
    public class Devis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("Professionel"), Column("professionel_id")]
        public int ProfessionelId { get; set; }
        public virtual Profile Professionel { get; set; }

        [ForeignKey("Patient"), Column("patient_id")]
        public int PatientId { get; set; }
        public virtual Profile Patient { get; set; }

        [ForeignKey("Appointment"), Column("appointment_id")]
        public int AppointmentId { get; set; }
        public virtual RendezVous Appointment { get; set; }

        [Column("prix_avant_tva")]
        public decimal PrixAvantTva { get; set; }

        [Column("tva")]
        public decimal Tva { get; set; }

        [Column("prix_final")]
        public decimal PrixFinal { get; set; }
    }

}
