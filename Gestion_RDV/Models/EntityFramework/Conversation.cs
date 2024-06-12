namespace Gestion_RDV.Models.EntityFramework
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_conversation_cnv")]
    public class Conversation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("cnv_id")]
        public int ConversationId { get; set; }


        [Required, StringLength(100), Column("cnv_name")]
        public string Name { get; set; }

        //ForeignKey


        // Navigation property
        [InverseProperty("Conversation")]
        public virtual ICollection<ConversationUser>? ConversationsUser { get; }

        [InverseProperty("Conversation")]
        public virtual ICollection<Message>? Messages { get; }
    }
}
