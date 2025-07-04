﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_review_rvw")]
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("rvw_id")]
        public int ReviewId { get; set; }

        [Required, Column("rvw_description"), StringLength(500)]
        public string Description { get; set; }

        [Column("rvw_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("rvw_type"), StringLength(50)]
        public string Type { get; set; }

        [Column("rvw_note"), Range(0, 5, ErrorMessage = "La note doit être comprise entre 0 et 5.")]
        public int Note { get; set; }

        //ForeignKey
        [Column("rdv_id")]
        public int RendezVousId { get; set; }

        //Inverse Property
        [InverseProperty("Review")]
        public virtual ICollection<Comment>? Comments { get; }

        [ForeignKey("RendezVousId"), InverseProperty("Review")]
        public RendezVous? RendezVous { get; set; }

        [InverseProperty("Review")]
        public virtual ICollection<LikeReview>? LikesReview { get; }

    }
}
