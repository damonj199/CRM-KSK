using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL;

public class CRM_KSKDbContext : DbContext
{
    public CRM_KSKDbContext(DbContextOptions<CRM_KSKDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Admin> Admins { get; set; }

}
