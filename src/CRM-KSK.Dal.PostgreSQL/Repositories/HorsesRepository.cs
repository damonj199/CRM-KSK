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
        var existing = await _context.Horses.FirstOrDefaultAsync(x =>
            x.StartWeek == horse.StartWeek &&
            x.RowNumber == horse.RowNumber, token);

        if (existing != null && !string.IsNullOrWhiteSpace(horse.Name))
        {
            existing.Name = horse.Name;
            await _context.SaveChangesAsync(token);
            return;
        }

        _context.Horses.Add(horse);
        await _context.SaveChangesAsync(token);
    }

    public async Task AddOrUpdateHorseWork(WorkHorse horse, CancellationToken token)
    {
        var existing = await _context.WorkHorses.FirstOrDefaultAsync(x =>
            x.RowNumber == horse.RowNumber &&
            x.Date == horse.Date &&
            x.WeekStartDate == horse.WeekStartDate, token);

        var isEmptyCell = string.IsNullOrWhiteSpace(horse.ContentText);

        if (existing != null)
        {
            if (isEmptyCell)
            {
                // Удаляем запись, если она стала пустой
                _context.WorkHorses.Remove(horse);
            }
            else
            {
                // Обновляем существующую запись
                existing.ContentText = horse.ContentText?.Trim();
            }
        }
        else
        {
            if (!isEmptyCell)
            {
                // Создаём новую запись
                var newHorse = new WorkHorse
                {
                    RowNumber = horse.RowNumber,
                    Date = horse.Date,
                    ContentText = horse.ContentText?.Trim()
                };

                newHorse.GetCurrentWeekMonday(horse.Date);

                _context.WorkHorses.Add(newHorse);
            }
        }

        await _context.SaveChangesAsync(token);
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
