using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class TrainingDto
{
    public Guid Id { get; set; }
    public TypeTrainings TypeTrainings { get; set; } = TypeTrainings.Unknown;
    public ScheduleDto Schedule { get; set; } = default!;
    public ScheduleMemberDto TrainerName { get; set; } = default!;
    public List<ScheduleMemberDto> ClientsName { get; set; } = [];
}
