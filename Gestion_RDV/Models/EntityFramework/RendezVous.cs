namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum Etats
    {
        Free,
        Occupied,
        Confirmed
    }

    [Table("RendezVous")]
    public class RendezVous
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profile"), Column("ProfessionelId")]
        public int ProfessionelId { get; set; }

        [ForeignKey("Profile"), Column("PatientId")]
        public int PatientId { get; set; }

        [Column("StartDate")]
        public DateTime StartDate { get; set; }

        [Column("EndDate")]
        public DateTime EndDate { get; set; }

        [Column("Etat")]
        public Etats Etat { get; set; }

        [Column("TypeRendezVous")]
        public string TypeRendezVous { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Prix")]
        public double Prix { get; set; }

        [Column("Idevent")]
        public int Idevent { get; set; }

        [Column("FichierJoint")]
        public string FichierJoint { get; set; }

        // Navigation properties
        public virtual Profile Professionel { get; set; }
        public virtual Profile Patient { get; set; }
    }

}
