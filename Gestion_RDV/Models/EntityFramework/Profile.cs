namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Profiles")]
    public class Profile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User"), Column("UserId")]
        public int UserId { get; set; }

        [Column("Diplome")]
        public string Diplome { get; set; }

        [Column("ImageDiplome")]
        public string ImageDiplome { get; set; }

        [Column("Rating")]
        public double Rating { get; set; }

        [Column("DomainePrincipal")]
        public string DomainePrincipal { get; set; }

        [Column("CV")]
        public string CV { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Metier")]
        public string Metier { get; set; }

        [Column("Avatar")]
        public string Avatar { get; set; }

        [Column("PrixPCR")]
        public double PrixPCR { get; set; }

        [Column("Video")]
        public string Video { get; set; }

        [Column("Nbyes")]
        public int Nbyes { get; set; }

        [Column("Nbno")]
        public int Nbno { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Address Address { get; set; }
        public virtual Telephone Telephone { get; set; }
        public virtual Social Social { get; set; }
        public virtual ICollection<Availability> Availables { get; set; } = new List<Availability>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Profile> Abonnes { get; set; } = new List<Profile>();
        public virtual ICollection<string> DomaineSecondaires { get; set; } = new List<string>();
        public virtual ICollection<string> Tags { get; set; } = new List<string>();
    }
}
