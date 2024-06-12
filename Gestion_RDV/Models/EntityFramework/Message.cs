namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_j_message_msg")]
    public class Message
    {
        [Required, Column("msg_created")]
        public DateTime Created { get; set; }

        [Required, Column("msg_text")]
        public string Text { get; set; }

        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        
        [Column("cnv_id")]
        public int ConversationId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("Messages")]
        public User User { get; set; }

        [ForeignKey("ConversationId"), InverseProperty("Messages")]
        public Conversation Conversation { get; set; }
    }

}
