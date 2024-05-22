namespace Gestion_RDV.Models.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_user")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }

        [Required, Column("first_name"), StringLength(100)]
        public string FirstName { get; set; }

        [Required, Column("last_name"), StringLength(100)]
        public string LastName { get; set; }

        [Required, Column("email"), StringLength(255)]
        public string Email { get; set; }

        [Required, Column("password"), StringLength(255)]
        public string Password { get; set; }

        [Required, Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("activated")]
        public bool Activated { get; set; } = false;

        [Column("secret_token"), StringLength(255)]
        public string SecretToken { get; set; }

        [Required, Column("role"), StringLength(50)]
        public string Role { get; set; }

        [Column("sexe"), StringLength(50)]
        public string Sexe { get; set; }
    }

}
