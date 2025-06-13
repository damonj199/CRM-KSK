using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IHorsesRepository
{
    Task AddHorseWork(WorkHorse horse, CancellationToken token);
    Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token);
    Task<List<WorkHorse>> GetAllScheduleWorkHorses(CancellationToken token);
    Task<List<WorkHorse>> GetScheduleWorkHorsesWeek(DateOnly sDate, DateOnly eDate, CancellationToken token);
}