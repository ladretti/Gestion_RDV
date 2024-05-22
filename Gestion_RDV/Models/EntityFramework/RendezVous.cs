namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_rendezvous")]
    public class RendezVous
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("Professionel"), Column("professionel_id")]
        public int ProfessionelId { get; set; }
        public virtual Profile Professionel { get; set; }

        [ForeignKey("Patient"), Column("patient_id")]
        public int PatientId { get; set; }
        public virtual Profile Patient { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("etat"), StringLength(50)]
        public string Etat { get; set; }

        [Column("type_rendezvous"), StringLength(100)]
        public string TypeRendezVous { get; set; }

        [Column("description"), StringLength(500)]
        public string Description { get; set; }

        [Column("prix")]
        public decimal Prix { get; set; }

        [Column("idevent")]
        public Guid Idevent { get; set; }

        [Column("fichier_joint"), StringLength(255)]
        public string FichierJoint { get; set; }
    }

}
