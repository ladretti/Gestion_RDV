namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_post_pst")]
    public class Post
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("pst_id")]
        public int PostId { get; set; }

        [Required, Column("pst_text")]
        public string Text { get; set; }

        [Column("pst_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("pst_type")]
        public string Type { get; set; } = "text";


        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("p_pst_id")]
        public int? ParentPostId { get; set; }

        // Navigation property
        [InverseProperty("Posts"), ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ParentPostId"), InverseProperty("ChildPosts")]
        public Post ParentPost { get; set; }

        [InverseProperty("ParentPost")]
        public ICollection<Post> ChildPosts { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<LikePost>? LikesPosts { get; }
    }
}
