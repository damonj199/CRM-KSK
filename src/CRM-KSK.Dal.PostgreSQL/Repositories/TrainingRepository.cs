using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class TrainingRepository : ITrainingRepository
{
    private readonly CRM_KSKDbContext _context;

    public TrainingRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddTainingAsync(Training training, CancellationToken token)
    {
        var schedule = await _context.Schedules.FindAsync(training.ScheduleId);
        training.Schedule = schedule;

        var trainer = await _context.Trainers.FindAsync(training.Trainer.Id);
        training.Trainer = trainer;

        var original = training.Clients.ToList();
        training.Clients.Clear();

        foreach (var client in original)
        {
            var existingClient = await _context.Clients.FindAsync(client.Id);
            if(existingClient != null)
            {
                training.Clients.Add(existingClient);
            }
        }
        _context.Trainings.Add(training);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteTrainingAsync(Guid id, CancellationToken token)
    {
        var training = await _context.Trainings.FindAsync(id);
        if (training != null)
        {
            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync(token);
        }
    }
}
