using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleRepository
{
    Task AddOrUpdateSchedule(Schedule schedule, CancellationToken cancellationToken);
    Task DeleteSchedule(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Schedule>> GetWeeksSchedule(DateOnly start, DateOnly end, CancellationToken cancellationToken);
    Task<List<Schedule>> GetYesterdaysScheduleAsync(DateOnly day, CancellationToken token);
}