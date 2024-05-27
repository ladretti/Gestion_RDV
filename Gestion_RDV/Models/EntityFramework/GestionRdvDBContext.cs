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

        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Devis> Devis { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=GestionRDV; uid=postgres; password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Addresses");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Gouvernerat)
                    .HasColumnName("Gouvernerat")
                    .HasMaxLength(100);

                entity.Property(e => e.Ville)
                    .HasColumnName("Ville")
                    .HasMaxLength(100);

                entity.Property(e => e.Cite)
                    .HasColumnName("Cite")
                    .HasMaxLength(100);

                entity.HasOne(e => e.Profile)
                    .WithOne(p => p.Address)
                    .HasForeignKey<Profile>(p => p.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("t_availability");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnName("start_date");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnName("end_date");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .HasColumnName("PostId")
                    .IsRequired();

                entity.Property(e => e.Text)
                    .HasColumnName("Text")
                    .IsRequired()
                    .HasMaxLength(1000); // Assuming a maximum length for text

                entity.Property(e => e.Date)
                    .HasColumnName("Date")
                    .HasDefaultValueSql("NOW()");

                entity.HasOne(e => e.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(e => e.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversations");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("Name");

                entity.HasMany(e => e.Messages)
                      .WithOne(m => m.Conversation)
                      .HasForeignKey(m => m.ConversationId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Participant)
                      .WithMany(u => u.Conversation);
            });
            modelBuilder.Entity<Devis>(entity =>
            {
                entity.ToTable("Devis");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProfessionelId)
                    .HasColumnName("ProfessionelId")
                    .IsRequired();

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientId")
                    .IsRequired();

                entity.Property(e => e.AppointmentId)
                    .HasColumnName("AppointmentId")
                    .IsRequired();

                entity.Property(e => e.PrixAvantTva)
                    .HasColumnName("PrixAvantTva");

                entity.Property(e => e.Tva)
                    .HasColumnName("Tva");

                entity.Property(e => e.PrixFinal)
                    .HasColumnName("PrixFinal");

                entity.HasOne(e => e.Professionel)
                    .WithMany()
                    .HasForeignKey(e => e.ProfessionelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Appointment)
                    .WithMany()
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Etat>(entity =>
            {
                entity.ToTable("Etats");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .IsRequired();
            });
            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("Likes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.PostId)
                    .HasColumnName("PostId")
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Post)
                    .WithMany()
                    .HasForeignKey(e => e.PostId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Created)
                    .HasColumnName("Created")
                    .IsRequired();

                entity.Property(e => e.From)
                    .HasColumnName("From")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Text)
                    .HasColumnName("Text")
                    .IsRequired();

                entity.Property(e => e.ConversationId)
                    .HasColumnName("ConversationId")
                    .IsRequired();

                entity.HasOne(e => e.Conversation)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(e => e.ConversationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notifications");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProfessionelId)
                    .HasColumnName("ProfessionelId")
                    .IsRequired();

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientId")
                    .IsRequired();

                entity.Property(e => e.SenderId)
                    .HasColumnName("SenderId")
                    .IsRequired();

                entity.Property(e => e.ReceiverId)
                    .HasColumnName("ReceiverId")
                    .IsRequired();

                entity.Property(e => e.RendezVousId)
                    .HasColumnName("RendezVousId")
                    .IsRequired();

                entity.Property(e => e.EtatId)
                    .HasColumnName("EtatId")
                    .IsRequired();

                entity.Property(e => e.Title)
                    .HasColumnName("Title")
                    .HasMaxLength(100);

                entity.Property(e => e.Date)
                    .HasColumnName("Date")
                    .HasDefaultValueSql("NOW()");

                entity.HasOne(e => e.Professionel)
                    .WithMany()
                    .HasForeignKey(e => e.ProfessionelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Sender)
                    .WithMany()
                    .HasForeignKey(e => e.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Receiver)
                    .WithMany()
                    .HasForeignKey(e => e.ReceiverId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RendezVous)
                    .WithMany()
                    .HasForeignKey(e => e.RendezVousId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Etat)
                    .WithMany()
                    .HasForeignKey(e => e.EtatId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.Text)
                    .HasColumnName("Text")
                    .IsRequired();

                entity.Property(e => e.Date)
                    .HasColumnName("Date")
                    .HasDefaultValueSql("NOW()");
                    
                entity.Property(e => e.Type)
                    .HasColumnName("Type")
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasDefaultValue("text");

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Likes)
                    .WithOne(l => l.Post)
                    .HasForeignKey(l => l.PostId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Comments)
                    .WithOne(c => c.Post)
                    .HasForeignKey(c => c.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profiles");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();

                entity.Property(e => e.Diplome)
                    .HasColumnName("Diplome");

                entity.Property(e => e.ImageDiplome)
                    .HasColumnName("ImageDiplome");

                entity.Property(e => e.Rating)
                    .HasColumnName("Rating");

                entity.Property(e => e.DomainePrincipal)
                    .HasColumnName("DomainePrincipal");

                entity.Property(e => e.CV)
                    .HasColumnName("CV");

                entity.Property(e => e.Description)
                    .HasColumnName("Description");

                entity.Property(e => e.Metier)
                    .HasColumnName("Metier");

                entity.Property(e => e.Avatar)
                    .HasColumnName("Avatar");

                entity.Property(e => e.PrixPCR)
                    .HasColumnName("PrixPCR");

                entity.Property(e => e.Video)
                    .HasColumnName("Video");

                entity.Property(e => e.Nbyes)
                    .HasColumnName("Nbyes");

                entity.Property(e => e.Nbno)
                    .HasColumnName("Nbno");

                entity.Property(e => e.Date)
                    .HasColumnName("Date")
                    .HasDefaultValueSql("NOW()");

                entity.HasOne(e => e.User)
                    .WithOne(u => u.Profile)
                    .HasForeignKey<Profile>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Address)
                    .WithOne(a => a.Profile)
                    .HasForeignKey<Address>(a => a.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Telephone)
                    .WithOne(t => t.Profile)
                    .HasForeignKey<Telephone>(t => t.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Social)
                    .WithOne(s => s.Profile)
                    .HasForeignKey<Social>(s => s.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Availables)
                    .WithOne(a => a.Profile)
                    .HasForeignKey(a => a.Id)
                    .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(e => e.Abonnes)
                    .WithMany()
                    .UsingEntity(j => j.ToTable("Abonnes"));

                entity.Property(e => e.DomaineSecondaires)
                    .HasColumnName("DomaineSecondaires")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.Property(e => e.Tags)
                    .HasColumnName("Tags")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });
            modelBuilder.Entity<RendezVous>(entity =>
            {
                entity.ToTable("RendezVous");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProfessionelId)
                    .HasColumnName("ProfessionelId")
                    .IsRequired();

                entity.Property(e => e.PatientId)
                    .HasColumnName("PatientId")
                    .IsRequired();

                entity.Property(e => e.StartDate)
                    .HasColumnName("StartDate")
                    .IsRequired();

                entity.Property(e => e.EndDate)
                    .HasColumnName("EndDate")
                    .IsRequired();

                entity.Property(e => e.EtatId)
                    .HasColumnName("EtatId")
                    .IsRequired();

                entity.Property(e => e.TypeRendezVous)
                    .HasColumnName("TypeRendezVous");

                entity.Property(e => e.Description)
                    .HasColumnName("Description");

                entity.Property(e => e.Prix)
                    .HasColumnName("Prix");

                entity.Property(e => e.Idevent)
                    .HasColumnName("Idevent");

                entity.Property(e => e.FichierJoint)
                    .HasColumnName("FichierJoint");

                entity.HasOne(e => e.Professionel)
                    .WithMany()
                    .HasForeignKey(e => e.ProfessionelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Patient)
                    .WithMany()
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("t_review");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasDefaultValueSql("NOW()");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50);

                entity.HasOne(e => e.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Comments)
                    .WithOne(c => c.Review)
                    .HasForeignKey(c => c.ReviewId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Social>(entity =>
            {
                entity.ToTable("t_social");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Youtube)
                    .HasColumnName("youtube")
                    .HasMaxLength(255);

                entity.Property(e => e.Twitter)
                    .HasColumnName("twitter")
                    .HasMaxLength(255);

                entity.Property(e => e.Facebook)
                    .HasColumnName("facebook")
                    .HasMaxLength(255);

                entity.Property(e => e.Linkedin)
                    .HasColumnName("linkedin")
                    .HasMaxLength(255);

                entity.Property(e => e.Instagram)
                    .HasColumnName("instagram")
                    .HasMaxLength(255);
            });
            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.ToTable("t_telephone");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.Property(e => e.Fix)
                    .HasColumnName("fix")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });
            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.ToTable("t_telephone");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                entity.Property(e => e.Fix)
                    .HasColumnName("fix")
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );
            });
        }

    }

}
