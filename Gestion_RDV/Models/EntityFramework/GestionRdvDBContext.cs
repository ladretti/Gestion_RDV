using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Gestion_RDV.Models.EntityFramework
{
    public class GestionRdvDbContext : DbContext
    {
        public GestionRdvDbContext()
        {

        }
        public GestionRdvDbContext(DbContextOptions<GestionRdvDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationUser> ConversationsUser { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LikePost> LikesPost { get; set; }
        public DbSet<LikeReview> LikesReview { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Office> Officies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMediaAccount> SocialMediaAccounts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=GestionRDV; uid=postgres; password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("t_e_address_adr");

                entity.HasKey(e => e.AdresseId)
                      .HasName("PK_Address");

                entity.Property(e => e.AdresseId)
                      .HasColumnName("adr_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Adresse)
                      .HasColumnName("adr_adresse")
                      .IsRequired();

                entity.Property(e => e.Ville)
                      .HasColumnName("adr_ville")
                      .IsRequired();

                entity.Property(e => e.CodePostal)
                      .HasColumnName("adr_codepostal")
                      .IsRequired();

                entity.HasMany(e => e.Users)
                      .WithOne(e => e.Adresse)
                      .HasForeignKey(e => e.AdresseId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Offices)
                      .WithOne(e => e.Adresse)
                      .HasForeignKey(e => e.AdresseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("t_e_availability_avb");

                entity.HasKey(e => e.AvailabilityId)
                      .HasName("PK_Availability");

                entity.Property(e => e.AvailabilityId)
                      .HasColumnName("avb_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.StartDate)
                      .HasColumnName("avb_start_date")
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .HasColumnName("avb_end_date")
                      .IsRequired();

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id")
                      .IsRequired();

                entity.HasOne(d => d.Office)
                      .WithMany(p => p.Availabilities)
                      .HasForeignKey(d => d.OfficeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Availability_Office");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("t_e_comment_cmt");

                entity.HasKey(e => e.CommentId)
                      .HasName("PK_Comment");

                entity.Property(e => e.CommentId)
                      .HasColumnName("cmt_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Text)
                      .HasColumnName("cmt_text")
                      .IsRequired();

                entity.Property(e => e.Date)
                      .HasColumnName("cmt_date")
                      .IsRequired();

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id")
                      .IsRequired();

                entity.Property(e => e.ReviewId)
                      .HasColumnName("rvw_id")
                      .IsRequired();

                entity.HasOne(d => d.User)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Comment_User");

                entity.HasOne(d => d.Review)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(d => d.ReviewId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Comment_Review");
            });
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("t_e_conversation_cnv");

                entity.HasKey(e => e.ConversationId)
                      .HasName("PK_Conversation");

                entity.Property(e => e.ConversationId)
                      .HasColumnName("cnv_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .HasColumnName("cnv_name")
                      .IsRequired()
                      .HasMaxLength(100);
            });
            modelBuilder.Entity<ConversationUser>(entity =>
            {
                entity.ToTable("t_j_conversationuser_cvu");

                entity.HasKey(e => new { e.UserId, e.ConversationId })
                      .HasName("PK_ConversationUser");

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id");

                entity.Property(e => e.ConversationId)
                      .HasColumnName("cnv_id");

                entity.HasOne(e => e.User)
                      .WithMany(e => e.ConversationsUser)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_ConversationUser_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Conversation)
                      .WithMany(e => e.ConversationsUser)
                      .HasForeignKey(e => e.ConversationId)
                      .HasConstraintName("FK_ConversationUser_Conversation")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Facture>(entity =>
            {
                entity.ToTable("t_e_facture_fct");

                entity.HasKey(e => e.FactureId)
                      .HasName("PK_Facture");

                entity.Property(e => e.FactureId)
                      .HasColumnName("fct_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.PrixAvantTva)
                      .HasColumnName("fct_prix_avant_tva")
                      .IsRequired();

                entity.Property(e => e.Informations)
                      .HasColumnName("fct_infos")
                      .IsRequired(false);

                entity.Property(e => e.Tva)
                      .HasColumnName("fct_tva")
                      .IsRequired();

                entity.Property(e => e.RendezVousId)
                      .HasColumnName("rdv_id")
                      .IsRequired();

                entity.HasOne(e => e.RendezVous)
                      .WithOne(r => r.Facture)
                      .HasForeignKey<Facture>(e => e.RendezVousId)
                      .HasConstraintName("FK_Facture_RendezVous")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<LikePost>(entity =>
            {
                entity.ToTable("t_j_like_post_lke");

                entity.HasKey(e => new { e.UserId, e.PostId })
                      .HasName("PK_LikePost");

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id");

                entity.Property(e => e.PostId)
                      .HasColumnName("pst_id");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.LikesPosts)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_LikePost_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Post)
                      .WithMany(p => p.LikesPosts)
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("FK_LikePost_Post")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<LikeReview>(entity =>
            {
                entity.ToTable("t_j_like_review_lke");

                entity.HasKey(e => new { e.UserId, e.ReviewId })
                      .HasName("PK_LikeReview");
                entity.Property(e => e.IsLiked)
                .IsRequired();

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id");

                entity.Property(e => e.ReviewId)
                      .HasColumnName("rvw_id");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.LikesReview)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_LikeReview_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Review)
                      .WithMany(r => r.LikesReview)
                      .HasForeignKey(e => e.ReviewId)
                      .HasConstraintName("FK_LikeReview_Review")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("t_j_message_msg");

                entity.HasKey(e => new { e.UserId, e.ConversationId, e.Created })
                      .HasName("PK_Message");

                entity.Property(e => e.Created)
                      .HasColumnName("msg_created")
                      .IsRequired();

                entity.Property(e => e.Text)
                      .HasColumnName("msg_text")
                      .IsRequired();

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id")
                      .IsRequired();

                entity.Property(e => e.ConversationId)
                      .HasColumnName("cnv_id")
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Messages)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_Message_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Conversation)
                      .WithMany(c => c.Messages)
                      .HasForeignKey(e => e.ConversationId)
                      .HasConstraintName("FK_Message_Conversation")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("t_e_notification_ntf");

                entity.HasKey(e => e.NotificationId)
                      .HasName("PK_Notification");

                entity.Property(e => e.NotificationId)
                      .HasColumnName("ntf_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                      .HasColumnName("ntf_title")
                      .HasMaxLength(100);

                entity.Property(e => e.Date)
                      .HasColumnName("ntf_date")
                      .IsRequired();

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id");

                entity.Property(e => e.RendezVousId)
                      .HasColumnName("rdv_id");

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notifications)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_Notification_User")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.RendezVous)
                      .WithMany(r => r.Notifications)
                      .HasForeignKey(e => e.RendezVousId)
                      .HasConstraintName("FK_Notification_RendezVous")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Office)
                      .WithMany(o => o.Notifications)
                      .HasForeignKey(e => e.OfficeId)
                      .HasConstraintName("FK_Notification_Office")
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("t_e_office_ofc");

                entity.HasKey(e => e.OfficeId)
                      .HasName("PK_Office");

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Diplome)
                      .HasColumnName("ofc_diplome");

                entity.Property(e => e.ImageDiplome)
                      .HasColumnName("ofc_image_diplome");

                entity.Property(e => e.DomainePrincipal)
                      .HasColumnName("ofc_domaine_principal");

                entity.Property(e => e.CV)
                      .HasColumnName("ofc_cv");

                entity.Property(e => e.Description)
                      .HasColumnName("ofc_description");

                entity.Property(e => e.Metier)
                      .HasColumnName("ofc_metier");

                entity.Property(e => e.PrixPCR)
                      .HasColumnName("ofc_prix_pcr");

                entity.Property(e => e.Video)
                      .HasColumnName("ofc_video");

                entity.Property(e => e.Date)
                      .HasColumnName("ofc_date")
                      .IsRequired();

                entity.Property(e => e.Telephone)
                      .HasColumnName("ofc_telephone");

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id")
                      .IsRequired();

                entity.Property(e => e.AdresseId)
                      .HasColumnName("adr_id")
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithOne(u => u.Office)
                      .HasForeignKey<Office>(e => e.UserId)
                      .HasConstraintName("FK_Office_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Adresse)
                      .WithMany(a => a.Offices)
                      .HasForeignKey(e => e.AdresseId)
                      .HasConstraintName("FK_Office_Address")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("t_e_post_pst");

                entity.HasKey(e => e.PostId).HasName("PK_t_e_post_pst");

                entity.Property(e => e.PostId).HasColumnName("pst_id").ValueGeneratedOnAdd();

                entity.Property(e => e.Text).HasColumnName("pst_text").IsRequired();

                entity.Property(e => e.Date).HasColumnName("pst_date").HasDefaultValueSql("NOW()");

                entity.Property(e => e.Type).HasColumnName("pst_type").HasDefaultValue("text");

                entity.Property(e => e.UserId).HasColumnName("usr_id");

                entity.Property(e => e.ParentPostId).HasColumnName("p_pst_id").IsRequired(false);


                entity.HasOne(e => e.User)
                    .WithMany(u => u.Posts)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_t_e_post_pst_t_e_user_usr_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ParentPost)
                    .WithMany(p => p.ChildPosts)
                    .HasForeignKey(e => e.ParentPostId)
                    .HasConstraintName("FK_t_e_post_pst_t_e_post_p_pst_id")
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<RendezVous>(entity =>
            {
                entity.ToTable("t_e_rendezvous_rdv");

                entity.HasKey(e => e.RendezVousId)
                      .HasName("PK_RendezVous");

                entity.Property(e => e.RendezVousId)
                      .HasColumnName("rdv_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.StartDate)
                      .HasColumnName("rdv_start_date")
                      .IsRequired();

                entity.Property(e => e.EndDate)
                      .HasColumnName("rdv_end_date")
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("rdv_description");

                entity.Property(e => e.Prix)
                      .HasColumnName("rdv_prix");

                entity.Property(e => e.FichierJoint)
                      .HasColumnName("rdv_fichier_joint")
                      .IsRequired(false);

                entity.Property(e => e.UserId)
                      .HasColumnName("usr_id")
                      .IsRequired();

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id")
                      .IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.RendezVous)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_RendezVous_User")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Office)
                      .WithMany(o => o.RendezVous)
                      .HasForeignKey(e => e.OfficeId)
                      .HasConstraintName("FK_RendezVous_Office")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Review)
                      .WithOne(r => r.RendezVous)
                      .HasForeignKey<Review>(r => r.RendezVousId)
                      .HasConstraintName("FK_Review_RendezVous")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Facture)
                      .WithOne(f => f.RendezVous)
                      .HasForeignKey<Facture>(f => f.RendezVousId)
                      .HasConstraintName("FK_Facture_RendezVous")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("t_e_review_rvw");

                entity.HasKey(e => e.ReviewId)
                      .HasName("PK_Review");

                entity.Property(e => e.ReviewId)
                      .HasColumnName("rvw_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                      .HasColumnName("rvw_description")
                      .HasMaxLength(500)
                      .IsRequired();

                entity.Property(e => e.Date)
                      .HasColumnName("rvw_date")
                      .IsRequired();

                entity.Property(e => e.Type)
                      .HasColumnName("rvw_type")
                      .HasMaxLength(50);

                entity.Property(e => e.RendezVousId)
                      .HasColumnName("rdv_id")
                      .IsRequired();

                entity.HasOne(e => e.RendezVous)
                      .WithOne(r => r.Review)
                      .HasForeignKey<Review>(e => e.RendezVousId)
                      .HasConstraintName("FK_Review_RendezVous")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Comments)
                      .WithOne(c => c.Review)
                      .HasForeignKey(c => c.ReviewId)
                      .HasConstraintName("FK_Comment_Review")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.LikesReview)
                      .WithOne(l => l.Review)
                      .HasForeignKey(l => l.ReviewId)
                      .HasConstraintName("FK_LikeReview_Review")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.Note)
                      .HasColumnName("rvw_note")
                      .IsRequired();
            });
            modelBuilder.Entity<SocialMediaAccount>(entity =>
            {
                entity.ToTable("t_e_socialmediaaccount_sma");

                entity.HasKey(e => e.SocialMediaAccountId)
                      .HasName("PK_SocialMediaAccount");

                entity.Property(e => e.SocialMediaAccountId)
                      .HasColumnName("sma_id")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Platform)
                      .HasColumnName("sma_platform")
                      .HasMaxLength(50);

                entity.Property(e => e.Url)
                      .HasColumnName("sma_url")
                      .HasMaxLength(255);

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id")
                      .IsRequired();

                entity.HasOne(e => e.Office)
                      .WithMany(u => u.Socials)
                      .HasForeignKey(e => e.OfficeId)
                      .HasConstraintName("FK_SocialMediaAccount_Office")
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.ToTable("t_j_subscription_sub");

                entity.HasKey(e => new { e.UserId, e.OfficeId })
                    .HasName("PK_Subscription");

                entity.Property(e => e.UserId)
                    .HasColumnName("usr_id");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("ofc_id");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Subscriptions)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Subscription_User")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Office)
                    .WithMany(o => o.Subscriptions)
                    .HasForeignKey(e => e.OfficeId)
                    .HasConstraintName("FK_Subscription_Office")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<User>(entity =>
                {
                    entity.ToTable("t_e_user_usr");

                    entity.HasKey(e => e.UserId)
                          .HasName("PK_User");

                    entity.Property(e => e.UserId)
                          .HasColumnName("usr_id")
                          .ValueGeneratedOnAdd();

                    entity.Property(e => e.FirstName)
                          .HasColumnName("usr_first_name")
                          .IsRequired();

                    entity.Property(e => e.LastName)
                          .HasColumnName("usr_last_name")
                          .IsRequired();

                    entity.Property(e => e.Email)
                          .HasColumnName("usr_email")
                          .IsRequired();
                    entity.HasIndex(e => e.Email)
                          .IsUnique();

                    entity.Property(e => e.Password)
                          .HasColumnName("usr_password")
                          .IsRequired();

                    entity.Property(e => e.BirthDate)
                          .HasColumnName("usr_birth_date")
                          .IsRequired();

                    entity.Property(e => e.Activated)
                          .HasColumnName("usr_activated")
                          .HasDefaultValue(false);

                    entity.Property(e => e.Avatar)
                          .HasColumnName("usr_avatar")
                          .IsRequired(false);

                    entity.Property(e => e.SecretToken)
                          .HasColumnName("usr_secret_token")
                          .IsRequired(false);

                    entity.Property(e => e.Role)
                          .HasColumnName("usr_role")
                          .IsRequired();

                    entity.Property(e => e.Sexe)
                          .HasColumnName("usr_sexe")
                          .IsRequired(false);

                    entity.Property(e => e.Telephone)
                          .HasColumnName("usr_telephone")
                          .IsRequired(false);

                    entity.Property(e => e.AdresseId)
                          .HasColumnName("adr_id")
                          .IsRequired(false);

                    entity.HasOne(e => e.Adresse)
                          .WithMany(a => a.Users)
                          .HasForeignKey(e => e.AdresseId)
                          .HasConstraintName("FK_User_Address")
                          .OnDelete(DeleteBehavior.SetNull);
                });


            base.OnModelCreating(modelBuilder);
        }

    }

}
