using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_medication_med")]
    public class Medication
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("med_id")]
        public int MedicationId { get; set; }

        [Column("med_name"), Required]
        public string Name { get; set; }

        [Column("med_dosage")]
        public string Dosage { get; set; }

        // Foreign Key

        // Navigation property
        [InverseProperty("Medication")]
        public ICollection<Prescription>? Prescriptions { get; set; }

    }
}
