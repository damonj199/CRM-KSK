using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly CRM_KSKDbContext _context;

    public TrainerRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddTrainerAsync(Trainer trainer, CancellationToken cancellationToken)
    {
        _context.Trainers.Add(trainer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Trainer>> GetTrainersAsync(CancellationToken cancellationToken)
    {
        var trainits = await _context.Trainers.AsNoTracking().ToListAsync();
        return trainits;
    }

    public async Task<Trainer> GetTrainerByNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var query = _context.Trainers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(f => f.FirstName.ToLower().Contains(firstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(query => query.LastName.ToLower().Contains(lastName.ToLower()));

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Trainer> GetTrainerByIdAsync(Guid id, CancellationToken token)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        return trainer;
    }

    public async Task UpdateTrainerInfoAsync(Trainer trainer, CancellationToken token)
    {
        var existingTrainer = await _context.Trainers.FindAsync(trainer.Id);
        if(existingTrainer != null)
        {
            existingTrainer.FirstName = trainer.FirstName;
            existingTrainer.LastName = trainer.LastName;
            existingTrainer.Surname = trainer.Surname;
            existingTrainer.DateOfBirth = trainer.DateOfBirth;
            existingTrainer.Phone = trainer.Phone;
            existingTrainer.Color = trainer.Color;
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteTraner(Guid id, CancellationToken cancellationToken)
    {
        var trainer = new Trainer { Id = id };

        _context.Trainers.Attach(trainer);
        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
