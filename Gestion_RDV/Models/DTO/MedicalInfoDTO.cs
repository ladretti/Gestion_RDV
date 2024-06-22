using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using Gestion_RDV.Services;


namespace Gestion_RDV.Models.DTO
{
    public class MedicalInfoDTO
    {
        public int MedicalInfoId { get; set; }
        public FileType Category { get; set; }
        public string Description { get; set; }
        public string CategoryName => Category.GetDisplayName();
    }
}
