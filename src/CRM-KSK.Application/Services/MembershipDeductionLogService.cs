using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;

namespace CRM_KSK.Application.Services;

public class MembershipDeductionLogService : IMembershipDeductionLogService
{
    private readonly IMembershipDeductionLogRepository _logRepository;

    public MembershipDeductionLogService(IMembershipDeductionLogRepository logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetDeductionLogsAsync(DateOnly date, CancellationToken token)
    {
        return await _logRepository.GetLogsAsync(date, token);
    }
} 