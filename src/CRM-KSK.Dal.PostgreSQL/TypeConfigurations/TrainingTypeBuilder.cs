using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class TrainingTypeBuilder : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder
            .HasOne(t => t.Trainer)
            .WithMany(tr => tr.Trainings)
            .HasForeignKey(t => t.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(t => t.Schedule)
            .WithMany(s => s.Trainings)
            .HasForeignKey(t => t.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasQueryFilter(t => t.Trainer == null || !t.Trainer.IsDeleted);
    }
}
