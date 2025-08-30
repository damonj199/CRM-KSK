using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using CRM_KSK.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRM_KSK.Dal.PostgreSQL.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly CRM_KSKDbContext _context;
    private readonly ILogger<MembershipRepository> _logger;

    public MembershipRepository(CRM_KSKDbContext context, ILogger<MembershipRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddMembershipAsync(List<Membership> memberships, CancellationToken token)
    {
        _logger.LogWarning($"Добавили абонемент для клиента {memberships.FirstOrDefault()?.Client.LastName}," +
            $"на {memberships.FirstOrDefault()?.AmountTraining} занятий, абонемент типа - {memberships.FirstOrDefault()?.TypeTrainings.ToString()}");

        _context.Memberships.AddRange(memberships);
        await _context.SaveChangesAsync(token);
    }

    public async Task<List<Membership>> GetAllMembershipClientAsync(Guid id, CancellationToken token)
    {
        var memberships = await _context.Memberships
            .Where(c => c.ClientId == id)
            .ToListAsync(token);

        return memberships ?? [];
    }

    public async Task<List<Membership>> GetMembershipsByClientAndTypeAsync(Guid id, TypeTrainings type, CancellationToken token)
    {
        var memberships = await _context.Memberships
            .Where(c => c.ClientId == id && c.TypeTrainings == type)
            .ToListAsync(token);
            
        return memberships ?? [];
    }

    public async Task<Membership> GetMembershipByIdAsync(Guid id, CancellationToken token)
    {
        var membership = await _context.Memberships.FindAsync(id, token);

        return membership;
    }

    public async Task UpdateMembershipAsync(Membership membership, CancellationToken token)
    {
        var exMembership = await _context.Memberships.FindAsync(membership.Id, token);
        if (exMembership != null)
        {
            exMembership.DateStart = membership.DateStart;
            exMembership.DateEnd = membership.DateEnd;
            exMembership.AmountTraining = membership.AmountTraining;
            exMembership.TypeTrainings = membership.TypeTrainings;
            exMembership.StatusMembership = membership.StatusMembership;
            exMembership.IsMorning = membership.IsMorning;
        }
        await _context.SaveChangesAsync(token);
    }

    public async Task SoftDeleteMembershipAsync(Guid id, CancellationToken token)
    {
        var mem = await _context.Memberships.FindAsync(id, token);
        if (mem != null)
        {
            _logger.LogWarning($"Абонемент {mem.TypeTrainings.ToString()}" +
                $"для клиента {mem.Client.LastName} {mem.Client.FirstName} был удален. Остаток занятий был {mem.AmountTraining}");

            mem.SoftDelete();
            await _context.SaveChangesAsync(token);
        }
    }
}
