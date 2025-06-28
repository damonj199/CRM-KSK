using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class WorkHorseTypeBuilder : IEntityTypeConfiguration<HorseWork>
{
    public void Configure(EntityTypeBuilder<HorseWork> builder)
    {
        builder
            .Property(x => x.StartWeek)
            .HasColumnType("date");
    }
}
