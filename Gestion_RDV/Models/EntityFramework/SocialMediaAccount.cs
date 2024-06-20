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
        [Column("ofc_id")]
        public int OfficeId { get; set; }

        //Inverse Property
        [ForeignKey("OfficeId"), InverseProperty("Socials")]
        public Office Office { get; set; }
    }
}
