using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FiscalManagement.Models;

namespace FiscalManagement.Data
{
    public class FiscalDbContext : IdentityDbContext
    {
        public FiscalDbContext(DbContextOptions<FiscalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contribuabil> Contribuabili { get; set; }
        public DbSet<Cerere> Cereri { get; set; }
        public DbSet<Audit> Audite { get; set; }
        public DbSet<Taskuri> Taskuri { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apelăm configurațiile implicite pentru Identity
            base.OnModelCreating(modelBuilder);

            // Relație: Cerere -> Contribuabil
            modelBuilder.Entity<Cerere>()
                .HasOne(c => c.Contribuabil)
                .WithMany()
                .HasForeignKey(c => c.ContribuabilID)
                .OnDelete(DeleteBehavior.Cascade);

            // Mapare explicită a entității Taskuri
            modelBuilder.Entity<Taskuri>().ToTable("Taskuri");
        }
    }
}
