using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class ScheduleFullDto
{
    public Guid ScheduleId { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }

    public Guid TrainingId { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;
    public ScheduleMemberDto TrainerName { get; set; } = default!;
    public List<ScheduleMemberDto> ClientsName { get; set; } = [];
}
