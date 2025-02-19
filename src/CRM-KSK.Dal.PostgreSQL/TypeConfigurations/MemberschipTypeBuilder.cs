using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM_KSK.Dal.PostgreSQL.TypeConfigurations;

internal sealed class MemberschipTypeBuilder : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder
            .HasOne(m => m.Client)
            .WithOne(c => c.Membership)
            .HasForeignKey<Membership>(m => m.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(x => x.StatusMembership)
            .HasConversion<string>();
    }
}
