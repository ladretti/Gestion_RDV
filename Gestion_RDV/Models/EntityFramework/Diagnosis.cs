using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_diagnosis_dia")]
    public class Diagnosis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("dia_id")]
        public int DiagnosisId { get; set; }

        [Column("dia_diagnosis_date"), Required]
        public DateTime DiagnosisDate { get; set; }

        [Column("dia_code")]
        public string Code { get; set; }

        [Column("dia_description")]
        public string Description { get; set; }

        [Column("dia_diagnosis_details")]
        public string DiagnosisDetails { get; set; }

        //Foreign Key
        [Column("mcr_id")]
        public int UserId { get; set; }

        [Column("rdv_id")]
        public int RendezVousId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("Diagnoses")]
        public User? User { get; set; }
        
        [ForeignKey("RendezVousId"), InverseProperty("Diagnoses")]
        public RendezVous? RendezVous { get; set; }

        [InverseProperty("Diagnosis")]
        public ICollection<Prescription>? Prescriptions { get; set; }
    }
}
