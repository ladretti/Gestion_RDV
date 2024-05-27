namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Messages")]
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, Column("Created")]
        public DateTime Created { get; set; }

        [Required, StringLength(100), Column("From")]
        public string From { get; set; }

        [Required, Column("Text")]
        public string Text { get; set; }

        [Required, Column("ConversationId")]
        public int ConversationId { get; set; }

        // Navigation property for related Conversation
        public virtual Conversation Conversation { get; set; }
    }

}
