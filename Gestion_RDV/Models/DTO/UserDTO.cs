using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class UserMedicalDetailDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Weigh { get; set; }
        public int? Height { get; set; }
        public string? BloodType { get; set; }
        public string Sexe { get; set; }
        public ICollection<MedicalInfoDTO>? MedicalInfos { get; set; }
        public ICollection<DiagnosisDTO>? Diagnoses { get; set; }

    }
}
