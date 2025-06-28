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

    public async Task AddHorse(Horse horse, CancellationToken token)
    {
        _context.Horses.Add(horse);
        await _context.SaveChangesAsync(token);
    }

    public async Task AddWorkHorse(WorkHorse horse, CancellationToken token)
    {
        _context.WorkHorses.Add(horse);
        await _context.SaveChangesAsync(token);
    }

    public async Task<Horse> GetHorseNameById(long id, CancellationToken token)
    {
        var horse = await _context.Horses.FindAsync(id, token);

        return horse;
    }

    public async Task<WorkHorse> GetWorkHorseById(Guid id, CancellationToken token)
    {
        var horse = await _context.WorkHorses.FindAsync(id, token);

        return horse;
    }

    public async Task<List<Horse>> GetHorsesNameWeek(DateOnly sDate, CancellationToken token)
    {
        var horses = await _context.Horses
            .Where(x => x.StartWeek == sDate)
            .ToListAsync(token);

        return horses ?? [];
    }

    public async Task<List<WorkHorse>> GetAllScheduleWorkHorses(CancellationToken token)
    {
        var allSchedduleHorses = await _context.WorkHorses.ToListAsync(token);

        return allSchedduleHorses ?? [];
    }

    public async Task<List<WorkHorse>> GetScheduleWorkHorsesWeek(DateOnly sDate, CancellationToken token)
    {
        var scheduleWeek = await _context.WorkHorses
            .Where(h => h.WeekStartDate == sDate)
            .OrderBy(h => h.Date)
            .ToListAsync(token);

        return scheduleWeek ?? [];
    }

    public async Task UpdateHorseName(long id, string name, CancellationToken token)
    {
        var horse = await _context.Horses.FindAsync(new object[] { id }, token);
        if (horse == null)
            return;

        horse.Name = name;
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateWorkHorse(Guid id, string content, CancellationToken token)
    {
        var workHorse = await _context.WorkHorses.FindAsync(new object[] { id }, token);
        if (workHorse == null)
            return;

        workHorse.ContentText = content;
        await _context.SaveChangesAsync(token);
    }

    public async Task<bool> DeleteHorseName(long id, CancellationToken token)
    {
        var horse = await _context.Horses.FindAsync(new object[] { id }, token);

        if (horse == null)
            return false;

        _context.Horses.Remove(horse);
        await _context.SaveChangesAsync(token);
        return true;
    }

    public async Task<bool> DeleteWorkHorseById(Guid id, CancellationToken token)
    {
        var workHorse = await _context.WorkHorses.FindAsync(new object[] { id }, token);

        if (workHorse == null)
            return false;

        _context.WorkHorses.Remove(workHorse);
        await _context.SaveChangesAsync(token);
        return true;
    }
}
