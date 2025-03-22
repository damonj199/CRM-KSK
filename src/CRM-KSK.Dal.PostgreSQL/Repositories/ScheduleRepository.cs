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

    public async Task DeleteSchedule(Guid id, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules.FindAsync(id, cancellationToken);

        _context.Schedules.Remove(schedule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
