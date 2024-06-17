namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("t_e_notification_ntf")]
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }


        [StringLength(100), Column("ntf_title")]
        public string Title { get; set; }

        [Column("ntf_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
        [Column("rdv_id")]
        public int RendezVousId { get; set; }
        [Column("ofc_id")]
        public int OfficeId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("Notifications")]
        public User User { get; set; } 

        [ForeignKey("RendezVousId"), InverseProperty("Notifications")]
        public RendezVous RendezVous { get; set; }

        [ForeignKey("OfficeId"), InverseProperty("Notifications")]
        public Office Office { get; set; }


    }

}
