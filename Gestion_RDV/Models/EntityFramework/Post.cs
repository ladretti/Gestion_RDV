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

        [ForeignKey("Profile"), Column("pst_user_id")]
        public int UserId { get; set; }

        [Required, Column("pst_text")]
        public string Text { get; set; }

        [Column("pst_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("pst_type")]
        public string Type { get; set; } = "text";

    }
}
