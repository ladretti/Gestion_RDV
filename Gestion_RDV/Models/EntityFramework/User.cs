namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_user_usr")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column("usr_first_name"), Required]
        public string FirstName { get; set; }

        [Column("usr_last_name"), Required]
        public string LastName { get; set; }

        [Column("usr_email"), Required]
        public string Email { get; set; }

        [Column("usr_password"), Required]
        public string Password { get; set; }

        [Column("usr_birth_date"), Required]
        public DateTime BirthDate { get; set; }

        [Column("usr_activated")]
        public bool Activated { get; set; } = false;

        [Column("usr_avatar")]
        public string Avatar { get; set; }

        [Column("usr_secret_token")]
        public string SecretToken { get; set; }
        [Column("usr_role"), Required]
        public string Role { get; set; }
     

        [Column("usr_sexe")]
        public string Sexe { get; set; }

        [Column("usr_telephone")]
        public string Telephone { get; set; }


        // Navigation property
        public Office Office { get; set; }
    

    }

}
