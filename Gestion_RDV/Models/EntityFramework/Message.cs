namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_message_msg")]
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("msg_id")]
        public int MessageId { get; set; }

        [Required, Column("msg_created")]
        public DateTime Created { get; set; }

        [Required, StringLength(100), Column("msg_from")]
        public string From { get; set; }

        [Required, Column("msg_text")]
        public string Text { get; set; }

        [Required, Column("msg_conversationid")]
        public int ConversationId { get; set; }

        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("Messages")]
        public User User { get; set; }
    }

}
