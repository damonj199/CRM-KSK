using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces
{
    public interface IHorsesWorkService
    {
        Task AddHorse(HorseDto horseDto, CancellationToken token);
        Task<bool> AddWorkHorse(WorkHorseDto horseDto, CancellationToken token);
        Task<bool> DeleteHorseById(long id, CancellationToken token);
        Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token);
        Task<List<WorkHorseDto>> GetAllScheduleWorkHorses(CancellationToken token);
        Task<List<HorseDto>> GetHorsesNameWeek(DateOnly sDate, CancellationToken token);
        Task<List<WorkHorseDto>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token);
        Task UpdateHorseName(long id, string name, CancellationToken token);
        Task UpdateWorkHorse(Guid id, string content, CancellationToken token);
    }
}