namespace Gestion_RDV.Models.EntityFramework
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Conversations")]
    public class Conversation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required, StringLength(100), Column("Name")]
        public string Name { get; set; }


        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<User> Participant { get; set; } = new List<User>();
    }
}
