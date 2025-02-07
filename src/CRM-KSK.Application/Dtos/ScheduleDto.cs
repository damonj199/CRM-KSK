using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class ScheduleDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public ScheduleMemberDto Trainer { get; set; } = default!;
    public ICollection<ScheduleMemberDto> Clients { get; set; } = [];
    public TypeTrainings TypeTrainings { get; set; }
}
