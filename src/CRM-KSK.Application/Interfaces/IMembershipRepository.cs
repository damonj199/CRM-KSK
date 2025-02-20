using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IMembershipRepository
{
    Task AddMembershipAsync(Membership membership, CancellationToken token);
    Task DeleteMembershipAsync(Guid id, CancellationToken token);
    Task<List<Membership>> GetAllMembershipClientAsync(Guid id, CancellationToken token);
    Task<Membership> GetMembershipByIdAsync(Guid id, CancellationToken token);
    Task UpdateMembershipAsync(Membership membership, CancellationToken token);
}