using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleService
{
    Task DeleteSchedule(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(DateOnly weekStart, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleDto>> GetScheduleHistory(DateOnly start, DateOnly end, CancellationToken cancellationToken);
}