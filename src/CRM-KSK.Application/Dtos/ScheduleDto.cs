using CRM_KSK.Core.Enums;

namespace CRM_KSK.Application.Dtos;

public class ScheduleDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public Guid TrainerId { get; set; }
    public Guid ClientId { get; set; }
    public TypeTrainings TypeTrainings { get; set; }
}
