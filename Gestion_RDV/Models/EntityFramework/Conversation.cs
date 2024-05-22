namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_conversation")]
    public class Conversation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Column("participants")]
        public ICollection<string> Participants { get; set; } = new List<string>();

        [Required, Column("name"), StringLength(100)]
        public string Name { get; set; }
    }

}
