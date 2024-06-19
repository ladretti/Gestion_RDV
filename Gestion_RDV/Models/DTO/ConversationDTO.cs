using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class ConversationDTO
    {
        public int ConversationId { get; set; }
        public string? Name { get; set; }
        public ICollection<ConversationDTO>? Conversations { get; }

    }
    public class ConversationUserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
