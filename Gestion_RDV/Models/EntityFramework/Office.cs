namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_office_ofc")]
    public class Office
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("ofc_id")]
        public int Id { get; set; }

        [Column("ofc_diplome")]
        public string Diplome { get; set; }

        [Column("ofc_image_diplome")]
        public string ImageDiplome { get; set; }

        [Column("ofc_rating")]
        public double Rating { get; set; }

        [Column("ofc_domaine_principal")]
        public string DomainePrincipal { get; set; }

        [Column("ofc_cv")]
        public string CV { get; set; }

        [Column("ofc_description")]
        public string Description { get; set; }

        [Column("ofc_metier")]
        public string Metier { get; set; }

        [Column("ofc_prix_pcr")]
        public double PrixPCR { get; set; }

        [Column("ofc_video")]
        public string Video { get; set; }

        [Column("ofc_nb_yes")]
        public int Nbyes { get; set; }

        [Column("ofc_nb_no")]
        public int Nbno { get; set; }

        [Column("ofc_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("ofc_telephone")]
        public string Telephone { get; set; }

        [ForeignKey("User"), Column("ofc_user_id")]
        public int UserId { get; set; }

        // Navigation property
        public User User { get; set; }

    }
}
