using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class ClientTypeBuilder : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .Property(x => x.LevelOfTraining)
            .HasConversion<string>();

        builder
            .HasOne(c => c.Membership)
            .WithOne(m => m.Client)
            .HasForeignKey<Membership>(m => m.ClientId);
    }
}
