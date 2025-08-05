using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class MembershipDeductionLogRepository : IMembershipDeductionLogRepository
{
    private readonly CRM_KSKDbContext _context;

    public MembershipDeductionLogRepository(CRM_KSKDbContext context)
    {
        _context = context;
    }

    public async Task<MembershipDeductionLog> CreateLogAsync(MembershipDeductionLog log, CancellationToken token)
    {
        try
        {
            _context.MembershipDeductionLogs.Add(log);
            await _context.SaveChangesAsync(token);
            return log;
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            throw new InvalidOperationException($"Ошибка при создании лога списания: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetLogsAsync(DateOnly date, CancellationToken token)
    {
        var logs = await _context.MembershipDeductionLogs
            .Include(l => l.Client)
            .Include(l => l.Schedule)
            .Include(l => l.Training)
            .Include(l => l.Training.Trainer)
            .Where(l => l.DeductionDate == date)
            .OrderByDescending(l => l.DeductionDate)
            .Select(l => new MembershipDeductionLogDto
            {
                Id = l.Id,
                DeductionDate = l.DeductionDate,
                ClientFullName = $"{l.Client.LastName} {l.Client.FirstName}",
                TrainingType = l.TrainingType,
                TrainingsBeforeDeduction = l.TrainingsBeforeDeduction,
                TrainingsAfterDeduction = l.TrainingsAfterDeduction,
                MembershipExpired = l.MembershipExpired,
                ScheduleDate = l.Schedule.Date.ToString("dd.MM.yyyy"),
                TrainerName = $"{l.Training.Trainer.LastName} {l.Training.Trainer.FirstName}"
            })
            .ToListAsync(token);

        return logs;
    }

    public async Task<IEnumerable<MembershipDeductionLogDto>> GetLogsByClientAsync(Guid clientId, CancellationToken token)
    {
        var logs = await _context.MembershipDeductionLogs
            .Include(l => l.Client)
            .Include(l => l.Schedule)
            .Include(l => l.Training)
            .Include(l => l.Training.Trainer)
            .Where(l => l.ClientId == clientId)
            .OrderByDescending(l => l.DeductionDate)
            .Select(l => new MembershipDeductionLogDto
            {
                Id = l.Id,
                DeductionDate = l.DeductionDate,
                ClientFullName = $"{l.Client.LastName} {l.Client.FirstName}",
                TrainingType = l.TrainingType,
                TrainingsBeforeDeduction = l.TrainingsBeforeDeduction,
                TrainingsAfterDeduction = l.TrainingsAfterDeduction,
                MembershipExpired = l.MembershipExpired,
                ScheduleDate = l.Schedule.Date.ToString("dd.MM.yyyy"),
                TrainerName = $"{l.Training.Trainer.LastName} {l.Training.Trainer.FirstName}"
            })
            .ToListAsync(token);

        return logs;
    }

    public async Task<int> GetLogsCountAsync(DateOnly date, CancellationToken token)
    {
        return await _context.MembershipDeductionLogs
            .Where(l => l.DeductionDate == date)
            .CountAsync(token);
    }
} 