using CRM_KSK.Application.Dtos;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleService
{
    Task<IReadOnlyList<ScheduleDto>> GetWeeksSchedule(DateOnly weekStart, CancellationToken cancellationToken);
    Task<IReadOnlyList<ScheduleDto>> GetScheduleHistory(DateOnly start, DateOnly end, CancellationToken cancellationToken);
    Task DeleteComment(int id, CancellationToken token);
    Task<List<ScheduleCommentDto>> GetScheduleComments(CancellationToken token);
    Task AddScheduleComment(ScheduleCommentDto commentDto, CancellationToken token);
}