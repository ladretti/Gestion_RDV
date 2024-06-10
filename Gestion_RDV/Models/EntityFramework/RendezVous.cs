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

        [ForeignKey("Profile"), Column("rdv_professionel_id")]
        public int ProfessionelId { get; set; }

        [ForeignKey("Profile"), Column("rdv_patient_id")]
        public int PatientId { get; set; }

        [Column("rdv_start_date")]
        public DateTime StartDate { get; set; }

        [Column("rdv_end_date")]
        public DateTime EndDate { get; set; }

        [ForeignKey("Etat"), Column("rdv_etat_id")]
        public int EtatId { get; set; }

        [Column("rdv_type_rendezvous")]
        public string TypeRendezVous { get; set; }

        [Column("rdv_description")]
        public string Description { get; set; }

        [Column("rdv_prix")]
        public double Prix { get; set; }

        [Column("rdv_id_event")]
        public int Idevent { get; set; }

        [Column("rdv_fichier_joint")]
        public string FichierJoint { get; set; }

    }

}
