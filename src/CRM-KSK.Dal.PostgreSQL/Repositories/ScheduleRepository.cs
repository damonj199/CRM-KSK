using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly CRM_KSKDbContext _context;

    public ScheduleRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddOrUpdateSchedule(Schedule schedule, CancellationToken cancellationToken)
    {
        var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == schedule.Id);
        if (existingSchedule == null)
        {
            _context.Schedules.Add(schedule);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task AddCommentAsync(DateOnly date, TimeSpan time, string comment, CancellationToken token)
    {
        var newComment = new ScheduleComment
        {
            Date = date,
            Time = time,
            CommentText = comment
        };
        _context.ScheduleComments.Add(newComment);
        await _context.SaveChangesAsync(token);
    }

    public async Task<List<ScheduleComment>> GetCommentsAsync(CancellationToken token)
    {
        var comments = await _context.ScheduleComments.ToListAsync(token);

        return comments ?? [];
    }

    public async Task<IReadOnlyList<Schedule>> GetWeeksSchedule(DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var schedules = await _context.Schedules
            .Where(s => s.Date >= start && s.Date <= end)
            .Include(tr => tr.Trainings)
                .ThenInclude(t => t.Trainer)
            .Include(tr => tr.Trainings)
                .ThenInclude(c => c.Clients)
             .IgnoreQueryFilters()
            .ToListAsync(cancellationToken);

        return schedules;
    }

    public async Task<List<Schedule>> GetYesterdaysScheduleAsync(DateOnly day, CancellationToken token)
    {
        var schedules = await _context.Schedules
            .Where(t => t.Date == day)
            .Include(c => c.Trainings)
                .ThenInclude(c => c.Clients)
            .ToListAsync(token);

        return schedules ?? [];
    }

    public async Task DeleteComment(int id, CancellationToken token)
    {
        var comment = await _context.ScheduleComments.FindAsync(id, token);
        if (comment != null)
        {
            _context.ScheduleComments.Remove(comment);
            await _context.SaveChangesAsync(token);
        }
    }
}
