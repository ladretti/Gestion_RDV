namespace Gestion_RDV.Models.EntityFramework
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_e_user_usr")]
    public class User
    {
        [Key, Column("usr_id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Column("usr_first_name"), Required]
        public string FirstName { get; set; }

        [Column("usr_last_name"), Required]
        public string LastName { get; set; }

        [Column("usr_email"), Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "L'adresse email n'est pas valide.")]
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
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public string Telephone { get; set; }



        //Foreign Key
        [Column("adr_id")]
        public int? AdresseId { get; set; }

        // Navigation property
        [InverseProperty("User")]
        public Office Office { get; set; }

        [ForeignKey("AdresseId"), InverseProperty("Users")]
        public Address Adresse { get; set; }
        
        [InverseProperty("User")]
        public virtual ICollection<LikePost>? LikesPosts { get; }
        [InverseProperty("User")]
        public virtual ICollection<LikeReview>? LikesReview { get; }
         [InverseProperty("User")]
        public virtual ICollection<Subscription>? Subscriptions { get; }
        [InverseProperty("User")]
        public virtual ICollection<Comment>? Comments { get; }

        [InverseProperty("User")]
        public virtual ICollection<RendezVous>? RendezVous { get; }
        [InverseProperty("User")]
        public virtual ICollection<Notification>? Notifications { get; }
        [InverseProperty("User")]
        public virtual ICollection<Message>? Messages { get; }
        [InverseProperty("User")]
        public virtual ICollection<ConversationUser>? ConversationsUser { get; }
        [InverseProperty("User")]
        public virtual ICollection<SocialMediaAccount>? Socials { get; }
        [InverseProperty("User")]
        public virtual ICollection<Post>? Posts { get; }


    }

}
