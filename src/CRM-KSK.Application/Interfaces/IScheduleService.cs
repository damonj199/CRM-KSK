using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleService
{
    Task AddOrUpdateSchedule(ScheduleDto scheduleDto, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(CancellationToken cancellationToken);
}