using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class ScheduleTypeBuilder : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder
            .HasOne(s => s.Trainer)
            .WithMany(t => t.Schedules);

        builder
            .HasMany(s => s.Clients)
            .WithMany(c => c.Schedules)
            .UsingEntity(j => j.ToTable("ScheduleClients"));
    }
}
