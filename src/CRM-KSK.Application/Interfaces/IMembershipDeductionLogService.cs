using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IMembershipDeductionLogService
{
    Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsAsync(DateOnly date, CancellationToken token);
    Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsByClientAsync(Guid clientId, CancellationToken token);
    Task<int> GetDeductionLogsCountAsync(DateOnly date, CancellationToken token);
} 