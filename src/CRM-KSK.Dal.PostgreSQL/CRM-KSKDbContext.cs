using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL;

public class CRM_KSKDbContext : DbContext
{
    public CRM_KSKDbContext(DbContextOptions<CRM_KSKDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Horse> Horses { get; set; }
    public DbSet<WorkHorse> WorkHorses { get; set; }
    public DbSet<ScheduleComment> ScheduleComments { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<BirthdayNotification> BirthDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRM_KSKDbContext).Assembly);
    }
}



