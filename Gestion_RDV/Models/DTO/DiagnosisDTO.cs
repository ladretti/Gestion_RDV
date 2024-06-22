using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class DiagnosisDTO
    {
        public int DiagnosisId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string DiagnosisDetails { get; set; }
        public ICollection<PrescriptionDTO>? Prescriptions { get; set; }
    }
}
