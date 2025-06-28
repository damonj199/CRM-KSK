using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IHorsesRepository
{
    Task AddHorse(Horse horse, CancellationToken token);
    Task AddWorkHorse(WorkHorse horse, CancellationToken token);
    Task<bool> DeleteHorseName(long id, CancellationToken token);
    Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token);
    Task<List<WorkHorse>> GetAllScheduleWorkHorses(CancellationToken token);
    Task<Horse> GetHorseNameById(long id, CancellationToken token);
    Task<List<Horse>> GetHorsesNameWeek(DateOnly sDate, CancellationToken token);
    Task<List<WorkHorse>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token);
    Task<WorkHorse> GetWorkHorseById(Guid id, CancellationToken token);
    Task UpdateHorseName(long id, string name, CancellationToken token);
    Task UpdateWorkHorse(Guid id, string content, CancellationToken token);
}