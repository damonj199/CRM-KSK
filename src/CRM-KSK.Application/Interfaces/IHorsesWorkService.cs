using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces
{
    public interface IHorsesWorkService
    {
        Task<bool> AddHorsesWork(WorkHorseDto horseDto, CancellationToken token);
        Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token);
        Task<List<WorkHorseDto>> GetAllScheduleWorkHorses(CancellationToken token);
        Task<List<WorkHorseDto>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token);
    }
}