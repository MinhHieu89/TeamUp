using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TeamUp.Models;

namespace TeamUp.Persistence
{
    public class TeamUpDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }

        public TeamUpDbContext(DbContextOptions<TeamUpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Team>()
                .HasOne(t => t.Captain)
                .WithOne(t => t.Team)
                .HasForeignKey<Team>(t => t.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasIndex(t => t.Name)
                .IsUnique();

            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.Name)
                .IsUnique();

        }
    }
}
