using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IMembershipService
{
    Task AddMembershipAsync(List<MembershipDto> memberships, CancellationToken token);
    Task DeleteMembershipAsync(Guid id, CancellationToken token);
    Task<List<MembershipDto>> GetAllMembershipClientAsync(Guid id, CancellationToken token);
    Task UpdateMembershipAsync(MembershipDto membershipDto, CancellationToken token);
}