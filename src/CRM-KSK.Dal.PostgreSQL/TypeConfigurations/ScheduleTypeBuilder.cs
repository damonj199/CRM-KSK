using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class ScheduleTypeBuilder : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder
            .HasMany(s => s.Trainings)
            .WithOne(t => t.Schedule)
            .HasForeignKey(t => t.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
