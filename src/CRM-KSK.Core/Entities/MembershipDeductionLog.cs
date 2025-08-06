using CRM_KSK.Core.Enums;

namespace CRM_KSK.Core.Entities;

public class MembershipDeductionLog
{
    public Guid Id { get; set; }
    public DateOnly DeductionDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public Guid ClientId { get; set; }
    public Guid MembershipId { get; set; }
    public Guid ScheduleId { get; set; }
    public Guid TrainingId { get; set; }
    public TypeTrainings TrainingType { get; set; }
    public int TrainingsBeforeDeduction { get; set; }
    public int TrainingsAfterDeduction { get; set; }
    public bool MembershipExpired { get; set; } = false;
    
    // Навигационные свойства
    public Client Client { get; set; } = default!;
    public Membership Membership { get; set; } = default!;
    public Schedule Schedule { get; set; } = default!;
    public Training Training { get; set; } = default!;
} 