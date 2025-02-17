using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface ITrainingService
{
    Task AddTrainingAsync(ScheduleFullDto scheduleFull, CancellationToken token);
    Task DeleteTrainingAsync(Guid id, CancellationToken token);
}