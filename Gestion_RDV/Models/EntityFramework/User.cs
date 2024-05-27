namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("FirstName"), Required]
        public string FirstName { get; set; }

        [Column("LastName"), Required]
        public string LastName { get; set; }

        [Column("Email"), Required/*, Unique*/]
        public string Email { get; set; }

        [Column("Password"), Required]
        public string Password { get; set; }

        [Column("BirthDate"), Required]
        public DateTime BirthDate { get; set; }

        [Column("Activated")]
        public bool Activated { get; set; } = false;

        [Column("SecretToken")]
        public string SecretToken { get; set; }

        [Column("Role"), Required]
        public string Role { get; set; }

        [Column("Sexe")]
        public string Sexe { get; set; }


        public virtual ICollection<Conversation> Conversation { get; set; } = new List<Conversation>();
        public virtual Profile Profile { get; set; }

    }

}
