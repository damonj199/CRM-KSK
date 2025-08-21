using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Interfaces;

public interface IMembershipRepository
{
    Task AddMembershipAsync(List<Membership> memberships, CancellationToken token);
    Task SoftDeleteMembershipAsync(Guid id, CancellationToken token);
    Task<List<Membership>> GetAllMembershipClientAsync(Guid id, CancellationToken token);
    Task<List<Membership>> GetMembershipsByClientAndTypeAsync(Guid id, TypeTrainings type, CancellationToken token);
    Task<Membership> GetMembershipByIdAsync(Guid id, CancellationToken token);
    Task UpdateMembershipAsync(Membership membership, CancellationToken token);
}