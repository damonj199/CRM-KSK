using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleRepository
{
    Task AddOrUpdateSchedule(Schedule schedule, CancellationToken cancellationToken);
    Task<IReadOnlyList<Schedule>> GetWeeksSchedule(DateOnly start, DateOnly end, CancellationToken cancellationToken);
}