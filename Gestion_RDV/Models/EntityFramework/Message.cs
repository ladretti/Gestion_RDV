namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_message")]
    public class Message
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Required, Column("created")]
        public DateTime Created { get; set; }

        [Required, Column("from"), StringLength(100)]
        public string From { get; set; }

        [Required, Column("text")]
        public string Text { get; set; }

        [Required, ForeignKey("Conversation"), Column("conversation_id")]
        public int ConversationId { get; set; }
        public virtual Conversation Conversation { get; set; }
    }

}
