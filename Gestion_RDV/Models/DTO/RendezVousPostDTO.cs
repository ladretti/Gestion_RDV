using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class RendezVousPostDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Prix { get; set; }
        public int UserId { get; set; }
        public int AvailabilityId { get; set; }
        public int OfficeId { get; set; }
    }
}
