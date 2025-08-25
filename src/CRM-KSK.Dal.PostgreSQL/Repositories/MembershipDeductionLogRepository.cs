using CRM_KSK.Application.Dtos;
using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class MembershipDeductionLogRepository : IMembershipDeductionLogRepository
{
    private readonly CRM_KSKDbContext _context;
    private readonly ILogger<MembershipDeductionLogRepository> _logger;

    public MembershipDeductionLogRepository(CRM_KSKDbContext context, ILogger<MembershipDeductionLogRepository> logger)
    {
        _context = context;
        _logger = logger;
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

    public async Task<IEnumerable<MembershipDeductionLog>> GetLogsAsync(DateOnly date, CancellationToken token)
    {
        var logs = await _context.MembershipDeductionLogs
            .Include(l => l.Client)
            .Include(l => l.Schedule)
            .Include(l => l.Training)
            .Include(l => l.Training.Trainer)
            .Where(l => l.DeductionDate == date)
            .OrderByDescending(l => l.DeductionDate)
            .ToListAsync(token);

        return logs;
    }
} 