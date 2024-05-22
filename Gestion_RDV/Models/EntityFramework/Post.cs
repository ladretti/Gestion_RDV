namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_post")]
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("User"), Column("user_id")]
        public int UserId { get; set; }
        public virtual Profile User { get; set; }

        [Required, Column("text")]
        public string Text { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("type"), StringLength(50)]
        public string Type { get; set; } = "text";
    }
}
