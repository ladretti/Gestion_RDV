using Gestion_RDV.Models.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.DTO
{
    public class ConversationUserDTO
    {
        public ConversationUser_ConversationDTO? Conversation { get; set; }
        public ConversationUser_UserDTO? User { get; set; }


    }
    public class ConversationUser_ConversationDTO
    {
        public int ConversationId { get; set; }
        public string? Name { get; set; }
    }
    public class ConversationUser_UserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}