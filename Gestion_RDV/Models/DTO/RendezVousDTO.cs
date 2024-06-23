using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class RendezVousDTO
    {
        public int RendezVousId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public string? FichierJoint { get; set; }
        public int UserId { get; set; }
        public int OfficeId { get; set; }
    }
    public class RendezVousSpecialDTO
    {
        public int RendezVousId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public string FichierJoint { get; set; }
        public OfficeUserDTO User { get; set; }

    }
    public class RendezVousByUserIdDTO
    {
        public int RendezVousId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public string FichierJoint { get; set; }
        public RendezVousOfficeUserDTO Office { get; set; }

    }
    public class RendezVousOfficeUserDTO
    {
        public OfficeUserDTO User { get; set; }
    }
}
