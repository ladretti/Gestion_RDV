using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_prescription_pre")]
    public class Prescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("pre_id")]
        public int PrescriptionId { get; set; }

        [Column("pre_date"), Required]
        public DateTime PrescriptionDate { get; set; }

        // Foreign Key
        [Column("dia_id")]
        public int DiagnosisId { get; set; }

        [Column("med_id")]
        public int MedicationId { get; set; }

        // Navigation property
        [ForeignKey("DiagnosisId"), InverseProperty("Prescriptions")]
        public Diagnosis? Diagnosis { get; set; }

        [ForeignKey("MedicationId"), InverseProperty("Prescriptions")]
        public Medication? Medication { get; set; }

        
    }
}
