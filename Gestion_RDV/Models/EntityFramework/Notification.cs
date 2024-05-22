namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_notification")]
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("Professionel"), Column("professionel_id")]
        public int ProfessionelId { get; set; }
        public virtual Profile Professionel { get; set; }

        [ForeignKey("Patient"), Column("patient_id")]
        public int PatientId { get; set; }
        public virtual Profile Patient { get; set; }

        [ForeignKey("Sender"), Column("sender_id")]
        public int SenderId { get; set; }
        public virtual Profile Sender { get; set; }

        [ForeignKey("Receiver"), Column("receiver_id")]
        public int ReceiverId { get; set; }
        public virtual Profile Receiver { get; set; }

        [ForeignKey("RendezVous"), Column("rendezvous_id")]
        public int RendezVousId { get; set; }
        public virtual RendezVous RendezVous { get; set; }

        [Column("etat"), StringLength(50)]
        public string Etat { get; set; }

        [Column("title"), StringLength(100)]
        public string Title { get; set; }

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }

}
