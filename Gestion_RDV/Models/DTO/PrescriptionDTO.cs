using Gestion_RDV.Models.EntityFramework;

namespace Gestion_RDV.Models.DTO
{
    public class PrescriptionDTO
    {
        public DateTime PrescriptionDate { get; set; }
        public MedicationDTO? Medication { get; set; }
    }
}
