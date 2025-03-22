using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class ClientTypeBuilder : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder
            .HasMany(c => c.Memberships)
            .WithOne(m => m.Client)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}
