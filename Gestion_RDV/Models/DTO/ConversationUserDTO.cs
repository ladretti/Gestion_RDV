using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class ConversationUserDTO
    {
        public int UserId { get; set; }
        public int ConversationId { get; set; }
    }
}
