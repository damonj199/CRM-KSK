using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class BirtDaysRepository : IBirtDaysRepository
{
    private readonly CRM_KSKDbContext _context;

    public BirtDaysRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task<List<BirthdayNotification>> GetAllFromBodAsync(CancellationToken token)
    {
        var people = await _context.BirthDays.ToListAsync(token);
        return people;
    }

    public async Task DeleteAllDataAsync(CancellationToken token)
    {
        var people = await GetAllFromBodAsync(token);

        _context.BirthDays.RemoveRange(people);
        await _context.SaveChangesAsync(token);
    }

    public async Task AddPeopleWithBirthDaysThisMonth(List<BirthdayNotification> people, CancellationToken token)
    {
        if (people != null)
            await _context.BirthDays.AddRangeAsync(people, token);

        await _context.SaveChangesAsync(token);
    }
}
