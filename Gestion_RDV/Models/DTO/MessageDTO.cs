using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class MessageDTO
    {
        public DateTime Created { get; set; }
        public string? Text { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}
