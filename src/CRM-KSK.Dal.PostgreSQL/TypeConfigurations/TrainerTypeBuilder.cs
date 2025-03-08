using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class TrainerTypeBuilder : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder
            .HasMany(t => t.Trainings)
            .WithOne(tr => tr.Trainer)
            .HasForeignKey(tr => tr.TrainerId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasQueryFilter(t => !t.IsDeleted)
    }

}
