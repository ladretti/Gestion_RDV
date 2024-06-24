using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class MedicationDTO
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
    }
}
