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

        [ForeignKey("RendezVous"), Column("msg_rendezvous_id")]
        public int RendezVousId { get; set; }

        [ForeignKey("Etat"), Column("msg_etat_id")]
        public int EtatId { get; set; }

        [StringLength(100), Column("msg_title")]
        public string Title { get; set; }

        [Column("msg_date")]
        public DateTime Date { get; set; } = DateTime.Now;


        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("Notifications")]
        public User User { get; set; }
    }

}
