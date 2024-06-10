using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_review_rvw")]
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("rvw_id")]
        public int ReviewId { get; set; }

        [ForeignKey("User"), Column("rvw_user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required, Column("rvw_description"), StringLength(500)]
        public string Description { get; set; }

        [Column("rvw_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("rvw_type"), StringLength(50)]
        public string Type { get; set; }

    }
}
