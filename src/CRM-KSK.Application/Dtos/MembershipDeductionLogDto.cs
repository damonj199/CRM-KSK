using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class MembershipDeductionLogDto
{
    public Guid Id { get; set; }
    public DateOnly DeductionDate { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public TypeTrainings TrainingType { get; set; }
    public int TrainingsBeforeDeduction { get; set; }
    public int TrainingsAfterDeduction { get; set; }
    public bool MembershipExpired { get; set; }
    public bool IsMorningMembership { get; set; }
    public bool IsOneTimeTraining { get; set; }
    public string ScheduleDate { get; set; } = string.Empty;
    public string? TrainerName { get; set; }
}