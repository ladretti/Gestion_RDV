namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("t_e_notification_ntf")]
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("msg_professionel_id")]
        public int ProfessionelId { get; set; }

        [ForeignKey("Profile"), Column("msg_patient_id")]
        public int PatientId { get; set; }

        [ForeignKey("Profile"), Column("msg_sender_id")]
        public int SenderId { get; set; }

        [ForeignKey("Profile"), Column("msg_receiver_id")]
        public int ReceiverId { get; set; }

        [ForeignKey("RendezVous"), Column("msg_rendezvous_id")]
        public int RendezVousId { get; set; }

        [ForeignKey("Etat"), Column("msg_etat_id")]
        public int EtatId { get; set; }

        [StringLength(100), Column("msg_title")]
        public string Title { get; set; }

        [Column("msg_date")]
        public DateTime Date { get; set; } = DateTime.Now;

    }

}
