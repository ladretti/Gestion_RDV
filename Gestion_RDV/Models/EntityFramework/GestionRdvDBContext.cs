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
        public DbSet<MedicalInfo> MedicalInfos { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<OfficeEquipment> OfficeEquipments { get; set; }
        public DbSet<Equipment> Equipments { get; set; }

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

                entity.Property(e => e.Reserve)
                      .HasColumnName("avb_reserve")
                      .IsRequired();

                entity.Property(e => e.OfficeId)
                      .HasColumnName("ofc_id")
                      .IsRequired();

                entity.HasOne(d => d.Office)
                      .WithMany(p => p.Availabilities)
                      .HasForeignKey(d => d.OfficeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Availability_Office");

                entity.HasOne(d => d.RendezVous)
                .WithOne(p => p.Availability)
                .HasForeignKey<RendezVous>(d => d.AvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);
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
                      .HasColumnName("ofc_image_diplome")
                      .IsRequired(false);

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

                entity.HasMany(e => e.OfficeEquipments)
                   .WithOne(p => p.Office)
                   .HasForeignKey(p => p.OfficeId)
                   .HasConstraintName("FK_OfficeEquipment_Office");
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
                    .OnDelete(DeleteBehavior.Cascade);
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

                entity.Property(e => e.AvailabilityId)
                      .HasColumnName("avb_id")
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

                entity.HasOne(d => d.Availability)
                .WithOne(p => p.RendezVous)
                .HasForeignKey<RendezVous>(d => d.AvailabilityId)
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

                    entity.Property(e => e.SecretTokenValidity)
                          .HasColumnName("usr_secret_token_validity")
                          .IsRequired(false);

                    entity.Property(e => e.Role)
                          .HasColumnName("usr_role")
                          .HasConversion(
                              v => v.ToString(),
                              v => (UserRole)Enum.Parse(typeof(UserRole), v))
                          .IsRequired();

                    entity.Property(e => e.Weigh).HasColumnName("mif_weight").IsRequired(false);

                    entity.Property(e => e.Height).HasColumnName("mif_height").IsRequired(false);

                    entity.Property(e => e.BloodType).HasColumnName("mif_blood_type").IsRequired(false);

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
                    entity.HasCheckConstraint("CHK_User_Role", $"\"usr_role\" IN ({string.Join(", ", Enum.GetNames(typeof(UserRole)).Select(r => $"'{r}'"))})");

                });


            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.ToTable("t_e_diagnosis_dia");

                entity.HasKey(e => e.DiagnosisId).HasName("PK_DiagnosisId");

                entity.Property(e => e.DiagnosisId).HasColumnName("dia_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.DiagnosisDate).HasColumnName("dia_diagnosis_date").IsRequired();
                entity.Property(e => e.Code).HasColumnName("dia_code").IsRequired(false);
                entity.Property(e => e.Description).HasColumnName("dia_description").IsRequired(false);
                entity.Property(e => e.DiagnosisDetails).HasColumnName("dia_diagnosis_details").IsRequired(false);

                entity.Property(e => e.UserId).HasColumnName("usr_id").IsRequired();
                entity.Property(e => e.RendezVousId).HasColumnName("rdv_id").IsRequired();

                entity.HasOne(d => d.User)
                      .WithMany(p => p.Diagnoses)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Diagnosis_User");

                entity.HasOne(d => d.RendezVous)
                      .WithMany(p => p.Diagnoses)
                      .HasForeignKey(d => d.RendezVousId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Diagnosis_RendezVous");

                entity.HasMany(e => e.Prescriptions)
                      .WithOne(p => p.Diagnosis)
                      .HasForeignKey(p => p.DiagnosisId)
                      .HasConstraintName("FK_Prescription_Diagnosis");
            });

            modelBuilder.Entity<MedicalInfo>(entity =>
            {
                entity.ToTable("t_e_medicalinfo_mif");

                entity.HasKey(e => e.MedicalInfoId).HasName("PK_InfoId");

                entity.Property(e => e.MedicalInfoId).HasColumnName("mif_id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(e => e.Category)
                      .HasColumnName("mif_category")
                      .HasConversion(
                          v => v.ToString(),
                          v => (FileType)Enum.Parse(typeof(FileType), v))
                      .IsRequired();
                entity.Property(e => e.Description).HasColumnName("mif_description").IsRequired(false);

                entity.Property(e => e.UserId).HasColumnName("usr_id").IsRequired();

                entity.HasOne(d => d.User)
                      .WithMany(p => p.MedicalInfos)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Diagnosis_User");

                entity.HasCheckConstraint("CHK_MedicalInfo_Category", $"\"mif_category\" IN ({string.Join(", ", Enum.GetNames(typeof(FileType)).Select(f => $"'{f}'"))})");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.ToTable("t_e_medication_med");

                entity.HasKey(e => e.MedicationId).HasName("PK_Medication");

                entity.Property(e => e.MedicationId).HasColumnName("med_id");
                entity.Property(e => e.Name).HasColumnName("med_name").IsRequired();
                entity.Property(e => e.Dosage).HasColumnName("med_dosage");

                // Relationships
                entity.HasMany(e => e.Prescriptions)
                    .WithOne(p => p.Medication)
                    .HasForeignKey(p => p.MedicationId)
                    .HasConstraintName("FK_Prescription_Medication");
            });

            // Configuration for Prescription
            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.ToTable("t_e_prescription_pre");

                entity.HasKey(e => e.PrescriptionId).HasName("PK_Prescription");

                entity.Property(e => e.PrescriptionId).HasColumnName("pre_id");
                entity.Property(e => e.Description).HasColumnName("pre_description").IsRequired(false);
                entity.Property(e => e.DiagnosisId).HasColumnName("dia_id");
                entity.Property(e => e.MedicationId).HasColumnName("med_id");

                // Relationships
                entity.HasOne(e => e.Diagnosis)
                    .WithMany(d => d.Prescriptions)
                    .HasForeignKey(e => e.DiagnosisId)
                    .HasConstraintName("FK_Prescription_Diagnosis");

                entity.HasOne(e => e.Medication)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(e => e.MedicationId)
                    .HasConstraintName("FK_Prescription_Medication");
            });
            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("t_e_equipment_equ");

                entity.HasKey(e => e.EquipmentId).HasName("PK_Equipment");

                entity.Property(e => e.EquipmentId).HasColumnName("equ_id");
                entity.Property(e => e.Name).HasColumnName("equ_name").IsRequired();

                // Relationships
                entity.HasMany(e => e.OfficeEquipments)
                    .WithOne(p => p.Equipment)
                    .HasForeignKey(p => p.EquipmentId)
                    .HasConstraintName("FK_OfficeEquipment_Equipment");
            });
            modelBuilder.Entity<OfficeEquipment>(entity =>
                {
                    entity.ToTable("t_j_office_equipment_ofe");

                    entity.HasKey(e => new { e.EquipmentId, e.OfficeId }).HasName("PK_OfficeEquipment");

                    entity.Property(e => e.OfficeId).HasColumnName("ofc_id");
                    entity.Property(e => e.EquipmentId).HasColumnName("equ_id");
                    entity.Property(e => e.Etat).HasColumnName("ofe_state").IsRequired();
                    entity.Property(e => e.FutureUpdate).HasColumnName("ofe_future_update").IsRequired();
                    entity.Property(e => e.LastUpdate).HasColumnName("ofe_last_update").IsRequired();

                    // Relationships
                    entity.HasOne(e => e.Office)
                     .WithMany(u => u.OfficeEquipments)
                     .HasForeignKey(e => e.OfficeId)
                     .HasConstraintName("FK_OfficeEquipment_Office")
                     .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(e => e.Equipment)
                        .WithMany(o => o.OfficeEquipments)
                        .HasForeignKey(e => e.EquipmentId)
                        .HasConstraintName("FK_OfficeEquipment_Equipment")
                        .OnDelete(DeleteBehavior.Cascade);
                });



            base.OnModelCreating(modelBuilder);
        }

    }

}
