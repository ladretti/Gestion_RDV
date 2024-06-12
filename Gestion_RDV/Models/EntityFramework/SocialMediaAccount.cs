using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_e_socialmediaaccount_sma")]
    public class SocialMediaAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("sma_id")]
        public int SocialMediaAccountId { get; set; }

        [Column("sma_platform"), StringLength(50)]
        public string Platform { get; set; }

        [Column("sma_url"), StringLength(255)]
        public string Url { get; set; }

        // Foreign key
        [Column("usr_id")]
        public int UserId { get; set; }

        //Inverse Property
        [ForeignKey("UserId"), InverseProperty("Socials")]
        public User User { get; set; }
    }
}
