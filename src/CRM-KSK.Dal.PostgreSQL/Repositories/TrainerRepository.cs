using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class TrainerRepository
{
    private readonly CRM_KSKDbContext _context;

    public TrainerRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task AddTrainerAsync(Trainer trainer)
    {
        _context.Trainers.Add(trainer);
        await _context.SaveChangesAsync();
    }

    public async Task<Trainer> GetTrainerByNameAsync(string name)
    {
        return await _context.Trainers.AsNoTracking().FirstOrDefaultAsync(n => n.FirstName == name);
    }
}
