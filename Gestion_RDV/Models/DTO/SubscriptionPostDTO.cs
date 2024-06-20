using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class SubscriptionPostDTO
    {
        public int UserId { get; set; }
        public int OfficeId { get; set; }
    }
}
