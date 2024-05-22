namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_profile")]
    public class Profile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [ForeignKey("User"), Column("user_id")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual Address Address { get; set; }
        public virtual Telephone Telephone { get; set; }

        [Column("diplome")]
        public ICollection<string> Diplome { get; set; } = new List<string>();

        [Column("image_diplome"), StringLength(255)]
        public string ImageDiplome { get; set; }

        [Column("rating")]
        public double Rating { get; set; }

        [Column("domaine_principal"), StringLength(100)]
        public string DomainePrincipal { get; set; }

        [Column("domaine_secondaires")]
        public ICollection<string> DomaineSecondaires { get; set; } = new List<string>();

        public virtual ICollection<Profile> Abonnes { get; set; } = new List<Profile>();

        public virtual Social Social { get; set; }

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column("cv"), StringLength(255)]
        public string CV { get; set; }

        [Column("description"), StringLength(500)]
        public string Description { get; set; }

        [Column("metier"), StringLength(100)]
        public string Metier { get; set; }

        [Column("tags")]
        public ICollection<string> Tags { get; set; } = new List<string>();

        [Column("avatar"), StringLength(255)]
        public string Avatar { get; set; }

        public virtual ICollection<Availability> Available { get; set; } = new List<Availability>();

        [Column("prix_pcr")]
        public decimal PrixPCR { get; set; }

        [Column("video"), StringLength(255)]
        public string Video { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [Column("nbyes")]
        public int Nbyes { get; set; }

        [Column("nbno")]
        public int Nbno { get; set; }
    }
}
