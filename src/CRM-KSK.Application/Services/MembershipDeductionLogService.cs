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
        var entities = await _logRepository.GetLogsAsync(date, token);
        
        var dtos = entities.Select(entity => new MembershipDeductionLogDto
        {
            Id = entity.Id,
            DeductionDate = entity.DeductionDate,
            ClientFullName = $"{entity.Client?.LastName} {entity.Client?.FirstName}",
            TrainingType = entity.TrainingType,
            TrainingsBeforeDeduction = entity.TrainingsBeforeDeduction,
            TrainingsAfterDeduction = entity.TrainingsAfterDeduction,
            MembershipExpired = entity.MembershipExpired,
            IsMorningMembership = entity.IsMorningMembership,
            IsOneTimeTraining = entity.IsOneTimeTraining,
            ScheduleDate = entity.Schedule?.Date.ToString("dd.MM.yyyy") ?? "",
            TrainerName = entity.Training?.Trainer != null ? $"{entity.Training.Trainer.LastName} {entity.Training.Trainer.FirstName}" : null
        }).ToList();
        
        return dtos;
    }
} 