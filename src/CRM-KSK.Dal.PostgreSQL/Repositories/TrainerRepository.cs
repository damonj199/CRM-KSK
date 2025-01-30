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

    public async Task<List<Trainer>> GetTrainerByNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        var query = _context.Trainers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(firstName))
            query = query.Where(f => f.FirstName.ToLower().Contains(firstName.ToLower()));

        if (!string.IsNullOrWhiteSpace(lastName))
            query = query.Where(query => query.LastName.ToLower().Contains(lastName.ToLower()));

        return await query.ToListAsync();
    }

    public async Task DeleteTraner(Trainer trainer, CancellationToken cancellationToken)
    {
        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
