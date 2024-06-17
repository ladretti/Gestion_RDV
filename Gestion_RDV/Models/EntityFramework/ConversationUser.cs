using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_j_conversationuser_cvu")]
    public class ConversationUser
    {
        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("cnv_id")]
        public int ConversationId { get; set; }

        // Navigation property
        [InverseProperty("ConversationsUser"), ForeignKey("UserId")]
        public User User { get; set; }

        [InverseProperty("ConversationsUser"), ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; }
    }  
}
