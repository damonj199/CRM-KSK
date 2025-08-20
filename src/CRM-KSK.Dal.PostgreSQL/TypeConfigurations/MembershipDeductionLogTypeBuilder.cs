using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

public class MembershipDeductionLogTypeBuilder : IEntityTypeConfiguration<MembershipDeductionLog>
{
    public void Configure(EntityTypeBuilder<MembershipDeductionLog> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.DeductionDate)
            .IsRequired();

        // Навигационные свойства
        builder.HasOne(x => x.Client)
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Membership)
            .WithMany()
            .HasForeignKey(x => x.MembershipId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Schedule)
            .WithMany()
            .HasForeignKey(x => x.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Training)
            .WithMany()
            .HasForeignKey(x => x.TrainingId)
            .OnDelete(DeleteBehavior.SetNull);

        // Индексы для улучшения производительности
        builder.HasIndex(x => x.ClientId);
        builder.HasIndex(x => x.DeductionDate);
        builder.HasIndex(x => x.TrainingType);
        builder.HasIndex(x => x.MembershipExpired);
    }
} 