using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IHorsesRepository
{
    Task AddHorse(Horse horse, CancellationToken token);
    Task AddOrUpdateHorseWork(WorkHorse horse, CancellationToken token);
    Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token);
    Task<List<WorkHorse>> GetAllScheduleWorkHorses(CancellationToken token);
    Task<List<Horse>> GetHorsesNameWeek(DateOnly sDate, CancellationToken token);
    Task<List<WorkHorse>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token);
}