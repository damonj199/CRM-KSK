using CRM_KSK.Application.Dtos;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IMembershipDeductionLogRepository
{
    Task<MembershipDeductionLog> CreateLogAsync(MembershipDeductionLog log, CancellationToken token);
    Task<IEnumerable<MembershipDeductionLogDto>> GetLogsAsync(DateOnly date, CancellationToken token);
    Task<IEnumerable<MembershipDeductionLogDto>> GetLogsByClientAsync(Guid clientId, CancellationToken token);
    Task<int> GetLogsCountAsync(DateOnly date, CancellationToken token);
} 