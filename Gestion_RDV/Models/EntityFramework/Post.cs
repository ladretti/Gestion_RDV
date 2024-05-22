namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Posts")]
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("UserId")]
        public int UserId { get; set; }

        [Required, Column("Text")]
        public string Text { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("Type")]
        public string Type { get; set; } = "text";

        // Navigation properties
        public virtual Profile User { get; set; }
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
