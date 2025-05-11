using CRM_KSK.Core.Entities;

namespace CRM_KSK.Application.Interfaces;

public interface IScheduleRepository
{
    Task AddCommentAsync(DateOnly date, TimeSpan time, string comment, CancellationToken token);
    Task AddOrUpdateSchedule(Schedule schedule, CancellationToken cancellationToken);
    Task DeleteComment(int id, CancellationToken token);
    Task<List<ScheduleComment>> GetCommentsAsync(CancellationToken token);
    Task<IReadOnlyList<Schedule>> GetWeeksSchedule(DateOnly start, DateOnly end, CancellationToken cancellationToken);
    Task<List<Schedule>> GetYesterdaysScheduleAsync(DateOnly day, CancellationToken token);
}