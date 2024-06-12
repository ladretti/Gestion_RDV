using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_RDV.Models.EntityFramework
{
    [Table("t_j_subscription_sub")]
    public class Subscription
    {
        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("ofc_id")]
        public int OfficeId { get; set; }

        // Navigation property
        [InverseProperty("Subscriptions"), ForeignKey("UserId")]
        public User User { get; set; }
        [InverseProperty("Subscriptions"), ForeignKey("OfficeId")]
        public Office Office { get; set; }
    }
}
