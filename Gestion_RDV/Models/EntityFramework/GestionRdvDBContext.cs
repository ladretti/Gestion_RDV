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
            
        }

    }

}
