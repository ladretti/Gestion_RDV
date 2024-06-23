using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    //----------------------GET----------------------
    public class ConversationDTO
    {
        public int ConversationId { get; set; }
        public string? ConversationName { get; set; }
        public ICollection<Conversation_UserDTO>? Users { get; set; }
    }
    public class Conversation_UserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    //----------------------POST----------------------
    public class ConversationPostDTO
    {
        public string? ConversationName { get; set; }
        public ICollection<int>? UserIds { get; set; }
    }
}