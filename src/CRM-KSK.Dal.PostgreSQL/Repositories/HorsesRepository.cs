using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class HorsesRepository : IHorsesRepository
{
    private readonly CRM_KSKDbContext _context;

    public HorsesRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    //Перенести все проверки в слой бизнес логики

    public async Task AddHorseWork(WorkHorse horse, CancellationToken token)
    {
        _context.WorkHorses.Add(horse);
        await _context.SaveChangesAsync(token);
    }

    public async Task<List<WorkHorse>> GetAllScheduleWorkHorses(CancellationToken token)
    {
        var allSchedduleHorses = await _context.WorkHorses.ToListAsync(token);

        return allSchedduleHorses ?? [];
    }

    public async Task<List<WorkHorse>> GetScheduleWorkHorsesWeek(DateOnly sDate, DateOnly eDate, CancellationToken token)
    {
        var scheduleWeek = await _context.WorkHorses
            .Where(h => h.Date >= eDate && h.Date < eDate)
            .OrderBy(h => h.Date)
            .ToListAsync(token);

        return scheduleWeek ?? [];
    }

    public async Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var content = await _context.WorkHorses
            .Where(h => h.Date >= today)
            .FirstOrDefaultAsync(h => h.Id == id, token);

        if (content != null)
        {
            _context.WorkHorses.Remove(content);
            await _context.SaveChangesAsync(token);
            return true;
        }

        return false;
    }
}
