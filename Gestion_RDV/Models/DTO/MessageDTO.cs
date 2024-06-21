using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class MessageDTO
    {
        public DateTime Created { get; set; }
        public string? Text { get; set; }
        public int ConversationId { get; set; }
        public  Message_UserDTO? User { get; set; }
    }
    public class Message_UserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
