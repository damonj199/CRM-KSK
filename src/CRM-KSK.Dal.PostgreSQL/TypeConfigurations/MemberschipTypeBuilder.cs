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
            .WithMany(c => c.Memberships)
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.StatusMembership)
            .HasConversion<byte>();

        builder
            .HasQueryFilter(m => (m.Client == null || !m.Client.IsDeleted) && !m.IsDeleted);
    }
}
