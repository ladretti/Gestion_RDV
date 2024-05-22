namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Devis")]
    public class Devis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("ProfessionelId")]
        public int ProfessionelId { get; set; }

        [ForeignKey("Profile"), Column("PatientId")]
        public int PatientId { get; set; }

        [ForeignKey("RendezVous"), Column("AppointmentId")]
        public int AppointmentId { get; set; }

        [Column("PrixAvantTva")]
        public decimal PrixAvantTva { get; set; }

        [Column("Tva")]
        public decimal Tva { get; set; }

        [Column("PrixFinal")]
        public decimal PrixFinal { get; set; }

        // Navigation properties
        public virtual Profile Professionel { get; set; }
        public virtual Profile Patient { get; set; }
        public virtual RendezVous Appointment { get; set; }
    }

}
