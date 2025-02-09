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

    public async Task<IReadOnlyList<Schedule>> GetWeeksSchedule(DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var schedules = await _context.Schedules
            .Where(s => s.Date >= start && s.Date <= end)
            .Include(t => t.Trainer)
            .Include(c => c.Clients)
            .ToListAsync(cancellationToken);

        return schedules;
    }

    public async Task AddOrUpdateSchedule(Schedule schedule, CancellationToken cancellationToken)
    {
        var existingSchedule = await _context.Schedules
            .Include(c => c.Clients)
            .Include(t => t.Trainer)
            .FirstOrDefaultAsync(s => s.Id == schedule.Id, cancellationToken);

        if(existingSchedule != null)
        {
            existingSchedule.Date = schedule.Date;
            existingSchedule.Time = schedule.Time;
            existingSchedule.TypeTrainings = schedule.TypeTrainings;

            var existingTrainer = await _context.Trainers.FindAsync(new object[] { schedule.Trainer.Id }, cancellationToken);
            if (existingTrainer != null)
            {
                existingSchedule.Trainer = existingTrainer;
            }
            else
            {
                _context.Attach(schedule.Trainer);
                existingSchedule.Trainer = schedule.Trainer;
            }

            existingSchedule.Clients.Clear();

            foreach(var client in schedule.Clients)
            {
                var existingClient = await _context.Clients.FindAsync(new object[] { client.Id }, cancellationToken);
                if (existingClient != null)
                {
                    existingSchedule.Clients.Add(existingClient);
                }
                else
                {
                    _context.Attach(client);
                    existingSchedule.Clients.Add(client);
                }
            }
        }
        else
        {
            var existingTrainer = await _context.Trainers.FindAsync(new object[] { schedule.Trainer.Id }, cancellationToken);
            if (existingTrainer != null)
            {
                schedule.Trainer = existingTrainer;
            }
            else
            {
                _context.Attach(schedule.Trainer);
            }

            var updatedClients = new List<Client>();
            foreach (var client in schedule.Clients)
            {
                var existingClient = await _context.Clients.FindAsync(new object[] { client.Id }, cancellationToken);
                if (existingClient != null)
                {
                    updatedClients.Add(existingClient);
                }
                else
                {
                    _context.Attach(client);
                    updatedClients.Add(client);
                }
            }
            schedule.Clients = updatedClients;

            _context.Schedules.Add(schedule);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
