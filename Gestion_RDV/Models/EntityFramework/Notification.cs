namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum Etats
    {
        Read,
        Unread
    }

    [Table("Notifications")]
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("ProfessionelId")]
        public int ProfessionelId { get; set; }

        [ForeignKey("Profile"), Column("PatientId")]
        public int PatientId { get; set; }

        [ForeignKey("Profile"), Column("SenderId")]
        public int SenderId { get; set; }

        [ForeignKey("Profile"), Column("ReceiverId")]
        public int ReceiverId { get; set; }

        [ForeignKey("RendezVous"), Column("RendezVousId")]
        public int RendezVousId { get; set; }

        [Column("Etat")]
        public Etats Etat { get; set; }

        [StringLength(100), Column("Title")]
        public string Title { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Profile Professionel { get; set; }
        public virtual Profile Patient { get; set; }
        public virtual Profile Sender { get; set; }
        public virtual Profile Receiver { get; set; }
        public virtual RendezVous RendezVous { get; set; }
    }

}
