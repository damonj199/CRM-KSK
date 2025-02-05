using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL;

public class CRM_KSKDbContext : DbContext
{
    public CRM_KSKDbContext(DbContextOptions<CRM_KSKDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Payment> Payment { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Trainer> Trainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Client>()
            .Property(x => x.LevelOfTraining)
            .HasConversion<string>();

        modelBuilder
            .Entity<Membership>()
            .Property(x => x.StatusMembership)
            .HasConversion<string>();

        modelBuilder
            .Entity<Payment>()
            .Property(x => x.PaymentMethod)
            .HasConversion<string>();

        modelBuilder.Entity<Client>()
        .HasOne(c => c.Membership)
        .WithOne(m => m.Client)
        .HasForeignKey<Membership>(m => m.ClientId);
    }
}
