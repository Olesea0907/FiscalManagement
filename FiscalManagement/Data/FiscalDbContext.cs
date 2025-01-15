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
        public DbSet<Plata> Plati { get; set; }
        public DbSet<Audit> Audite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurarea relației între `Cereri` și `Contribuabili`
            modelBuilder.Entity<Cerere>()
                .HasOne(c => c.Contribuabil)
                .WithMany()
                .HasForeignKey(c => c.ContribuabilID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurarea relației între `Plati` și `Contribuabili`
            modelBuilder.Entity<Plata>()
                .HasOne(p => p.Contribuabil)
                .WithMany()
                .HasForeignKey(p => p.ContribuabilID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurarea coloanei pentru fișier
            modelBuilder.Entity<Plata>()
                .Property(p => p.Fisier)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            modelBuilder.Entity<Plata>()
    .Property(p => p.Suma)
    .HasColumnType("decimal(18, 2)")
    .IsRequired();

        }

    }
}
