using Microsoft.EntityFrameworkCore;
using @switch.domain.Entities;

namespace @switch.infrastructure.DAL
{
    public class SwitchDbContext : DbContext
    {
        public DbSet<SwitchToggle> SwitchToggles { get; set; }

        public SwitchDbContext(DbContextOptions<SwitchDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SwitchToggle>().ToTable("SwitchToggles");
            modelBuilder.Entity<SwitchToggle>().HasKey(t => t.Id);
            modelBuilder.Entity<SwitchToggle>().Property(t => t.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<SwitchToggle>().Property(t => t.Description).HasMaxLength(250);
        }
    }
}