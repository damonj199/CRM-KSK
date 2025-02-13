using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL;

public class CRM_KSKDbContext : DbContext
{
    public CRM_KSKDbContext(DbContextOptions<CRM_KSKDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Training> Payment { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Trainer> Trainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRM_KSKDbContext).Assembly);

        modelBuilder
            .Entity<Membership>()
            .Property(x => x.StatusMembership)
            .HasConversion<string>();
    }
}



