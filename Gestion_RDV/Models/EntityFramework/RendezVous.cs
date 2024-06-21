namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("t_e_rendezvous_rdv")]
    public class RendezVous
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("rdv_id")]
        public int RendezVousId { get; set; }

        [Column("rdv_start_date")]
        public DateTime StartDate { get; set; }

        [Column("rdv_end_date")]
        public DateTime EndDate { get; set; }

        [Column("rdv_description")]
        public string Description { get; set; }

        [Column("rdv_prix")]
        public double Prix { get; set; }

        [Column("rdv_fichier_joint")]
        public string? FichierJoint { get; set; }

        //ForeignKey
        [Column("usr_id")]
        public int UserId { get; set; }
         [Column("ofc_id")]
        public int OfficeId { get; set; }

        // Navigation property
        [ForeignKey("UserId"), InverseProperty("RendezVous")]
        public User? User { get; set; }

        [ForeignKey("OfficeId"), InverseProperty("RendezVous")]
        public Office? Office { get; set; }

        [InverseProperty("RendezVous")]
        public Review? Review { get; set; }

        [InverseProperty("RendezVous")]
        public virtual ICollection<Notification>? Notifications { get; }

        [InverseProperty("RendezVous")]
        public Facture? Facture { get; set; }

    }

}
